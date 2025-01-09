using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGF.Site.SGF_Service;
using Telerik.Web.UI;
using SGF.Site.Controles;
using System.Net;
using System.Net.NetworkInformation;

namespace SGF.Site.Administracion
{
    public partial class Frm_Usuario : System.Web.UI.Page
    {
        #region Variables y Propiedades
        public bool EsNuevo
        {
            get
            {
                if (ViewState["EsNuevo"] == null)
                    ViewState["EsNuevo"] = false;
                return (bool)ViewState["EsNuevo"];
            }
            set
            {
                ViewState["EsNuevo"] = value;
            }          
        }
        protected List<SGF_Modulo> ListModulos
        {
            get
            {
                if (ViewState["ListModulos"] == null)
                    ViewState["ListModulos"] = new List<SGF_Modulo>();
                return (List<SGF_Modulo>)ViewState["ListModulos"];
            }
            set
            {
                ViewState["ListModulos"] = value;
            }
        }
        protected List<SGF_Modulo> ListModulosTemporal
        {
            get
            {
                if (ViewState["ListModulosTemporal"] == null)
                    ViewState["ListModulosTemporal"] = new List<SGF_Modulo>();
                return (List<SGF_Modulo>)ViewState["ListModulosTemporal"];
            }
            set
            {
                ViewState["ListModulosTemporal"] = value;
            }
        }

        protected List<SGF_Formulario> ListFormularios
        {
            get
            {
                if (ViewState["ListFormularios"] == null)
                    ViewState["ListFormularios"] = new List<SGF_Formulario>();
                return (List<SGF_Formulario>)ViewState["ListFormularios"];
            }
            set
            {
                ViewState["ListFormularios"] = value;
            }
        }
        protected List<SGF_Formulario> ListFormulariosTemporal
        {
            get
            {
                if (ViewState["ListFormulariosTemporal"] == null)
                    ViewState["ListFormulariosTemporal"] = new List<SGF_Formulario>();
                return (List<SGF_Formulario>)ViewState["ListFormulariosTemporal"];
            }
            set
            {
                ViewState["ListFormulariosTemporal"] = value;
            }
        }

        protected List<SGF_Boton> ListBotones
        {
            get
            {
                if (ViewState["ListBotones"] == null)
                    ViewState["ListBotones"] = new List<SGF_Boton>();
                return (List<SGF_Boton>)ViewState["ListBotones"];
            }
            set
            {
                ViewState["ListBotones"] = value;
            }
        }
        protected List<SGF_Boton> ListBotonesTemporal
        {
            get
            {
                if (ViewState["ListBotonesTemporal"] == null)
                    ViewState["ListBotonesTemporal"] = new List<SGF_Boton>();
                return (List<SGF_Boton>)ViewState["ListBotonesTemporal"];
            }
            set
            {
                ViewState["ListBotonesTemporal"] = value;
            }
        }

