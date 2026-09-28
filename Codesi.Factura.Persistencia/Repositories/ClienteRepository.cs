using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class ClienteRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public ClienteRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener todos los clientes
        public List<Cliente> ObtenerClientes()
        {
            return _context.Clientes
                .AsNoTracking()
                .ToList();
        }

        // Obtener cliente por ID
        public Cliente? ObtenerClientePorId(int id)
        {
            return _context.Clientes
                .AsNoTracking()
                .FirstOrDefault(c => c.IdCliente == id);
        }

        // Buscar por identificación
        public Cliente? ObtenerPorIdentificacion(string identificacion)
        {
            return _context.Clientes
                .AsNoTracking()
                .FirstOrDefault(c => c.Identificacion == identificacion);
        }

        // Insertar cliente
        public void InsertarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        // Actualizar cliente
        public void ActualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }
    }
}