using System.Data;
using Microsoft.Data.SqlClient;
using Unach.Inventario.API.Helpers;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.Model.Response;

namespace Unach.Inventario.API.BL.Ventas;

public class AdministracionVentas {
    public async Task<VentasResponse> AgregarVenta( VentasRequest ventasRequest ) {
        VentasResponse resultado = new VentasResponse();

        if( ventasRequest.Fecha != null && ventasRequest.IdVendedor != null && 
        ventasRequest.IdCliente != null && ventasRequest.Folio != null && 
        ventasRequest.IdEmpresa != null && ventasRequest.Total != null && 
        ventasRequest.Iva != null && ventasRequest.SubTotal != null && ventasRequest.PagoCon != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection  = conexion,
                    CommandText = "[dbo].[AdminVentas]",
                    CommandType = CommandType.StoredProcedure
                };

                comando.Parameters.AddWithValue( "@Opcion", "Insertar" );
                comando.Parameters.AddWithValue( "@Fecha", ventasRequest.Fecha );
                comando.Parameters.AddWithValue( "@IdVendedor", ventasRequest.IdVendedor );
                comando.Parameters.AddWithValue( "@IdCliente", ventasRequest.IdCliente );
                comando.Parameters.AddWithValue( "@Folio", ventasRequest.Folio );
                comando.Parameters.AddWithValue( "@IdEmpresa", ventasRequest.IdEmpresa );
                comando.Parameters.AddWithValue( "@Total", ventasRequest.Total );
                comando.Parameters.AddWithValue( "@Iva", ventasRequest.Iva );
                comando.Parameters.AddWithValue( "@SubTotal", ventasRequest.SubTotal );
                comando.Parameters.AddWithValue( "@PagoCon", ventasRequest.PagoCon );

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
                    resultado.Fecha = lectura.GetDateTime( "Fecha" );   
                    resultado.IdVendedor = lectura.GetGuid( "IdVendedor" );  
                    resultado.IdCliente = lectura.GetGuid( "IdCliente" );
                    resultado.Folio = lectura.GetInt32( "Folio" );
                    resultado.IdEmpresa = lectura.GetGuid( "IdEmpresa" );
                    resultado.Total = lectura.GetDecimal( "Total" );
                    resultado.Iva = lectura.GetDecimal( "Iva" );
                    resultado.SubTotal = lectura.GetDecimal( "SubTotal" );
                    resultado.PagoCon = lectura.GetString( "PagoCon" ); 
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

    public async Task<VentasResponse> ActualizarVentas( Guid? id, VentasRequest ventasRequest ) {
        VentasResponse resultado = new VentasResponse();

        if( id != null && ventasRequest.Fecha != null && ventasRequest.IdVendedor != null && 
        ventasRequest.IdCliente != null && ventasRequest.Folio != null && 
        ventasRequest.IdEmpresa != null && ventasRequest.Total != null && 
        ventasRequest.Iva != null && ventasRequest.SubTotal != null && ventasRequest.PagoCon != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminVentas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Id", id );
                comando.Parameters.AddWithValue( "@Opcion", "Actualizar" );
                comando.Parameters.AddWithValue( "@Fecha", ventasRequest.Fecha );
                comando.Parameters.AddWithValue( "@IdVendedor", ventasRequest.IdVendedor );
                comando.Parameters.AddWithValue( "@IdCliente", ventasRequest.IdCliente );
                comando.Parameters.AddWithValue( "@Folio", ventasRequest.Folio );
                comando.Parameters.AddWithValue( "@IdEmpresa", ventasRequest.IdEmpresa );
                comando.Parameters.AddWithValue( "@Total", ventasRequest.Total );
                comando.Parameters.AddWithValue( "@Iva", ventasRequest.Iva );
                comando.Parameters.AddWithValue( "@SubTotal", ventasRequest.SubTotal );
                comando.Parameters.AddWithValue( "@PagoCon", ventasRequest.PagoCon );

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
                    resultado.Fecha = lectura.GetDateTime( "Fecha" );   
                    resultado.IdVendedor = lectura.GetGuid( "IdVendedor" );  
                    resultado.IdCliente = lectura.GetGuid( "IdCliente" );
                    resultado.Folio = lectura.GetInt32( "Folio" );
                    resultado.IdEmpresa = lectura.GetGuid( "IdEmpresa" );
                    resultado.Total = lectura.GetDecimal( "Total" );
                    resultado.Iva = lectura.GetDecimal( "Iva" );
                    resultado.SubTotal = lectura.GetDecimal( "SubTotal" );
                    resultado.PagoCon = lectura.GetString( "PagoCon" );         
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

    public async Task<VentasResponse> EliminarVentas( Guid? id ) {
        VentasResponse resultado = new VentasResponse();

        if( id != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminVentas]",
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
                    resultado.Fecha = lectura.GetDateTime( "Fecha" );   
                    resultado.IdVendedor = lectura.GetGuid( "IdVendedor" );  
                    resultado.IdCliente = lectura.GetGuid( "IdCliente" );
                    resultado.Folio = lectura.GetInt32( "Folio" );
                    resultado.IdEmpresa = lectura.GetGuid( "IdEmpresa" );
                    resultado.Total = lectura.GetDecimal( "Total" );
                    resultado.Iva = lectura.GetDecimal( "Iva" );
                    resultado.SubTotal = lectura.GetDecimal( "SubTotal" );
                    resultado.PagoCon = lectura.GetString( "PagoCon" );             
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

    public async Task<List<VentasResponse>> ListarVentas() {
        List<VentasResponse> resultado = new List<VentasResponse>();

        using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
            conexion.Open();

            var comando = new SqlCommand {
                Connection = conexion,
                CommandText = "[dbo].[AdminVentas]",
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
                    Fecha = lectura.GetDateTime( "Fecha" ),   
                    IdVendedor = lectura.GetGuid( "IdVendedor" ),  
                    IdCliente = lectura.GetGuid( "IdCliente" ),
                    Folio = lectura.GetInt32( "Folio" ),
                    IdEmpresa = lectura.GetGuid( "IdEmpresa" ),
                    Total = lectura.GetDecimal( "Total" ),
                    Iva = lectura.GetDecimal( "Iva" ),
                    SubTotal = lectura.GetDecimal( "SubTotal" ),
                    PagoCon = lectura.GetString( "PagoCon" ),
                    Mensaje = "Listado exitoso",
                    Exito = true
                });
            }

            conexion.Close();
        }

        return resultado;
    }

    public async Task<List<VentasResponse>> ListarFiltroVentas( int? folio ) {
        List<VentasResponse> resultado = new List<VentasResponse>();

        if( folio != null ) {
            using( var conexion = new SqlConnection( ContextDB.CadenaConexion ) ) {
                conexion.Open();

                var comando = new SqlCommand {
                    Connection = conexion,
                    CommandText = "[dbo].[AdminVentas]",
                    CommandType = CommandType.StoredProcedure
                };
                
                comando.Parameters.AddWithValue( "@Opcion", "ListaFiltrada" );
                comando.Parameters.AddWithValue( "@Folio", folio );

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
                        Fecha = lectura.GetDateTime( "Fecha" ),   
                        IdVendedor = lectura.GetGuid( "IdVendedor" ),  
                        IdCliente = lectura.GetGuid( "IdCliente" ),
                        Folio = lectura.GetInt32( "Folio" ),
                        IdEmpresa = lectura.GetGuid( "IdEmpresa" ),
                        Total = lectura.GetDecimal( "Total" ),
                        Iva = lectura.GetDecimal( "Iva" ),
                        SubTotal = lectura.GetDecimal( "SubTotal" ),
                        PagoCon = lectura.GetString( "PagoCon" ),
                        Mensaje = "Listado con filtrado exitoso",
                        Exito = true
                    });
                }

                conexion.Close();
            }
        } else {
            resultado.Add( new VentasResponse {
                Mensaje = "El Folio es requerido",
                Exito = false
            });
        }

        return resultado;
    }
}