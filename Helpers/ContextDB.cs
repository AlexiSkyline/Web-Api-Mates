using Microsoft.EntityFrameworkCore;

namespace Unach.Inventario.API.Helpers {
    public class ContextDB {
        // * <Sumary>
        // * Obtiene o Asigna la cadena de conexión de la Base de datos
        public static string CadenaConexion { get; set; }

        public static DbContextOptions GetOptions( string conexion ) {
            return SqlServerDbContextOptionsExtensions
                            .UseSqlServer( new DbContextOptionsBuilder(), conexion ).Options;
        }
    }
}