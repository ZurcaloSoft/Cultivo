using SGF.Site;
using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Cultivo
{
    public partial class Frm_ActividadesMapaCultivo : System.Web.UI.Page
    {
        #region variables y propiedades
        protected List<SGF_CultivoCuadro_VTA> ListCuadroCultivo
        {
            get
            {
                if (ViewState["ListCuadroCultivo"] == null)
                    ViewState["ListCuadroCultivo"] = new List<SGF_CultivoCuadro_VTA>();
                return (List<SGF_CultivoCuadro_VTA>)ViewState["ListCuadroCultivo"];
            }
            set
            {
                ViewState["ListCuadroCultivo"] = value;
            }
        }
        protected List<SGF_CultivoCuadro_VTA> ListCuadroCultivoPasivo
        {
            get
            {
                if (ViewState["ListCuadroCultivoPasivo"] == null)
                    ViewState["ListCuadroCultivoPasivo"] = new List<SGF_CultivoCuadro_VTA>();
                return (List<SGF_CultivoCuadro_VTA>)ViewState["ListCuadroCultivoPasivo"];
            }
            set
            {
                ViewState["ListCuadroCultivoPasivo"] = value;
            }
        }
        protected List<SGF_CultivoCama_VTA> ListCamaCultivo
        {
            get
            {
                if (ViewState["ListCamaCultivo"] == null)
                    ViewState["ListCamaCultivo"] = new List<SGF_CultivoCama_VTA>();
                return (List<SGF_CultivoCama_VTA>)ViewState["ListCamaCultivo"];
            }
            set
            {
                ViewState["ListCamaCultivo"] = value;
            }
        }
        protected List<SGF_CultivoCama_VTA> ListCamaCultivoPasivo
        {
            get
            {
                if (ViewState["ListCamaCultivoPasivo"] == null)
                    ViewState["ListCamaCultivoPasivo"] = new List<SGF_CultivoCama_VTA>();
                return (List<SGF_CultivoCama_VTA>)ViewState["ListCamaCultivoPasivo"];
            }
            set
            {
                ViewState["ListCamaCultivoPasivo"] = value;
            }
        }
        protected List<SGF_CultivoCama_VTA> ListCamaCultivoLibres
        {
            get
            {
                if (ViewState["ListCamaCultivoLibres"] == null)
                    ViewState["ListCamaCultivoLibres"] = new List<SGF_CultivoCama_VTA>();
                return (List<SGF_CultivoCama_VTA>)ViewState["ListCamaCultivoLibres"];
            }
            set
            {
                ViewState["ListCamaCultivoLibres"] = value;
            }
        }
        protected List<SGF_CultivoCama_VTA> ListCamaCultivoSembradas
        {
            get
            {
                if (ViewState["ListCamaCultivoSembradas"] == null)
                    ViewState["ListCamaCultivoSembradas"] = new List<SGF_CultivoCama_VTA>();
                return (List<SGF_CultivoCama_VTA>)ViewState["ListCamaCultivoSembradas"];
            }
            set
            {
                ViewState["ListCamaCultivoSembradas"] = value;
            }
        }
        protected List<SGF_CultivoCama_VTA> ListCamaCultivoInjertadas
        {
            get
            {
                if (ViewState["ListCamaCultivoInjertadas"] == null)
                    ViewState["ListCamaCultivoInjertadas"] = new List<SGF_CultivoCama_VTA>();
                return (List<SGF_CultivoCama_VTA>)ViewState["ListCamaCultivoInjertadas"];
            }
            set
            {
                ViewState["ListCamaCultivoInjertadas"] = value;
            }
        }
        protected SGF_ActividadesMapaCultivo IdActividadesSecciones
        {
            get
            {
                if (ViewState["IdActividadesSecciones"] == null)
                    ViewState["IdActividadesSecciones"] = new SGF_ActividadesMapaCultivo();
                return (SGF_ActividadesMapaCultivo)ViewState["IdActividadesSecciones"];
            }
            set
            {
                ViewState["IdActividadesSecciones"] = value;
            }
        }
        protected List<SP_ActividadesMapaCultivo_BuscarVariedadCama_Result> ListVariedadCamaCultivo
        {
            get
            {
                if (ViewState["ListVariedadCamaCultivo"] == null)
                    ViewState["ListVariedadCamaCultivo"] = new List<SP_ActividadesMapaCultivo_BuscarVariedadCama_Result>();
                return (List<SP_ActividadesMapaCultivo_BuscarVariedadCama_Result>)ViewState["ListVariedadCamaCultivo"];
            }
            set
            {
                ViewState["ListVariedadCamaCultivo"] = value;
            }
        }
        protected List<SP_ActividadesMapaCultivo_BuscarResponsablePorCama_Result> ListResponsableCamaCultivo
        {
            get
            {
                if (ViewState["ListResponsableCamaCultivo"] == null)
                    ViewState["ListResponsableCamaCultivo"] = new List<SP_ActividadesMapaCultivo_BuscarResponsablePorCama_Result>();
                return (List<SP_ActividadesMapaCultivo_BuscarResponsablePorCama_Result>)ViewState["ListResponsableCamaCultivo"];
            }
            set
            {
                ViewState["ListResponsableCamaCultivo"] = value;
            }
        }
        public Int32 Numerador
        {
            get { return (Int32)ViewState["Numerador"]; }
            set { ViewState["Numerador"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Numerador = 0;
                CargarCombos();
                RecuperarDatos();
                dtp_FechaRegistro.SelectedDate = DateTime.Now;
                dtp_FechaSiembra.SelectedDate = DateTime.Now;
                dtp_FechaAsignacionCuadrante.SelectedDate = DateTime.Now;
            }
        }

        private void RecuperarDatos()
        {
            LogicClient client = new LogicClient();
            if (Request["type"] != null)
            {
                #region definición de variables
                hdn_TypeID.Value = Request["type"];
                hdn_ItemCultivoID.Value = Request["itemID"];
                Numerador = 0;
                IdActividadesSecciones.ActividadesMapaCultivoID = Guid.Empty;
                IdActividadesSecciones.UsuarioRegistro = Me.Usuario.NombreUsuario;
                tab_Container.Tabs[3].Visible = false; //Distribución de Cuadrante
                tab_Container.Tabs[4].Visible = false; //Habilitar Espacio
                tab_Container.Tabs[5].Visible = false; //Actualizar Valores
                tab_Container.Tabs[1].Visible = true; //Sembrar 
                tab_Distribucion.Visible = false;
                List<SGF_CultivoCama_VTA> _ListDistribucionCamas = new List<SGF_CultivoCama_VTA>();
                List<SGF_Persona> _ListResponsable = new List<SGF_Persona>();
                pnl_SiembraLibres.Visible = false;
                pnl_SiembraPatron.Visible = false;
                pnl_SiembraInjerto.Visible = false;
                string _InfAream2 = "", _InfNroPlantas = "", _InfFechaSiembra = "", _InfFechaInjerto = "", _InfFechaInicioBloque = "";
                #endregion
                #region Asignación según el nivel de control
                switch (int.Parse(hdn_TypeID.Value))
                {
                    case (int)Enums.MapaCultivo.Area:
                        ListCamaCultivo = client.MapaCultivo_RecuperarTodo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value)).OrderBy(x => x.Orden).OrderBy(x => x.CultivoNaveOrden).ToList();
                        ListCamaCultivoLibres = ListCamaCultivo.Where(x => x.VariedadSembrada == Guid.Empty && x.Estado != 0).ToList();
                        ListCamaCultivoSembradas = ListCamaCultivo.Where(x => x.VariedadSembrada == ActividadCultivo.PatronLibre && x.Estado != 0).ToList();
                        ListCamaCultivoInjertadas = ListCamaCultivo.Where(x => (x.VariedadSembrada != Guid.Empty && x.VariedadSembrada != ActividadCultivo.PatronLibre) && x.Estado != 0).ToList();
                        lbl_Titulo.Text = "ACTIVIDADES DEL " + ListCamaCultivo.FirstOrDefault().CultivoAreaNombre.ToUpper();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.Area, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CampoCultivoID : Guid.Empty;
                        IdActividadesSecciones.CultivoAreaID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoAreaID : Guid.Empty;
                        IdActividadesSecciones.CultivoBloqueID = Guid.Empty;
                        IdActividadesSecciones.CultivoLadoID = Guid.Empty;
                        IdActividadesSecciones.CultivoNaveID = Guid.Empty;
                        IdActividadesSecciones.CultivoCamaID = Guid.Empty;
                        IdActividadesSecciones.CultivoCuadroID = Guid.Empty;
                        _InfAream2 = ListCamaCultivo.FirstOrDefault().CultivoAreaAream2.ToString();
                        _InfNroPlantas = ListCamaCultivo.FirstOrDefault().CultivoAreaNroPlantas.ToString();
                        _InfFechaSiembra = ListCamaCultivo.FirstOrDefault().FechaSiembra == null ? "" : ListCamaCultivo.FirstOrDefault().FechaSiembra.Value.ToShortDateString();
                        _InfFechaInjerto = ListCamaCultivo.FirstOrDefault().FechaInjerto == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInjerto.Value.ToShortDateString();
                        _InfFechaInicioBloque = ListCamaCultivo.FirstOrDefault().FechaInicioBloque == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInicioBloque.Value.ToShortDateString();
                        ListVariedadCamaCultivo = client.MapaCultivoActividades_RecuperarVariedadPorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListResponsableCamaCultivo = client.MapaCultivoActividades_RecuperarResponsablePorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        dl_InfResponsable.DataSource = ListResponsableCamaCultivo;
                        dl_InfVariedadSembrada.DataSource = ListVariedadCamaCultivo;
                        break;
                    case (int)Enums.MapaCultivo.Bloque:
                        ListCamaCultivo = client.MapaCultivo_RecuperarTodo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value)).OrderBy(x => x.Orden).OrderBy(x => x.CultivoNaveOrden).ToList();
                        ListCamaCultivoLibres = ListCamaCultivo.Where(x => x.VariedadSembrada == Guid.Empty && x.Estado != 0).ToList();
                        ListCamaCultivoSembradas = ListCamaCultivo.Where(x => x.VariedadSembrada == ActividadCultivo.PatronLibre && x.Estado != 0).ToList();
                        ListCamaCultivoInjertadas = ListCamaCultivo.Where(x => (x.VariedadSembrada != Guid.Empty && x.VariedadSembrada != ActividadCultivo.PatronLibre) && x.Estado != 0).ToList();
                        _ListDistribucionCamas = ListCamaCultivo.Where(x => x.Responsable == Guid.Empty).ToList();
                        lbl_Titulo.Text = "ACTIVIDADES DEL " + ListCamaCultivo.FirstOrDefault().CultivoBloqueNombre.ToUpper();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.Bloque, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CampoCultivoID : Guid.Empty;
                        IdActividadesSecciones.CultivoAreaID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoAreaID : Guid.Empty;
                        IdActividadesSecciones.CultivoBloqueID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoBloqueID : Guid.Empty;
                        IdActividadesSecciones.CultivoLadoID = Guid.Empty;
                        IdActividadesSecciones.CultivoNaveID = Guid.Empty;
                        IdActividadesSecciones.CultivoCamaID = Guid.Empty;
                        IdActividadesSecciones.CultivoCuadroID = Guid.Empty;
                        tab_Container.Tabs[3].Visible = true;
                        tab_Distribucion.Visible = true;
                        chk_InfActualizarBloque.Visible = true;
                        lbl_InfNombreBloque.Visible = true;
                        cmb_InfBloque.Visible = true;
                        cmb_InfBloque.SelectedValue = ListCamaCultivo.FirstOrDefault().BloqueID.ToString();
                        _InfAream2 = ListCamaCultivo.FirstOrDefault().CultivoBloqueAream2.ToString();
                        _InfNroPlantas = ListCamaCultivo.FirstOrDefault().CultivoBloqueNroPlantas.ToString();
                        _InfFechaSiembra = ListCamaCultivo.FirstOrDefault().FechaSiembra == null ? "" : ListCamaCultivo.FirstOrDefault().FechaSiembra.Value.ToShortDateString();
                        _InfFechaInjerto = ListCamaCultivo.FirstOrDefault().FechaInjerto == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInjerto.Value.ToShortDateString();
                        _InfFechaInicioBloque = ListCamaCultivo.FirstOrDefault().FechaInicioBloque == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInicioBloque.Value.ToShortDateString();
                        rlb_AsignacionCuadrante.DataSource = ListCamaCultivo;
                        rlb_AsignacionCuadrante.DataTextField = "Nombre";
                        rlb_AsignacionCuadrante.DataValueField = "CultivoCamaID";
                        rlb_AsignacionCuadrante.DataBind();
                        int _totalCuadrante = 0;
                        foreach (RadListBoxItem _cama in rlb_AsignacionCuadrante.Items)
                        {
                            var cultivoItem = ListCamaCultivo.FirstOrDefault(c => c.CultivoCamaID == new Guid(_cama.Value));
                            if (cultivoItem.Responsable != Guid.Empty)
                            {
                                _cama.Checked = true;
                                _cama.Enabled = false;
                                _totalCuadrante = _totalCuadrante + 1;
                            }
                        }

                        if (_totalCuadrante == rlb_AsignacionCuadrante.Items.Count)
                        {
                            rlb_AsignacionCuadrante.ShowCheckAll = false;
                            btn_AsignarResponsable.Visible = false;
                            btn_ReasignarDistribucion.Visible = true;
                        }
                        else
                        {
                            if (_totalCuadrante == 0)
                                btn_ReasignarDistribucion.Visible = false;
                            else
                                btn_ReasignarDistribucion.Visible = true;
                        }
                        ListVariedadCamaCultivo = client.MapaCultivoActividades_RecuperarVariedadPorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListResponsableCamaCultivo = client.MapaCultivoActividades_RecuperarResponsablePorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        dl_InfResponsable.DataSource = ListResponsableCamaCultivo;
                        dl_InfVariedadSembrada.DataSource = ListVariedadCamaCultivo;
                        rlb_ItemsValores.DataSource = _ListDistribucionCamas;
                        rlb_ItemsValores.DataTextField = "Nombre";
                        rlb_ItemsValores.DataValueField = "CultivoCamaID";
                        rlb_ItemsValores.DataBind();
                        tab_Container.Tabs[5].Visible = true;
                        tab_Valores.Visible = true;

                        break;
                    case (int)Enums.MapaCultivo.Lado:
                        ListCamaCultivo = client.MapaCultivo_RecuperarTodo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value)).OrderBy(x => x.Orden).OrderBy(x => x.CultivoNaveOrden).ToList();
                        ListCamaCultivoLibres = ListCamaCultivo.Where(x => x.VariedadSembrada == Guid.Empty && x.Estado != 0).ToList();
                        ListCamaCultivoSembradas = ListCamaCultivo.Where(x => x.VariedadSembrada == ActividadCultivo.PatronLibre && x.Estado != 0).ToList();
                        ListCamaCultivoInjertadas = ListCamaCultivo.Where(x => (x.VariedadSembrada != Guid.Empty && x.VariedadSembrada != ActividadCultivo.PatronLibre) && x.Estado != 0).ToList();
                        lbl_Titulo.Text = "ACTIVIDADES DEL " + ListCamaCultivo.FirstOrDefault().CultivoLadoNombre.ToUpper();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.Lado, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CampoCultivoID : Guid.Empty;
                        IdActividadesSecciones.CultivoAreaID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoAreaID : Guid.Empty;
                        IdActividadesSecciones.CultivoBloqueID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoBloqueID : Guid.Empty;
                        IdActividadesSecciones.CultivoLadoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoLadoID : Guid.Empty;
                        IdActividadesSecciones.CultivoNaveID = Guid.Empty;
                        IdActividadesSecciones.CultivoCamaID = Guid.Empty;
                        IdActividadesSecciones.CultivoCuadroID = Guid.Empty;
                        _InfAream2 = ListCamaCultivo.FirstOrDefault().CultivoLadoAream2.ToString();
                        _InfNroPlantas = ListCamaCultivo.FirstOrDefault().CultivoLadoNroPlantas.ToString();
                        _InfFechaSiembra = ListCamaCultivo.FirstOrDefault().FechaSiembra == null ? "" : ListCamaCultivo.FirstOrDefault().FechaSiembra.Value.ToShortDateString();
                        _InfFechaInjerto = ListCamaCultivo.FirstOrDefault().FechaInjerto == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInjerto.Value.ToShortDateString();
                        _InfFechaInicioBloque = ListCamaCultivo.FirstOrDefault().FechaInicioBloque == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInicioBloque.Value.ToShortDateString();
                        ListVariedadCamaCultivo = client.MapaCultivoActividades_RecuperarVariedadPorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListResponsableCamaCultivo = client.MapaCultivoActividades_RecuperarResponsablePorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        dl_InfResponsable.DataSource = ListResponsableCamaCultivo;
                        dl_InfVariedadSembrada.DataSource = ListVariedadCamaCultivo;
                        break;
                    case (int)Enums.MapaCultivo.Nave:
                        ListCamaCultivo = client.MapaCultivo_RecuperarTodo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value)).OrderBy(x => x.Orden).ToList();
                        ListCamaCultivoPasivo = client.MapaCultivo_RecuperarTodoPasivo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListCamaCultivoLibres = ListCamaCultivo.Where(x => x.VariedadSembrada == Guid.Empty && x.Estado != 0).ToList();
                        ListCamaCultivoSembradas = ListCamaCultivo.Where(x => x.VariedadSembrada == ActividadCultivo.PatronLibre && x.Estado != 0).ToList();
                        ListCamaCultivoInjertadas = ListCamaCultivo.Where(x => (x.VariedadSembrada != Guid.Empty && x.VariedadSembrada != ActividadCultivo.PatronLibre) && x.Estado != 0).ToList();
                        lbl_Titulo.Text = "ACTIVIDADES DEL " + ListCamaCultivo.FirstOrDefault().CultivoNaveNombre.ToUpper();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.Nave, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CampoCultivoID : Guid.Empty;
                        IdActividadesSecciones.CultivoAreaID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoAreaID : Guid.Empty;
                        IdActividadesSecciones.CultivoBloqueID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoBloqueID : Guid.Empty;
                        IdActividadesSecciones.CultivoLadoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoLadoID : Guid.Empty;
                        IdActividadesSecciones.CultivoNaveID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoNaveID : Guid.Empty;
                        IdActividadesSecciones.CultivoCamaID = Guid.Empty;
                        IdActividadesSecciones.CultivoCuadroID = Guid.Empty;
                        _InfAream2 = ListCamaCultivo.FirstOrDefault().CultivoNaveAream2.ToString();
                        _InfNroPlantas = ListCamaCultivo.FirstOrDefault().CultivoNaveNroPlantas.ToString();
                        _InfFechaSiembra = ListCamaCultivo.FirstOrDefault().FechaSiembra == null ? "" : ListCamaCultivo.FirstOrDefault().FechaSiembra.Value.ToShortDateString();
                        _InfFechaInjerto = ListCamaCultivo.FirstOrDefault().FechaInjerto == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInjerto.Value.ToShortDateString();
                        _InfFechaInicioBloque = ListCamaCultivo.FirstOrDefault().FechaInicioBloque == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInicioBloque.Value.ToShortDateString();
                        ListVariedadCamaCultivo = client.MapaCultivoActividades_RecuperarVariedadPorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListResponsableCamaCultivo = client.MapaCultivoActividades_RecuperarResponsablePorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        dl_InfResponsable.DataSource = ListResponsableCamaCultivo;
                        dl_InfVariedadSembrada.DataSource = ListVariedadCamaCultivo;
                        tab_Container.Tabs[4].Visible = true;
                        tab_Estados.Visible = true;
                        rlb_ItemsActivos.DataSource = ListCamaCultivo.OrderBy(x => x.Nombre);
                        rlb_ItemsActivos.DataTextField = "Nombre";
                        rlb_ItemsActivos.DataValueField = "CultivoCamaID";
                        rlb_ItemsActivos.DataBind();
                        rlb_ItemsPasivos.DataSource = ListCamaCultivo.OrderBy(x => x.Nombre);
                        rlb_ItemsPasivos.DataTextField = "Nombre";
                        rlb_ItemsPasivos.DataValueField = "CultivoCamaID";
                        rlb_ItemsPasivos.DataBind();

                        break;
                    case (int)Enums.MapaCultivo.Cama:
                        var _TemplistCuadro = client.CultivoCuadro_Recuperar_VTA(new Guid(hdn_ItemCultivoID.Value), Guid.Empty);
                        ListCuadroCultivo = _TemplistCuadro.Where(x => x.Estado == 1).ToList();
                        ListCuadroCultivoPasivo = _TemplistCuadro.Where(x => x.Estado == 0).ToList();

                        rlb_ItemsActivos.DataSource = ListCuadroCultivo.OrderBy(x => x.NombreClasificador);
                        rlb_ItemsActivos.DataTextField = "NombreCompleto";
                        rlb_ItemsActivos.DataValueField = "CultivoCuadroID";
                        rlb_ItemsActivos.DataBind();
                        rlb_ItemsPasivos.DataSource = ListCuadroCultivoPasivo.OrderBy(x => x.NombreClasificador);
                        rlb_ItemsPasivos.DataTextField = "NombreCompleto";
                        rlb_ItemsPasivos.DataValueField = "CultivoCuadroID";
                        rlb_ItemsPasivos.DataBind();

                        ListCamaCultivo = client.MapaCultivo_RecuperarTodo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListCamaCultivoPasivo = client.MapaCultivo_RecuperarTodoPasivo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListCamaCultivoLibres = ListCamaCultivo.Where(x => x.VariedadSembrada == Guid.Empty && x.Estado != 0).ToList();
                        ListCamaCultivoSembradas = ListCamaCultivo.Where(x => x.VariedadSembrada == ActividadCultivo.PatronLibre && x.Estado != 0).ToList();
                        ListCamaCultivoInjertadas = ListCamaCultivo.Where(x => (x.VariedadSembrada != Guid.Empty && x.VariedadSembrada != ActividadCultivo.PatronLibre) && x.Estado != 0).ToList();
                        lbl_Titulo.Text = "ACTIVIDADES DEL " + ListCamaCultivo.FirstOrDefault().Nombre.ToUpper();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.Cama, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CampoCultivoID : Guid.Empty;
                        IdActividadesSecciones.CultivoAreaID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoAreaID : Guid.Empty;
                        IdActividadesSecciones.CultivoBloqueID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoBloqueID : Guid.Empty;
                        IdActividadesSecciones.CultivoLadoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoLadoID : Guid.Empty;
                        IdActividadesSecciones.CultivoNaveID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoNaveID : Guid.Empty;
                        IdActividadesSecciones.CultivoCamaID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CultivoCamaID : Guid.Empty;
                        IdActividadesSecciones.CultivoCuadroID = Guid.Empty;
                        _InfAream2 = ListCamaCultivo.FirstOrDefault().Aream2.ToString();
                        _InfNroPlantas = ListCamaCultivo.FirstOrDefault().CantidadPlantas.ToString();
                        _InfFechaSiembra = ListCamaCultivo.FirstOrDefault().FechaSiembra == null ? "" : ListCamaCultivo.FirstOrDefault().FechaSiembra.Value.ToShortDateString();
                        _InfFechaInjerto = ListCamaCultivo.FirstOrDefault().FechaInjerto == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInjerto.Value.ToShortDateString();
                        _InfFechaInicioBloque = ListCamaCultivo.FirstOrDefault().FechaInicioBloque == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInicioBloque.Value.ToShortDateString();
                        ListVariedadCamaCultivo = client.MapaCultivoActividades_RecuperarVariedadPorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListResponsableCamaCultivo = client.MapaCultivoActividades_RecuperarResponsablePorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        dl_InfResponsable.DataSource = ListResponsableCamaCultivo;
                        dl_InfVariedadSembrada.DataSource = ListVariedadCamaCultivo;
                        tab_Container.Tabs[4].Visible = true;
                        tab_Estados.Visible = true;
                        break;
                    case (int)Enums.MapaCultivo.Cuadro:
                        var _cultCuadro = client.CultivoCuadro_RecuperarPorID_VTA(new Guid(hdn_ItemCultivoID.Value));
                        if (_cultCuadro != null)
                        {
                            if (_cultCuadro.Estado == 1)
                                ListCuadroCultivo.Add(_cultCuadro);
                            else
                                ListCuadroCultivoPasivo.Add(_cultCuadro);

                        }
                        lbl_Titulo.Text = "ACTIVIDADES DEL " + ListCuadroCultivo.FirstOrDefault().Nombre.ToUpper();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.Cuadro, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = _cultCuadro == null ? Guid.Empty : _cultCuadro.CampoCultivoID;
                        IdActividadesSecciones.CultivoAreaID = _cultCuadro == null ? Guid.Empty : _cultCuadro.CultivoAreaID;
                        IdActividadesSecciones.CultivoBloqueID = _cultCuadro == null ? Guid.Empty : _cultCuadro.CultivoBloqueID;
                        IdActividadesSecciones.CultivoLadoID = _cultCuadro == null ? Guid.Empty : _cultCuadro.CultivoLadoID;
                        IdActividadesSecciones.CultivoNaveID = _cultCuadro == null ? Guid.Empty : _cultCuadro.CultivoNaveID;
                        IdActividadesSecciones.CultivoCamaID = _cultCuadro == null ? Guid.Empty : _cultCuadro.CultivoCamaID;
                        IdActividadesSecciones.CultivoCuadroID = new Guid(hdn_ItemCultivoID.Value);
                        tab_Container.Tabs[1].Visible = false; //Sembrar 
                        _InfAream2 = ListCuadroCultivo.FirstOrDefault().Aream2.ToString();
                        _InfNroPlantas = ListCuadroCultivo.FirstOrDefault().CantidadPlantas.ToString();
                        //_InfFechaSiembra = ListCamaCultivo.FirstOrDefault().FechaSiembra == null ? "" : ListCamaCultivo.FirstOrDefault().FechaSiembra.Value.ToShortDateString();
                        //_InfFechaInjerto = ListCamaCultivo.FirstOrDefault().FechaInjerto == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInjerto.Value.ToShortDateString();
                        //_InfFechaInicioBloque = ListCamaCultivo.FirstOrDefault().FechaInicioBloque == null ? "" : ListCamaCultivo.FirstOrDefault().FechaInicioBloque.Value.ToShortDateString();
                        ListVariedadCamaCultivo = client.MapaCultivoActividades_RecuperarVariedadPorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListResponsableCamaCultivo = client.MapaCultivoActividades_RecuperarResponsablePorTipoID(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        dl_InfResponsable.DataSource = ListResponsableCamaCultivo;
                        dl_InfVariedadSembrada.DataSource = ListVariedadCamaCultivo;
                        tab_Container.Tabs[4].Visible = true;
                        tab_Estados.Visible = true;
                        break;
                    default:
                        lbl_Titulo.Text = "ACTIVIDADES GENERALES";
                        ListCamaCultivo = client.MapaCultivo_RecuperarTodo_VTA(int.Parse(hdn_TypeID.Value), new Guid(hdn_ItemCultivoID.Value));
                        ListCamaCultivoLibres = ListCamaCultivo.Where(x => x.VariedadSembrada == Guid.Empty && x.Estado != 0).ToList();
                        ListCamaCultivoSembradas = ListCamaCultivo.Where(x => x.VariedadSembrada == ActividadCultivo.PatronLibre && x.Estado != 0).ToList();
                        ListCamaCultivoInjertadas = ListCamaCultivo.Where(x => (x.VariedadSembrada != Guid.Empty && x.VariedadSembrada != ActividadCultivo.PatronLibre) && x.Estado != 0).ToList();
                        grid_DetalleActividades.DataSource = client.MapaCultivoActividades_RecuperarPorTipoID((int)Enums.MapaCultivo.MapaCultivo, new Guid(hdn_ItemCultivoID.Value));
                        IdActividadesSecciones.CampoCultivoID = ListCamaCultivo.Count > 0 ? ListCamaCultivo.FirstOrDefault().CampoCultivoID : Guid.Empty;
                        IdActividadesSecciones.CultivoAreaID = Guid.Empty;
                        IdActividadesSecciones.CultivoBloqueID = Guid.Empty;
                        IdActividadesSecciones.CultivoLadoID = Guid.Empty;
                        IdActividadesSecciones.CultivoNaveID = Guid.Empty;
                        IdActividadesSecciones.CultivoCamaID = Guid.Empty;
                        IdActividadesSecciones.CultivoCuadroID = Guid.Empty;
                        break;
                }
                dl_InfResponsable.DataBind();
                dl_InfVariedadSembrada.DataBind();
                grid_DetalleActividades.DataBind();
                rlb_ItemsCultivo.DataSource = ListCamaCultivoLibres;
                rlb_ItemsCultivo.DataTextField = "Nombre";
                rlb_ItemsCultivo.DataValueField = "CultivoCamaID";
                rlb_ItemsCultivo.DataBind();
                rlb_ItemsSembrar.DataSource = ListCamaCultivoSembradas;
                rlb_ItemsSembrar.DataTextField = "Nombre";
                rlb_ItemsSembrar.DataValueField = "CultivoCamaID";
                rlb_ItemsSembrar.DataBind();
                rlb_ItemsInjertar.DataSource = ListCamaCultivoInjertadas;
                rlb_ItemsInjertar.DataTextField = "Nombre";
                rlb_ItemsInjertar.DataValueField = "CultivoCamaID";
                rlb_ItemsInjertar.DataBind();


                rlb_ItemsValores.DataSource = ListCamaCultivo.OrderBy(x => x.Nombre);
                rlb_ItemsValores.DataTextField = "Nombre";
                rlb_ItemsValores.DataValueField = "CultivoCamaID";
                rlb_ItemsValores.DataBind();

                #endregion
                #region Verificar Siembra e injerto
                //int _numCamas = ListCamaCultivo.Count;
                //int _numCamasLibres = 0;
                //int _numCamasSembradas = 0;
                //int _numCamasInjertadas = 0;
                //foreach (SGF_CultivoCama_VTA item in ListCamaCultivo)
                //{
                //    if (item.VariedadSembrada == Guid.Empty)
                //        _numCamasLibres = _numCamasLibres + 1;
                //    else
                //    if (item.VariedadSembrada == ActividadCultivo.PatronLibre)
                //        _numCamasSembradas = _numCamasSembradas + 1;
                //    else
                //        _numCamasInjertadas = _numCamasInjertadas + 1;
                //}
                txt_InfCamasLibres.Text = ListCamaCultivoLibres.Count.ToString();//_numCamasLibres.ToString();
                txt_InfCamasInjertadas.Text = ListCamaCultivoInjertadas.Count.ToString();// _numCamasInjertadas.ToString();
                txt_InfCamasSembradas.Text = ListCamaCultivoSembradas.Count.ToString();// _numCamasSembradas.ToString();
                txt_InfAream2.Text = _InfAream2;
                txt_InfNroPlantas.Text = _InfNroPlantas;
                txt_InfFechaSiembra.Text = _InfFechaSiembra;
                txt_InfFechaInjerto.Text = _InfFechaInjerto;
                txt_InfFechaInicioBloque.Text = _InfFechaInicioBloque;

                if (ListCamaCultivoLibres.Count > 0)//ListCamaCultivo.Count >= ListCamaCultivoLibres.Count)
                {
                    pnl_SiembraLibres.Visible = true;
                    //btn_Sembrar.Visible = true;
                    //btn_Injertar.Visible = false;
                    //btn_CierreSiembra.Visible = false;
                    //lbl_TextoCultivo.Visible = false;
                    //cmb_Variedad.Visible = false;
                    //lbl_FechaSiembra.Text = "Fecha de Siembra";
                }
                if (ListCamaCultivoSembradas.Count > 0)//ListCamaCultivo.Count == (ListCamaCultivoSembradas.Count + ListCamaCultivoInjertadas.Count))
                {
                    pnl_SiembraPatron.Visible = true;
                    //btn_Sembrar.Visible = false;
                    //btn_Injertar.Visible = true;
                    //btn_CierreSiembra.Visible = true;
                    //lbl_TextoCultivo.Visible = true;
                    //cmb_Variedad.Visible = true;
                    //lbl_FechaSiembra.Text = "Fecha de Injerto";
                    //btn_CierreSiembra.Visible = true;
                }
                if (ListCamaCultivoInjertadas.Count > 0)//ListCamaCultivo.Count == ListCamaCultivoInjertadas.Count)
                {
                    pnl_SiembraInjerto.Visible = true;
                    //btn_Sembrar.Visible = false;
                    //btn_Injertar.Visible = false;
                    //btn_CierreSiembra.Visible = true;
                    //rlb_ItemsCultivo.Enabled = false;
                    //rlb_ItemsInjertar.Enabled= false;
                    //rlb_ItemsSembrar.Enabled = false;   
                    //lbl_TextoCultivo.Visible = false;
                    //cmb_Variedad.Visible = false;
                    //dtp_FechaSiembra.Visible = false;
                    //lbl_FechaSiembra.Text = "PLANTAS INJERTADAS EN " + (ListCamaCultivo.Count == 0 ? "" : ListCamaCultivo.FirstOrDefault().Nombre.ToUpper());
                }

                // Marcar los elementos según su estado inicial
                int _total = 0;
                foreach (RadListBoxItem _cama in rlb_ItemsCultivo.Items)
                {
                    var cultivoItem = ListCamaCultivo.FirstOrDefault(c => c.CultivoCamaID == new Guid(_cama.Value));
                    if (cultivoItem.VariedadSembrada != Guid.Empty)
                    {
                        _cama.Checked = true;
                        _cama.Enabled = false;
                        _total = _total + 1;
                    }
                }
                if (_total == rlb_ItemsCultivo.Items.Count)
                    rlb_ItemsCultivo.ShowCheckAll = false;
                cmb_Variedad.SelectedValue = Guid.Empty.ToString();
                cmb_TipoActividad.SelectedValue = Guid.Empty.ToString();
                dtp_FechaSiembra.SelectedDate = null;
                dtp_FechaRegistro.SelectedDate = null;
                txt_DescripcionActividad.Text = "";
                #endregion
            }
        }

        protected Int32 NumeradorColumna()
        {
            Int32 _num = 0;
            _num = Numerador + 1;
            Numerador = _num;
            return _num;
        }

        private void CargarCombos()
        {
            LogicClient client = new LogicClient();
            cmb_TipoActividad.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Actividades);
            cmb_TipoActividad.DataTextField = "Nombre";
            cmb_TipoActividad.DataValueField = "ClasificadorID";
            cmb_TipoActividad.DataBind();
            cmb_TipoActividad.Items.Insert(0, new RadComboBoxItem("Seleccione la Actividad", Guid.Empty.ToString()));

            cmb_Variedad.DataSource = client.Variedad_ObtenerTodo();
            cmb_Variedad.DataTextField = "Nombre";
            cmb_Variedad.DataValueField = "VariedadID";
            cmb_Variedad.DataBind();
            cmb_Variedad.Items.Insert(0, new RadComboBoxItem("Seleccione la Variedad", Guid.Empty.ToString()));

            cmb_InfBloque.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.CultivoBloques);
            cmb_InfBloque.DataTextField = "Nombre";
            cmb_InfBloque.DataValueField = "ClasificadorID";
            cmb_InfBloque.DataBind();
            cmb_InfBloque.Items.Insert(0, new RadComboBoxItem("Seleccione Bloque", Guid.Empty.ToString()));

            cmb_Responsable.DataSource = client.Persona_ObtenerPorTipoPersona((Guid)TipoPersona.Empleado).ToList();
            cmb_Responsable.DataTextField = "Nombre";
            cmb_Responsable.DataValueField = "PersonaID";
            cmb_Responsable.DataBind();
            cmb_Responsable.Items.Insert(0, new RadComboBoxItem("Seleccione Responsable", Guid.Empty.ToString()));

        }

        protected void grid_Persona_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hdn_PersonaID.Value = grid_Persona.SelectedValues["PersonaID"].ToString();
            //hdn_PersonaNombre.Value = grid_Persona.SelectedValues["NombrePersona"].ToString();
            //hdn_PersonaIdentificacion.Value = grid_Persona.SelectedValues["Identificacion"].ToString();
            // Cierre de la ventana
            ScriptManager.RegisterStartupScript(this, typeof(string), "cierre", "returnToParent();", true);
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            //if (txt_Filtro.Text == "")
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar un criterio para poder realizar la búsqueda.");
            //    return;
            //}

            LogicClient client = new LogicClient();
            List<SGF_Persona_VTA> _listado = new List<SGF_Persona_VTA>();
            //if (rbt_Parametro.SelectedValue.ToString() == "CODIGO")
            //    _listado = client.Persona_BuscarPersonaVTA(Guid.Empty, txt_Filtro.Text, "").ToList();
            //else
            //    _listado = client.Persona_BuscarPersonaVTA(Guid.Empty, "", txt_Filtro.Text.ToUpper()).ToList();
            //grid_Persona.DataSource = _listado;
            //grid_Persona.DataBind();
        }

        private void VerMensaje(string title, string titIcon, string icon, string mensaje)
        {
            RadNotification1.Title = title;// "Friendship invitation";
            RadNotification1.TitleIcon = titIcon;// "none";//"info"
            RadNotification1.ContentIcon = icon;//"warning";//"info"
            RadNotification1.Text = mensaje;
            RadNotification1.Show();

        }

        protected void btn_Sembrar_Click(object sender, EventArgs e)
        {
            if (dtp_FechaSiembra.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la fecha de Siembra.");
                //Utils.MessageBox(this, "", "Debe seleccionar la fecha de Siembra", "info");
                return;
            }
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemCama in rlb_ItemsCultivo.Items)
            {
                if (itemCama.Checked == true)
                {
                    SGF_ActividadesMapaCultivo _cultivo = new SGF_ActividadesMapaCultivo();
                    _cultivo.ActividadesMapaCultivoID = Guid.NewGuid();
                    _cultivo.CampoCultivoID = ListCamaCultivoLibres.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CampoCultivoID;
                    _cultivo.CultivoAreaID = ListCamaCultivoLibres.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoAreaID;
                    _cultivo.CultivoBloqueID = ListCamaCultivoLibres.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoBloqueID;
                    _cultivo.CultivoLadoID = ListCamaCultivoLibres.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoLadoID;
                    _cultivo.CultivoNaveID = ListCamaCultivoLibres.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoNaveID;
                    _cultivo.CultivoCamaID = ListCamaCultivoLibres.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoCamaID;
                    _cultivo.CultivoCuadroID = Guid.Empty;
                    _cultivo.ActividadID = ActividadCultivo.Siembra;
                    _cultivo.FechaActividad = dtp_FechaSiembra.SelectedDate;
                    _cultivo.Descripcion = "Siembra de plantas en el " + ListCamaCultivoLibres.FirstOrDefault().CultivoLadoNombre.ToUpper();
                    _cultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _cultivo.FechaRegistro = DateTime.Now;
                    _cultivo.Estado = 1;
                    ////bool _grabar = client.MapaCultivoActividades_ValidarSiembraCama(1, (Guid)_cultivo.CultivoCamaID);
                    ////if (_grabar == true)
                    client.MapaCultivoActividades_Grabar(_cultivo);

                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                    _cama.CultivoCamaID = new Guid(itemCama.Value);
                    _cama.VariedadSembrada = ActividadCultivo.PatronLibre;
                    _cama.FechaRegistro = DateTime.Now;
                    _cama.FechaSiembra = dtp_FechaSiembra.SelectedDate;
                    _cama.Color = "Orange";
                    _cama.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    client.CultivoCama_SembrarVariedad(_cama, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                }
            }
            VerMensaje("INFORMACIÓN", "info", "info", "Registro de Siembra realizado correctamente.");
            RecuperarDatos();
        }

        protected void btn_Injertar_Click(object sender, EventArgs e)
        {
            if (dtp_FechaInjerto.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la fecha de Injerto");
                //Utils.MessageBox(this, "", "Debe seleccionar la fecha de Siembra", "info");
                return;
            }
            if (cmb_Variedad.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Variedad");
                return;
            }
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemCama in rlb_ItemsSembrar.Items)
            {
                if (itemCama.Checked == true)
                {
                    SGF_ActividadesMapaCultivo _cultivo = new SGF_ActividadesMapaCultivo();
                    _cultivo.ActividadesMapaCultivoID = Guid.NewGuid();
                    _cultivo.CampoCultivoID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CampoCultivoID;
                    _cultivo.CultivoAreaID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoAreaID;
                    _cultivo.CultivoBloqueID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoBloqueID;
                    _cultivo.CultivoLadoID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoLadoID;
                    _cultivo.CultivoNaveID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoNaveID;
                    _cultivo.CultivoCamaID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoCamaID;
                    _cultivo.CultivoCuadroID = Guid.Empty;
                    _cultivo.ActividadID = ActividadCultivo.Injerto;
                    _cultivo.FechaActividad = dtp_FechaSiembra.SelectedDate;
                    _cultivo.Descripcion = "Injerto de plantas en el " + ListCamaCultivoSembradas.FirstOrDefault().CultivoLadoNombre.ToUpper();
                    _cultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _cultivo.FechaRegistro = DateTime.Now;
                    _cultivo.Estado = 1;
                    //bool _grabar = client.MapaCultivoActividades_ValidarSiembraCama(2, (Guid)_cultivo.CultivoCamaID);
                    //if (_grabar == true)
                    client.MapaCultivoActividades_Grabar(_cultivo);

                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                    _cama.CultivoCamaID = new Guid(itemCama.Value);
                    _cama.VariedadSembrada = new Guid(cmb_Variedad.SelectedValue);
                    _cama.FechaRegistro = DateTime.Now;
                    _cama.FechaInjerto = dtp_FechaSiembra.SelectedDate;
                    _cama.Color = "LightGreen";
                    _cama.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    client.CultivoCama_InjertarVariedad(_cama, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                }
            }
            VerMensaje("INFORMACIÓN", "info", "info", "Registro de Injerto realizado correctamente.");

            //Utils.MessageBox(this, "", "Registro de Siembra realizado correctamente", "info");
            RecuperarDatos();
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "cierre", "returnToParent();", true);
        }

        protected void tab_Container_TabClick(object sender, RadTabStripEventArgs e)
        {

        }

        protected void rlb_ItemsCultivo_ItemCreated(object sender, RadListBoxItemEventArgs e)
        {
        }

        protected void btn_RegistrarActividad_Click(object sender, EventArgs e)
        {
            if (dtp_FechaRegistro.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "ACTIVIDADES: Debe seleccionar la fecha de registro de Actividad");
                return;
            }
            if (cmb_TipoActividad.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "ACTIVIDADES: Debe seleccionar el Tipo de Actividad");
                return;
            }
            if (txt_DescripcionActividad.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "ACTIVIDADES: Debe ingresar la descripción de la Actividad");
                return;
            }
            LogicClient client = new LogicClient();
            SGF_ActividadesMapaCultivo _cultivo = new SGF_ActividadesMapaCultivo();
            _cultivo.ActividadesMapaCultivoID = Guid.NewGuid();
            _cultivo.CampoCultivoID = IdActividadesSecciones.CampoCultivoID;
            _cultivo.CultivoAreaID = IdActividadesSecciones.CultivoAreaID;
            _cultivo.CultivoBloqueID = IdActividadesSecciones.CultivoBloqueID;
            _cultivo.CultivoLadoID = IdActividadesSecciones.CultivoLadoID;
            _cultivo.CultivoNaveID = IdActividadesSecciones.CultivoNaveID;
            _cultivo.CultivoCamaID = IdActividadesSecciones.CultivoCamaID;
            _cultivo.CultivoCuadroID = IdActividadesSecciones.CultivoCuadroID;
            _cultivo.ActividadID = new Guid(cmb_TipoActividad.SelectedValue);
            _cultivo.FechaActividad = dtp_FechaRegistro.SelectedDate;
            _cultivo.Descripcion = txt_DescripcionActividad.Text;
            _cultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
            _cultivo.FechaRegistro = DateTime.Now;
            _cultivo.Estado = 1;
            client.MapaCultivoActividades_Grabar(_cultivo);
            VerMensaje("INFORMACIÓN", "info", "info", "Actividad registrada correctamente.");
            RecuperarDatos();
        }

        protected void btn_AsignarResponsable_Click(object sender, EventArgs e)
        {
            if (cmb_Responsable.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Responsable");
                return;
            }
            if (dtp_FechaAsignacionCuadrante.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la fecha de Asignación");
                return;
            }
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemCama in rlb_AsignacionCuadrante.Items)
            {
                if (itemCama.Checked == true)
                {
                    SGF_ActividadesMapaCultivo _cultivo = new SGF_ActividadesMapaCultivo();
                    _cultivo.ActividadesMapaCultivoID = Guid.NewGuid();
                    _cultivo.CampoCultivoID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CampoCultivoID;
                    _cultivo.CultivoAreaID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoAreaID;
                    _cultivo.CultivoBloqueID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoBloqueID;
                    _cultivo.CultivoLadoID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoLadoID;
                    _cultivo.CultivoNaveID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoNaveID;
                    _cultivo.CultivoCamaID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoCamaID;
                    _cultivo.CultivoCuadroID = Guid.Empty;
                    _cultivo.ActividadID = ActividadCultivo.DistribucionCuadrantes;
                    _cultivo.FechaActividad = dtp_FechaAsignacionCuadrante.SelectedDate;
                    _cultivo.Descripcion = "Asignación de Responasble en el " + ListCamaCultivo.FirstOrDefault().CultivoLadoNombre.ToUpper();
                    _cultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _cultivo.FechaRegistro = DateTime.Now;
                    _cultivo.Estado = 1;
                    bool _grabar = client.MapaCultivoActividades_ValidarSiembraCama(5, (Guid)_cultivo.CultivoCamaID); //5. Validar Responsable
                    if (_grabar == true)
                        client.MapaCultivoActividades_Grabar(_cultivo);
                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                    _cama.CultivoCamaID = new Guid(itemCama.Value);
                    _cama.Responsable = new Guid(cmb_Responsable.SelectedValue);
                    _cama.FechaResponsable = dtp_FechaAsignacionCuadrante.SelectedDate;
                    _cama.FechaRegistro = DateTime.Now;
                    client.CultivoCama_AsignarResponsable(_cama, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                }
            }
            //VerMensaje("INFORMACIÓN", "info", "info", "Asignación de Resposnables Distribuido correctamente.");
            Utils.MessageBox(this, "", "Asignación de Responsables Distribuidos correctamente.", "info");
            RecuperarDatos();
        }

        protected void btn_ReasignarDistribucion_Click(object sender, EventArgs e)
        {
            if (dtp_FechaAsignacionCuadrante.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la fecha de Asignación");
                return;
            }
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemCama in rlb_AsignacionCuadrante.Items)
            {
                if (itemCama.Checked == true)
                {
                    SGF_ActividadesMapaCultivo _cultivo = new SGF_ActividadesMapaCultivo();
                    _cultivo.ActividadesMapaCultivoID = Guid.NewGuid();
                    _cultivo.CampoCultivoID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CampoCultivoID;
                    _cultivo.CultivoAreaID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoAreaID;
                    _cultivo.CultivoBloqueID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoBloqueID;
                    _cultivo.CultivoLadoID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoLadoID;
                    _cultivo.CultivoNaveID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoNaveID;
                    _cultivo.CultivoCamaID = ListCamaCultivo.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoCamaID;
                    _cultivo.CultivoCuadroID = Guid.Empty;
                    _cultivo.ActividadID = ActividadCultivo.ReDistribucionCuadrantes;
                    _cultivo.FechaActividad = dtp_FechaAsignacionCuadrante.SelectedDate;
                    _cultivo.Descripcion = "Limpiar Distribución de cuadrantes en el " + ListCamaCultivo.FirstOrDefault().CultivoLadoNombre.ToUpper();
                    _cultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _cultivo.FechaRegistro = DateTime.Now;
                    _cultivo.Estado = 1;
                    client.MapaCultivoActividades_Grabar(_cultivo);

                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                    _cama.CultivoCamaID = new Guid(itemCama.Value);
                    _cama.Responsable = Guid.Empty;
                    _cama.FechaResponsable = dtp_FechaAsignacionCuadrante.SelectedDate;
                    _cama.FechaRegistro = DateTime.Now;
                    client.CultivoCama_RedistribuirResponsable(_cama, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                }
            }
            //VerMensaje("INFORMACIÓN", "info", "info", "Asignación de Resposnables Distribuido correctamente.");
            Utils.MessageBox(this, "", "Asignación de Resposnables Distribuido correctamente.", "info");
            RecuperarDatos();
        }

        protected void btn_InfActualizaBloque_Click(object sender, ImageClickEventArgs e)
        {
            LogicClient client = new LogicClient();
            var _validarExistentes = client.CultivoBloque_ObtenerPorAreaBloqueIDs((Guid)IdActividadesSecciones.CultivoAreaID, new Guid(cmb_InfBloque.SelectedValue));
            if (_validarExistentes.Count > 0)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar otro Bloque porque el seleccionado ya se encuentra utilizado");
                return;
            }
            SGF_Clasificador _tmpClasificador = new SGF_Clasificador();
            _tmpClasificador = client.Clasificador_ObtenerPorID(new Guid(cmb_InfBloque.SelectedValue));
            SGF_CultivoBloque _newCultivoBloque = new SGF_CultivoBloque();
            _newCultivoBloque.CultivoBloqueID = (Guid)IdActividadesSecciones.CultivoBloqueID;
            _newCultivoBloque.CultivoAreaID = (Guid)IdActividadesSecciones.CultivoAreaID;
            _newCultivoBloque.BloqueID = _tmpClasificador.ClasificadorID;
            _newCultivoBloque.Nombre = _tmpClasificador.Nombre;
            _newCultivoBloque.Orden = _tmpClasificador.Orden;
            _newCultivoBloque.UsuarioRegistro = Me.Usuario.NombreUsuario;
            client.CultivoBloque_ActualizarBloqueID(_newCultivoBloque, Utils.getIP(), Utils.getHostName(Utils.getIP()));
            VerMensaje("INFORMACIÓN", "info", "info", "Bloque Actualizado Correctamente");
            cmb_InfBloque.Enabled = false;
            btn_InfActualizaBloque.Visible = false;
            chk_InfActualizarBloque.Checked = false;
        }

        protected void chk_InfActualizarBloque_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_InfActualizarBloque.Checked)
            {
                btn_InfActualizaBloque.Visible = true;
                cmb_InfBloque.Enabled = true;
            }
            else
            {
                btn_InfActualizaBloque.Visible = false;
                cmb_InfBloque.Enabled = false;
            }
        }

        protected void dl_InfResponsable_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_ID = (HiddenField)e.Item.FindControl("hdn_ID");
            RadTextBox txt_InfNombreResponsable = (RadTextBox)e.Item.FindControl("txt_InfNombreResponsable");
            RadTextBox txt_InfCamasResponsable = (RadTextBox)e.Item.FindControl("txt_InfCamasResponsable");
            #endregion
            #region Asignacion de valores en controles
            SP_ActividadesMapaCultivo_BuscarResponsablePorCama_Result item = (SP_ActividadesMapaCultivo_BuscarResponsablePorCama_Result)e.Item.DataItem;
            hdn_ID.Value = item.Id.ToString();
            txt_InfNombreResponsable.Text = item.Nombre;
            txt_InfCamasResponsable.Text = item.Camas;
            txt_InfNombreResponsable.BackColor = GetColor(item.Id, "1");
            txt_InfCamasResponsable.BackColor = GetColor(item.Id, "1");
            #endregion
        }
        protected Color GetColor(Guid id, string estado)
        {
            switch (estado)
            {
                case "1":
                    if (id == Guid.Empty)
                        return Color.White;
                    else
                    {
                        if (id == (Guid)ActividadCultivo.PatronLibre)
                            return Color.Orange;
                        else
                            return Color.LightGreen;
                    }
                case "0":
                    return Color.Gray;
                default:
                    return Color.White; // Color por defecto
            }
        }

        protected void dl_InfVariedadSembrada_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_ID = (HiddenField)e.Item.FindControl("hdn_ID");
            RadTextBox txt_InfNombreVariedad = (RadTextBox)e.Item.FindControl("txt_InfNombreVariedad");
            RadTextBox txt_InfCamasVariedad = (RadTextBox)e.Item.FindControl("txt_InfCamasVariedad");
            #endregion
            #region Asignacion de valores en controles
            SP_ActividadesMapaCultivo_BuscarVariedadCama_Result item = (SP_ActividadesMapaCultivo_BuscarVariedadCama_Result)e.Item.DataItem;
            hdn_ID.Value = item.Id.ToString();
            txt_InfNombreVariedad.Text = item.Nombre;
            txt_InfCamasVariedad.Text = item.Camas;
            txt_InfNombreVariedad.BackColor = GetColor(item.Id, "1");
            txt_InfCamasVariedad.BackColor = GetColor(item.Id, "1");
            #endregion
        }

        protected void btn_Deshabilitar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Habilitar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_CierreSiembra_Click(object sender, EventArgs e)
        {
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemCama in rlb_ItemsActivos.Items)
            {
                if (itemCama.Checked == true)
                {
                    SGF_ActividadesMapaCultivo _cultivo = new SGF_ActividadesMapaCultivo();
                    _cultivo.ActividadesMapaCultivoID = Guid.NewGuid();
                    _cultivo.CampoCultivoID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CampoCultivoID;
                    _cultivo.CultivoAreaID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoAreaID;
                    _cultivo.CultivoBloqueID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoBloqueID;
                    _cultivo.CultivoLadoID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoLadoID;
                    _cultivo.CultivoNaveID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoNaveID;
                    _cultivo.CultivoCamaID = ListCamaCultivoSembradas.First(x => x.CultivoCamaID == new Guid(itemCama.Value)).CultivoCamaID;
                    _cultivo.CultivoCuadroID = Guid.Empty;
                    _cultivo.ActividadID = ActividadCultivo.CierreSiembra;
                    _cultivo.FechaActividad = dtp_FechaSiembra.SelectedDate;
                    _cultivo.Descripcion = "Cierre de Siembra en " + ListCamaCultivoSembradas.FirstOrDefault().CultivoLadoNombre.ToUpper();
                    _cultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _cultivo.FechaRegistro = DateTime.Now;
                    _cultivo.Estado = 1;
                    //bool _grabar = client.MapaCultivoActividades_ValidarSiembraCama(2, (Guid)_cultivo.CultivoCamaID);
                    //if (_grabar == true)
                    client.MapaCultivoActividades_Grabar(_cultivo);

                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                    _cama.CultivoCamaID = new Guid(itemCama.Value);
                    _cama.VariedadSembrada = Guid.Empty;
                    _cama.FechaSiembra = dtp_FechaSiembra.SelectedDate;
                    _cama.FechaInjerto =  dtp_FechaSiembra.SelectedDate;
                    _cama.Color = "";
                    _cama.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    client.CultivoCama_LimpiarVariedad(_cama, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                }
            }
            VerMensaje("INFORMACIÓN", "info", "info", "Registro de Injerto realizado correctamente.");

            //Utils.MessageBox(this, "", "Registro de Siembra realizado correctamente", "info");
            RecuperarDatos();
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "cierre", "returnToParent();", true);
        }
    }
}