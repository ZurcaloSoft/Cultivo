using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SGF.DataAccess;
using Newtonsoft.Json;
using System.IO;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public SGF_Persona Persona_ObtenerPorID(string id)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Persona.First(x => x.PersonaID == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_Persona_VTA Persona_ObtenerPorID_VTA(string id)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Persona_VTA.First(x => x.PersonaID == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SGF_Persona Persona_ObtenerPorIdentificacion(string identificacion)
        {
            DataModel model = new DataModel();
            return model.SGF_Persona.First(x => x.Identificacion == identificacion);
        }
        [OperationContract]
        public List<SGF_Persona> Persona_ObtenerTodo()
        {
            DataModel model = new DataModel();
            var _res = model.SGF_Persona.ToList();
            return model.SGF_Persona.ToList();
        }
        [OperationContract]
        public List<SGF_Persona> Persona_ObtenerPorTipoPersona(Guid TipoPersona)
        {
            DataModel model = new DataModel();
            return model.SGF_Persona.Where(x => x.TipoPersonaID == TipoPersona).ToList();
        }
        [OperationContract]
        public void Persona_Grabar(SGF_Persona newPersona, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            if (model.SGF_Persona.Count(x => x.PersonaID == newPersona.PersonaID) > 0)
            {
                SGF_Persona _persona = model.SGF_Persona.First(x => x.PersonaID == newPersona.PersonaID);
                if (_persona.Cargo != newPersona.Cargo)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Cargo", ValorAnterior = _persona.Cargo == null ? "" : _persona.Cargo, ValorNuevo = newPersona.Cargo, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Cargo = newPersona.Cargo; }
                if (_persona.Celular != newPersona.Celular)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Celular", ValorAnterior = _persona.Celular == null ? "" : _persona.Celular, ValorNuevo = newPersona.Celular, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Celular = newPersona.Celular; }
                if (_persona.Ciudad != newPersona.Ciudad)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Ciudad", ValorAnterior = _persona.Ciudad == null ? "" : _persona.Ciudad, ValorNuevo = newPersona.Ciudad, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Ciudad = newPersona.Ciudad; }
                if (_persona.Codigo != newPersona.Codigo)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Codigo", ValorAnterior = _persona.Codigo == null ? "" : _persona.Codigo, ValorNuevo = newPersona.Codigo, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Codigo = newPersona.Codigo; }
                if (_persona.CtaContable != newPersona.CtaContable)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "CtaContable", ValorAnterior = _persona.CtaContable == null ? "" : _persona.CtaContable, ValorNuevo = newPersona.CtaContable, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.CtaContable = newPersona.CtaContable; }
                if (_persona.DiasCredito != newPersona.DiasCredito)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "DiasCredito", ValorAnterior = _persona.DiasCredito == null ? "" : _persona.DiasCredito.ToString(), ValorNuevo = newPersona.DiasCredito.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.DiasCredito = newPersona.DiasCredito; }
                if (_persona.Dirección != newPersona.Dirección)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Dirección", ValorAnterior = _persona.Dirección == null ? "" : _persona.Dirección, ValorNuevo = newPersona.Dirección, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Dirección = newPersona.Dirección; }
                if (_persona.Email != newPersona.Email)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Email", ValorAnterior = _persona.Email == null ? "" : _persona.Email, ValorNuevo = newPersona.Email, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Email = newPersona.Email; }
                if (_persona.EstadoCivil != newPersona.EstadoCivil)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "EstadoCivil", ValorAnterior = _persona.EstadoCivil == null ? "" : _persona.EstadoCivil.ToString(), ValorNuevo = newPersona.EstadoCivil.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.EstadoCivil = newPersona.EstadoCivil; }
                if (_persona.FechaNacimiento != newPersona.FechaNacimiento)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "FechaNacimiento", ValorAnterior = _persona.FechaNacimiento == null ? "" : _persona.FechaNacimiento.ToString(), ValorNuevo = newPersona.FechaNacimiento.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.FechaNacimiento = newPersona.FechaNacimiento; }
                if (_persona.Genero != newPersona.Genero)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Genero", ValorAnterior = _persona.Genero == null ? "" : _persona.Genero.ToString(), ValorNuevo = newPersona.Genero.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Genero = newPersona.Genero; }
                if (_persona.Identificacion != newPersona.Identificacion)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Identificacion", ValorAnterior = _persona.Identificacion == null ? "" : _persona.Identificacion, ValorNuevo = newPersona.Identificacion, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Identificacion = newPersona.Identificacion; }
                if (_persona.Nombre != newPersona.Nombre)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Nombre", ValorAnterior = _persona.Nombre == null ? "" : _persona.Nombre, ValorNuevo = newPersona.Nombre, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Nombre = newPersona.Nombre; }
                if (_persona.Pais != newPersona.Pais)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Pais", ValorAnterior = _persona.Pais == null ? "" : _persona.Pais.ToString(), ValorNuevo = newPersona.Pais.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Pais = newPersona.Pais; }
                if (_persona.RepresentanteLegal != newPersona.RepresentanteLegal)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "RepresentanteLegal", ValorAnterior = _persona.RepresentanteLegal == null ? "" : _persona.RepresentanteLegal, ValorNuevo = newPersona.RepresentanteLegal, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.RepresentanteLegal = newPersona.RepresentanteLegal; }
                if (_persona.Telefono != newPersona.Telefono)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Telefono", ValorAnterior = _persona.Telefono == null ? "" : _persona.Telefono, ValorNuevo = newPersona.Telefono, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.Telefono = newPersona.Telefono; }
                if (_persona.TipoCliente != newPersona.TipoCliente)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "TipoCliente", ValorAnterior = _persona.TipoCliente == null ? "" : _persona.TipoCliente.ToString(), ValorNuevo = newPersona.TipoCliente.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.TipoCliente = newPersona.TipoCliente; }
                if (_persona.TipoIdentificacion != newPersona.TipoIdentificacion)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "TipoIdentificacion", ValorAnterior = _persona.TipoIdentificacion.ToString(), ValorNuevo = newPersona.TipoIdentificacion.ToString(), FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.TipoIdentificacion = newPersona.TipoIdentificacion; }
                if (_persona.Observacion != newPersona.Observacion)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Update", Campo = "Observacion", ValorAnterior = _persona.Observacion == null ? "" : _persona.Observacion, ValorNuevo = newPersona.RepresentanteLegal, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioActualiza, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo de Talento Humano" }; Auditoria_Grabar(_auditoria); _persona.RepresentanteLegal = newPersona.RepresentanteLegal; }
            }
            else
            {
                newPersona.FechaCreacion = DateTime.Now;
                newPersona.FechaActualiza = DateTime.Now;
                newPersona.Estado = 1;
                // Crear y configurar el JsonSerializer
                var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented // Para una salida JSON más legible
                });
                // Serializar el objeto a JSON string
                using (var stringWriter = new StringWriter())
                {
                    jsonSerializer.Serialize(stringWriter, newPersona);
                    string jsonString = stringWriter.ToString();
                    _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Persona", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newPersona.UsuarioCreacion, RegistroID = newPersona.PersonaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Talento Humano" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToSGF_Persona(newPersona);
            }
            model.SaveChanges();
        }

        [OperationContract]
        public List<SGF_Persona_VTA> Persona_BuscarPersonaVTA(Guid TipoPersona, string identificacion, string nombre)
        {
            DataModel model = new DataModel();
            if (identificacion == "" && TipoPersona == Guid.Empty && nombre == "")
                return model.SGF_Persona_VTA.Where(x => x.EstadoPersona == "1" || x.EstadoPersona == "ACTIVO").ToList();
            if (identificacion != "")
                return model.SGF_Persona_VTA.Where(x => x.TipoPersonaID == (TipoPersona == Guid.Empty ? x.TipoPersonaID : TipoPersona) && x.Identificacion == identificacion && (x.EstadoPersona == "1" || x.EstadoPersona == "ACTIVO")).ToList();
            if (nombre != "")
                return model.SGF_Persona_VTA.Where(x => x.TipoPersonaID == (TipoPersona == Guid.Empty ? x.TipoPersonaID : TipoPersona) && x.NombrePersona.ToUpper().Contains(nombre) && (x.EstadoPersona == "1" || x.EstadoPersona == "ACTIVO")).ToList();
            if (TipoPersona != Guid.Empty)
                return model.SGF_Persona_VTA.Where(x => x.TipoPersonaID == (TipoPersona == Guid.Empty ? x.TipoPersonaID : TipoPersona) && (x.EstadoPersona == "1" || x.EstadoPersona == "ACTIVO")).ToList();
            return null;
        }

    }
}
