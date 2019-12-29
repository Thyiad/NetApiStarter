using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Helper.Ioc;
using Project.Model.sys;

namespace Project.Model
{
    public class DbEntities : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer(AppSettings.Instance?.ConnectionStrings.SqlServer ?? "data source=.;initial catalog=NetApiStarter;persist security info=True;user id=sa;password=sa123;");
            //optionsBuilder.UseSqlServer("server=.;port=3306;uid=sa;pwd=sa123;database=NetApiStarter");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
