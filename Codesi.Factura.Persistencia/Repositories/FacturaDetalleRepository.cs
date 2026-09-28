using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaDetalleRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public FacturaDetalleRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener detalles de una factura
        public List<FacturaDetalle> ObtenerPorFactura(int idFactura)
        {
            return _context.FacturaDetalles
                .AsNoTracking()
                .Where(d => d.IdFactura == idFactura)
                .ToList();
        }

        // Insertar detalle
        public void InsertarDetalle(FacturaDetalle detalle)
        {
            _context.FacturaDetalles.Add(detalle);
            _context.SaveChanges();
        }
    }
}