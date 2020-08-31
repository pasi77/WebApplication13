using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDemo.Models
{
    public class CandidateInfo
    {
        public int ID { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Republicans { get; set; }

        public int Democrats { get; set; }

        public List<CandidateInfo> candidates { get; set; }
    }
}
