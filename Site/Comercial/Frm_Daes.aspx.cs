using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Comercial
{
    public partial class Frm_Daes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                cargarCombos();
            }

        }

        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();
            cmb_BuscarPais.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Pais);
            cmb_BuscarPais.DataTextField = "Nombre";
            cmb_BuscarPais.DataValueField = "ClasificadorID";
            cmb_BuscarPais.DataBind();
            cmb_BuscarPais.Items.Insert(0, new RadComboBoxItem("Seleccione el País", Guid.Empty.ToString()));

            cmb_BuscarAduana.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Aduanas);
            cmb_BuscarAduana.DataTextField = "Nombre";
            cmb_BuscarAduana.DataValueField = "ClasificadorID";
            cmb_BuscarAduana.DataBind();
            cmb_BuscarAduana.Items.Insert(0, new RadComboBoxItem("Seleccione la Aduana", Guid.Empty.ToString()));

            cmb_Pais.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Pais);
            cmb_Pais.DataTextField = "Nombre";
            cmb_Pais.DataValueField = "ClasificadorID";
            cmb_Pais.DataBind();
            cmb_Pais.Items.Insert(0, new RadComboBoxItem("Seleccione el País", Guid.Empty.ToString()));

            cmb_Aduana.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Aduanas);
            cmb_Aduana.DataTextField = "Nombre";
            cmb_Aduana.DataValueField = "ClasificadorID";
            cmb_Aduana.DataBind();
            cmb_Aduana.Items.Insert(0, new RadComboBoxItem("Seleccione la Aduana", Guid.Empty.ToString()));

        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
            gv_Daes.DataBind();
        }

        private void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.DAES_ObtenerTodo_VTA();
            if (txt_BuscarDae.Text == "" && cmb_BuscarAduana.SelectedValue == Guid.Empty.ToString() && cmb_BuscarPais.SelectedValue == Guid.Empty.ToString() && rbt_BuscarFDesde.Checked == false && rbt_BuscarFHasta.Checked == false)
            {
                gv_Daes.DataSource = _parametro;
            }
            else
            {
                if (txt_BuscarDae.Text != "")
                    gv_Daes.DataSource = _parametro.Where(x => x.DAE.Contains(txt_BuscarDae.Text));
                if (cmb_BuscarAduana.SelectedValue != Guid.Empty.ToString())
                    gv_Daes.DataSource = _parametro.Where(x => x.AduanaID == new Guid(cmb_BuscarAduana.SelectedValue));
                if (cmb_BuscarPais.SelectedValue != Guid.Empty.ToString())
                    gv_Daes.DataSource = _parametro.Where(x => x.PaisID == new Guid(cmb_BuscarPais.SelectedValue));
                if (rbt_BuscarFDesde.Checked == true)
                {
                    if (dtp_BuscarFecha.SelectedDate == null)
                    {
                        VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Fecha de Búsqueda");
                        return;
                    }
                    gv_Daes.DataSource = _parametro.Where(x => x.Desde.Year == dtp_BuscarFecha.SelectedDate.Value.Year && x.Desde.Month == dtp_BuscarFecha.SelectedDate.Value.Month);
                }
                if (rbt_BuscarFHasta.Checked == true)
                {
                    if (dtp_BuscarFecha.SelectedDate == null)
                    {
                        VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Fecha de Búsqueda");
                        return;
                    }
                    gv_Daes.DataSource = _parametro.Where(x => x.Hasta.Year == dtp_BuscarFecha.SelectedDate.Value.Year && x.Hasta.Month == dtp_BuscarFecha.SelectedDate.Value.Month);
                }
            }
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            cmb_BuscarAduana.SelectedValue = Guid.Empty.ToString();
            cmb_BuscarPais.SelectedValue = Guid.Empty.ToString();
            txt_BuscarDae.Text = "";
            rbt_BuscarFDesde.Checked = false;
            rbt_BuscarFHasta.Checked = false;
            dtp_BuscarFecha.SelectedDate = null;
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

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_DaesID.Value = Guid.Empty.ToString();
            pnl_Buscador.Visible = false;
            pnl_Buscar.Visible = false;
            pnl_Datos.Visible = true;
        }

        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }

        protected void LimpiarControles()
        {
            hdn_DaesID.Value = null;
            gv_Daes.DataSource = null;
            cmb_Aduana.SelectedValue = Guid.Empty.ToString();
            cmb_Pais.SelectedValue = Guid.Empty.ToString();
            txt_DAE.Text = "";
            dtp_FechaDesde.SelectedDate = null;
            dtp_FechaHasta.SelectedDate = null;
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";

            txt_Estado.Text = "";
            //txt_Usuario.Text = "";
            //rdp_Fecha.SelectedDate = null;
        }


        protected void gv_Daes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void gv_Daes_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_DaesID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Buscar.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }

        protected void CargarDatos()
        {
            if (new Guid(hdn_DaesID.Value) == Guid.Empty) return;
            LogicClient client = new LogicClient();
            SGF_DAES_VTA _daes = client.DAES_ObtenerPorID_VTA(new Guid(hdn_DaesID.Value));
            if (_daes != null)
            {
                cmb_Aduana.SelectedValue = _daes.AduanaID.ToString();
                cmb_Pais.SelectedValue = _daes.PaisID.ToString();
                txt_DAE.Text = _daes.DAE;
                dtp_FechaDesde.SelectedDate = _daes.Desde;
                dtp_FechaHasta.SelectedDate = _daes.Hasta;
                txt_Observaciones.Text = _daes.Observaciones;
                txt_Estado.Text = ObtenerNombreEstado(_daes.Estado);
            }
        }

        protected void gv_Daes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_Buscar.Visible = true;
            pnl_Datos.Visible = false;
        }

        private void Eliminar()
        {
            LogicClient client = new LogicClient();
            client.DAES_Eliminar(new Guid(hdn_DaesID.Value), Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registro eliminado.", "info");

            Cancelar();
            llenarGrid();
            gv_Daes.DataBind();
        }
        private void Grabar()
        {
            if (cmb_Aduana.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Aduana");
                return;
            }
            if (cmb_Pais.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el País");
                return;
            }
            if (txt_DAE.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar la DAE");
                return;
            }
            if (dtp_FechaDesde.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Fecha Desde");
                return;
            }
            if (dtp_FechaHasta.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Fecha Hasta");
                return;
            }
            if (dtp_FechaHasta.SelectedDate.Value < dtp_FechaDesde.SelectedDate.Value)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "La Fecha Hasta no debe ser menor a la Fecha Desde");
                return;
            }
            try
            {
                SGF_DAES newDAES = new SGF_DAES();
                newDAES.DaesID = new Guid(hdn_DaesID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_DaesID.Value);
                newDAES.AduanaID = new Guid(cmb_Aduana.SelectedValue);
                newDAES.PaisID = new Guid(cmb_Pais.SelectedValue);
                newDAES.DAE = txt_DAE.Text;
                newDAES.Desde = dtp_FechaDesde.SelectedDate.Value;
                newDAES.Hasta = dtp_FechaHasta.SelectedDate.Value;
                newDAES.UsuarioCreacion = Me.Usuario.NombreUsuario;
                newDAES.FechaCreacion = DateTime.Now;
                newDAES.UsuarioActualiza = Me.Usuario.NombreUsuario;
                newDAES.FechaActualiza = newDAES.FechaCreacion;
                newDAES.Codigo = "";
                newDAES.Observaciones = txt_Observaciones.Text;
                newDAES.EmpresaID = Me.Usuario.EmpresaID;
                newDAES.Estado = 1;
                LogicClient client = new LogicClient();
                client.SGF_DAES_Grabar(newDAES, Utils.getHostName(Utils.getIP()), Utils.getIP(), Me.Usuario.NombreUsuario);
                VerMensaje("INFORMACIÓN", "info", "info", "Datos Registrado correctamente.");
                Cancelar();
                llenarGrid();
                gv_Daes.DataBind();
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