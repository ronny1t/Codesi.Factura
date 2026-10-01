using Codesi.Factura.Persistencia.Models.Universidad;
using Codesi.Factura.Persistencia.Repositories;

namespace Codesi.Factura.Api.Services
{
    public class RolService
    {
        private readonly RolRepository _repository;

        public RolService(RolRepository repository)
        {
            _repository = repository;
        }

        public List<Roles> ObtenerRoles()
        {
            return _repository.ObtenerRoles();
        }

        public List<Roles> ObtenerRolesActivos()
        {
            return _repository.ObtenerRolesActivos();
        }

        public Roles? ObtenerRolPorId(int id)
        {
            return _repository.ObtenerRolPorId(id);
        }

        public Roles? ObtenerPorNombre(string nombre)
        {
            return _repository.ObtenerPorNombre(nombre);
        }

        public void CrearRol(Roles rol)
        {
            _repository.InsertarRol(rol);
        }

        public void ActualizarRol(Roles rol)
        {
            _repository.ActualizarRol(rol);
        }

        public void DesactivarRol(int id)
        {
            _repository.DesactivarRol(id);
        }
    }
}