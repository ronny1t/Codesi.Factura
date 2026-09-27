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
                "Facturas"
            );
        }

        // Obtener una factura completa
        public async Task<Codesi.Factura.Aplicacion.Models.Factura?> ObtenerFacturaPorId(int id)
        {
            return await _api.GetAsync<Codesi.Factura.Aplicacion.Models.Factura>(
                $"Facturas/{id}"
            );
        }

        // Crear factura
        public async Task<Codesi.Factura.Aplicacion.Models.Factura?> CrearFactura(
            Codesi.Factura.Aplicacion.Models.Factura factura)
        {
            return await _api.PostAsync<Codesi.Factura.Aplicacion.Models.Factura>(
                "Facturas",
                factura
            );
        }

        // Actualizar estado SRI
        public async Task ActualizarEstado(
            int id,
            string estado)
        {
            await _api.PutAsync<object>(
                $"Facturas/{id}/estado/{Uri.EscapeDataString(estado)}",
                new { }
            );
        }

        // Obtener detalles
        public async Task<List<FacturaDetalle>> ObtenerDetalles(
            int idFactura)
        {
            return await _api.GetListAsync<FacturaDetalle>(
                $"FacturaDetalles/factura/{idFactura}"
            );
        }

        // Agregar detalle
        public async Task<FacturaDetalle?> CrearDetalle(
            FacturaDetalle detalle)
        {
            return await _api.PostAsync<FacturaDetalle>(
                "FacturaDetalles",
                detalle
            );
        }

        // Obtener pagos
        public async Task<List<FacturaPago>> ObtenerPagos(
            int idFactura)
        {
            return await _api.GetListAsync<FacturaPago>(
                $"FacturaPagos/factura/{idFactura}"
            );
        }

        // Agregar pago
        public async Task<FacturaPago?> CrearPago(
            FacturaPago pago)
        {
            return await _api.PostAsync<FacturaPago>(
                "FacturaPagos",
                pago
            );
        }
    }
}