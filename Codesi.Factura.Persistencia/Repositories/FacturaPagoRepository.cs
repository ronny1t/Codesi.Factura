using Codesi.Factura.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaPagoRepository
    {
        private readonly licoreriaContext _context;

        public FacturaPagoRepository(licoreriaContext context)
        {
            _context = context;
        }

        // Obtener pagos de una factura
        public List<factura_pagos> ObtenerPorFactura(int idFactura)
        {
            return _context.factura_pagos
                .AsNoTracking()
                .Where(p => p.id_factura == idFactura)
                .ToList();
        }

        // Insertar pago
        public void InsertarPago(factura_pagos pago)
        {
            _context.factura_pagos.Add(pago);
            _context.SaveChanges();
        }
    }
}