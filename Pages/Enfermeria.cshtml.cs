using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using SportMedical;
using System.Data;

namespace MyWebApp.Pages
{
    public class EnfermeriaModel : PageModel
    {
        private MySqlCommand comando;
        public DataTable Pacientes;
        public void OnGet()
        {
            
        }
        public IActionResult OnPost(string paciente)
        {
            VGlobales.paciente = paciente;
            return RedirectToPage("/Notas");
        }
        public void getPacients()
        {
            DataTable TablaPacientes = new DataTable();
            comando = Datos.crearComando();
            comando.Parameters.Clear();
            comando.CommandText = "SELECT i.id as Id_Ingreso, CONCAT(p.nombre1,' ',p.nombre2,' ',p.apellido1,' ',p.apellido2) as Nombre FROM ingresos i JOIN pacientes p on i.pacientes_id = p.id WHERE (fecha = @fecha OR fecha = @fecha1) AND medicos_id=@medicos_id ORDER BY hora;";
            comando.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@fecha1", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@medicos_id", VGlobales.idMedico);
            TablaPacientes = Datos.ejecutarComandoSelect(comando);
            Pacientes = TablaPacientes;
        }
    }
}
