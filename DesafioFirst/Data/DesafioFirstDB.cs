using DesafioFirst.Data.Map;
using DesafioFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioFirst.Data
{
    public class DesafioFirstDB : DbContext
    {
        //criando o banco 
        public DesafioFirstDB(DbContextOptions<DesafioFirstDB> options) 
            : base(options) { }
        
        //criando a tabela Pessoas
        public DbSet<PessoaModel> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
