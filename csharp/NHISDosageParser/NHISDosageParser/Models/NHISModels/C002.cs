using System;

namespace NHISDosageParser.Models.NHISModels
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    [System.Xml.Serialization.XmlRootAttribute("message", Namespace = "https://www.his.bg", IsNullable = false)]
    public partial class C002
    {

        private messageHeader headerField;

        private messageC002Contents contentsField;

        public C002()
        {

        }

        public C002(messageHeader _header)
        {
            header = new messageHeader()
            {
                createdOn = new messageHeaderCreatedOn() { value = DateTime.Now },
                messageId = new messageHeaderMessageId() { value = _header.messageId.value },
                messageType = new messageHeaderMessageType() { value = "C002" },
                recipient = new messageHeaderRecipient() { value = _header.sender.value },
                recipientId = new messageHeaderRecipientId() { value = _header.senderId.value },
                sender = new messageHeaderSender() { value = 4 },
                senderId = new messageHeaderSenderId() { value = "NHIS" }
            };
        }

        /// <remarks/>
        public messageHeader header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        public messageC002Contents contents
        {
            get
            {
                return this.contentsField;
            }
            set
            {
                this.contentsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageNomenclature
    {

        private messageContentsNomenclatureNomenclatureId nomenclatureIdField;

        private messageContentsNomenclatureEntry[] entryField;

        /// <remarks/>
        public messageContentsNomenclatureNomenclatureId nomenclatureId
        {
            get
            {
                return this.nomenclatureIdField;
            }
            set
            {
                this.nomenclatureIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("entry")]
        public messageContentsNomenclatureEntry[] entry
        {
            get
            {
                return this.entryField;
            }
            set
            {
                this.entryField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureNomenclatureId
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureEntry
    {

        private messageContentsNomenclatureEntryKey keyField;

        private messageContentsNomenclatureEntryDescription descriptionField;

        private messageContentsNomenclatureEntryMeta[] metaField;

        /// <remarks/>
        public messageContentsNomenclatureEntryKey key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        public messageContentsNomenclatureEntryDescription description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("meta")]
        public messageContentsNomenclatureEntryMeta[] meta
        {
            get
            {
                return this.metaField;
            }
            set
            {
                this.metaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureEntryKey
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureEntryDescription
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureEntryMeta
    {

        private messageContentsNomenclatureEntryMetaName nameField;

        private messageContentsNomenclatureEntryMetaValue valueField;

        /// <remarks/>
        public messageContentsNomenclatureEntryMetaName name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public messageContentsNomenclatureEntryMetaValue value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureEntryMetaName
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageContentsNomenclatureEntryMetaValue
    {

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageC002Contents
    {
        private messageNomenclature[] nomenclatureField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("nomenclature")]
        public messageNomenclature[] nomenclature
        {
            get
            {
                return this.nomenclatureField;
            }
            set
            {
                this.nomenclatureField = value;
            }
        }
    }

}