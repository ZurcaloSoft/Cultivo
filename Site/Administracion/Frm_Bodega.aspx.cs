using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Administracion
{
    public partial class Frm_Bodega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;

                Cargar_Combos();
            }
        }

        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }
        private void Cargar_Combos()
        {
            LogicClient client = new LogicClient();
            List<CO_ALMACEN> entidad = client.Almacen_ObtenerPorEmpresa(Me.Usuario.EmpresaID);
            cmb_AlmacenBuscar.DataSource = entidad.OrderBy(t => t.CO_ALM_NOM);
            cmb_AlmacenBuscar.DataValueField = "CO_ALM_COD";
            cmb_AlmacenBuscar.DataTextField = "CO_ALM_NOM";
            cmb_AlmacenBuscar.DataBind();
            cmb_AlmacenBuscar.Items.Insert(0, new RadComboBoxItem("Seleccionar Finca", "0"));
            cmb_AlmacenBuscar.SelectedValue = "0";

            cmb_Almacen.DataSource = entidad.OrderBy(t => t.CO_ALM_NOM);
            cmb_Almacen.DataValueField = "CO_ALM_COD";
            cmb_Almacen.DataTextField = "CO_ALM_NOM";
            cmb_Almacen.DataBind();
            cmb_Almacen.Items.Insert(0, new RadComboBoxItem("Seleccionar Finca", "0"));
            cmb_Almacen.SelectedValue = "0";

            var _clasificador = client.Clasificador_ObtenerPorTipoClasificador(TipoClasificador.TipoBodega);
            cmb_TipoBodega.DataSource = _clasificador.OrderBy(t => t.Nombre);
            cmb_TipoBodega.DataValueField = "ClasificadorID";
            cmb_TipoBodega.DataTextField = "Nombre";
            cmb_TipoBodega.DataBind();
            cmb_TipoBodega.Items.Insert(0, new RadComboBoxItem("Seleccionar Finca", Guid.Empty.ToString()));
            cmb_TipoBodega.SelectedValue = Guid.Empty.ToString();
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            LogicClient client = new LogicClient();
            if (cmb_AlmacenBuscar.SelectedValue != "0")
            {
                gv_Bodega.DataSource = client.Bodega_ObtenerPorAlmacenEmpresa_VTA(Me.Usuario.EmpresaID, int.Parse(cmb_AlmacenBuscar.SelectedValue));
            }
            else
            {
                llenarGrid();
            }
            gv_Bodega.DataBind();
        }
        protected void LimpiarControles()
        {
            hdn_BodegaID.Value = null;
            gv_Bodega.DataSource = null;
            cmb_TipoBodega.SelectedValue = Guid.Empty.ToString();
            txt_Nombre.Text = "";
            txt_Ubicacion.Text = "";
            txt_Estado.Text = "";
            txt_Empresa.Text = "";
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            hdn_BodegaID.Value = "0";
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.Bodega_ObtenerPorEmpresa_VTA(Me.Usuario.EmpresaID);
            gv_Bodega.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.CO_BOD_NOM).ToList() : _parametro;
        }

        protected void gv_Bodega_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }

        protected void gv_Bodega_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_BodegaID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            if (hdn_BodegaID.Value == string.Empty) return;
            LogicClient client = new LogicClient();
            CO_BODEGA _bodega = client.Bodega_ObtenerPorID(int.Parse(hdn_BodegaID.Value));
            if (_bodega != null)
            {
                cmb_Almacen.SelectedValue = _bodega.CO_ALM_COD.ToString();
                cmb_TipoBodega.SelectedValue = _bodega.TipoBodegaID.ToString();
                txt_Nombre.Text = _bodega.CO_BOD_NOM;
                txt_Ubicacion.Text = _bodega.Ubicacion;
                txt_Empresa.Text = _bodega.CO_EMP_RUC;
                txt_Estado.Text = ObtenerNombreEstado((int)_bodega.Estado);
            }
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
                case "Eliminar":
                    Eliminar();
                    break;
            }
        }
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscar.Visible = true;
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }
        private void Eliminar()
        {
            LogicClient client = new LogicClient();
            client.Bodega_Eliminar(int.Parse(hdn_BodegaID.Value), "", Utils.getIP(), Utils.getHostName(Utils.getIP()),  Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registrado eliminado.", "info");

            Cancelar();
            llenarGrid();
            gv_Bodega.DataBind();
        }
        private void Grabar()
        {
            string Message = "";

            if (txt_Nombre.Text == "")
            {
                Message += "Debe ingresar Nombre." + Environment.NewLine;
            }
            if (cmb_Almacen.SelectedValue == "0")
            {
                Message += "Debe seleccionar La Finca." + Environment.NewLine;
            }
            if (cmb_TipoBodega.SelectedValue == Guid.Empty.ToString())
            {
                Message += "Debe seleccionar el Tipo de Bodega." + Environment.NewLine;
            }
            if (Message != string.Empty)
            {
                Utils.MessageBox(this, "", "" + Message, "info");
                return;
            }
            try
            {
                CO_BODEGA newBodega = new CO_BODEGA();
                newBodega.CO_BOD_COD = hdn_BodegaID.Value == ""? 0 : int.Parse(hdn_BodegaID.Value);
                newBodega.CO_ALM_COD = int.Parse(cmb_Almacen.SelectedValue);
                newBodega.CO_EMP_RUC = txt_Empresa.Text;
                newBodega.CO_BOD_NOM = txt_Nombre.Text;
                newBodega.TipoBodegaID = new Guid(cmb_TipoBodega.SelectedValue);
                newBodega.Ubicacion = txt_Ubicacion.Text;
                newBodega.Estado = 1;
                LogicClient client = new LogicClient();
                client.Bodega_Grabar(newBodega, Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
                Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_Bodega.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            pnl_Buscador.Visible = false;
            pnl_Buscar.Visible = false;
            pnl_Datos.Visible = true;
            txt_Empresa.Text = Me.Usuario.EmpresaID;

        }
    }
}