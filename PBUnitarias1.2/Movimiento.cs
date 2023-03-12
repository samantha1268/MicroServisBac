using ArquitecturaMicrosoft.Controllers;
using Atata;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ninject.Activation;
using OpenQA.Selenium.DevTools.V105.Page;
using PbUnitaria1._2;
using System;

namespace PBUnitarias1._2
{
    [TestClass]
    public class Movimiento : BasePruebas
    {
        [TestMethod]
        public async Task GetMovimientoModelIdNoExistente()
        {

            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            //prueba
            var controller = new MovimientoController(contexto);
            var respuesta = await controller.GetMovimientoModel(1);

            //verificacion 
            var personas = respuesta.Result as StatusCodeResult;


            await contexto.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);
        }
        [TestMethod]
        public async Task DeleteMovimentoQueNoExiste()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            //prueba
            var controller = new MovimientoController(contexto);

            //verificacion 
            var respuesta = await controller.DeleteMovimientoModelo(1);
            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

    }
}