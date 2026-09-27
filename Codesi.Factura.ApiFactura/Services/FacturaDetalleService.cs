using Codesi.Factura.Persistencia.Models;
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
        public List<factura_detalles> ObtenerPorFactura(int idFactura)
        {
            return _repository.ObtenerPorFactura(idFactura);
        }

        // Crear detalle
        public void CrearDetalle(factura_detalles detalle)
        {
            _repository.InsertarDetalle(detalle);
        }
    }
}