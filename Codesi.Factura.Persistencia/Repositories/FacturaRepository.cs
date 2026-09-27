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
                .AsNoTracking()
                .FirstOrDefault(f => f.id_factura == id);
        }

        // Insertar factura
        public void InsertarFactura(facturas factura)
        {
            var ultimoSecuencial = _context.facturas
                .Where(f =>
                    f.establecimiento == factura.establecimiento &&
                    f.punto_emision == factura.punto_emision)
                .OrderByDescending(f => f.id_factura)
                .Select(f => f.secuencial)
                .FirstOrDefault();

            int siguiente = 1;

            if (!string.IsNullOrWhiteSpace(ultimoSecuencial) &&
                int.TryParse(ultimoSecuencial, out int numero))
            {
                siguiente = numero + 1;
            }

            factura.secuencial = siguiente.ToString("D9");

            if (string.IsNullOrWhiteSpace(factura.clave_acceso))
            {
                factura.clave_acceso = Guid.NewGuid().ToString("N");
            }

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
        //Actualizar factura
        public void ActualizarFactura(
    int id,
    facturas factura,
    List<factura_detalles> detalles,
    List<factura_pagos> pagos)
        {
            using var transaction =
                _context.Database.BeginTransaction();

            try
            {
                var facturaExistente = _context.facturas
                    .FirstOrDefault(f => f.id_factura == id);

                if (facturaExistente == null)
                {
                    throw new KeyNotFoundException(
                        "Factura no encontrada."
                    );
                }

                if (!string.Equals(
                        facturaExistente.estado_sri,
                        "CREADA",
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "Solo se pueden editar facturas en estado CREADA."
                    );
                }

                // Actualizar cabecera
                facturaExistente.id_cliente =
                    factura.id_cliente;

                facturaExistente.fecha_emision =
                    factura.fecha_emision;

                facturaExistente.subtotal_sin_impuestos =
                    factura.subtotal_sin_impuestos;

                facturaExistente.total_descuento =
                    factura.total_descuento;

                facturaExistente.subtotal_iva =
                    factura.subtotal_iva;

                facturaExistente.propina =
                    factura.propina;

                facturaExistente.importe_total =
                    factura.importe_total;

                // Obtener detalles actuales
                var detallesActuales =
                    _context.factura_detalles
                        .Where(d => d.id_factura == id)
                        .ToList();

                _context.factura_detalles
                    .RemoveRange(detallesActuales);

                // Obtener pagos actuales
                var pagosActuales =
                    _context.factura_pagos
                        .Where(p => p.id_factura == id)
                        .ToList();

                _context.factura_pagos
                    .RemoveRange(pagosActuales);

                _context.SaveChanges();

                // Agregar nuevos detalles
                if (detalles.Count > 0)
                {
                    _context.factura_detalles
                        .AddRange(detalles);
                }

                // Agregar nuevos pagos
                if (pagos.Count > 0)
                {
                    _context.factura_pagos
                        .AddRange(pagos);
                }

                _context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}