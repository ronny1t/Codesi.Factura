using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        // =====================================================
        // OBTENER TODOS
        // =====================================================

        public List<Usuarios> ObtenerUsuarios()
        {
            return _repository.ObtenerUsuarios();
        }

        // =====================================================
        // OBTENER ACTIVOS
        // =====================================================

        public List<Usuarios> ObtenerUsuariosActivos()
        {
            return _repository.ObtenerUsuariosActivos();
        }

        // =====================================================
        // OBTENER POR ID
        // =====================================================

        public Usuarios? ObtenerUsuarioPorId(int id)
        {
            return _repository.ObtenerUsuarioPorId(id);
        }

        // =====================================================
        // OBTENER POR ID MICROSOFT
        // =====================================================

        public Usuarios? ObtenerPorIdMicrosoft(
            string idMicrosoft)
        {
            return _repository.ObtenerPorIdMicrosoft(
                idMicrosoft);
        }

        // =====================================================
        // OBTENER POR EMAIL
        // =====================================================

        public Usuarios? ObtenerPorEmail(string email)
        {
            return _repository.ObtenerPorEmail(email);
        }

        // =====================================================
        // OBTENER CON ROLES
        // =====================================================

        public Usuarios? ObtenerUsuarioConRoles(int id)
        {
            return _repository.ObtenerUsuarioConRoles(id);
        }

        // =====================================================
        // OBTENER POR ID MICROSOFT CON ROLES
        // =====================================================

        public Usuarios? ObtenerPorIdMicrosoftConRoles(
            string idMicrosoft)
        {
            return _repository.ObtenerPorIdMicrosoftConRoles(
                idMicrosoft);
        }

        // =====================================================
        // LOGIN
        // =====================================================

        public Usuarios? Login(
            string email,
            string cedula)
        {
            return _repository
                .ObtenerPorEmailYCedulaConRoles(
                    email,
                    cedula);
        }

        // =====================================================
        // CREAR USUARIO
        // =====================================================

        public void CrearUsuario(Usuarios usuario)
        {
            _repository.InsertarUsuario(usuario);
        }

        // =====================================================
        // ACTUALIZAR USUARIO
        // =====================================================

        public void ActualizarUsuario(Usuarios usuario)
        {
            _repository.ActualizarUsuario(usuario);
        }

        // =====================================================
        // DESACTIVAR USUARIO
        // =====================================================

        public void DesactivarUsuario(int id)
        {
            _repository.DesactivarUsuario(id);
        }

        // =====================================================
        // ASIGNAR ROL
        // =====================================================

        public bool AsignarRol(
            int idUsuario,
            int idRol)
        {
            return _repository.AsignarRol(
                idUsuario,
                idRol);
        }
    }
}