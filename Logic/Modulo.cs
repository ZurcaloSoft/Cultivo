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
        public SGF_Modulo Modulo_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Modulo.First(x => x.ModuloID == id);
        }
        [OperationContract]
        public List<SGF_Modulo> Modulol_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_Modulo.Where(x => x.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
        }
        [OperationContract]
        public List<SGF_Modulo> Modulo_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_Modulo.Where(x => x.Estado >= 0).ToList();
        }
        [OperationContract]
        public void Modulo_Grabar(SGF_Modulo newModulo)
        {
            DataModel model = new DataModel();
            if (model.SGF_Modulo.Count(x => x.ModuloID == newModulo.ModuloID) == 0)
            {
                model.AddToSGF_Modulo(newModulo);
            }
            else
            {
                SGF_Modulo _modulo= model.SGF_Modulo.First(x => x.ModuloID == newModulo.ModuloID);
                _modulo.Nombre = newModulo.Nombre;
                _modulo.Descripcion = newModulo.Descripcion;
            }
            model.SaveChanges();
        }
        [OperationContract]
        public void Modulo_Eliminar(Guid moduloID, string observacion)
        {
            DataModel model = new DataModel();
            SGF_Modulo _clasificador = model.SGF_Modulo.First(x => x.ModuloID == moduloID);
            if (_clasificador != null)
            {
                _clasificador.Estado = 0;
                _clasificador.Descripcion = observacion;
                model.SaveChanges();
            }
        }
    }
}
