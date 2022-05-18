using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.DetalleVentas;

public class AdministracionDetalleVentas {
    public async Task<DetalleVentasResponse> AgregarDetalleVentas( DetalleVentasRequest detalleVentasRequest ) {
        DetalleVentasResponse resultado = new DetalleVentasResponse();

        if( detalleVentasRequest.IdVentas != null && detalleVentasRequest.IdArticulo != null && 
            detalleVentasRequest.Cantidad > 0 && detalleVentasRequest.PrecioCompra > 0 && 
            detalleVentasRequest.Importe > 0 && detalleVentasRequest.Cantidad != null &&  
            detalleVentasRequest.PrecioCompra != null && detalleVentasRequest.Importe != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdminDetalleVenta]",
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );
                comando.Parameters.AddWithValue( "@IdVenta", detalleVentasRequest.IdVentas );
                comando.Parameters.AddWithValue( "@IdArticulo", detalleVentasRequest.IdArticulo );
                comando.Parameters.AddWithValue( "@Cantidad", detalleVentasRequest.Cantidad );
                comando.Parameters.AddWithValue( "@PrecioCompra", detalleVentasRequest.PrecioCompra );
                comando.Parameters.AddWithValue( "@Importe", detalleVentasRequest.Importe );
                

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
                    resultado.IdVentas = lectura.GetGuid( "IdVentas" );
                    resultado.IdArticulo = lectura.GetGuid( "IdArticulo" );
                    resultado.Cantidad = lectura.GetInt32( "Cantidad" );
                    resultado.PrecioCompra = lectura.GetDecimal( "PrecioCompra" );
                    resultado.Importe = lectura.GetDecimal( "Importe" );
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value;
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "Todos los campos son obligatorios no pueden estar vacios o con valores menores a cero";
        }

        return resultado;
    }

    public async Task<List<DetalleVentasResponse>> ListarDetalleVentas() {
        List<DetalleVentasResponse> resultado = new List<DetalleVentasResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminDetalleVenta]",
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
                    IdVentas = lectura.GetGuid( "IdVentas" ),
                    IdArticulo = lectura.GetGuid( "IdArticulo" ),
                    Cantidad = lectura.GetInt32( "Cantidad" ),
                    PrecioCompra = lectura.GetDecimal( "PrecioCompra" ),
                    Importe = lectura.GetDecimal( "Importe" ),
                    Mensaje = "Listado exitoso",
                    Exito = true
                });
            }

            conexion.Close();
        }

        return resultado;
    }
}