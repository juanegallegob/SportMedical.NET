using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportMedical
{
    class VGlobales
    {
        // Variables constantes para la cadena de conexion de la base de datos.


        public static string HOST = "72.167.51.48";

        public static string USUARIO_DB = "sportmedical";
        public static string PASSWORD_DB = "MARIA*41945090*";
        public static string DATABASE = "sportmedicalcenter";


        //public static string HOST = "localhost";
        //public static string USUARIO_DB = "arceing2";
        //public static string PASSWORD_DB = "pechuga";
        //public static string DATABASE = "san_esteban_pyp";



        public static string CIUDAD = "";
        public static string SEDE = "";
        public static string DIRECCION = "";
        public static string TELEFONO = "";
        public static string nombrecompleto = "";

        // Variable que almacena el usuario que hizo Log in.
        public static string usuario;

        // Se almacena el id del médico.
        public static string idMedico;

        // Se almacena el tipo de acceso.
        public static string TipoAcceso;

        public static string paciente;

    }
}
