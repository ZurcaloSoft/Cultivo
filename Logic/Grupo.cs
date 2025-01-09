using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
      
        //[OperationContract]
        //public SGF_Grupo Grupo_ObtenerPorID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Grupo.First(x => x.GrupoID == id);
        //}
        //[OperationContract]
        //public SGF_Grupo Grupo_ObtenerPorNombre(string nombre)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Grupo.First(x => x.Nombre== nombre);
        //}
        //[OperationContract]
        //public List<SGF_Grupo> Grupo_ObtenerTodo()
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Grupo.ToList();
        //}
        //[OperationContract]
        //public void Grupo_Grabar(SGF_Grupo newgrupo)
        //{
        //    DataModel model = new DataModel();
        //    if (model.SGF_Grupo.Count(x => x.GrupoID == newgrupo.GrupoID) > 0)
        //    {
        //        SGF_Grupo _grupo = model.SGF_Grupo.First(x => x.GrupoID == newgrupo.GrupoID);
        //        _grupo.Codigo = newgrupo.Codigo;
        //        _grupo.Nombre = newgrupo.Nombre;
        //        _grupo.Observaciones = newgrupo.Observaciones;
        //        _grupo.Estado = newgrupo.Estado;
        //    }
        //    else
        //    {
        //        newgrupo.Estado = 1;
        //        model.AddToSGF_Grupo(newgrupo);
        //    }
        //    model.SaveChanges();
        //}
        //[OperationContract]
        //public void Grupo_Eliminar(Guid grupoID, string observacion)
        //{
        //    DataModel model = new DataModel();
        //    SGF_Grupo _grupo = model.SGF_Grupo.First(x => x.GrupoID == grupoID);
        //    if (_grupo != null)
        //    {
        //        _grupo.Estado = 0;
        //        _grupo.Observaciones = observacion;
        //        model.SaveChanges();
        //    }
        //}
    }
}
