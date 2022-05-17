using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnidadesMedidasController : ControllerBase {
    #region "Propiedades"
    BL.UnidadesMedidas.AdministracionUnidadMedida BLAdminU = new BL.UnidadesMedidas.AdministracionUnidadMedida(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarUnidadMedida")]
    public async Task<IActionResult> InsertarUnidadMedida( Model.Request.UnidadMedidaRequest unidad ) {
        var peticion = await BLAdminU.AgregarUnidadMedida( unidad );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpPut("ActualizarUnidadMedida/{id}")]
    public async Task<IActionResult> ActualizarUnidadMedida( Guid id, UnidadMedidaRequest unidad ) {
        var peticion = await BLAdminU.ActualizarUnidadMedida( id, unidad );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }
    
    [HttpDelete("EliminarUnidadMedida/{id}")]
    public async Task<IActionResult> EliminarUnidadMedida( Guid id ) {
        var peticion = await BLAdminU.EliminarUnidadMedida( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarUnidadMedida")]
    public async Task<IActionResult> ListarUnidadMedida() {
        return Ok( await BLAdminU.ListarUnidadMedida() );
    }

    [HttpGet("ListarFiltradaUnidadMedida/{descripcion}")]
    public async Task<IActionResult> ListarFiltradaUnidadMedida( string descripcion ) {
        return Ok( await BLAdminU.ListarFiltrada( descripcion ) );
    }
    #endregion
}