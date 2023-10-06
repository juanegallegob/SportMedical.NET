using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.Xml;

namespace SportMedical
{
    /// <summary>
    /// Framework para realizar consultas de una manera mas simplificada en C#.
    /// Developer: Andrés Felipe García Ortiz
    /// ARCE INGENIERIA SAS
    /// </summary>
    class Datos
    {
        public static MySqlCommand comando;
        public static MySqlConnection conexion;
        public static DataTable tabla;

        private static string cadena_conexion = string.Format("server={0};Database={1};Uid={2};Pwd={3};Allow Zero Datetime=True;Convert Zero Datetime=True;", VGlobales.HOST, VGlobales.DATABASE, VGlobales.USUARIO_DB, VGlobales.PASSWORD_DB);

        /// <summary>
        /// Metodo usado para crear un objeto de tipo MysqlCommand, el cual ya posee la conexión creada y esta listo para que le asignen la Query y sus parametros.
        /// </summary>
        /// <returns> Objeto de tipo MysqlCommand </returns>
        public static MySqlCommand crearComando()
        {
            cadena_conexion = string.Format("server={0};Database={1};Uid={2};Pwd={3};Allow Zero Datetime=True;Convert Zero Datetime=True;", VGlobales.HOST, VGlobales.DATABASE, VGlobales.USUARIO_DB, VGlobales.PASSWORD_DB);
            //if (conexion == null)
            //{
            conexion = new MySqlConnection(cadena_conexion);
            //}

            //if (comando == null)
            //{
            comando = conexion.CreateCommand();
            //}
            return comando;
        }

        /// <summary>
        /// Metodo encargado de ejecutar un comando de tipo select
        /// </summary>
        /// <param name="comando"> Comando el cual posee la consulta que se va a efectuar. Junto con sus parametros.</param>
        /// <returns>DataTable con el contenido de la Query.; En caso de error arroja excepción.</returns>
        public static DataTable ejecutarComandoSelect(MySqlCommand cmd)
        {

            tabla = new DataTable();

            tabla.Rows.Clear();
            tabla.Clear();
            try
            {
                cmd.CommandTimeout = 600;
                cmd.Connection.Open();

                MySqlDataReader lector = cmd.ExecuteReader();
                tabla.Constraints.Clear();
                tabla.Load(lector);
                lector.Close();
            }
            catch (Exception ex)
            {
                //LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return tabla;
        }

        /// <summary>
        /// Metodo usado para ejecutar un comando de tipo INSERT, UPDATE, DELETE. Querys que no retornen resultados.
        /// </summary>
        /// <param name="comando"> Comando el cual posee la consulta que se va a efectuar. Junto con sus parametros.</param>
        /// <returns> True si fue satisfactorio ; False si ocurrió un error.</returns>
        public static bool ejecutarComando(MySqlCommand cmd)
        {
            try
            {
                cmd.CommandTimeout = 600;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);

                return false;
                //throw ex;

            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// Metodo especial para inserciones a la base de datos, en los cuales se requiere obtener el id de la inserción.
        /// </summary>
        /// <param name="comando">Comando con la consulta tipo INSERT</param>
        /// <returns>El id de la inserción</returns>
        public static int ejecutarComandoInsertId(MySqlCommand comando)
        {
            int id = -1;
            try
            {
                comando.CommandTimeout = 600;
                comando.Connection.Open();
                comando.ExecuteNonQuery();
                id = unchecked((int)comando.LastInsertedId);
                return id;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // LOGS.LOGGER.crearLog(ex.StackTrace + "\n" + ex.Message);
                throw ex;
                return id;

            }
            finally
            {
                comando.Connection.Close();
            }
        }

        /// <summary>
        /// Este metodo retorna en LETRAS un numero dado.
        /// </summary>
        /// <param name="num"> Numero int o Double</param>
        /// <returns>representacion del numero en letras.</returns>
        public static string numeroALetras(string num)
        {

            string res, dec = "";

            Int64 entero;

            int decimales;

            double nro;

            try
            {

                nro = Convert.ToDouble(num);

            }

            catch
            {

                return "";

            }

            entero = Convert.ToInt64(Math.Truncate(nro));

            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {

                dec = " CON " + decimales.ToString() + "/100";

            }

            res = toText(Convert.ToDouble(entero)) + dec;

            return res;

        }

        /// <summary>
        /// Metodo auxiliar del metodo numeroALetras
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string toText(double value)
        {

            string Num2Text = "";
            string value2 = value + "";
            value2 = value2.Replace("-", "");
            value = Convert.ToDouble(value2);
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);

            else if (value < 1000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);

            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);

            else if (value < 1000000000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);

            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {

                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            }

            return Num2Text;

        }

        /// <summary>
        /// Metodo Para calcular la edad, se le pasa un string con la fecha y regresa un texto con la edad
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string Edad(string fecha)
        {
            ///calcular fecha de nacimiento
            DateTime fechaNacimiento = Convert.ToDateTime(fecha);
            //Obtengo la diferencia en años.
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            DateTime nuevafecha = fechaNacimiento.AddYears(edad);
            //tiene años
            if (edad > 0)
            {
                if (nuevafecha > DateTime.Now)
                {
                    edad = edad - 1;
                    if (edad == 0)
                    {
                        int mesActual = DateTime.Now.Month;
                        int meses = (mesActual + 12) - fechaNacimiento.Month;
                        if (meses > 0)
                        {
                            if (meses > 1)
                            {
                                return meses + " meses";
                            }
                            else
                            {
                                return meses + " mes";
                            }
                        }
                        else
                        {
                            int dias = DateTime.Now.Day - fechaNacimiento.Day;
                            if (dias == 1)
                            {
                                return dias + " dia";
                            }
                            else
                            {
                                return dias + " dias";
                            }
                        }
                    }
                    return edad + " años";
                }
                else
                {
                    return edad + " años";
                }
            }
            // Tiene meses o dias
            else
            {
                int meses = DateTime.Now.Month - fechaNacimiento.Month;
                if (meses > 0)
                {
                    if (meses > 1)
                    {
                        return meses + " meses";
                    }
                    else
                    {
                        return meses + " mes";
                    }
                }
                else
                {
                    int dias = DateTime.Now.Day - fechaNacimiento.Day;
                    if (dias == 1)
                    {
                        return dias + " dia";
                    }
                    else
                    {
                        return dias + " dias";
                    }
                }
            }
            //fin de calcular edad

        }

        /// <summary>
        /// Se inserta un LOG del proceso realizado por el usuario.
        /// </summary>
        /// <param name="formulario"> Formulario del cual se llamo el LOG</param>
        /// <param name="descripcion"> Que operación se efectuo.</param>
        public static void crearLOG(string formulario, string descripcion)
        {
            MySqlCommand cmd = Datos.crearComando();
            cmd.CommandText = "INSERT INTO logs (fecha,usuario,pc_usuario,formulario,descripcion) VALUES (now(),@usuario,@pc_usuario,@formulario,@descripcion)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@usuario", VGlobales.usuario);
            cmd.Parameters.AddWithValue("@pc_usuario", System.Environment.MachineName);
            cmd.Parameters.AddWithValue("@formulario", formulario);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);

            Datos.ejecutarComando(cmd);
        }
    }
}
