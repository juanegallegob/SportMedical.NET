using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using SportMedical;
using System.Data;

namespace MyWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private MySqlCommand comando;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnPost(string username, string password)
    {
        DataTable TablaUsuarios = new DataTable();
        comando = Datos.crearComando();
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            // Invalid input; return an error message or redirect to a login page
            return RedirectToPage(""); // Redirect to a login page
        }
        comando.Parameters.Clear();
        comando.CommandText = "SELECT * FROM clv WHERE nombre = @nombre AND clave=@clave";
        comando.Parameters.AddWithValue("@nombre", username);
        comando.Parameters.AddWithValue("@clave", password);

        TablaUsuarios = Datos.ejecutarComandoSelect(comando);
        if (TablaUsuarios.Rows.Count > 0)
        {
            VGlobales.idMedico = TablaUsuarios.Rows[0]["medico"].ToString();
            return RedirectToPage("/Inicio");
        }
        else
        {
            return RedirectToPage("");
        }
            
    }
}


