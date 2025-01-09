using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public SGF_DAES DAES_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_DAES.First(x => x.DaesID == id);
        }
        [OperationContract]
        public SGF_DAES_VTA DAES_ObtenerPorID_VTA(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_DAES_VTA.First(x => x.DaesID == id);
        }
        [OperationContract]
        public List<SGF_DAES> DAES_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_DAES.ToList();
        }
        [OperationContract]
        public List<SGF_DAES_VTA> DAES_ObtenerTodo_VTA()
        {
            DataModel model = new DataModel();
            return model.SGF_DAES_VTA.ToList();
        }
        [OperationContract]
        public SGF_DAES DAES_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_DAES.First(x => x.DAE == nombre);
        }
        [OperationContract]
        public List<SGF_DAES> DAES_ObtenerPorAduana(Guid Aduana)
        {
            DataModel model = new DataModel();
            return model.SGF_DAES.Where(x => x.AduanaID == Aduana).ToList();
        }
        [OperationContract]
        public List<SGF_DAES_VTA> DAES_ObtenerPorAduana_VTA(Guid Aduana)
        {
            DataModel model = new DataModel();
            return model.SGF_DAES_VTA.Where(x => x.AduanaID == Aduana).ToList();
        }
        [OperationContract]
        public List<SGF_DAES_VTA> DAES_ObtenerPorPais_VTA(Guid pais)
        {
            DataModel model = new DataModel();
            return model.SGF_DAES_VTA.Where(x => x.PaisID == pais).ToList();
        }
        [OperationContract]
        public void SGF_DAES_Grabar(SGF_DAES newDaes, string nomPC, string ip, string user)
        {
            DataModel model = new DataModel();
            if (model.SGF_DAES.Count(x => x.DaesID == newDaes.DaesID) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_DAES _daes = model.SGF_DAES.First(x => x.DaesID == newDaes.DaesID);
                if (_daes != null)
                {
                    if (newDaes.Codigo != _daes.Codigo)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "Codigo", ValorAnterior = _daes.Codigo, ValorNuevo = newDaes.Codigo, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.Codigo = newDaes.Codigo; }
                    if (newDaes.AduanaID != _daes.AduanaID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "AduanaID", ValorAnterior = _daes.AduanaID.ToString(), ValorNuevo = newDaes.AduanaID.ToString(), FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.AduanaID = newDaes.AduanaID; }
                    if (newDaes.PaisID != _daes.PaisID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "PaisID", ValorAnterior = _daes.PaisID.ToString(), ValorNuevo = newDaes.PaisID.ToString(), FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.PaisID = newDaes.PaisID; }
                    if (newDaes.DAE != _daes.DAE)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "DAE", ValorAnterior = _daes.DAE, ValorNuevo = newDaes.DAE, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.DAE = newDaes.DAE; }
                    if (newDaes.Desde != _daes.Desde)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "Desde", ValorAnterior = _daes.Desde.ToString(), ValorNuevo = newDaes.Desde.ToString(), FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.Desde = newDaes.Desde; }
                    if (newDaes.Hasta != _daes.Hasta)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "Hasta", ValorAnterior = _daes.Hasta.ToString(), ValorNuevo = newDaes.Hasta.ToString(), FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.Hasta = newDaes.Hasta; }
                    if (newDaes.Observaciones != _daes.Observaciones)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Update", Campo = "Observaciones", ValorAnterior = _daes.Observaciones, ValorNuevo = newDaes.Observaciones, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _daes.Observaciones = newDaes.Observaciones; }
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
                    jsonSerializer.Serialize(stringWriter, newDaes);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newDaes.DaesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToSGF_DAES(newDaes);
            }
            model.SaveChanges();
        }
        [OperationContract]
        public void DAES_Eliminar(Guid daesID, string nomPC, string ip, string user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_DAES _daes = model.SGF_DAES.First(x => x.DaesID == daesID);
            if (_daes != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_DAES", Tipo = "Delete", Campo = "Estado", ValorAnterior = _daes.Estado.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = user, RegistroID = daesID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria);
                _daes.Estado = 0;
                model.SaveChanges();
            }
        }
    }
}
