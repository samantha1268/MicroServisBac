using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitecturaMicrosoft.Model
{

    public class Movimento
    {
        [Key]
        public int IdMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string tipoMovimiento { get; set; }
        public int valor { get; set; }
        public int saldo { get; set; }
        public int IdCuenta { get; set; }


    }
}
