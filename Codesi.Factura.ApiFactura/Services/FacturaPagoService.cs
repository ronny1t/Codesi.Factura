using Codesi.Factura.Persistencia.Models.Universidad;
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

        public List<FacturaPago> ObtenerPorFactura(
            int idFactura)
        {
            return _repository.ObtenerPorFactura(idFactura);
        }

        public void CrearPago(FacturaPago pago)
        {
            _repository.InsertarPago(pago);
        }
    }
}