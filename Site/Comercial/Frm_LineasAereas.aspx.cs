using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Skins;

namespace SGF.Site.Comercial
{
    public partial class Frm_LineasAereas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
            }

        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }
        protected void LimpiarControles()
        {
            hdn_LineAerealID.Value = null;
            gv_LineAerea.DataSource = null;
            txt_Codigo.Text = "";
            txt_Nombre.Text = "";
            txt_CedRUC.Text = "";
            txt_CodCAE.Text = "";
            txt_CodSRI.Text = "";
            txt_PreGuia.Text = "";
            txt_Email.Text = "";
            txt_Observacion.Text = "";
            txt_Estado.Text = "";
            //txt_Usuario.Text = "";
            //rdp_Fecha.SelectedDate = null;
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_LineAerealID.Value = Guid.Empty.ToString();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
        }
        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            LogicClient client = new LogicClient();
            var _parametro = client.LineaAerea_ObtenerPorNombre(txt_BuscNombre.Text);
            gv_LineAerea.DataSource = _parametro;
        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            txt_BuscNombre.Text = "";
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.LineaAerea_ObtenerTodo();
            gv_LineAerea.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.Nombre).ToList() : _parametro;
        }
        protected void gv_LineAerea_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_LineAerealID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            if (new Guid(hdn_LineAerealID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_LineaAerea _sucursal = client.LineaAerea_ObtenerPorID(new Guid(hdn_LineAerealID.Value));
            if (_sucursal != null)
            {
                txt_Codigo.Text = _sucursal.Codigo;
                txt_Nombre.Text = _sucursal.Nombre;
                txt_CedRUC.Text = _sucursal.CedulaRUC;
                txt_CodCAE.Text = _sucursal.CodigoCAE;
                txt_CodSRI.Text = _sucursal.CodigoSRI;
                txt_PreGuia.Text = _sucursal.PrefijoGuia;
                txt_Email.Text = _sucursal.Email;
                txt_Observacion.Text = _sucursal.Observacion;
                //txt_Usuario.Text = _sucursal.Usuario;
                //rdp_Fecha.SelectedDate = _sucursal.Fecha;
                txt_Estado.Text = ObtenerNombreEstado((int)_sucursal.Estado);
            }
        }
        protected void gv_LineAerea_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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
            client.LineaAerea_Eliminar(new Guid(hdn_LineAerealID.Value),Utils.getHostName(Utils.getIP()),Utils.getIP(),Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registro eliminado.", "info");

            Cancelar();
            llenarGrid();
            gv_LineAerea.DataBind();
        }
        private void Grabar()
        {
            string Message = "";
            if (txt_Codigo.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Código");
                return;
            }
            if (txt_Nombre.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre");
                return;
            }      
            try
            {
                SGF_LineaAerea newLineaAerea = new SGF_LineaAerea();
                newLineaAerea.LineaAereaID = new Guid(hdn_LineAerealID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_LineAerealID.Value);
                newLineaAerea.Codigo = txt_Codigo.Text;
                newLineaAerea.Nombre = txt_Nombre.Text;
                newLineaAerea.CedulaRUC = txt_CedRUC.Text;
                newLineaAerea.CodigoCAE = txt_CodCAE.Text;
                newLineaAerea.CodigoSRI = txt_CodSRI.Text;
                newLineaAerea.PrefijoGuia = txt_PreGuia.Text;
                newLineaAerea.Email = txt_Email.Text;
                newLineaAerea.Observacion = txt_Observacion.Text;
                newLineaAerea.Usuario = Me.Usuario.NombreUsuario;
                newLineaAerea.Fecha = DateTime.Now;
                newLineaAerea.Estado = 1;
                LogicClient client = new LogicClient();
                client.LineaAerea_Grabar(newLineaAerea, Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
                VerMensaje("INFORMACIÓN", "info", "info", "Datos Registrado correctamente.");

                //Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_LineAerea.DataBind();
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Hubo un problema al momento de grabar el registro.");
                //Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
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