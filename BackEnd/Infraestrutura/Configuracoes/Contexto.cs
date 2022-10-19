using Entidades.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Configuracoes
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        public string ObterStringConexao()
        {
            //string strcon = "Data Source=DESKTOP-MDSVJUS\\Trabalho;Initial Catalog=Banco_DDD_2022;Integrated Security=False;User ID = **;Password = ***********;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            string strcon = "Data Source=localhost;Initial Catalog=Banco_DDD_2022;Integrated Security=False;User ID=sa;Password=God(Akamady)01234;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True";
            //string strcon = "Server=localhost;Database=Banco_DDD_2022;User Id=sa;Password=God(Akamady)01234;MultipleActiveResultSets=true;";
            return strcon;
        }
    }
}
