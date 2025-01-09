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
        [OperationContract]
        public SGF_LineaAerea LineaAerea_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_LineaAerea.First(x => x.LineaAereaID == id);
        }
        [OperationContract]
        public List<SGF_LineaAerea> LineaAerea_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_LineaAerea.ToList();
        }
        [OperationContract]
        public SGF_LineaAerea LineaAerea_ObtenerPorNombre(string nombre)
        {
            DataModel model = new DataModel();
            return model.SGF_LineaAerea.First(x => x.Nombre == nombre);
        }
        [OperationContract]
        public List<SGF_LineaAerea> LineaAerea_ObtenerPorEmpresa(Guid linearea)
        {
            DataModel model = new DataModel();
            return model.SGF_LineaAerea.Where(x => x.LineaAereaID == linearea).ToList();
        }
        [OperationContract]
        public void LineaAerea_Grabar(SGF_LineaAerea newlinearea, string nomPC, string ip, string user)
        {
            DataModel model = new DataModel();
            if (model.SGF_LineaAerea.Count(x => x.LineaAereaID == newlinearea.LineaAereaID) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_LineaAerea _linea = model.SGF_LineaAerea.First(x => x.LineaAereaID == newlinearea.LineaAereaID);
                if (_linea != null)
                {
                    if (newlinearea.Codigo != _linea.Codigo)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "Codigo", ValorAnterior = _linea.Codigo, ValorNuevo = newlinearea.Codigo, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.Codigo = newlinearea.Codigo; }
                    if (newlinearea.Nombre != _linea.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "Nombre", ValorAnterior = _linea.Nombre, ValorNuevo = newlinearea.Nombre, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.Nombre = newlinearea.Nombre; }
                    if (newlinearea.CedulaRUC != _linea.CedulaRUC)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "CedulaRUC", ValorAnterior = _linea.CedulaRUC, ValorNuevo = newlinearea.CedulaRUC, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.CedulaRUC = newlinearea.CedulaRUC; }
                    if (newlinearea.CodigoCAE != _linea.CodigoCAE)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "CodigoCAE", ValorAnterior = _linea.CodigoCAE, ValorNuevo = newlinearea.CodigoCAE, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.CodigoCAE = newlinearea.CodigoCAE; }
                    if (newlinearea.CodigoSRI != _linea.CodigoSRI)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "CodigoSRI", ValorAnterior = _linea.CodigoSRI, ValorNuevo = newlinearea.CodigoSRI, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.CodigoSRI = newlinearea.CodigoSRI; }
                    if (newlinearea.PrefijoGuia != _linea.PrefijoGuia)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "PrefijoGuia", ValorAnterior = _linea.PrefijoGuia, ValorNuevo = newlinearea.PrefijoGuia, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.PrefijoGuia = newlinearea.PrefijoGuia; }
                    if (newlinearea.Email != _linea.Email)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "Email", ValorAnterior = _linea.Email, ValorNuevo = newlinearea.Email, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.Email = newlinearea.Email; }
                    if (newlinearea.Observacion != _linea.Observacion)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Update", Campo = "Observacion", ValorAnterior = _linea.Observacion, ValorNuevo = newlinearea.Observacion, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _linea.Observacion = newlinearea.Observacion; }
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
                    jsonSerializer.Serialize(stringWriter, newlinearea);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newlinearea.LineaAereaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToSGF_LineaAerea(newlinearea);
            }
            model.SaveChanges();
        }
        [OperationContract]
        public void LineaAerea_Eliminar(Guid lineareaID, string nomPC, string ip, string user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_LineaAerea _lineareaID = model.SGF_LineaAerea.First(x => x.LineaAereaID == lineareaID);
            if (_lineareaID != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_LineaAerea", Tipo = "Delete", Campo = "Estado", ValorAnterior = _lineareaID.Estado.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = user, RegistroID = lineareaID.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria);
                _lineareaID.Estado = 0;
                model.SaveChanges();
            }
        }
    }
}
