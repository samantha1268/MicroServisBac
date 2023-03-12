using ArquitecturaMicrosoft.Data;
using ArquitecturaMicrosoft.Model;
using Atata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;
using System.Collections;
using System.Linq;

namespace ArquitecturaMicrosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly ArquitecturaMicrosoftContext _context;

        public MovimientoController(ArquitecturaMicrosoftContext context)
        {
            _context = context;
        }
        // GET: api/Movimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimento>> GetMovimientoModel(int id)
        {
            var MovimientoModel = await _context.Movimento.FindAsync(id);
            var idcuenta = _context.Cuenta.Any(e => e.idcliente == id);
            var idCuMov = _context.Movimento.Any(e => e.IdCuenta == id);
            if (MovimientoModel == null)
            {
                return NotFound("Transaccion no realizada");
            }
            else
            {
                return MovimientoModel;
            }

        }
        // GET: api/Movimiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimento>>> GetMovimientoModelo()
        {
            return await _context.Movimento.ToListAsync();
        }

        // PUT: api/Movimiento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimientoModelo(int id, Movimento movimientoModelo)
        {
            if (id != movimientoModelo.IdMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(movimientoModelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoModeloExists(id))
                {
                    return NotFound("Tranzaccion no realizada ");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovimientoModelo", new { id = movimientoModelo.IdMovimiento }, movimientoModelo);
        }

        // POST: api/Movimiento
        [HttpPost]
        public async Task<ActionResult<Movimento>> PostMovimientoModelo(Movimento movimientoModelo)
        {
            var Movimiento = movimientoModelo.IdMovimiento;
            var idcuenta = movimientoModelo.IdCuenta;
            var valor = movimientoModelo.valor;
            var Movimentotip = movimientoModelo.tipoMovimiento;
           
           if (!CuentaExists(idcuenta))
           {
             return NotFound("Tranzacion no reliazada la Cuenta no existe");
           }         
                    else
                    {
                        if (Movimentotip == "Deposito"|| Movimentotip == "DEPOSITO"|| Movimentotip == "deposito"|| Movimentotip == "Retiro" || Movimentotip == "RETIRO" || Movimentotip == "retiro")
                        {
                                _context.Movimento.Add(movimientoModelo);
                                await _context.SaveChangesAsync();
                                return CreatedAtAction("GetMovimientoModelo", new { id = movimientoModelo.IdMovimiento }, movimientoModelo);
                        }
                        else
                        {
                            return NotFound("Tipo de trasferencia incorrecta");
                        }
            }
        }

        // DELETE: api/Movimiento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimientoModelo(int id)
        {
            var movimientoModelo = await _context.Movimento.FindAsync(id);
            if (movimientoModelo == null)
            {
                return NotFound();
            }

            _context.Movimento.Remove(movimientoModelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool MovimientoModeloExists(int id)
        {
            return _context.Movimento.Any(e => e.IdMovimiento == id);
        }
        private bool CuentaExists(int id)
        {
            return _context.Cuenta.Any(e => e.idcliente == id);
        }

    }
}
