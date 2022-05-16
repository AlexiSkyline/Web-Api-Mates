using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;

namespace Unach.Inventario.API.BL.Seguridad {
    public class Login {
        // * En este sitio va toda la configuración de la base de datos
        public async Task<Model.Response.LoginResponse> LoginPortal( Model.Request.CredencialRequest credencial ) {
            Model.Response.LoginResponse resultado = new Model.Response.LoginResponse();

            if( credencial.Password != null && credencial.Usuario != null ) {
                using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                    conexion.Open();

                    var comando = new SqlCommand {
                        Connection  = conexion,
                        CommandText = "[dbo].[Login]",
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    comando.Parameters.AddWithValue( "@Usuario", credencial.Usuario );
                    comando.Parameters.AddWithValue( "@Password", credencial.Password );

                    SqlParameter exito  = new SqlParameter();
                    exito.ParameterName = "@Exito";
                    exito.SqlDbType     = System.Data.SqlDbType.Bit;
                    exito.Direction     = System.Data.ParameterDirection.Output;

                    comando.Parameters.Add( exito );

                    SqlParameter mensaje  = new SqlParameter();
                    mensaje.ParameterName = "@Mensaje";
                    mensaje.SqlDbType     = System.Data.SqlDbType.VarChar;
                    mensaje.Direction     = System.Data.ParameterDirection.Output;
                    mensaje.Size          = 4000;

                    comando.Parameters.Add( mensaje );

                    var lectura = await comando.ExecuteReaderAsync();

                    while( lectura.Read() ) {
                        resultado.Id        = lectura.GetGuid( "Id" );  
                        resultado.Nombre    = lectura.GetString( "Nombre" );                
                        resultado.Apellidos = lectura.GetString( "Apellidos" );                
                        resultado.RFC       = lectura.GetString( "RFC" );
                        resultado.Correo    = lectura.GetString( "Correo" );
                        resultado.Telefono  = lectura.GetString( "Telefono" );
                    }

                    conexion.Close();
                    resultado.Exito   = (bool) exito.Value; 
                    resultado.Mensaje = (string) mensaje.Value; 
                }
            } else {
                resultado.Exito   = false;
                resultado.Mensaje = "Ingresar usuario y/o contraseña";
            }

            return resultado;
        }
    }
}