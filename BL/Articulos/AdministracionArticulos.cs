using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Articulos;
public class AdministracionArticulos {
    public async Task<ArticulosResponse> AgregarArticulo( ArticulosRequest articuloRequest ) {
        ArticulosResponse resultado = new ArticulosResponse();

        if( articuloRequest.Nombre != null && articuloRequest.Descripcion != null && 
            articuloRequest.IdUnidadMedida != null && articuloRequest.IdMarca != null &&
            articuloRequest.Stock != null && articuloRequest.IdProveedor != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdminArticulos]",
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );
                comando.Parameters.AddWithValue( "@Nombre", articuloRequest.Nombre );
                comando.Parameters.AddWithValue( "@Descripcion", articuloRequest.Descripcion );
                comando.Parameters.AddWithValue( "@IdUnidadMedida", articuloRequest.IdUnidadMedida );
                comando.Parameters.AddWithValue( "@IdMarca", articuloRequest.IdMarca );
                comando.Parameters.AddWithValue( "@Stock", articuloRequest.Stock );
                comando.Parameters.AddWithValue( "@IdProveedor", articuloRequest.IdProveedor );

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
                    resultado.Descripcion = lectura.GetString( "Descripcion" );  
                    resultado.IdUnidadMedida = lectura.GetGuid( "IdUnidadMedida" );  
                    resultado.IdMarca = lectura.GetGuid( "IdMarca" );  
                    resultado.Stock = lectura.GetInt32( "Stock" );  
                    resultado.IdProveedor = lectura.GetGuid( "IdProveedor" );  
                    
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

    public async Task<List<ArticulosResponse>> ListarArticulos() {
        List<ArticulosResponse> resultado = new List<ArticulosResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminArticulos]",
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
                    Descripcion = lectura.GetString( "Descripcion" ),  
                    IdUnidadMedida = lectura.GetGuid( "IdUnidadMedida" ),  
                    IdMarca = lectura.GetGuid( "IdMarca" ),  
                    Stock = lectura.GetInt32( "Stock" ),  
                    IdProveedor = lectura.GetGuid( "IdProveedor" ),  
                    Mensaje = "Listado exitoso",
                    Exito = true
                });
            }

            conexion.Close();
        }

        return resultado;
    }
}