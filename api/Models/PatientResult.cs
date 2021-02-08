using DataTables.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace api.Models
{
    public class PatientResult
    {
        [SearchableString]
        [Sortable(Default = false)]
        [DataMember(Name = "ID")]
        public decimal ID { get; set; }

        [SearchableDateTime]
        [Sortable(Default = true)]
        [DataMember(Name = "BASVURUTARIHI")]
        public DateTime BASVURUTARIHI { get; set; }

        [SearchableString]
        [Sortable(Default = false)]
        [DataMember(Name = "BIRIM")]
        public string BIRIM { get; set; }

        [SearchableString]
        [Sortable(Default = false)]
        [DataMember(Name = "DOKTOR")]
        public string DOKTOR { get; set; }

        [SearchableString]
        [Sortable(Default = false)]
        [DataMember(Name = "TUR")]
        public string TUR { get; set; }

        [SearchableString]
        [Sortable(Default = false)]
        [DataMember(Name = "ACIKLAMA")]
        public string ACIKLAMA { get; set; }

    }
}
