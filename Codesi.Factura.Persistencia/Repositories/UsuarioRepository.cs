using Codesi.Factura.Persistencia.Models.Universidad;
using Microsoft.EntityFrameworkCore;

namespace Codesi.Factura.Persistencia.Repositories
{
    public class UsuarioRepository
    {
        private readonly FacturacionUniversidadContext _context;

        public UsuarioRepository(FacturacionUniversidadContext context)
        {
            _context = context;
        }

        // =====================================================
        // OBTENER TODOS LOS USUARIOS
        // =====================================================

        public List<Usuarios> ObtenerUsuarios()
        {
            return _context.Usuarios
                .AsNoTracking()
                .ToList();
        }

        // =====================================================
        // OBTENER USUARIOS ACTIVOS
        // =====================================================

        public List<Usuarios> ObtenerUsuariosActivos()
        {
            return _context.Usuarios
                .AsNoTracking()
                .Where(u => u.Activo)
                .ToList();
        }

        // =====================================================
        // OBTENER USUARIO POR ID
        // =====================================================

        public Usuarios? ObtenerUsuarioPorId(int id)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.IdUsuario == id);
        }

        // =====================================================
        // BUSCAR POR ID MICROSOFT
        // =====================================================

        public Usuarios? ObtenerPorIdMicrosoft(string idMicrosoft)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.IdMicrosoft == idMicrosoft);
        }

        // =====================================================
        // BUSCAR POR EMAIL
        // =====================================================

        public Usuarios? ObtenerPorEmail(string email)
        {
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email);
        }

        // =====================================================
        // OBTENER USUARIO CON ROLES
        // =====================================================

        public Usuarios? ObtenerUsuarioConRoles(int id)
        {
            return _context.Usuarios
                .Include(u => u.IdRol)
                .AsNoTracking()
                .FirstOrDefault(u => u.IdUsuario == id);
        }

        // =====================================================
        // OBTENER POR ID MICROSOFT CON ROLES
        // =====================================================

        public Usuarios? ObtenerPorIdMicrosoftConRoles(
            string idMicrosoft)
        {
            return _context.Usuarios
                .Include(u => u.IdRol)
                .AsNoTracking()
                .FirstOrDefault(u =>
                    u.IdMicrosoft == idMicrosoft);
        }

        // =====================================================
        // LOGIN
        //
        // Busca:
        // Email
        // Cédula
        // Usuario activo
        //
        // Además carga los roles.
        // =====================================================

        public Usuarios? ObtenerPorEmailYCedulaConRoles(
            string email,
            string cedula)
        {
            return _context.Usuarios
                .Include(u => u.IdRol)
                .AsNoTracking()
                .FirstOrDefault(u =>
                    u.Email == email &&
                    u.Cedula == cedula &&
                    u.Activo);
        }

        // =====================================================
        // INSERTAR USUARIO
        // =====================================================

        public void InsertarUsuario(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        // =====================================================
        // ACTUALIZAR USUARIO
        // =====================================================

        public void ActualizarUsuario(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        // =====================================================
        // DESACTIVAR USUARIO
        // =====================================================

        public void DesactivarUsuario(int id)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.IdUsuario == id);

            if (usuario != null)
            {
                usuario.Activo = false;

                _context.SaveChanges();
            }
        }

        // =====================================================
        // ASIGNAR ROL
        // =====================================================

        public bool AsignarRol(
            int idUsuario,
            int idRol)
        {
            var usuario = _context.Usuarios
                .Include(u => u.IdRol)
                .FirstOrDefault(u =>
                    u.IdUsuario == idUsuario);

            if (usuario == null)
                return false;

            var rol = _context.Roles
                .FirstOrDefault(r =>
                    r.IdRol == idRol);

            if (rol == null)
                return false;

            // Evitar duplicar la relación
            if (!usuario.IdRol.Any(r => r.IdRol == idRol))
            {
                usuario.IdRol.Add(rol);

                _context.SaveChanges();
            }

            return true;
        }
    }
}