using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using Newtonsoft.Json;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        //[OperationContract]
        //public SGF_Almacen Alamcen_ObtenerPorID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Almacen.First(x => x.AlmacenID == id);
        //}
        //[OperationContract]
        //public SGF_Almacen Almacen_ObtenerPorNombre(string nombre)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Almacen.First(x => x.Nombre == nombre);
        //}
        //[OperationContract]
        //public List<SGF_Almacen> Almacen_ObtenerPorSucursal(Guid sucursal)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Almacen.Where(x => x.AlmacenID == sucursal).ToList();
        //}
        //[OperationContract]
        //public List<SGF_Almacen> Almacen_ObtenerTodo()
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Almacen.ToList();
        //}
        //[OperationContract]
        //public void Almacen_Grabar(SGF_Almacen newalmacen)
        //{
        //    DataModel model = new DataModel();
        //    if (model.SGF_Almacen.Count(x => x.AlmacenID == newalmacen.AlmacenID) > 0)
        //    {
        //        SGF_Almacen _empresa = model.SGF_Almacen.First(x => x.AlmacenID == newalmacen.AlmacenID);
        //        _empresa.SucursalID = newalmacen.SucursalID;
        //        _empresa.Nombre = newalmacen.Nombre;
        //        _empresa.Direccion = newalmacen.Direccion;
        //        _empresa.Estado = newalmacen.Estado;
        //    }
        //    else
        //    {
        //        model.AddToSGF_Almacen(newalmacen);

        //    }

        //    model.SaveChanges();
        //}
        //[OperationContract]
        //public void Almacen_Eliminar(Guid sucursalID)
        //{
        //    DataModel model = new DataModel();
        //    SGF_Almacen _almacen = model.SGF_Almacen.First(x => x.SucursalID == sucursalID);
        //    if (_almacen != null)
        //    {
        //        _almacen.Estado = 0;
        //        model.SaveChanges();
        //    }
        //}


        #region Nueva Tabla
        [OperationContract]
        public CO_ALMACEN Almacen_ObtenerPorID(int id)
        {
            DataModel model = new DataModel();
            return model.CO_ALMACEN.First(x => x.CO_ALM_COD == id);
        }
        [OperationContract]
        public List<CO_ALMACEN> Almacen_ObtenerPorEmpresa(string ruc)
        {
            DataModel model = new DataModel();
            return model.CO_ALMACEN.Where(x => x.CO_EMP_RUC == ruc).ToList();
        }
        [OperationContract]
        public CO_ALMACEN Almacen_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.CO_ALMACEN.First(x => x.CO_ALM_NOM == nombre);
        }
        [OperationContract]
        public List<CO_ALMACEN> Almacen_ObtenerPorContenerNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.CO_ALMACEN.Where(x => x.CO_ALM_NOM.ToUpper().Contains(nombre.ToUpper())).ToList();
        }
        [OperationContract]
        public List<CO_ALMACEN> Almacen_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.CO_ALMACEN.ToList();
        }
        [OperationContract]
        public void Almacen_Grabar(CO_ALMACEN newalmacen, string nomPC, string ip, string userID)
        {
            DataModel model = new DataModel();
            if (model.CO_ALMACEN.Count(x => x.CO_ALM_COD == newalmacen.CO_ALM_COD) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                CO_ALMACEN _almacen = model.CO_ALMACEN.First(x => x.CO_ALM_COD == newalmacen.CO_ALM_COD);

                if (_almacen != null)
                {
                    if (newalmacen.CO_ALM_NOM != _almacen.CO_ALM_NOM)//Nombre Almacen 
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_ALMACEN", Tipo = "Update", Campo = "CO_ALM_NOM", ValorAnterior = _almacen.CO_ALM_NOM, ValorNuevo = newalmacen.CO_ALM_NOM, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newalmacen.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _almacen.CO_ALM_NOM = newalmacen.CO_ALM_NOM; }
                    if (newalmacen.Telefono != _almacen.Telefono)//Telefono
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_ALMACEN", Tipo = "Update", Campo = "Telefono", ValorAnterior = _almacen.Telefono, ValorNuevo = newalmacen.Telefono, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newalmacen.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _almacen.Telefono = newalmacen.Telefono; }
                    if (newalmacen.Direccion != _almacen.Direccion)//Direccion
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_ALMACEN", Tipo = "Update", Campo = "Direccion", ValorAnterior = _almacen.Direccion, ValorNuevo = newalmacen.Direccion, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newalmacen.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _almacen.Direccion = newalmacen.Direccion; }

                }
            }
            else
            {
                // Crear y configurar el JsonSerializer
                var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented // Para una salida JSON más legible
                });
                // Serializar el objeto a JSON string
                using (var stringWriter = new StringWriter())
                {
                    jsonSerializer.Serialize(stringWriter, newalmacen);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_ALMACEN", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newalmacen.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToCO_ALMACEN(newalmacen);
            }

            model.SaveChanges();
        }
        [OperationContract]
        public void Almacen_Eliminar(int ID, string observacion, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_Observaciones _observaciones = new SGF_Observaciones();
            CO_ALMACEN _almacen = model.CO_ALMACEN.First(x => x.CO_ALM_COD == ID);
            if (_almacen != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_ALMACEN", Tipo = "Delete", Campo = "Estado", ValorAnterior = _almacen.Estado.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = ID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                _almacen.Estado = 0;
                _observaciones.ObservacionesID = Guid.NewGuid();
                _observaciones.RegID = ID.ToString();
                _observaciones.SusurcalID = ID.ToString();
                _observaciones.Descripcion = observacion;
                _observaciones.Usuario = _user;
                _observaciones.ModuloID = "Módulo Administración";
                _observaciones.Fecha = DateTime.Now;
                _observaciones.EsActivo = true;
                Observaciones_Grabar(_observaciones);
                model.SaveChanges();
            }
        }
        #endregion
    }
}
