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

    [HttpPut("ActualizarDetalleVentas/{id}")]
    public async Task<IActionResult> ActualizarDetalleVentas( Guid id, DetalleVentasRequest detalleVenta ) {
        var peticion = await BLDetalleVentasAdmin.ActualizarDetalleVentas( id, detalleVenta );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpDelete("EliminarDetalleVentas/{id}")]
    public async Task<IActionResult> EliminarDetalleVentas( Guid id ) {
        var peticion = await BLDetalleVentasAdmin.EliminarDetalleVentas( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarDetalleVentas")]
    public async Task<IActionResult> ListarDetalleVentas() {
        return Ok( await BLDetalleVentasAdmin.ListarDetalleVentas() );
    }

    [HttpGet("ListarDetalleVentasPorId/{id}")]
    public async Task<IActionResult> ListarDetalleVentasPorId( Guid id ) {
        return Ok( await BLDetalleVentasAdmin.ListarDetalleVentasPorId( id ) );
    }
    #endregion
}