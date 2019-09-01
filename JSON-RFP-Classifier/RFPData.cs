using Newtonsoft.Json;
using System.Runtime.Serialization;


namespace JSON_RFP_Classifier
{
    [DataContract]
    class RFPData
    {

        [DataMember(Name = "AGENCY")]
        public string Agency { get; set; }


        [DataMember(Name = "OFFICE")]
        public string Office { get; set; }


        [DataMember(Name = "CLASSCOD")]
        public string ClassCode { get; set; }


        [DataMember(Name = "NAICS")]
        public string Naics { get; set; }


        [DataMember(Name = "SUBJECT")]
        public string Subject { get; set; }


        [DataMember(Name = "SOLNBR")]
        public string SolNbr { get; set; }


        [DataMember(Name = "SETASIDE")]
        public string SetAside { get; set; }
    }
}

/*
 * 
        [DataMember(Name = "DATE")]
        public string Date { get; set; }


        [DataMember(Name = "YEAR")]
        public string Year { get; set; }


        [DataMember(Name = "AGENCY")]
        public string Agency { get; set; }


        [DataMember(Name = "OFFICE")]
        public string Office { get; set; }


        [DataMember(Name = "LOCATION")] 
        public string Location { get; set; }


        [DataMember(Name = "ZIP")]
        public string Zip { get; set; }


        [DataMember(Name = "CLASSCOD")]
        public string ClassCode { get; set; }


        [DataMember(Name = "NAICS")]
        public string Naics { get; set; }


        [DataMember(Name = "OFFADD")]
        public string OffAdd { get; set; }


        [DataMember(Name = "SUBJECT")]
        public string Subject { get; set; }


        [DataMember(Name = "SOLNBR")]
        public string SolNbr { get; set; }


        [DataMember(Name = "RESPDATE")]
        public string RespDate { get; set; }


        [DataMember(Name = "ARCHDATE")]
        public string ArchDate { get; set; }


        [DataMember(Name = "CONTACT")]
        public string Contact { get; set; }


        [DataMember(Name = "DESC")]
        public string Desc { get; set; }


        [DataMember(Name = "LINK")]
        public string link { get; set; }


        [DataMember(Name = "URL")]
        public string Url { get; set; }


        [DataMember(Name = "DESC2")]
        public string Desc2 { get; set; }


        [DataMember(Name = "SETASIDE")]
        public string SetAside { get; set; }
    }
*/