using Codesi.Factura.Api.Models;
using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class FacturaService
    {
        private readonly FacturaRepository _repository;

        public FacturaService(FacturaRepository repository)
        {
            _repository = repository;
        }

        public List<Codesi.Factura.Persistencia.Models.Universidad.Factura> ObtenerFacturas()
        {
            return _repository.ObtenerFacturas();
        }

        public Codesi.Factura.Persistencia.Models.Universidad.Factura? ObtenerFacturaPorId(int id)
        {
            return _repository.ObtenerFacturaPorId(id);
        }

        public void CrearFactura(
            Codesi.Factura.Persistencia.Models.Universidad.Factura factura)
        {
            _repository.InsertarFactura(factura);
        }

        public void ActualizarEstado(int id, string estado)
        {
            _repository.ActualizarEstadoSri(id, estado);
        }

        public void ActualizarFactura(
            int id,
            FacturaActualizarRequest request)
        {
            var factura =
                new Codesi.Factura.Persistencia.Models.Universidad.Factura
                {
                    IdFactura = id,
                    IdCliente = request.id_cliente,
                    FechaEmision = request.fecha_emision,
                    SubtotalSinImpuestos =
                        request.subtotal_sin_impuestos,
                    TotalDescuento =
                        request.total_descuento ?? 0,
                    SubtotalIva =
                        request.subtotal_iva,
                    Propina =
                        request.propina ?? 0,
                    ImporteTotal =
                        request.importe_total
                };

            var detalles =
     request.detalles
         .Select(d =>
             new FacturaDetalle
             {
                 IdFactura = id,
                 IdProducto = d.IdProducto,
                 Cantidad = d.Cantidad,
                 PrecioUnitario = d.PrecioUnitario,
                 Descuento = d.Descuento ?? 0,
                 Subtotal = d.Subtotal,
                 ValorIva = d.ValorIva,
                 Total = d.Total
             })
         .ToList();

            var pagos = new List<FacturaPago>();

            if (!string.IsNullOrWhiteSpace(
                    request.forma_pago))
            {
                pagos.Add(
                    new FacturaPago
                    {
                        IdFactura = id,
                        FormaPago = request.forma_pago,
                        Total = request.total_pago
                    });
            }

            _repository.ActualizarFactura(
                id,
                factura,
                detalles,
                pagos
            );
        }
    }
}