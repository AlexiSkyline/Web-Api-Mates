namespace Unach.Inventario.API.Model.Response;
public class MarcasResponse : SingleResponse {
    public Guid Id { get; set; }
    public string? Descripcion { get; set; }
}