using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportMedical;

namespace MyWebApp.Pages
{
    public class NotasModel : PageModel
    {
        public string ingreso = VGlobales.paciente;
        public void OnGet()
        {
        }
    }
}
