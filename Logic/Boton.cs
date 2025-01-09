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
        public SGF_Boton Boton_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Boton.First(x => x.BotonID == id);
        }
        [OperationContract]
        public SGF_Boton Boton_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_Boton.First(x => x.Nombre == nombre);
        }
        [OperationContract]
        public List<SGF_Boton> Boton_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_Boton.Where(x => x.Estado == 1).ToList();
        }

        [OperationContract]
        public List<SGF_Boton> Boton_ObtenerPorFormularioID(Guid formularioID)
        {
            DataModel model = new DataModel();
            return model.SGF_Boton.Where(x => x.FormularioID == formularioID && x.Estado == 1).ToList();
        }
        [OperationContract]
        public List<SEG_Botones_VTA> Boton_ObtenerPorUsuarioID_VTA(Guid usuarioID)
        {
            DataModel model = new DataModel();
            return model.SEG_Botones_VTA.Where(x => x.UsuarioID == usuarioID && x.EstadoPermiso == 1).ToList();
        }
        [OperationContract]
        public void Boton_Grabar(SGF_Boton newBoton)
        {
            DataModel model = new DataModel();
            if (model.SGF_Boton.Count(x => x.BotonID == newBoton.BotonID) > 0)
            {
                SGF_Boton _boton = model.SGF_Boton.First(x => x.BotonID == newBoton.BotonID);
                _boton.Nombre = newBoton.Nombre;
                _boton.FormularioID = newBoton.FormularioID;
            }
            else
            {
                model.AddToSGF_Boton(newBoton);
            }
            model.SaveChanges();
        }


        [OperationContract]
        public void PermisoBoton_Grabar(SGF_PermisoBoton newBoton)
        {
            DataModel model = new DataModel();
            if (model.SGF_PermisoBoton.Count(x => x.BotonID == newBoton.BotonID) > 0)
            {
                SGF_PermisoBoton _boton = model.SGF_PermisoBoton.First(x => x.PermisoBotonID == newBoton.PermisoBotonID);
                _boton.Estado = newBoton.Estado;
                _boton.Usuario = newBoton.Usuario;
            }
            else
            {
                model.AddToSGF_PermisoBoton(newBoton);
            }
            model.SaveChanges();
        }
    }
}
