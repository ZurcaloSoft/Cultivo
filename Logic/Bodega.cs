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
        //public SGF_Bodega Bodega_ObtenerPorID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Bodega.First(x => x.BodegaID == id);
        //}
        //[OperationContract]
        //public SGF_Bodega Bodega_ObtenerPorNombre(string nombre)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Bodega.First(x => x.Nombre == nombre);
        //}
        //[OperationContract]
        //public List<SGF_Bodega> Bodega_ObtenerPorEmpresa(Guid almacen)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Bodega.Where(x => x.AlmacenID == almacen).ToList();
        //}
        //[OperationContract]
        //public List<SGF_Bodega> Bodega_ObtenerTodo()
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Bodega.ToList();
        //}
        //[OperationContract]
        //public void Bodega_Grabar(SGF_Bodega newbodega)
        //{

        //    DataModel model = new DataModel();
        //    if (model.SGF_Bodega.Count(x => x.BodegaID == newbodega.BodegaID) > 0)
        //    {
        //        SGF_Bodega _empresa = model.SGF_Bodega.First(x => x.BodegaID == newbodega.BodegaID);
        //        _empresa.AlmacenID = newbodega.AlmacenID;
        //        _empresa.Nombre = newbodega.Nombre;
        //        _empresa.Ubicacion = newbodega.Ubicacion;
        //        _empresa.Estado = newbodega.Estado;
        //    }
        //    else
        //    {
        //        model.AddToSGF_Bodega(newbodega);

        //    }

        //    model.SaveChanges();
        //}
        //[OperationContract]
        //public void Bodega_Eliminar(Guid bodegaID)
        //{
        //    DataModel model = new DataModel();
        //    SGF_Bodega _bodega = model.SGF_Bodega.First(x => x.BodegaID == bodegaID);
        //    if (_bodega != null)
        //    {
        //        _bodega.Estado = 0;
        //        model.SaveChanges();
        //    }
        //}

        #region Nuevas tablas
        [OperationContract]
        public CO_BODEGA Bodega_ObtenerPorID(int id)
        {
            DataModel model = new DataModel();
            return model.CO_BODEGA.First(x => x.CO_BOD_COD == id);
        }
        [OperationContract]
        public CO_BODEGA Bodega_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.CO_BODEGA.First(x => x.CO_BOD_NOM.ToUpper().Trim() == nombre.ToUpper().Trim());
        }
        [OperationContract]
        public List<CO_BODEGA> Bodega_ObtenerPorNombres(string nombre)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_BODEGA.Where(x => x.CO_BOD_NOM.Trim().ToUpper().Contains(nombre.Trim().ToUpper())).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_Bodega_VTA> Bodega_ObtenerPorAlmacen(int almacen)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Bodega_VTA.Where(x => x.CO_ALM_COD == almacen).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [OperationContract]
        public List<SGF_Bodega_VTA> Bodega_ObtenerPorAlmacenEmpresa_VTA(string empresa, int almacen)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Bodega_VTA.Where(x => x.CO_EMP_RUC == empresa && x.CO_ALM_COD == almacen).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [OperationContract]
        public List<CO_BODEGA> Bodega_ObtenerPorAlmacenEmpresa(string empresa, int almacen)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_BODEGA.Where(x => x.CO_EMP_RUC == empresa && x.CO_ALM_COD == almacen).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [OperationContract]
        public List<CO_BODEGA> Bodega_ObtenerPorEmpresa(string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_BODEGA.Where(x => x.CO_EMP_RUC == empresa).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_Bodega_VTA> Bodega_ObtenerPorEmpresa_VTA(string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Bodega_VTA.Where(x => x.CO_EMP_RUC == empresa).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<CO_BODEGA> Bodega_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.CO_BODEGA.ToList();
        }
        [OperationContract]
        public List<SGF_Bodega_VTA> Bodega_ObtenerTodo_VTA()
        {
            DataModel model = new DataModel();
            return model.SGF_Bodega_VTA.ToList();
        }
        [OperationContract]
        public void Bodega_Grabar(CO_BODEGA newbodega, string nomPC, string ip, string userID)
        {

            DataModel model = new DataModel();
            if (model.CO_BODEGA.Count(x => x.CO_BOD_COD == newbodega.CO_BOD_COD) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                CO_BODEGA _bodega = model.CO_BODEGA.First(x => x.CO_BOD_COD == newbodega.CO_BOD_COD);


                if (_bodega != null)
                {
                    if (newbodega.CO_BOD_NOM != _bodega.CO_BOD_NOM)//Nombre Bodega
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_BODEGA", Tipo = "Update", Campo = "CO_BOD_NOM", ValorAnterior = _bodega.CO_BOD_NOM, ValorNuevo = newbodega.CO_BOD_NOM, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newbodega.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _bodega.CO_BOD_NOM = newbodega.CO_BOD_NOM; }
                    if (newbodega.TipoBodegaID != _bodega.TipoBodegaID)//Tipo Bodega
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_BODEGA", Tipo = "Update", Campo = "TipoBodegaID", ValorAnterior = _bodega.TipoBodegaID.ToString(), ValorNuevo = newbodega.TipoBodegaID.ToString(), FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newbodega.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _bodega.TipoBodegaID = newbodega.TipoBodegaID; }
                    if (newbodega.Ubicacion != _bodega.Ubicacion)//Ubicación
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_BODEGA", Tipo = "Update", Campo = "Ubicacion", ValorAnterior = _bodega.Ubicacion, ValorNuevo = newbodega.Ubicacion, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newbodega.CO_ALM_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _bodega.Ubicacion = newbodega.Ubicacion; }

                }
                _bodega.TipoBodegaID = newbodega.TipoBodegaID;
                _bodega.CO_BOD_NOM = newbodega.CO_BOD_NOM;
                _bodega.Ubicacion = newbodega.Ubicacion;
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
                    jsonSerializer.Serialize(stringWriter, newbodega);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_BODEGA", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newbodega.CO_BOD_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToCO_BODEGA(newbodega);

            }

            model.SaveChanges();
        }
        [OperationContract]
        public void Bodega_Eliminar(int bodegaID, string observacion, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_Observaciones _observaciones = new SGF_Observaciones();
            CO_BODEGA _bodega = model.CO_BODEGA.First(x => x.CO_BOD_COD == bodegaID);
            if (_bodega != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_BODEGA", Tipo = "Delete", Campo = "Estado", ValorAnterior = _bodega.Estado.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = bodegaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                _bodega.Estado = 0;
                _observaciones.ObservacionesID = Guid.NewGuid();
                _observaciones.RegID = bodegaID.ToString();
                _observaciones.SusurcalID = _bodega.CO_ALM_COD.ToString();
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
