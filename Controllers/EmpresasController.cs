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

    [HttpGet("ListarEmpresas")]
    public async Task<IActionResult> ListarEmpresas() {
        return Ok( await BLEmpresaAdmin.ListarEmpresas() );
    }
    #endregion
}