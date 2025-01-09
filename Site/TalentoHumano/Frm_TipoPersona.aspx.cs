using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
namespace SGF.Site.TalentoHumano
{
    public partial class Frm_TipoPersona : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.TalentoHumano;
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
        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            hdn_TipoPersonaID.Value = Guid.Empty.ToString();
        }
        protected void LimpiarControles()
        {
            txt_BuscarNombre.Text = "";
            hdn_TipoPersonaID.Value = null;
            gv_TipoPersona.DataSource = null;
            txt_Nombre.Text = "";
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";
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
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }

        private void Grabar()
        {
            if (txt_Nombre.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar el nombre ", "info");
                return;
            }
            try
            {
                SGF_TipoPersona newTipoPersona = new SGF_TipoPersona();
                newTipoPersona.TipoPersonaID = new Guid(hdn_TipoPersonaID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_TipoPersonaID.Value);
                newTipoPersona.Nombre = txt_Nombre.Text;
                newTipoPersona.Descripcion = txt_Observaciones.Text;
                newTipoPersona.Estado = true;
                LogicClient client = new LogicClient();
                client.TipoPersona_Grabar(newTipoPersona);
                Utils.MessageBox(this, "", "Tipo de Persona registrado correctamente.", "success");
                Cancelar();
                LimpiarControles();
                llenarGrid();
                gv_TipoPersona.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            List<SGF_TipoPersona> persona = client.TipoPersona_ObtenerTodo();
            if (persona != null)
            {
                gv_TipoPersona.DataSource = persona;
            }

        }
        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            LogicClient client = new LogicClient();
            if (txt_BuscarNombre.Text == "")
                gv_TipoPersona.DataSource = client.TipoPersona_ObtenerTodo();
            else
                gv_TipoPersona.DataSource = client.TipoPersona_ObtenerPorUsername(txt_BuscarNombre.Text);

            gv_TipoPersona.DataBind();
        }
        protected void gv_TipoPersona_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_TipoPersonaID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            if (new Guid(hdn_TipoPersonaID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_TipoPersona _tipoPersona = client.TipoPersona_ObtenerPorID(new Guid(hdn_TipoPersonaID.Value));
            if (_tipoPersona != null)
            {
                txt_Estado.Text = ObtenerNombreEstado(_tipoPersona.Estado.ToString());
                txt_Nombre.Text = _tipoPersona.Nombre;
                txt_Observaciones.Text = _tipoPersona.Descripcion;
            }
        }
        protected void gv_TipoPersona_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }
    }
}