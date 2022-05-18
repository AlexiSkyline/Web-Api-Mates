namespace Unach.Inventario.API.Model;

public class DetalleVentasBase: SingleResponse
{
    public Guid? Id { get; set; }
    public Guid? IdVenta { get; set; }
    public Guid? IdArticulo { get; set; }
    public int? Cantidad { get; set; }
    public decimal? PrecioCompra { get; set; }
    public decimal? Importe { get; set; }
}