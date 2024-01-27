using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrate
{
    public class UserMusic
    {
        [Key]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string? SongName { get; set; }
        public string? Singer { get; set; }
        public string? AlbumCoverUrl { get; set; }
        public AppUser? User { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
