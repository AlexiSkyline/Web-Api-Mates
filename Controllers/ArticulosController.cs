using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Articulos;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticulosController : ControllerBase {
    #region "Propiedades"
    AdministracionArticulos BLArticulosAdmin = new AdministracionArticulos(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarArticulo")]
    public async Task<IActionResult> InsertarArticulo( ArticulosRequest Articulo ) {
        var peticion = await BLArticulosAdmin.AgregarArticulo( Articulo );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    // [HttpPut("ActualizarArticulo/{id}")]
    // public async Task<IActionResult> ActualizarArticulo( Guid id, ArticuloRequest Articulo ) {
    //     var peticion = await BLArticulosAdmin.ActualizarArticulo( id, Articulo );
    //     return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    // }

    // [HttpDelete("EliminarArticulo/{id}")]
    // public async Task<IActionResult> EliminarArticulo( Guid id ) {
    //     var peticion = await BLArticulosAdmin.EliminarArticulo( id );
    //     return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    // }

    // [HttpGet("ListarArticulos")]
    // public async Task<IActionResult> ListarArticulos() {
    //     return Ok( await BLArticulosAdmin.ListarArticulos() );
    // }

    // [HttpGet("ListarFiltroArticulos/{nombre}")]
    // public async Task<IActionResult> ListarFiltroArticulos( string nombre ) {
    //     return Ok( await BLArticulosAdmin.ListarFiltroArticulos( nombre ) );
    // }
    #endregion
}