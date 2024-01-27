using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class Music
    {
        [Key]
        public int ID { get; set; }
        public string? SongName { get; set; }
        public string? Artist { get; set; }
        public string? SongContent { get; set; }
        
    }
}
