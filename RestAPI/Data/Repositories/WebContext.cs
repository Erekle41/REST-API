using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Data.Repository
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> opt) : base(opt)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Wlog> Wlogs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<WlogHistory> History { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Account>().OwnsOne(a => a.SysFields);
            //builder.Entity<Wlog>().OwnsOne(a => a.SysField);
            //builder.Entity<Comment>().OwnsOne(a => a.systemFields);
            //builder.Entity<WlogHistory>().OwnsOne(a => a.systemFields);

            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //builder.Entity<Account>().HasIndex(x => x.UserName).IsUnique();
            //modelBuilder.Entity<Account>().HasIndex(x => x.EMail).IsUnique();
            //modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
    }
}
