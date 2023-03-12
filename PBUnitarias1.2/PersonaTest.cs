using ArquitecturaMicrosoft.Controllers;
using Atata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.DevTools.V105.Page;
using PbUnitaria1._2;

namespace PBUnitarias1._2
{
    [TestClass]
    public class Personas : BasePruebas
    {
        [TestMethod]
        public async Task GetTodasLaspersonas()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            contexto.Persona.Add(new ArquitecturaMicrosoft.Model.Persona()
            {
                IdPesona = 34247,
                nombre = "salvador",
                genero = "Masculino",
                edad = 28,
                identificación = "SADAV4H3Y4GD",
                dirección = "AV.INDEPENDENCIA #543",
                teléfono = "1234358349"
            });
            contexto.Persona.Add(new ArquitecturaMicrosoft.Model.Persona()
            {
                IdPesona = 34246,
                nombre = "Samantha",
                genero = "Femenino",
                edad = 26,
                identificación = "SAA964H3Y4GD",
                dirección = "AV.Francisco #543",
                teléfono = "8721398629"
            });
            await contexto.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);
            //prueba
            var controller = new PersonaController(context2);
            var respuesta = await controller.GetpersonaModel();

            //verificacion 
            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta);
        }
        [TestMethod]
        public async Task GetPersonaModelIdNoExistente()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            //prueba
            var controller = new PersonaController(contexto);
            var respuesta = await controller.GetPersonaModel(1);

            //verificacion 
            var personas = respuesta.Result as StatusCodeResult;
            Assert.IsInstanceOfType(personas , typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task GetPersonaModelIdExistente()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            contexto.Persona.Add(new ArquitecturaMicrosoft.Model.Persona()
            {
                IdPesona = 34247,
                nombre = "salvador",
                genero = "Masculino",
                edad = 28,
                identificación = "SADAV4H3Y4GD",
                dirección = "AV.INDEPENDENCIA #543",
                teléfono = "1234358349"
            });
            contexto.Persona.Add(new ArquitecturaMicrosoft.Model.Persona()
            {
                IdPesona = 34246,
                nombre = "Samantha",
                genero = "Femenino",
                edad = 26,
                identificación = "SAA964H3Y4GD",
                dirección = "AV.Francisco #543",
                teléfono = "8721398629"
            });
            await contexto.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);
            
            //prueba
            var controller = new PersonaController(contexto);

            //verificacion 
            var id = 34247;
            var personas = await controller.GetPersonaModel(id);
            var resultado = personas.Value;
            Assert.AreEqual(id, resultado.IdPesona);
        }
        [TestMethod]
        public async Task PostpersonaNueva()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            var nuevaPersona = new ArquitecturaMicrosoft.Model.Persona()
            {
                IdPesona = 34251,
                nombre = "daniel",
                genero = "Masculino",
                edad = 30,
                identificación = "DADMV4H454DI",
                dirección = "AV.Independencia #543",
                teléfono = "1237858999"
            };

            //prueba
            var controller = new PersonaController(contexto);

            //verificacion 
            var personas = await controller.PostpersonaModel(nuevaPersona);
            var resultado = personas.Result as CreatedAtActionResult;
            Assert.IsNotNull(resultado);

            var context2 = ConstruirContext(nombreDB);
            var cantidad = await context2.Persona.CountAsync();
            Assert.AreEqual(1, cantidad);
        }
    }
}
