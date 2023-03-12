using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitecturaMicrosoft.Model
{
    public class Persona
    {
        [Key]
        public int IdPesona { get; set; }
        public string nombre { get; set; }
        public string genero { get; set; }
        public int edad { get; set; }
        public string identificación { get; set; }
        public string dirección { get; set; }
        public string teléfono { get; set; }

    }
}
