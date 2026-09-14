using Codesi.Factura.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaDetalleRepository
    {
        private readonly licoreriaContext _context;

        public FacturaDetalleRepository(licoreriaContext context)
        {
            _context = context;
        }

        // Obtener detalles de una factura
        public List<factura_detalles> ObtenerPorFactura(int idFactura)
        {
            return _context.factura_detalles
                .AsNoTracking()
                .Where(d => d.id_factura == idFactura)
                .ToList();
        }

        // Insertar detalle
        public void InsertarDetalle(factura_detalles detalle)
        {
            _context.factura_detalles.Add(detalle);
            _context.SaveChanges();
        }
    }
}