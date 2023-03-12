using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitecturaMicrosoft.Model
{
    public class Cliente
    {

  
        [Key]

        public int Clienteid { get; set; }
        public string contraseña { get; set; }
   
        public string estado { get; set; }
        public int IdPersona { get; set; }

    }
    
}
