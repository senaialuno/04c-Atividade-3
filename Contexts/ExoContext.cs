using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sql;

namespace Exo.WebApi.Contexts

{
    public class ExoContext : DbContext
    {
        public ExoContext()
        {
        }
        public ExoContext(DbContextOptions<ExoContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            if(!OptionsBuilder.IsConfigured)
            {
                OptionsBuilder.UseSqlServer("Server=DESKTOP-T23FVCA\\SQLEXPRESS;" + "Database=ExoApi;Trusted_Connection=True;");
            }
        }
        public DbSet<Projeto> Projetos {get; set;}
    }
}