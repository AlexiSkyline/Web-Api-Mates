using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Marcas;

public class AdministracionMarcas {
    public async Task<MarcasResponse> AgregarMarca( MarcasRequest marcasRequest ) {
        MarcasResponse resultado = new MarcasResponse();

        if( marcasRequest.Descripcion != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdministracionMarcas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", marcasRequest.Id );
                comando.Parameters.AddWithValue( "@Descripcion", marcasRequest.Descripcion );
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
                    resultado.Descripcion = lectura.GetString( "Descripcion" );                
                }

                conexion.Close();
                resultado.Exito = (bool) exito.Value; 
                resultado.Mensaje = (string) mensaje.Value; 
            }
        } else {
            resultado.Exito = false;
            resultado.Mensaje = "Ingresar la Descripción de la Unidad de Medida";
        }

        return resultado;
    }
}