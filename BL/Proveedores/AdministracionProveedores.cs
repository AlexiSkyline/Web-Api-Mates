using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Proveedores;

public class AdministracionProveedores {
    public async Task<ProveedorResponse> AgregarProveedor( ProveedorRequest proveedorRequest ) {
        ProveedorResponse resultado = new ProveedorResponse();

        if( proveedorRequest.Nombre != null && proveedorRequest.Apellidos != null && proveedorRequest.RFC != null && 
            proveedorRequest.Direccion != null && proveedorRequest.Telefono != null && 
            proveedorRequest.Correo != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdminProveedores]",
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );
                comando.Parameters.AddWithValue( "@Nombre", proveedorRequest.Nombre );
                comando.Parameters.AddWithValue( "@Apellidos", proveedorRequest.Apellidos );
                comando.Parameters.AddWithValue( "@RFC", proveedorRequest.RFC );
                comando.Parameters.AddWithValue( "@Direccion", proveedorRequest.Direccion );
                comando.Parameters.AddWithValue( "@Correo", proveedorRequest.Correo );
                comando.Parameters.AddWithValue( "@Telefono", proveedorRequest.Telefono );

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
                    resultado.IdDatosGenerales = lectura.GetGuid( "IdDatosGenerales" );  
                    resultado.Nombre = lectura.GetString( "Nombre" );            
                    resultado.Apellidos = lectura.GetString( "Apellidos" );            
                    resultado.RFC = lectura.GetString( "RFC" );            
                    resultado.Direccion = lectura.GetString( "Direccion" );            
                    resultado.Correo = lectura.GetString( "Correo" );            
                    resultado.Telefono = lectura.GetString( "Telefono" ); 
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value;
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "Todos los campos son obligatorios";
        }

        return resultado;
    }

    public async Task<ProveedorResponse> ActualizarProveedor( Guid? id, ProveedorRequest ProveedorRequest ) {
        ProveedorResponse resultado = new ProveedorResponse();

        if( ProveedorRequest.Nombre != null && ProveedorRequest.Apellidos != null && ProveedorRequest.RFC != null && 
            ProveedorRequest.Direccion != null && ProveedorRequest.Telefono != null && 
            ProveedorRequest.Correo != null && id != null && ProveedorRequest.IdDatosGenerales != null )  {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminProveedores]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", id );
                comando.Parameters.AddWithValue( "@Opcion", "Actualizar" );
                comando.Parameters.AddWithValue( "@IdDatosGenerales", ProveedorRequest.IdDatosGenerales );
                comando.Parameters.AddWithValue( "@Nombre", ProveedorRequest.Nombre );
                comando.Parameters.AddWithValue( "@Apellidos", ProveedorRequest.Apellidos );
                comando.Parameters.AddWithValue( "@RFC", ProveedorRequest.RFC );
                comando.Parameters.AddWithValue( "@Direccion", ProveedorRequest.Direccion );
                comando.Parameters.AddWithValue( "@Correo", ProveedorRequest.Correo );
                comando.Parameters.AddWithValue( "@Telefono", ProveedorRequest.Telefono );

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
                    resultado.IdDatosGenerales = lectura.GetGuid( "IdDatosGenerales" );  
                    resultado.Nombre = lectura.GetString( "Nombre" );            
                    resultado.Apellidos = lectura.GetString( "Apellidos" );            
                    resultado.RFC = lectura.GetString( "RFC" );            
                    resultado.Direccion = lectura.GetString( "Direccion" );            
                    resultado.Correo = lectura.GetString( "Correo" );        
                    resultado.Telefono = lectura.GetString( "Telefono" );        
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value; 
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "Todos los campos son obligatorios";
        }

        return resultado;
    }

    public async Task<List<ProveedorResponse>> ListarProveedores() {
        List<ProveedorResponse> resultado = new List<ProveedorResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminProveedores]",
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
                    Apellidos = lectura.GetString( "Apellidos" ),
                    RFC = lectura.GetString( "RFC" ),
                    Direccion = lectura.GetString( "Direccion" ),
                    Correo = lectura.GetString( "Correo" ),
                    Telefono = lectura.GetString( "Telefono" ),
                    IdDatosGenerales = lectura.GetGuid( "IdDatosGenerales" ),
                    Mensaje = "Listado exitoso",
                    Exito = true
                });
            }

            conexion.Close();
        }

        return resultado;
    }

    public async Task<ProveedorResponse> EliminarProveedor( Guid? id ) {
        ProveedorResponse resultado = new ProveedorResponse();

        if( id != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminProveedores]",
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
                    resultado.IdDatosGenerales = lectura.GetGuid( "IdDatosGenerales" );  
                    resultado.Nombre = lectura.GetString( "Nombre" );            
                    resultado.Apellidos = lectura.GetString( "Apellidos" );            
                    resultado.RFC = lectura.GetString( "RFC" );            
                    resultado.Direccion = lectura.GetString( "Direccion" );            
                    resultado.Correo = lectura.GetString( "Correo" );            
                    resultado.Telefono = lectura.GetString( "Telefono" );          
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

    public async Task<List<ProveedorResponse>> ListarFiltroProveedores( string nombre ) {
        List<ProveedorResponse> resultado = new List<ProveedorResponse>();

        if( nombre != "" )
        {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminProveedores]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Opcion", "ListaFiltrada" );
                comando.Parameters.AddWithValue( "@Nombre", nombre );

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
                        Apellidos = lectura.GetString( "Apellidos" ),
                        RFC = lectura.GetString( "RFC" ),
                        Direccion = lectura.GetString( "Direccion" ),
                        Correo = lectura.GetString( "Correo" ),
                        Telefono = lectura.GetString( "Telefono" ),
                        IdDatosGenerales = lectura.GetGuid( "IdDatosGenerales" ),
                        Mensaje = "Listado exitoso",
                        Exito = true
                    });
                }

                conexion.Close();
            }
        } else {
            resultado.Add( new ProveedorResponse {
                Mensaje = "El nombre es requerido",
                Exito = false
            });
        }

        return resultado;
    }
}