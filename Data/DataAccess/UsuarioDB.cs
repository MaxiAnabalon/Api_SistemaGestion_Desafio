using Entidades;
using Data.Context;

namespace Data.DataAccess;

public class UsuarioDB
{
    private readonly DBConnection _conexion;

    // Constructor que utiliza la inyección de dependencias
    public UsuarioDB(DBConnection conexion)
    {
        _conexion = conexion;
    }

    public Usuario? OneUsuario(int Id)
    {
        return _conexion.Usuarios.FirstOrDefault(usu => usu.Id == Id);

    }
    public List<Usuario> GetUsuarios() 
    {
        return _conexion.Usuarios.ToList();
    }

    public bool UpdateUsuario(int Id, Usuario usuario)
    {
        try
        {
            var usuarioEditar = OneUsuario(Id);
            if (usuarioEditar != null)
            {
                usuarioEditar.nombre = usuario.nombre;
                usuarioEditar.apellido = usuario.apellido;
                usuarioEditar.nombreDeUsuario = usuario.nombreDeUsuario;
                usuarioEditar.contraseña = usuario.contraseña;
                usuarioEditar.email = usuario.email; 
                _conexion.SaveChanges();
                return true;
            }
            else
            {   
                // Usuario no encontrado 
                return false;
            }
        }
        catch (Exception ex)  
        {
            // Manejo de la excepción, puedes registrar el error
            Console.WriteLine($"Error al actualizar el usuario: {ex.Message}");
            return false;
        }
    }

    public Usuario InsertUsuario(Usuario usuario)
    {
        _conexion.Usuarios.Add(usuario);
        _conexion.SaveChanges();
        return usuario;
    }
    public bool DeleteUsuario(int Id)
    {
        //var usuarioAEliminar = OneUsuario(Id);
        //if (usuarioAEliminar != null)
        //{
        //    _conexion.Usuarios.Remove(usuarioAEliminar);
        //    _conexion.SaveChanges();
        //}
        try
        {
            var usuarioAEliminar = OneUsuario(Id);
            if (usuarioAEliminar != null)
            {
                _conexion.Usuarios.Remove(usuarioAEliminar);
                _conexion.SaveChanges();
                return true; // Eliminación exitosa
            }
            return false; // Usuario no encontrado
        }
        catch (Exception ex)
        {
            // Aquí podrías registrar el error
            Console.WriteLine($"Error al eliminar el usuario: {ex.Message}");
            return false; // Opcional: lanzar de nuevo la excepción si necesitas manejarla más arriba
        }
    }
}
