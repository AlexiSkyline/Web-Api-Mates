using Microsoft.AspNetCore.Mvc;

namespace Unach.Inventario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeguridadController : ControllerBase {
    #region "Propiedades"
    BL.Seguridad.Login BLLogin = new BL.Seguridad.Login(); 
    #endregion

    #region  "Metodos"
    [HttpPost("AutentificarUsuario")]
    public async Task<IActionResult> AutentificarUsuario( Model.Request.CredencialRequest credencial ) {
        var peticion = await BLLogin.LoginPortal( credencial );

        if( peticion.Exito == false ) {
            var mensaje = new { peticion.Mensaje };
            return Ok(  mensaje );
        }

        return Ok( peticion );
    }
    #endregion
}