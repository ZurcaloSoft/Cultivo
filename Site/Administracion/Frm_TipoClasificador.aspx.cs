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
    public partial class Frm_TipoClasificador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                llenarGrid();
                gv_TipoClasificador.DataBind();
            }
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            hdn_TipoClasificadorID.Value = Guid.Empty.ToString();
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            llenarGrid();
            gv_TipoClasificador.DataBind();
        }

        protected void gv_TipoClasificador_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_TipoClasificadorID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }

        protected void gv_TipoClasificador_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
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
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            List<SGF_TipoClasificador> _modulo = new List<SGF_TipoClasificador>();
            if (txt_BuscarNombre.Text == "")
                _modulo = client.TipoClasificador_ObtenerTodo();
            else
                _modulo = client.TipoClasificador_ObtenerPorNombre(txt_BuscarNombre.Text);
            gv_TipoClasificador.DataSource = _modulo;
        }
        protected void LimpiarControles()
        {
            txt_BuscarNombre.Text = "";
            hdn_TipoClasificadorID.Value = null;
            gv_TipoClasificador.DataSource = null;
            txt_Nombre.Text = "";
          //  txt_Observaciones.Text = "";
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
            if (new Guid(hdn_TipoClasificadorID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_TipoClasificador _tipoClasificador= client.TipoClasificador_ObtenerPorID(new Guid(hdn_TipoClasificadorID.Value));
            if (_tipoClasificador != null)
            {
                txt_Estado.Text = ObtenerNombreEstado((int)_tipoClasificador.Estado);
                txt_Nombre.Text = _tipoClasificador.Nombre;
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
                SGF_TipoClasificador newTipoClasificador = new SGF_TipoClasificador();
                newTipoClasificador.TipoClasificadorID = new Guid(hdn_TipoClasificadorID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_TipoClasificadorID.Value);
                newTipoClasificador.Nombre = txt_Nombre.Text;
                newTipoClasificador.Estado = 1;
                LogicClient client = new LogicClient();
                client.TipoClasificador_Grabar(newTipoClasificador);
                VerMensaje("INFORMACIÓN", "info", "info", "Tipo de Clasificador registrado correctamente.");
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