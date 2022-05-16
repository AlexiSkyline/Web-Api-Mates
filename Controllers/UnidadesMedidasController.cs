using Microsoft.AspNetCore.Mvc;

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

        if( peticion.Exito == false ) {
            var mensaje = new { peticion.Mensaje };
            return Ok(  mensaje );
        }

        return Ok( peticion );
    }

    [HttpPut("ActualizarUnidadMedida")]
    public async Task<IActionResult> ActualizarUnidadMedida( Model.Request.UnidadMedidaRequest unidad ) {
        var peticion = await BLAdminU.ActualizarUnidadMedida( unidad );

        if( peticion.Exito == false ) {
            var mensaje = new { peticion.Mensaje };
            return Ok(  mensaje );
        }

        return Ok( peticion );
    }
    
    [HttpDelete("EliminarUnidadMedida")]
    public async Task<IActionResult> EliminarUnidadMedida( Model.Request.UnidadMedidaRequest unidad ) {
        var peticion = await BLAdminU.EliminarUnidadMedida( unidad );

        if( peticion.Exito == false ) {
            var mensaje = new { peticion.Mensaje };
            return Ok(  mensaje );
        }

        return Ok( peticion );
    }

    [HttpGet("ListarUnidadMedida")]
    public async Task<IActionResult> ListarUnidadMedida() {
        var peticion = await BLAdminU.ListarUnidadMedida();

        return Ok( peticion );
    }
    #endregion
}