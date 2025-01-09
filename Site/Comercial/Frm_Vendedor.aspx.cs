using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Comercial
{
    public partial class Frm_Vendedor : System.Web.UI.Page
    {
        public bool EsNuevo
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
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                EsNuevo = true;
                llenarGrid();
                gv_Vendedor.DataBind();
            }
        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }

        protected void LimpiarControles()
        {
            hdn_VendedorID.Value = null;
            gv_Vendedor.DataSource = null;
            txt_EmpresaID.Text = "";
            txt_VendedorCedula.Text = "";
            txt_VendedorCelular.Text = "";
            txt_VendedorDireccion.Text = "";
            txt_VendedorEmail.Text = "";
            txt_VendedorEstado.Text = "";
            txt_VendedorNombre.Text = "";
            txt_VendedorTelefono.Text = "";
            EsNuevo = false;
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_VendedorID.Value = Guid.Empty.ToString();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            EsNuevo = true;

        }

        protected void gv_Vendedor_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_VendedorID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    EsNuevo = false;
                    CargarDatos();
                    break;
            }
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.Vendedor_ObtenerTodo();
            if (rbt_Nombre.Checked == false && rbt_CedulaRUC.Checked == false)
                gv_Vendedor.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.CO_VEN_NOM).ToList() : _parametro;
            else
            {
                if (rbt_CedulaRUC.Checked == true)
                    gv_Vendedor.DataSource = _parametro.Count() > 0 ? _parametro.Where(x => x.CO_VEN_CED.Trim() == txt_BuscNombre.Text.Trim()).OrderBy(x => x.CO_VEN_NOM).ToList() : _parametro;
                else
                    gv_Vendedor.DataSource = _parametro.Count() > 0 ? _parametro.Where(x => x.CO_VEN_NOM.ToUpper().Contains(txt_BuscNombre.Text.ToUpper())).OrderBy(x => x.CO_VEN_NOM).ToList() : _parametro;
            }
        }
        protected void CargarDatos()
        {
            if (hdn_VendedorID.Value == null) return;
            LogicClient client = new LogicClient();
            CO_VENDEDOR _vendedor = client.Vendedor_ObtenerPorID(hdn_VendedorID.Value);
            if (_vendedor != null)
            {
                txt_EmpresaID.Text = _vendedor.CO_EMP_RUC;
                txt_VendedorCedula.Text = _vendedor.CO_VEN_CED;
                txt_VendedorCelular.Text = _vendedor.CO_VEN_CEL;
                txt_VendedorDireccion.Text = _vendedor.CO_VEN_DIR;
                txt_VendedorEmail.Text = _vendedor.CO_VEN_EMAIL;
                txt_VendedorEstado.Text = ObtenerNombreEstado(_vendedor.CO_ESTADO == true ? 1 : 0);
                txt_VendedorNombre.Text = _vendedor.CO_VEN_NOM;
                txt_VendedorTelefono.Text = _vendedor.CO_VEN_TEL;
            }
        }
        protected void gv_Vendedor_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
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
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }

        private void Eliminar()
        {
            LogicClient client = new LogicClient();
            client.Vendedor_Eliminar(hdn_VendedorID.Value, Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registro eliminado.", "info");
            Cancelar();
            llenarGrid();
            gv_Vendedor.DataBind();
        }
        private void Grabar()
        {
            string Message = "";

            if (txt_VendedorCedula.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar la identificación del Vendedor.", "info");
                return;
            }
            if (txt_VendedorNombre.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar el Nombre del Vendedor.", "info");
                return;
            }
            try
            {
                CO_VENDEDOR newVendedor = new CO_VENDEDOR();
                newVendedor.CO_VEN_CED = EsNuevo == true ? txt_VendedorCedula.Text : hdn_VendedorID.Value;
                newVendedor.CO_EMP_RUC = txt_EmpresaID.Text;
                newVendedor.CO_VEN_CEL = txt_VendedorCelular.Text;
                newVendedor.CO_VEN_NOM = txt_VendedorNombre.Text;
                newVendedor.CO_VEN_TEL = txt_VendedorTelefono.Text;
                newVendedor.CO_VEN_EMAIL = txt_VendedorEmail.Text;
                newVendedor.CO_VEN_DIR = txt_VendedorDireccion.Text;
                newVendedor.CO_VEN_CTA = Me.Usuario.NombreUsuario;
                newVendedor.CO_ESTADO = true;
                LogicClient client = new LogicClient();
                client.Vendedor_Grabar(newVendedor, Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
                Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_Vendedor.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
            gv_Vendedor.DataBind();
        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            rbt_CedulaRUC.Checked = false;
            rbt_Nombre.Checked = false;
            txt_BuscNombre.Text = "";
            llenarGrid();
            gv_Vendedor.DataBind();
        }
    }
}