using ArquitecturaMicrosoft.Data;
using ArquitecturaMicrosoft.Model;
using Atata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;
using System.Collections;

namespace ArquitecturaMicrosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ArquitecturaMicrosoftContext _context;

        public ReporteController(ArquitecturaMicrosoftContext context)
        {
            _context = context;
        }
        //POST: api/MovimientoR
        [HttpPost]
        public async Task<ActionResult<Reporte>> PostMovimientoReporte(DateTime fecha, int id)
        {

            List<Reporte> reportes = new List<Reporte>();

            SqlConnection con = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand comando = con.CreateCommand();
            con.Open();
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = "ReportMovimientoPorFecha";
            comando.Parameters.Add("@fecha", System.Data.SqlDbType.DateTime).Value = fecha;
            comando.Parameters.Add("@usuario", System.Data.SqlDbType.Int).Value = id;
            SqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Reporte repor = new Reporte();

                repor.Fecha = (DateTime)reader["Fecha"];
                repor.nombre = (String)reader["nombre"];
                repor.númeroCuenta = (int)reader["númeroCuenta"];
                repor.tipoCuenta = (string)reader["tipoCuenta"];
                repor.saldoInicial = (int)reader["saldoInicial"];
                repor.estado = (string)reader["estado"];
                repor.valor = (int)reader["valor"];
                repor.saldoInicial = (int)reader["saldoInicial"];
                reportes.Add(repor);
            }
            con.Close();
            return Ok(reportes);
        }

    }
}