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
}