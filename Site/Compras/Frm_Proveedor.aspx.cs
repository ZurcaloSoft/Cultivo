using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace SGF.Site.Compras
{
    public partial class Frm_Proveedor : System.Web.UI.Page
    {
        #region variables y propiedades
        protected List<CO_ALMACEN> ListFincas
        {
            get
            {
                if (ViewState["ListFincas"] == null)
                    ViewState["ListFincas"] = new List<CO_ALMACEN>();
                return (List<CO_ALMACEN>)ViewState["ListFincas"];
            }
            set
            {
                ViewState["ListFincas"] = value;
            }
        }
        protected List<CO_BODEGA> ListBodegas
        {
            get
            {
                if (ViewState["ListBodegas"] == null)
                    ViewState["ListBodegas"] = new List<CO_BODEGA>();
                return (List<CO_BODEGA>)ViewState["ListBodegas"];
            }
            set
            {
                ViewState["ListBodegas"] = value;
            }
        }

        protected List<SGF_ProveedorBodega> ListProveedorBodega
        {
            get
            {
                if (ViewState["ListProveedorBodega"] == null)
                    ViewState["ListProveedorBodega"] = new List<SGF_ProveedorBodega>();
                return (List<SGF_ProveedorBodega>)ViewState["ListProveedorBodega"];
            }
            set
            {
                ViewState["ListProveedorBodega"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Recepcion;
                cargarCombos();
                txt_EmpresaRUC.Text = Me.Usuario.EmpresaID;
            }
        }

        private void cargarCombos()
        {
            LogicClient client = new LogicClient();
            cmb_ProveedorTipo.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoProveedores);
            cmb_ProveedorTipo.DataTextField = "Nombre";
            cmb_ProveedorTipo.DataValueField = "ClasificadorID";
            cmb_ProveedorTipo.DataBind();
            cmb_ProveedorTipo.Items.Insert(0, new RadComboBoxItem("Seleccionar Tipo de Proveedor", Guid.Empty.ToString()));

            cmb_ProveedorTipoIdentificacion.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.TipoIdentificacionNew);
            cmb_ProveedorTipoIdentificacion.DataTextField = "Nombre";
            cmb_ProveedorTipoIdentificacion.DataValueField = "ClasificadorID";
            cmb_ProveedorTipoIdentificacion.DataBind();
            cmb_ProveedorTipoIdentificacion.Items.Insert(0, new RadComboBoxItem("Seleccionar Tipo de Identificación", Guid.Empty.ToString()));

            cmb_ProveedorPais.DataSource = client.Clasificador_ObtenerPorTipoClasificador((Guid)TipoClasificador.Pais);
            cmb_ProveedorPais.DataTextField = "Nombre";
            cmb_ProveedorPais.DataValueField = "ClasificadorID";
            cmb_ProveedorPais.DataBind();
            cmb_ProveedorPais.Items.Insert(0, new RadComboBoxItem("Seleccionar País", Guid.Empty.ToString()));

            ListBodegas = client.Bodega_ObtenerPorEmpresa(Me.Usuario.EmpresaID);
            ListFincas = client.Almacen_ObtenerPorEmpresa(Me.Usuario.EmpresaID);
            rlb_Finca.DataSource = ListFincas;
            rlb_Finca.DataTextField = "CO_ALM_NOM";
            rlb_Finca.DataValueField = "CO_ALM_COD";
            rlb_Finca.DataBind();

            cmb_Finca.DataSource = ListFincas;
            cmb_Finca.DataTextField = "CO_ALM_NOM";
            cmb_Finca.DataValueField = "CO_ALM_COD";
            cmb_Finca.DataBind();
            cmb_Finca.Items.Insert(0, new RadComboBoxItem("Seleccionar la Finca", "0"));
        }
        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Buscar.Visible = false;
            pnl_Datos.Visible = true;
            LimpiarControles();

        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            consultar();
            gv_Persona.DataBind();
        }
        private void consultar()
        {
            LogicClient client = new LogicClient();
            var _resultado = client.Proveedor_ObtenerTodo();
            if (txt_BuscarCedula.Text == "" && txt_BuscarNombre.Text == "")
                gv_Persona.DataSource = _resultado.OrderBy(x => x.CO_PRO_NOM);
            else
            {
                if (txt_BuscarCedula.Text.Length > 0)
                    gv_Persona.DataSource = _resultado.Where(x => x.CO_PRO_CED == txt_BuscarCedula.Text.Trim()).OrderBy(x => x.CO_PRO_NOM);
                if (txt_BuscarNombre.Text.Length > 0)
                    gv_Persona.DataSource = _resultado.Where(x => x.CO_PRO_NOM.ToUpper().Contains(txt_BuscarNombre.Text.Trim().ToUpper())).OrderBy(x => x.CO_PRO_NOM);
            }
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            txt_BuscarCedula.Text = "";
            txt_BuscarNombre.Text = "";
        }

        protected void gv_Persona_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            consultar();
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

        private void CargarDatos()
        {
            LogicClient client = new LogicClient();
            CO_PROVEEDOR _proveedor = new CO_PROVEEDOR();
            _proveedor = client.Proveedor_ObtenerPorID(hdn_PersonaID.Value.ToString());
            if (_proveedor != null)
            {
                txt_ProveedorIdentificacion.Text = _proveedor.CO_PRO_COD;
                txt_EmpresaRUC.Text = _proveedor.CO_EMP_RUC;
                txt_Codigo.Text = _proveedor.CODIGO;
                cmb_ProveedorTipo.SelectedValue = _proveedor.CO_TIP_PRO_COD.ToString();
                cmb_ProveedorTipoIdentificacion.SelectedValue = _proveedor.CO_TIP_IDE_COD.ToString();
                //_proveedor.CO_PRO_CED = txt_ProveedorIdentificacion.Text;
                txt_ProveedorNombre.Text = _proveedor.CO_PRO_NOM;
                txt_ProveedorRepresentanteLegal.Text = _proveedor.CO_PRO_CON; //Contacto PRincipal del Proveedor
                cmb_ProveedorPais.SelectedValue = _proveedor.CO_PRO_PAI.ToString();
                txt_ProveedorCiudad.Text = _proveedor.CO_PRO_CIU;
                txt_ProveedorDireccion.Text = _proveedor.CO_PRO_DIR;
                txt_ProveedorTelefono.Text = _proveedor.CO_PRO_TEL1;
                txt_ProveedorTelefono2.Text = _proveedor.CO_PRO_TEL2;
                txt_ProveedorCelular.Text = _proveedor.CO_PRO_CEL;
                txt_ProveedorEmail.Text = _proveedor.CO_PRO_MAI;
                txt_ProveedorEstado.Text = _proveedor.CO_PRO_EST;
                txt_ProveedorCuenta.Text = _proveedor.CO_PRO_CUE;
                chk_ProveedorConEsp.Checked = (bool)_proveedor.CO_PRO_CON_ESP;
                chk_ProveedorCredito.Checked = (bool)_proveedor.CO_PRO_CRE;
                if (chk_ProveedorCredito.Checked == true)
                {
                    lbl_ProveedorCredito.Visible =
                        txt_ProveedorLimiteCredito.Visible = true;
                    txt_ProveedorLimiteCredito.Value = _proveedor.CO_PRO_LIM_CRE;
                }
                else
                {
                    lbl_ProveedorCredito.Visible =
                        txt_ProveedorLimiteCredito.Visible = false;
                    txt_ProveedorLimiteCredito.Value = 0;
                }
                //_proveedor.CO_PRO_USU_ALI = Me.Usuario.NombreUsuario;
                txt_ProveedorEmail2.Text = _proveedor.CO_PRO_MAI2;
                txt_ProveedorEmail3.Text = _proveedor.CO_PRO_MAI3;
                txt_ProveedorObservaciones.Text = _proveedor.OBSERVACIONES;

                foreach (var _item in chk_ProveedorCultivo.Items)
                {
                    var _data = ((Telerik.Web.UI.ButtonListItem)_item);
                    if (_data.Value == "1" && _proveedor.TIENE_CULTIVO == true)
                    {
                        _data.Selected = true;
                    }
                    if (_data.Value == "2" && _proveedor.SOLO_ENVIO == true)
                    {
                        _data.Selected = true;
                    }
                    if (_data.Value == "3" && _proveedor.ENTREGA_DIRECTA == true)
                    {
                        _data.Selected = true;
                    }
                }
                ListProveedorBodega = client.ProveedorBodega_ObtenerPorEmpresaProveedor(txt_EmpresaRUC.Text, hdn_PersonaID.Value.ToString());
                hdn_ProveedorBodega.Value = ListProveedorBodega.FirstOrDefault().ProveedorBodegaID.ToString();
                cmb_Finca.SelectedValue = ListProveedorBodega.FirstOrDefault()?.AlmacenID.ToString();
                cmb_Finca_SelectedIndexChanged(null, null);
                cmb_Bodega.SelectedValue = ListProveedorBodega.FirstOrDefault()?.BodegaID.ToString();

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

        protected void cmb_TipoIdentificacion_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmb_ProveedorTipoIdentificacion.SelectedValue.ToString() != "0")
            {
                switch (cmb_ProveedorTipoIdentificacion.SelectedValue.ToString())
                {
                    case "0":
                        break;
                    case "1"://Cédula
                        txt_ProveedorIdentificacion.MaxLength = 10;
                        break;
                    case "2"://Ruc
                        txt_ProveedorIdentificacion.MaxLength = 13;
                        break;
                    case "3"://Pasaporte
                        break;
                }
            }
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
            }
        }
        protected void LimpiarControles()
        {
            cmb_ProveedorPais.SelectedValue = Guid.Empty.ToString();
            cmb_ProveedorTipo.SelectedValue = Guid.Empty.ToString();
            cmb_ProveedorTipoIdentificacion.SelectedValue = Guid.Empty.ToString();
            cmb_Finca.SelectedValue = "0";
            cmb_Bodega.Items.Clear();
            cmb_Bodega.DataSource = null;
            cmb_Bodega.DataBind();
            txt_Codigo.Text = "";
            hdn_PersonaID.Value = null;
            hdn_ProveedorBodega.Value = null;
            txt_EmpresaRUC.Text = "";
            txt_ProveedorCelular.Text = "";
            txt_ProveedorCiudad.Text = "";
            txt_ProveedorRepresentanteLegal.Text = "";
            txt_ProveedorCuenta.Text = "";
            txt_ProveedorEmail.Text = "";
            txt_ProveedorEmail2.Text = "";
            txt_ProveedorEmail3.Text = "";
            txt_ProveedorEstado.Text = "";
            txt_ProveedorIdentificacion.Text = "";
            txt_ProveedorLimiteCredito.Text = "";
            txt_ProveedorNombre.Text = "";
            txt_ProveedorObservaciones.Text = "";
            txt_ProveedorTelefono.Text = "";
            txt_ProveedorTelefono2.Text = "";
            chk_ProveedorConEsp.Checked = false;
            chk_ProveedorCredito.Checked = false;
            txt_ProveedorDireccion.Text = "";
            foreach (var _item in chk_ProveedorCultivo.Items)
            {
                var _data = ((Telerik.Web.UI.ButtonListItem)_item);
                _data.Selected = false;
            }
            lbl_ProveedorCredito.Visible =
                txt_ProveedorLimiteCredito.Visible = false;
            txt_ProveedorLimiteCredito.Value = 0;
            txt_EmpresaRUC.Text = Me.Usuario.EmpresaID;
            rlb_Finca.DataSource = ListFincas;
            rlb_Finca.DataBind();
            rlb_Bodega.DataSource = null;
            rlb_Bodega.Items.Clear();
            rlb_Bodega.DataBind();
            hdn_ProveedorBodega.Value = null;
        }
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscar.Visible = true;
            pnl_Datos.Visible = false;
        }
        private void Grabar()
        {
            LogicClient client = new LogicClient();
            List<SGF_ProveedorBodega> _provBod = new List<SGF_ProveedorBodega>();
            #region Validaciones
            if (txt_EmpresaRUC.Text == string.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar la Identificación");
                return;
            }

            if (txt_Codigo.Text == string.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Código de Proveedor");
                return;
            }

            int _res = client.Proveedor_ValidarCodigoPorEmpresaID(txt_EmpresaRUC.Text, txt_Codigo.Text.ToUpper());
            if (_res > 1)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "El Código de Proveedor ya existe");
                return;
            }

            if (cmb_ProveedorTipo.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Tipo de Proveedor");
                return;
            }
            if (cmb_ProveedorTipoIdentificacion.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el Tipo de Identificación");
                return;
            }
            if (txt_ProveedorNombre.Text == string.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre del Proveedor");
                return;
            }

            if (cmb_ProveedorPais.SelectedValue == Guid.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el País");
                return;
            }
            switch (txt_EmpresaRUC.MaxLength)
            {
                case 10:
                    //if (cmb_Genero.SelectedValue == Guid.Empty.ToString())
                    //{
                    //    VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el género");
                    //    return;
                    //}
                    //if (cmb_EstadoCivil.SelectedValue == Guid.Empty.ToString())
                    //{
                    //    VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar el estado civil");
                    //    return;
                    //}
                    break;
                case 13:
                    //if (txt_RepIdentificacion.Text == string.Empty.ToString())
                    //{
                    //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar la cédula del Representante Legal");
                    //    return;
                    //}
                    //if (txt_RepNombre.Text == string.Empty.ToString())
                    //{
                    //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar la cédula el Nombre del Representante Legal");
                    //    return;
                    //}
                    //if (txt_NombreComercial.Text == string.Empty.ToString())
                    //{
                    //    VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre Comercial");
                    //    return;
                    //}
                    break;
                default:
                    break;
            }
            if (txt_ProveedorEmail.Text == string.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Email");
                return;
            }
            if (txt_ProveedorTelefono.Text == string.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el # de Teléfono");
                return;
            }
            if (txt_ProveedorCelular.Text == string.Empty.ToString())
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el # Celular");
                return;
            }
            int cont = 0;
            foreach (var _item in chk_ProveedorCultivo.Items)
            {
                if (((Telerik.Web.UI.ButtonListItem)_item).Selected == true)
                    cont = cont + 1;
            }
            if (cont == 0)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar al menos un item en los Datos de Cultivo");
                return;
            }
            //string _result = ValidarListBox(rlb_Finca);
            //if (_result != string.Empty)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", _result);
            //    return;
            //}
            //_result = ValidarListBox(rlb_Bodega);
            //if (_result != string.Empty)
            //{
            //    VerMensaje("INFORMACIÓN", "info", "info", _result);
            //    return;
            //}
            if (cmb_Finca.SelectedValue.ToString() == "0")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Finca de Recepción");
                return;
            }
            if (cmb_Bodega.SelectedValue.ToString() == "0")
            {
                VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Bodega de recepción");
                return;
            }
            #endregion
            #region Proceso Grabar
            CO_PROVEEDOR _proveedor = new CO_PROVEEDOR();
            _proveedor.CO_PRO_COD = txt_ProveedorIdentificacion.Text;
            _proveedor.CO_EMP_RUC = Me.Usuario.EmpresaID;
            _proveedor.CO_TIP_PRO_COD = new Guid(cmb_ProveedorTipo.SelectedValue);
            _proveedor.CO_TIP_IDE_COD = new Guid(cmb_ProveedorTipoIdentificacion.SelectedValue);
            _proveedor.CO_PRO_CED = txt_ProveedorIdentificacion.Text;
            _proveedor.CO_PRO_NOM = txt_ProveedorNombre.Text;
            _proveedor.CO_PRO_CON = txt_ProveedorRepresentanteLegal.Text; //Contacto PRincipal del Proveedor
            _proveedor.CO_PRO_PAI = new Guid(cmb_ProveedorPais.SelectedValue);
            _proveedor.CO_PRO_CIU = txt_ProveedorCiudad.Text;
            _proveedor.CO_PRO_DIR = txt_ProveedorDireccion.Text;
            _proveedor.CO_PRO_TEL1 = txt_ProveedorTelefono.Text;
            _proveedor.CO_PRO_TEL2 = txt_ProveedorTelefono2.Text;
            _proveedor.CO_PRO_FAX = "";
            _proveedor.CO_PRO_CEL = txt_ProveedorCelular.Text;
            _proveedor.CO_PRO_MAI = txt_ProveedorEmail.Text;
            _proveedor.CO_PRO_EST = "ACTIVO";
            _proveedor.CO_PRO_CUE = txt_ProveedorCuenta.Text;
            _proveedor.CO_PRO_CON_ESP = chk_ProveedorConEsp.Checked;
            _proveedor.CO_PRO_CRE = chk_ProveedorCredito.Checked;
            _proveedor.CO_PRO_LIM_CRE = txt_ProveedorLimiteCredito.Value;
            _proveedor.CO_PRO_USU_ALI = Me.Usuario.NombreUsuario;
            _proveedor.CO_PRO_MAI2 = txt_ProveedorEmail2.Text;
            _proveedor.CO_PRO_MAI3 = txt_ProveedorEmail3.Text;
            _proveedor.OBSERVACIONES = txt_ProveedorObservaciones.Text;
            _proveedor.CODIGO = txt_Codigo.Text;
            foreach (var _item in chk_ProveedorCultivo.Items)
            {
                var _data = ((Telerik.Web.UI.ButtonListItem)_item);
                if (_data.Value == "1" && _data.Selected)
                {
                    _proveedor.TIENE_CULTIVO = true;
                }
                if (_data.Value == "2" && _data.Selected)
                {
                    _proveedor.SOLO_ENVIO = true;
                }
                if (_data.Value == "3" && _data.Selected)
                {
                    _proveedor.ENTREGA_DIRECTA = true;
                }
            }
            _provBod = RecogerDatosListBox();
            SGF_ProveedorBodega _newProvBod = new SGF_ProveedorBodega();
            _newProvBod.ProveedorBodegaID = hdn_ProveedorBodega.Value == null ? Guid.NewGuid() : hdn_ProveedorBodega.Value == "" ? Guid.NewGuid() : new Guid(hdn_ProveedorBodega.Value);
            _newProvBod.ProveedorID = txt_ProveedorIdentificacion.Text;
            _newProvBod.AlmacenID = int.Parse(cmb_Finca.SelectedValue);
            _newProvBod.BodegaID = int.Parse(cmb_Bodega.SelectedValue);
            _newProvBod.EmpresaID = Me.Usuario.EmpresaID;
            _newProvBod.ClasificadorID = "";
            _newProvBod.Usuario = Me.Usuario.NombreUsuario;
            _newProvBod.Observaciones = "";
            _newProvBod.FechaRegistro = DateTime.Now;
            _newProvBod.Estado = 1;
            client.Proveedor_Grabar(_proveedor, Utils.getHostName(Utils.getIP()), Utils.getIP());
            //foreach (SGF_ProveedorBodega item in _provBod)
            //{
            client.ProveedorBodega_Grabar(_newProvBod, Utils.getHostName(Utils.getIP()), Utils.getIP());
            //}
            VerMensaje("INFORMACIÓN", "info", "info", "Datos del Proveedor registrados correctamente");
            Cancelar();
            consultar();
            gv_Persona.DataBind();
            #endregion
        }

        private void SeleccionarListBox(RadListBox _list)
        {
            foreach (RadListBoxItem item in _list.Items)
            {
                if (item.Checked == true)
                {
                    break;
                }
            }
        }
        private string ValidarListBox(RadListBox _list)
        {
            string respuesta = "No tiene seleccionado ningún item en el listado de " + _list.ToolTip;
            foreach (RadListBoxItem item in _list.Items)
            {
                if (item.Checked == true)
                {
                    respuesta = "";
                    break;
                }
            }
            return respuesta;
        }

        private List<SGF_ProveedorBodega> RecogerDatosListBox()
        {
            string _Mercado = ValidarListBox(rlb_Finca);
            string _Paises = ValidarListBox(rlb_Bodega);
            List<SGF_ProveedorBodega> _newProvBod = new List<SGF_ProveedorBodega>();
            LogicClient client = new LogicClient();
            foreach (RadListBoxItem itemAlmacen in rlb_Finca.Items)
            {
                if (itemAlmacen.Checked == true)
                {
                    foreach (RadListBoxItem itemBodega in rlb_Bodega.Items)
                    {
                        if (itemBodega.Checked == true)
                        {
                            SGF_ProveedorBodega _newProducto = new SGF_ProveedorBodega();
                            _newProducto.ProveedorBodegaID = Guid.NewGuid();
                            _newProducto.ProveedorID = txt_ProveedorIdentificacion.Text;
                            _newProducto.AlmacenID = int.Parse(itemAlmacen.Value);
                            _newProducto.BodegaID = int.Parse(itemBodega.Value);
                            _newProducto.EmpresaID = Me.Usuario.EmpresaID;
                            _newProducto.ClasificadorID = "";
                            _newProducto.Usuario = Me.Usuario.NombreUsuario;
                            _newProducto.Observaciones = "";
                            _newProducto.FechaRegistro = DateTime.Now;
                            _newProducto.Estado = 1;
                            _newProvBod.Add(_newProducto);
                        }
                    }
                }
            }
            return _newProvBod;
        }

        private void VerMensaje(string title, string titIcon, string icon, string mensaje)
        {
            RadNotification1.Title = title;// "Friendship invitation";
            RadNotification1.TitleIcon = titIcon;// "none";//"info"
            RadNotification1.ContentIcon = icon;//"warning";//"info"
            RadNotification1.Text = mensaje;
            RadNotification1.Show();

        }

        protected void chk_ProveedorCredito_Click(object sender, EventArgs e)
        {
            if (chk_ProveedorCredito.Checked == true)
            {
                lbl_ProveedorCredito.Visible =
                txt_ProveedorLimiteCredito.Visible = true;

            }
            else
            {
                lbl_ProveedorCredito.Visible =
                txt_ProveedorLimiteCredito.Visible = false;
                txt_ProveedorLimiteCredito.Value = 0;
            }
        }

        protected void rlb_Finca_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadListBox radListBox = sender as RadListBox;
            if (radListBox != null && radListBox.SelectedItem != null)
            {
                string selectedText = radListBox.SelectedItem.Text;
                string selectedValue = radListBox.SelectedItem.Value;
                if (ListBodegas != null)
                {
                    rlb_Bodega.DataSource = ListBodegas.Where(x => x.CO_ALM_COD == int.Parse(selectedValue));
                    rlb_Bodega.DataTextField = "CO_BOD_NOM";
                    rlb_Bodega.DataValueField = "CO_BOD_COD";
                }
                rlb_Bodega.DataBind();
                //recorrerListBox(rlb_Formulario, 2);
            }
            else
            {
                // LabelSelectedItem.Text = "Selected Item: None";
            }
        }

        protected void cmb_Finca_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cmb_Finca.SelectedValue != null)
            {
                if (ListBodegas != null)
                {
                    cmb_Bodega.DataSource = ListBodegas.Where(x => x.CO_ALM_COD == int.Parse(cmb_Finca.SelectedValue) && x.CO_EMP_RUC == Me.Usuario.EmpresaID);
                    cmb_Bodega.DataTextField = "CO_BOD_NOM";
                    cmb_Bodega.DataValueField = "CO_BOD_COD";
                    cmb_Bodega.DataBind();
                    cmb_Bodega.Items.Insert(0, new RadComboBoxItem("Seleccionar la Bodega", "0"));
                }
            }
        }
    }
}