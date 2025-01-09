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
    public partial class Frm_Formulario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                cargarCombos();
            }

        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            hdn_FormularioID.Value = Guid.Empty.ToString();
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            LogicClient client = new LogicClient();
            var _formulario = client.Formulario_ObtenerPorNombre_ModuloID_VTA(new Guid(cmb_BuscarModulo.SelectedValue), txt_BuscarNombre.Text);
            gv_Formulario.DataSource = _formulario;
            gv_Formulario.DataBind();
        }

        protected void gv_Formulario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_Formulario_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_FormularioID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }

        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
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

        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();

            cmb_BuscarModulo.DataSource = client.Modulo_ObtenerTodo();
            cmb_BuscarModulo.DataTextField = "Nombre";
            cmb_BuscarModulo.DataValueField = "ModuloID";
            cmb_BuscarModulo.DataBind();
            cmb_BuscarModulo.Items.Insert(0, new RadComboBoxItem("Seleccione el Módulo", Guid.Empty.ToString()));

            cmb_Modulo.DataSource = client.Modulo_ObtenerTodo();
            cmb_Modulo.DataTextField = "Nombre";
            cmb_Modulo.DataValueField = "ModuloID";
            cmb_Modulo.DataBind();
            cmb_Modulo.Items.Insert(0, new RadComboBoxItem("Seleccione el Módulo", Guid.Empty.ToString()));

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

        protected void LimpiarControles()
        {
            txt_BuscarNombre.Text = "";
            cmb_BuscarModulo.SelectedValue = Guid.Empty.ToString();
            cmb_Modulo.SelectedValue = Guid.Empty.ToString();
            hdn_FormularioID.Value = null;
            gv_Formulario.DataSource = null;
            txt_Codigo.Text = "";
            txt_Estado.Text = "";
            txt_NombreFormulario.Text = "";
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";
        }

        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }
        protected void CargarDatos()
        {
            if (new Guid(hdn_FormularioID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_Formulario _formulario = client.Formulario_ObtenerPorID(new Guid(hdn_FormularioID.Value));
            if (_formulario != null)
            {
                cmb_Modulo.SelectedValue = _formulario.ModuloID.ToString();
                txt_Codigo.Text = _formulario.Codigo;
                txt_Estado.Text = ObtenerNombreEstado(_formulario.Estado);
                txt_NombreFormulario.Text = _formulario.Nombre;
                txt_Observaciones.Text = _formulario.Descripcion;
            }
        }
        private void Grabar()
        {
            if (txt_NombreFormulario.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el nombre del Formulario");
                return;
            }
            if (cmb_Modulo.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Módulo ");
                return;
            }
            try
            {
                SGF_Formulario newFormulario = new SGF_Formulario();
                newFormulario.FormularioID = new Guid(hdn_FormularioID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_FormularioID.Value);
                newFormulario.Codigo = txt_Codigo.Text;
                newFormulario.Nombre = txt_NombreFormulario.Text;
                newFormulario.ModuloID = new Guid(cmb_Modulo.SelectedValue);
                newFormulario.Descripcion = txt_Observaciones.Text;
                newFormulario.Estado = 1;
                LogicClient client = new LogicClient();
                client.Formulario_Grabar(newFormulario);
                VerMensaje("INFORMACIÓN", "info", "info", "Formulario registrado correctamente.");
                Cancelar();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Hubo un problema al momento de grabar el registro.");
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
    }
}