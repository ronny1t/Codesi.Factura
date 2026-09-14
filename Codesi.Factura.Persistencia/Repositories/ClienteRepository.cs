using Codesi.Factura.Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class ClienteRepository
    {
        private readonly licoreriaContext _context;

        public ClienteRepository(licoreriaContext context)
        {
            _context = context;
        }

        // Obtener todos los clientes
        public List<clientes> ObtenerClientes()
        {
            return _context.clientes
                .AsNoTracking()
                .ToList();
        }

        // Obtener cliente por ID
        public clientes? ObtenerClientePorId(int id)
        {
            return _context.clientes
                .AsNoTracking()
                .FirstOrDefault(c => c.id_cliente == id);
        }

        // Buscar por identificación
        public clientes? ObtenerPorIdentificacion(string identificacion)
        {
            return _context.clientes
                .AsNoTracking()
                .FirstOrDefault(c => c.identificacion == identificacion);
        }

        // Insertar cliente
        public void InsertarCliente(clientes cliente)
        {
            _context.clientes.Add(cliente);
            _context.SaveChanges();
        }

        // Actualizar cliente
        public void ActualizarCliente(clientes cliente)
        {
            _context.clientes.Update(cliente);
            _context.SaveChanges();
        }
    }
}