using Codesi.Factura.Persistencia.Models;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class FacturaPagoService
    {
        private readonly FacturaPagoRepository _repository;

        public FacturaPagoService(
            FacturaPagoRepository repository)
        {
            _repository = repository;
        }

        public List<factura_pagos> ObtenerPorFactura(
            int idFactura)
        {
            return _repository.ObtenerPorFactura(idFactura);
        }

        public void CrearPago(factura_pagos pago)
        {
            _repository.InsertarPago(pago);
        }
    }
}