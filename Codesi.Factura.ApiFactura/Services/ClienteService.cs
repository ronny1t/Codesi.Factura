using Codesi.Factura.Persistencia.Models;
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

        public List<clientes> ObtenerClientes()
        {
            return _repository.ObtenerClientes();
        }

        public clientes? ObtenerClientePorId(int id)
        {
            return _repository.ObtenerClientePorId(id);
        }

        public clientes? ObtenerPorIdentificacion(string identificacion)
        {
            return _repository.ObtenerPorIdentificacion(identificacion);
        }

        public void CrearCliente(clientes cliente)
        {
            _repository.InsertarCliente(cliente);
        }

        public void ActualizarCliente(clientes cliente)
        {
            _repository.ActualizarCliente(cliente);
        }
    }
}