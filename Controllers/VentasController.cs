using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Ventas;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase {
    #region "Propiedades"
    AdministracionVentas BLVentasAdmin = new AdministracionVentas(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarVenta")]
    public async Task<IActionResult> InsertarVenta( VentasRequest Ventas ) {
        var peticion = await BLVentasAdmin.AgregarVenta( Ventas );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    // [HttpPut("ActualizarVentas/{id}")]
    // public async Task<IActionResult> ActualizarVededor( Guid id, VentasRequest Vededor ) {
    //     var peticion = await BLVentasAdmin.ActualizarVentas( id, Vededor );
    //     return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    // }

    [HttpDelete("EliminarVentas/{id}")]
    public async Task<IActionResult> EliminarVentas( Guid id ) {
        var peticion = await BLVentasAdmin.EliminarVentas( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarVentas")]
    public async Task<IActionResult> ListarVentas() {
        return Ok( await BLVentasAdmin.ListarVentas() );
    }

    [HttpGet("ListarFiltroVentas/{folio}")]
    public async Task<IActionResult> ListarFiltroVentas( int folio ) {
        return Ok( await BLVentasAdmin.ListarFiltroVentas( folio ) );
    }
    #endregion
}