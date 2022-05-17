using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Empresas;

public class AdministracionEmpresas {
    public async Task<EmpresaResponse> AgregarEmpresa( EmpresaRequest empresaRequest ) {
        EmpresaResponse resultado = new EmpresaResponse();

        if( empresaRequest.Nombre != null && empresaRequest.Direccion != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminEmpresas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", empresaRequest.Id );
                comando.Parameters.AddWithValue( "@Nombre", empresaRequest.Nombre );
                comando.Parameters.AddWithValue( "@Direccion", empresaRequest.Direccion );
                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );

                SqlParameter exito  = new SqlParameter();
                exito.ParameterName = "@Exito";
                exito.SqlDbType = System.Data.SqlDbType.Bit;
                exito.Direction = System.Data.ParameterDirection.Output;

                comando.Parameters.Add( exito );

                SqlParameter mensaje = new SqlParameter();
                mensaje.ParameterName = "@Mensaje";
                mensaje.SqlDbType = System.Data.SqlDbType.VarChar;
                mensaje.Direction = System.Data.ParameterDirection.Output;
                mensaje.Size = 4000;

                comando.Parameters.Add( mensaje );

                var lectura = await comando.ExecuteReaderAsync();

                while( lectura.Read() ) {
                    resultado.Id = lectura.GetGuid( "Id" );  
                    resultado.Nombre = lectura.GetString( "Nombre" );                
                    resultado.Direccion = lectura.GetString( "Direccion" );                
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value; 
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "El nombre y la dirección son requeridos";
        }

        return resultado;
    }

    public async Task<EmpresaResponse> ActualizarEmpresa( Guid? id, EmpresaRequest body ) {
        EmpresaResponse resultado = new EmpresaResponse();

        if( id != null && body.Nombre != null && body.Direccion != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminEmpresas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", id );
                comando.Parameters.AddWithValue( "@Nombre", body.Nombre );
                comando.Parameters.AddWithValue( "@Direccion", body.Direccion );
                comando.Parameters.AddWithValue( "@Opcion", "Actualizar" );

                SqlParameter exito  = new SqlParameter();
                exito.ParameterName = "@Exito";
                exito.SqlDbType = System.Data.SqlDbType.Bit;
                exito.Direction = System.Data.ParameterDirection.Output;

                comando.Parameters.Add( exito );

                SqlParameter mensaje  = new SqlParameter();
                mensaje.ParameterName = "@Mensaje";
                mensaje.SqlDbType = System.Data.SqlDbType.VarChar;
                mensaje.Direction = System.Data.ParameterDirection.Output;
                mensaje.Size = 4000;

                comando.Parameters.Add( mensaje );

                var lectura = await comando.ExecuteReaderAsync();

                while( lectura.Read() ) {
                    resultado.Id = lectura.GetGuid( "Id" );  
                    resultado.Nombre = lectura.GetString( "Nombre" );                
                    resultado.Direccion = lectura.GetString( "Direccion" );                
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value; 
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "El ID, el nombre y la dirección son requeridos";
        }

        return resultado;
    }

    public async Task<EmpresaResponse> EliminarEmpresa( Guid? id ) {
        EmpresaResponse resultado = new EmpresaResponse();

        if( id != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminEmpresas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", id );
                comando.Parameters.AddWithValue( "@Opcion", "Eliminar" );

                SqlParameter exito = new SqlParameter();
                exito.ParameterName = "@Exito";
                exito.SqlDbType = System.Data.SqlDbType.Bit;
                exito.Direction = System.Data.ParameterDirection.Output;

                comando.Parameters.Add( exito );

                SqlParameter mensaje = new SqlParameter();
                mensaje.ParameterName = "@Mensaje";
                mensaje.SqlDbType = System.Data.SqlDbType.VarChar;
                mensaje.Direction = System.Data.ParameterDirection.Output;
                mensaje.Size = 4000;

                comando.Parameters.Add( mensaje );

                var lectura = await comando.ExecuteReaderAsync();

                while( lectura.Read() ) {
                    resultado.Id = lectura.GetGuid( "Id" );  
                    resultado.Nombre = lectura.GetString( "Nombre" );                
                    resultado.Direccion = lectura.GetString( "Direccion" );                
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value; 
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "El ID es requerido";
        }

        return resultado;
    }

    public async Task<List<EmpresaResponse>> ListarEmpresas() {
        List<EmpresaResponse> resultado = new List<EmpresaResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminEmpresas]",
                CommandType = CommandType.StoredProcedure
            };
            
            comando.Parameters.AddWithValue( "@Opcion", "Listar" );

            SqlParameter exito = new SqlParameter();
            exito.ParameterName = "@Exito";
            exito.SqlDbType = System.Data.SqlDbType.Bit;
            exito.Direction = System.Data.ParameterDirection.Output;

            comando.Parameters.Add( exito );

            SqlParameter mensaje = new SqlParameter();
            mensaje.ParameterName = "@Mensaje";
            mensaje.SqlDbType = System.Data.SqlDbType.VarChar;
            mensaje.Direction = System.Data.ParameterDirection.Output;
            mensaje.Size = 4000;

            comando.Parameters.Add( mensaje );

            var lectura = await comando.ExecuteReaderAsync();

            while( lectura.Read() ) {
                resultado.Add( new(){
                    Id = lectura.GetGuid( "Id" ),
                    Nombre = lectura.GetString( "Nombre" ),
                    Direccion = lectura.GetString( "Direccion" ),
                    Mensaje = "Listado exitoso",
                    Exito = true
                });
            }

            conexion.Close();
        }

        return resultado;
    }
}