namespace Unach.Inventario.API.Model;
public class VentasBase: SingleResponse {
    public Guid? Id { get; set; }
    public DateTime? Fecha { get; set; }
    public Guid? IdVendedor { get; set; }
    public Guid? IdCliente { get; set; }
    public int? Folio { get; set; }
    public Guid? IdEmpresa { get; set; }
    public Decimal? Total { get; set; }
    public Decimal? Iva { get; set; }
    public Decimal? SubTotal { get; set; }
    public String? PagoCon { get; set; }
}