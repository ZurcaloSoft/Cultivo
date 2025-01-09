using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SGF.DataAccess;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        [OperationContract]
        public List<SGF_Parametro> Parametros_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_Parametro.ToList();
        }
        [OperationContract]
        public SGF_Parametro Parametros_ObtenerPorID(Guid id)
        {
            DataModel model = new DataModel();
            return model.SGF_Parametro.First(x => x.ParametroID == id);
        }

        [OperationContract]
        public SGF_Parametro Parametros_ObtenerPorPkey(string pKey, string empresaID)
        {
            DataModel model = new DataModel();
            return model.SGF_Parametro.First(x => x.EmpresaID == empresaID && x.Pkey== pKey);
        }
        [OperationContract]
        public void Parametros_Grabar(SGF_Parametro _newParametro, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            if (model.SGF_Parametro.Count(x => x.ParametroID == _newParametro.ParametroID) > 0)
            {
                SGF_Parametro _parametro = model.SGF_Parametro.First(x => x.ParametroID == _newParametro.ParametroID);

                if (_newParametro.Pkey != _parametro.Pkey)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "Pkey", ValorAnterior = _parametro.Pkey.ToString(), ValorNuevo = _newParametro.Pkey.ToString(), FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _parametro.Pkey = _newParametro.Pkey; }
                if (_newParametro.TipoParametro != _parametro.TipoParametro)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "TipoParametro", ValorAnterior = _parametro.TipoParametro.ToString(), ValorNuevo = _newParametro.TipoParametro.ToString(), FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _parametro.TipoParametro = _newParametro.TipoParametro; }
                if (_newParametro.Valor != _parametro.Valor)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "Valor", ValorAnterior = _parametro.Valor.ToString(), ValorNuevo = _newParametro.Valor.ToString(), FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _parametro.Valor = _newParametro.Valor; }
                if (_newParametro.Comentarios != _parametro.Comentarios)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "Comentarios", ValorAnterior = _parametro.Comentarios.ToString(), ValorNuevo = _newParametro.Comentarios.ToString(), FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _parametro.Comentarios = _newParametro.Comentarios; }
                if (_newParametro.ModuloID != _parametro.ModuloID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "ModuloID", ValorAnterior = _parametro.ModuloID == null ? "" : _parametro.ModuloID.ToString(), ValorNuevo = _newParametro.ModuloID.ToString(), FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); _parametro.ModuloID = _newParametro.ModuloID; }

            }
            else
            {
                _newParametro.Estado = 1;
                model.AddToSGF_Parametro(_newParametro);
            }
            model.SaveChanges();
        }
        [OperationContract]
        public void Parametros_Eliminar(Guid parametroID, string observacion, string ip, string nompc, string _user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_Parametro _parametro= model.SGF_Parametro.First(x => x.ParametroID == parametroID);
            if (_parametro != null)
            {
                 _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "Estado", ValorAnterior = _parametro.Estado.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria); 
                _parametro.Estado = 0;
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Parametro", Tipo = "Update", Campo = "Estado", ValorAnterior = _parametro.Comentarios.ToString(), ValorNuevo = observacion, FechaRegistro = DateTime.Now, Usuario = _user, RegistroID = _parametro.ParametroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Administración" }; Auditoria_Grabar(_auditoria);
                _parametro.Comentarios = observacion;
                model.SaveChanges();
            }
        }
    }
}
