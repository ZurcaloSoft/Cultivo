using Microsoft.Win32;
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
        public void ProveedorVariedad_Grabar(SGF_ProveedorVariedad registro, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_ProveedorVariedad _temporada = new SGF_ProveedorVariedad();
            try
            {
                //if (registro.AlmacenID != 0)
                //    _temporada = model.SGF_ProveedorVariedad.First(x => x.VariedadID == registro.VariedadID && x.AlmacenID == registro.AlmacenID && x.Estado == 1);
                //else
                    _temporada = model.SGF_ProveedorVariedad.First(x => x.VariedadID == registro.VariedadID && x.ProveedorID == registro.ProveedorID && x.Estado == 1);

            }
            catch (Exception ex)
            {
                _temporada = null;
            }

            if (_temporada != null)
            {
                if (_temporada.ProveedorID != registro.ProveedorID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorVariedad", Tipo = "Update", Campo = "ProveedorID", ValorAnterior = _temporada.ProveedorID.ToString(), ValorNuevo = registro.ProveedorID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorVariedadID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.ProveedorID = registro.ProveedorID; }
                //if (_temporada.AlmacenID != registro.AlmacenID)
                //{ _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorVariedad", Tipo = "Update", Campo = "AlmacenID", ValorAnterior = _temporada.AlmacenID == null ? "" : _temporada.AlmacenID.ToString(), ValorNuevo = registro.AlmacenID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorVariedadID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.AlmacenID = registro.AlmacenID; }
                if (_temporada.VariedadID != registro.VariedadID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorVariedad", Tipo = "Update", Campo = "VariedadID", ValorAnterior = _temporada.VariedadID.ToString(), ValorNuevo = registro.VariedadID.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorVariedadID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.VariedadID = registro.VariedadID; }
                if (_temporada.Observaciones != registro.Observaciones)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorVariedad", Tipo = "Update", Campo = "Observaciones", ValorAnterior = _temporada.Observaciones, ValorNuevo = registro.Observaciones, FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorVariedadID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.Observaciones = registro.Observaciones; }
            }
            else
            {
                model.AddToSGF_ProveedorVariedad(registro);
            }
            model.SaveChanges();
        }
        [OperationContract]
        public void ProveedorVariedad_Eliminar(SGF_ProveedorVariedad registro, string ip, string nompc)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                SGF_ProveedorVariedad _temporada = model.SGF_ProveedorVariedad.First(x => x.ProveedorVariedadID == registro.ProveedorVariedadID);
                if (_temporada != null)
                {
                    if (_temporada.Estado != registro.Estado)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_ProveedorVariedad", Tipo = "Update", Campo = "Estado", ValorAnterior = _temporada.Estado.ToString(), ValorNuevo = registro.Estado.ToString(), FechaRegistro = DateTime.Now, Usuario = registro.Usuario, RegistroID = _temporada.ProveedorVariedadID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Recepción" }; Auditoria_Grabar(_auditoria); _temporada.Estado = registro.Estado; }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        [OperationContract]
        public SGF_ProveedorVariedad ProveedorVariedad_ObtenerPorID(Guid Id)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_ProveedorVariedad _temporada = model.SGF_ProveedorVariedad.First(x => x.ProveedorVariedadID == Id);
                return _temporada;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<SGF_ProveedorVariedad> ProveedorVariedad_ObtenerTodo()
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_ProveedorVariedad.OrderBy(x => x.ProveedorID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_ProveedorVariedad> ProveedorVariedad_ObtenerPorFiltro(string proveedor, Guid variedad)
        {
            try
            {
                DataModel model = new DataModel();
                if (proveedor == string.Empty && variedad == Guid.Empty)
                    return model.SGF_ProveedorVariedad.OrderBy(x => x.ProveedorID).ToList();
                else
                {
                    if (proveedor != string.Empty && variedad != Guid.Empty)
                        return model.SGF_ProveedorVariedad.Where(x => x.VariedadID == variedad && x.ProveedorID == proveedor).OrderBy(x => x.ProveedorID).ToList();
                    else
                    {
                        if (proveedor != string.Empty && variedad == Guid.Empty)
                            return model.SGF_ProveedorVariedad.Where(x => x.ProveedorID == proveedor).OrderBy(x => x.ProveedorID).ToList();
                        else
                        {
                            if (proveedor == string.Empty && variedad != Guid.Empty)
                                return model.SGF_ProveedorVariedad.Where(x => x.VariedadID == variedad).OrderBy(x => x.ProveedorID).ToList();
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
        public SGF_ProveedorVariedad ProveedorVariedad_ObtenerPorPVE(string proveedor, Guid variedad, string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                //if (proveedor.Trim().Length < 5)
                //{
                //    if (variedad.Equals(Guid.Empty))
                //        return model.SGF_ProveedorVariedad.FirstOrDefault(x => x.AlmacenID.ToString() == proveedor && x.Estado == 1 && x.EmpresaID == empresa);
                //    else
                //        return model.SGF_ProveedorVariedad.First(x => x.AlmacenID.ToString() == proveedor && x.VariedadID == variedad && x.Estado == 1 && x.EmpresaID == empresa);
                //}
                //else
                //{
                if (variedad.Equals(Guid.Empty))
                    return model.SGF_ProveedorVariedad.FirstOrDefault(x => x.ProveedorID == proveedor && x.Estado == 1 && x.EmpresaID == empresa);
                else
                    return model.SGF_ProveedorVariedad.First(x => x.ProveedorID == proveedor && x.VariedadID == variedad && x.Estado == 1 && x.EmpresaID == empresa);
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SGF_ProveedorVariedad_VTA> ProveedorVariedad_ObtenerPorFiltro_VTA(string proveedor, Guid variedad, string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                if (proveedor == string.Empty && variedad == Guid.Empty)
                    return model.SGF_ProveedorVariedad_VTA.Where(x => x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                else
                {
                    if (proveedor != string.Empty && variedad != Guid.Empty)
                        return model.SGF_ProveedorVariedad_VTA.Where(x => x.VariedadID == variedad && x.ProveedorID == proveedor && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                    else
                    {
                        if (proveedor != string.Empty && variedad == Guid.Empty)
                            return model.SGF_ProveedorVariedad_VTA.Where(x => x.ProveedorID == proveedor && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
                        else
                        {
                            if (proveedor == string.Empty && variedad != Guid.Empty)
                                return model.SGF_ProveedorVariedad_VTA.Where(x => x.VariedadID == variedad && x.EmpresaID == empresa).OrderBy(x => x.ProveedorID).ToList();
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
        public int ProveedorVariedad_ValidarExistencia(string proveedor, Guid variedad, string empresa)
        {
            try
            {
                DataModel model = new DataModel();
                List<SGF_ProveedorVariedad> _datos = new List<SGF_ProveedorVariedad>();
                //if (proveedor.Trim().Length < 5)
                //{
                //    _datos = model.SGF_ProveedorVariedad.Where(x => x.AlmacenID.ToString() == proveedor && x.VariedadID == variedad && x.Estado == 1 && x.EmpresaID == empresa).ToList();
                //}
                //else
                //{
                    _datos = model.SGF_ProveedorVariedad.Where(x => x.ProveedorID == proveedor && x.VariedadID == variedad && x.Estado == 1 && x.EmpresaID == empresa).ToList();
                //}
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
