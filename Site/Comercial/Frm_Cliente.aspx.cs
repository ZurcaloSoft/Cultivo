using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Comercial
{
    public partial class Frm_Cliente : System.Web.UI.Page
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

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Buscar.Visible = false;
            pnl_Datos.Visible = true;
            LimpiarControles();

        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {

        }

        protected void gv_Persona_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_Persona_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            RadToolBarButton btn = e.Item as RadToolBarButton;
            switch (btn.CommandName)
            {
                case "Grabar":
                   // Grabar();
                    break;
                case "Cancelar":
                    Cancelar();
                    break;
            }
        }

        protected void cmb_TipoIdentificacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        protected void LimpiarControles()
        {
            cmb_TipoIdentificacion.SelectedValue = "0";
            cmb_TipoPersona.SelectedValue = Guid.Empty.ToString();
            cmb_Pais.SelectedValue = Guid.Empty.ToString();
            cmb_EstadoCivil.SelectedValue = Guid.Empty.ToString();
            cmb_Genero.SelectedValue = Guid.Empty.ToString();
            hdn_PersonaID.Value = null;
            txt_Codigo.Text = "";
            txt_Identificacion.Text = "";
            txt_NombrePersona.Text = "";
            txt_RepIdentificacion.Text = "";
            txt_RepNombre.Text = "";
            txt_NombreComercial.Text = "";
            dtp_FechaNacimiento.SelectedDate = null;
            txt_Email.Text = "";
            txt_Telefono.Text = "";
            txt_Celular.Text = "";
            txt_Cargo.Text = "";
            dtp_FechaIngreso.SelectedDate = null;
            dtp_FechaExpiracion.SelectedDate = null;
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";
        }
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscar.Visible = true;
            pnl_Datos.Visible = false;
        }
    }
}