using SGF.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SGF.BussinessLogic
{
    public partial class Logic
    {

        #region Mapa de Cultivo
        [OperationContract]
        public SGF_CampoCultivo MapaCultivo_ObtenerPorID(Guid id)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_CampoCultivo _campoCultivo = new SGF_CampoCultivo();
                var campos = model.SGF_CampoCultivo
                     .Include("SGF_CultivoArea")  // Cargar libros y sus capítulos relacionados
                     .Where(x => x.CampoCultivoID == id).ToList();
                _campoCultivo = campos.First(x => x.CampoCultivoID == id);
                return campos.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_CampoCultivo MapaCultivo_ObtenerPorIDTotal(Guid id)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_CampoCultivo _campoCultivo = new SGF_CampoCultivo();
                var campos = model.SGF_CampoCultivo
                     .Include("SGF_CultivoArea.SGF_CultivoBloque.SGF_CultivoLado.SGF_CultivoNave.SGF_CultivoCama.SGF_CultivoCuadro")  // Cargar libros y sus capítulos relacionados
                     .Where(x => x.CampoCultivoID == id).ToList();
                _campoCultivo = campos.First(x => x.CampoCultivoID == id);
                return campos.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public SGF_CampoCultivo MapaCultivo_ObtenerPorIDs(Guid _campoId, Guid _areaId, Guid _bloqueId)
        {
            DataModel model = new DataModel();
            SGF_CampoCultivo _campoCultivo = new SGF_CampoCultivo();
            List<SGF_CultivoArea> _cArea = new List<SGF_CultivoArea>();
            List<SGF_CultivoBloque> _cBloque = new List<SGF_CultivoBloque>();

            // Consulta inicial con Include para cargar las relaciones
            var campoCultivo = model.SGF_CampoCultivo
                .Include("SGF_CultivoArea.SGF_CultivoBloque.SGF_CultivoLado.SGF_CultivoNave.SGF_CultivoCama.SGF_CultivoCuadro")
                .Where(cc =>
                    cc.CampoCultivoID == _campoId &&
                    (cc.SGF_CultivoArea.Any(ca => ca.CultivoAreaID == _areaId)) &&
                    (cc.SGF_CultivoArea.Any(ca => ca.SGF_CultivoBloque.Any(cb => cb.CultivoBloqueID == _bloqueId)))
                )
                .ToList();

            _campoCultivo = campoCultivo.FirstOrDefault();
            return _campoCultivo;
        }

        [OperationContract]
        public List<SGF_CampoCultivo> MapaCultivo_ObtenerTodo()
        {
            DataModel model = new DataModel();
            return model.SGF_CampoCultivo.Where(x => x.Estado == 1).ToList();
        }

        [OperationContract]
        public List<SGF_CampoCultivo> MapaCultivo_ObtenerPorProveedorID(string id)
        {
            DataModel model = new DataModel();
            return model.SGF_CampoCultivo.Where(x => x.Estado == 1 && x.ProveedorID == id).ToList();
        }

        [OperationContract]
        public void MapaCultivo_Grabar(SGF_CampoCultivo newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();

            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CampoCultivo _campoCultivo = new SGF_CampoCultivo();
            if (model.SGF_CampoCultivo.Count(X => X.CampoCultivoID == newCampo.CampoCultivoID) == 0)
            {
                //using (var transaction = model.Connection.BeginTransaction())
                //{
                model.AddToSGF_CampoCultivo(newCampo);
                model.SaveChanges();
                //    transaction.Commit();
                //}
            }
            else
            {
                //  _campoCultivo = model.SGF_CampoCultivo.First(X => X.CampoCultivoID == newCampo.CampoCultivoID);
                var campos = model.SGF_CampoCultivo
     .Include("SGF_CultivoArea.SGF_CultivoBloque.SGF_CultivoLado.SGF_CultivoNave.SGF_CultivoCama.SGF_CultivoCuadro")  // Cargar libros y sus capítulos relacionados
     .Where(x => x.CampoCultivoID == newCampo.CampoCultivoID).ToList();


                if (campos != null)
                {
                    _campoCultivo = campos.First(x => x.CampoCultivoID == newCampo.CampoCultivoID);
                    foreach (SGF_CultivoArea _area in newCampo.SGF_CultivoArea.Where(x => x.CampoCultivoID == newCampo.CampoCultivoID))
                    {
                        if (_campoCultivo.SGF_CultivoArea.Count(X => X.CampoCultivoID == newCampo.CampoCultivoID && X.CultivoAreaID == _area.CultivoAreaID) == 0)
                        {
                            //using (var transaction = model.Connection.BeginTransaction())
                            //{
                            _campoCultivo.SGF_CultivoArea.Add(_area);
                            model.SaveChanges();
                            //    transaction.Commit();
                            //}

                            break;
                        }
                        else
                        {
                            foreach (SGF_CultivoBloque _bloque in _area.SGF_CultivoBloque)
                            {
                                if (_campoCultivo.SGF_CultivoArea.First(X => X.CampoCultivoID == newCampo.CampoCultivoID && X.CultivoAreaID == _area.CultivoAreaID).SGF_CultivoBloque.Count(X => X.CultivoBloqueID == _bloque.CultivoBloqueID && X.CultivoAreaID == _bloque.CultivoAreaID) == 0)
                                {

                                    //using (var transaction = model.Connection.BeginTransaction())
                                    //{
                                    _campoCultivo.SGF_CultivoArea.First(X => X.CampoCultivoID == newCampo.CampoCultivoID && X.CultivoAreaID == _area.CultivoAreaID).SGF_CultivoBloque.Add(_bloque);
                                    model.SaveChanges();
                                    //    transaction.Commit();
                                    //}

                                    break;
                                }
                            }
                        }
                    }
                    if (_campoCultivo.EmpresaID != newCampo.EmpresaID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CampoCultivo", Tipo = "Update", Campo = "EmpresaID", ValorAnterior = _campoCultivo.EmpresaID.ToString(), ValorNuevo = newCampo.EmpresaID.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _campoCultivo.CampoCultivoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _campoCultivo.EmpresaID = newCampo.EmpresaID; }
                    if (_campoCultivo.ProveedorID != newCampo.ProveedorID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CampoCultivo", Tipo = "Update", Campo = "ProveedorID", ValorAnterior = _campoCultivo.ProveedorID.ToString(), ValorNuevo = newCampo.ProveedorID.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _campoCultivo.CampoCultivoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _campoCultivo.ProveedorID = newCampo.ProveedorID; }
                    if (_campoCultivo.Aream2 != newCampo.Aream2)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CampoCultivo", Tipo = "Update", Campo = "Aream2", ValorAnterior = _campoCultivo.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _campoCultivo.CampoCultivoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _campoCultivo.Aream2 = newCampo.Aream2; }
                    if (_campoCultivo.CantidadPlantas != newCampo.CantidadPlantas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CampoCultivo", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _campoCultivo.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _campoCultivo.CampoCultivoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _campoCultivo.CantidadPlantas = newCampo.CantidadPlantas; }
                    if (_campoCultivo.Descripcion != newCampo.Descripcion)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CampoCultivo", Tipo = "Update", Campo = "Descripcion", ValorAnterior = _campoCultivo.Descripcion, ValorNuevo = newCampo.Descripcion, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _campoCultivo.CampoCultivoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _campoCultivo.Descripcion = newCampo.Descripcion; }
                    if (_campoCultivo.Direccion != newCampo.Direccion)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CampoCultivo", Tipo = "Update", Campo = "Direccion", ValorAnterior = _campoCultivo.Direccion, ValorNuevo = newCampo.Descripcion, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _campoCultivo.CampoCultivoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _campoCultivo.Direccion = newCampo.Direccion; }

                }
            }
            model.SaveChanges();
        }

        [OperationContract]
        public void MapaCultivo_GrabarSecciones(int _Tipo, Guid _campoID, Guid _seccionId, Guid _Id, string _nombre, int _orden, decimal _aream2, int _nroPlantas, string _usuario, DateTime _fecha, int _estado)
        {
            DataModel model = new DataModel();
            SGF_CampoCultivo _campoCultivo = new SGF_CampoCultivo();
            var _res = model.SP_MapaCultivo_InsertarSecciones(_Tipo, _campoID, _seccionId, _Id, _nombre, _orden, _aream2, _nroPlantas, _usuario, _fecha, _estado);
        }

        [OperationContract]
        public List<SGF_CultivoCama_VTA> MapaCultivo_RecuperarTodo_VTA(int type, Guid itemID)
        {
            DataModel model = new DataModel();
            List<SGF_CultivoCama_VTA> _resultado = new List<SGF_CultivoCama_VTA>();
            try
            {
                switch (type)
                {
                    case 1://Area
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoAreaID == itemID && x.Estado == 1).OrderBy(x => x.CultivoAreaOrden).OrderBy(x => x.CultivoBloqueOrden).OrderBy(x => x.CultivoLadoOrden).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 2://Bloque
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoBloqueID == itemID && x.Estado == 1).OrderBy(x => x.CultivoBloqueOrden).OrderBy(x => x.CultivoLadoOrden).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 3://Lado
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoLadoID == itemID && x.Estado == 1).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 4://Nave
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoNaveID == itemID && x.Estado == 1).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 5://Cama
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoCamaID == itemID && x.Estado == 1).ToList();
                        break;
                        //case 6://Cuadra
                        //    _resultado = model.SGF_CultivoCama_VTA.Where(x => x.cul.CultivoCuadroID == itemID).ToList();
                        //    break;
                }
                return _resultado;
            }
            catch (Exception ex)
            {
                return _resultado;
            }
        }

        [OperationContract]
        public List<SGF_CultivoCama_VTA> MapaCultivo_RecuperarTodoPasivo_VTA(int type, Guid itemID)
        {
            DataModel model = new DataModel();
            List<SGF_CultivoCama_VTA> _resultado = new List<SGF_CultivoCama_VTA>();
            try
            {
                switch (type)
                {
                    case 1://Area
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoAreaID == itemID && x.Estado == 0).OrderBy(x => x.CultivoAreaOrden).OrderBy(x => x.CultivoBloqueOrden).OrderBy(x => x.CultivoLadoOrden).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 2://Bloque
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoBloqueID == itemID && x.Estado == 0).OrderBy(x => x.CultivoBloqueOrden).OrderBy(x => x.CultivoLadoOrden).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 3://Lado
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoLadoID == itemID && x.Estado == 0).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 4://Nave
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoNaveID == itemID && x.Estado == 0).OrderBy(x => x.CultivoNaveOrden).OrderBy(x => x.Orden).ToList();
                        break;
                    case 5://Cama
                        _resultado = model.SGF_CultivoCama_VTA.Where(x => x.CultivoCamaID == itemID && x.Estado == 0).ToList();
                        break;
                    //case 6://Cuadra
                    //    _resultado = model.SGF_CultivoCama_VTA.Where(x => x.cult.CultivoCuadroID == itemID).ToList();
                    //    break;
                }
                return _resultado;
            }
            catch (Exception ex)
            {
                return _resultado;
            }
        }
        #endregion

        #region Cultivo Area
        [OperationContract]
        public void CultivoArea_Actualizar(SGF_CultivoArea newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoArea _cultivoArea = new SGF_CultivoArea();
            if (model.SGF_CultivoArea.Count(X => X.CultivoAreaID == newCampo.CultivoAreaID) > 0)
            {
                _cultivoArea = model.SGF_CultivoArea.First(X => X.CultivoAreaID == newCampo.CultivoAreaID);
                if (_cultivoArea != null)
                {
                    if (_cultivoArea.Aream2 != newCampo.Aream2)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "Aream2", ValorAnterior = _cultivoArea.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.Aream2 = newCampo.Aream2; }
                    if (_cultivoArea.CantidadPlantas != newCampo.CantidadPlantas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _cultivoArea.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.CantidadPlantas = newCampo.CantidadPlantas; }
                    if (_cultivoArea.AreaID != newCampo.AreaID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "AreaID", ValorAnterior = _cultivoArea.AreaID.ToString(), ValorNuevo = newCampo.AreaID.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.AreaID = newCampo.AreaID; }
                    if (_cultivoArea.Nombre != newCampo.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoArea.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.Nombre = newCampo.Nombre; }
                    model.SaveChanges();
                }
            }
            else
            {
                model.AddToSGF_CultivoArea(newCampo);
                model.SaveChanges();
            }
        }
        [OperationContract]
        public void CultivoArea_ActualizarAreaID(SGF_CultivoArea newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoArea _cultivoArea = new SGF_CultivoArea();
            if (model.SGF_CultivoArea.Count(X => X.CultivoAreaID == newCampo.CultivoAreaID && X.CampoCultivoID == newCampo.CampoCultivoID) > 0)
            {
                _cultivoArea = model.SGF_CultivoArea.First(X => X.CultivoAreaID == newCampo.CultivoAreaID);
                if (_cultivoArea != null)
                {
                    if (_cultivoArea.AreaID != newCampo.AreaID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "AreaID", ValorAnterior = _cultivoArea.AreaID.ToString(), ValorNuevo = newCampo.AreaID.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.AreaID = newCampo.AreaID; }
                    if (_cultivoArea.Nombre != newCampo.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoArea.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.Nombre = newCampo.Nombre; }
                    if (_cultivoArea.Orden != newCampo.Orden)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoArea", Tipo = "Update", Campo = "Orden", ValorAnterior = _cultivoArea.Orden.ToString(), ValorNuevo = newCampo.Orden.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoArea.CultivoAreaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoArea.Orden = newCampo.Orden; }
                    model.SaveChanges();
                }
            }
        }

        [OperationContract]
        public int CultivoArea_ContarAreaPorCampoID(Guid id)
        {
            try
            {
                DataModel model = new DataModel();
                var campos = model.SGF_CultivoArea_VTA.Where(x => x.CampoCultivoID == id).ToList();
                return campos.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion
        #region Cultivo Bloque
        [OperationContract]
        public void CultivoBloque_Actualizar(SGF_CultivoBloque newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoBloque _cultivoBloque = new SGF_CultivoBloque();
            if (model.SGF_CultivoBloque.Count(X => X.CultivoBloqueID == newCampo.CultivoBloqueID && X.CultivoAreaID == newCampo.CultivoAreaID) > 0)
            {
                _cultivoBloque = model.SGF_CultivoBloque.First(X => X.CultivoBloqueID == newCampo.CultivoBloqueID && X.CultivoAreaID == newCampo.CultivoAreaID);
                if (_cultivoBloque != null)
                {
                    if (_cultivoBloque.Aream2 != newCampo.Aream2)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "Aream2", ValorAnterior = _cultivoBloque.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.Aream2 = newCampo.Aream2; }
                    if (_cultivoBloque.CantidadPlantas != newCampo.CantidadPlantas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _cultivoBloque.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.CantidadPlantas = newCampo.CantidadPlantas; }
                    if (_cultivoBloque.BloqueID != newCampo.BloqueID)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "BloqueID", ValorAnterior = _cultivoBloque.BloqueID.ToString(), ValorNuevo = newCampo.BloqueID.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.BloqueID = newCampo.BloqueID; }
                    if (_cultivoBloque.Nombre != newCampo.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoBloque.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.Nombre = newCampo.Nombre; }
                    model.SaveChanges();
                }
            }
        }

        [OperationContract]
        public void CultivoBloque_ActualizarBloqueID(SGF_CultivoBloque newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoBloque _cultivoBloque = new SGF_CultivoBloque();
            _cultivoBloque = model.SGF_CultivoBloque.First(X => X.CultivoBloqueID == newCampo.CultivoBloqueID && X.CultivoAreaID == newCampo.CultivoAreaID);
            if (_cultivoBloque != null)
            {
                if (_cultivoBloque.BloqueID != newCampo.BloqueID)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "BloqueID", ValorAnterior = _cultivoBloque.BloqueID.ToString(), ValorNuevo = newCampo.BloqueID.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.BloqueID = newCampo.BloqueID; }
                if (_cultivoBloque.Nombre != newCampo.Nombre)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoBloque.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.Nombre = newCampo.Nombre; }
                if (_cultivoBloque.Orden != newCampo.Orden)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoBloque", Tipo = "Update", Campo = "Orden", ValorAnterior = _cultivoBloque.Orden.ToString(), ValorNuevo = newCampo.Orden.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoBloque.CultivoBloqueID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoBloque.Orden = newCampo.Orden; }
                model.SaveChanges();
            }
        }

        [OperationContract]
        public SGF_CultivoBloque CultivoBloque_ObtenerTodoPorID(Guid id)
        {
            try
            {
                DataModel model = new DataModel();
                SGF_CultivoBloque _campoCultivo = new SGF_CultivoBloque();
                var campos = model.SGF_CultivoBloque
                     .Include("SGF_CultivoLado.SGF_CultivoNave.SGF_CultivoCama.SGF_CultivoCuadro")  // Cargar libros y sus capítulos relacionados
                     .Where(x => x.CultivoBloqueID == id).ToList();
                _campoCultivo = campos.First(x => x.CultivoBloqueID == id);
                return campos.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public int CultivoBloque_ContarBloquePorCampoID(Guid id)
        {
            try
            {
                DataModel model = new DataModel();
                var campos = model.SGF_CultivoBloque_VTA.Where(x => x.CampoCultivoID == id).ToList();
                return campos.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion
        #region Cultivo Lado
        [OperationContract]
        public void CultivoLado_Actualizar(SGF_CultivoLado newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoLado _cultivoLado = new SGF_CultivoLado();
            if (model.SGF_CultivoLado.Count(X => X.CultivoLadoID == newCampo.CultivoLadoID) > 0)
            {
                _cultivoLado = model.SGF_CultivoLado.First(X => X.CultivoBloqueID == newCampo.CultivoBloqueID);
                if (_cultivoLado != null)
                {
                    if (_cultivoLado.Aream2 != newCampo.Aream2)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoLado", Tipo = "Update", Campo = "Aream2", ValorAnterior = _cultivoLado.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoLado.CultivoLadoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoLado.Aream2 = newCampo.Aream2; }
                    if (_cultivoLado.CantidadPlantas != newCampo.CantidadPlantas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoLado", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _cultivoLado.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoLado.CultivoLadoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoLado.CantidadPlantas = newCampo.CantidadPlantas; }
                    if (_cultivoLado.Nombre != newCampo.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoLado", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoLado.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoLado.CultivoLadoID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoLado.Nombre = newCampo.Nombre; }
                    model.SaveChanges();
                }
            }
        }
        #endregion
        #region Cultivo Nave
        [OperationContract]
        public void CultivoNave_Actualizar(SGF_CultivoNave newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoNave _cultivoNave = new SGF_CultivoNave();
            if (model.SGF_CultivoLado.Count(X => X.CultivoLadoID == newCampo.CultivoLadoID) > 0)
            {
                _cultivoNave = model.SGF_CultivoNave.First(X => X.CultivoNaveID == newCampo.CultivoNaveID);
                if (_cultivoNave != null)
                {
                    if (_cultivoNave.Aream2 != newCampo.Aream2)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoNave", Tipo = "Update", Campo = "Aream2", ValorAnterior = _cultivoNave.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoNave.CultivoNaveID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoNave.Aream2 = newCampo.Aream2; }
                    if (_cultivoNave.CantidadPlantas != newCampo.CantidadPlantas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoNave", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _cultivoNave.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoNave.CultivoNaveID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoNave.CantidadPlantas = newCampo.CantidadPlantas; }
                    if (_cultivoNave.Nombre != newCampo.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoNave", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoNave.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoNave.CultivoNaveID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoNave.Nombre = newCampo.Nombre; }
                    model.SaveChanges();
                }
            }
        }
        #endregion
        #region Cultivo Cama
        [OperationContract]
        public void CultivoCama_Actualizar(SGF_CultivoCama newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == newCampo.CultivoCamaID) > 0)
            {
                _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == newCampo.CultivoCamaID);
                if (_cultivoCama != null)
                {
                    if (_cultivoCama.Aream2 != newCampo.Aream2)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Aream2", ValorAnterior = _cultivoCama.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Aream2 = newCampo.Aream2; }
                    if (_cultivoCama.CantidadPlantas != newCampo.CantidadPlantas)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _cultivoCama.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.CantidadPlantas = newCampo.CantidadPlantas; }
                    if (_cultivoCama.Nombre != newCampo.Nombre)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoCama.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Nombre = newCampo.Nombre; }
                    model.SaveChanges();
                }
            }
        }

        [OperationContract]
        public void CultivoCama_SembrarVariedad(SGF_CultivoCama newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            //if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == newCampo.CultivoCamaID) > 0)
            //{
            _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == newCampo.CultivoCamaID);
            if (_cultivoCama != null)
            {
                if (_cultivoCama.VariedadSembrada != newCampo.VariedadSembrada)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "VariedadSembrada", ValorAnterior = _cultivoCama.VariedadSembrada.ToString(), ValorNuevo = newCampo.VariedadSembrada.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.VariedadSembrada = newCampo.VariedadSembrada; }
                if (_cultivoCama.FechaSiembra != newCampo.FechaSiembra)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "FechaSiembra", ValorAnterior = _cultivoCama.FechaSiembra == null ? "" : _cultivoCama.FechaSiembra.ToString(), ValorNuevo = newCampo.FechaSiembra.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.FechaSiembra = newCampo.FechaSiembra; }
                if (_cultivoCama.Color != newCampo.Color)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Color", ValorAnterior = _cultivoCama.Color == null ? "" : _cultivoCama.Color.ToString(), ValorNuevo = newCampo.Color.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Color = newCampo.Color; }
                model.SaveChanges();
                foreach (SGF_CultivoCuadro item in model.SGF_CultivoCuadro.Where(x => x.CultivoCamaID == _cultivoCama.CultivoCamaID))
                {
                    item.UsuarioRegistro = _cultivoCama.UsuarioRegistro;
                    item.Color = newCampo.Color;
                    item.Nombre = "Patrón";// _cultivoCama.Nombre;
                    CultivoCuadro_ActualizarNombreColor(item, ip, nompc);
                }
            }
            //}
        }
        [OperationContract]
        public void CultivoCama_InjertarVariedad(SGF_CultivoCama newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            //if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == newCampo.CultivoCamaID) > 0)
            //{
            _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == newCampo.CultivoCamaID);
            if (_cultivoCama != null)
            {
                if (_cultivoCama.VariedadSembrada != newCampo.VariedadSembrada)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "VariedadSembrada", ValorAnterior = _cultivoCama.VariedadSembrada.ToString(), ValorNuevo = newCampo.VariedadSembrada.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.VariedadSembrada = newCampo.VariedadSembrada; }
                if (_cultivoCama.FechaInjerto != newCampo.FechaInjerto)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "FechaInjerto", ValorAnterior = _cultivoCama.FechaInjerto == null ? "" : _cultivoCama.FechaInjerto.ToString(), ValorNuevo = newCampo.FechaInjerto.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.FechaInjerto = newCampo.FechaInjerto; }
                if (_cultivoCama.Color != newCampo.Color)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Color", ValorAnterior = _cultivoCama.Color == null ? "" : _cultivoCama.Color.ToString(), ValorNuevo = newCampo.Color.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Color = newCampo.Color; }
                model.SaveChanges();
                var _variedad = model.SGF_Variedad.First(x => x.VariedadID == newCampo.VariedadSembrada);
                foreach (SGF_CultivoCuadro item in model.SGF_CultivoCuadro.Where(x => x.CultivoCamaID == _cultivoCama.CultivoCamaID))
                {
                    item.UsuarioRegistro = _cultivoCama.UsuarioRegistro;
                    item.Color = newCampo.Color;
                    item.Nombre = _variedad == null ? item.Nombre : _variedad.Codigo;
                    CultivoCuadro_ActualizarNombreColor(item, ip, nompc);
                }
            }
            //}
        }
        [OperationContract]
        public void CultivoCama_LimpiarVariedad(SGF_CultivoCama newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == newCampo.CultivoCamaID) > 0)
            {
                _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == newCampo.CultivoCamaID);
                if (_cultivoCama != null)
                {
                    if (_cultivoCama.VariedadSembrada != newCampo.VariedadSembrada)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "VariedadSembrada", ValorAnterior = _cultivoCama.VariedadSembrada.ToString(), ValorNuevo = newCampo.VariedadSembrada.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.VariedadSembrada = newCampo.VariedadSembrada; }
                    if (_cultivoCama.FechaSiembra != newCampo.FechaSiembra)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "FechaSiembra", ValorAnterior = _cultivoCama.FechaSiembra == null ? "" : _cultivoCama.FechaSiembra.ToString(), ValorNuevo = newCampo.FechaSiembra.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.FechaSiembra = newCampo.FechaSiembra; }
                    if (_cultivoCama.FechaInjerto != newCampo.FechaInjerto)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "FechaInjerto", ValorAnterior = _cultivoCama.FechaInjerto == null ? "" : _cultivoCama.FechaInjerto.ToString(), ValorNuevo = newCampo.FechaInjerto.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.FechaInjerto = newCampo.FechaInjerto; }
                    if (_cultivoCama.Color != newCampo.Color)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Color", ValorAnterior = _cultivoCama.Color == null ? "" : _cultivoCama.Color.ToString(), ValorNuevo = newCampo.FechaInjerto.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Color = newCampo.Color; }
                    model.SaveChanges();
                }
            }
        }
        [OperationContract]
        public void CultivoCama_AsignarResponsable(SGF_CultivoCama newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == newCampo.CultivoCamaID) > 0)
            {
                _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == newCampo.CultivoCamaID);
                if (_cultivoCama != null)
                {
                    if (_cultivoCama.Responsable != newCampo.Responsable)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Responsable", ValorAnterior = _cultivoCama.Responsable == null ? "" : _cultivoCama.Responsable.ToString(), ValorNuevo = newCampo.Responsable.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Responsable = newCampo.Responsable; }
                    if (_cultivoCama.FechaResponsable != newCampo.FechaResponsable)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "FechaResponsable", ValorAnterior = _cultivoCama.FechaResponsable == null ? "" : _cultivoCama.FechaResponsable.ToString(), ValorNuevo = newCampo.FechaResponsable.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.FechaResponsable = newCampo.FechaResponsable; }
                    model.SaveChanges();
                }
            }
        }
        [OperationContract]
        public void CultivoCama_RedistribuirResponsable(SGF_CultivoCama newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == newCampo.CultivoCamaID) > 0)
            {
                _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == newCampo.CultivoCamaID);
                if (_cultivoCama != null)
                {
                    if (_cultivoCama.Responsable != newCampo.Responsable)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "Responsable", ValorAnterior = _cultivoCama.Responsable == null ? "" : _cultivoCama.Responsable.ToString(), ValorNuevo = newCampo.Responsable.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.Responsable = newCampo.Responsable; }
                    if (_cultivoCama.FechaResponsable != newCampo.FechaResponsable)
                    { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCama", Tipo = "Update", Campo = "FechaResponsable", ValorAnterior = _cultivoCama.FechaResponsable == null ? "" : _cultivoCama.FechaResponsable.ToString(), ValorNuevo = newCampo.FechaResponsable == null ? "" : newCampo.FechaResponsable.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCama.CultivoCamaID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCama.FechaResponsable = newCampo.FechaResponsable; }
                    model.SaveChanges();
                }
            }
        }
        #endregion
        #region Cultivo Cuadro
        [OperationContract]
        public void CultivoCuadro_Actualizar(SGF_CultivoCuadro newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCuadro _cultivoCuadro = new SGF_CultivoCuadro();
            //if (model.SGF_CultivoCuadro.Count(X => X.CultivoCuadroID == newCampo.CultivoCuadroID) > 0)
            //{
            _cultivoCuadro = model.SGF_CultivoCuadro.First(X => X.CultivoCuadroID == newCampo.CultivoCuadroID);
            if (_cultivoCuadro != null)
            {
                if (_cultivoCuadro.Aream2 != newCampo.Aream2)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCuadro", Tipo = "Update", Campo = "Aream2", ValorAnterior = _cultivoCuadro.Aream2.ToString(), ValorNuevo = newCampo.Aream2.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCuadro.CultivoCuadroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCuadro.Aream2 = newCampo.Aream2; }
                if (_cultivoCuadro.CantidadPlantas != newCampo.CantidadPlantas)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCuadro", Tipo = "Update", Campo = "CantidadPlantas", ValorAnterior = _cultivoCuadro.CantidadPlantas.ToString(), ValorNuevo = newCampo.CantidadPlantas.ToString(), FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCuadro.CultivoCuadroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCuadro.CantidadPlantas = newCampo.CantidadPlantas; }
                if (_cultivoCuadro.Nombre != newCampo.Nombre)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCuadro", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoCuadro.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCuadro.CultivoCuadroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCuadro.Nombre = newCampo.Nombre; }
                if (_cultivoCuadro.Color != newCampo.Color)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCuadro", Tipo = "Update", Campo = "Color", ValorAnterior = _cultivoCuadro.Color, ValorNuevo = newCampo.Color, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCuadro.CultivoCuadroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCuadro.Color = newCampo.Color; }
                model.SaveChanges();
            }
            //}
        }
        [OperationContract]
        public void CultivoCuadro_ActualizarNombreColor(SGF_CultivoCuadro newCampo, string ip, string nompc)
        {
            DataModel model = new DataModel();
            SGF_Auditoria _auditoria = new SGF_Auditoria();
            SGF_CultivoCuadro _cultivoCuadro = new SGF_CultivoCuadro();
            _cultivoCuadro = model.SGF_CultivoCuadro.First(X => X.CultivoCuadroID == newCampo.CultivoCuadroID);
            if (_cultivoCuadro != null)
            {
                if (_cultivoCuadro.Nombre != newCampo.Nombre)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCuadro", Tipo = "Update", Campo = "Nombre", ValorAnterior = _cultivoCuadro.Nombre, ValorNuevo = newCampo.Nombre, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCuadro.CultivoCuadroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCuadro.Nombre = newCampo.Nombre; }
                if (_cultivoCuadro.Color != newCampo.Color)
                { _auditoria = new SGF_Auditoria() { AuditoriaID = Guid.NewGuid(), Tabla = "SGF_CultivoCuadro", Tipo = "Update", Campo = "Color", ValorAnterior = _cultivoCuadro.Color, ValorNuevo = newCampo.Color, FechaRegistro = DateTime.Now, Usuario = newCampo.UsuarioRegistro, RegistroID = _cultivoCuadro.CultivoCuadroID.ToString(), IPAddress = ip, namePC = nompc, ApplicationName = "Módulo Cultivo" }; Auditoria_Grabar(_auditoria); _cultivoCuadro.Color = newCampo.Color; }
                model.SaveChanges();
            }
        }
        [OperationContract]
        public List<SGF_CultivoCuadro_VTA> CultivoCuadro_Recuperar_VTA(Guid camaID, Guid cuadroID)
        {
            DataModel model = new DataModel();
            List<SGF_CultivoCuadro_VTA> _resultado = new List<SGF_CultivoCuadro_VTA>();
            if (camaID == Guid.Empty)
                _resultado = model.SGF_CultivoCuadro_VTA.Where(x => x.CultivoCuadroID == cuadroID).ToList();
            else
                _resultado = model.SGF_CultivoCuadro_VTA.Where(x => x.CultivoCamaID == camaID).ToList();
            return _resultado;
        }
        [OperationContract]
        public SGF_CultivoCuadro_VTA CultivoCuadro_RecuperarPorID_VTA(Guid cuadroID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SGF_CultivoCuadro_VTA.First(x => x.CultivoCuadroID == cuadroID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Actividades Mapa de Cultivo
        [OperationContract]
        public void MapaCultivoActividades_Grabar(SGF_ActividadesMapaCultivo _actividades)
        {
            DataModel model = new DataModel();
            model.AddToSGF_ActividadesMapaCultivo(_actividades);
            model.SaveChanges();
        }

        [OperationContract]
        public bool MapaCultivoActividades_ValidarSiembraCama(int tipo, Guid _cultivoCamaID)
        {
            DataModel model = new DataModel();
            SGF_CultivoCama _cultivoCama = new SGF_CultivoCama();
            bool resultado = false;
            if (model.SGF_CultivoCama.Count(X => X.CultivoCamaID == _cultivoCamaID) > 0)
            {
                _cultivoCama = model.SGF_CultivoCama.First(X => X.CultivoCamaID == _cultivoCamaID);
                if (_cultivoCama != null)
                {
                    switch (tipo)
                    {
                        case 1: //Validar para sembrar
                            if (_cultivoCama.VariedadSembrada == Guid.Empty)
                                resultado = true;
                            break;
                        case 2://Validar para injertar
                            if (_cultivoCama.VariedadSembrada == new Guid("00000000-1111-1111-1111-000000000000"))
                                resultado = true;
                            break;
                        case 5://Validar para responsable
                            if (_cultivoCama.Responsable == Guid.Empty)
                                resultado = true;
                            break;
                        default:
                            resultado = false;
                            break;
                    }
                }
            }
            return resultado;
        }

        [OperationContract]
        public List<SGF_ActividadesMapaCultivo_VTA> MapaCultivoActividades_RecuperarID(int tipo, Guid _cultivoID)
        {
            try
            {
                DataModel model = new DataModel();
                List<SGF_ActividadesMapaCultivo_VTA> resultado = new List<SGF_ActividadesMapaCultivo_VTA>();
                switch (tipo)
                {
                    case (int)Enums.MapaCultivo.Area:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CultivoAreaID == _cultivoID).ToList();
                        break;
                    case (int)Enums.MapaCultivo.Bloque:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CultivoBloqueID == _cultivoID).ToList();
                        break;
                    case (int)Enums.MapaCultivo.Lado:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CultivoLadoID == _cultivoID).ToList();
                        break;
                    case (int)Enums.MapaCultivo.Nave:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CultivoNaveID == _cultivoID).ToList();
                        break;
                    case (int)Enums.MapaCultivo.Cama:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CultivoCamaID == _cultivoID).ToList();
                        break;
                    case (int)Enums.MapaCultivo.Cuadro:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CultivoCuadroID == _cultivoID).ToList();
                        break;
                    default:
                        resultado = model.SGF_ActividadesMapaCultivo_VTA.Where(x => x.CampoCultivoID == _cultivoID).ToList();
                        break;
                }
                return resultado.OrderBy(x => x.FechaActividad).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SP_ActividadesMapaCultivo_Buscar_Result> MapaCultivoActividades_RecuperarPorTipoID(int tipo, Guid _ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SP_ActividadesMapaCultivo_Buscar(tipo, _ID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [OperationContract]
        public List<SP_ActividadesMapaCultivo_BuscarVariedadCama_Result> MapaCultivoActividades_RecuperarVariedadPorTipoID(int tipo, Guid _ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SP_ActividadesMapaCultivo_BuscarVariedadCama(tipo, _ID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [OperationContract]
        public List<SP_ActividadesMapaCultivo_BuscarResponsablePorCama_Result> MapaCultivoActividades_RecuperarResponsablePorTipoID(int tipo, Guid _ID)
        {
            try
            {
                DataModel model = new DataModel();
                return model.SP_ActividadesMapaCultivo_BuscarResponsablePorCama(tipo, _ID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
