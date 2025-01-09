using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using static System.Net.Mime.MediaTypeNames;

namespace SGF.Site.Compras
{
    public partial class Frm_ProveedorVariedad : System.Web.UI.Page
    {
        public Int32 Numerador
        {
            get { return (Int32)ViewState["Numerador"]; }
            set { ViewState["Numerador"] = value; }
        }
        public bool EsNuevo
        {
            get { return (bool)ViewState["EsNuevo"]; }
            set { ViewState["EsNuevo"] = value; }
        }

        protected List<CO_PROVEEDOR> ListProveedorTemporal
        {
            get
            {
                if (ViewState["ListProveedorTemporal"] == null)
                    ViewState["ListProveedorTemporal"] = new List<CO_PROVEEDOR>();
                return (List<CO_PROVEEDOR>)ViewState["ListProveedorTemporal"];
            }
            set
            {
                ViewState["ListProveedorTemporal"] = value;
            }
        }
        protected List<SGF_Variedad> ListVariedadTemporal
        {
            get
            {
                if (ViewState["ListVariedadTemporal"] == null)
                    ViewState["ListVariedadTemporal"] = new List<SGF_Variedad>();
                return (List<SGF_Variedad>)ViewState["ListVariedadTemporal"];
            }
            set
            {
                ViewState["ListVariedadTemporal"] = value;
            }
        }

