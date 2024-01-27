using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }
        
        public string? AboutName1 { get; set; }
        public string? AboutName2 { get; set; }
        public string? AboutImage { get; set; }
        public string? AboutStatus { get; set; }
        public string? AboutContent1 { get; set; }
        public string? AboutContent2 { get; set; }
    }
}

