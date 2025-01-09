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
        public SGF_Clasificador Clasificador_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador.First(x => x.ClasificadorID == id);
        }
        [OperationContract]
        public List<SGF_Clasificador> Clasificador_ObtenerPorUsername(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador.Where(x => x.Nombre.ToUpper().Contains(nombre.ToUpper())).ToList();
        }
        [OperationContract]
        public List<SGF_Clasificador> Clasificador_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador.ToList();
        }
        [OperationContract]
        public List<SGF_Clasificador> Clasificador_ObtenerPorTipoClasificador(Guid TipoClasificador)
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador.Where(x => x.TipoClasificadorID == TipoClasificador).OrderBy(x => x.Nombre).ToList();
        }
        [OperationContract]
        public void Clasificador_Grabar(SGF_Clasificador newclasificador)
        {
            DataModel model = new DataModel();
            if (model.SGF_Clasificador.Count(x => x.ClasificadorID == newclasificador.ClasificadorID) > 0)
            {
                SGF_Clasificador _clasificador = model.SGF_Clasificador.First(x => x.ClasificadorID == newclasificador.ClasificadorID);
                _clasificador.Nombre = newclasificador.Nombre;
                _clasificador.ParentID = newclasificador.ParentID;
                _clasificador.Codigo = newclasificador.Codigo;
                _clasificador.Valor = newclasificador.Valor;
                _clasificador.Descripcion = newclasificador.Descripcion;
            }
            else
            {
                newclasificador.Estado = 1;
                model.AddToSGF_Clasificador(newclasificador);
            }
            model.SaveChanges();
        }

        [OperationContract]
        public void Clasificador_Eliminar(Guid clasificadorID, string observacion)
        {
            DataModel model = new DataModel();
            SGF_Clasificador _clasificador = model.SGF_Clasificador.First(x => x.ClasificadorID == clasificadorID);
            if (_clasificador != null)
            {
                _clasificador.Estado = 0;
                _clasificador.Descripcion = observacion;
                model.SaveChanges();
            }
        }
        [OperationContract]
        public void Clasificador_Activar(Guid clasificadorID, string observacion)
        {
            DataModel model = new DataModel();
            SGF_Clasificador _clasificador = model.SGF_Clasificador.First(x => x.ClasificadorID == clasificadorID);
            if (_clasificador != null)
            {
                _clasificador.Estado = 1;
                _clasificador.Descripcion = observacion;
                model.SaveChanges();
            }
        }
        [OperationContract]
        public SGF_Clasificador_VTA Clasificador_ObtenerPorID_VTA(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador_VTA.First(x => x.ClasificadorID == id);
        }
        [OperationContract]
        public List<SGF_Clasificador_VTA> Clasificador_ObtenerPorUsername_VTA(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador_VTA.Where(x => x.NombreClasificador.ToUpper().Contains(nombre.ToUpper())).ToList();
        }
        [OperationContract]
        public List<SGF_Clasificador_VTA> Clasificador_ObtenerTodo_VTA()
        {
            DataModel model = new DataModel();
            return model.SGF_Clasificador_VTA.ToList();
        }

        [OperationContract]
        public List<SGF_Clasificador_VTA> Clasificador_Buscar_VTA(string nombre, Guid tipoClasificadorID, Guid parentID)
        {
            DataModel model = new DataModel();
            List<SGF_Clasificador_VTA> _resultado = new List<SGF_Clasificador_VTA>();
            if (nombre == "" && tipoClasificadorID == Guid.Empty && parentID == Guid.Empty)
                _resultado = model.SGF_Clasificador_VTA.ToList();
            if (nombre != "")
                _resultado = model.SGF_Clasificador_VTA.Where(x => x.NombreClasificador.ToUpper().Contains(nombre.ToUpper())).ToList();
            if (tipoClasificadorID != Guid.Empty)
                _resultado = model.SGF_Clasificador_VTA.Where(x => x.TipoClasificadorID == tipoClasificadorID).ToList();
            //if (parentID != Guid.Empty)
            //    _resultado = model.SGF_Clasificador_VTA.Where(x => x.ParentID == parentID).ToList();
            return _resultado;
        }
    }
}
