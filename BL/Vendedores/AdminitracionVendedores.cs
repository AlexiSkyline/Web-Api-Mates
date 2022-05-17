using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Vendedores;

public class AdministracionVendedores {
    public async Task<VendedorResponse> AgregarVendedor( VendedorRequest vendedorRequest ) {
        VendedorResponse resultado = new VendedorResponse();

        if( vendedorRequest.Nombre != null && vendedorRequest.Apellidos != null && vendedorRequest.RFC != null && 
            vendedorRequest.Direccion != null && vendedorRequest.Telefono != null && 
            vendedorRequest.Correo != null && vendedorRequest.UserName != null && 
            vendedorRequest.Password != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdminVendedores]",
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );
                comando.Parameters.AddWithValue( "@Nombre", vendedorRequest.Nombre );
                comando.Parameters.AddWithValue( "@Apellidos", vendedorRequest.Apellidos );
                comando.Parameters.AddWithValue( "@RFC", vendedorRequest.RFC );
                comando.Parameters.AddWithValue( "@Direccion", vendedorRequest.Direccion );
                comando.Parameters.AddWithValue( "@Correo", vendedorRequest.Correo );
                comando.Parameters.AddWithValue( "@Telefono", vendedorRequest.Telefono );
                comando.Parameters.AddWithValue( "@UserName", vendedorRequest.UserName );
                comando.Parameters.AddWithValue( "@Password", vendedorRequest.Password );

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
                    resultado.UserName = lectura.GetString( "UserName" );      
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

    public async Task<List<VendedorResponse>> ListarVendedores() {
        List<VendedorResponse> resultado = new List<VendedorResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminVendedores]",
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
                    UserName = lectura.GetString( "UserName" ),
                    Mensaje = "Listado exitoso",
                    Exito = true
                });
            }

            conexion.Close();
        }

        return resultado;
    }
}