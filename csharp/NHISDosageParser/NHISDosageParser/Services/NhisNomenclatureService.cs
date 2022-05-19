using Newtonsoft.Json;
using NHISDosageParser.Contracts;
using NHISDosageParser.Extensions;
using NHISDosageParser.Models;
using NHISDosageParser.Models.NHISModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NHISDosageParser.Services
{
    /// <summary>
    /// Loads and updates 
    /// NHIS Dosage Parser nomenclatures
    /// </summary>
    public class NhisNomenclatureService : INhisNomenclatureService
    {
        private readonly string nomenclaturesDirectory;

        public NhisNomenclatureService()
        {
            nomenclaturesDirectory = Path
                .Combine(Path.GetDirectoryName(Assembly
                .GetEntryAssembly()
                .Location), "Nomenclatures") + Path.DirectorySeparatorChar;
        }

        /// <summary>
        /// Load nomenclatures from files
        /// </summary>
        /// <returns>Tuple with loaded nomenclatures</returns>
        public (NhisNomenclature cl013, NhisNomenclature cl020, NhisNomenclature cl034, NhisNomenclature cl035) LoadNomenclatures()
        {
            NhisNomenclature cl013 = JsonConvert.DeserializeObject<NhisNomenclature>(File.ReadAllText($"{nomenclaturesDirectory}cl013.json"));
            NhisNomenclature cl020 = JsonConvert.DeserializeObject<NhisNomenclature>(File.ReadAllText($"{nomenclaturesDirectory}cl020.json"));
            NhisNomenclature cl034 = JsonConvert.DeserializeObject<NhisNomenclature>(File.ReadAllText($"{nomenclaturesDirectory}cl034.json"));
            NhisNomenclature cl035 = JsonConvert.DeserializeObject<NhisNomenclature>(File.ReadAllText($"{nomenclaturesDirectory}cl035.json"));

            return (cl013, cl020, cl034, cl035);
        }

        /// <summary>
        /// Downloads nomenclatures from NHIS 
        /// and saves them to files
        /// </summary>
        /// <returns></returns>
        public async Task UpdateNomenclatures()
        {
            string data = PrepareRequestBody();
            var result = await Request("https://api.his.bg/v1/nomenclatures/all/get", HttpMethod.Post, data);

            if (result.IsSuccessStatusCode)
            {
                byte[] nom = await result.Content.ReadAsByteArrayAsync();

                XmlSerializer serializer = new XmlSerializer(typeof(C002));
                C002 message = null;

                using (MemoryStream ms = new MemoryStream(nom))
                {
                    message = serializer.Deserialize(ms) as C002;
                }

                if (message != null)
                {
                    foreach (var nomenclature in message.contents.nomenclature)
                    {
                        switch (nomenclature.nomenclatureId.value.ToUpper())
                        {
                            case "CL013":
                            case "CL034":
                                SaveOnlyNomenclature(nomenclature.entry, nomenclature.nomenclatureId.value.ToLower());
                                break;
                            case "CL020":
                            case "CL035":
                                SaveWithPlural(nomenclature.entry, nomenclature.nomenclatureId.value.ToLower());
                                break;
                            default:
                                break;
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// Saves nomenclature to file
        /// </summary>
        /// <param name="entry">Nomenclature info</param>
        /// <param name="name">Nomenclature name</param>
        /// <returns></returns>
        private void SaveOnlyNomenclature(messageContentsNomenclatureEntry[] entry, string name)
        {
            NhisNomenclature nomenclature = new NhisNomenclature()
            {
                DateUpdated = DateTime.UtcNow,
                Nom = new Dictionary<string, string>(),
                NomPlural = new Dictionary<string, string>()
            };

            foreach (var item in entry)
            {
                nomenclature.Nom.Add(item.key.value, item.description.value.Trim());
            }

            string strNomenclature = JsonConvert.SerializeObject(nomenclature);

            File.WriteAllText($"{nomenclaturesDirectory}{name}.json", strNomenclature);
        }

        /// <summary>
        /// Saves nomenclature info and pluralized form
        /// </summary>
        /// <param name="entry">Nomenclature info</param>
        /// <param name="name">Nomenclature name</param>
        /// <returns></returns>
        private void SaveWithPlural(messageContentsNomenclatureEntry[] entry, string name)
        {
            NhisNomenclature nomenclature = new NhisNomenclature()
            {
                DateUpdated = DateTime.UtcNow,
                Nom = new Dictionary<string, string>(),
                NomPlural = new Dictionary<string, string>()
            };

            foreach (var item in entry)
            {
                nomenclature.Nom.Add(item.key.value, item.description.value.Trim());
                nomenclature.NomPlural.Add(
                    item.key.value, 
                    item.meta
                    .Where(m => m.name.value == "plural")
                    .FirstOrDefault()?.value.value.Trim() ?? String.Empty);
            }

            string strNomenclature = JsonConvert.SerializeObject(nomenclature);

            File.WriteAllText($"{nomenclaturesDirectory}{name}.json", strNomenclature);
        }

        /// <summary>
        /// Prepares message data for C001 request
        /// </summary>
        /// <returns></returns>
        private string PrepareRequestBody()
        {
            string result = string.Empty;

            C001 message = new C001()
            {
                header = new messageHeader()
                {
                    createdOn = new messageHeaderCreatedOn() { value = DateTime.UtcNow },
                    messageId = new messageHeaderMessageId() { value = Guid.NewGuid().ToString() },
                    messageType = new messageHeaderMessageType() { value = "C001" },
                    recipient = new messageHeaderRecipient() { value = 4 },
                    recipientId = new messageHeaderRecipientId() { value = "NHIS" },
                    sender = new messageHeaderSender() { value = 4 },
                    senderId = new messageHeaderSenderId() { value = "0000000002" },
                    senderISName = new messageHeaderSenderISName() { value = "DoseParserC#" }
                },
                contents = new messageC001Contents()
                {
                    nomenclatureId = new messageC001ContentsNomenclatureId[] 
                    { 
                        new messageC001ContentsNomenclatureId() { value = "CL013" },
                        new messageC001ContentsNomenclatureId() { value = "CL020" },
                        new messageC001ContentsNomenclatureId() { value = "CL034" },
                        new messageC001ContentsNomenclatureId() { value = "CL035" }
                    }
                }
            };

            XmlSerializer serializer = new XmlSerializer(typeof(C001));

            using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, message);
                result = writer.ToString();
            }

            return result;
        }

        /// <summary>
        /// Requests data from NHIS
        /// </summary>
        /// <param name="url">URL of the requested resource</param>
        /// <param name="method">Request Method</param>
        /// <param name="data">Request body</param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> Request(string url, HttpMethod method, string data = null)
        {
            var request = new HttpRequestMessage(method, url);
            //request.Version = new Version(2, 0);
            HttpClient client = new HttpClient();

            if (data != null)
            {
                request.Content = new StringContent(data, Encoding.UTF8, "application/xml");
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            return await client.SendAsync(request);
        }
    }
}
