using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;
using Unach.Inventario.API.Model;

namespace Unach.Inventario.API.BL.UnidadesMedidas {
    public class AdministracionUnidadMedida {
        public async Task<UnidadMedidaResponse> AgregarUnidadMedida( UnidadMedidaRequest unidadMedidaRequest ) {
            UnidadMedidaResponse resultado = new UnidadMedidaResponse();

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
                        resultado.Id          = lectura.GetGuid( "Id" );  
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

        public async Task<UnidadMedidaResponse> ActualizarUnidadMedida( Guid? id, UnidadMedidaRequest body ) {
            UnidadMedidaResponse resultado = new UnidadMedidaResponse();

            if( id != null && body.Descripcion != null ) {
                using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                    conexion.Open();

                    var comando = new SqlCommand {
                        Connection  = conexion,
                        CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    comando.Parameters.AddWithValue( "@Id", id );
                    comando.Parameters.AddWithValue( "@Descripcion", body.Descripcion );
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

                    while( lectura.Read() ) {
                        resultado.Id          = lectura.GetGuid( "Id" );  
                        resultado.Descripcion = lectura.GetString( "Descripcion" );                
                    }

                    conexion.Close();
                    resultado.Exito   = (bool) exito.Value; 
                    resultado.Mensaje = (string) mensaje.Value; 
                }
            } else {
                resultado.Exito   = false;
                resultado.Mensaje = "El ID y la Descripción son obligatorios";
            }

            return resultado;
        }

        public async Task<UnidadMedidaResponse> EliminarUnidadMedida( Guid? id ) {
            UnidadMedidaResponse resultado = new UnidadMedidaResponse();

            if( id != null ) {
                using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                    conexion.Open();

                    var comando = new SqlCommand {
                        Connection  = conexion,
                        CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                        CommandType = CommandType.StoredProcedure
                    };
                    
                    comando.Parameters.AddWithValue( "@Id", id );
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

                    while( lectura.Read() ) {
                        resultado.Id          = lectura.GetGuid( "Id" );  
                        resultado.Descripcion = lectura.GetString( "Descripcion" );                
                    }

                    conexion.Close();
                    resultado.Exito   = (bool) exito.Value; 
                    resultado.Mensaje = (string) mensaje.Value; 
                }
            } else {
                resultado.Exito   = false;
                resultado.Mensaje = "El ID es obligatorio";
            }

            return resultado;
        }
    
        public async Task<List<UnidadMedidaResponse>> ListarUnidadMedida() {
            List<UnidadMedidaResponse> resultado = new List<UnidadMedidaResponse>();

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
                        Id          = lectura.GetGuid( "Id" ),
                        Descripcion = lectura.GetString( "Descripcion" ),
                        Mensaje     = "Listado exitoso",
                        Exito       = true
                    });
                }

                conexion.Close();
            }

            return resultado;
        }
        
        public async Task<List<UnidadMedidaResponse>> ListarFiltrada( string descripcion ) {
            List<UnidadMedidaResponse> resultado = new List<UnidadMedidaResponse>();

            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdministracionUnidadesMedidas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Opcion", "ListaFiltrada" );
                comando.Parameters.AddWithValue( "@Descripcion", descripcion );

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
                        Id          = lectura.GetGuid( "Id" ),
                        Descripcion = lectura.GetString( "Descripcion" ),
                        Mensaje     = "Listado filtrado exitoso",
                        Exito       = true
                    });
                }

                conexion.Close();
            }

            return resultado;
        }
    }
}