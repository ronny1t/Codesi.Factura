using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class FacturaDetalleService
    {
        private readonly FacturaDetalleRepository _repository;

        public FacturaDetalleService(
            FacturaDetalleRepository repository)
        {
            _repository = repository;
        }

        // Obtener detalles de una factura
        public List<FacturaDetalle> ObtenerPorFactura(int idFactura)
        {
            return _repository.ObtenerPorFactura(idFactura);
        }

        // Crear detalle
        public void CrearDetalle(FacturaDetalle detalle)
        {
            _repository.InsertarDetalle(detalle);
        }
    }
}