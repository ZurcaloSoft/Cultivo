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
        public SGF_TipoClasificador TipoClasificador_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_TipoClasificador.First(x => x.TipoClasificadorID == id);
        }
        [OperationContract]
        public SGF_TipoClasificador TipoClasificador_ObtenerPorUsername(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_TipoClasificador.First(x => x.Nombre == nombre);
        }

        [OperationContract]
        public List<SGF_TipoClasificador> TipoClasificador_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_TipoClasificador.Where(x => x.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
        }

        [OperationContract]
        public List<SGF_TipoClasificador> TipoClasificador_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_TipoClasificador.ToList();
        }
        [OperationContract]
        public void TipoClasificador_Grabar(SGF_TipoClasificador tipoclasificador)
        {
            DataModel model = new DataModel();
            SGF_TipoClasificador _newTipo = new SGF_TipoClasificador();
            if (model.SGF_TipoClasificador.Count(x => x.TipoClasificadorID == tipoclasificador.TipoClasificadorID) == 0)
                model.AddToSGF_TipoClasificador(tipoclasificador);
            else
            {
                _newTipo = model.SGF_TipoClasificador.First(x => x.TipoClasificadorID == tipoclasificador.TipoClasificadorID);
                _newTipo.Nombre = tipoclasificador.Nombre;
            }
            model.SaveChanges();
        }
    }
}
