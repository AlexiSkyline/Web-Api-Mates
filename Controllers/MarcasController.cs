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
    
    [HttpDelete("EliminarMarca/{id}")]
    public async Task<IActionResult> EliminarMarca( Guid id ) {
        var peticion = await BLMarcaAdmin.EliminarMarcas( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarMarcas")]
    public async Task<IActionResult> ListarMarcas() {
        return Ok( await BLMarcaAdmin.ListarMarcas() );
    }

    [HttpGet("ListarFiltradaMarcas/{descripcion}")]
    public async Task<IActionResult> ListarFiltradaMarcas( string descripcion ) {
        return Ok( await BLMarcaAdmin.ListarFiltradaMarcas( descripcion ) );
    }
    #endregion
}