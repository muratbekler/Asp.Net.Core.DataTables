using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace api.Models
{
    [DataContract(Name = "patient")]
    public class PatientResultEntity
    {
        [DataMember(Name = "ID")]
        public decimal ID { get; set; }

        [DataMember(Name = "BASVURUTARIHI")]
        public DateTime BASVURUTARIHI { get; set; }

        [DataMember(Name = "BIRIM")]
        public string BIRIM { get; set; }

        [DataMember(Name = "DOKTOR")]
        public string DOKTOR { get; set; }

        [DataMember(Name = "TUR")]
        public string TUR { get; set; }

        [DataMember(Name = "ACIKLAMA")]
        public string ACIKLAMA { get; set; }

        [DataMember(Name = "STATU")]
        public string STATU { get; set; }

    }
}
