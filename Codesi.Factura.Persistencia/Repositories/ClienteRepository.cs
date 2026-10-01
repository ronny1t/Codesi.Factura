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

        // ============================================================
        // OBTENER TODOS LOS CLIENTES
        // ============================================================

        public List<Cliente> ObtenerClientes()
        {
            return _context.Clientes
                .AsNoTracking()
                .ToList();
        }

        // ============================================================
        // OBTENER CLIENTE POR ID
        // ============================================================

        public Cliente? ObtenerClientePorId(int id)
        {
            return _context.Clientes
                .AsNoTracking()
                .FirstOrDefault(c => c.IdCliente == id);
        }

        // ============================================================
        // BUSCAR POR IDENTIFICACIÓN
        // ============================================================

        public Cliente? ObtenerPorIdentificacion(
            string identificacion)
        {
            return _context.Clientes
                .AsNoTracking()
                .FirstOrDefault(
                    c => c.Identificacion == identificacion
                );
        }

        // ============================================================
        // INSERTAR CLIENTE
        // ============================================================

        public void InsertarCliente(Cliente cliente)
        {
            // Si no viene una fecha de creación válida,
            // asignamos la fecha actual.
            if (cliente.FechaCreacion < new DateTime(1753, 1, 1))
            {
                cliente.FechaCreacion = DateTime.Now;
            }

            _context.Clientes.Add(cliente);

            _context.SaveChanges();
        }

        // ============================================================
        // ACTUALIZAR CLIENTE
        // ============================================================

        public void ActualizarCliente(Cliente cliente)
        {
            // Buscar el registro ORIGINAL en la base de datos.
            var clienteExistente = _context.Clientes
                .FirstOrDefault(
                    c => c.IdCliente == cliente.IdCliente
                );

            if (clienteExistente == null)
            {
                throw new Exception(
                    "El cliente que intenta actualizar no existe."
                );
            }

            // ========================================================
            // ACTUALIZAR SOLO LOS CAMPOS EDITABLES
            // ========================================================

            clienteExistente.TipoIdentificacion =
                cliente.TipoIdentificacion;

            clienteExistente.Identificacion =
                cliente.Identificacion;

            clienteExistente.RazonSocial =
                cliente.RazonSocial;

            clienteExistente.Direccion =
                cliente.Direccion;

            clienteExistente.Telefono =
                cliente.Telefono;

            clienteExistente.Email =
                cliente.Email;

            clienteExistente.Activo =
                cliente.Activo;

            // ========================================================
            // IMPORTANTE
            // ========================================================
            //
            // NO hacemos:
            //
            // clienteExistente.FechaCreacion =
            //     cliente.FechaCreacion;
            //
            // La fecha original que está en SQL Server
            // debe conservarse.
            //
            // ========================================================

            _context.SaveChanges();
        }
    }
}