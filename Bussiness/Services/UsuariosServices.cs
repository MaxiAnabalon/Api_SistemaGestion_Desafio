using Data.DataAccess;
using Entidades;

namespace Bussiness.Services;

public class UsuariosServices
{
    private UsuarioDB _usuariosServicesDataAccess;

    public UsuariosServices(UsuarioDB usuariosServicesDataAccess) 
    {
        _usuariosServicesDataAccess = usuariosServicesDataAccess;
    }

    public Usuario? OneUsuarioSer(int Id)
    {
        return _usuariosServicesDataAccess.OneUsuario(Id);
    }
    public List<Usuario> GetUsuariosSer()
    {
        return _usuariosServicesDataAccess.GetUsuarios();
    }

    public List<Usuario> GetUsuariosPorNombre(string nombre)
    {
        
        return _usuariosServicesDataAccess.GetUsuariosPorNombre(nombre);
    }

    public bool UpdateUsuarioSer(int Id, Usuario usuario)
    {
       return _usuariosServicesDataAccess.UpdateUsuario(Id, usuario); 
    }

    public Usuario InsertUsuarioSer(Usuario usuario)
    {
        _usuariosServicesDataAccess.InsertUsuario(usuario);
        return usuario;
    }
    public bool DeleteUsuarioSer(int Id)
    {
        return _usuariosServicesDataAccess.DeleteUsuario(Id); 
    }

}

