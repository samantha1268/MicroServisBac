using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitecturaMicrosoft.Model
{

    public class Cuenta
    {

        [Key]
        public int IdNúmeroCuenta { get; set; }
 
        public int númeroCuenta { get; set; }

        public string tipoCuenta { get; set; }

        public int saldoInicial { get; set; }

        public string estado { get; set; }

        public int idcliente { get; set; }
    }
}
