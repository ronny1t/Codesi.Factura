using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public FacturaRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener todas las facturas
        public List<Codesi.Factura.Persistencia.Models.Universidad.Factura> ObtenerFacturas()
        {
            return _context.Facturas
                .AsNoTracking()
                .ToList();
        }

        // Obtener factura por ID
        public Codesi.Factura.Persistencia.Models.Universidad.Factura? ObtenerFacturaPorId(int id)
        {
            return _context.Facturas
                .AsNoTracking()
                .FirstOrDefault(f => f.IdFactura == id);
        }

        // Insertar factura
        public void InsertarFactura(
            Codesi.Factura.Persistencia.Models.Universidad.Factura factura)
        {
            var ultimoSecuencial = _context.Facturas
                .Where(f =>
                    f.Establecimiento == factura.Establecimiento &&
                    f.PuntoEmision == factura.PuntoEmision)
                .OrderByDescending(f => f.IdFactura)
                .Select(f => f.Secuencial)
                .FirstOrDefault();

            int siguiente = 1;

            if (!string.IsNullOrWhiteSpace(ultimoSecuencial) &&
                int.TryParse(ultimoSecuencial, out int numero))
            {
                siguiente = numero + 1;
            }

            factura.Secuencial = siguiente.ToString("D9");

            if (string.IsNullOrWhiteSpace(factura.ClaveAcceso))
            {
                factura.ClaveAcceso = Guid.NewGuid().ToString("N");
            }

            _context.Facturas.Add(factura);

            _context.SaveChanges();
        }

        // Actualizar estado SRI
        public void ActualizarEstadoSri(int id, string estado)
        {
            var factura = _context.Facturas
                .FirstOrDefault(f => f.IdFactura == id);

            if (factura != null)
            {
                factura.EstadoSri = estado;
                _context.SaveChanges();
            }
        }

        // Actualizar factura
        public void ActualizarFactura(
            int id,
            Codesi.Factura.Persistencia.Models.Universidad.Factura factura,
            List<FacturaDetalle> detalles,
            List<FacturaPago> pagos)
        {
            using var transaction =
                _context.Database.BeginTransaction();

            try
            {
                var facturaExistente = _context.Facturas
                    .FirstOrDefault(f => f.IdFactura == id);

                if (facturaExistente == null)
                {
                    throw new KeyNotFoundException(
                        "Factura no encontrada."
                    );
                }

                if (!string.Equals(
                        facturaExistente.EstadoSri,
                        "CREADA",
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "Solo se pueden editar facturas en estado CREADA."
                    );
                }

                // Actualizar cabecera
                facturaExistente.IdCliente =
                    factura.IdCliente;

                facturaExistente.FechaEmision =
                    factura.FechaEmision;

                facturaExistente.SubtotalSinImpuestos =
                    factura.SubtotalSinImpuestos;

                facturaExistente.TotalDescuento =
                    factura.TotalDescuento;

                facturaExistente.SubtotalIva =
                    factura.SubtotalIva;

                facturaExistente.Propina =
                    factura.Propina;

                facturaExistente.ImporteTotal =
                    factura.ImporteTotal;

                // Obtener detalles actuales
                var detallesActuales =
                    _context.FacturaDetalles
                        .Where(d => d.IdFactura == id)
                        .ToList();

                _context.FacturaDetalles
                    .RemoveRange(detallesActuales);

                // Obtener pagos actuales
                var pagosActuales =
                    _context.FacturaPagos
                        .Where(p => p.IdFactura == id)
                        .ToList();

                _context.FacturaPagos
                    .RemoveRange(pagosActuales);

                _context.SaveChanges();

                // Agregar nuevos detalles
                if (detalles.Count > 0)
                {
                    _context.FacturaDetalles
                        .AddRange(detalles);
                }

                // Agregar nuevos pagos
                if (pagos.Count > 0)
                {
                    _context.FacturaPagos
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