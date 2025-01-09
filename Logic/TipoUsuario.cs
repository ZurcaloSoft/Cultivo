using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public SGF_TipoUsuario TipoUsuario_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_TipoUsuario.First(x => x.TipoUsuarioID == id);
        }

        [OperationContract]
        public List<SGF_TipoUsuario> TipoUsuario_ObtenerPorUsername(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_TipoUsuario.Where(x => x.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
        }        

        [OperationContract]
        public List<SGF_TipoUsuario> TipoUsuario_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_TipoUsuario.Where(x => x.Estado == true || x.Estado==false).ToList();
        }

        [OperationContract]
        public void TipoUsuario_Grabar(SGF_TipoUsuario newTipoUsuario)
        {
            DataModel model = new DataModel();
            if (model.SGF_TipoUsuario.Count(x => x.TipoUsuarioID == newTipoUsuario.TipoUsuarioID) == 0)
            {
                model.AddToSGF_TipoUsuario(newTipoUsuario);
            }
            else
            {
                SGF_TipoUsuario _modulo = model.SGF_TipoUsuario.First(x => x.TipoUsuarioID == newTipoUsuario.TipoUsuarioID);
                _modulo.Nombre = newTipoUsuario.Nombre;
                _modulo.Descripcion = newTipoUsuario.Descripcion;
            }
            model.SaveChanges();
        }
    
    }
}
