using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Marcas;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasController : ControllerBase {
    #region "Propiedades"
    AdministracionMarcas BLMarcaAdmin = new AdministracionMarcas(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarMarca")]
    public async Task<IActionResult> InsertarMarca( MarcasRequest marca ) {
        var peticion = await BLMarcaAdmin.AgregarMarca( marca );

        if( peticion.Exito == false ) {
            var mensaje = new { peticion.Mensaje };
            return Ok(  mensaje );
        }

        return Ok( peticion );
    }
    #endregion
}