using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGF.Site.SGF_Service;
using Telerik.Web.UI;

namespace SGF.Site.TalentoHumano
{
    public partial class Frm_Persona : System.Web.UI.Page
    {
        public string TypePersona
        {
            get { return ViewState["TypePersona"].ToString(); }
            set { ViewState["TypePersona"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.TalentoHumano;
                //LogicClient client = new LogicClient();
                //var _aux = client.Persona_ObtenerTodo();
                //txt_Codigo.Text = _aux.First().Codigo.ToString();
                //txt_Identificacion.Text = _aux.First().Identificacion.ToString();
                //txt_Nombre.Text = _aux.First().Nombre.ToString();
                cargarCombos();

                if (!string.IsNullOrEmpty(Request["Type"]))
                {
                    TypePersona = Request["Type"].ToString();
                    switch (TypePersona)
                    {
                        #region Comercial                  
                        case "8E4F8CD1-964E-47CB-ACF1-0A28BE059B3F":       //Comercial - Cargueras
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Comercial;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE CARGUERAS";
                            cmb_BuscarTipoPersona.Enabled = false;
                            cmb_TipoPersona.Enabled = false;
                            cmb_BuscarTipoPersona.SelectedValue = TipoPersona.Carguera.ToString();
                            cmb_TipoPersona.SelectedValue = TipoPersona.Carguera.ToString();
                            //fiel_Buscar.Visible = false;
                            break;
                        #endregion
                        #region Recepción
                        case "C22CDF1E-28C3-4D23-A19B-409748464417":        //Compras
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.Recepcion;
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE PROVEEDORES";
                            cmb_BuscarTipoPersona.SelectedValue = TipoPersona.Proveedor.ToString();
                            cmb_TipoPersona.SelectedValue = TipoPersona.Proveedor.ToString();
                            break;
                        #endregion
                        case "38B30AFB-3437-4648-9CCA-B25BFFFD7176":        //TalentoHumano
                            lbl_Titulo.Text = "ADMINISTRACIÓN DE EMPLEADOS";
                            master.VisibilityMenuItem = (int)Enums.ModuloIndex.TalentoHumano;
                            cmb_BuscarTipoPersona.SelectedValue = TipoPersona.Empleado.ToString();
                            cmb_TipoPersona.SelectedValue = TipoPersona.Empleado.ToString();
                            break;
                    }
                    EjecutarBusqueda();
                    gv_Persona.DataBind();
                }
            }
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            EjecutarBusqueda();
            gv_Persona.DataBind();
        }

        private void EjecutarBusqueda()
        {
            LogicClient client = new LogicClient();
            var _personas = client.Persona_BuscarPersonaVTA(new Guid(cmb_BuscarTipoPersona.SelectedValue.ToString()), txt_BuscarCedula.Text, txt_BuscarNombre.Text);
            gv_Persona.DataSource = _personas;
        }

        private void ControlesCarguera()
        {

        }

        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            cmb_BuscarTipoPersona.SelectedValue = Guid.Empty.ToString();
            txt_BuscarCedula.Text = string.Empty;
            txt_BuscarNombre.Text = string.Empty;
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

        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();
            var _tipoPersona = client.TipoPersona_ObtenerTodo();
            cmb_BuscarTipoPersona.DataSource = _tipoPersona;
            cmb_BuscarTipoPersona.DataTextField = "Nombre";
            cmb_BuscarTipoPersona.DataValueField = "TipoPersonaID";
            cmb_BuscarTipoPersona.DataBind();
            cmb_BuscarTipoPersona.Items.Insert(0, new RadComboBoxItem("Seleccione el tipo de Persona", Guid.Empty.ToString()));

            cmb_TipoPersona.DataSource = _tipoPersona;
            cmb_TipoPersona.DataTextField = "Nombre";
            cmb_TipoPersona.DataValueField = "TipoPersonaID";
            cmb_TipoPersona.DataBind();
            cmb_BuscarTipoPersona.Items.Insert(0, new RadComboBoxItem("Seleccione el tipo de Persona", Guid.Empty.ToString()));

            cmb_TipoIdentificacion.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoIdentificacion);
            cmb_TipoIdentificacion.DataTextField = "Nombre";
            cmb_TipoIdentificacion.DataValueField = "ClasificadorID";
            cmb_TipoIdentificacion.DataBind();
            cmb_TipoIdentificacion.Items.Insert(0, new RadComboBoxItem("Seleccione el tipo de Identificación", Guid.Empty.ToString()));

