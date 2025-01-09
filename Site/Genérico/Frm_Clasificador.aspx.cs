using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGF.Site.Genérico
{
    public partial class Frm_Clasificador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Me.ValidarSesion();

        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {

        }

        protected void btn_limpiar_Click1(object sender, EventArgs e)
        {

        }

        protected void Grid_Clasificador_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void Grid_Clasificador_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void tool_principal_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {

        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            switch (Estado)
            {
                case 1:
                    return "Activo";
                case 2:
                    return "Borrado";
            }
            return "";
        }
    }
}