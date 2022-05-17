using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Empresas;

public class AdministracionEmpresas {
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