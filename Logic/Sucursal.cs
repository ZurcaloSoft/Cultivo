using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        //[OperationContract]
        //public SGF_Sucursal Sucursal_ObtenerPorID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Sucursal.First(x => x.SucursalID == id);
        //}
        //[OperationContract]
        //public SGF_Sucursal Sucursal_ObtenerPorNombre(string nombre)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Sucursal.First(x => x.Nombre == nombre);
        //}
        //[OperationContract]
        //public List<SGF_Sucursal> Sucursal_ObtenerPorEmpresa(Guid empresa)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Sucursal.Where(x => x.EmpresaID == empresa).ToList();
        //}
        //[OperationContract]
        //public List<SGF_Sucursal> Sucursal_ObtenerTodo()
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Sucursal.ToList();
        //}
        //[OperationContract]
        //public void Sucursal_Grabar(SGF_Sucursal newsucursal)
        //{
        //    DataModel model = new DataModel();
        //    if (model.SGF_Sucursal.Count(x => x.SucursalID == newsucursal.SucursalID) > 0)
        //    {
        //        SGF_Sucursal _empresa = model.SGF_Sucursal.First(x => x.SucursalID == newsucursal.SucursalID);
        //        _empresa.EmpresaID = newsucursal.EmpresaID;
        //        _empresa.Nombre = newsucursal.Nombre;
        //        _empresa.Telefono = newsucursal.Telefono;
        //        _empresa.Direccion = newsucursal.Direccion;
        //        _empresa.Estado = newsucursal.Estado;
        //    }
        //    else
        //    {
        //        model.AddToSGF_Sucursal(newsucursal);

        //    }

        //    model.SaveChanges();
        //}
        //[OperationContract]
        //public void Sucursal_Eliminar(Guid sucursalID)
        //{
        //    DataModel model = new DataModel();
        //    SGF_Sucursal _sucursal = model.SGF_Sucursal.First(x => x.SucursalID == sucursalID);
        //    if (_sucursal != null)
        //    {
        //        _sucursal.Estado = 0;
        //        model.SaveChanges();
        //    }
        //}
    }
}