        protected List<SEG_Formularios_VTA> ListPermisoFormulariosVTA
        {
            get
            {
                if (ViewState["ListPermisoFormulariosVTA"] == null)
                    ViewState["ListPermisoFormulariosVTA"] = new List<SEG_Formularios_VTA>();
                return (List<SEG_Formularios_VTA>)ViewState["ListPermisoFormulariosVTA"];
            }
            set
            {
                ViewState["ListPermisoFormulariosVTA"] = value;
            }
        }
        protected List<SEG_Botones_VTA> ListPermisoBotonesVTA
        {
            get
            {
                if (ViewState["ListPermisoBotonesVTA"] == null)
                    ViewState["ListPermisoBotonesVTA"] = new List<SEG_Botones_VTA>();
                return (List<SEG_Botones_VTA>)ViewState["ListPermisoBotonesVTA"];
            }
            set
            {
                ViewState["ListPermisoBotonesVTA"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                cargarCombos();
                txt_IP.Text = GetIPAddress();
                txt_NombrePC.Text = GetClientHostName(txt_IP.Text.Trim());
                txt_MAC.Text = GetMAC();
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());// objeto para guardar la ip
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        txt_IP.Text = ip.ToString();// esta es nuestra ip
                    }
                }
                EsNuevo = false;
            }
        }
        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();

            cmb_TipoUsuario.DataSource = client.TipoUsuario_ObtenerTodo();
            cmb_TipoUsuario.DataTextField = "Nombre";
            cmb_TipoUsuario.DataValueField = "TipoUsuarioID";
            cmb_TipoUsuario.DataBind();
            cmb_TipoUsuario.Items.Insert(0, new RadComboBoxItem("Seleccione el tipo de Usuario", Guid.Empty.ToString()));

            cmb_BuscarTipoUsuario.DataSource = client.TipoUsuario_ObtenerTodo();
            cmb_BuscarTipoUsuario.DataTextField = "Nombre";
            cmb_BuscarTipoUsuario.DataValueField = "TipoUsuarioID";
            cmb_BuscarTipoUsuario.DataBind();
            cmb_BuscarTipoUsuario.Items.Insert(0, new RadComboBoxItem("Seleccione el tipo de Persona", Guid.Empty.ToString()));

            rlb_Modulo.DataSource = client.Modulo_ObtenerTodo();
            rlb_Modulo.DataTextField = "Nombre";
            rlb_Modulo.DataValueField = "ModuloID";
            rlb_Modulo.DataBind();

            ListModulosTemporal = client.Modulo_ObtenerTodo().ToList();

            foreach (SGF_Modulo item in ListModulosTemporal)
            {
                item.Estado = 0;
                // do your stuff with the item
            }
            ListFormulariosTemporal = client.Formulario_ObtenerTodo().ToList();
            foreach (SGF_Formulario item in ListFormulariosTemporal)
            {
                item.Estado = 0;
                // do your stuff with the item
            }
            ListBotonesTemporal = client.Boton_ObtenerTodo().ToList();
            foreach (SGF_Boton item in ListBotonesTemporal)
            {
                item.Estado = 0;
                // do your stuff with the item
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
            foreach (SGF_Modulo item in ListModulosTemporal)
            {
                item.Estado = 0;
            }
            foreach (SGF_Formulario item in ListFormulariosTemporal)
            {
                item.Estado = 0;
            }
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
            LimpiarControles();
            txt_IP.Text = GetIPAddress();
            txt_NombrePC.Text = GetClientHostName(txt_IP.Text.Trim());
            txt_MAC.Text = GetMAC();
            hdn_UsuarioID.Value = Guid.Empty.ToString();
            EsNuevo = true;

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());// objeto para guardar la ip
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    txt_IP.Text = ip.ToString();// esta es nuestra ip
                }
            }
        }

        protected void LimpiarControles()
        {
            cmb_TipoUsuario.SelectedValue = Guid.Empty.ToString();
            hdn_PersonaID.Value = null;
            hdn_UsuarioID.Value = null;
            gv_Usuario.DataSource = null;
            txt_CedulaPersona.Text = "";
            txt_NombrePersona.Text = "";
            txt_IP.Text = "";
            txt_Usuario.Text = "";
            txt_Password.Text = "";
            txt_NombrePC.Text = "";
            txt_Estado.Text = "";
            txt_MAC.Text = "";
            dtp_FechaExpiracion.SelectedDate = null;
            txt_Observaciones.Text = "";
            txt_Estado.Text = "";
            rlb_Modulo.DataSource = ListModulosTemporal;
            rlb_Formulario.DataSource = null;
            rlb_Modulo.DataBind();
            rlb_Formulario.DataBind();
            ListPermisoBotonesVTA = null;
            ListPermisoFormulariosVTA = null;
            EsNuevo = false;
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            LogicClient client = new LogicClient();
            var _usuario = client.Usuario_BuscarUsuarioVTA(new Guid(cmb_BuscarTipoUsuario.SelectedValue.ToString()), txt_BuscarCedula.Text, txt_BuscarNombre.Text);
            gv_Usuario.DataSource = _usuario;
            gv_Usuario.DataBind();
        }

        protected void rlb_Modulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadListBox radListBox = sender as RadListBox;
            if (radListBox != null && radListBox.SelectedItem != null)
            {
                string selectedText = radListBox.SelectedItem.Text;
                string selectedValue = radListBox.SelectedItem.Value;
                LogicClient client = new LogicClient();
                //                rlb_Formulario.DataSource = client.Formulario_ObtenerPorModuloID(new Guid(selectedValue));
                rlb_Formulario.DataSource = ListFormulariosTemporal.Where(x => x.ModuloID == new Guid(selectedValue));
                rlb_Formulario.DataTextField = "Nombre";
                rlb_Formulario.DataValueField = "FormularioID";
                rlb_Formulario.DataBind();
                //cargarCheck(Ctl_Formularios, new Guid(selectedValue));
                recorrerListBox(rlb_Formulario, 2);
            }
            else
            {
                // LabelSelectedItem.Text = "Selected Item: None";
            }
        }

        protected void rlb_Formulario_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadListBox radListBox = sender as RadListBox;
            if (radListBox != null && radListBox.SelectedItem != null)
            {
                string selectedText = radListBox.SelectedItem.Text;
                string selectedValue = radListBox.SelectedItem.Value;
                LogicClient client = new LogicClient();
                //rlb_Formulario.DataSource = ListBotonesTemporal.Where(x => x.FormularioID == new Guid(selectedValue));
                //rlb_Boton.DataTextField = "Nombre";
                //rlb_Boton.DataValueField = "BotonID";
                //rlb_Boton.DataBind();
                //recorrerListBox(rlb_Boton, 3);
            }
            else
            {
                // LabelSelectedItem.Text = "Selected Item: None";
            }
        }

        private void recorrerListBox(RadListBox _list, int type)
        {
            switch (type)
            {
                case 1:
                    foreach (RadListBoxItem item in _list.Items)
                    {
                        if (ListModulosTemporal.Count(x => x.ModuloID == new Guid(item.Value)) > 0)
                        {
                            item.Checked = ListModulosTemporal.First(x => x.ModuloID == new Guid(item.Value)).Estado == 1 ? true : false;
                        }
                    }
                    break;
                case 2:
                    foreach (RadListBoxItem item in _list.Items)
                    {
                        if (ListFormulariosTemporal.Count(x => x.FormularioID == new Guid(item.Value)) > 0)
                        {
                            item.Checked = ListFormulariosTemporal.First(x => x.FormularioID == new Guid(item.Value)).Estado == 1 ? true : false;
                        }
                    }
                    break;
                case 3:
                    foreach (RadListBoxItem item in _list.Items)
                    {
                        if (ListBotonesTemporal.Count(x => x.BotonID == new Guid(item.Value)) > 0)
                        {
                            item.Checked = ListBotonesTemporal.First(x => x.BotonID == new Guid(item.Value)).Estado == 1 ? true : false;
                        }
                    }
                    break;
            }

        }

        private void cargarCheck(Control_Multiple _control, Guid _ID)
        {
            _control.LimpiarControl();
            _control.TipoClasificador = _ID;
            _control.CargarControlFormulario(1);
            _control.VisualizarControl(1);
            _control.setAttribute(2, 2, 4);
            _control.setPostBack(1, false);
        }

        protected void rlb_Formulario_ItemCheck(object sender, RadListBoxItemEventArgs e)
        {
            Guid _id = new Guid(e.Item.Value);
            ListFormulariosTemporal.First(x => x.FormularioID == _id).Estado = (short)(e.Item.Checked == true ? 1 : 0);
        }

        protected void rlb_Formulario_CheckAllCheck(object sender, RadListBoxCheckAllCheckEventArgs e)
        {
            RadListBox radListBox = sender as RadListBox;

            List<string> checkedItems = new List<string>();

            foreach (RadListBoxItem item in radListBox.Items)
            {
                //if (item.Checked)
                //{
                ListFormulariosTemporal.First(x => x.FormularioID == new Guid(item.Value)).Estado = (short)(item.Checked == true ? 1 : 0);
                //}
            }

        }

        protected void btn_BuscarPersona_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected string FuncionNavigateURL()
        {
            return "javascript:AbrirModalPersona();";
        }

        protected void btn_RetornoVentana_Click(object sender, ImageClickEventArgs e)
        {
            if (hdn_PersonaID.Value == "") return;
            txt_CedulaPersona.Text = hdn_PersonaIdentificacion.Value;
            txt_NombrePersona.Text = hdn_PersonaNombre.Value;
            //VerMensaje("INFORMACIÓN", "info", "info", "La actividad seleccionada ya se encuentra agregada debe escoger otra Actividad");
            //return;
        }
     
        private string GetClientHostName(string ipAddress)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
                return hostEntry.HostName;
            }
            catch (Exception)
            {
                return "Desconocido";
            }
        }

        private string GetMAC()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void gv_Usuario_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_Usuario_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_UsuarioID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
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
            }
        }

        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }

        protected void CargarDatos()
        {
            foreach (SGF_Modulo item in ListModulosTemporal)
                item.Estado = 0;
            foreach (SGF_Formulario item in ListFormulariosTemporal)
                item.Estado = 0;
            foreach (SGF_Boton item in ListBotonesTemporal)
                item.Estado = 0;
            if (new Guid(hdn_UsuarioID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_Usuario _usuario = client.Usuario_ObtenerPorID(new Guid(hdn_UsuarioID.Value));
            SGF_Usuario_VTA _usuarioVTA = client.Usuario_BuscarUsuarioPorID_VTA(new Guid(hdn_UsuarioID.Value));
            if (_usuario != null)
            {
                cmb_TipoUsuario.SelectedValue = _usuario.TipoUsuarioID.ToString();
                hdn_PersonaID.Value = _usuario.PersonaID.ToString();
                txt_CedulaPersona.Text = _usuarioVTA.Identificacion;
                txt_NombrePersona.Text = _usuarioVTA.NombreCompleto;
                txt_IP.Text = _usuario.IP;
                txt_Usuario.Text = _usuario.UserName;
                //  txt_Password.Text = _usuario.Password;
                txt_NombrePC.Text = _usuario.NombrePC;
                txt_Estado.Text = ObtenerNombreEstado(_usuario.Estado);
                txt_MAC.Text = _usuario.MAC;
                dtp_FechaExpiracion.SelectedDate = _usuario.FechaExpiracion;
                txt_Observaciones.Text = _usuario.Observacion;


                ListPermisoFormulariosVTA = client.Formulario_ObtenerPorUsuarioID_VTA(new Guid(hdn_UsuarioID.Value));
                ListPermisoBotonesVTA = client.Boton_ObtenerPorUsuarioID_VTA(new Guid(hdn_UsuarioID.Value));

                foreach (SEG_Formularios_VTA item in ListPermisoFormulariosVTA)
                {
                    if (ListModulosTemporal.Count(x => x.ModuloID == item.ModuloID) > 0)
                        ListModulosTemporal.First(x => x.ModuloID == item.ModuloID).Estado = 1;
                    if (ListFormulariosTemporal.Count(x => x.FormularioID == item.FormularioID) > 0)
                    {
                        ListFormulariosTemporal.First(x => x.FormularioID == item.FormularioID).Estado = 1;
                        //if (ListBotonesTemporal.Count(x => x.FormularioID == item.FormularioID) > 0)
                        //    ListBotonesTemporal.First(x => x.Per BotonID == item.BotonID).Estado = 1;
                    }
                }
                recorrerListBox(rlb_Modulo, 1);
                recorrerListBox(rlb_Formulario, 2);
               // recorrerListBox(rlb_Boton, 3);
            }
            txt_IP.Text = Utils.getIP();
            txt_NombrePC.Text = Utils.getHostName(txt_IP.Text.Trim());
            txt_MAC.Text = Utils.getMAC();
            EsNuevo = false;
        }

        private void Grabar()
        {
            if (txt_CedulaPersona.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Persona");
                return;
            }
            if (cmb_TipoUsuario.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Tipo de Usuario");
                return;
            }
            if (txt_Usuario.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el user name");
                return;
            }

            if (EsNuevo==true && txt_Password.Text == "")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Password");
                return;
            }
            if (dtp_FechaExpiracion.SelectedDate == null)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe Seleccionar la Fecha de Expiración");
                return;
            }
            SGF_Usuario _usuario = new SGF_Usuario();
            _usuario.UsuarioID = new Guid(hdn_UsuarioID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_UsuarioID.Value);
            _usuario.PersonaID = hdn_PersonaID.Value;
            _usuario.TipoUsuarioID = new Guid(cmb_TipoUsuario.SelectedValue);
            _usuario.UserName = txt_Usuario.Text;
            if(EsNuevo==true)
            {
                string pass = EncryptText.EncryptText.EncryptPassword(txt_Password.Text);
                _usuario.Password = pass;
            }
            _usuario.FechaCreacion = DateTime.Now;
            _usuario.UsuarioCreacion = Me.Usuario.NombreUsuario;
            _usuario.CambioPassword = false;
            _usuario.Bloqueado = false;
            _usuario.NumeroIntentos = 0;
            _usuario.IP = txt_IP.Text;
            _usuario.MAC = txt_MAC.Text;
            _usuario.NombrePC = txt_NombrePC.Text;
            _usuario.FechaExpiracion = dtp_FechaExpiracion.SelectedDate;
            _usuario.FechaActualiza = _usuario.FechaCreacion;
            _usuario.UsuarioActualiza = Me.Usuario.NombreUsuario;
            _usuario.Observacion = txt_Observaciones.Text;
            _usuario.Estado = 1;
            _usuario.EmpresaID = "";
            //    recogerDatosListBox(rlb_Modulo, 1);
            //    recogerDatosListBox(rlb_Formulario, 2);
            //    recogerDatosListBox(rlb_Boton, 3);
            recogerDatosListas(rlb_Modulo);
            List<SGF_Permiso> _permiso = new List<SGF_Permiso>();
            List<SGF_PermisoBoton> _permisoboton = new List<SGF_PermisoBoton>();
            LogicClient client = new LogicClient();
            client.Usuario_Grabar(_usuario);

            foreach (SGF_Modulo _modulo in ListModulos)
            {
                foreach (SGF_Formulario _formulario in ListFormularios.Where(x => x.ModuloID == _modulo.ModuloID))
                {
                    SGF_Permiso _tmpPermiso = new SGF_Permiso();
                    if (ListPermisoFormulariosVTA.Count(x => x.ModuloID == _formulario.ModuloID && x.FormularioID == _formulario.FormularioID && x.UsuarioID == new Guid(hdn_UsuarioID.Value)) > 0)
                    {
                        SEG_Formularios_VTA _tmpPermisoVTA = ListPermisoFormulariosVTA.First(x => x.ModuloID == _formulario.ModuloID && x.FormularioID == _formulario.FormularioID && x.UsuarioID == new Guid(hdn_UsuarioID.Value));
                        _tmpPermiso.PermisoID = _tmpPermisoVTA.PermisoID;
                        _tmpPermiso.UsuarioID = _tmpPermisoVTA.UsuarioID;
                        _tmpPermiso.FormularioID = _tmpPermisoVTA.FormularioID;
                        _tmpPermiso.Agregar = _tmpPermisoVTA.Agregar;
                        _tmpPermiso.Editar = _tmpPermisoVTA.Editar;
                        _tmpPermiso.Eliminar = _tmpPermisoVTA.Eliminar;
                        _tmpPermiso.Procesar = true;
                        _tmpPermiso.Imprimir = _tmpPermisoVTA.Imprimir;
                        _tmpPermiso.Value = _tmpPermisoVTA.Value;
                        _tmpPermiso.FechaCreacion = DateTime.Now;
                        _tmpPermiso.UsuarioCreacion = Me.Usuario.NombreUsuario;
                        _tmpPermiso.Estado = _tmpPermisoVTA.EstadoPermiso;
                    }
                    else
                    {
                        _tmpPermiso.PermisoID = Guid.NewGuid();
                        _tmpPermiso.UsuarioID = new Guid(hdn_UsuarioID.Value);
                        _tmpPermiso.FormularioID = _formulario.FormularioID;
                        _tmpPermiso.Agregar = true;
                        _tmpPermiso.Editar = true;
                        _tmpPermiso.Eliminar = true;
                        _tmpPermiso.Procesar = true;
                        _tmpPermiso.Imprimir = true;
                        _tmpPermiso.Value = null;
                        _tmpPermiso.FechaCreacion = DateTime.Now;
                        _tmpPermiso.UsuarioCreacion = Me.Usuario.NombreUsuario;
                        _tmpPermiso.Estado = 1;
                    }
                    _permiso.Add(_tmpPermiso);
                    //foreach (SGF_Boton _boton in ListBotones)
                    //{

                    //}
                }
            }
            client.PermisoFormulario_LimpiarEstado(new Guid(hdn_UsuarioID.Value));
            foreach (SGF_Permiso item in _permiso)
            {

                client.PermisoFormulario_Grabar(item);
            }
            VerMensaje("INFORMACIÓN", "info", "info", "Datos Registrados correctamente");
            Cancelar();
        }

        private void recogerDatosListBox(RadListBox _list, int type)
        {
            switch (type)
            {
                case 1:
                    foreach (RadListBoxItem item in _list.Items)
                    {
                        if (ListModulosTemporal.Count(x => x.ModuloID == new Guid(item.Value)) > 0 && item.Checked == true)
                        {
                            ListModulos.Add(ListModulosTemporal.First(x => x.ModuloID == new Guid(item.Value)));
                            //item.Checked = ListModulosTemporal.First(x => x.ModuloID == new Guid(item.Value)).Estado == 1 ? true : false;
                        }
                    }
                    break;
                case 2:
                    foreach (RadListBoxItem item in _list.Items)
                    {
                        if (ListFormulariosTemporal.Count(x => x.FormularioID == new Guid(item.Value)) > 0 && item.Checked == true)
                        {
                            ListFormularios.Add(ListFormulariosTemporal.First(x => x.FormularioID == new Guid(item.Value)));
                            //item.Checked = ListFormulariosTemporal.First(x => x.FormularioID == new Guid(item.Value)).Estado == 1 ? true : false;
                        }
                    }
                    break;
                case 3:
                    foreach (RadListBoxItem item in _list.Items)
                    {
                        if (ListBotonesTemporal.Count(x => x.BotonID == new Guid(item.Value)) > 0 && item.Checked == true)
                        {
                            ListBotones.Add(ListBotonesTemporal.First(x => x.BotonID == new Guid(item.Value)));
                            //item.Checked = ListFormulariosTemporal.First(x => x.FormularioID == new Guid(item.Value)).Estado == 1 ? true : false;
                        }
                    }
                    break;
            }
        }

        private void recogerDatosListas(RadListBox _list)
        {
            foreach (RadListBoxItem item in _list.Items)
            {
                if (ListModulosTemporal.Count(x => x.ModuloID == new Guid(item.Value)) > 0 && item.Checked == true)
                {
                    ListModulos.Add(ListModulosTemporal.First(x => x.ModuloID == new Guid(item.Value)));
                    //item.Checked = ListModulosTemporal.First(x => x.ModuloID == new Guid(item.Value)).Estado == 1 ? true : false;
                }
            }
            //foreach (SGF_Modulo item in ListModulosTemporal.Where(x => x.Estado == 1))
            //{
            //    ListModulos.Add(ListModulosTemporal.First(x => x.ModuloID == item.ModuloID));
            //}
            foreach (SGF_Formulario item in ListFormulariosTemporal.Where(x => x.Estado == 1))
            {
                ListFormularios.Add(ListFormulariosTemporal.First(x => x.ModuloID == item.ModuloID && x.FormularioID == item.FormularioID));
            }
            foreach (SGF_Boton item in ListBotonesTemporal.Where(x => x.Estado == 1))
            {
                ListBotones.Add(ListBotonesTemporal.First(x => x.BotonID == item.BotonID && x.FormularioID == item.FormularioID));
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
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
    }
    [Serializable]
    public class ObjRequerimiento
    {
        public Guid RowID { get; set; }
        public String Name { get; set; }
        public bool Value { get; set; }
    }
}