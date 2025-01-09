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
    public partial class Frm_Modulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
            }
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            LogicClient client = new LogicClient();
            List<SGF_Modulo> _modulo = new List<SGF_Modulo>();
            if (txt_BuscarNombre.Text == "")
                _modulo=client.Modulo_ObtenerTodo();
            else
                _modulo = client.Modulol_ObtenerPorNombre(txt_BuscarNombre.Text);
            gv_Modulo.DataSource = _modulo;
            gv_Modulo.DataBind();
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            hdn_ModuloID.Value = Guid.Empty.ToString();
        }

        protected void gv_Modulo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_Modulo_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_ModuloID.Value = e.CommandArgument.ToString();
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
            hdn_ModuloID.Value = null;
            gv_Modulo.DataSource = null;
            txt_Nombre.Text = "";
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
            if (new Guid(hdn_ModuloID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_Modulo _modulo = client.Modulo_ObtenerPorID(new Guid(hdn_ModuloID.Value));
            if (_modulo != null)
            {
                txt_Estado.Text = ObtenerNombreEstado(_modulo.Estado);
                txt_Nombre.Text = _modulo.Nombre;
                txt_Observaciones.Text = _modulo.Descripcion;
            }
        }
        private void Grabar()
        {
            if (txt_Nombre.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el nombre del Módulo");
                return;
            }
            try
            {
                SGF_Modulo newModulo = new SGF_Modulo();
                newModulo.ModuloID = new Guid(hdn_ModuloID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_ModuloID.Value);
                newModulo.Nombre = txt_Nombre.Text;
                newModulo.Descripcion = txt_Observaciones.Text;
                newModulo.Estado = 1;
                LogicClient client = new LogicClient();
                client.Modulo_Grabar(newModulo);
                VerMensaje("INFORMACIÓN", "info", "info", "Módulo registrado correctamente.");
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