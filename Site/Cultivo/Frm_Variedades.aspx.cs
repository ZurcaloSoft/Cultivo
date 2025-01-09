using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Cultivo
{
    public partial class Frm_Variedades : System.Web.UI.Page
    {
        #region Variables y Propiedades
        protected List<SGF_Clasificador> ListClasificadorTemporal
        {
            get
            {
                if (ViewState["ListClasificadorTemporal"] == null)
                    ViewState["ListClasificadorTemporal"] = new List<SGF_Clasificador>();
                return (List<SGF_Clasificador>)ViewState["ListClasificadorTemporal"];
            }
            set
            {
                ViewState["ListClasificadorTemporal"] = value;
            }
        }
        protected List<SGF_Modulo> ListModulosTemporal
        {
            get
            {
                if (ViewState["ListModulosTemporal"] == null)
                    ViewState["ListModulosTemporal"] = new List<SGF_Modulo>();
                return (List<SGF_Modulo>)ViewState["ListModulosTemporal"];
            }
            set
            {
                ViewState["ListModulosTemporal"] = value;
            }
        }

        protected List<SGF_Formulario> ListFormularios
        {
            get
            {
                if (ViewState["ListFormularios"] == null)
                    ViewState["ListFormularios"] = new List<SGF_Formulario>();
                return (List<SGF_Formulario>)ViewState["ListFormularios"];
            }
            set
            {
                ViewState["ListFormularios"] = value;
            }
        }
        protected List<SGF_Formulario> ListFormulariosTemporal
        {
            get
            {
                if (ViewState["ListFormulariosTemporal"] == null)
                    ViewState["ListFormulariosTemporal"] = new List<SGF_Formulario>();
                return (List<SGF_Formulario>)ViewState["ListFormulariosTemporal"];
            }
            set
            {
                ViewState["ListFormulariosTemporal"] = value;
            }
        }

        protected List<SGF_Boton> ListBotones
        {
            get
            {
                if (ViewState["ListBotones"] == null)
                    ViewState["ListBotones"] = new List<SGF_Boton>();
                return (List<SGF_Boton>)ViewState["ListBotones"];
            }
            set
            {
                ViewState["ListBotones"] = value;
            }
        }
        protected List<SGF_Boton> ListBotonesTemporal
        {
            get
            {
                if (ViewState["ListBotonesTemporal"] == null)
                    ViewState["ListBotonesTemporal"] = new List<SGF_Boton>();
                return (List<SGF_Boton>)ViewState["ListBotonesTemporal"];
            }
            set
            {
                ViewState["ListBotonesTemporal"] = value;
            }
        }

        protected List<SEG_Formularios_VTA> ListPermisoFormulariosVTA
        {
            get
            {
                if (ViewState["ListPermisoFormulariosVTA"] == null)
                    ViewState["ListPermisoFormulariosVTA"] = new List<SEG_Formularios_VTA>();
                return (List<SEG_Formularios_VTA>)ViewState["ListPermisoFormulariosVTA"];
            }
            set
            {
                ViewState["ListPermisoFormulariosVTA"] = value;
            }
        }
        protected List<SEG_Botones_VTA> ListPermisoBotonesVTA
        {
            get
            {
                if (ViewState["ListPermisoBotonesVTA"] == null)
                    ViewState["ListPermisoBotonesVTA"] = new List<SEG_Botones_VTA>();
                return (List<SEG_Botones_VTA>)ViewState["ListPermisoBotonesVTA"];
            }
            set
            {
                ViewState["ListPermisoBotonesVTA"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                cargarCombos();
                ConsultarVariedad();
                gv_Variedad.DataBind();
            }
        }
        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();
            ListClasificadorTemporal = client.Clasificador_ObtenerTodo();
            //cmb_Color.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Color);
            cmb_Color.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.Color);
            cmb_Color.DataTextField = "Nombre";
            cmb_Color.DataValueField = "ClasificadorID";
            cmb_Color.DataBind();
            cmb_Color.Items.Insert(0, new RadComboBoxItem("Seleccione el Color", Guid.Empty.ToString()));

            //cmb_Calidad.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.CalidadFlor);
            cmb_Calidad.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CalidadFlor);
            cmb_Calidad.DataTextField = "Nombre";
            cmb_Calidad.DataValueField = "ClasificadorID";
            cmb_Calidad.DataBind();
            cmb_Calidad.Items.Insert(0, new RadComboBoxItem("Seleccione la Calidad", Guid.Empty.ToString()));

            //cmb_TipoFlor.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoFlor);
            cmb_TipoFlor.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.TipoFlor);
            cmb_TipoFlor.DataTextField = "Nombre";
            cmb_TipoFlor.DataValueField = "ClasificadorID";
            cmb_TipoFlor.DataBind();
            cmb_TipoFlor.Items.Insert(0, new RadComboBoxItem("Seleccione el Tipo de Flor", Guid.Empty.ToString()));

            //cmb_LongitudMinima.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Longitudes);
            cmb_LongitudMinima.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.Longitudes);
            cmb_LongitudMinima.DataTextField = "Nombre";
            cmb_LongitudMinima.DataValueField = "ClasificadorID";
            cmb_LongitudMinima.DataBind();
            cmb_LongitudMinima.Items.Insert(0, new RadComboBoxItem("Seleccione Longitud del Bonche", Guid.Empty.ToString()));

            //cmb_LongitudMaxima.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Longitudes);
            cmb_LongitudMaxima.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.Longitudes);
            cmb_LongitudMaxima.DataTextField = "Nombre";
            cmb_LongitudMaxima.DataValueField = "ClasificadorID";
            cmb_LongitudMaxima.DataBind();
            cmb_LongitudMaxima.Items.Insert(0, new RadComboBoxItem("Seleccione Longitud de Malla", Guid.Empty.ToString()));

            //cmb_NroTalloPorMalla.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TallosPorMalla);
            cmb_NroTalloPorMalla.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.TallosPorMalla);
            cmb_NroTalloPorMalla.DataTextField = "Nombre";
            cmb_NroTalloPorMalla.DataValueField = "ClasificadorID";
            cmb_NroTalloPorMalla.DataBind();
            cmb_NroTalloPorMalla.Items.Insert(0, new RadComboBoxItem("Seleccione Nro. Tallos por Malla", Guid.Empty.ToString()));

            //rlb_TipoCalidad.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoCalidad);
            rlb_TipoCalidad.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.TipoCalidad);
            rlb_TipoCalidad.DataTextField = "Nombre";
            rlb_TipoCalidad.DataValueField = "ClasificadorID";
            rlb_TipoCalidad.DataBind();

            //rlb_Longitud.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Longitudes);
            rlb_Longitud.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.Longitudes);
            rlb_Longitud.DataTextField = "Nombre";
            rlb_Longitud.DataValueField = "ClasificadorID";
            rlb_Longitud.DataBind();

            //rlb_Tallos.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TallosPorBonche);
            rlb_Tallos.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.TallosPorBonche);
            rlb_Tallos.DataTextField = "Nombre";
            rlb_Tallos.DataValueField = "ClasificadorID";
            rlb_Tallos.DataBind();

            //rlb_Mercado.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Mercado);
            rlb_Mercado.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.Mercado);
            rlb_Mercado.DataTextField = "Nombre";
            rlb_Mercado.DataValueField = "ClasificadorID";
            rlb_Mercado.DataBind();

            //rlb_TallosPorMalla.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TallosPorMalla);
            rlb_TallosPorMalla.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.TallosPorMalla).OrderBy(x => x.Orden);
            rlb_TallosPorMalla.DataTextField = "Nombre";
            rlb_TallosPorMalla.DataValueField = "ClasificadorID";
            rlb_TallosPorMalla.DataBind();

            //rlb_Pais.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Pais);
            //rlb_Pais.DataTextField = "Nombre";
            //rlb_Pais.DataValueField = "ClasificadorID";
            //rlb_Pais.DataBind();

            //ListModulosTemporal = client.Modulo_ObtenerTodo().ToList();

            //foreach (SGF_Modulo item in ListModulosTemporal)
            //{
            //    item.Estado = 0;
            //    // do your stuff with the item
            //}
            //ListFormulariosTemporal = client.Formulario_ObtenerTodo().ToList();
            //foreach (SGF_Formulario item in ListFormulariosTemporal)
            //{
            //    item.Estado = 0;
            //    // do your stuff with the item
            //}
            //ListBotonesTemporal = client.Boton_ObtenerTodo().ToList();
            //foreach (SGF_Boton item in ListBotonesTemporal)
            //{
            //    item.Estado = 0;
            //    // do your stuff with the item
            //}
        }

        protected void btn_RetornoVentana_Click(object sender, ImageClickEventArgs e)
        {
            if (hdn_PersonaID.Value == "") return;
            txt_ObtentorCedula.Text = hdn_PersonaIdentificacion.Value;
            txt_ObtentorNombre.Text = hdn_PersonaNombre.Value;
        }

        protected void chk_CrearProducto_Click(object sender, EventArgs e)
        {
            if (chk_CrearProducto.Checked == true)
            {
                pnl_DatosProducto.Visible = true;
                pnl_ParametrosAdicionales.Visible = false;
                btn_GenerarProducto.Visible = true;
                //   chk_Mercado.Visible = true;
            }
            else
            {
                pnl_DatosProducto.Visible = false;
                pnl_ParametrosAdicionales.Visible = false;
                btn_GenerarProducto.Visible = false;
                //                chk_Mercado.Visible = false;
                //                chk_Pais.Visible = false;

            }
        }

        protected void chk_Mercado_Click(object sender, EventArgs e)
        {
            if (chk_Mercado.Checked == true)
            {
                //chk_Pais.Visible = true;
                //rlb_Mercado.Visible = true;
                //td_Mercado.Visible = true;
                //td_Mercado1.Visible = true;
                pnl_ParametrosAdicionales.Visible = true;
            }
            else
            {
                //chk_Pais.Visible = false;
                rlb_Mercado.Visible = false;
                chk_Pais.Checked = false;
                //td_Mercado.Visible = false;
                //td_Mercado1.Visible = false;
                //td_Pais.Visible = false;
                //td_Pais1.Visible = false;
                pnl_ParametrosAdicionales.Visible = false;

            }
        }

        protected void chk_Pais_Click(object sender, EventArgs e)
        {
            //if (chk_Pais.Checked == true)
            //{
            //    td_Pais.Visible = true;
            //    td_Pais1.Visible = true;
            //    rlb_Pais.Visible = true;
            //}
            //else
            //{
            //    //  chk_Pais.Visible = false;
            //    td_Pais.Visible = false;
            //    td_Pais1.Visible = false;
            //}
        }

        protected string ObtenerNombreEstado(Int32 Estado)
        {
            switch (Estado)
            {
                case 0:
                    return "Pasivo";
                case 1:
                    return "Activo";
                case 2:
                    return "Pasivo";
            }
            return "";
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Buscador.Visible = false;
            pnl_DatosVariedad.Visible = true;
            chk_CrearProducto.Visible = false;
            LimpiarControles();
            hdn_VariedadID.Value = Guid.Empty.ToString();
        }

        protected void LimpiarControles()
        {
            hdn_PersonaID.Value = null;
            hdn_PersonaIdentificacion.Value = null;
            hdn_PersonaNombre.Value = null;
            hdn_VariedadID.Value = null;
            gv_Variedad.DataSource = null;
            //rlb_TipoCalidad.DataSource = List ListModulosTemporal;
            //rlb_Formulario.DataSource = null;
            //rlb_Modulo.DataBind();
            //rlb_Formulario.DataBind();
            //ListPermisoBotonesVTA = null;
            //ListPermisoFormulariosVTA = null;

            txt_Codigo.Text = "";
            txt_NombreVariedad.Text = "";
            txt_ObtentorCedula.Text = "";
            txt_ObtentorNombre.Text = "";
            cmb_Color.SelectedValue = Guid.Empty.ToString();
            cmb_Calidad.SelectedValue = Guid.Empty.ToString();
            cmb_TipoFlor.SelectedValue = Guid.Empty.ToString();
            cmb_LongitudMinima.SelectedValue = Guid.Empty.ToString();
            cmb_LongitudMaxima.SelectedValue = Guid.Empty.ToString();
            cmb_NroTalloPorMalla.SelectedValue = Guid.Empty.ToString();

            txt_Rotacion.Value = 0;
            txt_IndiceProdMensual.Value = 0;
            txt_TamanoBoton.Value = 0;
            txt_NroPetalos.Value = 0;
            txt_Ciclo.Value = 0;
            txt_DiasFlorero.Value = 0;
            chk_CrearProducto.Checked = false;
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";
            gv_Producto.DataSource = null;
            rlb_TallosPorMalla.DataSource = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.TallosPorMalla).OrderBy(x => x.Orden);
            rlb_TallosPorMalla.DataTextField = "Nombre";
            rlb_TallosPorMalla.DataValueField = "ClasificadorID";
            rlb_TallosPorMalla.DataBind();
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            ConsultarVariedad();
            gv_Variedad.DataBind();
        }

        private void ConsultarVariedad()
        {
            LogicClient client = new LogicClient();
            var _variedad = client.Variedad_ObtenerPorCodNameVTA(txt_BuscarCodigo.Text, txt_BuscarNombre.Text);
            gv_Variedad.DataSource = _variedad;
        }

        protected void tool_principal_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Grabar":
                    Grabar();
                    break;
                case "Cancelar":
                    Cancelar();
                    break;
            }
        }

        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_DatosVariedad.Visible = false;
        }

        protected void CargarDatos()
        {
            foreach (SGF_Modulo item in ListModulosTemporal)
                item.Estado = 0;
            foreach (SGF_Formulario item in ListFormulariosTemporal)
                item.Estado = 0;
            foreach (SGF_Boton item in ListBotonesTemporal)
                item.Estado = 0;
            if (new Guid(hdn_VariedadID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_Variedad _variedad = client.Variedad_ObtenerPorID(new Guid(hdn_VariedadID.Value));
            if (_variedad != null)
            {
                txt_Codigo.Text = _variedad.Codigo;
                txt_NombreVariedad.Text = _variedad.Nombre;
                SGF_Persona_VTA _obtentor = client.Persona_ObtenerPorID_VTA(_variedad.ObtentorID.ToString());
                txt_ObtentorCedula.Text = _obtentor == null ? "" : _obtentor.Identificacion;
                txt_ObtentorNombre.Text = _obtentor == null ? "" : _obtentor.NombrePersona;
                cmb_Color.SelectedValue = _variedad.ColorID.ToString();
                cmb_Calidad.SelectedValue = _variedad.CalidadID.ToString();
                cmb_TipoFlor.SelectedValue = _variedad.TipoFlorID.ToString();
                cmb_LongitudMinima.SelectedValue = _variedad.LongitudIDMin.ToString();
                cmb_LongitudMaxima.SelectedValue = _variedad.LongitudIDMax.ToString();
                cmb_NroTalloPorMalla.SelectedValue = _variedad.TallosPorMalla.ToString();
                txt_Rotacion.Value = _variedad.Rotacion;
                txt_IndiceProdMensual.Value = (double)_variedad.IndProdMensual;
                txt_TamanoBoton.Value = (double)_variedad.TamanoBoton;
                txt_NroPetalos.Value = _variedad.NumeroPetalos;
                txt_Ciclo.Value = _variedad.Ciclo;
                txt_DiasFlorero.Value = _variedad.DiasFlorero;
                //chk_CrearProducto.Checked = false;
                txt_Observaciones.Text = _variedad.Observaciones;
                txt_Estado.Text = ObtenerNombreEstado((int)_variedad.Estado);

                var _productsVTA = client.Producto_ObtenerPorVariedadVTA(new Guid(hdn_VariedadID.Value));
                gv_Producto.DataSource = _productsVTA;
                gv_Producto.DataBind();

                var _talloXmalla = client.Variedad_TallosPorMallaPorVariedad_VTA(new Guid(hdn_VariedadID.Value));
                int i = 0;
                foreach (SGF_VariedadTallosPorMalla_VTA _tallos in _talloXmalla)
                {
                    if (_tallos.Estado == 1)
                    {
                        foreach (RadListBoxItem itemTallos in rlb_TallosPorMalla.Items)
                        {
                            if (new Guid(itemTallos.Value) == _tallos.TallosPorMallaID)
                            {
                                itemTallos.Checked = true;
                                if (_tallos.EsPrincipal == true && i == 0)
                                {
                                    itemTallos.Selected = true;
                                    i = 1 + 1;
                                }
                            }
                        }
                    }
                }

            }
        }

        private void Grabar()
        {
            #region Validaciones
            if (txt_Codigo.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Código de la Variedad");
                return;
            }
            if (txt_NombreVariedad.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre de la Variedad");
                return;
            }
            if (cmb_Color.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Color");
                return;
            }
            if (cmb_Calidad.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Calidad");
                return;
            }
            if (cmb_TipoFlor.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Tipo de Flor");
                return;
            }
            if (cmb_NroTalloPorMalla.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Nro. de Tallos por Malla");
                return;
            }
            if (cmb_LongitudMinima.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Longitud Mínima");
                return;
            }
            if (cmb_LongitudMaxima.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Longitud Máxima");
                return;
            }
            //if (txt_Rotacion.Value == 0)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar la Rotación");
            //    return;
            //}
            if (txt_IndiceProdMensual.Value == 0)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Índice de Promedio Mensual");
                return;
            }
            //if (txt_TamanoBoton.Value == 0)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Tamaño de Botón");
            //    return;
            //}
            //if (txt_Ciclo.Value == 0)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Ciclo");
            //    return;
            //}
            //if (txt_DiasFlorero.Value == 0)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar los Días de Florero");
            //    return;
            //}
            #endregion
            SGF_Variedad _variedad = new SGF_Variedad();
            _variedad.VariedadID = new Guid(hdn_VariedadID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_VariedadID.Value);
            _variedad.Codigo = txt_Codigo.Text;
            _variedad.Nombre = txt_NombreVariedad.Text;
            _variedad.ObtentorID = txt_ObtentorCedula.Text == "" ? Guid.Empty : new Guid(hdn_PersonaID.Value);
            _variedad.ObtentorNombre = txt_ObtentorCedula.Text == "" ? "" : txt_ObtentorCedula + " - " + txt_ObtentorNombre;
            _variedad.ColorID = new Guid(cmb_Color.SelectedValue.ToString());
            _variedad.CalidadID = new Guid(cmb_Calidad.SelectedValue.ToString());
            _variedad.TipoFlorID = new Guid(cmb_TipoFlor.SelectedValue.ToString());
            _variedad.LongitudIDMin = new Guid(cmb_LongitudMinima.SelectedValue.ToString());
            _variedad.LongitudIDMax = new Guid(cmb_LongitudMaxima.SelectedValue.ToString());
            _variedad.TallosPorMalla = new Guid(cmb_NroTalloPorMalla.SelectedValue.ToString());
            _variedad.Rotacion = (int)txt_Rotacion.Value;
            _variedad.IndProdMensual = (decimal)txt_IndiceProdMensual.Value;
            _variedad.TamanoBoton = (decimal)txt_TamanoBoton.Value;
            _variedad.NumeroPetalos = (int)txt_NroPetalos.Value;
            _variedad.Ciclo = (int)txt_Ciclo.Value;
            _variedad.DiasFlorero = (int)txt_DiasFlorero.Value;
            _variedad.Observaciones = txt_Observaciones.Text;
            _variedad.Estado = 1;
            _variedad.Fecha = DateTime.Now;
            _variedad.Usuario = Me.Usuario.NombreUsuario;
            _variedad.EmpresaID = Me.Usuario.EmpresaID;
            LogicClient client = new LogicClient();
            client.Variedad_Grabar(_variedad, Utils.getIP(), Utils.getHostName(Utils.getIP()));
            var _TallosXMalla = RecogerDatosListBoxTallosXMalla();
            foreach (SGF_VariedadTallosPorMalla item in _TallosXMalla)
            {
                client.Variedad_TallosPorMallaGrabar(item, Utils.getIP(), Utils.getHostName(Utils.getIP()));
            }
            VerMensaje("INFORMACIÓN", "info", "info", "Datos de Variedad Registrados correctamente");
            Cancelar();
        }

        protected void gv_Variedad_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ConsultarVariedad();
        }

        protected void gv_Variedad_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_VariedadID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_DatosVariedad.Visible = true;
                    chk_CrearProducto.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        private void VerMensaje(string title, string titIcon, string icon, string mensaje)
        {
            RadNotification1.Title = title;// "Friendship invitation";
            RadNotification1.TitleIcon = titIcon;// "none";//"info"
            RadNotification1.ContentIcon = icon;//"warning";//"info"
            RadNotification1.Text = mensaje;
            RadNotification1.Show();

        }

        protected void rlb_Mercado_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadListBox radListBox = sender as RadListBox;
            if (radListBox != null && radListBox.SelectedItem != null)
            {
                string selectedText = radListBox.SelectedItem.Text;
                string selectedValue = radListBox.SelectedItem.Value;
                LogicClient client = new LogicClient();
                //                rlb_Formulario.DataSource = client.Formulario_ObtenerPorModuloID(new Guid(selectedValue));
                rlb_Pais.DataSource = ListClasificadorTemporal.Where(x => x.ParentID == new Guid(selectedValue));
                rlb_Pais.DataTextField = "Nombre";
                rlb_Pais.DataValueField = "ClasificadorID";
                rlb_Pais.DataBind();
                //cargarCheck(Ctl_Formularios, new Guid(selectedValue));
                //recorrerListBox(rlb_Formulario, 2);

                //rlb_Pais.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Pais);
                //rlb_Pais.DataTextField = "Nombre";
                //rlb_Pais.DataValueField = "ClasificadorID";
                //rlb_Pais.DataBind();
            }
            else
            {
                // LabelSelectedItem.Text = "Selected Item: None";
            }
        }

        protected void btn_GenerarProducto_Click(object sender, EventArgs e)
        {
            LogicClient client = new LogicClient();
            List<CO_ARTICULO> _productos = new List<CO_ARTICULO>();
            #region validar 
            string _res = ValidarListBox(rlb_Tallos);
            if (_res != string.Empty)
            {
                VerMensaje("INFORMACIÓN", "info", "info", _res);
                return;
            }
            _res = ValidarListBox(rlb_Longitud);
            if (_res != string.Empty)
            {
                VerMensaje("INFORMACIÓN", "info", "info", _res);
                return;
            }
            _res = ValidarListBox(rlb_TipoCalidad);
            if (_res != string.Empty)
            {
                VerMensaje("INFORMACIÓN", "info", "info", _res);
                return;
            }

            _productos = RecogerDatosListBox();

            if (_productos.Count > 0)
            {
                foreach (CO_ARTICULO _itemProducto in _productos)
                {
                    client.Producto_Grabar(_itemProducto, Utils.getHostName(Utils.getIP()), Utils.getIP());
                    var _productsVTA = client.Producto_ObtenerPorVariedadVTA(new Guid(hdn_VariedadID.Value));
                    gv_Producto.DataSource = _productsVTA;
                    gv_Producto.DataBind();
                }
                VerMensaje("INFORMACIÓN", "info", "info", "Productos creados: " + _productos.Count.ToString());

            }
            #endregion
        }
        private string ValidarListBox(RadListBox _list)
        {
            string respuesta = "No tiene seleccionado ningún item en el listado de " + _list.ToolTip;
            foreach (RadListBoxItem item in _list.Items)
            {
                if (item.Checked == true)
                {
                    respuesta = "";
                    break;
                }
            }
            return respuesta;
        }

        private List<CO_ARTICULO> RecogerDatosListBox()
        {
            string _Mercado = ValidarListBox(rlb_Mercado);
            string _Paises = ValidarListBox(rlb_Pais);
            List<CO_ARTICULO> _productos = new List<CO_ARTICULO>();
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemCalidad in rlb_TipoCalidad.Items)
            {
                if (itemCalidad.Checked == true)
                {
                    foreach (RadListBoxItem itemLongitud in rlb_Longitud.Items)
                    {
                        if (itemLongitud.Checked == true)
                        {
                            foreach (RadListBoxItem itemTallos in rlb_Tallos.Items)
                            {
                                if (itemTallos.Checked == true)
                                {
                                    CO_ARTICULO _newProducto = new CO_ARTICULO();
                                    if (_Mercado == string.Empty) //Tiene seleccionado items en el Mercado
                                    {
                                        foreach (RadListBoxItem itemMercado in rlb_Mercado.Items)
                                        {
                                            if (itemMercado.Checked == true)
                                            {
                                                if (_Paises == string.Empty) //Tiene seleccionado items en Paises
                                                {
                                                    foreach (RadListBoxItem itemPais in rlb_Pais.Items)
                                                    {
                                                        if (itemPais.Checked == true)
                                                        {
                                                            var _clCalidad = client.Clasificador_ObtenerPorID(new Guid(itemCalidad.Value));
                                                            var _clLongitud = client.Clasificador_ObtenerPorID(new Guid(itemLongitud.Value));
                                                            var _clTallo = client.Clasificador_ObtenerPorID(new Guid(itemTallos.Value));
                                                            var _clMercado = client.Clasificador_ObtenerPorID(new Guid(itemMercado.Value));
                                                            var _clPais = client.Clasificador_ObtenerPorID(new Guid(itemPais.Value));
                                                            var _clIva = client.Parametros_ObtenerPorPkey("PorcentajeIVA", Me.Usuario.EmpresaID);
                                                            _newProducto = new CO_ARTICULO();
                                                            _newProducto.CO_EMP_RUC = Me.Usuario.EmpresaID;
                                                            _newProducto.CO_FAM_ART_COD = new Guid(itemLongitud.Value);
                                                            _newProducto.CO_VARIEDAD_COD = new Guid(hdn_VariedadID.Value);
                                                            _newProducto.CO_ART_COD = _clCalidad.Valor.Trim() + "." + txt_Codigo.Text.Trim() + "." + _clLongitud.Valor.Trim() + "." + _clTallo.Valor.Trim() + "." + _clMercado.Valor.Trim() + "." + _clPais.Valor.Trim();
                                                            _newProducto.CO_ART_NOM = txt_NombreVariedad.Text + ": " + _newProducto.CO_ART_COD;
                                                            _newProducto.CO_TIP_ART_COD = new Guid(itemCalidad.Value);
                                                            _newProducto.CO_CATEGORIA4_COD = new Guid(itemMercado.Value);
                                                            _newProducto.CO_CATEGORIA3_COD = new Guid(itemTallos.Value);
                                                            _newProducto.CO_PAIS_ORIGEN_COD = new Guid(itemPais.Value); ;
                                                            _newProducto.DESCRIPCION = "";
                                                            _newProducto.CO_ART_PRE_COS = 0; //ValorRefCompra
                                                            _newProducto.CO_ART_PES = 0;  //ValorRefVenta
                                                            _newProducto.CO_ART_USU_ALI = Me.Usuario.NombreUsuario;
                                                            _newProducto.PRESENTACION = "";
                                                            _newProducto.FECHA_CREACION = DateTime.Now;
                                                            _newProducto.CO_ART_EST = "ACTIVO";
                                                            _newProducto.CO_ART_POR_IVA = double.Parse(_clIva.Valor);
                                                            _productos.Add(_newProducto);
                                                        }
                                                    }
                                                }
                                                else //No Tiene seleccionado items en Paises
                                                {
                                                    var _clCalidad = client.Clasificador_ObtenerPorID(new Guid(itemCalidad.Value));
                                                    var _clLongitud = client.Clasificador_ObtenerPorID(new Guid(itemLongitud.Value));
                                                    var _clTallo = client.Clasificador_ObtenerPorID(new Guid(itemTallos.Value));
                                                    var _clMercado = client.Clasificador_ObtenerPorID(new Guid(itemMercado.Value));
                                                    var _clIva = client.Parametros_ObtenerPorPkey("PorcentajeIVA", Me.Usuario.EmpresaID);
                                                    _newProducto = new CO_ARTICULO();
                                                    _newProducto.CO_EMP_RUC = Me.Usuario.EmpresaID;
                                                    _newProducto.CO_FAM_ART_COD = new Guid(itemLongitud.Value);
                                                    _newProducto.CO_VARIEDAD_COD = new Guid(hdn_VariedadID.Value);
                                                    _newProducto.CO_ART_COD = _clCalidad.Valor.Trim() + "." + txt_Codigo.Text.Trim() + "." + _clLongitud.Valor.Trim() + "." + _clTallo.Valor.Trim() + "." + _clMercado.Valor.Trim();
                                                    _newProducto.CO_ART_NOM = txt_NombreVariedad.Text + ": " + _newProducto.CO_ART_COD;
                                                    _newProducto.CO_TIP_ART_COD = new Guid(itemCalidad.Value);
                                                    _newProducto.CO_CATEGORIA4_COD = new Guid(itemMercado.Value);
                                                    _newProducto.CO_CATEGORIA3_COD = new Guid(itemTallos.Value);
                                                    _newProducto.CO_ART_USU_ALI = Me.Usuario.NombreUsuario;
                                                    _newProducto.CO_PAIS_ORIGEN_COD = Guid.Empty;
                                                    _newProducto.DESCRIPCION = "";
                                                    _newProducto.CO_ART_PRE_COS = 0; //ValorRefCompra
                                                    _newProducto.CO_ART_PES = 0;  //ValorRefVenta
                                                    _newProducto.PRESENTACION = "";
                                                    _newProducto.FECHA_CREACION = DateTime.Now;
                                                    _newProducto.CO_ART_EST = "ACTIVO";
                                                    _newProducto.CO_ART_POR_IVA = double.Parse(_clIva.Valor);
                                                    _productos.Add(_newProducto);
                                                }
                                            }
                                        }
                                    }
                                    else //No tiene seleccionado items en el Mercado
                                    {
                                        var _clCalidad = client.Clasificador_ObtenerPorID(new Guid(itemCalidad.Value));
                                        var _clLongitud = client.Clasificador_ObtenerPorID(new Guid(itemLongitud.Value));
                                        var _clTallo = client.Clasificador_ObtenerPorID(new Guid(itemTallos.Value));
                                        var _clIva = client.Parametros_ObtenerPorPkey("PorcentajeIVA", Me.Usuario.EmpresaID);
                                        _newProducto = new CO_ARTICULO();
                                        _newProducto.CO_EMP_RUC = Me.Usuario.EmpresaID;
                                        _newProducto.CO_FAM_ART_COD = new Guid(itemLongitud.Value);
                                        _newProducto.CO_VARIEDAD_COD = new Guid(hdn_VariedadID.Value);
                                        _newProducto.CO_ART_COD = _clCalidad.Valor.Trim() + "." + txt_Codigo.Text.Trim() + "." + _clLongitud.Valor.Trim() + "." + _clTallo.Valor.Trim();
                                        _newProducto.CO_ART_NOM = txt_NombreVariedad.Text + ": " + _newProducto.CO_ART_COD;
                                        _newProducto.CO_TIP_ART_COD = new Guid(itemCalidad.Value);
                                        _newProducto.CO_CATEGORIA4_COD = Guid.Empty;
                                        _newProducto.CO_CATEGORIA3_COD = new Guid(itemTallos.Value);
                                        _newProducto.CO_ART_USU_ALI = Me.Usuario.NombreUsuario;
                                        _newProducto.CO_PAIS_ORIGEN_COD = Guid.Empty;
                                        _newProducto.DESCRIPCION = "";
                                        _newProducto.CO_ART_PRE_COS = 0; //ValorRefCompra
                                        _newProducto.CO_ART_PES = 0;  //ValorRefVenta
                                        _newProducto.PRESENTACION = Me.Usuario.NombreUsuario;
                                        _newProducto.FECHA_CREACION = DateTime.Now;
                                        _newProducto.CO_ART_EST = "ACTIVO";
                                        _newProducto.CO_ART_POR_IVA = double.Parse(_clIva.Valor);
                                        _productos.Add(_newProducto);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return _productos;
        }

        private List<SGF_VariedadTallosPorMalla> RecogerDatosListBoxTallosXMalla()
        {
            List<SGF_VariedadTallosPorMalla> VariedadTalloXMalla = new List<SGF_VariedadTallosPorMalla>();
            foreach (RadListBoxItem itemTallos in rlb_TallosPorMalla.Items)
            {
                SGF_VariedadTallosPorMalla _newVarTalloXMalla = new SGF_VariedadTallosPorMalla();
                _newVarTalloXMalla.VariedadTallosPorMallaID = Guid.NewGuid();
                _newVarTalloXMalla.VariedadID = new Guid(hdn_VariedadID.Value);
                _newVarTalloXMalla.TallosPorMallaID = new Guid(itemTallos.Value);
                _newVarTalloXMalla.Usuario = Me.Usuario.NombreUsuario;
                _newVarTalloXMalla.Fecha = DateTime.Now;
                if (itemTallos.Checked == true)
                    _newVarTalloXMalla.Estado = 1;
                else
                    _newVarTalloXMalla.Estado = 0;

                if (_newVarTalloXMalla.TallosPorMallaID == new Guid(cmb_NroTalloPorMalla.SelectedValue))
                {
                    _newVarTalloXMalla.EsPrincipal = true;
                    _newVarTalloXMalla.Estado = 1;
                }
                else
                    _newVarTalloXMalla.EsPrincipal = false;

                VariedadTalloXMalla.Add(_newVarTalloXMalla);
            }
            return VariedadTalloXMalla;
        }

        protected void gv_Producto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LogicClient client = new LogicClient();
            var _productsVTA = client.Producto_ObtenerPorVariedadVTA(new Guid(hdn_VariedadID.Value));
            gv_Producto.DataSource = _productsVTA;

        }

        protected void gv_Producto_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }
    }
}
