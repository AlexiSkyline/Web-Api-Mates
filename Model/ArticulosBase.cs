namespace Unach.Inventario.API.Model;
public class ArticulosBase: SingleResponse
{
    public Guid? Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public Guid? IdUnidadMedida { get; set; }
    public Guid? IdMarca { get; set; }
    public int? Stock { get; set; }
    public Guid? IdProveedor { get; set; }
}