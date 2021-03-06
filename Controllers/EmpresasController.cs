using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Empresas;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase {
    #region "Propiedades"
    AdministracionEmpresas BLEmpresaAdmin = new AdministracionEmpresas(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarEmpresa")]
    public async Task<IActionResult> InsertarEmpresa( EmpresaRequest Empresa ) {
        var peticion = await BLEmpresaAdmin.AgregarEmpresa( Empresa );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpPut("ActualizarEmpresa/{id}")]
    public async Task<IActionResult> ActualizarEmpresa( Guid id, EmpresaRequest empresa ) {
        var peticion = await BLEmpresaAdmin.ActualizarEmpresa( id, empresa );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpDelete("EliminarEmpresa/{id}")]
    public async Task<IActionResult> EliminarEmpresa( Guid id ) {
        var peticion = await BLEmpresaAdmin.EliminarEmpresa( id );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }

    [HttpGet("ListarEmpresas")]
    public async Task<IActionResult> ListarEmpresas() {
        return Ok( await BLEmpresaAdmin.ListarEmpresas() );
    }

    [HttpGet("ListarFiltradaEmpresas/{nombre}")]
    public async Task<IActionResult> ListarFiltradaEmpresas( string nombre ) {
        return Ok( await BLEmpresaAdmin.ListarFiltradaEmpresas( nombre ) );
    }
    #endregion
}