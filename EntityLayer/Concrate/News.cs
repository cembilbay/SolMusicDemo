using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class News
    {
        [Key]
        public int NewsID { get; set; }
        public string? NewsTitle { get; set; }
        public string? NewsContent { get; set; }
        public string? NewsImage { get; set; }
        public DateTime? NewsCreateTime { get; set; }
        public bool? NewsStatus { get; set; }
    }
}
