using Codesi.Factura.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaRepository
    {
        private readonly licoreriaContext _context;

        public FacturaRepository(licoreriaContext context)
        {
            _context = context;
        }

        // Obtener todas las facturas
        public List<facturas> ObtenerFacturas()
        {
            return _context.facturas
                .AsNoTracking()
                .ToList();
        }

        // Obtener factura por ID
        public facturas? ObtenerFacturaPorId(int id)
        {
            return _context.facturas
                .Include(f => f.factura_detalles)
                .Include(f => f.factura_pagos)
                .FirstOrDefault(f => f.id_factura == id);
        }

        // Insertar factura
        public void InsertarFactura(facturas factura)
        {
            _context.facturas.Add(factura);
            _context.SaveChanges();
        }

        // Actualizar estado SRI
        public void ActualizarEstadoSri(int id, string estado)
        {
            var factura = _context.facturas
                .FirstOrDefault(f => f.id_factura == id);

            if (factura != null)
            {
                factura.estado_sri = estado;
                _context.SaveChanges();
            }
        }
    }
}