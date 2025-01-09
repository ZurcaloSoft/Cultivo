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
        #region Anterior
        //[OperationContract]
        //public SGF_Vendedor Vendedor_ObtenerPorID(Guid id)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Vendedor.First(x => x.VendedorID == id);
        //}
        //[OperationContract]
        //public List<SGF_Vendedor> Vendedor_ObtenerTodo()
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Vendedor.ToList();
        //}
        //[OperationContract]
        //public SGF_Vendedor Vendedor_ObtenerPorNombre(string nombre)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Vendedor.First(x => x.Nombre == nombre);
        //}
        //[OperationContract]
        //public List<SGF_Vendedor> Vendedor_ObtenerPorEmpresa(Guid vendedorID)
        //{
        //    DataModel model = new DataModel();
        //    return model.SGF_Vendedor.Where(x => x.VendedorID == vendedorID).ToList();
        //}
        //[OperationContract]
        //public void Vendedor_Grabar(SGF_Vendedor newvendedor)
        //{
        //    DataModel model = new DataModel();
        //    if (model.SGF_Vendedor.Count(x => x.VendedorID == newvendedor.VendedorID) > 0)
        //    {
        //        SGF_Vendedor _empresa = model.SGF_Vendedor.First(x => x.VendedorID == newvendedor.VendedorID);
        //        _empresa.Login = newvendedor.Login;
        //        _empresa.Nombre = newvendedor.Nombre;
        //        _empresa.Identificacion = newvendedor.Identificacion;
        //        _empresa.Telefono = newvendedor.Telefono;
        //        _empresa.Email = newvendedor.Email;
        //        _empresa.Direccion = newvendedor.Direccion;
        //        _empresa.Usuario = newvendedor.Usuario;
        //        _empresa.Fecha = newvendedor.Fecha;
        //        _empresa.Estado = newvendedor.Estado;
        //    }
        //    else
        //    {
        //        model.AddToSGF_Vendedor(newvendedor);

        //    }

        //    model.SaveChanges();

        //}
        //[OperationContract]
        //public void Vendedor_Eliminar(Guid vendedorID)
        //{
        //    DataModel model = new DataModel();
        //    SGF_Vendedor _vendedorID = model.SGF_Vendedor.First(x => x.VendedorID == vendedorID);
        //    if (_vendedorID != null)
        //    {
        //        _vendedorID.Estado = 0;
        //        model.SaveChanges();
        //    }
        //}
        #endregion
        #region Nuevo
        [OperationContract]
        public CO_VENDEDOR Vendedor_ObtenerPorID(string id)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_VENDEDOR.First(x => x.CO_VEN_CED == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<CO_VENDEDOR> Vendedor_ObtenerTodo()
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_VENDEDOR.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public CO_VENDEDOR Vendedor_ObtenerPorNombre(string nombre)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_VENDEDOR.First(x => x.CO_VEN_NOM.ToUpper() == nombre.ToUpper());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<CO_VENDEDOR> Vendedor_ObtenerPorNombres(string nombre)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_VENDEDOR.Where(x => x.CO_VEN_NOM.ToUpper().Contains(nombre.ToUpper())).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<CO_VENDEDOR> Vendedor_ObtenerPorEmpresa(string vendedorID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.CO_VENDEDOR.Where(x => x.CO_VEN_CED== vendedorID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public void Vendedor_Grabar(CO_VENDEDOR newvendedor, string nomPC, string ip, string user)
        {
            DataModel model = new DataModel();
            if (model.CO_VENDEDOR.Count(x => x.CO_VEN_CED == newvendedor.CO_VEN_CED) > 0)
            {
                SGF_Auditoria _auditoria = new SGF_Auditoria();
                CO_VENDEDOR _vendedor = model.CO_VENDEDOR.First(x => x.CO_VEN_CED == newvendedor.CO_VEN_CED);
                if(_vendedor!=null)
                {
                    if (_vendedor.CO_EMP_RUC != newvendedor.CO_EMP_RUC)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_EMP_RUC", ValorAnterior = _vendedor.CO_EMP_RUC, ValorNuevo = newvendedor.CO_EMP_RUC, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_EMP_RUC = newvendedor.CO_EMP_RUC; }
                    if (_vendedor.CO_VEN_NOM != newvendedor.CO_VEN_NOM)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_VEN_NOM", ValorAnterior = _vendedor.CO_VEN_NOM, ValorNuevo = newvendedor.CO_VEN_NOM, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_VEN_NOM = newvendedor.CO_VEN_NOM; }
                    if (_vendedor.CO_VEN_DIR != newvendedor.CO_VEN_DIR)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_VEN_DIR", ValorAnterior = _vendedor.CO_VEN_DIR, ValorNuevo = newvendedor.CO_VEN_DIR, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_VEN_DIR = newvendedor.CO_VEN_DIR; }
                    if (_vendedor.CO_VEN_TEL != newvendedor.CO_VEN_TEL)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_VEN_TEL", ValorAnterior = _vendedor.CO_VEN_TEL, ValorNuevo = newvendedor.CO_VEN_TEL, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_VEN_TEL = newvendedor.CO_VEN_TEL; }
                    if (_vendedor.CO_VEN_CEL != newvendedor.CO_VEN_CEL)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_VEN_CEL", ValorAnterior = _vendedor.CO_VEN_CEL, ValorNuevo = newvendedor.CO_VEN_CEL, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_VEN_CEL = newvendedor.CO_VEN_CEL; }
                    if (_vendedor.CO_VEN_CTA != newvendedor.CO_VEN_CTA)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_VEN_CTA", ValorAnterior = _vendedor.CO_VEN_CTA, ValorNuevo = newvendedor.CO_VEN_CTA, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_VEN_CTA = newvendedor.CO_VEN_CTA; }
                    if (_vendedor.CO_VEN_EMAIL != newvendedor.CO_VEN_EMAIL)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Update", Campo = "CO_VEN_EMAIL", ValorAnterior = _vendedor.CO_VEN_EMAIL, ValorNuevo = newvendedor.CO_VEN_EMAIL, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria); _vendedor.CO_VEN_EMAIL = newvendedor.CO_VEN_EMAIL; }
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
                    jsonSerializer.Serialize(stringWriter, newvendedor);
                    string jsonString = stringWriter.ToString();
                    SGF_Auditoria _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Insert", Campo = "Objeto", ValorAnterior = "", ValorNuevo = jsonString, FechaRegistro = DateTime.Now, Usuario = user, RegistroID = newvendedor.CO_VEN_CED.ToString(), IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria);
                }

                model.AddToCO_VENDEDOR(newvendedor);
            }

            model.SaveChanges();

        }
        [OperationContract]
        public void Vendedor_Eliminar(string vendedorID, string nomPC, string ip, string user)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            CO_VENDEDOR _vendedor= model.CO_VENDEDOR.First(x => x.CO_VEN_CED== vendedorID);
            if (_vendedor != null)
            {
                _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "CO_VENDEDOR", Tipo = "Delete", Campo = "CO_ESTADO", ValorAnterior = _vendedor.CO_ESTADO.ToString(), ValorNuevo = "0", FechaRegistro = DateTime.Now, Usuario = user, RegistroID = _vendedor.CO_VEN_CED, IPAddress = ip, namePC = nomPC, ApplicationName = "Módulo Comercial" }; Auditoria_Grabar(_auditoria);
                _vendedor.CO_ESTADO= false;
                model.SaveChanges();
            }
        }
        #endregion
    }
}
