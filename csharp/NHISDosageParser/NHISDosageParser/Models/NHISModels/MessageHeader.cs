namespace NHISDosageParser.Models.NHISModels
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageHeader
    {
        private messageHeaderSender senderField;

        private messageHeaderSenderId senderIdField;

        private messageHeaderSenderISName senderISNameField;

        private messageHeaderRecipient recipientField;

        private messageHeaderRecipientId recipientIdField;

        private messageHeaderMessageId messageIdField;

        private messageHeaderMessageType messageTypeField;

        private messageHeaderCreatedOn createdOnField;

        public messageHeaderSender sender
        {
            get
            {
                return this.senderField;
            }
            set
            {
                this.senderField = value;
            }
        }

        public messageHeaderSenderId senderId
        {
            get
            {
                return this.senderIdField;
            }
            set
            {
                this.senderIdField = value;
            }
        }

        public messageHeaderSenderISName senderISName
        {
            get
            {
                return this.senderISNameField;
            }
            set
            {
                this.senderISNameField = value;
            }
        }

        public messageHeaderRecipient recipient
        {
            get
            {
                return this.recipientField;
            }
            set
            {
                this.recipientField = value;
            }
        }

        public messageHeaderRecipientId recipientId
        {
            get
            {
                return this.recipientIdField;
            }
            set
            {
                this.recipientIdField = value;
            }
        }

        public messageHeaderMessageId messageId
        {
            get
            {
                return this.messageIdField;
            }
            set
            {
                this.messageIdField = value;
            }
        }

        public messageHeaderMessageType messageType
        {
            get
            {
                return this.messageTypeField;
            }
            set
            {
                this.messageTypeField = value;
            }
        }

        public messageHeaderCreatedOn createdOn
        {
            get
            {
                return this.createdOnField;
            }
            set
            {
                this.createdOnField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageHeaderSender
    {

        private int valueField;

        /// <summary>
        /// Тип изпращач на съобщението
        /// CL018
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int value
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
    public partial class messageHeaderSenderId
    {

        private string valueField;

        /// <summary>
        /// ID na лицето, което изпраща съобщението (УИН на лекар/зъболекар/специалист)
        /// </summary>
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
    public partial class messageHeaderSenderISName
    {

        private string valueField;

        /// <summary>
        /// Име и версия на информационната система, изпратила съобщението
        /// </summary>
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
    public partial class messageHeaderRecipient
    {

        private int valueField;

        /// <summary>
        /// Тип на получател на съобщението
        /// CL018
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int value
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
    public partial class messageHeaderRecipientId
    {

        private string valueField;

        /// <summary>
        /// ID на получателя на съобщението
        /// </summary>
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
    public partial class messageHeaderMessageId
    {

        private string valueField;

        /// <summary>
        /// Уникален идентификатор на съобщението (препоръчително е да се изпраща UUID или пореден целочислен номер)
        /// </summary>
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
    public partial class messageHeaderMessageType
    {

        private string valueField;

        /// <summary>
        /// Тип на съобщението (R001)
        /// </summary>
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
    public partial class messageHeaderCreatedOn
    {

        private System.DateTime valueField;

        /// <summary>
        /// Дата и час на изготвяне на съобщението по ISO 8601
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime value
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
}
