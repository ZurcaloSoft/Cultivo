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
    public partial class Frm_Almacen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                //    Cargar_Sucursal(cmb_AlmacenBuscar);
                //    Cargar_Sucursal(cmb_Almacen);
            }
        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }
        private void Cargar_Sucursal(RadComboBox rcbSucursal)
        {
        //    LogicClient client = new LogicClient();
        //    List<SGF_Sucursal> entidad = client.Sucursal_ObtenerTodo();
        //    rcbSucursal.DataSource = entidad.OrderBy(t => t.Nombre);
        //    rcbSucursal.DataValueField = "SucursalID";
        //    rcbSucursal.DataTextField = "Nombre";
        //    rcbSucursal.DataBind();
        //    rcbSucursal.SelectedValue = Guid.Empty.ToString();
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_AlmacenID.Value = "";
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            txt_Empresa.Text=Me.Usuario.EmpresaID;

        }
        protected void LimpiarControles()
        {
            hdn_AlmacenID.Value = null;
            gv_Almacen.DataSource = null;
            gv_Almacen.DataBind();
            txt_Nombre.Text = "";
            txt_Direccion.Text = "";
            txt_Estado.Text = "";
            txt_Codigo.Text = "";
            txt_Teléfono.Text = "";
            txt_Empresa.Text = "";
        }

        protected void gv_Almacen_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_AlmacenID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            if (hdn_AlmacenID.Value == string.Empty) return;
            LogicClient client = new LogicClient();
            CO_ALMACEN _almacen = client.Almacen_ObtenerPorID(int.Parse(hdn_AlmacenID.Value));
            if (_almacen != null)
            {
                //  cmb_Almacen.SelectedValue = _almacen.AlmacenID.ToString();
                txt_Codigo.Text = _almacen.CO_ALM_COD.ToString();
                txt_Empresa.Text = _almacen.CO_EMP_RUC.ToString();
                txt_Nombre.Text = _almacen.CO_ALM_NOM;
                txt_Teléfono.Text = _almacen.Telefono;
                txt_Direccion.Text = _almacen.Direccion;
                txt_Estado.Text = ObtenerNombreEstado((int)_almacen.Estado);
            }
        }
        protected void gv_Almacen_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }

        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.Almacen_ObtenerTodo();
            gv_Almacen.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.CO_ALM_COD).ToList() : _parametro;
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
            client.Almacen_Eliminar(int.Parse(hdn_AlmacenID.Value), "", Utils.getIP(), Utils.getHostName(Utils.getIP()), Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registrado eliminado.", "info");

            Cancelar();
            llenarGrid();
            gv_Almacen.DataBind();
        }
        private void Grabar()
        {
            string Message = "";

            if (txt_Nombre.Text == "")
            {
                Message += "Ingresar nombre de la Finca." + Environment.NewLine;
            }
            //if (cmb_Almacen.SelectedValue != string.Empty)
            //    //{
            //    Message += "Debe seleccionar sucursal." + Environment.NewLine;
            //}

            if (Message != string.Empty)
            {
                Utils.MessageBox(this, "", "" + Message, "info");
                return;
            }

            try
            {
                CO_ALMACEN newAlmacen = new CO_ALMACEN();
                newAlmacen.CO_ALM_COD = hdn_AlmacenID.Value == "" ? 0 : int.Parse(hdn_AlmacenID.Value);
                newAlmacen.CO_EMP_RUC = txt_Empresa.Text;
                newAlmacen.CO_ALM_NOM = txt_Nombre.Text;
                newAlmacen.Telefono = txt_Teléfono.Text;
                newAlmacen.Direccion = txt_Direccion.Text;
                newAlmacen.Estado = 1;
                LogicClient client = new LogicClient();
                client.Almacen_Grabar(newAlmacen, Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
                Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_Almacen.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            LogicClient client = new LogicClient();
            if (txt_BuscarNombre.Text != string.Empty)
                gv_Almacen.DataSource = client.Almacen_ObtenerPorNombre(txt_BuscarNombre.Text.ToUpper());
            else
                llenarGrid();
            gv_Almacen.DataBind();
        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {

        }
    }
}