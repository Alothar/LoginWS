using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LoginWS.DB
{
    public class EFAccountDB : System.Data.Entity.DbContext
    {
        public EFAccountDB(string connectionString)
        {
            Database.SetInitializer<EFAccountDB>(new DropCreateDatabaseIfModelChanges<EFAccountDB>());
            this.Database.Connection.ConnectionString = connectionString;
        }
        public System.Data.Entity.DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(p => new { p.AccountLogin, p.AccountPassword })
                .IsUnique(true);
        }
    }
}