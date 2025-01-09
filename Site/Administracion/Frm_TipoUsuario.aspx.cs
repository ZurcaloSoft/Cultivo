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
    public partial class Frm_TipoUsuario : System.Web.UI.Page
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
            List<Frm_TipoUsuario> _tipousuario = new List<Frm_TipoUsuario>();
            if (txt_BuscarNombre.Text == "")
                gv_TipoUsuario.DataSource = client.TipoUsuario_ObtenerTodo();
            else
                gv_TipoUsuario.DataSource = client.TipoUsuario_ObtenerPorUsername(txt_BuscarNombre.Text);

            //gv_TipoUsuario.DataSource = _tipousuario;
            gv_TipoUsuario.DataBind();
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            hdn_TipoUsuarioID.Value = Guid.Empty.ToString();
        }

        protected void gv_TipoUsuario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_TipoUsuario_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_TipoUsuarioID.Value = e.CommandArgument.ToString();
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

        protected string ObtenerNombreEstado(string Estado)
        {
            switch (Estado)
            {
                case "False":
                    return "Pasivo";
                case "True":
                    return "Activo";
                case "2":
                    return "Pasivo";
            }
            return "";
        }

        protected void LimpiarControles()
        {
            txt_BuscarNombre.Text = "";
            hdn_TipoUsuarioID.Value = null;
            gv_TipoUsuario.DataSource = null;
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
            if (new Guid(hdn_TipoUsuarioID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_TipoUsuario _tipousuario = client.TipoUsuario_ObtenerPorID(new Guid(hdn_TipoUsuarioID.Value));
            if (_tipousuario != null)
            {
                txt_Estado.Text = ObtenerNombreEstado(_tipousuario.Estado.ToString());
                txt_Nombre.Text = _tipousuario.Nombre;
                txt_Observaciones.Text = _tipousuario.Descripcion;
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
                SGF_TipoUsuario newTipoUsuario = new SGF_TipoUsuario();
                newTipoUsuario.TipoUsuarioID = new Guid(hdn_TipoUsuarioID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_TipoUsuarioID.Value);
                newTipoUsuario.Nombre = txt_Nombre.Text;
                newTipoUsuario.Descripcion = txt_Observaciones.Text;
                newTipoUsuario.Estado = true;
                LogicClient client = new LogicClient();
                client.TipoUsuario_Grabar(newTipoUsuario);
                VerMensaje("INFORMACIÓN", "info", "info", "Tipo de Usuario registrado correctamente.");
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