            cmb_CargueraTipoIdentificacion.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoIdentificacion);
            cmb_CargueraTipoIdentificacion.DataTextField = "Nombre";
            cmb_CargueraTipoIdentificacion.DataValueField = "ClasificadorID";
            cmb_CargueraTipoIdentificacion.DataBind();
            cmb_CargueraTipoIdentificacion.Items.Insert(0, new RadComboBoxItem("Seleccione el tipo de Identificación", Guid.Empty.ToString()));

            cmb_Pais.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Pais);
            cmb_Pais.DataTextField = "Nombre";
            cmb_Pais.DataValueField = "ClasificadorID";
            cmb_Pais.DataBind();
            cmb_Pais.Items.Insert(0, new RadComboBoxItem("Seleccione el País", Guid.Empty.ToString()));

            cmb_EstadoCivil.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.EstadoCivil);
            cmb_EstadoCivil.DataTextField = "Nombre";
            cmb_EstadoCivil.DataValueField = "ClasificadorID";
            cmb_EstadoCivil.DataBind();
            cmb_EstadoCivil.Items.Insert(0, new RadComboBoxItem("Seleccione el Estado Civil", Guid.Empty.ToString()));

            cmb_Genero.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Genero);
            cmb_Genero.DataTextField = "Nombre";
            cmb_Genero.DataValueField = "ClasificadorID";
            cmb_Genero.DataBind();
            cmb_Genero.Items.Insert(0, new RadComboBoxItem("Seleccione el Género", Guid.Empty.ToString()));

            cmb_CargueraCuartoFrio.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.CuartoFrioCarguera);
            cmb_CargueraCuartoFrio.DataTextField = "Nombre";
            cmb_CargueraCuartoFrio.DataValueField = "ClasificadorID";
            cmb_CargueraCuartoFrio.DataBind();
            cmb_CargueraCuartoFrio.Items.Insert(0, new RadComboBoxItem("Seleccione el Cuarto Frío", Guid.Empty.ToString()));
        }
        protected void gv_Persona_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }

        protected void gv_Persona_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_PersonaID.Value = e.CommandArgument.ToString();
                    pnl_Buscar.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Buscar.Visible = false;
            pnl_Datos.Visible = true;
            if (TypePersona.ToString().ToUpper() == TipoPersona.Empleado.ToString().ToUpper())
            {
                pnl_DatosCarguera.Visible = false;
                pnl_DatosEmpleado.Visible = true;
            }
            if (TypePersona.ToString().ToUpper() == TipoPersona.Carguera.ToString().ToUpper())
            {
                pnl_DatosCarguera.Visible = true;
                pnl_DatosEmpleado.Visible = false;
            }
            LimpiarControles();

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

            txt_CargueraCelular.Text = "";
            txt_CargueraCodigo.Text = "";
            txt_CargueraContacto.Text = "";
            txt_CargueraEmail.Text = "";
            txt_CargueraIdentificacion.Text = "";
            txt_CargueraNombre.Text = "";
            txt_CargueraTelefono.Text = "";
            txt_CargueraEstado.Text = "";
            txt_CargueraObservaciones.Text = "";
            cmb_CargueraCuartoFrio.SelectedValue = Guid.Empty.ToString();
            cmb_CargueraTipoIdentificacion.SelectedValue = "0";
            txt_CargueraDireccion.Text = "";
            txt_CargueraSitioWeb.Text = "";
        }

        private void CargarDatos()
        {
            if (new Guid(hdn_PersonaID.Value) == Guid.Empty) return;

            LogicClient client = new LogicClient();
            SGF_Persona_VTA _persona = client.Persona_ObtenerPorID_VTA(hdn_PersonaID.Value);
            if (TypePersona.ToUpper() == TipoPersona.Empleado.ToString().ToUpper())
            {
                cmb_TipoPersona.SelectedValue = _persona.TipoPersonaID.ToString();
                cmb_TipoIdentificacion.SelectedValue = _persona.TipoIdentificacion.ToString();
                cmb_Pais.SelectedValue = _persona.Pais == null ? Guid.Empty.ToString() : _persona.Pais.ToString();
                cmb_EstadoCivil.SelectedValue = _persona.EstadoCivil == null ? Guid.Empty.ToString() : _persona.EstadoCivil.ToString();
                cmb_Genero.SelectedValue = _persona.Genero == null ? Guid.Empty.ToString() : _persona.Genero.ToString();
                txt_Codigo.Text = _persona.Codigo.ToString();
                txt_Identificacion.Text = _persona.Identificacion;
                txt_NombrePersona.Text = _persona.NombrePersona;
                txt_RepIdentificacion.Text = _persona.IdentificacionRepresentante;
                txt_RepNombre.Text = _persona.RepresentanteLegal;
                txt_NombreComercial.Text = _persona.NombreComercial;
                dtp_FechaNacimiento.SelectedDate = _persona.FechaNacimiento;
                txt_Email.Text = _persona.Email;
                txt_Telefono.Text = _persona.Telefono;
                txt_Celular.Text = _persona.Celular;
                txt_Cargo.Text = _persona.Cargo;
                dtp_FechaIngreso.SelectedDate = _persona.FechaIngreso;
                dtp_FechaExpiracion.SelectedDate = _persona.FechaExpiracion;
                txt_Observaciones.Text = _persona.Observacion;
                txt_Estado.Text = _persona.EstadoPersona;
                pnl_DatosEmpleado.Visible = true;
            }
            if (TypePersona.ToUpper() == TipoPersona.Carguera.ToString().ToUpper())
            {
                txt_CargueraCelular.Text = _persona.Celular;
                txt_CargueraCodigo.Text = _persona.Codigo.ToString();
                txt_CargueraContacto.Text = _persona.RepresentanteLegal;
                txt_CargueraEmail.Text = _persona.Email;
                txt_CargueraIdentificacion.Text = _persona.Identificacion;
                txt_CargueraNombre.Text = _persona.NombrePersona;
                txt_CargueraTelefono.Text = _persona.Telefono;
                txt_CargueraEstado.Text = _persona.EstadoPersona;
                txt_CargueraObservaciones.Text = _persona.Observacion;
                cmb_CargueraCuartoFrio.SelectedValue = _persona.Genero == null ? Guid.Empty.ToString() : _persona.Genero.ToString();
                cmb_CargueraTipoIdentificacion.SelectedValue = _persona.TipoIdentificacion.ToString();
                txt_CargueraDireccion.Text = _persona.Descripcion;
                txt_CargueraSitioWeb.Text = _persona.RepresentanteLegal;
                pnl_DatosCarguera.Visible = true;
            }

        }
        private void Grabar()
        {
            string Valor = "";
            SGF_Persona _newPersona = new SGF_Persona();
            _newPersona.PersonaID = String.IsNullOrEmpty(hdn_PersonaID.Value) ? Guid.NewGuid().ToString() : hdn_PersonaID.Value;
            _newPersona.TipoPersonaID = new Guid(TypePersona);
            _newPersona.UsuarioCreacion = Me.Usuario.NombreUsuario;
            _newPersona.FechaCreacion = DateTime.Now;
            _newPersona.EmpresaID = Me.Usuario.EmpresaID;
            _newPersona.UsuarioActualiza = _newPersona.UsuarioCreacion;
            _newPersona.FechaActualiza = _newPersona.FechaCreacion;

            if (TypePersona.ToUpper() == TipoPersona.Empleado.ToString().ToUpper())
            {
                if (cmb_TipoPersona.SelectedValue.ToString() == Guid.Empty.ToString())
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Tipo de Persona");
                    return;
                }
                if (cmb_TipoIdentificacion.SelectedValue.ToString() == "0")
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Tipo de Identificación");
                    return;
                }
                if (txt_Identificacion.Text == string.Empty.ToString())
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar la Identificación");
                    return;
                }
                if (txt_NombrePersona.Text == string.Empty.ToString())
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre del Empleado");
                    return;
                }
                if (txt_NombrePersona.Text == string.Empty.ToString())
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre del Empleado");
                    return;
                }

                _newPersona.TipoIdentificacion = new Guid(cmb_TipoIdentificacion.SelectedValue.ToString());
                _newPersona.Pais = new Guid(cmb_Pais.SelectedValue.ToString());
                _newPersona.EstadoCivil = new Guid(cmb_EstadoCivil.SelectedValue.ToString());
                _newPersona.Genero = new Guid(cmb_Genero.SelectedValue.ToString());
                _newPersona.Codigo = txt_Codigo.Text.ToString();
                _newPersona.Identificacion = txt_Identificacion.Text;
                _newPersona.Nombre = txt_NombrePersona.Text;
                _newPersona.IdentificacionRepresentante = txt_RepIdentificacion.Text;
                _newPersona.RepresentanteLegal = txt_RepNombre.Text;
                _newPersona.NombreComercial = txt_NombreComercial.Text;
                _newPersona.FechaNacimiento = dtp_FechaNacimiento.SelectedDate;
                _newPersona.Email = txt_Email.Text;
                _newPersona.Telefono = txt_Telefono.Text;
                _newPersona.Celular = txt_Celular.Text;
                _newPersona.Cargo = txt_Cargo.Text;
                _newPersona.FechaIngreso = dtp_FechaIngreso.SelectedDate;
                _newPersona.FechaExpiracion = dtp_FechaExpiracion.SelectedDate;
                _newPersona.Observacion = txt_Observaciones.Text;
                _newPersona.Estado = 1;

            }
            if (TypePersona.ToUpper() == TipoPersona.Carguera.ToString().ToUpper())
            {
                if (txt_CargueraNombre.Text == string.Empty.ToString())
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre de la Carguera");
                    return;
                }
                if (txt_CargueraCodigo.Text == string.Empty.ToString())
                {
                    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Código de la Carguera");
                    return;
                }
                _newPersona.Celular = txt_CargueraCelular.Text;
                _newPersona.Codigo = txt_CargueraCodigo.Text;
                _newPersona.Cargo = txt_CargueraContacto.Text;
                _newPersona.Email = txt_CargueraEmail.Text;
                _newPersona.Identificacion = txt_CargueraIdentificacion.Text;
                _newPersona.Nombre = txt_CargueraNombre.Text;
                _newPersona.Telefono = txt_CargueraTelefono.Text;
                _newPersona.Estado = 1;
                _newPersona.Observacion = txt_CargueraObservaciones.Text;
                _newPersona.Genero = new Guid(cmb_CargueraCuartoFrio.SelectedValue);
                _newPersona.TipoIdentificacion = new Guid(cmb_CargueraTipoIdentificacion.SelectedValue);
                _newPersona.Dirección = txt_CargueraDireccion.Text;
                _newPersona.RepresentanteLegal = txt_CargueraSitioWeb.Text;
            }
            LogicClient client = new LogicClient();
            client.Persona_Grabar(_newPersona, Utils.getIP(), Utils.getHostName(Utils.getIP()), Me.Usuario.NombreUsuario);
            VerMensaje("INFORMACIÓN", "info", "info", "Registro ingresado correctamente");
            Cancelar();

        }

        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscar.Visible = true;
            pnl_Datos.Visible = false;
            pnl_DatosCarguera.Visible = false;
            pnl_DatosEmpleado.Visible = false;
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

        protected void cmb_TipoIdentificacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmb_TipoIdentificacion.SelectedValue.ToString() != "0")
            {
                switch (cmb_TipoIdentificacion.SelectedValue.ToString())
                {
                    case "0":
                        break;
                    case "1"://Cédula
                        txt_Identificacion.MaxLength = 10;
                        txt_Identificacion.InputType = ((Html5InputType)(int)Html5InputType.Number);
                        break;
                    case "2"://Ruc
                        txt_Identificacion.MaxLength = 13;
                        txt_Identificacion.InputType = ((Html5InputType)(int)Html5InputType.Number);
                        break;
                    case "3"://Pasaporte
                        break;
                }
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

        protected void cmb_CargueraTipoIdentificacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmb_CargueraTipoIdentificacion.SelectedValue.ToString() != "0")
            {
                switch (cmb_CargueraTipoIdentificacion.SelectedValue.ToString())
                {
                    case "0":
                        break;
                    case "1"://Cédula
                        txt_CargueraIdentificacion.MaxLength = 10;
                        txt_CargueraIdentificacion.InputType = ((Html5InputType)(int)Html5InputType.Number);
                        break;
                    case "2"://Ruc
                        txt_CargueraIdentificacion.MaxLength = 13;
                        txt_CargueraIdentificacion.InputType = ((Html5InputType)(int)Html5InputType.Number);
                        break;
                    case "3"://Pasaporte
                        break;
                }
            }
        }
    }
}