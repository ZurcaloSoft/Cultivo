using Newtonsoft.Json;
using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {
        #region Proyección
        [OperationContract]
        public SGF_Proyeccion Proyeccion_ObtenerPorID(Guid ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proyeccion.First(x => x.ProyeccionID == ID);
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_Proyeccion_VTA Proyeccion_ObtenerPorID_VTA(Guid ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proyeccion_VTA.First(x => x.ProyeccionID == ID);
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_Proyeccion> Proyeccion_ObtenerPorEmpresaProveedorID(string empresa, string proveedor)
        {
            try
            {
                DataModel model = new DataModel();
                if (proveedor != "")
                    return model.SGF_Proyeccion.Where(x => x.EmpresaID == empresa && x.ProveedorID == proveedor).ToList();
                else
                    return model.SGF_Proyeccion.Where(x => x.EmpresaID == empresa && x.ProveedorID.Length <= 5).ToList();

            }
            catch
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_Proyeccion> Proyeccion_ObtenerPorRecepcionEmpresaID(string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proyeccion.Where(x => x.EmpresaID == empresa && (x.Estado == 4 || x.Estado == 3)).ToList();

            }
            catch
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_Proyeccion> Proyeccion_ObtenerPorFechaEmpresaProveedorID(string empresa, string proveedor, DateTime fecha)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proyeccion.Where(x => x.EmpresaID == empresa && x.ProveedorID == proveedor && x.FechaProyeccion == fecha).ToList();
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_Proyeccion> Proyeccion_ObtenerPorEmpresaFecha(string empresa, DateTime fecha)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_Proyeccion.Where(x => x.EmpresaID == empresa && x.FechaProyeccion == fecha).ToList();
            }
            catch
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_Proyeccion_VTA> Proyeccion_ObtenerPorFecha(string empresa, DateTime fecha, int estado)
        {
            try
            {
                DataModel model = new DataModel();
                if (estado == 0)
                    return model.SGF_Proyeccion_VTA.Where(x => x.EmpresaID == empresa && x.FechaProyeccion == fecha).ToList();
                else
                    return model.SGF_Proyeccion_VTA.Where(x => x.EmpresaID == empresa && x.FechaProyeccion == fecha && x.Estado == estado).ToList();
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public void Proyeccion_Grabar(SGF_Proyeccion newProyeccion, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_Proyeccion _proyeccion = new SGF_Proyeccion();
                if (model.SGF_Proyeccion.Count(x => x.ProyeccionID == newProyeccion.ProyeccionID) > 0)
                {
                    _proyeccion = model.SGF_Proyeccion.First(x => x.ProyeccionID == newProyeccion.ProyeccionID);
                    if (_proyeccion == null)
                    {
                        _proyeccion.Estado = 1;
                        _proyeccion.FechaRegistro = DateTime.Now;
                        // Crear y configurar el JsonSerializer
                        var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                        {
                            Formatting = Formatting.Indented // Para una salida JSON más legible
                        });
                        // Serializar el objeto a JSON string
                        using (var stringWriter = new StringWriter())
                        {
                            jsonSerializer.Serialize(stringWriter, newProyeccion);
                            string jsonString = stringWriter.ToString();
                            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProyeccion.UsuarioRegistro, RegistroID = newProyeccion.ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        }
                        model.AddToSGF_Proyeccion(newProyeccion);
                    }
                    else
                    {
                        if (newProyeccion.TotalTallos != _proyeccion.TotalTallos)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "TotalTallos", ValorAnterior = _proyeccion.TotalTallos.ToString(), ValorNuevo = newProyeccion.TotalTallos.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.UsuarioRegistro, RegistroID = newProyeccion.ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.TotalTallos = newProyeccion.TotalTallos; }
                        if (newProyeccion.TotalTallos != _proyeccion.TotalTallos)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "TotalTallos", ValorAnterior = _proyeccion.TotalTallos.ToString(), ValorNuevo = newProyeccion.TotalTallos.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.UsuarioRegistro, RegistroID = newProyeccion.ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.TotalTallos = newProyeccion.TotalTallos; }
                        if (newProyeccion.TotalTallos != _proyeccion.TotalTallos)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "TotalTallos", ValorAnterior = _proyeccion.TotalTallos.ToString(), ValorNuevo = newProyeccion.TotalTallos.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.UsuarioRegistro, RegistroID = newProyeccion.ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.TotalTallos = newProyeccion.TotalTallos; }

                    }
                }
                else
                {
                    _proyeccion.Estado = 1;
                    _proyeccion.FechaRegistro = DateTime.Now;
                    // Crear y configurar el JsonSerializer
                    var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented // Para una salida JSON más legible
                    });
                    // Serializar el objeto a JSON string
                    using (var stringWriter = new StringWriter())
                    {
                        jsonSerializer.Serialize(stringWriter, newProyeccion);
                        string jsonString = stringWriter.ToString();
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProyeccion.UsuarioRegistro, RegistroID = newProyeccion.ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                    }
                    model.AddToSGF_Proyeccion(newProyeccion);
                }
                model.SaveChanges();
            }
            catch
            {
            }
        }

        [OperationContract]
        public void Proyeccion_ActualizarTotales(Guid ProyeccionID, string usr, string ip, string nompc, int tipo)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_Proyeccion _proyeccion = new SGF_Proyeccion();
                if (model.SGF_Proyeccion.Count(x => x.ProyeccionID == ProyeccionID) > 0)
                {
                    int _sumTallos = 0;
                    int _sumMallas = 0;
                    int _sumsueltos = 0;
                    _proyeccion = model.SGF_Proyeccion.First(x => x.ProyeccionID == ProyeccionID);
                    foreach (SGF_ProyeccionProduccion item in model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == ProyeccionID))
                    {
                        switch (tipo)
                        {
                            case 1: //Proyección
                                _sumTallos = _sumTallos + (int)item.ProyeccionNroTallo;
                                _sumMallas = _sumMallas + (int)item.ProyeccionNroMalla;
                                _sumsueltos = _sumsueltos + (int)item.ProyeccionNroTalloSuelto;
                                break;
                            case 2: //Envio
                                _sumTallos = _sumTallos + (int)item.EnvioNroTallo;
                                _sumMallas = _sumMallas + (int)item.EnvioNroMalla;
                                _sumsueltos = _sumsueltos + (int)item.EnvioNroTalloSuelto;
                                break;
                            case 3: //Recepción
                                _sumTallos = _sumTallos + (int)item.RecepcionNroTallo;
                                _sumMallas = _sumMallas + (int)item.RecepcionNroMalla;
                                _sumsueltos = _sumsueltos + (int)item.RecepcionNroTalloSuelto;
                                break;
                        }
                    }
                    if (_proyeccion.TotalTallos != _sumTallos)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "TotalTallos", ValorAnterior = _proyeccion.TotalTallos.ToString(), ValorNuevo = _sumTallos.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.TotalTallos = _sumTallos; }
                    if (_proyeccion.TotalMalla != _sumMallas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "TotalMalla", ValorAnterior = _proyeccion.TotalMalla.ToString(), ValorNuevo = _sumMallas.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.TotalMalla = _sumMallas; }
                    if (_proyeccion.TotalSobrantes != _sumsueltos)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "TotalSobrantes", ValorAnterior = _proyeccion.TotalSobrantes.ToString(), ValorNuevo = _sumsueltos.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.TotalSobrantes = _sumsueltos; }
                    model.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }


        [OperationContract]
        public void Proyeccion_ActualizarEstado(Guid ProyeccionID, int estado, string usr, string ip, string nompc, int tipo)
        {
            try
            {
                //Estado. 1. Borrador Proyeccion, 2. Aprobado Proyeccion (Visualizar en Envio), 3. Aprobar Envio (Visualizar en Recepcion), 4. Aprobar Recepcion, 5 Procesado
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_Proyeccion _proyeccion = new SGF_Proyeccion();
                SGF_ProyeccionProduccion _proyeccionProduccion = new SGF_ProyeccionProduccion();
                if (model.SGF_Proyeccion.Count(x => x.ProyeccionID == ProyeccionID) > 0)
                {
                    _proyeccion = model.SGF_Proyeccion.First(x => x.ProyeccionID == ProyeccionID);
                    _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_Proyeccion", Tipo = "Update", Campo = "Estado", ValorAnterior = _proyeccion.Estado.ToString(), ValorNuevo = estado.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = _proyeccion.ProyeccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                    switch (tipo)
                    {
                        case 1://Aprobar Proyección - 1. Pendiente de Confirmar, 2. aprobador para Enviar
                            _proyeccion.Estado = estado;
                            foreach (SGF_ProyeccionProduccion item in model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == ProyeccionID))
                            {
                                _proyeccionProduccion = model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == item.ProyeccionProduccionID);
                                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Update", Campo = "ProyeccionEstado", ValorAnterior = _proyeccionProduccion.ProyeccionEstado.ToString(), ValorNuevo = "2", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = _proyeccionProduccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                                _proyeccionProduccion.ProyeccionEstado = 2;
                                ProyeccionProduccion_ActualizarEnvio(item, ip, nompc);
                            }
                            break;
                        case 2: //Aprobar Envio 2. Pendiente confirmar, 3. Enviado
                            _proyeccion.Estado = estado;
                            foreach (SGF_ProyeccionProduccion item in model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == ProyeccionID))
                            {
                                _proyeccionProduccion = model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == item.ProyeccionProduccionID);
                                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Update", Campo = "EnvioEstado", ValorAnterior = _proyeccionProduccion.EnvioEstado.ToString(), ValorNuevo = "2", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = _proyeccionProduccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                                _proyeccionProduccion.EnvioEstado = 2;
                                ProyeccionProduccion_ActualizarRecepcion(item, ip, nompc);
                            }
                            break;
                        case 3: //Aprobar Recepción 3. Pendiente de Recibir, 4. Recibido
                            _proyeccion.Estado = estado;
                            foreach (SGF_ProyeccionProduccion item in model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == ProyeccionID))
                            {
                                _proyeccionProduccion = model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == item.ProyeccionProduccionID);
                                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Update", Campo = "RecepcionEstado", ValorAnterior = _proyeccionProduccion.RecepcionEstado.ToString(), ValorNuevo = "2", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = _proyeccionProduccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                                _proyeccionProduccion.RecepcionEstado = 2;
                            }
                            break;
                        case 4: // Procesado 5 
                            _proyeccion.Estado = estado;
                            break;

                    }
                    model.SaveChanges();
                }
            }
            catch { }
        }
        #endregion

        #region Proyección Producción
        [OperationContract]
        public void ProyeccionProduccion_Grabar(SGF_ProyeccionProduccion newProyeccion, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProyeccionProduccion _proyeccion = new SGF_ProyeccionProduccion();
                if (model.SGF_ProyeccionProduccion.Count(x => x.ProyeccionProduccionID == newProyeccion.ProyeccionProduccionID) > 0)
                {
                    _proyeccion = model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == newProyeccion.ProyeccionProduccionID);
                    if (_proyeccion == null)
                    {
                        _proyeccion.ProyeccionEstado = 1;
                        _proyeccion.ProyeccionFechaRegistro = DateTime.Now;
                        // Crear y configurar el JsonSerializer
                        var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                        {
                            Formatting = Formatting.Indented // Para una salida JSON más legible
                        });
                        // Serializar el objeto a JSON string
                        using (var stringWriter = new StringWriter())
                        {
                            jsonSerializer.Serialize(stringWriter, newProyeccion);
                            string jsonString = stringWriter.ToString();
                            _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        }
                        model.AddToSGF_ProyeccionProduccion(newProyeccion);
                    }
                    else
                    {
                        if (newProyeccion.ProyeccionNroTallo != _proyeccion.ProyeccionNroTallo)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "ProyeccionNroTallo", ValorAnterior = _proyeccion.ProyeccionNroTallo == null ? "" : _proyeccion.ProyeccionNroTallo.ToString(), ValorNuevo = newProyeccion.ProyeccionNroTallo.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.ProyeccionNroTallo = newProyeccion.ProyeccionNroTallo; }
                        if (newProyeccion.ProyeccionNroMalla != _proyeccion.ProyeccionNroMalla)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "ProyeccionNroMalla", ValorAnterior = _proyeccion.ProyeccionNroMalla == null ? "" : _proyeccion.ProyeccionNroMalla.ToString(), ValorNuevo = newProyeccion.ProyeccionNroMalla.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.ProyeccionNroMalla = newProyeccion.ProyeccionNroMalla; }
                        if (newProyeccion.ProyeccionNroTalloSuelto != _proyeccion.ProyeccionNroTalloSuelto)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "ProyeccionNroTalloSuelto", ValorAnterior = _proyeccion.ProyeccionNroTalloSuelto == null ? "" : _proyeccion.ProyeccionNroTalloSuelto.ToString(), ValorNuevo = newProyeccion.ProyeccionNroTalloSuelto.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.ProyeccionNroTalloSuelto = newProyeccion.ProyeccionNroTalloSuelto; }
                        if (newProyeccion.ProyeccionUsuarioRegistro != _proyeccion.ProyeccionUsuarioRegistro)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "ProyeccionUsuarioRegistro", ValorAnterior = _proyeccion.ProyeccionUsuarioRegistro == null ? "" : _proyeccion.ProyeccionUsuarioRegistro.ToString(), ValorNuevo = newProyeccion.ProyeccionUsuarioRegistro.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.ProyeccionUsuarioRegistro = newProyeccion.ProyeccionUsuarioRegistro; }
                        //if (newProyeccion.EnvioObservaciones != _proyeccion.EnvioObservaciones)
                        //{ _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioObservaciones", ValorAnterior = _proyeccion.EnvioObservaciones == null ? "" : _proyeccion.EnvioObservaciones.ToString(), ValorNuevo = newProyeccion.EnvioObservaciones.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioObservaciones = newProyeccion.EnvioObservaciones; }
                        if (newProyeccion.ProyeccionEstado != _proyeccion.ProyeccionEstado)
                        { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "ProyeccionEstado", ValorAnterior = _proyeccion.EnvioEstado == null ? "" : _proyeccion.ProyeccionEstado.ToString(), ValorNuevo = newProyeccion.ProyeccionEstado.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.ProyeccionEstado = newProyeccion.ProyeccionEstado; }
                    }
                }
                else
                {
                    _proyeccion.ProyeccionEstado = 1;
                    _proyeccion.ProyeccionFechaRegistro = DateTime.Now;
                    // Crear y configurar el JsonSerializer
                    var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented // Para una salida JSON más legible
                    });
                    // Serializar el objeto a JSON string
                    using (var stringWriter = new StringWriter())
                    {
                        jsonSerializer.Serialize(stringWriter, newProyeccion);
                        string jsonString = stringWriter.ToString();
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                    }
                    model.AddToSGF_ProyeccionProduccion(newProyeccion);
                }
                model.SaveChanges();
            }
            catch
            {
            }
        }

        [OperationContract]
        public void ProyeccionProduccion_GrabarEnvio(Guid id, string usr, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProyeccionProduccion _proyeccion = new SGF_ProyeccionProduccion();
                if (model.SGF_ProyeccionProduccion.Count(x => x.ProyeccionID == id) > 0)
                {
                    foreach (SGF_ProyeccionProduccion item in model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == id))
                    {
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioNroTallo", ValorAnterior = item.EnvioNroTallo == null ? "" : item.EnvioNroTallo.ToString(), ValorNuevo = item.ProyeccionNroTallo.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioNroMalla", ValorAnterior = item.EnvioNroMalla == null ? "" : item.EnvioNroMalla.ToString(), ValorNuevo = item.ProyeccionNroMalla.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioNroTalloSuelto", ValorAnterior = item.EnvioNroTalloSuelto == null ? "" : item.EnvioNroTalloSuelto.ToString(), ValorNuevo = item.ProyeccionNroTalloSuelto.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioFecha", ValorAnterior = item.EnvioFecha == null ? "" : item.EnvioFecha.ToString(), ValorNuevo = DateTime.Now.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioUsuario", ValorAnterior = item.EnvioUsuario == null ? "" : item.EnvioUsuario.ToString(), ValorNuevo = usr, FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioObservaciones", ValorAnterior = item.EnvioObservaciones == null ? "" : item.EnvioObservaciones.ToString(), ValorNuevo = "", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioEstado", ValorAnterior = item.EnvioEstado == null ? "" : item.EnvioEstado.ToString(), ValorNuevo = "1", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        item.EnvioNroTallo = item.ProyeccionNroTallo;
                        item.EnvioNroMalla = item.ProyeccionNroMalla;
                        item.EnvioNroTalloSuelto = item.ProyeccionNroTalloSuelto;
                        item.EnvioEstado = 1;
                        item.EnvioFecha = DateTime.Now;
                        item.EnvioUsuario = usr;
                        item.EnvioObservaciones = "";
                    }
                }
                model.SaveChanges();
            }
            catch
            {
            }
        }
        [OperationContract]
        public void ProyeccionProduccion_ActualizarEnvio(SGF_ProyeccionProduccion newProyeccion, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProyeccionProduccion _proyeccion = new SGF_ProyeccionProduccion();
                if (model.SGF_ProyeccionProduccion.Count(x => x.ProyeccionProduccionID == newProyeccion.ProyeccionProduccionID) == 0)
                {
                    _proyeccion.EnvioEstado = 0;
                    _proyeccion.ProyeccionFechaRegistro = DateTime.Now;
                    // Crear y configurar el JsonSerializer
                    var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented // Para una salida JSON más legible
                    });
                    // Serializar el objeto a JSON string
                    using (var stringWriter = new StringWriter())
                    {
                        jsonSerializer.Serialize(stringWriter, newProyeccion);
                        string jsonString = stringWriter.ToString();
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                    }
                    model.AddToSGF_ProyeccionProduccion(newProyeccion);
                }
                else
                {
                    _proyeccion = model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == newProyeccion.ProyeccionProduccionID);
                    if (newProyeccion.EnvioNroTallo != _proyeccion.EnvioNroTallo)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioNroTallo", ValorAnterior = _proyeccion.EnvioNroTallo == null ? "" : _proyeccion.EnvioNroTallo.ToString(), ValorNuevo = newProyeccion.EnvioNroTallo.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioNroTallo = newProyeccion.EnvioNroTallo; }
                    if (newProyeccion.EnvioNroMalla != _proyeccion.EnvioNroMalla)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioNroMalla", ValorAnterior = _proyeccion.EnvioNroMalla == null ? "" : _proyeccion.EnvioNroMalla.ToString(), ValorNuevo = newProyeccion.EnvioNroMalla.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioNroMalla = newProyeccion.EnvioNroMalla; }
                    if (newProyeccion.EnvioNroTalloSuelto != _proyeccion.EnvioNroTalloSuelto)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioNroTalloSuelto", ValorAnterior = _proyeccion.EnvioNroTalloSuelto == null ? "" : _proyeccion.EnvioNroTalloSuelto.ToString(), ValorNuevo = newProyeccion.EnvioNroTalloSuelto.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioNroTalloSuelto = newProyeccion.EnvioNroTalloSuelto; }
                    if (newProyeccion.EnvioFecha != _proyeccion.EnvioFecha)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioFecha", ValorAnterior = _proyeccion.EnvioFecha == null ? "" : _proyeccion.EnvioFecha.ToString(), ValorNuevo = newProyeccion.EnvioFecha.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioFecha = newProyeccion.EnvioFecha; }
                    if (newProyeccion.EnvioUsuario != _proyeccion.EnvioUsuario)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioUsuario", ValorAnterior = _proyeccion.EnvioUsuario == null ? "" : _proyeccion.EnvioUsuario.ToString(), ValorNuevo = newProyeccion.EnvioUsuario.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioUsuario = newProyeccion.EnvioUsuario; }
                    if (newProyeccion.EnvioObservaciones != _proyeccion.EnvioObservaciones)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioObservaciones", ValorAnterior = _proyeccion.EnvioObservaciones == null ? "" : _proyeccion.EnvioObservaciones.ToString(), ValorNuevo = newProyeccion.EnvioObservaciones.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioObservaciones = newProyeccion.EnvioObservaciones; }
                    if (newProyeccion.EnvioEstado != _proyeccion.EnvioEstado)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "EnvioEstado", ValorAnterior = _proyeccion.EnvioEstado == null ? "" : _proyeccion.EnvioEstado.ToString(), ValorNuevo = newProyeccion.EnvioEstado.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.EnvioEstado = newProyeccion.EnvioEstado; }
                }
                model.SaveChanges();
            }
            catch
            {
            }
        }

        [OperationContract]
        public void ProyeccionProduccion_ActualizarRecepcion(SGF_ProyeccionProduccion newProyeccion, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProyeccionProduccion _proyeccion = new SGF_ProyeccionProduccion();
                if (model.SGF_ProyeccionProduccion.Count(x => x.ProyeccionProduccionID == newProyeccion.ProyeccionProduccionID) == 0)
                {
                    // Crear y configurar el JsonSerializer
                    var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented // Para una salida JSON más legible
                    });
                    // Serializar el objeto a JSON string
                    using (var stringWriter = new StringWriter())
                    {
                        jsonSerializer.Serialize(stringWriter, newProyeccion);
                        string jsonString = stringWriter.ToString();
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionProduccion", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = newProyeccion.ProyeccionUsuarioRegistro, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                    }
                    model.AddToSGF_ProyeccionProduccion(newProyeccion);
                }
                else
                {
                    _proyeccion = model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == newProyeccion.ProyeccionProduccionID);
                    //_proyeccion.RecepcionEstado = 1;
                    //_proyeccion.ProyeccionFechaRegistro = DateTime.Now;
                    if (newProyeccion.RecepcionNroTallo != _proyeccion.RecepcionNroTallo)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionNroTallo", ValorAnterior = _proyeccion.RecepcionNroTallo == null ? "" : _proyeccion.RecepcionNroTallo.ToString(), ValorNuevo = newProyeccion.RecepcionNroTallo.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionNroTallo = newProyeccion.RecepcionNroTallo; }
                    if (newProyeccion.RecepcionNroMalla != _proyeccion.RecepcionNroMalla)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionNroMalla", ValorAnterior = _proyeccion.RecepcionNroMalla == null ? "" : _proyeccion.RecepcionNroMalla.ToString(), ValorNuevo = newProyeccion.RecepcionNroMalla.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionNroMalla = newProyeccion.RecepcionNroMalla; }
                    if (newProyeccion.RecepcionNroTalloSuelto != _proyeccion.RecepcionNroTalloSuelto)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionNroTalloSuelto", ValorAnterior = _proyeccion.RecepcionNroTalloSuelto == null ? "" : _proyeccion.RecepcionNroTalloSuelto.ToString(), ValorNuevo = newProyeccion.RecepcionNroTalloSuelto.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionNroTalloSuelto = newProyeccion.RecepcionNroTalloSuelto; }
                    if (newProyeccion.RecepcionFecha != _proyeccion.RecepcionFecha)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionFecha", ValorAnterior = _proyeccion.RecepcionFecha == null ? "" : _proyeccion.RecepcionFecha.ToString(), ValorNuevo = newProyeccion.RecepcionFecha.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionFecha = newProyeccion.RecepcionFecha; }
                    if (newProyeccion.RecepcionUsuario != _proyeccion.RecepcionUsuario)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionUsuario", ValorAnterior = _proyeccion.RecepcionUsuario == null ? "" : _proyeccion.RecepcionUsuario.ToString(), ValorNuevo = newProyeccion.RecepcionUsuario.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionUsuario = newProyeccion.RecepcionUsuario; }
                    if (newProyeccion.RecepcionObservaciones != _proyeccion.RecepcionObservaciones)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionObservaciones", ValorAnterior = _proyeccion.RecepcionObservaciones == null ? "" : _proyeccion.RecepcionObservaciones.ToString(), ValorNuevo = newProyeccion.RecepcionObservaciones.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionObservaciones = newProyeccion.RecepcionObservaciones; }
                    if (newProyeccion.RecepcionEstado != _proyeccion.RecepcionEstado)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionEstado", ValorAnterior = _proyeccion.RecepcionEstado == null ? "" : _proyeccion.RecepcionEstado.ToString(), ValorNuevo = newProyeccion.RecepcionEstado.ToString(), FechaRegistro = DateTime.Now, Usuario = newProyeccion.EnvioUsuario, RegistroID = newProyeccion.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _proyeccion.RecepcionEstado = newProyeccion.RecepcionEstado; }
                }
                model.SaveChanges();
            }
            catch
            {
            }
        }
        [OperationContract]
        public void ProyeccionProduccion_GrabarRecepcion(Guid id, string usr, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProyeccionProduccion _proyeccion = new SGF_ProyeccionProduccion();
                if (model.SGF_ProyeccionProduccion.Count(x => x.ProyeccionID == id) > 0)
                {
                    foreach (SGF_ProyeccionProduccion item in model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == id))
                    {
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionNroTallo", ValorAnterior = item.RecepcionNroTallo == null ? "" : item.RecepcionNroTallo.ToString(), ValorNuevo = item.EnvioNroTallo.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionNroMalla", ValorAnterior = item.RecepcionNroMalla == null ? "" : item.RecepcionNroMalla.ToString(), ValorNuevo = item.EnvioNroMalla.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionNroTalloSuelto", ValorAnterior = item.RecepcionNroTalloSuelto == null ? "" : item.RecepcionNroTalloSuelto.ToString(), ValorNuevo = item.EnvioNroTalloSuelto.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionFecha", ValorAnterior = item.RecepcionFecha == null ? "" : item.RecepcionFecha.ToString(), ValorNuevo = DateTime.Now.ToString(), FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionUsuario", ValorAnterior = item.RecepcionUsuario == null ? "" : item.RecepcionUsuario.ToString(), ValorNuevo = usr, FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionObservaciones", ValorAnterior = item.RecepcionObservaciones == null ? "" : item.RecepcionObservaciones.ToString(), ValorNuevo = "", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProyeccionPrdouccion", Tipo = "Update", Campo = "RecepcionEstado", ValorAnterior = item.RecepcionEstado == null ? "" : item.RecepcionEstado.ToString(), ValorNuevo = "1", FechaRegistro = DateTime.Now, Usuario = usr, RegistroID = item.ProyeccionProduccionID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria);
                        item.RecepcionNroTallo = item.EnvioNroTallo;
                        item.RecepcionNroMalla = item.EnvioNroMalla;
                        item.RecepcionNroTalloSuelto = item.EnvioNroTalloSuelto;
                        item.RecepcionEstado = 1;
                        item.RecepcionFecha = DateTime.Now;
                        item.RecepcionUsuario = usr;
                        item.RecepcionObservaciones = "";
                    }
                }
                model.SaveChanges();
            }
            catch
            {
            }
        }
        [OperationContract]
        public SGF_ProyeccionProduccion SGF_ProyeccionProduccion_ObtenerPorID(Guid ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProyeccionProduccion.First(x => x.ProyeccionProduccionID == ID);
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_ProyeccionProduccion_VTA SGF_ProyeccionProduccion_ObtenerPorID_VTA(Guid ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProyeccionProduccion_VTA.First(x => x.ProyeccionProduccionID == ID);
            }
            catch
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_ProyeccionProduccion_VTA> SGF_ProyeccionProduccion_ObtenerPorProveedorID(string proveedor)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProyeccionProduccion_VTA.Where(x => x.ProveedorID == proveedor).ToList();
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_ProyeccionProduccion> SGF_ProyeccionProduccion_ObtenerPorProyeccionID(Guid ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProyeccionProduccion.Where(x => x.ProyeccionID == ID).ToList();
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_ProyeccionProduccion_VTA> SGF_ProyeccionProduccion_ObtenerPorProyeccionID_VTA(Guid ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProyeccionProduccion_VTA.Where(x => x.ProyeccionID == ID).ToList();
            }
            catch
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_ProyeccionProduccion_VTA> SGF_ProyeccionProduccion_ObtenerPorFecha(DateTime fecha)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProyeccionProduccion_VTA.Where(x => x.FechaProyeccion == fecha).ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
