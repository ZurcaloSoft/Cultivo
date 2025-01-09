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
    public partial class Frm_Sucursal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                Cargar_Grupo(cmb_EmpresaBuscar);
                Cargar_Grupo(cmb_Empresa);
            }
        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }
        private void Cargar_Grupo(RadComboBox rcbGrupo)
        {
            LogicClient client = new LogicClient();
            List<CO_EMPRESA> entidad = client.Empresa_ObtenerTodo();
            rcbGrupo.DataSource = entidad.OrderBy(t => t.CO_EMP_RAZ_SOC);
            rcbGrupo.DataValueField = "CO_EMP_RUC";
            rcbGrupo.DataTextField = "CO_EMP_NOM";
            rcbGrupo.DataBind();
            rcbGrupo.SelectedValue = Guid.Empty.ToString();
        }
        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_SucursalID.Value = Guid.Empty.ToString();
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
        }
        protected void LimpiarControles()
        {
            hdn_SucursalID.Value = null;
            gv_Sucursal.DataSource = null;
            txt_Nombre.Text = "";
            txt_Telefono.Text = "";
            txt_Direccion.Text = "";
            txt_Estado.Text = "";
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {

        }
        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            //LogicClient client = new LogicClient();
            //if (new Guid(cmb_EmpresaBuscar.SelectedValue) != Guid.Empty)
            //{
            //    gv_Sucursal.DataSource = client.Sucursal_ObtenerPorEmpresa(new Guid(cmb_EmpresaBuscar.SelectedValue));
            //}
            //else
            //{
            //    llenarGrid();
            //}
            //gv_Sucursal.DataBind();
        }
        protected void llenarGrid()
        {
            //LogicClient client = new LogicClient();
            //var _parametro = client.Sucursal_ObtenerTodo();
            //gv_Sucursal.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.Nombre).ToList() : _parametro;
        }

        protected void gv_Sucursal_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_SucursalID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            //if (new Guid(hdn_SucursalID.Value) == Guid.Empty) return;
            //LogicClient client = new LogicClient();
            //SGF_Sucursal _sucursal = client.Sucursal_ObtenerPorID(new Guid(hdn_SucursalID.Value));
            //if (_sucursal != null)
            //{
            //    cmb_Empresa.SelectedValue = _sucursal.EmpresaID.ToString();
            //    txt_Nombre.Text = _sucursal.Nombre;
            //    txt_Telefono.Text = _sucursal.Telefono;
            //    txt_Direccion.Text = _sucursal.Direccion;
            //    txt_Estado.Text = ObtenerNombreEstado((int)_sucursal.Estado);
            //}
        }
        protected void gv_Sucursal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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
            //LogicClient client = new LogicClient();
            //client.Sucursal_Eliminar(new Guid(hdn_SucursalID.Value));
            //Utils.MessageBox(this, "", "Registrado eliminado.", "info");

            //Cancelar();
            //llenarGrid();
            //gv_Sucursal.DataBind();
        }
        private void Grabar()
        {
            string Message = "";

            if (txt_Nombre.Text == "")
            {
                Message += "Debe ingresar nombre de Sucursal." + Environment.NewLine;
            }
            //if (cmb_Empresa.SelectedValue.ToString() != string.Empty)
            //{
            //    Message += "Debe seleccionar empresa." + Environment.NewLine;
            //}

            if (Message != string.Empty)
            {
                Utils.MessageBox(this, "", "" + Message, "info");
                return;
            }

            try
            {
                //SGF_Sucursal newSucursal = new SGF_Sucursal();
                //newSucursal.SucursalID = new Guid(hdn_SucursalID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_SucursalID.Value);
                //newSucursal.EmpresaID = new Guid(cmb_Empresa.SelectedValue);
                //newSucursal.Nombre = txt_Nombre.Text;
                //newSucursal.Telefono = txt_Telefono.Text;
                //newSucursal.Direccion = txt_Direccion.Text;
                //newSucursal.Estado = 1;
                //LogicClient client = new LogicClient();
                //client.Sucursal_Grabar(newSucursal);
                //Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                //Cancelar();
                //llenarGrid();
                //gv_Sucursal.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }
    }
}