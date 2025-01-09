using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGF.Cultivo
{
    public partial class Frm_BuscarProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["type"] != null)
                {
                    hdn_TypeID.Value = Request["type"];
                    switch (hdn_TypeID.Value)
                    {
                        case "1":
                            lbl_Titulo.Text = "BUSCAR EMPLEADOS";
                            break;
                        case "2":
                            lbl_Titulo.Text = "BUSCAR CLIENTES";
                            break;
                        case "3":
                            lbl_Titulo.Text = "BUSCAR PROVEEDORES";
                            break;
                        default:
                            lbl_Titulo.Text = "BUSCAR PERSONA";
                            break;
                    }
                }
            }
        }

        protected void grid_Persona_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdn_PersonaID.Value = grid_Persona.SelectedValues["PersonaID"].ToString();
            hdn_PersonaNombre.Value = grid_Persona.SelectedValues["NombrePersona"].ToString();
            hdn_PersonaIdentificacion.Value = grid_Persona.SelectedValues["Identificacion"].ToString();

            // Cierre de la ventana
            ScriptManager.RegisterStartupScript(this, typeof(string), "cierre", "returnToParent();", true);
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            if (txt_Filtro.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar un criterio para poder realizar la búsqueda.");
                return;
            }

            LogicClient client = new LogicClient();
            List<SGF_Persona_VTA> _listado = new List<SGF_Persona_VTA>();
            if (rbt_Parametro.SelectedValue.ToString() == "CODIGO")
                _listado = client.Persona_BuscarPersonaVTA(Guid.Empty, txt_Filtro.Text, "").ToList();
            else
                _listado = client.Persona_BuscarPersonaVTA(Guid.Empty, "", txt_Filtro.Text.ToUpper()).ToList();
            grid_Persona.DataSource = _listado;
            grid_Persona.DataBind();
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