        protected List<SGF_ProveedorVariedad> ListProveedorVariedadGeneral
        {
            get
            {
                if (ViewState["ListProveedorVariedadGeneral"] == null)
                    ViewState["ListProveedorVariedadGeneral"] = new List<SGF_ProveedorVariedad>();
                return (List<SGF_ProveedorVariedad>)ViewState["ListProveedorVariedadGeneral"];
            }
            set
            {
                ViewState["ListProveedorVariedadGeneral"] = value;
            }
        }
        protected List<SGF_ProveedorVariedad> ListProveedorVariedadIndividual
        {
            get
            {
                if (ViewState["ListProveedorVariedadIndividual"] == null)
                    ViewState["ListProveedorVariedadIndividual"] = new List<SGF_ProveedorVariedad>();
                return (List<SGF_ProveedorVariedad>)ViewState["ListProveedorVariedadIndividual"];
            }
            set
            {
                ViewState["ListProveedorVariedadIndividual"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                Numerador = 0;
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Recepcion;
                cargarCombos();
                ConsultarListado();
                gv_Temporada.DataBind();
                EsNuevo = false;
                txt_EmpresaRUC.Text = Me.Usuario.EmpresaID;
                txt_EliminadoEmpresa.Text = Me.Usuario.EmpresaID;
            }
        }

        private void cargarCombos()
        {
            LogicClient client = new LogicClient();
            ListProveedorTemporal = client.Proveedor_ObtenerPorEmpresaID(Me.Usuario.EmpresaID).OrderBy(x => x.CO_PRO_NOM).ToList();
            cmb_BuscarProveedor.DataSource = ListProveedorTemporal;
            cmb_BuscarProveedor.DataTextField = "CO_PRO_NOM";
            cmb_BuscarProveedor.DataValueField = "CO_PRO_COD";
            cmb_BuscarProveedor.DataBind();
            cmb_BuscarProveedor.Items.Insert(0, new RadComboBoxItem("Seleccionar el Proveedor", "0"));

            ListVariedadTemporal = client.Variedad_ObtenerTodo().Where(x => x.Estado == 1).OrderBy(x => x.Nombre).ToList();
            cmb_BuscarVariedad.DataSource = ListVariedadTemporal;
            cmb_BuscarVariedad.DataTextField = "Nombre";
            cmb_BuscarVariedad.DataValueField = "VariedadID";
            cmb_BuscarVariedad.DataBind();
            cmb_BuscarVariedad.Items.Insert(0, new RadComboBoxItem("Seleccionar la Variedad", Guid.Empty.ToString()));

            Numerador = 0;
            rlb_Proveedor.DataSource = ListProveedorTemporal;
            rlb_Proveedor.DataTextField = "CO_PRO_NOM";
            rlb_Proveedor.DataValueField = "CO_PRO_COD";
            rlb_Proveedor.DataBind();

            dl_InfProVar.DataSource = ListProveedorTemporal;
            dl_InfProVar.DataBind();
            rlb_Variedad.DataSource = ListVariedadTemporal;
            rlb_Variedad.DataTextField = "Nombre";
            rlb_Variedad.DataValueField = "VariedadID";
            rlb_Variedad.DataBind();

            cmb_EliminadoProveedor.DataSource = ListProveedorTemporal;
            cmb_EliminadoProveedor.DataTextField = "CO_PRO_NOM";
            cmb_EliminadoProveedor.DataValueField = "CO_PRO_COD";
            cmb_EliminadoProveedor.DataBind();
            cmb_EliminadoProveedor.Items.Insert(0, new RadComboBoxItem("Seleccionar el Proveedor", "0"));

            cmb_EliminadoVariedad.DataSource = ListVariedadTemporal;
            cmb_EliminadoVariedad.DataTextField = "Nombre";
            cmb_EliminadoVariedad.DataValueField = "VariedadID";
            cmb_EliminadoVariedad.DataBind();
            cmb_EliminadoVariedad.Items.Insert(0, new RadComboBoxItem("Seleccionar la Variedad", Guid.Empty.ToString()));

        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Datos.Visible = false;
            pnl_Buscador.Visible = false;
            pnl_Variedad.Visible = true;
            LimpiarControles();
            EsNuevo = true;
            tool_principal.Items[0].Visible = true;
            tool_principal.Items[1].Visible = false;
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            ConsultarListado();
            gv_Temporada.DataBind();
            gv_TemporadaEliminado.DataBind();
        }

        private void ConsultarListado()
        {
            LogicClient client = new LogicClient();
            Numerador = 0;
            ListProveedorTemporal = client.Proveedor_ObtenerPorEmpresaID(Me.Usuario.EmpresaID).OrderBy(x => x.CO_PRO_NOM).ToList();
            var _resultado = client.ProveedorVariedad_ObtenerPorFiltro_VTA(cmb_BuscarProveedor.SelectedValue == "0" ? string.Empty : cmb_BuscarProveedor.SelectedValue, new Guid(cmb_BuscarVariedad.SelectedValue), Me.Usuario.EmpresaID);
            gv_Temporada.DataSource = _resultado == null ? null : _resultado.Count > 0 ? _resultado.Where(x => x.Estado == (int)Enums.Estado.Activo).ToList() : _resultado;
            //gv_TemporadaEliminado.DataSource = _resultado == null ? null : _resultado.Count > 0 ? _resultado.Where(x => x.Estado == (int)Enums.Estado.Pasivo).ToList() : _resultado;
            dl_InfProVar.DataSource = ListProveedorTemporal;
            dl_InfProVar.DataBind();
        }
        protected Int32 NumeradorColumna()
        {
            Int32 _num = 0;
            _num = Numerador + 1;
            Numerador = _num;
            return _num;
        }
        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Grabar":
                    Grabar();
                    ConsultarListado();
                    break;
                case "Eliminar":
                    ActivarEliminar((int)Enums.Estado.Pasivo);
                    ConsultarListado();
                    gv_Temporada.DataBind();
                    gv_TemporadaEliminado.DataBind();
                    Cancelar();
                    break;
                case "Cancelar":
                    Cancelar();
                    break;
            }
        }
        private void ActivarEliminar(int estado)
        {
            LogicClient client = new LogicClient();
            SGF_ProveedorVariedad _proveedorVariedad = new SGF_ProveedorVariedad();
            _proveedorVariedad.ProveedorVariedadID = String.IsNullOrEmpty(hdn_ProveedorVariedadID.Value) ? Guid.NewGuid() : new Guid(hdn_ProveedorVariedadID.Value);
            _proveedorVariedad.EmpresaID = Me.Usuario.EmpresaID;
            //_proveedorVariedad.ProveedorID = cmb_Proveedor.SelectedValue;
            //_proveedorVariedad.VariedadID = new Guid(cmb_Variedad.SelectedValue);
            _proveedorVariedad.Observaciones = txt_Observaciones.Text;
            _proveedorVariedad.FechaRegistro = DateTime.Now;
            _proveedorVariedad.Usuario = Me.Usuario.NombreUsuario;
            _proveedorVariedad.Estado = estado;
            try
            {
                client.ProveedorVariedad_Eliminar(_proveedorVariedad, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                VerMensaje("INFORMACIÓN", "info", "info", "Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Hubo problemas al grabar el Registro: " + ex.Message.ToString());
            }
        }
        private void LimpiarControles()
        {
            cmb_EliminadoProveedor.SelectedValue = "0";
            cmb_EliminadoVariedad.SelectedValue = Guid.Empty.ToString();
            txt_EliminadoEstado.Text = "";
            txt_EliminadoObservaciones.Text = "";
            txt_Estado.Text = "";
            txt_Observaciones.Text = "";
            hdn_ProveedorVariedadID.Value = null;
            txt_EmpresaRUC.Text = Me.Usuario.EmpresaID;
            dl_InfProVar.DataSource = null;
            dl_InfProVar.DataBind();
        }
        private void Cancelar()
        {
            pnl_Datos.Visible = true;
            pnl_Buscador.Visible = true;
            pnl_Variedad.Visible = false;
            LimpiarControles();
        }
        private void CancelarEliminar()
        {
            pnl_DatosEliminado.Visible = true;
            pnl_Buscador.Visible = true;
            pnl_VariedadEliminada.Visible = false;
            LimpiarControles();
        }

        private void Grabar()
        {
            LogicClient client = new LogicClient();
            RecogerDatos();
            SGF_ProveedorVariedad _proveedorVariedad = new SGF_ProveedorVariedad();
            _proveedorVariedad = ListProveedorVariedadGeneral.First();
            try
            {
                foreach (var item in ListProveedorVariedadGeneral)
                {
                    client.ProveedorVariedad_Grabar(item, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                }
                VerMensaje("INFORMACIÓN", "info", "info", "Registro grabado correctamente");
                Cancelar();
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Hubo problemas al grabar el Registro: " + ex.Message.ToString());
            }
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
        private void VerMensaje(string title, string titIcon, string icon, string mensaje)
        {
            RadNotification1.Title = title;// "Friendship invitation";
            RadNotification1.TitleIcon = titIcon;// "none";//"info"
            RadNotification1.ContentIcon = icon;//"warning";//"info"
            RadNotification1.Text = mensaje;
            RadNotification1.Show();
        }

        protected void gv_Temporada_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ConsultarListado();
        }

        protected void gv_Temporada_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_ProveedorVariedadID.Value = e.CommandArgument.ToString();
                    //pnl_Datos.Visible = false;
                    //pnl_Buscador.Visible = false;
                    //pnl_Variedad.Visible = true;
                    //CargarDatos();
                    //EsNuevo = false;
                    //tool_principal.Items[1].Visible = true;
                    //tool_principal.Items[0].Visible = false;

                    EsNuevo = false;
                    ActivarEliminar((int)Enums.Estado.Pasivo);
                    ConsultarListado();
                    gv_Temporada.DataBind();
                    gv_TemporadaEliminado.DataBind();
                    break;
            }
        }
        private void CargarDatos()
        {
            if (new Guid(hdn_ProveedorVariedadID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_ProveedorVariedad _temporada = client.ProveedorVariedad_ObtenerPorID(new Guid(hdn_ProveedorVariedadID.Value));
            txt_EmpresaRUC.Text = _temporada.EmpresaID.ToString();
            txt_Observaciones.Text = _temporada.Observaciones.ToString();
            txt_Estado.Text = ObtenerNombreEstado((int)_temporada.Estado);
        }

        private void CargarDatosEliminado()
        {
            if (new Guid(hdn_ProveedorVariedadID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_ProveedorVariedad _temporada = client.ProveedorVariedad_ObtenerPorID(new Guid(hdn_ProveedorVariedadID.Value));
            cmb_EliminadoProveedor.SelectedValue = _temporada.ProveedorID.ToString();
            cmb_EliminadoVariedad.SelectedValue = _temporada.VariedadID.ToString();
            txt_EliminadoObservaciones.Text = _temporada.Observaciones.ToString();
            txt_EliminadoEstado.Text = ObtenerNombreEstado((int)_temporada.Estado);
        }
        protected void tool_principal_eliminar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Activar":
                    LogicClient client = new LogicClient();
                    int _resultado = client.ProveedorVariedad_ValidarExistencia(cmb_EliminadoProveedor.SelectedValue, new Guid(cmb_EliminadoVariedad.SelectedValue), Me.Usuario.EmpresaID);
                    if (_resultado > 0)
                    {
                        VerMensaje("INFORMACIÓN", "info", "info", "Ya existe un registro con los mismos datos");
                        return;
                    }
                    ActivarEliminar((int)Enums.Estado.Activo);
                    ConsultarListado();
                    gv_Temporada.DataBind();
                    gv_TemporadaEliminado.DataBind();
                    CancelarEliminar();
                    break;
                case "Cancelar":
                    CancelarEliminar();
                    break;
            }

        }

        protected void gv_TemporadaEliminado_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ConsultarListado();
        }

        protected void gv_TemporadaEliminado_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_ProveedorVariedadID.Value = e.CommandArgument.ToString();
                    pnl_DatosEliminado.Visible = false;
                    pnl_Buscador.Visible = false;
                    pnl_VariedadEliminada.Visible = true;
                    CargarDatosEliminado();
                    break;
            }
        }

        protected void dl_CatastroProveedor_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void dl_CatastroProveedor_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_Proveedor_ID = (HiddenField)e.Item.FindControl("hdn_Proveedor_ID");
            RadTextBox txt_Nro = (RadTextBox)e.Item.FindControl("txt_Nro");
            RadTextBox txt_Ruc = (RadTextBox)e.Item.FindControl("txt_Ruc");
            RadTextBox txt_RazonSocial = (RadTextBox)e.Item.FindControl("txt_RazonSocial");
            RadImageButton btn_Calcular = (RadImageButton)e.Item.FindControl("btn_Calcular");
            RadCheckBox chk_SeleccionarProveedor = (RadCheckBox)e.Item.FindControl("chk_SeleccionarProveedor");
            #endregion
            #region Asignacion de valores en controles
            LogicClient client = new LogicClient();
            CO_PROVEEDOR item = (CO_PROVEEDOR)e.Item.DataItem;
            hdn_Proveedor_ID.Value = item.CO_PRO_COD.ToString();
            txt_Ruc.Text = item.CO_PRO_COD;
            txt_RazonSocial.Text = item.CO_PRO_NOM;
            //btn_Calcular.Visible = true;
            chk_SeleccionarProveedor.Checked = true;
            //Text = '<%# NumeradorColumna().ToString() %>'
            txt_Nro.Text = NumeradorColumna().ToString();
            #endregion
        }

        protected void dl_CatastroProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dl_CatastroVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dl_CatastroVariedad_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void dl_CatastroVariedad_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_Proveedor_ID = (HiddenField)e.Item.FindControl("hdn_Variedad_ID");
            RadTextBox txt_Nro = (RadTextBox)e.Item.FindControl("txt_Nro");
            RadTextBox txt_Variedad = (RadTextBox)e.Item.FindControl("txt_Variedad");
            RadImageButton btn_Calcular = (RadImageButton)e.Item.FindControl("btn_Calcular");
            RadCheckBox chk_SeleccionarVariedad = (RadCheckBox)e.Item.FindControl("chk_SeleccionarVariedad");
            #endregion
            #region Asignacion de valores en controles
            LogicClient client = new LogicClient();
            SGF_Variedad item = (SGF_Variedad)e.Item.DataItem;
            hdn_Proveedor_ID.Value = item.VariedadID.ToString();
            //            txt_Ruc.Text = item.CO_PRO_COD;
            txt_Variedad.Text = item.Nombre;
            //btn_Calcular.Visible = true;
            chk_SeleccionarVariedad.Checked = false;
            //Text = '<%# NumeradorColumna().ToString() %>'
            txt_Nro.Text = NumeradorColumna().ToString();
            #endregion
        }



        protected void lnkConfigurar_Click(object sender, EventArgs e)
        {
            pnl_Datos.Visible = false;
            pnl_Buscador.Visible = false;
            pnl_Variedad.Visible = true;
          //  LimpiarControles();
            EsNuevo = true;
            tool_principal.Items[0].Visible = true;
            tool_principal.Items[1].Visible = false;

        }



        protected void dl_InfProVar_ItemCommand(object source, DataListCommandEventArgs e)
        {

            LogicClient client = new LogicClient();
            switch (e.CommandName)
            {
                case "grabar":
                    Guid ID = new Guid(e.CommandArgument.ToString());
                    //Guid _AreaID = new Guid(hdn_CultivoAreaID.Value);
                    // var bloquesFiltrados = client.MapaCultivo_ObtenerPorIDs(new Guid(hdn_CampoCultivoID.Value), new Guid(hdn_CultivoAreaID.Value), ID);
                    // dlAreas.DataSource = bloquesFiltrados.SGF_CultivoArea;// ListAreaCultivo.OrderBy(x => x.Orden).ToList();
                    // dlAreas.DataBind();
                    break;
            }
        }

        protected void dl_InfProVar_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_ProveedorID = (HiddenField)e.Item.FindControl("hdn_ProveedorID");
            HiddenField hdn_EmpresaID = (HiddenField)e.Item.FindControl("hdn_EmpresaID");
            RadCheckBox chk_Proveedor = (RadCheckBox)e.Item.FindControl("chk_Proveedor");
            DataList dl_Variedades = (DataList)e.Item.FindControl("dl_Variedades");
            #endregion
            #region Asignacion de valores en controles
            CO_PROVEEDOR item = (CO_PROVEEDOR)e.Item.DataItem;
            hdn_ProveedorID.Value = item.CO_PRO_COD;
            hdn_EmpresaID.Value = item.CO_EMP_RUC;
            chk_Proveedor.Text = item.CO_PRO_NOM;
            chk_Proveedor.BorderStyle = BorderStyle.Double;
            LogicClient client = new LogicClient();
            var _prov = client.ProveedorVariedad_ObtenerPorPVE(item.CO_PRO_COD, Guid.Empty, item.CO_EMP_RUC);
            if (_prov != null)
            {
                if (_prov.EmpresaID == item.CO_EMP_RUC)
                    chk_Proveedor.Checked = true;
            }
            foreach (SGF_Variedad _variedad in ListVariedadTemporal)
            {
                _variedad.ObtentorNombre = item.CO_PRO_COD;
            }
            dl_Variedades.DataSource = ListVariedadTemporal;
            dl_Variedades.DataBind();

            #endregion
        }
        protected void dl_Variedades_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void dl_Variedades_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_VariedadID = (HiddenField)e.Item.FindControl("hdn_VariedadID");
            HiddenField hdn_ProvID = (HiddenField)e.Item.FindControl("hdn_ProvID");
            HiddenField hdn_EmpID = (HiddenField)e.Item.FindControl("hdn_EmpID");
            RadCheckBox chk_Variedad = (RadCheckBox)e.Item.FindControl("chk_Variedad");
            #endregion
            #region Asignacion de valores en controles
            SGF_Variedad item = (SGF_Variedad)e.Item.DataItem;
            hdn_VariedadID.Value = item.VariedadID.ToString();
            hdn_ProvID.Value = item.ObtentorNombre;
            hdn_EmpID.Value = item.EmpresaID;
            chk_Variedad.Text = item.Codigo + " - " + item.Nombre;
            chk_Variedad.BorderStyle = BorderStyle.Double;

            LogicClient client = new LogicClient();
            var _prov = client.ProveedorVariedad_ObtenerPorPVE(item.ObtentorNombre, item.VariedadID, item.EmpresaID);
            if (_prov != null)
            {
                if (_prov.VariedadID == item.VariedadID && _prov.EmpresaID == item.EmpresaID)
                    chk_Variedad.Checked = true;
            }

            #endregion
        }

        private void RecogerDatos()
        { // descargo todos los datos llenados en el datalist, para que luego en la carga puedan ser leídos
            foreach (DataListItem _item in dl_InfProVar.Items)
            {
                #region Declaracion de controles
                HiddenField hdn_ProveedorID = (HiddenField)_item.FindControl("hdn_ProveedorID");
                HiddenField hdn_EmpresaID = (HiddenField)_item.FindControl("hdn_EmpresaID");
                RadCheckBox chk_Proveedor = (RadCheckBox)_item.FindControl("chk_Proveedor");
                DataList dl_Variedades = (DataList)_item.FindControl("dl_Variedades");
                #endregion
                #region Asignación de valores en el listado en memoria

                if (chk_Proveedor.Checked == true)
                {
                    foreach (DataListItem _itemvar in dl_Variedades.Items)
                    {
                        #region Declaracion de controles
                        HiddenField hdn_VariedadID = (HiddenField)_itemvar.FindControl("hdn_VariedadID");
                        HiddenField hdn_ProvID = (HiddenField)_itemvar.FindControl("hdn_ProvID");
                        HiddenField hdn_EmpID = (HiddenField)_itemvar.FindControl("hdn_EmpID");
                        RadCheckBox chk_Variedad = (RadCheckBox)_itemvar.FindControl("chk_Variedad");
                        #endregion
                        #region Asignación de valores en el listado en memoria
                        if (chk_Variedad.Checked == true)
                        {
                            SGF_ProveedorVariedad _provar = new SGF_ProveedorVariedad();
                            _provar.ProveedorVariedadID = Guid.NewGuid();
                            //if (hdn_ProvID.Value.Trim().Length < 5)
                            //{
                            //    _provar.ProveedorID = "";
                            //    _provar.AlmacenID = int.Parse(hdn_ProvID.Value);
                            //}
                            //else
                                _provar.ProveedorID = hdn_ProvID.Value;
//                                _provar.AlmacenID = 0;
                            #endregion
                            _provar.EmpresaID = hdn_EmpID.Value;
                            _provar.VariedadID = new Guid(hdn_VariedadID.Value.ToString());
                            _provar.Observaciones = txt_Observaciones.Text;
                            _provar.FechaRegistro = DateTime.Now;
                            _provar.Usuario = Me.Usuario.NombreUsuario;
                            _provar.Estado = 1;
                            ListProveedorVariedadGeneral.Add(_provar);
                        }

                    }
                }

                #endregion
            }
        }
        protected void chk_ProveedorCredito_Click(object sender, EventArgs e)
        {
            //if (chk_Proveedor.Checked == true)
            //{
            //    lbl_ProveedorCredito.Visible =
            //    txt_ProveedorLimiteCredito.Visible = true;

            //}
            //else
            //{
            //    lbl_ProveedorCredito.Visible =
            //    txt_ProveedorLimiteCredito.Visible = false;
            //    txt_ProveedorLimiteCredito.Value = 0;
            //}
        }

        protected void rbt_General_CheckedChanged(object sender, EventArgs e)
        {
            table_Detallado.Visible = false;
            table_General.Visible = true;
        }

        protected void rbt_Detallada_CheckedChanged(object sender, EventArgs e)
        {

            table_Detallado.Visible = true;
            table_General.Visible = false;
        }
        protected void rbt_General_Click(object sender, EventArgs e)
        {

        }

        protected void rbt_Detallada_Click(object sender, EventArgs e)
        {
        }

    }
}