using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaPagoRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public FacturaPagoRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener pagos de una factura
        public List<FacturaPago> ObtenerPorFactura(int idFactura)
        {
            return _context.FacturaPagos
                .AsNoTracking()
                .Where(p => p.IdFactura == idFactura)
                .ToList();
        }

        // Insertar pago
        public void InsertarPago(FacturaPago pago)
        {
            _context.FacturaPagos.Add(pago);
            _context.SaveChanges();
        }
    }
}