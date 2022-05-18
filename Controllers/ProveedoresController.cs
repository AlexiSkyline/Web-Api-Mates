using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Proveedores;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase {
    #region "Propiedades"
    AdministracionProveedores BLProveedorAdmin = new AdministracionProveedores(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarProveedor")]
    public async Task<IActionResult> InsertarProveedor( ProveedorRequest proveedor ) {
        var peticion = await BLProveedorAdmin.AgregarProveedor( proveedor );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpPut("ActualizarProveedor/{id}")]
    public async Task<IActionResult> ActualizarProveedor( Guid id, ProveedorRequest proveedor ) {
        var peticion = await BLProveedorAdmin.ActualizarProveedor( id, proveedor );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpDelete("EliminarProveedor/{id}")]
    public async Task<IActionResult> EliminarProveedor( Guid id ) {
        var peticion = await BLProveedorAdmin.EliminarProveedor( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarProveedores")]
    public async Task<IActionResult> ListarProveedores() {
        return Ok( await BLProveedorAdmin.ListarProveedores() );
    }

    [HttpGet("ListarFiltroProveedores/{nombre}")]
    public async Task<IActionResult> ListarFiltroProveedores( string nombre ) {
        return Ok( await BLProveedorAdmin.ListarFiltroProveedores( nombre ) );
    }
    #endregion
}