using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;

        public ClienteService(ClienteRepository repository)
        {
            _repository = repository;
        }

        public List<Cliente> ObtenerClientes()
        {
            return _repository.ObtenerClientes();
        }

        public Cliente? ObtenerClientePorId(int id)
        {
            return _repository.ObtenerClientePorId(id);
        }

        public Cliente? ObtenerPorIdentificacion(string identificacion)
        {
            return _repository.ObtenerPorIdentificacion(identificacion);
        }

        public void CrearCliente(Cliente cliente)
        {
            _repository.InsertarCliente(cliente);
        }

        public void ActualizarCliente(Cliente cliente)
        {
            _repository.ActualizarCliente(cliente);
        }
    }
}