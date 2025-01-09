using Microsoft.Win32;
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
        public void ProveedorPrecio_Grabar(SGF_ProveedorPrecio registro, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_ProveedorPrecio _temporada = new SGF_ProveedorPrecio();
            try
            {
                _temporada = model.SGF_ProveedorPrecio.First(x => x.ProveedorPrecioID == registro.ProveedorPrecioID);
            }
            catch (Exception ex)
            {
                _temporada = null;
            }
            if (_temporada != null)
            {
                if (_temporada.ProveedorID != registro.ProveedorID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "ProveedorID", ValorAnterior = _temporada.ProveedorID.ToString(), ValorNuevo = registro.ProveedorID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.ProveedorID = registro.ProveedorID; }
                if (_temporada.VariedadID != registro.VariedadID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "VariedadID", ValorAnterior = _temporada.VariedadID.ToString(), ValorNuevo = registro.VariedadID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.VariedadID = registro.VariedadID; }
                if (_temporada.ValorNegociacion != registro.ValorNegociacion)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "ValorNegociacion", ValorAnterior = _temporada.ValorNegociacion.ToString(), ValorNuevo = registro.ValorNegociacion.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.ValorNegociacion = registro.ValorNegociacion; }
                if (_temporada.ValorEstablecido != registro.ValorEstablecido)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "ValorEstablecido", ValorAnterior = _temporada.ValorEstablecido.ToString(), ValorNuevo = registro.ValorEstablecido.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.ValorEstablecido = registro.ValorEstablecido; }
                if (_temporada.CalidadID != registro.CalidadID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "CalidadID", ValorAnterior = _temporada.CalidadID.ToString(), ValorNuevo = registro.CalidadID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.CalidadID = registro.CalidadID; }
                if (_temporada.LongitudID != registro.LongitudID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "LongitudID", ValorAnterior = _temporada.LongitudID.ToString(), ValorNuevo = registro.LongitudID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.LongitudID = registro.LongitudID; }
                if (_temporada.Observaciones != registro.Observaciones)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "Observaciones", ValorAnterior = _temporada.Observaciones, ValorNuevo = registro.Observaciones, FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.Observaciones = registro.Observaciones; }
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
                    jsonSerializer.Serialize(stringWriter, registro);
                    string jsonString = stringWriter.ToString();
                    _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = registro.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria);
                }
                model.AddToSGF_ProveedorPrecio(registro);
            }
            model.SaveChanges();
        }

        [OperationContract]
        public void ProveedorPrecio_Eliminar(SGF_ProveedorPrecio registro, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProveedorPrecio _temporada = model.SGF_ProveedorPrecio.First(x => x.ProveedorPrecioID == registro.ProveedorPrecioID);
                if (_temporada != null)
                {
                    if (_temporada.Estado != registro.Estado)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorPrecio", Tipo = "Update", Campo = "Estado", ValorAnterior = _temporada.Estado.ToString(), ValorNuevo = registro.Estado.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorPrecioID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.Estado = registro.Estado; }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
        [OperationContract]
        public SGF_ProveedorPrecio ProveedorPrecio_ObtenerPorID(Guid Id)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_ProveedorPrecio _temporada = model.SGF_ProveedorPrecio.First(x => x.ProveedorPrecioID == Id);
                return _temporada;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_ProveedorPrecio> ProveedorPrecio_ObtenerTodo()
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProveedorPrecio.OrderBy(x => x.ProveedorID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_ProveedorPrecio> ProveedorPrecio_ObtenerPorFiltro(string proveedor, Guid variedad, string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                if (proveedor == string.Empty && variedad == Guid.Empty)
                    return model.SGF_ProveedorPrecio.Where(x => x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                else
                {
                    if (proveedor != string.Empty && variedad != Guid.Empty)
                        return model.SGF_ProveedorPrecio.Where(x => x.VariedadID == variedad && x.ProveedorID == proveedor && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                    else
                    {
                        if (proveedor != string.Empty && variedad == Guid.Empty)
                            return model.SGF_ProveedorPrecio.Where(x => x.ProveedorID == proveedor && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                        else
                        {
                            if (proveedor == string.Empty && variedad != Guid.Empty)
                                return model.SGF_ProveedorPrecio.Where(x => x.VariedadID == variedad && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                            else
                                return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_ProveedorPrecio_VTA> ProveedorPrecio_ObtenerPorFiltro_VTA(string proveedor, Guid variedad, string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                if (proveedor == string.Empty && variedad == Guid.Empty)
                    return model.SGF_ProveedorPrecio_VTA.Where(x => x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                else
                {
                    if (proveedor != string.Empty && variedad != Guid.Empty)
                        return model.SGF_ProveedorPrecio_VTA.Where(x => x.VariedadID == variedad && x.ProveedorID == proveedor && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                    else
                    {
                        if (proveedor != string.Empty && variedad == Guid.Empty)
                            return model.SGF_ProveedorPrecio_VTA.Where(x => x.ProveedorID == proveedor && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                        else
                        {
                            if (proveedor == string.Empty && variedad != Guid.Empty)
                                return model.SGF_ProveedorPrecio_VTA.Where(x => x.VariedadID == variedad && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                            else
                                return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public int ProveedorPrecio_ValidarExistencia(string proveedor, Guid variedad, string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                var _datos = model.SGF_ProveedorPrecio.Where(x => x.ProveedorID == proveedor && x.VariedadID == variedad && x.Estado == 1 && x.EmpresaID == empresa).ToList();
                if (_datos.Count == 0)
                    return 0;
                else
                    return _datos.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


    }
}
