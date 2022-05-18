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
}