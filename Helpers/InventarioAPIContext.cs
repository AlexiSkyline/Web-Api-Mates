using Microsoft.EntityFrameworkCore;

namespace Unach.Inventario.API.Helpers {
    public class InventarioAPIContext : DbContext {
        public InventarioAPIContext(){}

        public InventarioAPIContext( DbContextOptions<InventarioAPIContext> options ) : base(options) {}

        public InventarioAPIContext( string cadenaConexion ) : 
        base( Helpers.ContextDB.GetOptions( Helpers.ContextDB.CadenaConexion! ) ){}

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
            if( !optionsBuilder.IsConfigured ) {
                optionsBuilder.UseSqlServer( Helpers.ContextDB.CadenaConexion!, builder => {
                    builder.EnableRetryOnFailure( 5, TimeSpan.FromSeconds( 10 ), null );
                });
            }
        }
    }
}