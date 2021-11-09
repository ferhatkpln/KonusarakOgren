using KonusarakOgren.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgren.Models
{
    public class Context : DbContext
    {
        public DbSet<Sınav> Sınavlar { get; set; }
        public DbSet<Sorular> Sorular { get; set; }
        public DbSet<TextViewModel> Text { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=KonusarakOgren.db");
    }
}
