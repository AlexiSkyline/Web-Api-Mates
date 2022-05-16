namespace Unach.Inventario.API.Model.Response {
    public class UnidadMedidaResponse : SingleResponse {
        public Guid Id { get; set; }
        public string? Descripcion { get; set; }
    }
}