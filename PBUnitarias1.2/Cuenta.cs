using ArquitecturaMicrosoft.Controllers;
using Atata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.DevTools.V105.Page;
using PbUnitaria1._2;

namespace PBUnitarias1._2
{
    [TestClass]
    public class Cuenta : BasePruebas
    {
        [TestMethod]
        public async Task GetTodasLasCuentas()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            contexto.Cuenta.Add(new ArquitecturaMicrosoft.Model.Cuenta()
            {

                IdNúmeroCuenta = 1,
                númeroCuenta = 34247,
                tipoCuenta = "Corriente",
                saldoInicial = 70000,
                estado = "string",
                idcliente = 1

            });
            //contexto.Cuenta.Add(new ArquitecturaMicrosoft.Model.Cuenta()
            //{
            //    IdNúmeroCuenta = 34247,
            //    númeroCuenta = 34247,
            //    tipoCuenta = "Corriente",
            //    saldoInicial = 70000,
            //    estado = "string",
            //    idcliente = 34247
            //});
            await contexto.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);
            //prueba
            var controller = new CuentaController(context2);
            var respuesta = await controller.GetCuentaModelo();

            //verificacion 
            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta);
        }
        [TestMethod]
        public async Task GetCuentaModelIdNoExistente()
        {

            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            //prueba
            var controller = new CuentaController(contexto);
            var respuesta = await controller.GetcuentaModel(1);

            //verificacion 
            var personas = respuesta.Result as StatusCodeResult;


            await contexto.SaveChangesAsync();

            var context2 = ConstruirContext(nombreDB);
        }
        [TestMethod]
        public async Task PutCuentaModel()
        {
            //preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            
            contexto.Cuenta.Add(new ArquitecturaMicrosoft.Model.Cuenta() { IdNúmeroCuenta=1, númeroCuenta=1,tipoCuenta= "Corriente",saldoInicial= 70000,estado= "string", idcliente=1});
            await contexto.SaveChangesAsync();
            
            var contexto2 = ConstruirContext(nombreDB);
            //prueba
            var controller = new CuentaController(contexto2);

            var ActulizarCuenta = new ArquitecturaMicrosoft.Model.Cuenta() { IdNúmeroCuenta = 1, númeroCuenta = 1, tipoCuenta = "Corriente", saldoInicial = 70000, estado = "False", idcliente = 1 };

            var id = 1;

            //verificacion
            var respuesta = await controller.PutCuentaModelo(id, ActulizarCuenta);
            var resultado = respuesta as CreatedAtActionResult;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreDB);
            var existe = await contexto3.Cuenta.AnyAsync(x => x.idcliente == 1);
            Assert.AreEqual(existe, true);
        }
    }
}