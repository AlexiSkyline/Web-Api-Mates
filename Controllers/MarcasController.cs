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
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpPut("ActualizarMarca/{id}")]
    public async Task<IActionResult> ActualizarMarca( Guid id, MarcasRequest marca ) {
        var peticion = await BLMarcaAdmin.ActualizarMarcas( id, marca );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }
    
    #endregion
}