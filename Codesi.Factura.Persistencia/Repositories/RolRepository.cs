using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class RolRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public RolRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // Obtener todos los roles
        public List<Roles> ObtenerRoles()
        {
            return _context.Roles
                .AsNoTracking()
                .ToList();
        }

        // Obtener roles activos
        public List<Roles> ObtenerRolesActivos()
        {
            return _context.Roles
                .AsNoTracking()
                .Where(r => r.Activo == true)
                .ToList();
        }

        // Obtener rol por ID
        public Roles? ObtenerRolPorId(int id)
        {
            return _context.Roles
                .AsNoTracking()
                .FirstOrDefault(r => r.IdRol == id);
        }

        // Buscar rol por nombre
        public Roles? ObtenerPorNombre(string nombre)
        {
            return _context.Roles
                .AsNoTracking()
                .FirstOrDefault(r => r.Nombre == nombre);
        }

        // Insertar rol
        public void InsertarRol(Roles rol)
        {
            _context.Roles.Add(rol);
            _context.SaveChanges();
        }

        // Actualizar rol
        public void ActualizarRol(Roles rol)
        {
            _context.Roles.Update(rol);
            _context.SaveChanges();
        }

        // Desactivar rol
        public void DesactivarRol(int id)
        {
            var rol = _context.Roles
                .FirstOrDefault(r => r.IdRol == id);

            if (rol != null)
            {
                rol.Activo = false;
                _context.SaveChanges();
            }
        }
    }
}