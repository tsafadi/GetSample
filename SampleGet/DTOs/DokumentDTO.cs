using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleGet.DTOs
{
    public class DokumentDTO
    {
        public int Id { get; set; }
        public int AuftragId { get; set; }
        public string Typ { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; }
        public DateTime ErstellungsDatum { get; set; }
    }
}
