namespace Unach.Inventario.API.Model.Response;
public class EmpresaResponse : SingleResponse {
    public Guid? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
}