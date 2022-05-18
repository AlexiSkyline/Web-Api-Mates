using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Clientes;

public class AdministracionClientes {
    public async Task<ClienteResponse> AgregarCliente( ClienteRequest clienteRequest ) {
        ClienteResponse resultado = new ClienteResponse();

        if( clienteRequest.Nombre != null && clienteRequest.Apellidos != null && clienteRequest.RFC != null && 
            clienteRequest.Direccion != null && clienteRequest.Telefono != null && 
            clienteRequest.Correo != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdminClientes]",
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );
                comando.Parameters.AddWithValue( "@Nombre", clienteRequest.Nombre );
                comando.Parameters.AddWithValue( "@Apellidos", clienteRequest.Apellidos );
                comando.Parameters.AddWithValue( "@RFC", clienteRequest.RFC );
                comando.Parameters.AddWithValue( "@Direccion", clienteRequest.Direccion );
                comando.Parameters.AddWithValue( "@Correo", clienteRequest.Correo );
                comando.Parameters.AddWithValue( "@Telefono", clienteRequest.Telefono );

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

     public async Task<ClienteResponse> ActualizarCliente( Guid? id, ClienteRequest clienteRequest ) {
        ClienteResponse resultado = new ClienteResponse();

        if( id != null && clienteRequest.Nombre != null && clienteRequest.Apellidos != null && clienteRequest.RFC != null && 
            clienteRequest.Direccion != null && clienteRequest.Telefono != null && 
            clienteRequest.Correo != null && id != null && clienteRequest.IdDatosGenerales != null )  {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminClientes]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", id );
                comando.Parameters.AddWithValue( "@Opcion", "Actualizar" );
                comando.Parameters.AddWithValue( "@IdDatosGenerales", clienteRequest.IdDatosGenerales );
                comando.Parameters.AddWithValue( "@Nombre", clienteRequest.Nombre );
                comando.Parameters.AddWithValue( "@Apellidos", clienteRequest.Apellidos );
                comando.Parameters.AddWithValue( "@RFC", clienteRequest.RFC );
                comando.Parameters.AddWithValue( "@Direccion", clienteRequest.Direccion );
                comando.Parameters.AddWithValue( "@Correo", clienteRequest.Correo );
                comando.Parameters.AddWithValue( "@Telefono", clienteRequest.Telefono );

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

    public async Task<ClienteResponse> EliminarCliente( Guid? id ) {
        ClienteResponse resultado = new ClienteResponse();

        if( id != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminClientes]",
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

    public async Task<List<ClienteResponse>> ListarClientes() {
        List<ClienteResponse> resultado = new List<ClienteResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminClientes]",
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

    public async Task<List<ClienteResponse>> ListarFiltroClientes( string nombre) {
        List<ClienteResponse> resultado = new List<ClienteResponse>();

        if( nombre != "" ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminClientes]",
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
            resultado.Add( new ClienteResponse {
                Mensaje = "El nombre es requerido",
                Exito = false
            });
        }

        return resultado;
    }
}