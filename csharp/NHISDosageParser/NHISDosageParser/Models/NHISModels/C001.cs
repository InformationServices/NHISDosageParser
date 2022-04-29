namespace NHISDosageParser.Models.NHISModels
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    [System.Xml.Serialization.XmlRootAttribute("message", Namespace = "https://www.his.bg", IsNullable = false)]
    public partial class C001
    {

        private messageHeader headerField;

        private messageC001Contents contentsField;

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

        public messageC001Contents contents
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
    public partial class messageC001Contents
    {

        private messageC001ContentsNomenclatureId[] nomenclatureIdField;

        private messageC001ContentsUpdateDate updateDateField;

        [System.Xml.Serialization.XmlElementAttribute("nomenclatureId")]
        public messageC001ContentsNomenclatureId[] nomenclatureId
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
        public messageC001ContentsUpdateDate updateDate
        {
            get
            {
                return this.updateDateField;
            }
            set
            {
                this.updateDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www.his.bg")]
    public partial class messageC001ContentsNomenclatureId
    {

        private string valueField;

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
    public partial class messageC001ContentsUpdateDate
    {

        private System.DateTime valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
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
