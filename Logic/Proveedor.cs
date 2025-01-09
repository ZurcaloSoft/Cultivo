using Newtonsoft.Json;
using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        #region Proveedor
        [OperationContract]
        public void Proveedor_Grabar(CO_PROVEEDOR newProveedor, string nomPC, string ip)
        {
            DataModel model = new DataModel();
            if (model.CO_PROVEEDOR.Count(x => x.CO_PRO_COD == newProveedor.CO_PRO_COD && x.CO_EMP_RUC == newProveedor.CO_EMP_RUC) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                CO_PROVEEDOR _proveedor = model.CO_PROVEEDOR.First(x => x.CO_PRO_COD == newProveedor.CO_PRO_COD && x.CO_EMP_RUC == newProveedor.CO_EMP_RUC);
                if (_proveedor != null)
                {
                    if (newProveedor.CO_TIP_PRO_COD != _proveedor.CO_TIP_PRO_COD)//Codigo Proveedor
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_TIP_PRO_COD", ValorAnterior = _proveedor.CO_TIP_PRO_COD.ToString(), ValorNuevo = newProveedor.CO_TIP_PRO_COD.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_TIP_PRO_COD = newProveedor.CO_TIP_PRO_COD; }
                    if (newProveedor.CO_TIP_IDE_COD != _proveedor.CO_TIP_IDE_COD)//Tipo documento
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_TIP_IDE_COD", ValorAnterior = _proveedor.CO_TIP_IDE_COD.ToString(), ValorNuevo = newProveedor.CO_TIP_IDE_COD.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_TIP_IDE_COD = newProveedor.CO_TIP_IDE_COD; }
                    if (newProveedor.CO_PRO_CED != _proveedor.CO_PRO_CED)//Identificación
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_CED", ValorAnterior = _proveedor.CO_PRO_CED.ToString(), ValorNuevo = newProveedor.CO_PRO_CED.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CED = newProveedor.CO_PRO_CED; }
                    if (newProveedor.CO_PRO_NOM != _proveedor.CO_PRO_NOM)//Nombre
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_NOM", ValorAnterior = _proveedor.CO_PRO_NOM.ToString(), ValorNuevo = newProveedor.CO_PRO_NOM.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_NOM = newProveedor.CO_PRO_NOM; }
                    if (newProveedor.CO_PRO_CON != _proveedor.CO_PRO_CON)//Representante Legal
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_CON", ValorAnterior = _proveedor.CO_PRO_CON.ToString(), ValorNuevo = newProveedor.CO_PRO_CON.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CON = newProveedor.CO_PRO_CON; }
                    if (newProveedor.CO_PRO_PAI != _proveedor.CO_PRO_PAI)//País
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_PAI", ValorAnterior = _proveedor.CO_PRO_PAI.ToString(), ValorNuevo = newProveedor.CO_PRO_PAI.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_PAI = newProveedor.CO_PRO_PAI; }
                    if (newProveedor.CO_PRO_CIU != _proveedor.CO_PRO_CIU)//Ciudad
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_CIU", ValorAnterior = _proveedor.CO_PRO_CIU.ToString(), ValorNuevo = newProveedor.CO_PRO_CIU.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CIU = newProveedor.CO_PRO_CIU; }
                    if (newProveedor.CO_PRO_DIR != _proveedor.CO_PRO_DIR)//Dirección
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_DIR", ValorAnterior = _proveedor.CO_PRO_DIR.ToString(), ValorNuevo = newProveedor.CO_PRO_DIR.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_DIR = newProveedor.CO_PRO_DIR; }
                    if (newProveedor.CO_PRO_TEL1 != _proveedor.CO_PRO_TEL1)//Teléfono 1
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_TEL1", ValorAnterior = _proveedor.CO_PRO_TEL1.ToString(), ValorNuevo = newProveedor.CO_PRO_TEL1.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_TEL1 = newProveedor.CO_PRO_TEL1; }
                    if (newProveedor.CO_PRO_TEL2 != _proveedor.CO_PRO_TEL2)//Teléfono 2
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_TEL2", ValorAnterior = _proveedor.CO_PRO_TEL2.ToString(), ValorNuevo = newProveedor.CO_PRO_TEL2.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_TEL2 = newProveedor.CO_PRO_TEL2; }
                    if (newProveedor.CO_PRO_FAX != _proveedor.CO_PRO_FAX)//Fax
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_FAX", ValorAnterior = _proveedor.CO_PRO_FAX.ToString(), ValorNuevo = newProveedor.CO_PRO_FAX.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_FAX = newProveedor.CO_PRO_FAX; }
                    if (newProveedor.CO_PRO_CEL != _proveedor.CO_PRO_CEL)//Celular
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_CEL", ValorAnterior = _proveedor.CO_PRO_CEL.ToString(), ValorNuevo = newProveedor.CO_PRO_CEL.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CEL = newProveedor.CO_PRO_CEL; }
                    if (newProveedor.CO_PRO_MAI != _proveedor.CO_PRO_MAI)//Email
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_MAI", ValorAnterior = _proveedor.CO_PRO_MAI.ToString(), ValorNuevo = newProveedor.CO_PRO_MAI.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_MAI = newProveedor.CO_PRO_MAI; }
                    if (newProveedor.CO_PRO_CUE != _proveedor.CO_PRO_CUE)//Cuenta
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_CUE", ValorAnterior = _proveedor.CO_PRO_CUE.ToString(), ValorNuevo = newProveedor.CO_PRO_CUE.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CUE = newProveedor.CO_PRO_CUE; }
                    if (newProveedor.CO_PRO_CON_ESP != _proveedor.CO_PRO_CON_ESP)//Contribuyente Especial
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_TIP_PRO_COD", ValorAnterior = _proveedor.CO_PRO_CON_ESP.ToString(), ValorNuevo = newProveedor.CO_PRO_CON_ESP.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CON_ESP = newProveedor.CO_PRO_CON_ESP; }
                    if (newProveedor.CO_PRO_CRE != _proveedor.CO_PRO_CRE)//Crédito
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_CRE", ValorAnterior = _proveedor.CO_PRO_CRE.ToString(), ValorNuevo = newProveedor.CO_PRO_CRE.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_CRE = newProveedor.CO_PRO_CRE; }
                    if (newProveedor.CO_PRO_LIM_CRE != _proveedor.CO_PRO_LIM_CRE)//Límite Crédito
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_LIM_CRE", ValorAnterior = _proveedor.CO_PRO_LIM_CRE.ToString(), ValorNuevo = newProveedor.CO_PRO_LIM_CRE.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_LIM_CRE = newProveedor.CO_PRO_LIM_CRE; }
                    if (newProveedor.CO_PRO_USU_ALI != _proveedor.CO_PRO_USU_ALI)//Usuario
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_USU_ALI", ValorAnterior = _proveedor.CO_PRO_USU_ALI.ToString(), ValorNuevo = newProveedor.CO_PRO_USU_ALI.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_USU_ALI = newProveedor.CO_PRO_USU_ALI; }
                    if (newProveedor.CO_PRO_MAI2 != _proveedor.CO_PRO_MAI2)//Email 2
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_MAI2", ValorAnterior = _proveedor.CO_PRO_MAI2.ToString(), ValorNuevo = newProveedor.CO_PRO_MAI2.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_MAI2 = newProveedor.CO_PRO_MAI2; }
                    if (newProveedor.CO_PRO_MAI3 != _proveedor.CO_PRO_MAI3)//Email 3
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CO_PRO_MAI3", ValorAnterior = _proveedor.CO_PRO_MAI3.ToString(), ValorNuevo = newProveedor.CO_PRO_MAI3.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CO_PRO_MAI3 = newProveedor.CO_PRO_MAI3; }
                    //,[CO_PRO_EST]=@CO_PRO_EST
                    if (newProveedor.TIENE_CULTIVO != _proveedor.TIENE_CULTIVO)//Tiene Cultivo
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "TIENE_CULTIVO", ValorAnterior = _proveedor.TIENE_CULTIVO == null ? "" : _proveedor.TIENE_CULTIVO.ToString(), ValorNuevo = newProveedor.TIENE_CULTIVO.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.TIENE_CULTIVO = newProveedor.TIENE_CULTIVO; }
                    if (newProveedor.SOLO_ENVIO != _proveedor.SOLO_ENVIO)//Solo Envío sin proyección
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "SOLO_ENVIO", ValorAnterior = _proveedor.SOLO_ENVIO == null ? "" : _proveedor.SOLO_ENVIO.ToString(), ValorNuevo = newProveedor.SOLO_ENVIO.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.SOLO_ENVIO = newProveedor.SOLO_ENVIO; }
                    if (newProveedor.ENTREGA_DIRECTA != _proveedor.ENTREGA_DIRECTA)//Entrega Directa sin proyección ni envío
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "ENTREGA_DIRECTA", ValorAnterior = _proveedor.ENTREGA_DIRECTA == null ? "" : _proveedor.ENTREGA_DIRECTA.ToString(), ValorNuevo = newProveedor.ENTREGA_DIRECTA.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.ENTREGA_DIRECTA = newProveedor.ENTREGA_DIRECTA; }
                    if (newProveedor.OBSERVACIONES != _proveedor.OBSERVACIONES)//Observaciones
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "OBSERVACIONES", ValorAnterior = _proveedor.OBSERVACIONES == null ? "" : _proveedor.OBSERVACIONES.ToString(), ValorNuevo = newProveedor.OBSERVACIONES.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.OBSERVACIONES = newProveedor.OBSERVACIONES; }
                    if (newProveedor.CODIGO != _proveedor.CODIGO)//Código
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Update", Campo = "CODIGO", ValorAnterior = _proveedor.CODIGO == null ? "" : _proveedor.CODIGO.ToString(), ValorNuevo = newProveedor.CODIGO.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.CODIGO = newProveedor.CODIGO; }
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
                    jsonSerializer.Serialize(stringWriter, newProveedor);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProveedor.CO_PRO_USU_ALI, RegistroID = newProveedor.CO_PRO_COD.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToCO_PROVEEDOR(newProveedor);
            }
            model.SaveChanges();
        }

        [OperationContract]
        public CO_PROVEEDOR Proveedor_ObtenerPorID(string id)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_PROVEEDOR.First(x => x.CO_PRO_COD == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_Proveedor_VTA Proveedor_ObtenerPorID_VTA(string id)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proveedor_VTA.First(x => x.ProveedorID == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_Proveedor_VTA Proveedor_ObtenerPorRUC_VTA(string id)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proveedor_VTA.First(x => x.ProveedorCedula == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public CO_PROVEEDOR Proveedor_ObtenerPorIdentificacion(string identificacion)
        {
            DataModel model = new DataModel();
            return model.CO_PROVEEDOR.First(x => x.CO_PRO_CED == identificacion);
        }
        [OperationContract]
        public List<CO_PROVEEDOR> Proveedor_ObtenerTodo()
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_PROVEEDOR.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_Proveedor_VTA> Proveedor_ObtenerTodo_VTA()
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proveedor_VTA.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<CO_PROVEEDOR> Proveedor_ObtenerPorTipoPersona(Guid TipoPersona)
        {
            DataModel model = new DataModel();
            return model.CO_PROVEEDOR.Where(x => x.CO_TIP_PRO_COD == TipoPersona).ToList();
        }

        [OperationContract]
        public List<CO_PROVEEDOR> Proveedor_ObtenerPorEmpresaID(string EmpresaID)
        {
            DataModel model = new DataModel();
            return model.CO_PROVEEDOR.Where(x => x.CO_EMP_RUC == EmpresaID).ToList();
        }
        [OperationContract]
        public List<SGF_Proveedor_VTA> Proveedor_ObtenerPorEmpresaID_VTA(string EmpresaID)
        {
            DataModel model = new DataModel();
            return model.SGF_Proveedor_VTA.Where(x => x.EmpresaID == EmpresaID).ToList();
        }

        [OperationContract]
        public int Proveedor_ValidarCodigoPorEmpresaID(string EmpresaID, string codigo)
        {
            try
            {
                DataModel model = new DataModel();
                var _proveedor = model.CO_PROVEEDOR.Where(x => x.CO_EMP_RUC == EmpresaID).ToList();
                int resultado = 0;
                foreach (var item in _proveedor)
                {
                    string _codi = (item.CODIGO == null ? "" : item.CODIGO.ToUpper());
                    if (item.CO_PRO_EST == "ACTIVO" && _codi.ToUpper() == codigo.ToUpper())
                        resultado = resultado + 1;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [OperationContract]
        public void Proveedor_Eliminar(string ID, string observacion, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_Observaciones _observaciones = new SGF_Observaciones();
            CO_PROVEEDOR _proveedor = model.CO_PROVEEDOR.First(x => x.CO_PRO_COD == ID);
            if (_proveedor != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_PROVEEDOR", Tipo = "Delete", Campo = "CO_PRO_EST", ValorAnterior = _proveedor.CO_PRO_EST.ToString(), ValorNuevo = "PASIVO", FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = ID, IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria);
                _proveedor.CO_PRO_EST = "PASIVO";
                _observaciones.ObservacionesID = Guid.NewGuid();
                _observaciones.RegID = ID;
                _observaciones.SusurcalID = "";
                _observaciones.Descripcion = observacion;
                _observaciones.Usuario = _user;
                _observaciones.ModuloID = "Módulo Recepción";
                _observaciones.Fecha = DateTime.Now;
                _observaciones.EsActivo = true;
                Observaciones_Grabar(_observaciones);
                model.SaveChanges();
            }
        }
        #endregion
        #region Proveedor - Bodega
        [OperationContract]
        public void ProveedorBodega_Grabar(SGF_ProveedorBodega newProveedor, string nomPC, string ip)
        {
            DataModel model = new DataModel();
            if (model.SGF_ProveedorBodega.Count(x => x.ProveedorBodegaID == newProveedor.ProveedorBodegaID) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProveedorBodega _proveedor = model.SGF_ProveedorBodega.First(x => x.ProveedorBodegaID == newProveedor.ProveedorBodegaID);
                if (_proveedor != null)
                {
                    if (newProveedor.ProveedorID != _proveedor.ProveedorID)//Codigo Proveedor
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "ProveedorID", ValorAnterior = _proveedor.ProveedorID.ToString(), ValorNuevo = newProveedor.ProveedorID.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.ProveedorID = newProveedor.ProveedorID; }
                    if (newProveedor.AlmacenID != _proveedor.AlmacenID)//Codigo Almacen /Finca)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "AlmacenID", ValorAnterior = _proveedor.AlmacenID.ToString(), ValorNuevo = newProveedor.AlmacenID.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.AlmacenID = newProveedor.AlmacenID; }
                    if (newProveedor.BodegaID != _proveedor.BodegaID)//Codigo Bodega
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "BodegaID", ValorAnterior = _proveedor.BodegaID.ToString(), ValorNuevo = newProveedor.BodegaID.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.BodegaID = newProveedor.BodegaID; }
                    if (newProveedor.ClasificadorID != _proveedor.ClasificadorID)//Clasificador 
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "ClasificadorID", ValorAnterior = _proveedor.ClasificadorID.ToString(), ValorNuevo = newProveedor.ClasificadorID.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.ClasificadorID = newProveedor.ClasificadorID; }
                    if (newProveedor.Observaciones != _proveedor.Observaciones)//Observaciones
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "Observaciones", ValorAnterior = _proveedor.Observaciones.ToString(), ValorNuevo = newProveedor.Observaciones.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.Observaciones = newProveedor.Observaciones; }
                    if (newProveedor.Usuario != _proveedor.Usuario)//Usuario
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "Usuario", ValorAnterior = _proveedor.Usuario.ToString(), ValorNuevo = newProveedor.Usuario.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.Usuario = newProveedor.Usuario; }
                    if (newProveedor.Estado != _proveedor.Estado)//Estado
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Update", Campo = "Estado", ValorAnterior = _proveedor.Estado == null ? "" : _proveedor.Estado.ToString(), ValorNuevo = newProveedor.Estado.ToString(), FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorBodegaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _proveedor.Estado = newProveedor.Estado; }
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
                    jsonSerializer.Serialize(stringWriter, newProveedor);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorBodega", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProveedor.Usuario, RegistroID = newProveedor.ProveedorID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToSGF_ProveedorBodega(newProveedor);

            }
            model.SaveChanges();
        }
        [OperationContract]
        public List<SGF_ProveedorBodega> ProveedorBodega_ObtenerPorEmpresaProveedor(string empresa, string proveedor)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProveedorBodega.Where(x => x.EmpresaID == empresa && x.ProveedorID == proveedor).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion
    }


}
