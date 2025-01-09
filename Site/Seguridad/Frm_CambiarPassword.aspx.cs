using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Seguridad
{
    public partial class Frm_CambiarPassword : System.Web.UI.Page
    {
        public List<SGF_ParametrosPassword> listadoParametrosPassword
        {
            get
            {
                if (ViewState["listadoParametrosPassword"] == null)
                    ViewState["listadoParametrosPassword"] = new List<SGF_ParametrosPassword>();
                return (List<SGF_ParametrosPassword>)ViewState["listadoParametrosPassword"];
            }
            set
            {
                ViewState["listadoParametrosPassword"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            txtUsuario.Text = Me.Usuario.NombreUsuario;
            Cache.Remove(txtClaveAnt.Text);

            if (!this.IsPostBack)
            {
                LogicClient client = new LogicClient();
                listadoParametrosPassword = client.Seguridad_ParametrosPassword().ToList();
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtClaveAnt.Text) || string.IsNullOrEmpty(txtClaveNueva.Text) || string.IsNullOrEmpty(txtClaveNueva2.Text))
            {
                Utils.MessageBox(this, "","Usuario y Claves son campos requeridos.","info");
                return;
            }
            try
            {
                LogicClient cliente = new LogicClient();
                // TODO: Revisar la parte de encriptación del logueo de usuarios
                //var usuario = cliente.Usuario_ObtenerPorID(txtUsuario.Text, EncryptText.EncryptText.Encrypt(txtClaveAnt.Text, txtClaveAnt.Text));
                var usuario = cliente.Seguridad_ObtenerUsuarioPorID(txtUsuario.Text, txtClaveAnt.Text);
                if (usuario == null)
                {
                    Utils.MessageBox(this, "","Las credenciales ingresadas son incorrectas o no tiene asignado un perfil", "info");
                    return;
                }
                if (txtClaveNueva.Text.Trim() != txtClaveNueva2.Text.Trim())
                {
                    Utils.MessageBox(this,"", "Las nuevas credenciales ingresadas son diferentes Verifique", "info");
                    return;
                }
                else
                {
                    SGF_ParametrosPassword ConfiguracionPassword = listadoParametrosPassword.FirstOrDefault();
                    int upper = 0, lower = 0;
                    int number = 0, special = 0;
                    String str = txtClaveNueva.Text.Trim();
                    for (int i = 0; i < str.Length; i++)
                    {
                        char ch = str[i];
                        if (ch >= 'A' && ch <= 'Z')
                            upper++;
                        else if (ch >= 'a' && ch <= 'z')
                            lower++;
                        else if (ch >= '0' && ch <= '9')
                            number++;
                        else
                            special++;
                    }

                    if (NumCaracteres(txtClaveNueva, txtClaveNueva.Text, (int)ConfiguracionPassword.LongitudMinima) == false)
                    {
                        return;
                    }

                    if (upper < (int)ConfiguracionPassword.NumMayusculas)
                    {
                        Utils.MessageBox(this, "","La contraseña debe contener por lo menos " + ConfiguracionPassword.NumMayusculas.ToString() + " letra en mayúscula", "info");
                        return;
                    }
                    if (lower < (int)ConfiguracionPassword.NumMinusculas)
                    {
                        Utils.MessageBox(this, "","La contraseña debe contener por lo menos " + ConfiguracionPassword.NumMinusculas.ToString() + " letra en minúscula", "info");
                        return;
                    }
                    if (number < (int)ConfiguracionPassword.NumNumeros)
                    {
                        Utils.MessageBox(this, "","La contraseña debe contener por lo menos " + ConfiguracionPassword.NumNumeros.ToString() + " número", "info");
                        return;
                    }
                    if (special < (int)ConfiguracionPassword.NumCaracteresEspeciales)
                    {
                        Utils.MessageBox(this,"", "La contraseña debe contener por lo menos " + ConfiguracionPassword.NumCaracteresEspeciales.ToString() + " caracter especial", "info");
                        return;
                    }
                    string pass = EncryptText.EncryptText.EncryptPassword(txtClaveNueva.Text);
                    cliente.Usuario_ActualizarPassword(txtUsuario.Text, usuario.Password, pass, "", "", "", Me.Usuario.NombreUsuario);
                    if (Me.Logueado)
                        Me.CerrarSession();
                }
            }
            catch (Exception ex)
            {
                Utils.HandleException(this, ex);
            }
        }
        private bool NumCaracteres(RadTextBox TextBox, string Cadena, Int32 caracter)
        {
            if (Cadena.Trim().Length < caracter)
            {
                Utils.MessageBox(this, "","La contraseña debe tener " + caracter.ToString() + " caracteres como mínimo", "info");
                TextBox.Focus();
                return false;
            }
            else
                return true;
        }
    }
}