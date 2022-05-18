using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.DetalleVentas;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleVentasController : ControllerBase {
    #region "Propiedades"
    AdministracionDetalleVentas BLDetalleVentasAdmin = new AdministracionDetalleVentas(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarDetalleVenta")]
    public async Task<IActionResult> InsertarDetalleVentas( DetalleVentasRequest detalleVenta ) {
        var peticion = await BLDetalleVentasAdmin.AgregarDetalleVentas( detalleVenta );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    // [HttpPut("ActualizarVentas/{id}")]
    // public async Task<IActionResult> ActualizarVentas( Guid id, VentasRequest venta ) {
    //     var peticion = await BLVentasAdmin.ActualizarVentas( id, venta );
    //     return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    // }

    // [HttpDelete("EliminarVentas/{id}")]
    // public async Task<IActionResult> EliminarVentas( Guid id ) {
    //     var peticion = await BLVentasAdmin.EliminarVentas( id );
    //     return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    // }

    [HttpGet("ListarDetalleVentas")]
    public async Task<IActionResult> ListarDetalleVentas() {
        return Ok( await BLDetalleVentasAdmin.ListarDetalleVentas() );
    }

    // [HttpGet("ListarFiltroVentas/{folio}")]
    // public async Task<IActionResult> ListarFiltroVentas( int folio ) {
    //     return Ok( await BLVentasAdmin.ListarFiltroVentas( folio ) );
    // }
    #endregion
}