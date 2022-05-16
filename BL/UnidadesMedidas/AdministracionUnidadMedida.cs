using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;

namespace Unach.Inventario.API.BL.UnidadesMedidas {
    public class AdministracionUnidadMedida {
        // * En este sitio va toda la configuración de la base de datos
        public async Task<Model.Response.UnidadMedidaResponse> AgregarUnidadMedida( Model.Request.UnidadMedidaRequest unidadMedidaRequest ) {
            Model.Response.UnidadMedidaResponse resultado = new Model.Response.UnidadMedidaResponse();

            if( unidadMedidaRequest.Descripcion != null ) {
                using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                    conexion.Open();

                    var comando = new SqlCommand {
                        Connection  = conexion,
                        CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    comando.Parameters.AddWithValue( "@Id", unidadMedidaRequest.Id );
                    comando.Parameters.AddWithValue( "@Descripcion", unidadMedidaRequest.Descripcion );
                    comando.Parameters.AddWithValue( "@Opcion", "Insertar" );

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
                        resultado.Id          = lectura.GetInt32( "Id" );  
                        resultado.Descripcion = lectura.GetString( "Descripcion" );                
                    }

                    conexion.Close();
                    resultado.Exito   = (bool) exito.Value; 
                    resultado.Mensaje = (string) mensaje.Value; 
                }
            } else {
                resultado.Exito   = false;
                resultado.Mensaje = "Ingresar la Descripción de la Unidad de Medida";
            }

            return resultado;
        }

        public async Task<Model.Response.UnidadMedidaResponse> ActualizarUnidadMedida( Model.Request.UnidadMedidaRequest unidadMedidaRequest ) {
            Model.Response.UnidadMedidaResponse resultado = new Model.Response.UnidadMedidaResponse();

            if( unidadMedidaRequest.Descripcion != null ) {
                using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                    conexion.Open();

                    var comando = new SqlCommand {
                        Connection  = conexion,
                        CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    comando.Parameters.AddWithValue( "@Id", unidadMedidaRequest.Id );
                    comando.Parameters.AddWithValue( "@Descripcion", unidadMedidaRequest.Descripcion );
                    comando.Parameters.AddWithValue( "@Opcion", "Actualizar" );

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

                    conexion.Close();
                    resultado.Exito   = (bool) exito.Value; 
                    resultado.Mensaje = (string) mensaje.Value; 
                }
            } else {
                resultado.Exito   = false;
                resultado.Mensaje = "Ingresar la Descripción de la Unidad de Medida";
            }

            return resultado;
        }

        public async Task<Model.Response.UnidadMedidaResponse> EliminarUnidadMedida( Model.Request.UnidadMedidaRequest unidadMedidaRequest ) {
            Model.Response.UnidadMedidaResponse resultado = new Model.Response.UnidadMedidaResponse();

            if( unidadMedidaRequest.Descripcion != null ) {
                using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                    conexion.Open();

                    var comando = new SqlCommand {
                        Connection  = conexion,
                        CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    comando.Parameters.AddWithValue( "@Id", unidadMedidaRequest.Id );
                    comando.Parameters.AddWithValue( "@Opcion", "Eliminar" );

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

                    conexion.Close();
                    resultado.Exito   = (bool) exito.Value; 
                    resultado.Mensaje = (string) mensaje.Value; 
                }
            } else {
                resultado.Exito   = false;
                resultado.Mensaje = "Ingresar la Descripción de la Unidad de Medida";
            }

            return resultado;
        }
    
        public async Task<List<Model.Response.UnidadMedidaResponse>> ListarUnidadMedida() {
            List<Model.Response.UnidadMedidaResponse> resultado = new List<Model.Response.UnidadMedidaResponse>();

            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Opcion", "Listar" );

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
                    resultado.Add( new(){
                        Id          = lectura.GetInt32( "Id" ),
                        Descripcion = lectura.GetString( "Descripcion" )
                    });
                }

                conexion.Close();
            }

            return resultado;
        }
    }
}