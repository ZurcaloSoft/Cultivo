using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Cultivo
{
    public partial class Frm_EnvioDeFlor : System.Web.UI.Page
    {
        #region Variables y Propiedades
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

        protected DateTime today
        {
            get
            {
                if (ViewState["today"] == null)
                    ViewState["today"] = DateTime.Now;
                return (DateTime)ViewState["today"];
            }
            set
            {
                ViewState["today"] = value;
            }
        }

        protected List<SGF_ProveedorVariedad_VTA> ListProveedorVariedadTemporal
        {
            get
            {
                if (ViewState["ListProveedorVariedadTemporal"] == null)
                    ViewState["ListProveedorVariedadTemporal"] = new List<SGF_ProveedorVariedad_VTA>();
                return (List<SGF_ProveedorVariedad_VTA>)ViewState["ListProveedorVariedadTemporal"];
            }
            set
            {
                ViewState["ListProveedorVariedadTemporal"] = value;
            }
        }

        protected List<SGF_ProyeccionProduccion> ListProyeccionTemporal
        {
            get
            {
                if (ViewState["ListProyeccionTemporal"] == null)
                    ViewState["ListProyeccionTemporal"] = new List<SGF_ProyeccionProduccion>();
                return (List<SGF_ProyeccionProduccion>)ViewState["ListProyeccionTemporal"];
            }
            set
            {
                ViewState["ListProyeccionTemporal"] = value;
            }
        }

        protected List<SGF_Proyeccion> ListProyeccion
        {
            get
            {
                if (ViewState["ListProyeccion"] == null)
                    ViewState["ListProyeccion"] = new List<SGF_Proyeccion>();
                return (List<SGF_Proyeccion>)ViewState["ListProyeccion"];
            }
            set
            {
                ViewState["ListProyeccion"] = value;
            }
        }
        protected List<SGF_ProyeccionProduccion_VTA> ListProyeccionTemporal_VTA
        {
            get
            {
                if (ViewState["ListProyeccionTemporal_VTA"] == null)
                    ViewState["ListProyeccionTemporal_VTA"] = new List<SGF_ProyeccionProduccion_VTA>();
                return (List<SGF_ProyeccionProduccion_VTA>)ViewState["ListProyeccionTemporal_VTA"];
            }
            set
            {
                ViewState["ListProyeccionTemporal_VTA"] = value;
            }
        }
        public bool EsProveedor
        {
            get
            {
                if (ViewState["EsNuevo"] == null)
                    ViewState["EsNuevo"] = false;
                return (bool)ViewState["EsNuevo"];
            }
            set
            {
                ViewState["EsNuevo"] = value;
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
                CargarCombos();
                CargarDatosCalendar();
                // Configurar límites del calendario al mes actual
                today = DateTime.Now;
                cal_ProyeccionGeneral.RangeMinDate = new DateTime(today.Year, today.Month == 1 ? today.Month : today.Month - 1, 1); // Primer día del mes actual
                //cal_ProyeccionGeneral.RangeMaxDate = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)); // Último día del mes actual
                cal_ProyeccionGeneral.RangeMaxDate = new DateTime(2025, 1, DateTime.DaysInMonth(2025, 1)); // Último día del mes actual
                EsProveedor = verificarEsProveedor();
            }
        }

        protected bool verificarEsProveedor()
        {
            LogicClient client = new LogicClient();
            SGF_Proveedor_VTA Proveedor_VTA = client.Proveedor_ObtenerPorID_VTA(Me.Usuario.Cedula);
            if (Proveedor_VTA == null)
                return false;
            else
            {
                return true;// Proveedor_VTA.SoloEnvio == null ? false : Proveedor_VTA.SoloEnvio;
            }
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Buscador.Visible = false;
            pnl_Informacion.Visible = true;
            pnl_Cabecera.Visible = false;
            pnl_Detalle.Visible = true;
            pnl_DetalleProyeccion.Visible = false;
            table_ProyeccionGeneral.Visible = true;
            LimpiarControles();
            hdn_ProyeccionProduccionID.Value = Guid.Empty.ToString();
            hdn_ProyeccionID.Value = Guid.NewGuid().ToString();
            tool_principal.Items[0].Visible = true;
            tool_principal.Items[2].Visible = false;
        }

        protected void cal_ProyeccionGeneral_DayRender(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {
            // Acceder a cada día del calendario
            var day = e.Day.Date;
            e.Cell.Style["border"] = "1px solid #cccccc";
            foreach (SGF_Proyeccion item in ListProyeccion)
            {
                if (day == item.FechaProyeccion)
                {
                    //// Cambiar el estilo del día marcado
                    string tooltipText = "Fecha Proyección: " + Utils.FormatearFecha(day) + "\nCódigo: " + item.NumeroProyeccion;
                    switch (item.Estado)
                    {
                        case 2:
                            // Personaliza el día
                            e.Cell.ToolTip = "**ENVIO**\n" + tooltipText;// "Día Especial: " + e.Day.Date.ToString("d");
                            e.Cell.BackColor = System.Drawing.Color.Violet;//.Gold;
                            e.Cell.ForeColor = System.Drawing.Color.White;
                            e.Cell.Font.Bold = true;

                            // Opcional: Haz el día no seleccionable
                            e.Day.IsSelectable = false;

                            // Opcional: Agrega una clase CSS personalizada
                            e.Cell.CssClass = "special-day";
                            break;
                        case 3:
                            // Personaliza el día
                            e.Cell.ToolTip = "**RECEPCIÓN**\n" + tooltipText;// "Día Especial: " + e.Day.Date.ToString("d");
                            e.Cell.BackColor = System.Drawing.Color.Cyan;//.Cyan;
                            e.Cell.ForeColor = System.Drawing.Color.Black;
                            e.Cell.Font.Bold = true;

                            // Opcional: Haz el día no seleccionable
                            e.Day.IsSelectable = false;

                            // Opcional: Agrega una clase CSS personalizada
                            e.Cell.CssClass = "special-day-envio";
                            break;
                    }
                }
            }
        }

        protected void cal_ProyeccionGeneral_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        { // Obtener las fechas seleccionadas
            var selectedDates = cal_ProyeccionGeneral.SelectedDates;
            var auc = cal_ProyeccionGeneral.SelectedDate;
            CargarDatos(auc);
        }

        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Grabar":
                    Grabar();
                    break;
                case "Refrescar":
                    CargarDatos(dt_FechaProyeccion.SelectedDate.Value);
                    break;
                case "Aprobar":
                    Aprobar();
                    break;
                case "Cancelar":
                    Cancelar();
                    break;
            }
        }

        protected void dl_ProyeccionGeneral_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        protected void dl_ProyeccionGeneral_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles

            HiddenField hdn_ProveedorID = (HiddenField)e.Item.FindControl("hdn_ProveedorID");
            HiddenField hdn_ProveedorCodigo = (HiddenField)e.Item.FindControl("hdn_ProveedorCodigo");
            HiddenField hdn_EmpresaID = (HiddenField)e.Item.FindControl("hdn_EmpresaID");
            RadCheckBox chk_Proveedor = (RadCheckBox)e.Item.FindControl("chk_Proveedor");
            DataList dl_Variedades = (DataList)e.Item.FindControl("dl_Variedades");
            RadCalendar cal_DiasProyeccion = (RadCalendar)e.Item.FindControl("cal_DiasProyeccion");
            #endregion
            #region Asignacion de valores en controles
            SGF_Proveedor_VTA item = (SGF_Proveedor_VTA)e.Item.DataItem;
            hdn_ProveedorID.Value = item.ProveedorID;
            hdn_EmpresaID.Value = item.EmpresaID;
            hdn_ProveedorCodigo.Value = item.CodigoProveedor;
            chk_Proveedor.Text = item.ProveedorNombre;
            chk_Proveedor.BorderStyle = BorderStyle.Double;
            LogicClient client = new LogicClient();
            var _prov = client.ProveedorVariedad_ObtenerPorPVE(item.ProveedorID, Guid.Empty, item.EmpresaID);
            if (_prov != null)
            {
                if (_prov.EmpresaID == item.EmpresaID)
                    chk_Proveedor.Checked = true;
            }
            //cal_DiasProyeccion.RangeMinDate = DateTime.Now;// new DateTime(today.Year, today.Month, 1); // Primer día del mes actual
            //cal_DiasProyeccion.RangeMaxDate = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)); // Último día del mes actual

            dl_Variedades.DataSource = ListProveedorVariedadTemporal;
            dl_Variedades.DataBind();
            #endregion
        }

        protected void dl_DetalleProyeccion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "update":

                    Guid ID = new Guid(e.CommandArgument.ToString());
                    LogicClient client = new LogicClient();
                    // Obtén la referencia al elemento actual
                    DataListItem item = e.Item;
                    // Accede a los controles en el elemento
                    HiddenField hdn_ProvID = (HiddenField)e.Item.FindControl("hdn_ProvID");
                    HiddenField hdn_VarID = (HiddenField)e.Item.FindControl("hdn_VarID");
                    HiddenField hdn_EmpID = (HiddenField)e.Item.FindControl("hdn_EmpID");
                    HiddenField hdn_ProyID = (HiddenField)e.Item.FindControl("hdn_ProyID");
                    HiddenField hdn_ProyProdID = (HiddenField)e.Item.FindControl("hdn_ProyProdID");
                    RadNumericTextBox txt_NroTallos = (RadNumericTextBox)e.Item.FindControl("txt_NroTallos");
                    SGF_Variedad_VTA _variedad = new SGF_Variedad_VTA();
                    _variedad = client.Variedad_ObtenerPorID_VTA(new Guid(hdn_VarID.Value));

                    int gruposCompletos = (int)txt_NroTallos.Value / (int)_variedad.TallosPorMalla;
                    // Número de tallossobrantes
                    int sobrantes = (int)txt_NroTallos.Value % (int)_variedad.TallosPorMalla;

                    SGF_ProyeccionProduccion _updProyProd = new SGF_ProyeccionProduccion();
                    _updProyProd.ProyeccionID = new Guid(hdn_ProyID.Value);
                    _updProyProd.ProyeccionProduccionID = new Guid(hdn_ProyProdID.Value);
                    _updProyProd.VariedadID = new Guid(hdn_VarID.Value);
                    _updProyProd.EmpresaID = hdn_EmpID.Value;
                    _updProyProd.EnvioNroTallo = (int)txt_NroTallos.Value;
                    _updProyProd.EnvioNroMalla = gruposCompletos;
                    _updProyProd.EnvioNroTalloSuelto = sobrantes;
                    _updProyProd.EnvioUsuario = Me.Usuario.NombreUsuario;
                    _updProyProd.EnvioObservaciones = "";
                    _updProyProd.EnvioEstado = 1;
                    _updProyProd.EnvioFecha = DateTime.Now;
                    client.ProyeccionProduccion_ActualizarEnvio(_updProyProd, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                    client.Proyeccion_ActualizarTotales(new Guid(hdn_ProyID.Value), Me.Usuario.NombreUsuario, Utils.getIP(), Utils.getHostName(Utils.getIP()), 2);
                    CargarDatos(dt_FechaProyeccion.SelectedDate.Value);
                    VerMensaje("INFORMACIÓN", "info", "info", "Registro actualizado correctamente");
                    break;
            }
        }

        protected void dl_DetalleProyeccion_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_ProvID = (HiddenField)e.Item.FindControl("hdn_ProvID");
            HiddenField hdn_VarID = (HiddenField)e.Item.FindControl("hdn_VarID");
            HiddenField hdn_EmpID = (HiddenField)e.Item.FindControl("hdn_EmpID");
            HiddenField hdn_ProyID = (HiddenField)e.Item.FindControl("hdn_ProyID");
            HiddenField hdn_ProyProdID = (HiddenField)e.Item.FindControl("hdn_ProyProdID");
            RadLabel lbl_Proveedor = (RadLabel)e.Item.FindControl("lbl_Proveedor");
            RadLabel lbl_Variedad = (RadLabel)e.Item.FindControl("lbl_Variedad");
            //  RadTextBox txt_Variedad = (RadTextBox)e.Item.FindControl("txt_Variedad");
            RadNumericTextBox txt_NroTallos = (RadNumericTextBox)e.Item.FindControl("txt_NroTallos");
            RadNumericTextBox txt_NroMallas = (RadNumericTextBox)e.Item.FindControl("txt_NroMallas");
            RadNumericTextBox txt_NroSueltos = (RadNumericTextBox)e.Item.FindControl("txt_NroSueltos");
            RadImageButton btn_Grabar = (RadImageButton)e.Item.FindControl("btn_Grabar");
            #endregion
            #region Asignacion de valores en controles
            SGF_ProyeccionProduccion_VTA item = (SGF_ProyeccionProduccion_VTA)e.Item.DataItem;
            hdn_ProvID.Value = item.ProveedorID;
            hdn_VarID.Value = item.VariedadID.ToString();
            hdn_EmpID.Value = item.EmpresaID;
            hdn_ProyID.Value = item.ProyeccionID.ToString();
            hdn_ProyProdID.Value = item.ProyeccionProduccionID.ToString();
            lbl_Proveedor.Text = item.ProveedorNombre;
            lbl_Proveedor.Font.Bold = true;
            lbl_Variedad.Text = item.VariedadNombre;
            lbl_Proveedor.Font.Bold = true;
            txt_NroTallos.Value = item.EnvioNroTallo;
            txt_NroMallas.Value = item.EnvioNroMalla;
            txt_NroSueltos.Value = item.EnvioNroTalloSuelto;
            if (item.EnvioEstado == 2)
                btn_Grabar.Visible = false;
            else
                btn_Grabar.Visible = true;
            #endregion
        }

        protected void dl_Variedades_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }
        protected void dl_Variedades_ItemDataBound(object source, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_VariedadID = (HiddenField)e.Item.FindControl("hdn_VariedadID");
            HiddenField hdn_ProvID = (HiddenField)e.Item.FindControl("hdn_ProvID");
            HiddenField hdn_EmpID = (HiddenField)e.Item.FindControl("hdn_EmpID");
            HiddenField hdn_TallosXMalla = (HiddenField)e.Item.FindControl("hdn_TallosXMalla");
            RadLabel lbl_Variedad = (RadLabel)e.Item.FindControl("lbl_Variedad");
            RadNumericTextBox txt_NroTallos = (RadNumericTextBox)e.Item.FindControl("txt_NroTallos");
            #endregion
            #region Asignacion de valores en controles
            SGF_ProveedorVariedad_VTA item = (SGF_ProveedorVariedad_VTA)e.Item.DataItem;
            hdn_VariedadID.Value = item.VariedadID.ToString();
            hdn_ProvID.Value = item.ProveedorID;
            hdn_EmpID.Value = item.EmpresaID;
            lbl_Variedad.Text = item.VariedadNombre;
            lbl_Variedad.BorderStyle = BorderStyle.Double;
            txt_NroTallos.Value = 0;
            LogicClient client = new LogicClient();
            var _variedad = client.Variedad_ObtenerPorID_VTA(item.VariedadID);
            hdn_TallosXMalla.Value = _variedad.TallosPorMalla.ToString();
            #endregion
        }

        #region Métodos
        private void CargarCombos()
        {
            LogicClient client = new LogicClient();
            ListVariedadTemporal = client.Variedad_ObtenerTodo().Where(x => x.Estado == 1).OrderBy(x => x.Nombre).ToList();
            if (Me.Usuario.TipoUsuarioID == TipoUsuario.Proveedor && Me.Usuario.TipoPersonaID == TipoPersona.Proveedor)
            {
                dl_ProyeccionGeneral.DataSource = client.Proveedor_ObtenerTodo_VTA().Where(x => x.ProveedorID == Me.Usuario.Cedula).ToList();
                ListProveedorVariedadTemporal = client.ProveedorVariedad_ObtenerPorFiltro_VTA(Me.Usuario.Cedula, Guid.Empty, Me.Usuario.EmpresaID);
            }
            else
            {
                var _res = client.Proveedor_ObtenerTodo_VTA();
                dl_ProyeccionGeneral.DataSource = _res.Where(x => x.EmpresaID == Me.Usuario.EmpresaID && x.ProveedorID.Length <= 5).ToList();
                ListProveedorVariedadTemporal = client.ProveedorVariedad_ObtenerPorFiltro_VTA("", Guid.Empty, Me.Usuario.EmpresaID);
            }
            dl_ProyeccionGeneral.DataBind();
        }

        private void CargarDatosCalendar()
        {
            LogicClient client = new LogicClient();
            if (Me.Usuario.TipoPersonaID == TipoPersona.Proveedor)
                ListProyeccion = client.Proyeccion_ObtenerPorEmpresaProveedorID(Me.Usuario.EmpresaID, Me.Usuario.Cedula);
            else
                ListProyeccion = client.Proyeccion_ObtenerPorEmpresaProveedorID(Me.Usuario.EmpresaID, "");
        }

        private void LimpiarControles()
        {
            txt_NroTallos.Value = 0;
            txt_NroMallas.Value = 0;
            txt_NroTallosSueltos.Value = 0;
            txt_Estado.Text = "";
            txt_Observaciones.Text = "";
            hdn_ProyeccionProduccionID.Value = null;
            hdn_ProyeccionID.Value = null;
            cal_ProyeccionGeneral.SelectedDates.Clear();
            ListProyeccionTemporal = new List<SGF_ProyeccionProduccion>();
            ListProyeccion = new List<SGF_Proyeccion>();
        }

        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_Informacion.Visible = false;
            pnl_DetalleProyeccion.Visible = false;
            pnl_Cabecera.Visible = false;
            pnl_Detalle.Visible = false;
            pnl_DetalleProyeccion.Visible = false;
            table_ProyeccionGeneral.Visible = false;
            CargarDatosCalendar();
        }

        private void Grabar()
        {
            #region Validaciones
            String Message = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (txt_NroTallos.Value == 0)
            {
                Message += "Debe ingresar el Nro. de Tallos." + Environment.NewLine;
                sb.AppendLine("* Debe ingresar el Nro. de Tallos.");
                sb.AppendLine(" ");
                //VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre");
                //return;
            }
            if (txt_NroMallas.Value == 0)
            {
                Message += "Debe ingresar el Nro. de Mallas." + Environment.NewLine;
                sb.AppendLine("* Debe ingresar el Nro. de Mallas.");
                sb.AppendLine(" ");
                //VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Área Total del Cultivo");
                //return;
            }
            //string result = sb.ToString();
            //if (Message != string.Empty)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", result);
            //    //Utils.MessageBox(this, "", Message, "info");
            //    return;
            //}

            #endregion
            RecogerDatos();
            LogicClient client = new LogicClient();
            foreach (SGF_Proyeccion _newProyeccion in ListProyeccion)
            {
                client.Proyeccion_Grabar(_newProyeccion, Utils.getIP(), Utils.getHostName(Utils.getIP()));
            }
            foreach (SGF_ProyeccionProduccion _newProyeccion in ListProyeccionTemporal)
            {
                client.ProyeccionProduccion_ActualizarEnvio(_newProyeccion, Utils.getIP(), Utils.getHostName(Utils.getIP()));
            }
            VerMensaje("INFORMACIÓN", "info", "info", "Proyección Registrada correctamente");
            Cancelar();
        }

        private void Aprobar()
        {
            LogicClient client = new LogicClient();
            client.Proyeccion_ActualizarEstado(new Guid(hdn_ProyeccionID.Value), 3, Me.Usuario.NombreUsuario, Utils.getIP(), Utils.getHostName(Utils.getIP()), 2);
            client.ProyeccionProduccion_GrabarRecepcion(new Guid(hdn_ProyeccionID.Value), Me.Usuario.NombreUsuario, Utils.getIP(), Utils.getHostName(Utils.getIP()));
            VerMensaje("INFORMACIÓN", "info", "info", "Proyección Aprobada para la Recepción");
            Cancelar();
            CargarDatosCalendar();
        }
        private void CargarDatos(DateTime _day)
        {
            LogicClient client = new LogicClient();
            List<SGF_Proyeccion> _ProyDay = ListProyeccion.Where(x => x.FechaProyeccion == _day && (x.Estado == 3 || x.Estado == 2)).ToList();

            string _aux1 = Me.Usuario.EmpresaID;
            string _aux2 = Me.Usuario.TipoPersonaID.ToString();
            string _aux3 = Me.Usuario.TipoUsuarioID.ToString();

            List<SGF_ProyeccionProduccion_VTA> _ProyDay_VTa = new List<SGF_ProyeccionProduccion_VTA>();
            List<SGF_ProyeccionProduccion_VTA> _Temp_ProyDay_VTa = new List<SGF_ProyeccionProduccion_VTA>();
            if (_ProyDay.Count > 0)
            {
                pnl_Buscador.Visible = false;
                pnl_Cabecera.Visible = true;
                pnl_Informacion.Visible = true;
                pnl_DetalleProyeccion.Visible = true;
                tool_principal.Items[0].Visible = false;
                tool_principal.Items[1].Visible = true;
                tool_principal.Items[2].Visible = true;
                pnl_DetalleProyeccion.Enabled = true;
                if (_ProyDay.Count > 1)
                {
                    _Temp_ProyDay_VTa = client.SGF_ProyeccionProduccion_ObtenerPorFecha((DateTime)_ProyDay.First().FechaProyeccion);
                    foreach (SGF_Proyeccion item in _ProyDay)
                    {
                        txt_NroMallas.Value = txt_NroMallas.Value + item.TotalMalla;
                        txt_NroTallos.Value = txt_NroTallos.Value + item.TotalTallos;
                        txt_NroTallosSueltos.Value = txt_NroTallosSueltos.Value + item.TotalSobrantes;
                        foreach (SGF_ProyeccionProduccion_VTA proyProd in _Temp_ProyDay_VTa.Where(x => x.ProyeccionID == item.ProyeccionID))
                        {
                            _ProyDay_VTa.Add(proyProd);
                        }
                    }
                    txt_Estado.Text = "Pendiente de Enviar";
                    txt_Proveedor.Text = Me.Usuario.EmpresaNombre;
                    txt_Observaciones.Text = _ProyDay.First().Observaciones;
                    dt_FechaProyeccion.SelectedDate = _ProyDay.First().FechaProyeccion;
                    hdn_ProyeccionID.Value = _ProyDay.First().ProyeccionID.ToString();
                    dl_DetalleProyeccion.DataSource = _ProyDay_VTa;
                    dl_DetalleProyeccion.DataBind();

                    if (_ProyDay.First().Estado == 3)
                    {
                        tool_principal.Items[0].Visible = false;
                        tool_principal.Items[1].Visible = false;
                        tool_principal.Items[2].Visible = false;
                        pnl_DetalleProyeccion.Enabled = false;
                    }
                }
                else
                {
                    _ProyDay_VTa = client.SGF_ProyeccionProduccion_ObtenerPorProyeccionID_VTA(_ProyDay.First().ProyeccionID);
                    txt_Estado.Text = "Pendiente de Enviar";
                    txt_NroMallas.Value = _ProyDay_VTa.First().TotalMalla;
                    txt_NroTallos.Value = _ProyDay_VTa.First().TotalTallos;
                    txt_NroTallosSueltos.Value = _ProyDay_VTa.First().TotalSobrantes;
                    txt_Proveedor.Text = Me.Usuario.EmpresaNombre;
                    txt_Observaciones.Text = _ProyDay.First().Observaciones;
                    hdn_ProyeccionID.Value = _ProyDay_VTa.First().ProyeccionID.ToString();
                    dt_FechaProyeccion.SelectedDate = _ProyDay_VTa.First().FechaProyeccion;
                    dl_DetalleProyeccion.DataSource = _ProyDay_VTa;
                    dl_DetalleProyeccion.DataBind();
                    if (_ProyDay.First().Estado == 3)
                    {
                        tool_principal.Items[0].Visible = false;
                        tool_principal.Items[1].Visible = false;
                        tool_principal.Items[2].Visible = false;
                        pnl_DetalleProyeccion.Enabled = false;
                    }
                }
            }
            if (_ProyDay.Count == 0)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "No existen datos con la fecha seleccionada");
                return;
            }
        }
        private void RecogerDatos()
        {
            // descargo todos los datos llenados en el datalist, para que luego en la carga puedan ser leídos
            int _sumaTallosPorDia = 0;
            int _sumaMallasPorDia = 0;
            int _sumaSueltosPorDia = 0;
            string _prefijo = "";
            if (Me.Usuario.TipoPersonaID == TipoPersona.Proveedor)
                _prefijo = "P";
            else
                _prefijo = "F";

            foreach (DataListItem item in dl_ProyeccionGeneral.Items)
            {
                #region Declaracion de controles

                HiddenField hdn_ProveedorID = (HiddenField)item.FindControl("hdn_ProveedorID");
                HiddenField hdn_ProveedorCodigo = (HiddenField)item.FindControl("hdn_ProveedorCodigo");
                HiddenField hdn_EmpresaID = (HiddenField)item.FindControl("hdn_EmpresaID");
                RadCheckBox chk_Proveedor = (RadCheckBox)item.FindControl("chk_Proveedor");
                DataList dl_Variedades = (DataList)item.FindControl("dl_Variedades");
                RadCalendar cal_DiasProyeccion = (RadCalendar)item.FindControl("cal_DiasProyeccion");
                #endregion

                List<DateTime> selectedDates = new List<DateTime>();
                if (cal_DiasProyeccion.SelectedDates.Count == 0)
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar al menos un día");
                    return;
                }

                foreach (Telerik.Web.UI.RadDate selectedDate in cal_DiasProyeccion.SelectedDates)
                {
                    hdn_ProyeccionID.Value = Guid.NewGuid().ToString();
                    selectedDates.Add(selectedDate.Date);
                    _sumaTallosPorDia = 0;
                    _sumaMallasPorDia = 0;
                    _sumaSueltosPorDia = 0;
                    foreach (DataListItem itemVrd in dl_Variedades.Items)
                    {
                        #region Declaracion de controles
                        HiddenField hdn_VariedadID = (HiddenField)itemVrd.FindControl("hdn_VariedadID");
                        HiddenField hdn_ProvID = (HiddenField)itemVrd.FindControl("hdn_ProvID");
                        HiddenField hdn_EmpID = (HiddenField)itemVrd.FindControl("hdn_EmpID");
                        RadLabel lbl_Variedad = (RadLabel)itemVrd.FindControl("lbl_Variedad");
                        RadNumericTextBox txt_NroTallos = (RadNumericTextBox)itemVrd.FindControl("txt_NroTallos");
                        HiddenField hdn_TallosXMalla = (HiddenField)itemVrd.FindControl("hdn_TallosXMalla");
                        #endregion
                        if (txt_NroTallos.Value > 0)
                        {
                            int totalTallos = (int)txt_NroTallos.Value;
                            int tamañoGrupo = int.Parse(hdn_TallosXMalla.Value);
                            // Número de grupos completos
                            int gruposCompletos = totalTallos / tamañoGrupo;
                            // Número de tallossobrantes
                            int sobrantes = totalTallos % tamañoGrupo;
                            _sumaTallosPorDia = _sumaTallosPorDia + totalTallos;
                            _sumaMallasPorDia = _sumaMallasPorDia + gruposCompletos;
                            _sumaSueltosPorDia = _sumaSueltosPorDia + sobrantes;
                            SGF_ProyeccionProduccion newProyeccionProduccion = new SGF_ProyeccionProduccion();
                            newProyeccionProduccion.ProyeccionProduccionID = Guid.NewGuid();
                            newProyeccionProduccion.ProyeccionID = new Guid(hdn_ProyeccionID.Value);
                            newProyeccionProduccion.ProveedorID = hdn_ProveedorID.Value;
                            newProyeccionProduccion.EmpresaID = hdn_EmpresaID.Value;
                            newProyeccionProduccion.VariedadID = new Guid(hdn_VariedadID.Value);
                            newProyeccionProduccion.BloqueID = Guid.Empty;
                            newProyeccionProduccion.EnvioNroTallo = totalTallos;
                            newProyeccionProduccion.EnvioNroMalla = gruposCompletos;
                            newProyeccionProduccion.EnvioNroTalloSuelto = sobrantes;
                            newProyeccionProduccion.EnvioFecha = DateTime.Now;
                            newProyeccionProduccion.EnvioUsuario = Me.Usuario.NombreUsuario;
                            newProyeccionProduccion.ProyeccionFechaEntrega = selectedDate.Date;
                            newProyeccionProduccion.EnvioObservaciones = "";
                            newProyeccionProduccion.EnvioEstado = 1;
                            ListProyeccionTemporal.Add(newProyeccionProduccion);
                        }
                    }
                    if (_sumaTallosPorDia > 0)
                    {
                        SGF_Proyeccion _newProyeccion = new SGF_Proyeccion();
                        _newProyeccion.ProyeccionID = new Guid(hdn_ProyeccionID.Value);
                        _newProyeccion.ProveedorID = hdn_ProveedorID.Value;
                        _newProyeccion.EmpresaID = Me.Usuario.EmpresaID;
                        _newProyeccion.TotalTallos = _sumaTallosPorDia;
                        _newProyeccion.TotalMalla = _sumaMallasPorDia;
                        _newProyeccion.TotalSobrantes = _sumaSueltosPorDia;
                        _newProyeccion.NumeroProyeccion = _prefijo+ hdn_ProveedorCodigo.Value.Trim() + "-" + selectedDate.Date.Day.ToString() + selectedDate.Date.Month.ToString() + "-" + selectedDate.Date.Year.ToString();
                        _newProyeccion.FechaRegistro = DateTime.Now;
                        _newProyeccion.UsuarioRegistro = Me.Usuario.NombreUsuario;
                        _newProyeccion.FechaProyeccion = selectedDate.Date;
                        _newProyeccion.Estado = 2;
                        _newProyeccion.Observaciones = txt_Observaciones.Text;
                        ListProyeccion.Add(_newProyeccion);
                    }
                }
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


        #endregion
    }
}