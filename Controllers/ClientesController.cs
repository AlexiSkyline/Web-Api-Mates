using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Clientes;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase {
    #region "Propiedades"
    AdministracionClientes BLClientesAdmin = new AdministracionClientes(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarCliente")]
    public async Task<IActionResult> InsertarCliente( ClienteRequest Cliente ) {
        var peticion = await BLClientesAdmin.AgregarCliente( Cliente );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    // [HttpPut("ActualizarCliente/{id}")]
    // public async Task<IActionResult> ActualizarCliente( Guid id, ClienteRequest Cliente ) {
    //     var peticion = await BLClienteAdmin.ActualizarCliente( id, Cliente );
    //     return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    // }

    [HttpDelete("EliminarCliente/{id}")]
    public async Task<IActionResult> EliminarCliente( Guid id ) {
        var peticion = await BLClientesAdmin.EliminarCliente( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarClientes")]
    public async Task<IActionResult> ListarClientes() {
        return Ok( await BLClientesAdmin.ListarClientes() );
    }

    [HttpGet("ListarFiltroClientes/{nombre}")]
    public async Task<IActionResult> ListarFiltroClientes( string nombre ) {
        return Ok( await BLClientesAdmin.ListarFiltroClientes( nombre ) );
    }
    #endregion
}