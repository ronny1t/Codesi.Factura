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
    }
}