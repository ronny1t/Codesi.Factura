using Codesi.Factura.Aplicacion.Models;

namespace Codesi.Factura.Aplicacion.Services
{
    public class FacturaService
    {
        private readonly ApiService _api;

        public FacturaService(ApiService api)
        {
            _api = api;
        }

        // Obtener todas las facturas
        public async Task<List<Codesi.Factura.Aplicacion.Models.Factura>> ObtenerFacturas()
        {
            return await _api.GetListAsync<Codesi.Factura.Aplicacion.Models.Factura>(
                "api/Facturas"
            );
        }

        // Obtener factura por ID
        public async Task<Codesi.Factura.Aplicacion.Models.Factura?> ObtenerFacturaPorId(int id)
        {
            return await _api.GetAsync<Codesi.Factura.Aplicacion.Models.Factura>(
                $"api/Facturas/{id}"
            );
        }

        // Crear factura
        public async Task<Codesi.Factura.Aplicacion.Models.Factura?> CrearFactura(
            Codesi.Factura.Aplicacion.Models.Factura factura)
        {
            var request = new
            {
                establecimiento = factura.establecimiento,
                punto_emision = factura.punto_emision,
                secuencial = factura.secuencial,
                clave_acceso = factura.clave_acceso,
                fecha_emision = factura.fecha_emision,

                id_cliente = factura.id_cliente,

                subtotal_sin_impuestos = factura.subtotal_sin_impuestos,
                total_descuento = factura.total_descuento ?? 0,
                subtotal_iva = factura.subtotal_iva,
                propina = factura.propina ?? 0,
                importe_total = factura.importe_total,
                estado_sri = factura.estado_sri
            };

            Console.WriteLine("====================================");
            Console.WriteLine("CREANDO FACTURA DESDE FACTURASERVICE");
            Console.WriteLine($"id_cliente: {request.id_cliente}");
            Console.WriteLine($"importe_total: {request.importe_total}");
            Console.WriteLine("====================================");

            return await _api.PostAsync<
                object,
                Codesi.Factura.Aplicacion.Models.Factura
            >(
                "api/Facturas",
                request
            );
        }
        // Actualizar factura completa
        public async Task ActualizarFactura(
            int id,
            Codesi.Factura.Aplicacion.Models.Factura factura,
            List<FacturaDetalle> detalles,
            string formaPago,
            decimal totalPago)
        {
            var request = new
            {
                id_cliente = factura.id_cliente,
                fecha_emision = factura.fecha_emision,
                subtotal_sin_impuestos =
                    factura.subtotal_sin_impuestos,
                total_descuento =
                    factura.total_descuento,
                subtotal_iva =
                    factura.subtotal_iva,
                propina =
                    factura.propina,
                importe_total =
                    factura.importe_total,

                detalles = detalles,

                forma_pago = formaPago,
                total_pago = totalPago
            };

            await _api.PutAsync<object>(
                $"api/Facturas/{id}",
                request
            );
        }

        // Actualizar estado SRI
        public async Task ActualizarEstado(int id, string estado)
        {
            await _api.PutAsync<object>(
                $"api/Facturas/{id}/estado/{Uri.EscapeDataString(estado)}",
                new { }
            );
        }

        // Obtener detalles
        public async Task<List<FacturaDetalle>> ObtenerDetalles(int idFactura)
        {
            return await _api.GetListAsync<FacturaDetalle>(
                $"api/FacturaDetalles/factura/{idFactura}"
            );
        }

        // Crear detalle
        public async Task<FacturaDetalle?> CrearDetalle(FacturaDetalle detalle)
        {
            return await _api.PostAsync<
                FacturaDetalle,
                FacturaDetalle
            >(
                "api/FacturaDetalles",
                detalle
            );
        }

        // Obtener pagos
        public async Task<List<FacturaPago>> ObtenerPagos(int idFactura)
        {
            return await _api.GetListAsync<FacturaPago>(
                $"api/FacturaPagos/factura/{idFactura}"
            );
        }

        // Crear pago
        public async Task<FacturaPago?> CrearPago(FacturaPago pago)
        {
            return await _api.PostAsync<
                FacturaPago,
                FacturaPago
            >(
                "api/FacturaPagos",
                pago
            );
        }
    }
}