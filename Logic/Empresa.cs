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
        //public SGF_Empresa  Empresa_ObtenerPorID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Empresa.First(x => x.EmpresaID == id);
        //}
        //[OperationContract]
        //public List<SGF_Empresa> Empresa_ObtenerGrupoID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Empresa.Where(x => x.GrupoID == id).ToList();
        //}
        //[OperationContract]
        //public SGF_Empresa Empresa_ObtenerPorNombre(string nombre)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Empresa.First(x => x.RazonSocial == nombre);
        //}
        //[OperationContract]
        //public SGF_Empresa Empresa_ObtenerPorRUC(string ruc)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Empresa.First(x => x.RUC == ruc);
        //}
        //[OperationContract]
        //public List<SGF_Empresa> Empresa_ObtenerTodo()
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Empresa.ToList();
        //}
        //[OperationContract]
        //public void Empresa_Grabar(SGF_Empresa newempresa)
        //{
        //    DataModel model = new DataModel();
        //    if (model.SGF_Empresa.Count(x => x.GrupoID == newempresa.GrupoID) > 0)
        //    {
        //        SGF_Empresa _empresa = model.SGF_Empresa.First(x => x.EmpresaID == newempresa.EmpresaID);
        //        _empresa.GrupoID = newempresa.GrupoID;
        //        _empresa.RUC = newempresa.RUC;
        //        _empresa.RazonSocial = newempresa.RazonSocial;
        //        _empresa.NombreComercial = newempresa.NombreComercial;
        //        _empresa.RepresentanteLegal = newempresa.RepresentanteLegal;
        //        _empresa.CedulaRepresentante = newempresa.CedulaRepresentante;
        //        _empresa.Ciudad = newempresa.Ciudad;
        //        _empresa.Telefono = newempresa.Telefono;
        //        _empresa.Celular = newempresa.Celular;
        //        _empresa.Fax = newempresa.Fax;
        //        _empresa.Email = newempresa.Email;
        //        _empresa.Direccion = newempresa.Direccion;
        //        _empresa.Estado = newempresa.Estado;
        //    }
        //    else
        //    {
        //        model.AddToSGF_Empresa(newempresa);
        //    }
        //    model.SaveChanges();
        //}
        //[OperationContract]
        //public void Empresa_Eliminar(Guid empresaID)
        //{
        //    DataModel model = new DataModel();
        //    SGF_Empresa _empresa = model.SGF_Empresa.First(x => x.EmpresaID == empresaID);
        //    if (_empresa != null)
        //    {
        //        _empresa.Estado = 0;
        //        model.SaveChanges();
        //    }
        //}


        #region Nuevas Tablas
        //[OperationContract]
        //public CO_EMPRESA Empresa_ObtenerPorID(string ruc)
        //{
        //    DataModel model = new DataModel();
        //    return model.CO_EMPRESA.First(x => x.CO_EMP_RUC == ruc);
        //}
        [OperationContract]
        public List<CO_EMPRESA> Empresa_ObtenerGrupoID(string idGrupo)
        {
            DataModel model = new DataModel();
            return model.CO_EMPRESA.Where(x => x.EMPRESA_PADRE == idGrupo).ToList();
        }
        [OperationContract]
        public CO_EMPRESA Empresa_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.CO_EMPRESA.First(x => x.CO_EMP_RAZ_SOC.ToUpper() == nombre.ToUpper());
        }

        [OperationContract]
        public List<CO_EMPRESA> Empresa_ObtenerPorContenerNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.CO_EMPRESA.Where(x => x.CO_EMP_RAZ_SOC.ToUpper().Contains(nombre.ToUpper())).ToList();
        }
        [OperationContract]
        public CO_EMPRESA Empresa_ObtenerPorRUC(string ruc)
        {
            DataModel model = new DataModel();
            return model.CO_EMPRESA.First(x => x.CO_EMP_RUC == ruc);
        }
        [OperationContract]
        public List<CO_EMPRESA> Empresa_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.CO_EMPRESA.ToList();
        }
        [OperationContract]
        public void Empresa_Grabar(CO_EMPRESA newempresa, string userID, string ip, string nomPC)
        {
            DataModel model = new DataModel();
            if (model.CO_EMPRESA.Count(x => x.CO_EMP_RUC == newempresa.CO_EMP_RUC) > 0)
            {
                CO_EMPRESA _empresa = model.CO_EMPRESA.First(x => x.CO_EMP_RUC == newempresa.CO_EMP_RUC);
                _empresa.CO_EMP_RUC = newempresa.CO_EMP_RUC;
                _empresa.CO_EMP_NOM = newempresa.CO_EMP_NOM;
                _empresa.CO_EMP_NOM_REP = newempresa.CO_EMP_NOM_REP;
                _empresa.CO_EMP_NOM_CON = newempresa.CO_EMP_NOM_CON;
                _empresa.CO_EMP_REG_CON = newempresa.CO_EMP_REG_CON;
                _empresa.CO_EMP_DIR = newempresa.CO_EMP_DIR;
                _empresa.CO_EMP_CIU = newempresa.CO_EMP_CIU;
                _empresa.CO_EMP_TEL = newempresa.CO_EMP_TEL;
                _empresa.CO_EMP_FAX = newempresa.CO_EMP_FAX;
                _empresa.CO_EMP_TIP_IDE = newempresa.CO_EMP_TIP_IDE;
                _empresa.CO_EMP_REP_IDE = newempresa.CO_EMP_REP_IDE;
                _empresa.CO_EMP_RUC_CON = newempresa.CO_EMP_RUC_CON;
                _empresa.CO_EMP_MAI = newempresa.CO_EMP_MAI;
                _empresa.CO_EMP_RAZ_SOC = newempresa.CO_EMP_RAZ_SOC;
                _empresa.CO_CONTRIBUYENTE_ESPECIAL = newempresa.CO_CONTRIBUYENTE_ESPECIAL;
                _empresa.CO_CONTRIBUYENTE_NUMERO = newempresa.CO_CONTRIBUYENTE_NUMERO;
                _empresa.CO_OBLIGADO_CONTABILIDAD = newempresa.CO_OBLIGADO_CONTABILIDAD;
                _empresa.CO_EMPRESA_LOGO = newempresa.CO_EMPRESA_LOGO;
                _empresa.CO_EMPRESA_LOGO_FILE = newempresa.CO_EMPRESA_LOGO_FILE;
                _empresa.CO_EMPRESA_LEYENDA = newempresa.CO_EMPRESA_LEYENDA;
                _empresa.CO_EMPRESA_FIRMA = newempresa.CO_EMPRESA_FIRMA;
                _empresa.CO_EMPRESA_FIRMA_FILE = newempresa.CO_EMPRESA_FIRMA_FILE;
                _empresa.CO_EMPRESA_FIRMA_CLAVE = newempresa.CO_EMPRESA_FIRMA_CLAVE;
                _empresa.CO_FECHA_CREACION = newempresa.CO_FECHA_CREACION;
                _empresa.CO_FECHA_ACTUALIZACION = newempresa.CO_FECHA_ACTUALIZACION;
                _empresa.CO_ESTADO = newempresa.CO_ESTADO;
                _empresa.EMPRESA_PADRE = newempresa.EMPRESA_PADRE;
                _empresa.ES_GRUPO_EMPRESA = newempresa.ES_GRUPO_EMPRESA;
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
                    jsonSerializer.Serialize(stringWriter, newempresa);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_EMPRESA", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = userID, RegistroID = newempresa.CO_EMP_RUC.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToCO_EMPRESA(newempresa);
            }
            model.SaveChanges();
        }

        [OperationContract]
        public void Empresa_Eliminar(string ID, string observacion, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_Observaciones _observaciones = new SGF_Observaciones();
            CO_EMPRESA _empresa = model.CO_EMPRESA.First(x => x.CO_EMP_RUC == ID);
            if (_empresa != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_EMPRESA", Tipo = "Delete", Campo = "CO_ESTADO", ValorAnterior = _empresa.CO_ESTADO.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = ID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                _empresa.CO_ESTADO = 0;
                _observaciones.ObservacionesID = Guid.NewGuid();
                _observaciones.RegID = ID.ToString();
                _observaciones.SusurcalID = "";
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
