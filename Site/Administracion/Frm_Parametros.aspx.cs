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
    public partial class Frm_Parametros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;

                llenarGrid();
                gv_Parametro.DataBind();
                cargarCombos();
            }
        }

        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();
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
                    return "Borrado";
                case 1:
                    return "Activo";
                case 2:
                    return "Borrado";
            }
            return "";
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_ParametroID.Value = Guid.Empty.ToString();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
        }
        protected void LimpiarControles()
        {
            hdn_ParametroID.Value = null;
            gv_Parametro.DataSource = null;
            txt_pkey.Text = "";
            txt_Estado.Text = "";
            txt_Parametro.Text = "";
            txt_Valor.Text = "";
            txt_Comentario.Text = "";
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
            client.Parametros_Eliminar(new Guid(hdn_ParametroID.Value), "Registro eliminado", Utils.getIP(), Utils.getHostName(Utils.getIP()), Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registrado eliminado.", "info");

            Cancelar();
            llenarGrid();
            gv_Parametro.DataBind();
        }
        private void Grabar()
        {
            if (txt_pkey.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar pkey.", "info");
                return;
            }
            if (txt_Parametro.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar el parametro.", "info");
                return;
            }

            try
            {
                SGF_Parametro newParametro = new SGF_Parametro();
                newParametro.ParametroID = new Guid(hdn_ParametroID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_ParametroID.Value);
                newParametro.Pkey = txt_pkey.Text;
                newParametro.TipoParametro = int.Parse( txt_Parametro.Text);
                newParametro.Valor = txt_Valor.Text;
                newParametro.Comentarios = txt_Comentario.Text;
                newParametro.Estado = 1;
                newParametro.EmpresaID = Me.Usuario.EmpresaID;
                LogicClient client = new LogicClient();
                client.Parametros_Grabar(newParametro, Utils.getIP(), Utils.getHostName(Utils.getIP()), Me.Usuario.NombreUsuario);
                Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_Parametro.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }
        protected void gv_Parametro_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_ParametroID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }

        protected void CargarDatos()
        {
            if (new Guid(hdn_ParametroID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_Parametro _clasificador = client.Parametros_ObtenerPorID(new Guid(hdn_ParametroID.Value));
            if (_clasificador != null)
            {
                txt_pkey.Text = _clasificador.Pkey;
                txt_Parametro.Text = _clasificador.TipoParametro.ToString();
                txt_Estado.Text = ObtenerNombreEstado((int)_clasificador.Estado);
                txt_Comentario.Text = _clasificador.Comentarios;
                txt_Valor.Text = _clasificador.Valor;
            }
        }

        protected void gv_Parametro_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.Parametros_ObtenerTodo();
            gv_Parametro.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.TipoParametro).ToList() : _parametro;
        }
    }
}