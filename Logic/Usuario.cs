using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SGF.DataAccess;
using System.Web.Services;
using Newtonsoft.Json;
using System.IO;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        [WebMethod]
        [FaultContract(typeof(Common.InfraestructureException))]
        [FaultContract(typeof(Common.LogicException))]
        [FaultContract(typeof(Common.SecurityException))]
        public SGF_Usuario Usuario_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Usuario.First(x => x.UsuarioID == id);
        }
        [OperationContract]
        public SGF_Usuario Usuario_ObtenerPorUsername(string username)
        {
            DataModel model = new DataModel();
            return model.SGF_Usuario.First(x => x.UserName == username);
        }
        [OperationContract]
        public List<SGF_Usuario> Usuario_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_Usuario.ToList();
        }
        [OperationContract]
        public List<SGF_Usuario> Usuario_ObtenerPorTipoUsuario(Guid TipoUsuario)
        {
            DataModel model = new DataModel();
            return model.SGF_Usuario.Where(x => x.TipoUsuarioID == TipoUsuario).ToList();
        }
        [OperationContract]
        public void Usuario_Grabar(SGF_Usuario newUsuario)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            if (model.SGF_Usuario.Count(x => x.UsuarioID == newUsuario.UsuarioID) > 0)
            {
                SGF_Usuario _usuario= model.SGF_Usuario.First(x => x.UsuarioID== newUsuario.UsuarioID);
                //if (_usuario.Password != newUsuario.Password)
                //{ _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "Password", ValorAnterior = _usuario.Password == null ? "" : _usuario.Password.ToString(), ValorNuevo = newUsuario.Password.ToString(), FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioActualiza, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo de Adminsitración" }; Auditoria_Grabar(_auditoria); _usuario.Password = newUsuario.Password; }
                if (_usuario.TipoUsuarioID != newUsuario.TipoUsuarioID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "TipoUsuarioID", ValorAnterior = _usuario.TipoUsuarioID == null ? "" : _usuario.TipoUsuarioID.ToString(), ValorNuevo = newUsuario.TipoUsuarioID.ToString(), FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioActualiza, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo de Adminsitración" }; Auditoria_Grabar(_auditoria); _usuario.TipoUsuarioID = newUsuario.TipoUsuarioID; }
                if (_usuario.IP != newUsuario.IP)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "IP", ValorAnterior = _usuario.IP == null ? "" : _usuario.IP.ToString(), ValorNuevo = newUsuario.IP.ToString(), FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioActualiza, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo de Adminsitración" }; Auditoria_Grabar(_auditoria); _usuario.IP = newUsuario.IP; }
                if (_usuario.NombrePC!= newUsuario.NombrePC)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "NombrePC", ValorAnterior = _usuario.NombrePC == null ? "" : _usuario.NombrePC.ToString(), ValorNuevo = newUsuario.NombrePC.ToString(), FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioActualiza, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo de Adminsitración" }; Auditoria_Grabar(_auditoria); _usuario.NombrePC = newUsuario.NombrePC; }
                if (_usuario.MAC != newUsuario.MAC)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "MAC", ValorAnterior = _usuario.MAC == null ? "" : _usuario.MAC.ToString(), ValorNuevo = newUsuario.MAC.ToString(), FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioActualiza, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo de Adminsitración" }; Auditoria_Grabar(_auditoria); _usuario.MAC = newUsuario.MAC; }
                if (_usuario.Observacion != newUsuario.Observacion)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "Observacion", ValorAnterior = _usuario.Observacion == null ? "" : _usuario.Observacion.ToString(), ValorNuevo = newUsuario.Observacion.ToString(), FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioActualiza, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo de Adminsitración" }; Auditoria_Grabar(_auditoria); _usuario.Observacion = newUsuario.Observacion; }
            }
            else
            {
                var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented // Para una salida JSON más legible
                });
                // Serializar el objeto a JSON string
                using (var stringWriter = new StringWriter())
                {
                    jsonSerializer.Serialize(stringWriter, newUsuario);
                    string jsonString = stringWriter.ToString();
                    _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newUsuario.UsuarioCreacion, RegistroID = newUsuario.UsuarioID.ToString(), IPAddress = newUsuario.IP, namePC = newUsuario.NombrePC, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToSGF_Usuario(newUsuario);
            }
            model.SaveChanges();
        }

        [OperationContract]
        public void Usuario_ActualizarPassword(string userName, string clvAnt, string clvAct, string ip, string mac, string nompc, string logueado)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_Usuario _usuario = model.SGF_Usuario.First(x => x.UserName == userName && x.Estado == 1);
            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "Password", ValorAnterior = clvAnt, ValorNuevo = clvAct, FechaRegistro = DateTime.Now, Usuario = logueado, RegistroID = _usuario.UsuarioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Seguridad" }; Auditoria_Grabar(_auditoria);
            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "NombrePC", ValorAnterior = _usuario.NombrePC, ValorNuevo = nompc, FechaRegistro = DateTime.Now, Usuario = logueado, RegistroID = _usuario.UsuarioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Seguridad" }; Auditoria_Grabar(_auditoria);
            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "IP", ValorAnterior = _usuario.IP, ValorNuevo = ip, FechaRegistro = DateTime.Now, Usuario = logueado, RegistroID = _usuario.UsuarioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Seguridad" }; Auditoria_Grabar(_auditoria);
            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "MAC", ValorAnterior = _usuario.MAC, ValorNuevo = mac, FechaRegistro = DateTime.Now, Usuario = logueado, RegistroID = _usuario.UsuarioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Seguridad" }; Auditoria_Grabar(_auditoria);
            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "UsuarioActualiza", ValorAnterior = _usuario.UsuarioActualiza, ValorNuevo = logueado, FechaRegistro = DateTime.Now, Usuario = logueado, RegistroID = _usuario.UsuarioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Seguridad" }; Auditoria_Grabar(_auditoria);
            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Usuario", Tipo = "Update", Campo = "FechaActualiza", ValorAnterior = _usuario.FechaActualiza == null ? "" : _usuario.FechaActualiza.ToString(), ValorNuevo = DateTime.Now.ToString(), FechaRegistro = DateTime.Now, Usuario = logueado, RegistroID = _usuario.UsuarioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Seguridad" }; Auditoria_Grabar(_auditoria);
            _usuario.Password = clvAct;
            _usuario.NombrePC = nompc;
            _usuario.IP = ip;
            _usuario.MAC = mac;
            _usuario.UsuarioActualiza = logueado;
            _usuario.FechaActualiza = DateTime.Now;
            model.SaveChanges();
        }

        [OperationContract]
        public List<SGF_Usuario_VTA> Usuario_BuscarUsuarioVTA(Guid TipoUsuario, string identificacion, string nombre)
        {
            DataModel model = new DataModel();
            if (identificacion == "" && TipoUsuario == Guid.Empty && nombre == "")
                return model.SGF_Usuario_VTA.Where(x => x.EstadoUsuario == 1).ToList();
            if (identificacion != "")
                return model.SGF_Usuario_VTA.Where(x => x.Identificacion == identificacion && x.EstadoUsuario == 1).ToList();
            if (TipoUsuario != Guid.Empty)
                return model.SGF_Usuario_VTA.Where(x => x.TipoPersonaID == TipoUsuario && x.EstadoUsuario == 1).ToList();
            if (nombre != "")
                return model.SGF_Usuario_VTA.Where(x => x.Nombre.ToUpper().Contains(nombre) && x.EstadoUsuario == 1).ToList();
            return null;
        }

        [OperationContract]
        public SGF_Usuario_VTA Usuario_BuscarUsuarioPorID_VTA(Guid usuarioID)
        {
            DataModel model = new DataModel();
            return model.SGF_Usuario_VTA.First(x => x.UsuarioID == usuarioID);
        }
    }
}
