using ArquitecturaMicrosoft.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PbUnitaria1._2
{
    public class BasePruebas 
    {
       
       protected ArquitecturaMicrosoftContext ConstruirContext(string nombreDB)
        {
           var opciones = new DbContextOptionsBuilder<ArquitecturaMicrosoftContext>().UseInMemoryDatabase(nombreDB).Options;

           var dbContext = new ArquitecturaMicrosoftContext(opciones);
           return dbContext;
        }
    }
}
