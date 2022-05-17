namespace Unach.Inventario.API.Model;
public class DatosGenerales: SingleResponse {
    public Guid? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? RFC { get; set; }
    public string? Direccion { get; set; }
    public string? Correo { get; set; }
    public string? Telefono { get; set; }
}