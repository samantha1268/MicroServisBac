using Microsoft.EntityFrameworkCore;

namespace ArquitecturaMicrosoft.Data
{
    public class ArquitecturaMicrosoftContext : DbContext
    {
        public ArquitecturaMicrosoftContext(DbContextOptions<ArquitecturaMicrosoftContext> options)
            : base(options)
        {

        }

        public DbSet<ArquitecturaMicrosoft.Model.Persona> Persona { get; set; }

        public DbSet<ArquitecturaMicrosoft.Model.Cliente> Cliente { get; set; }

        public DbSet<ArquitecturaMicrosoft.Model.Movimento> Movimento { get; set; }
        public DbSet<ArquitecturaMicrosoft.Model.Cuenta> Cuenta { get; set; }
        
    }
    public class Reporte
    {
        public DateTime Fecha { get; set; }
        public string nombre { get; set; }
        public int númeroCuenta { get; set; }
        public string tipoCuenta { get; set; }
        public int saldoInicial { get; set; }
        public string estado { get; set; }
        public int valor { get; set; }
        public int saldo { get; set; }

    }
}
