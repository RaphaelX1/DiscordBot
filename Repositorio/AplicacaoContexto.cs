using Microsoft.EntityFrameworkCore;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio
{
    public class AplicacaoContexto: DbContext
    {
        public AplicacaoContexto()
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e=> e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JE3OOOP\\SQLEXPRESS;Database=BotRpg;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Arcano> Arcanos { get; set; }


        public DbSet<Atributo> Atributos { get; set; }


        public DbSet<Efeito> Efeitos { get; set; }


        public DbSet<Formacao> Formacoes { get; set; }

        public DbSet<Nacao> Nacoes { get; set; }

        public DbSet<Pericia> Pericias { get; set; }

        public DbSet<Personagem> Personagens { get; set; }

        public DbSet<PersonagemArcano> PersonagemArcanos { get; set; }

        public DbSet<PersonagemAtributo> PersonagemAtributos { get; set; }

        public DbSet<PersonagemEfeito> PersonagemEfeitos { get; set; }

        public DbSet<PersonagemFormacao> PersonagemFormacoes { get; set; }

        public DbSet<PersonagemPericia> PersonagemPericias { get; set; }

        public DbSet<PersonagemVantagem> PersonagemVantagens { get; set; }

        public DbSet<Religiao> Religioes { get; set; }

        public DbSet<Vantagem> Vantagens { get; set; }
    }
}
