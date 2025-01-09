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
    public partial class Frm_Grupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;

                llenarGrid();
                gv_Grupo.DataBind();
            }

        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_GrupoID.Value = Guid.Empty.ToString();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
        }
        protected void LimpiarControles()
        {
            hdn_GrupoID.Value = null;
            gv_Grupo.DataSource = null;
            txt_Codigo.Text = "";
            txt_Nombre.Text = "";
            txt_Observacion.Text = "";
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
            client.Grupo_Eliminar(new Guid(hdn_GrupoID.Value), "Registro eliminado");
            Utils.MessageBox(this, "", "Registrado eliminado.", "info");

            Cancelar();
            llenarGrid();
            gv_Grupo.DataBind();
        }
        private void Grabar()
        {
            if (txt_Codigo.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar codigo.", "info");
                return;
            }
            if (txt_Nombre.Text == "")
            {
                Utils.MessageBox(this, "", "Debe ingresar el nombre.", "info");
                return;
            }

            try
            {
                SGF_Grupo newGrupo= new SGF_Grupo();
                newGrupo.GrupoID = new Guid(hdn_GrupoID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_GrupoID.Value);
                newGrupo.Codigo = txt_Codigo.Text;
                newGrupo.Nombre =txt_Nombre.Text;
                newGrupo.Observaciones = txt_Observacion.Text;
                newGrupo.Estado = 1;
                LogicClient client = new LogicClient();
                client.Grupo_Grabar(newGrupo);
                Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_Grupo.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.Grupo_ObtenerTodo();
            gv_Grupo.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.Nombre).ToList() : _parametro;
        }
        protected void gv_Grupo_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_GrupoID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            if (new Guid(hdn_GrupoID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_Grupo _grupo = client.Grupo_ObtenerPorID(new Guid(hdn_GrupoID.Value));
            if (_grupo != null)
            {
                txt_Codigo.Text = _grupo.Codigo;
                txt_Nombre.Text = _grupo.Nombre.ToString();
                txt_Estado.Text = ObtenerNombreEstado((int)_grupo.Estado);
                txt_Observacion.Text = _grupo.Observaciones;
            }
        }
        protected void gv_Grupo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }
    }
}