using Microsoft.AspNetCore.Mvc;
using Unach.Inventario.API.Model.Request;
using Unach.Inventario.API.BL.Vendedores;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendedoresController : ControllerBase {
    #region "Propiedades"
    AdministracionVendedores BLVendedorAdmin = new AdministracionVendedores(); 
    #endregion

    #region  "Metodos"
    [HttpPost("InsertarVendedor")]
    public async Task<IActionResult> InsertarVendedor( VendedorRequest Vendedor ) {
        var peticion = await BLVendedorAdmin.AgregarVendedor( Vendedor );
        return peticion.Exito.Equals( false ) ? Ok( new{ mensaje = peticion.Mensaje } ) : Ok( peticion );
    }
    #endregion
}