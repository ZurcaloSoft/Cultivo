using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public void Temporada_Grabar(SGF_Temporada registro)
        {
            DataModel model = new DataModel();
            model.AddToSGF_Temporada(registro);
            model.SaveChanges();
        }

        [OperationContract]
        public void Temporada_Actualizar(SGF_Temporada registro, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_Temporada _temporada = model.SGF_Temporada.First(x => x.TemporadaID == registro.TemporadaID);
                if (_temporada != null)
                {
                    if (_temporada.ProveedorID != registro.ProveedorID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "ProveedorID", ValorAnterior = _temporada.ProveedorID.ToString(), ValorNuevo = registro.ProveedorID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.ProveedorID = registro.ProveedorID; }
                    if (_temporada.FechaInicio != registro.FechaInicio)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "FechaInicio", ValorAnterior = _temporada.FechaInicio == null ? "" : _temporada.FechaInicio.ToString(), ValorNuevo = registro.FechaInicio.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.FechaInicio = registro.FechaInicio; }
                    if (_temporada.FechaFin != registro.FechaFin)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "FechaFin", ValorAnterior = _temporada.FechaFin == null ? "" : _temporada.FechaFin.ToString(), ValorNuevo = registro.FechaFin.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.FechaFin = registro.FechaFin; }
                    if (_temporada.FechaActualiza != registro.FechaActualiza)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "FechaActualiza", ValorAnterior = _temporada.FechaActualiza == null ? "" : _temporada.FechaActualiza.ToString(), ValorNuevo = registro.FechaActualiza.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.FechaActualiza = registro.FechaActualiza; }
                    if (_temporada.UsuarioActualiza != registro.UsuarioActualiza)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "UsuarioActualiza", ValorAnterior = _temporada.UsuarioActualiza, ValorNuevo = registro.UsuarioActualiza, FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.UsuarioActualiza = registro.UsuarioActualiza; }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        [OperationContract]
        public void Temporada_Eliminar(SGF_Temporada registro, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_Temporada _temporada = model.SGF_Temporada.First(x => x.TemporadaID == registro.TemporadaID);
                if (_temporada != null)
                {
                    if (_temporada.Estado != registro.Estado)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "Estado", ValorAnterior = _temporada.Estado.ToString(), ValorNuevo = registro.Estado.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.Estado = registro.Estado; }
                    if (_temporada.FechaActualiza != registro.FechaActualiza)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "FechaActualiza", ValorAnterior = _temporada.FechaActualiza == null ? "" : _temporada.FechaActualiza.ToString(), ValorNuevo = registro.FechaActualiza.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.FechaActualiza = registro.FechaActualiza; }
                    if (_temporada.UsuarioActualiza != registro.UsuarioActualiza)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Temporada", Tipo = "Update", Campo = "UsuarioActualiza", ValorAnterior = _temporada.UsuarioActualiza, ValorNuevo = registro.UsuarioActualiza, FechaRegistro = DateTime.Now, Usuario = registro.UsuarioRegistro, RegistroID = _temporada.TemporadaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _temporada.UsuarioActualiza = registro.UsuarioActualiza; }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
