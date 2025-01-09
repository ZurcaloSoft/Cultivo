using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGF.Site.SGF_Service;

namespace SGF.Site.Genérico
{
    public partial class Frm_Persona : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogicClient client = new LogicClient();
                var _aux = client.Persona_ObtenerTodo();
                txt_Codigo.Text = _aux.First().Codigo.ToString();
                txt_Identificacion.Text = _aux.First().Identificacion.ToString();
                txt_Nombre.Text = _aux.First().Nombre.ToString();
            }
        }
    }
}