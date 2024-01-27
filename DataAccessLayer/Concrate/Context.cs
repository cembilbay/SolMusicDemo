using EntityLayer.Concrate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrate
{
    public class Context:IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-IM93EIQA\\SQLEXPRESS;database=BitirmeDemoDb;integrated security=true;TrustServerCertificate=True;");
        }
        public DbSet<Music> Musics { get; set; }
        public DbSet<UserMusic> UserMusics { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        
    }
}
