using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class FacturaRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public FacturaRepository(
            FacturacionUniversidadContext context)
        {
            _context = context;
        }

        public List<Codesi.Factura.Persistencia.Models.Universidad.Factura>
            ObtenerFacturas()
        {
            return _context.Facturas
                .AsNoTracking()
                .ToList();
        }

        public Codesi.Factura.Persistencia.Models.Universidad.Factura?
            ObtenerFacturaPorId(int id)
        {
            return _context.Facturas
                .AsNoTracking()
                .Include(f => f.IdClienteNavigation)
                .Include(f => f.FacturaDetalles)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(f => f.FacturaPagos)
                .FirstOrDefault(
                    f => f.IdFactura == id
                );
        }

        public void InsertarFactura(
            Codesi.Factura.Persistencia.Models.Universidad.Factura factura)
        {
            var ultimoSecuencial =
                _context.Facturas
                    .Where(f =>
                        f.Establecimiento ==
                            factura.Establecimiento &&
                        f.PuntoEmision ==
                            factura.PuntoEmision)
                    .OrderByDescending(
                        f => f.IdFactura)
                    .Select(
                        f => f.Secuencial)
                    .FirstOrDefault();

            int siguiente = 1;

            if (!string.IsNullOrWhiteSpace(
                    ultimoSecuencial) &&
                int.TryParse(
                    ultimoSecuencial,
                    out int numero))
            {
                siguiente = numero + 1;
            }

            factura.Secuencial =
                siguiente.ToString("D9");

            if (string.IsNullOrWhiteSpace(
                    factura.ClaveAcceso))
            {
                factura.ClaveAcceso =
                    Guid.NewGuid().ToString("N");
            }

            if (factura.FechaEmision <
                new DateTime(1753, 1, 1))
            {
                factura.FechaEmision =
                    DateTime.Now;
            }

            _context.Facturas.Add(factura);

            _context.SaveChanges();
        }

        // ============================================
        // CREAR FACTURA COMPLETA
        // ============================================

        public Codesi.Factura.Persistencia.Models.Universidad.Factura
            InsertarFacturaCompleta(
                Codesi.Factura.Persistencia.Models.Universidad.Factura factura,
                List<FacturaDetalle> detalles,
                FacturaPago pago)
        {
            using var transaction =
                _context.Database.BeginTransaction(
                    IsolationLevel.Serializable
                );

            try
            {
                // ------------------------------------
                // 1. GENERAR SECUENCIAL
                // ------------------------------------

                var ultimoSecuencial =
                    _context.Facturas
                        .Where(f =>
                            f.Establecimiento ==
                                factura.Establecimiento &&
                            f.PuntoEmision ==
                                factura.PuntoEmision)
                        .OrderByDescending(
                            f => f.IdFactura)
                        .Select(
                            f => f.Secuencial)
                        .FirstOrDefault();

                int siguiente = 1;

                if (!string.IsNullOrWhiteSpace(
                        ultimoSecuencial) &&
                    int.TryParse(
                        ultimoSecuencial,
                        out int numero))
                {
                    siguiente = numero + 1;
                }

                factura.Secuencial =
                    siguiente.ToString("D9");

                // ------------------------------------
                // 2. GENERAR CLAVE DE ACCESO
                // ------------------------------------

                if (string.IsNullOrWhiteSpace(
                        factura.ClaveAcceso))
                {
                    factura.ClaveAcceso =
                        Guid.NewGuid().ToString("N");
                }

                // ------------------------------------
                // 3. VALIDAR FECHA
                // ------------------------------------

                if (factura.FechaEmision <
                    new DateTime(1753, 1, 1))
                {
                    factura.FechaEmision =
                        DateTime.Now;
                }

                // ------------------------------------
                // 4. GUARDAR CABECERA
                // ------------------------------------

                _context.Facturas.Add(factura);

                _context.SaveChanges();

                // ------------------------------------
                // 5. ASIGNAR ID DE FACTURA
                // ------------------------------------

                foreach (var detalle in detalles)
                {
                    detalle.IdFactura =
                        factura.IdFactura;
                }

                pago.IdFactura =
                    factura.IdFactura;

                // ------------------------------------
                // 6. GUARDAR DETALLES
                // ------------------------------------

                _context.FacturaDetalles
                    .AddRange(detalles);

                // ------------------------------------
                // 7. GUARDAR PAGO
                // ------------------------------------

                _context.FacturaPagos
                    .Add(pago);

                // ------------------------------------
                // 8. GUARDAR TODO
                // ------------------------------------

                _context.SaveChanges();

                // ------------------------------------
                // 9. CONFIRMAR TRANSACCIÓN
                // ------------------------------------

                transaction.Commit();

                // ------------------------------------
                // 10. DEVOLVER FACTURA CREADA
                // ------------------------------------

                return factura;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void ActualizarEstadoSri(
            int id,
            string estado)
        {
            var factura =
                _context.Facturas
                    .FirstOrDefault(
                        f => f.IdFactura == id
                    );

            if (factura != null)
            {
                factura.EstadoSri =
                    estado;

                _context.SaveChanges();
            }
        }

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
                var facturaExistente =
                    _context.Facturas
                        .FirstOrDefault(
                            f => f.IdFactura == id
                        );

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

                var detallesActuales =
                    _context.FacturaDetalles
                        .Where(
                            d => d.IdFactura == id)
                        .ToList();

                _context.FacturaDetalles
                    .RemoveRange(detallesActuales);

                var pagosActuales =
                    _context.FacturaPagos
                        .Where(
                            p => p.IdFactura == id)
                        .ToList();

                _context.FacturaPagos
                    .RemoveRange(pagosActuales);

                _context.SaveChanges();

                if (detalles.Count > 0)
                {
                    _context.FacturaDetalles
                        .AddRange(detalles);
                }

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