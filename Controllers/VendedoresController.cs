using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Vendedores;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendedoresController : ControllerBase {
    #region "Propiedades"
    AdministracionVendedores BLVendedorAdmin = new AdministracionVendedores(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarVendedor")]
    public async Task<IActionResult> InsertarVendedor( VendedorRequest Vendedor ) {
        var peticion = await BLVendedorAdmin.AgregarVendedor( Vendedor );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpPut("ActualizarVendedor/{id}")]
    public async Task<IActionResult> ActualizarVededor( Guid id, VendedorRequest Vededor ) {
        var peticion = await BLVendedorAdmin.ActualizarVendedor( id, Vededor );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarVendedores")]
    public async Task<IActionResult> ListarVendedores() {
        return Ok( await BLVendedorAdmin.ListarVendedores() );
    }
    #endregion
}