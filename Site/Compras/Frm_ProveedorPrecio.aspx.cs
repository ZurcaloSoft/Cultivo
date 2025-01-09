using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Compras
{
    public partial class Frm_ProveedorPrecio : System.Web.UI.Page
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
                //  txt_EliminadoEmpresa.Text = Me.Usuario.EmpresaID;
            }
        }

        private void cargarCombos()
        {
            LogicClient client = new LogicClient();
            var _proveedor = client.Proveedor_ObtenerPorEmpresaID(Me.Usuario.EmpresaID).OrderBy(x => x.CO_PRO_NOM);
            var _variedad = client.Variedad_ObtenerTodo().Where(x => x.Estado == 1).OrderBy(x => x.Nombre);
            cmb_BuscarProveedor.DataSource = _proveedor;
            cmb_BuscarProveedor.DataTextField = "CO_PRO_NOM";
            cmb_BuscarProveedor.DataValueField = "CO_PRO_COD";
            cmb_BuscarProveedor.DataBind();
            cmb_BuscarProveedor.Items.Insert(0, new RadComboBoxItem("Seleccionar el Proveedor", "0"));

            cmb_BuscarVariedad.DataSource = _variedad;
            cmb_BuscarVariedad.DataTextField = "Nombre";
            cmb_BuscarVariedad.DataValueField = "VariedadID";
            cmb_BuscarVariedad.DataBind();
            cmb_BuscarVariedad.Items.Insert(0, new RadComboBoxItem("Seleccionar la Variedad", Guid.Empty.ToString()));

            //cmb_Empresa.DataSource = _proveedor;
            //cmb_Empresa.DataTextField = "CO_EMP_NOM";
            //cmb_Empresa.DataValueField = "CO_EMP_RUC";
            //cmb_Empresa.DataBind();
            //cmb_Empresa.Items.Insert(0, new RadComboBoxItem("Seleccionar la Empresa", "0"));

            cmb_Proveedor.DataSource = _proveedor;
            cmb_Proveedor.DataTextField = "CO_PRO_NOM";
            cmb_Proveedor.DataValueField = "CO_PRO_COD";
            cmb_Proveedor.DataBind();
            cmb_Proveedor.Items.Insert(0, new RadComboBoxItem("Seleccionar el Proveedor", "0"));

            cmb_Variedad.DataSource = _variedad;
            cmb_Variedad.DataTextField = "Nombre";
            cmb_Variedad.DataValueField = "VariedadID";
            cmb_Variedad.DataBind();
            cmb_Variedad.Items.Insert(0, new RadComboBoxItem("Seleccionar la Variedad", Guid.Empty.ToString()));

            //cmb_EliminadoProveedor.DataSource = _proveedor;
            //cmb_EliminadoProveedor.DataTextField = "CO_PRO_NOM";
            //cmb_EliminadoProveedor.DataValueField = "CO_PRO_COD";
            //cmb_EliminadoProveedor.DataBind();
            //cmb_EliminadoProveedor.Items.Insert(0, new RadComboBoxItem("Seleccionar el Proveedor", "0"));

            //cmb_EliminadoVariedad.DataSource = _variedad;
            //cmb_EliminadoVariedad.DataTextField = "Nombre";
            //cmb_EliminadoVariedad.DataValueField = "VariedadID";
            //cmb_EliminadoVariedad.DataBind();
            //cmb_EliminadoVariedad.Items.Insert(0, new RadComboBoxItem("Seleccionar la Variedad", Guid.Empty.ToString()));

            rlb_TipoCalidad.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoCalidad);
            rlb_TipoCalidad.DataTextField = "Nombre";
            rlb_TipoCalidad.DataValueField = "ClasificadorID";
            rlb_TipoCalidad.DataBind();

            rlb_Longitud.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Longitudes);
            rlb_Longitud.DataTextField = "Nombre";
            rlb_Longitud.DataValueField = "ClasificadorID";
            rlb_Longitud.DataBind();

        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Datos.Visible = false;
            pnl_Buscador.Visible = false;
            pnl_Variedad.Visible = true;
            pnl_Detalle.Visible = false;
            LimpiarControles();
            EsNuevo = true;
            tool_principal.Items[0].Visible = true;
            tool_principal.Items[1].Visible = false;
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            ConsultarListado();
            gv_Temporada.DataBind();
            // gv_TemporadaEliminado.DataBind();
        }

        private void ConsultarListado()
        {
            LogicClient client = new LogicClient();
            Numerador = 0;
            var _resultado = client.ProveedorPrecio_ObtenerPorFiltro_VTA(cmb_BuscarProveedor.SelectedValue == "0" ? string.Empty : cmb_BuscarProveedor.SelectedValue, new Guid(cmb_BuscarVariedad.SelectedValue), Me.Usuario.EmpresaID);
            gv_Temporada.DataSource = _resultado.Count > 0 ? _resultado.Where(x => x.Estado == (int)Enums.Estado.Activo).ToList() : _resultado;
            //   gv_TemporadaEliminado.DataSource = _resultado.Count > 0 ? _resultado.Where(x => x.Estado == (int)Enums.Estado.Pasivo).ToList() : _resultado;
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
                    break;
                case "Eliminar":
                    ActivarEliminar((int)Enums.Estado.Pasivo);
                    ConsultarListado();
                    gv_Temporada.DataBind();
                    //   gv_TemporadaEliminado.DataBind();
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
            SGF_ProveedorPrecio _proveedorPrecio = new SGF_ProveedorPrecio();
            _proveedorPrecio.ProveedorPrecioID = String.IsNullOrEmpty(hdn_ProveedorPrecioID.Value) ? Guid.NewGuid() : new Guid(hdn_ProveedorPrecioID.Value);
            _proveedorPrecio.ProveedorID = cmb_Proveedor.SelectedValue;
            _proveedorPrecio.VariedadID = new Guid(cmb_Variedad.SelectedValue);
            _proveedorPrecio.Observaciones = txt_Observaciones.Text;
            _proveedorPrecio.FechaRegistro = DateTime.Now;
            _proveedorPrecio.Usuario = Me.Usuario.NombreUsuario;
            _proveedorPrecio.Estado = estado;
            _proveedorPrecio.EmpresaID = Me.Usuario.EmpresaID;
            try
            {
                client.ProveedorPrecio_Eliminar(_proveedorPrecio, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                VerMensaje("INFORMACIÓN", "info", "info", "Registro actualizado correctamente.");
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Hubo problemas al grabar el Registro: " + ex.Message.ToString());
            }
        }
        private void LimpiarControles()
        {
            // cmb_EliminadoProveedor.SelectedValue = "0";
            //cmb_EliminadoVariedad.SelectedValue = Guid.Empty.ToString();
            cmb_Proveedor.SelectedValue = "0";
            cmb_Variedad.SelectedValue = Guid.Empty.ToString();
            //txt_EliminadoEstado.Text = "";
            //txt_EliminadoObservaciones.Text = "";
            txt_Estado.Text = "";
            txt_Observaciones.Text = "";
            hdn_ProveedorPrecioID.Value = null;
            txt_EmpresaRUC.Text = Me.Usuario.EmpresaID;
        }
        private void Cancelar()
        {
            pnl_Datos.Visible = true;
            pnl_Buscador.Visible = true;
            pnl_Variedad.Visible = false;
            pnl_Detalle.Visible = false;
            LimpiarControles();
        }
        private void CancelarEliminar()
        {
            //  pnl_DatosEliminado.Visible = true;
            pnl_Buscador.Visible = true;
            //pnl_VariedadEliminada.Visible = false;
            LimpiarControles();
        }

        private void Grabar()
        {
            LogicClient client = new LogicClient();
            if (cmb_Proveedor.SelectedValue == "0")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Proveedor");
                return;
            }
            if (cmb_Variedad.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Variedad");
                return;
            }
            if (txt_PrecioNegociacion.Value == 0)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Precio de Negociación");
                return;
            }
            int _resultado = client.ProveedorPrecio_ValidarExistencia(cmb_Proveedor.SelectedValue, new Guid(cmb_Variedad.SelectedValue), Me.Usuario.EmpresaID);
            if (_resultado > 0)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Ya existe un registro con los mismos datos");
                return;
            }
            SGF_ProveedorPrecio _proveedorPrecio = new SGF_ProveedorPrecio();
            _proveedorPrecio.ProveedorPrecioID = String.IsNullOrEmpty(hdn_ProveedorPrecioID.Value) ? Guid.NewGuid() : new Guid(hdn_ProveedorPrecioID.Value);
            _proveedorPrecio.ProveedorID = cmb_Proveedor.SelectedValue;
            _proveedorPrecio.VariedadID = new Guid(cmb_Variedad.SelectedValue);
            _proveedorPrecio.CalidadID = Guid.Empty;
            _proveedorPrecio.LongitudID = Guid.Empty;
            _proveedorPrecio.EmpresaID = Me.Usuario.EmpresaID;
            _proveedorPrecio.ValorNegociacion = (decimal)txt_PrecioNegociacion.Value;
            _proveedorPrecio.ValorEstablecido = (decimal)txt_PrecioNegociacion.Value;
            _proveedorPrecio.Observaciones = txt_Observaciones.Text;
            _proveedorPrecio.FechaRegistro = DateTime.Now;
            _proveedorPrecio.Usuario = Me.Usuario.NombreUsuario;
            _proveedorPrecio.Estado = (int)Enums.Estado.Activo;
            try
            {
                client.ProveedorPrecio_Grabar(_proveedorPrecio, Utils.getIP(), Utils.getHostName(Utils.getIP()));
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
            gv_Temporada.DataBind();
        }

        protected void gv_Temporada_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_ProveedorPrecioID.Value = e.CommandArgument.ToString();
                    pnl_Datos.Visible = false;
                    pnl_Buscador.Visible = false;
                    pnl_Variedad.Visible = true;
                    CargarDatos();
                    EsNuevo = false;
                    tool_principal.Items[1].Visible = true;
                    tool_principal.Items[0].Visible = false;
                    break;
            }
        }
        private void CargarDatos()
        {
            if (new Guid(hdn_ProveedorPrecioID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_ProveedorPrecio _temporada = client.ProveedorPrecio_ObtenerPorID(new Guid(hdn_ProveedorPrecioID.Value));
            txt_EmpresaRUC.Text = _temporada.EmpresaID.ToString();
            //cmb_Empresa_SelectedIndexChanged(null, null);
            cmb_Proveedor.SelectedValue = _temporada.ProveedorID.ToString();
            cmb_Variedad.SelectedValue = _temporada.VariedadID.ToString();
            txt_PrecioNegociacion.Value = (double)_temporada.ValorNegociacion;
            txt_Observaciones.Text = _temporada.Observaciones.ToString();
            txt_Estado.Text = ObtenerNombreEstado((int)_temporada.Estado);
        }

        private void CargarDatosEliminado()
        {
            if (new Guid(hdn_ProveedorPrecioID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_ProveedorVariedad _temporada = client.ProveedorVariedad_ObtenerPorID(new Guid(hdn_ProveedorPrecioID.Value));
            //cmb_EliminadoProveedor.SelectedValue = _temporada.ProveedorID.ToString();
            //cmb_EliminadoVariedad.SelectedValue = _temporada.VariedadID.ToString();
            //txt_EliminadoObservaciones.Text = _temporada.Observaciones.ToString();
            //txt_EliminadoEstado.Text = ObtenerNombreEstado((int)_temporada.Estado);
        }
        protected void tool_principal_eliminar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Activar":
                    LogicClient client = new LogicClient();
                    //int _resultado = client.ProveedorVariedad_ValidarExistencia(cmb_EliminadoProveedor.SelectedValue, new Guid(cmb_EliminadoVariedad.SelectedValue));
                    //if (_resultado > 0)
                    //{
                    //    VerMensaje("INFORMACIÓN", "info", "info", "Ya existe un registro con los mismos datos");
                    //    return;
                    //}
                    //ActivarEliminar((int)Enums.Estado.Activo);
                    ConsultarListado();
                    gv_Temporada.DataBind();
                    // gv_TemporadaEliminado.DataBind();
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
            // gv_TemporadaEliminado.DataBind();
        }

        protected void gv_TemporadaEliminado_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_ProveedorPrecioID.Value = e.CommandArgument.ToString();
                    //    pnl_DatosEliminado.Visible = false;
                    pnl_Buscador.Visible = false;
                    //   pnl_VariedadEliminada.Visible = true;
                    CargarDatosEliminado();
                    break;
            }
        }

        protected void chk_Calidad_Click(object sender, EventArgs e)
        {
            if (chk_Calidad.Checked == true)
                pnl_Calidad.Enabled = true;
            else
                pnl_Calidad.Enabled = false;
        }

        protected void chk_Longitud_Click(object sender, EventArgs e)
        {
            if (chk_Longitud.Checked == true)
                pnl_Longitud.Enabled = true;
            else
                pnl_Longitud.Enabled = false;

        }

        //protected void cmb_Empresa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    if (cmb_Empresa.SelectedValue != "0")
        //    {
        //        LogicClient client = new LogicClient();
        //        var _proveedores = client.Proveedor_ObtenerPorEmpresaID(cmb_Empresa.SelectedValue.ToString());
        //        cmb_Proveedor.DataSource = _proveedores;
        //        cmb_Proveedor.DataTextField = "CO_PRO_NOM";
        //        cmb_Proveedor.DataValueField = "CO_PRO_COD";
        //        cmb_Proveedor.DataBind();
        //        cmb_Proveedor.Items.Insert(0, new RadComboBoxItem("Seleccionar el Proveedor", "0"));
        //    }
        //    else
        //    {
        //        cmb_Proveedor.DataSource = null;
        //        cmb_Proveedor.DataBind();
        //    }
        //}
    }
}