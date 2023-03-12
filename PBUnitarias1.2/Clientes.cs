using ArquitecturaMicrosoft.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PbUnitaria1._2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBUnitarias1._2
{
    [TestClass]
    public class Cliente : BasePruebas
    {
        [TestMethod]
        public async Task PutClienteModel()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            contexto.Cliente.Add(new ArquitecturaMicrosoft.Model.Cliente() { Clienteid = 34245, contraseña = "1268", estado = "true", IdPersona = 34245 });
            await contexto.SaveChangesAsync();

            var contexto2= ConstruirContext(nombreDB);
            //prueba
            var controller = new ClienteController(contexto2);

            var ActulizarCliente  =  new ArquitecturaMicrosoft.Model.Cliente() { Clienteid = 34245, contraseña = "1258", estado = "true", IdPersona = 34245 };




            //verificacion 
            var id = 34245;
            var respuesta = await controller.PutClienteModel(id, ActulizarCliente);
            var resultado = respuesta as CreatedAtActionResult;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreDB);
            var existe = await contexto3.Cliente.AnyAsync(x => x.Clienteid == 34245 && x.contraseña == "1258");
            Assert.IsTrue(existe);
        }
        [TestMethod]
        public async Task DeleteClienteQueNoExiste()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            //prueba
            var controller = new ClienteController(contexto);

            //verificacion 
            var respuesta = await controller.DeleteClienteModel(1);
            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(404,resultado.StatusCode);
        }
        [TestMethod]
        public async Task DeleteClienteExiste()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            //prueba
            contexto.Cliente.Add(new ArquitecturaMicrosoft.Model.Cliente() { Clienteid = 34245, contraseña = "1268", estado = "true", IdPersona = 34245 });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var controller = new ClienteController(contexto2);

            //verificacion 
            var respuesta = await controller.DeleteClienteModel(34245);
            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(204, resultado.StatusCode);

            var contexto3 = ConstruirContext(nombreDB);
            var existe = await contexto3.Cliente.AnyAsync();
            Assert.IsFalse(existe);
        }

    }
}
