namespace Unach.Inventario.API.Model.Request;
public class VendedorRequest: DatosGenerales {
    public Guid? IdDatosGenerales { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}