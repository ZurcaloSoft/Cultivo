using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public SGF_Formulario Formulario_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Formulario.First(x => x.FormularioID == id);
        }
        [OperationContract]
        public SGF_Formulario Formulario_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_Formulario.First(x => x.Nombre == nombre);
        }
        [OperationContract]
        public List<SGF_Formulario> Formulario_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_Formulario.Where(x => x.Estado == 1).ToList();
        }

        [OperationContract]
        public List<SGF_Formulario> Formulario_ObtenerPorModuloID(Guid moduloID)
        {
            DataModel model = new DataModel();
            return model.SGF_Formulario.Where(x => x.ModuloID == moduloID && x.Estado == 1).ToList();
        }

        [OperationContract]
        public List<SGF_Formulario_VTA> Formulario_ObtenerPorNombre_ModuloID_VTA(Guid moduloID, string nombre)
        {
            DataModel model = new DataModel();
            if (moduloID == Guid.Empty && nombre == "")
                return model.SGF_Formulario_VTA.OrderBy(x => x.NombreModulo).OrderBy(x => x.NombreFormulario).ToList();
            if (moduloID != Guid.Empty)
                return model.SGF_Formulario_VTA.Where(x => x.ModuloID == moduloID).OrderBy(x => x.NombreFormulario).ToList();
            else
                return model.SGF_Formulario_VTA.Where(x => x.NombreFormulario.ToUpper().Contains(nombre.ToUpper())).OrderBy(x => x.NombreFormulario).ToList();
        }

        [OperationContract]
        public void Formulario_Grabar(SGF_Formulario newFormulario)
        {
            DataModel model = new DataModel();
            if (model.SGF_Formulario.Count(x => x.FormularioID == newFormulario.FormularioID) == 0)
            {
                model.AddToSGF_Formulario(newFormulario);
            }
            else
            {
                SGF_Formulario _formulario = model.SGF_Formulario.First(x => x.FormularioID == newFormulario.FormularioID);
                _formulario.Nombre = newFormulario.Nombre;
                _formulario.Codigo = newFormulario.Codigo;
                _formulario.ModuloID = newFormulario.ModuloID;
                _formulario.Descripcion = newFormulario.Descripcion;
            }
            model.SaveChanges();
        }

        [OperationContract]
        public List<SEG_Formularios_VTA> Formulario_ObtenerPorUsuarioID_VTA(Guid usuarioID)
        {
            DataModel model = new DataModel();
            return model.SEG_Formularios_VTA.Where(x => x.UsuarioID == usuarioID && x.EstadoPermiso == 1).ToList();
        }

        [OperationContract]
        public void PermisoFormulario_LimpiarEstado(Guid usuarioID)
        {
            DataModel model = new DataModel();
            foreach(SGF_Permiso item in model.SGF_Permiso.Where(x=>x.UsuarioID== usuarioID))
            {
                SGF_Permiso _permiso = model.SGF_Permiso.First(x => x.PermisoID == item.PermisoID && x.UsuarioID == usuarioID);
                _permiso.Estado = 0;
            }
            model.SaveChanges();
        }
        [OperationContract]
        public void PermisoFormulario_Grabar(SGF_Permiso newPermiso)
        {
            DataModel model = new DataModel();
            if (model.SGF_Permiso.Count(x => x.FormularioID  == newPermiso.FormularioID && x.UsuarioID == newPermiso.UsuarioID && x.PermisoID == newPermiso.PermisoID) > 0)
            {
                SGF_Permiso _permiso= model.SGF_Permiso.First(x => x.PermisoID == newPermiso.PermisoID);
                _permiso.Estado = newPermiso.Estado;
            }
            else
            {
                model.AddToSGF_Permiso(newPermiso);
            }
            model.SaveChanges();
        }
    }
}
