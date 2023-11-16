using API_INTERFAZ.Models;


namespace API_INTERFAZ.Servicios
{
    public interface IServicio_API
    {
        Task<List<Alumno>> Lista();
        Task<Alumno> Obtener(int IdMatricula);
        Task<bool> Guardar(Alumno objeto);
        Task<bool> Editar(Alumno objeto);
        Task<bool> Eliminar(int IdMatricula);


    }
}
