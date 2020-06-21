using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CementControl.Models;

namespace CementControl.DataAccess
{
    public class CementContext : DbContext
    {

        public CementContext() : base("CementContext")
        {
        }

        public DbSet<CementData> CementDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
