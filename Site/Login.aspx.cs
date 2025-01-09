using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGF.Site.SGF_Service;

namespace SGF.Site
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Me.Logueado)
                Me.CerrarSession();

        }

        protected void btn_Ingresar_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txt_Username.Text) || string.IsNullOrEmpty(txt_Password.Text))
            {
                Utils.MessageBox2(this, "Usuario y Clave son campos requeridos.");
                return;
            }

            try
            {
                LogicClient client = new LogicClient();
                // TODO: Revisar la parte de encriptación del logueo de usuarios
                var usuario = client.Seguridad_ObtenerUsuarioPorID(txt_Username.Text, txt_Password.Text);
                if (usuario == null)
                {
                    Utils.MessageBox2(this, "Las credenciales ingresadas son incorrectas o no tiene asignado un perfil");
                    return;
                }

                Me.LlenaUsuario(
        usuario,
        new List<SEG_Formularios_VTA>(client.Menu_ObtenerTodosPor(usuario.UsuarioID)),
        new List<SEG_Botones_VTA>(client.Botones_ObtenerTodosPor(usuario.UsuarioID)));
                Response.Redirect("~/Default.aspx", true);

            }
            catch (Exception ex)
            {
                //  string _ex = ((System.ServiceModel.FaultException<SGF.Site.SGF_Service.SecurityException>)ex).Detail.Messagek__BackingField;
                Utils.MessageBox2(this, ex.ToString());
            }
        }
    }
}