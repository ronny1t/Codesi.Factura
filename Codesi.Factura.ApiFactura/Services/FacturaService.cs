using Codesi.Factura.Api.Models;
using Codesi.Factura.Persistencia.Models;
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

        public List<facturas> ObtenerFacturas()
        {
            return _repository.ObtenerFacturas();
        }

        public facturas? ObtenerFacturaPorId(int id)
        {
            return _repository.ObtenerFacturaPorId(id);
        }

        public void CrearFactura(facturas factura)
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
            var factura = new facturas
            {
                id_factura = id,
                id_cliente = request.id_cliente,
                fecha_emision = request.fecha_emision,
                subtotal_sin_impuestos =
                    request.subtotal_sin_impuestos,
                total_descuento =
                    request.total_descuento,
                subtotal_iva =
                    request.subtotal_iva,
                propina =
                    request.propina,
                importe_total =
                    request.importe_total
            };

            var detalles =
                request.detalles
                    .Select(d => new factura_detalles
                    {
                        id_factura = id,
                        id_producto = d.id_producto,
                        cantidad = d.cantidad,
                        precio_unitario =
                            d.precio_unitario,
                        descuento = d.descuento,
                        subtotal = d.subtotal,
                        valor_iva = d.valor_iva,
                        total = d.total
                    })
                    .ToList();

            var pagos = new List<factura_pagos>();

            if (!string.IsNullOrWhiteSpace(
                    request.forma_pago))
            {
                pagos.Add(new factura_pagos
                {
                    id_factura = id,
                    forma_pago = request.forma_pago,
                    total = request.total_pago
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