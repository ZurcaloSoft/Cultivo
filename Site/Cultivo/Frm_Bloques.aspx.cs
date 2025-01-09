using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using static System.Net.Mime.MediaTypeNames;

namespace SGF.Site.Cultivo
{
    public partial class Frm_Bloques : System.Web.UI.Page
    {
        #region Variables y Propiedades
        protected List<SGF_Clasificador> ListClasificadorTemporal
        {
            get
            {
                if (ViewState["ListClasificadorTemporal"] == null)
                    ViewState["ListClasificadorTemporal"] = new List<SGF_Clasificador>();
                return (List<SGF_Clasificador>)ViewState["ListClasificadorTemporal"];
            }
            set
            {
                ViewState["ListClasificadorTemporal"] = value;
            }
        }
        protected List<SGF_CultivoArea> ListAreaCultivo
        {
            get
            {
                if (ViewState["ListAreaCultivo"] == null)
                    ViewState["ListAreaCultivo"] = new List<SGF_CultivoArea>();
                return (List<SGF_CultivoArea>)ViewState["ListAreaCultivo"];
            }
            set
            {
                ViewState["ListAreaCultivo"] = value;
            }
        }
        protected SGF_Proveedor_VTA Proveedor_VTA
        {
            get
            {
                if (ViewState["Proveedor_VTA"] == null)
                    ViewState["Proveedor_VTA"] = new SGF_Proveedor_VTA();
                return (SGF_Proveedor_VTA)ViewState["Proveedor_VTA"];
            }
            set
            {
                ViewState["Proveedor_VTA"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Cultivo;
                if (Me.Usuario.TipoUsuarioID == TipoUsuario.Administrador)
                {
                    CargarParametros();
                    cargarCombos();
                    ConsultarMapaCultivo();
                    gv_MapaCultivo.DataBind();
                    cmb_BuscarSucursal.Enabled = true;
                    cmb_Sucursal.Enabled = true;
                }
                else
                {
                    if (verificarPermisos() == true)
                    {
                        CargarParametros();
                        cargarCombos();
                        ConsultarMapaCultivo();
                        gv_MapaCultivo.DataBind();
                        cmb_BuscarSucursal.Enabled = false;
                        cmb_Sucursal.Enabled = false;
                        cmb_Sucursal.SelectedValue = Me.Usuario.Cedula;
                    }
                    else
                    {
                        pnl_Buscador.Visible = false;
                    }
                }
            }
        }

        protected bool verificarPermisos()
        {
            LogicClient client = new LogicClient();
            Proveedor_VTA = client.Proveedor_ObtenerPorID_VTA(Me.Usuario.Cedula);
            if (Proveedor_VTA == null)
                return false;
            else
            {
                return Proveedor_VTA.TieneCultivo;
            }
        }
        protected void cargarCombos()
        {
            LogicClient client = new LogicClient();
            cmb_BuscarSucursal.DataSource = client.Proveedor_ObtenerTodo_VTA();
            cmb_BuscarSucursal.DataTextField = "ProveedorNombre";
            cmb_BuscarSucursal.DataValueField = "ProveedorID";
            cmb_BuscarSucursal.DataBind();
            cmb_BuscarSucursal.Items.Insert(0, new RadComboBoxItem("Seleccione la Finca", "0"));

            cmb_Sucursal.DataSource = client.Proveedor_ObtenerTodo_VTA();
            cmb_Sucursal.DataTextField = "ProveedorNombre";
            cmb_Sucursal.DataValueField = "ProveedorID";
            cmb_Sucursal.DataBind();
            cmb_Sucursal.Items.Insert(0, new RadComboBoxItem("Seleccione la Finca", "0"));

            cmb_CultivoArea.DataSource = client.Clasificador_ObtenerPorTipoClasificador(TipoClasificador.CultivoAreas);
            cmb_CultivoArea.DataTextField = "Nombre";
            cmb_CultivoArea.DataValueField = "ClasificadorID";
            cmb_CultivoArea.DataBind();
            cmb_CultivoArea.Items.Insert(0, new RadComboBoxItem("Seleccione el Área ", Guid.Empty.ToString()));

            cmb_CultivoBloque.DataSource = client.Clasificador_ObtenerPorTipoClasificador(TipoClasificador.CultivoBloques);
            cmb_CultivoBloque.DataTextField = "Nombre";
            cmb_CultivoBloque.DataValueField = "ClasificadorID";
            cmb_CultivoBloque.DataBind();
            cmb_CultivoBloque.Items.Insert(0, new RadComboBoxItem("Seleccione el Bloque", Guid.Empty.ToString()));

        }

        private void CargarParametros()
        {
            LogicClient client = new LogicClient();
            ListClasificadorTemporal = client.Clasificador_ObtenerTodo();
            hdn_CultivoArea.Value = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CultivoAreas).Max(x => int.Parse(x.Valor.ToString())).ToString();
            lbl_TextAreasValor.Text = hdn_CultivoArea.Value;
            txt_NroAreas.MaxValue = int.Parse(hdn_CultivoArea.Value);

            hdn_CultivoBloque.Value = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CultivoBloques).Max(x => int.Parse(x.Valor.ToString())).ToString();
            lbl_TextBloquesValor.Text = hdn_CultivoBloque.Value;
            txt_NroBloques.MaxValue = int.Parse(hdn_CultivoBloque.Value);

            hdn_CultivoLado.Value = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CultivoLados).Max(x => int.Parse(x.Valor.ToString())).ToString();
            lbl_TextLadosValor.Text = hdn_CultivoLado.Value;
            txt_NroLados.MaxValue = int.Parse(hdn_CultivoLado.Value);

            hdn_CultivoNave.Value = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CultivoNaves).Max(x => int.Parse(x.Valor.ToString())).ToString();
            lbl_TextNavesValor.Text = hdn_CultivoNave.Value;
            txt_NroNaves.MaxValue = int.Parse(hdn_CultivoNave.Value);

            hdn_CultivoCama.Value = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CultivoCamas).Max(x => int.Parse(x.Valor.ToString())).ToString();
            lbl_TextCamasValor.Text = hdn_CultivoCama.Value;
            txt_NroCamas.MaxValue = int.Parse(hdn_CultivoCama.Value);

            hdn_CultivoCuadro.Value = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == (Guid)TipoClasificador.CultivoCuadros).Max(x => int.Parse(x.Valor.ToString())).ToString();
            lbl_TextCuadrosValor.Text = hdn_CultivoCuadro.Value;
            txt_NroCuadros.MaxValue = int.Parse(hdn_CultivoCuadro.Value);
        }

        protected void btn_GenerarCampo_Click(object sender, EventArgs e)
        {
            pnl_CargarMapaCultivo.Visible = true;
            List<SGF_CultivoArea> _areaCultivo1 = new List<SGF_CultivoArea>();
            List<SGF_CultivoBloque> _bloqueCultivo = new List<SGF_CultivoBloque>();
            List<SGF_CultivoLado> _ladoCultivo = new List<SGF_CultivoLado>();
            List<SGF_CultivoNave> _naveCultivo = new List<SGF_CultivoNave>();
            List<SGF_CultivoCama> _camaCultivo = new List<SGF_CultivoCama>();
            List<SGF_CultivoCuadro> _cuadroCultivo = new List<SGF_CultivoCuadro>();
            SGF_Clasificador _tmpClasificador = new SGF_Clasificador();
            ListAreaCultivo = new List<SGF_CultivoArea>();
            LogicClient client = new LogicClient();
            DateTime _fechaRegistro = DateTime.Now;
            #region Validaciones
            if (txt_NroAreas.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Áreas debe ser mayor a 0.", "info");
                txt_NroAreas.Focus();
                return;
            }
            if (txt_NroBloques.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Bloques debe ser mayor a 0.", "info");
                txt_NroBloques.Focus();
                return;
            }
            if (txt_NroLados.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Lados debe ser mayor a 0.", "info");
                txt_NroLados.Focus();
                return;
            }
            if (txt_NroNaves.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Naves debe ser mayor a 0.", "info");
                txt_NroNaves.Focus();
                return;
            }
            if (txt_NroCamas.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Camas debe ser mayor a 0.", "info");
                txt_NroCamas.Focus();
                return;
            }
            if (txt_NroCuadros.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Cuadros debe ser mayor a 0.", "info");
                txt_NroCuadros.Focus();
                return;
            }
            if (txt_CantidadPlantas.Value == 0)
            {
                Utils.MessageBox(this, "", "La cantidad de Plantas por Bloque debe ser mayor a 0.", "info");
                txt_CantidadPlantas.Focus();
                return;
            }
            if (txt_AreaBloqueM2.Value == 0)
            {
                Utils.MessageBox(this, "", "El Área del Bloque debe ser mayor a 0.", "info");
                txt_AreaBloqueM2.Focus();
                return;
            }
            if ((txt_NroNaves.Value * txt_NroCamas.Value) > int.Parse(txt_NroCamas.MaxValue.ToString()))
            {
                Utils.MessageBox(this, "", "El Nro. de Camas por Nave es " + (txt_NroNaves.Value * txt_NroCamas.Value).ToString() + ", cantidad que supera el máximo permitido que es " + hdn_CultivoCama.Value, "info");
                txt_AreaBloqueM2.Focus();
                return;
            }
            decimal _sumAream2 = 0, _sumPlantas = 0;
            int _numAreas = client.CultivoArea_ContarAreaPorCampoID(new Guid(hdn_CampoCultivoID.Value));
            int _numBloques = client.CultivoBloque_ContarBloquePorCampoID(new Guid(hdn_CampoCultivoID.Value));

            #endregion
            try
            {
                for (int i = 1; i <= txt_NroAreas.Value; i++)
                {
                    _numAreas = _numAreas + 1;
                    _tmpClasificador = new SGF_Clasificador();
                    _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoAreas).First(x => x.Valor == _numAreas.ToString());
                    SGF_CultivoArea _area = new SGF_CultivoArea();
                    _area.CampoCultivoID = new Guid(hdn_CampoCultivoID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_CampoCultivoID.Value);
                    _area.CultivoAreaID = Guid.NewGuid();
                    _area.AreaID = _tmpClasificador.ClasificadorID;
                    _area.Nombre = _tmpClasificador.Nombre;// "AREA " + i.ToString();
                    _area.Orden = _tmpClasificador.Orden;
                    _area.FechaRegistro = _fechaRegistro;
                    _area.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _area.Aream2 = (decimal)(txt_AreaBloqueM2.Value * txt_NroBloques.Value);
                    _area.CantidadPlantas = (int)(txt_CantidadPlantas.Value * txt_NroBloques.Value);
                    _area.Estado = (int)Enums.Estado.Activo;
                    _sumAream2 = (decimal)(_sumAream2 + _area.Aream2);
                    _sumPlantas = (decimal)(_sumPlantas + _area.CantidadPlantas);
                    _bloqueCultivo = new List<SGF_CultivoBloque>();
                    //int contBloques = 0;
                    for (int j = 1; j <= txt_NroBloques.Value; j++)
                    {
                        //contBloques = contBloques + 1;
                        _numBloques = _numBloques + 1;
                        _tmpClasificador = new SGF_Clasificador();
                        _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoBloques).First(x => x.Valor == _numBloques.ToString());
                        SGF_CultivoBloque _bloque = new SGF_CultivoBloque();
                        _bloque.CultivoBloqueID = Guid.NewGuid();
                        _bloque.CultivoAreaID = _area.CultivoAreaID;
                        _bloque.BloqueID = _tmpClasificador.ClasificadorID;
                        _bloque.Nombre = _tmpClasificador.Nombre;// "BLOQUE " + j.ToString();
                        _bloque.Orden = _tmpClasificador.Orden;
                        _bloque.FechaRegistro = _fechaRegistro;
                        _bloque.UsuarioRegistro = Me.Usuario.NombreUsuario;
                        _bloque.Aream2 = (decimal)txt_AreaBloqueM2.Value;
                        _bloque.CantidadPlantas = (int)txt_CantidadPlantas.Value;
                        _bloque.Estado = (int)Enums.Estado.Activo;
                        _bloque.FechaInicioBloque = _fechaRegistro;
                        _ladoCultivo = new List<SGF_CultivoLado>();
                        int contLados = 0;
                        int contCama = 0;
                        for (int k = 1; k <= txt_NroLados.Value; k++)
                        {
                            int contNaves = 0;
                            contLados = contLados + 1;
                            _tmpClasificador = new SGF_Clasificador();
                            _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoLados).First(x => x.Valor == contLados.ToString());
                            SGF_CultivoLado _lado = new SGF_CultivoLado();
                            _lado.CultivoLadoID = Guid.NewGuid();
                            _lado.CultivoBloqueID = _bloque.CultivoBloqueID;
                            _lado.Orden = k;
                            _lado.LadoID = _tmpClasificador.ClasificadorID;
                            _lado.Nombre = _tmpClasificador.Nombre;// "LADO " + k.ToString();
                            _lado.FechaRegistro = _fechaRegistro;
                            _lado.UsuarioRegistro = Me.Usuario.NombreUsuario;
                            _lado.Aream2 = (decimal)(_bloque.Aream2 / (decimal)txt_NroLados.Value);
                            _lado.CantidadPlantas = (int)(Math.Round((double)(txt_CantidadPlantas.Value / txt_NroLados.Value)));
                            _lado.Estado = (int)Enums.Estado.Activo;
                            _naveCultivo = new List<SGF_CultivoNave>();
                            for (int m = 1; m <= txt_NroNaves.Value; m++)
                            {
                                contNaves = contNaves + 1;
                                _tmpClasificador = new SGF_Clasificador();
                                _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoNaves).First(x => x.Valor == contNaves.ToString());
                                SGF_CultivoNave _nave = new SGF_CultivoNave();
                                _nave.CultivoNaveID = Guid.NewGuid();
                                _nave.CultivoLadoID = _lado.CultivoLadoID;
                                _nave.Orden = m;
                                _nave.NaveID = _tmpClasificador.ClasificadorID;
                                _nave.Nombre = _tmpClasificador.Nombre;// "NAVE " + m.ToString();
                                _nave.FechaRegistro = _fechaRegistro;
                                _nave.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                _nave.Aream2 = _lado.Aream2 / (decimal)txt_NroNaves.Value;
                                _nave.CantidadPlantas = (int)(Math.Round((double)(_lado.CantidadPlantas / (decimal)txt_NroNaves.Value)));
                                _nave.Estado = (int)Enums.Estado.Activo;
                                _camaCultivo = new List<SGF_CultivoCama>();
                                //for (int n = 1; n <= (txt_NroCamas.Value) / (txt_NroLados.Value); n++)
                                for (int n = 1; n <= txt_NroCamas.Value; n++)
                                {
                                    contCama = contCama + 1;
                                    _tmpClasificador = new SGF_Clasificador();
                                    _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoCamas).First(x => x.Valor == contCama.ToString());
                                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                                    _cama.CultivoCamaID = Guid.NewGuid();
                                    _cama.CultivoNaveID = _nave.CultivoNaveID;
                                    _cama.Orden = n;
                                    _cama.CamaID = _tmpClasificador.ClasificadorID;
                                    _cama.Nombre = _tmpClasificador.Nombre;// "CAMA " + n.ToString();
                                    _cama.FechaRegistro = _fechaRegistro;
                                    _cama.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                    _cama.Aream2 = _nave.Aream2 / (decimal)txt_NroCamas.Value;
                                    _cama.CantidadPlantas = (int)(Math.Round((double)(_nave.CantidadPlantas / (decimal)txt_NroCamas.Value)));
                                    _cama.VariedadSembrada = Guid.Empty;
                                    _cama.Estado = (int)Enums.Estado.Activo;
                                    _cama.Responsable = Guid.Empty;
                                    _cama.Color = "White";
                                    _cuadroCultivo = new List<SGF_CultivoCuadro>();
                                    for (int p = 1; p <= txt_NroCuadros.Value; p++)
                                    {
                                        _tmpClasificador = new SGF_Clasificador();
                                        _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoCuadros).First(x => x.Valor == p.ToString());
                                        SGF_CultivoCuadro _cuadro = new SGF_CultivoCuadro();
                                        _cuadro.CultivoCuadroID = Guid.NewGuid();
                                        _cuadro.CultivoCamaID = _cama.CultivoCamaID;
                                        _cuadro.CuadroID = _tmpClasificador.ClasificadorID;
                                        _cuadro.Orden = p;
                                        _cuadro.Nombre = _tmpClasificador.Nombre;// "Cuadro " + p.ToString();
                                        _cuadro.FechaRegistro = _fechaRegistro;
                                        _cuadro.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                        _cuadro.Aream2 = _cama.Aream2 / (decimal)txt_NroCuadros.Value;
                                        _cuadro.CantidadPlantas = (int)(Math.Round((double)(_cama.CantidadPlantas / (decimal)txt_NroCuadros.Value))); ;
                                        _cuadro.Estado = (int)Enums.Estado.Activo;
                                        _cuadro.Color = "White";
                                        _cuadroCultivo.Add(_cuadro);
                                    }
                                    _cama.SGF_CultivoCuadro = _cuadroCultivo;
                                    _camaCultivo.Add(_cama);
                                }
                                _nave.SGF_CultivoCama = _camaCultivo;
                                _naveCultivo.Add(_nave);
                            }
                            _lado.SGF_CultivoNave = _naveCultivo;
                            _ladoCultivo.Add(_lado);
                        }
                        _bloque.SGF_CultivoLado = _ladoCultivo;
                        _bloqueCultivo.Add(_bloque);
                    }
                    _area.SGF_CultivoBloque = _bloqueCultivo;
                    ListAreaCultivo.Add(_area);
                }
                dlAreas.DataSource = ListAreaCultivo;
                dlAreas.DataBind();
                txt_NroPlantas.Value = (double)_sumPlantas;
                txt_AreaCultivo.Value = (double)_sumAream2;
                dlAreas.Enabled = false;

            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "No se pudo generar el Campo de Cultivo");
            }
        }

        protected bool VisualizarBotones(Guid _id, int _type, int _typeLado)
        {
            bool _visible = false;
            List<SGF_CultivoLado> _ladoCultivo = new List<SGF_CultivoLado>();
            List<SGF_CultivoNave> _naveCultivo = new List<SGF_CultivoNave>();
            List<SGF_CultivoCama> _camaCultivo = new List<SGF_CultivoCama>();
            Guid _idLado = Guid.Empty;
            string _Lado = "";
            if (_typeLado == 1)
                _Lado = "LZ";
            else
                _Lado = "LD";
            switch (_type)
            {
                case 1://Naves
                    foreach (SGF_CultivoArea _cArea in ListAreaCultivo.OrderBy(x => x.Orden))
                    {
                        foreach (SGF_CultivoBloque _cBloque in _cArea.SGF_CultivoBloque.OrderBy(x => x.Orden))
                        {
                            foreach (SGF_CultivoLado _cLado in _cBloque.SGF_CultivoLado.OrderBy(x => x.Orden))
                            {
                                _idLado = Guid.Empty;
                                foreach (SGF_CultivoNave _cNave in _cLado.SGF_CultivoNave)
                                {
                                    if (_cNave.CultivoNaveID == _id)
                                    {
                                        _idLado = _cNave.CultivoNaveID;
                                        break;
                                    }
                                }
                                if (_idLado != Guid.Empty)
                                {
                                    _idLado = _cLado.LadoID;
                                    break;
                                }
                            }
                            if (_idLado != Guid.Empty)
                            {
                                break;
                            }
                        }
                        if (_idLado != Guid.Empty)
                            break;
                    }
                    if (_idLado == Lados.LadoA && _Lado == "LZ")
                        _visible = true;
                    if (_idLado == Lados.LadoA && _Lado == "LD")
                        _visible = false;
                    if (_idLado == Lados.LadoB && _Lado == "LZ")
                        _visible = false;
                    if (_idLado == Lados.LadoB && _Lado == "LD")
                        _visible = true;
                    break;
                case 2://Camas
                    foreach (SGF_CultivoArea _cArea in ListAreaCultivo.OrderBy(x => x.Orden))
                    {
                        foreach (SGF_CultivoBloque _cBloque in _cArea.SGF_CultivoBloque.OrderBy(x => x.Orden))
                        {
                            foreach (SGF_CultivoLado _cLado in _cBloque.SGF_CultivoLado.OrderBy(x => x.Orden))
                            {
                                _idLado = Guid.Empty;
                                foreach (SGF_CultivoNave _cNave in _cLado.SGF_CultivoNave)
                                {
                                    if (_cNave.CultivoNaveID == _id)
                                    {
                                        _idLado = _cNave.CultivoLadoID;
                                        break;
                                    }
                                }
                                if (_idLado != Guid.Empty)
                                {
                                    _idLado = _cLado.LadoID;
                                    break;
                                }
                            }
                            if (_idLado != Guid.Empty)
                            {
                                //_idLado = _cLado.LadoID;
                                break;
                            }
                        }
                        if (_idLado != Guid.Empty)
                            break;
                    }
                    if (_idLado == Lados.LadoA && _Lado == "LZ")
                        _visible = false;
                    if (_idLado == Lados.LadoA && _Lado == "LD")
                        _visible = true;
                    if (_idLado == Lados.LadoB && _Lado == "LZ")
                        _visible = true;
                    if (_idLado == Lados.LadoB && _Lado == "LD")
                        _visible = false;
                    break;
            }
            return _visible;
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

        protected Color GetColor(Guid id, string estado)
        {
            switch (estado)
            {
                case "1":
                    if (id == Guid.Empty)
                        return Color.White;
                    else
                    {
                        if (id == (Guid)ActividadCultivo.PatronLibre)
                            return Color.Orange;
                        else
                            return Color.LightGreen;
                    }
                case "0":
                    return Color.Gray;
                default:
                    return Color.White; // Color por defecto
            }
        }

        protected Color GetColorCuadro(Guid cultivocamaId)
        {
            LogicClient client = new LogicClient();
            //var _cama = client.CultivoCama_ObtenerPorID(cultivocamaId);
            SGF_CultivoCama _cama = null;
            return GetColor(_cama == null ? Guid.Empty : _cama.VariedadSembrada, "1");
        }

        protected string GetNombreCuadro(Guid cultivocamaId, string nombre)
        {
            LogicClient client = new LogicClient();
            //var _cama = client.CultivoCama_ObtenerPorID(cultivocamaId);
            //var _variedad = client.Variedad_ObtenerPorID(_cama == null ? Guid.Empty : _cama.VariedadSembrada);
            SGF_Variedad _variedad = null;
            if (_variedad != null)
                return _variedad.Codigo;
            else
                return nombre;
        }

        protected string getString(Guid id)
        {
            return id.ToString();
        }
        protected void btn_Area_Click(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "javascript:AbrirModalMapaCultivo('9');", true);
        }

        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            pnl_Buscador.Visible = false;
            pnl_DatosCultivo.Visible = true;
            //chk_GenerarMapa.Visible = true;
            // LimpiarControles();
            hdn_CampoCultivoID.Value = Guid.Empty.ToString();
            tool_principal.Items[1].Visible = false; //Oculto el botón de Refrescar
            cmb_Sucursal.SelectedValue = Me.Usuario.Cedula;
        }

        protected void btn_Buscar_Click(object sender, ImageClickEventArgs e)
        {
            ConsultarMapaCultivo();
            gv_MapaCultivo.DataBind();
        }

        private void ConsultarMapaCultivo()
        {
            LogicClient client = new LogicClient();
            List<SGF_CampoCultivo> _cultivo = new List<SGF_CampoCultivo>();
            if (Me.Usuario.TipoUsuarioID == TipoUsuario.Administrador)
                _cultivo = client.MapaCultivo_ObtenerTodo();
            else
                _cultivo = client.MapaCultivo_ObtenerPorProveedorID(Proveedor_VTA.ProveedorID);

            gv_MapaCultivo.DataSource = _cultivo;
        }

        protected void gv_MapaCultivo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ConsultarMapaCultivo();
        }

        protected void gv_MapaCultivo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":

                    hdn_CampoCultivoID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_DatosCultivo.Visible = true;
                    tool_principal.Items[1].Visible = true; //Habilito el botón de Refrescar
                    CargarDatos();
                    break;
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

        protected void chk_GenerarMapa_Click(object sender, EventArgs e)
        {
            if (chk_GenerarMapa.Checked == true)
            {
                //pnl_CargarMapaCultivo.Visible = false;
                ////pnl_GenerarMapaCultivo.Visible = true;
                //rbt_TipoTerreno.Visible = true;
                //tb_ListAreas.Visible = false;

                pnl_CargarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivo.Visible = true;
                pnl_GenerarMapaCultivoIrregular.Visible = false;
                tr_AddArea.Visible = true;
                btn_GenerarBloques.Visible = false;
                btn_GenerarCampo.Visible = true;
            }
            else
            {
                pnl_CargarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivoIrregular.Visible = false;

                //pnl_CargarMapaCultivo.Visible = false;
                //pnl_GenerarMapaCultivo.Visible = false;
                //rbt_TipoTerreno.Visible = false;
                //tb_ListAreas.Visible = true;
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
                case "Refrescar":
                    CargarDatos();
                    break;
                case "Cancelar":
                    Cancelar();
                    break;
            }
        }

        protected void LimpiarControles()
        {
            hdn_CultivoAreaID.Value = null;
            hdn_CultivoArea.Value = null;
            hdn_CultivoBloque.Value = null;
            hdn_CultivoCama.Value = null;
            hdn_CultivoCuadro.Value = null;
            hdn_CultivoLado.Value = null;
            hdn_CultivoNave.Value = null;
            hdn_VariedadID.Value = null;
            gv_MapaCultivo.DataSource = null;
            txt_NombreCultivo.Text = "";
            txt_Descripcion.Text = "";
            txt_Direccion.Text = "";
            cmb_Sucursal.SelectedValue = "0";
            cmb_BuscarSucursal.SelectedValue = "0";
            txt_NroPlantas.Value = 0;
            txt_AreaCultivo.Value = 0;
            txt_Estado.Text = "";
            ListAreaCultivo = new List<SGF_CultivoArea>();
            cmb_CultivoArea.SelectedValue = Guid.Empty.ToString();
            cmb_CultivoBloque.SelectedValue = Guid.Empty.ToString();
            limpiarControlesGenerarMap();
            chk_GenerarMapa.Checked = false;
            dlAreas.DataSource = null;
            dlAreas.DataBind();

            dl_BloquesTotales.DataSource = null;
            dl_BloquesTotales.DataBind();

        }

        private void limpiarControlesGenerarMap()
        {
            txt_NroAreas.Value = 0;
            txt_NroBloques.Value = 0;
            txt_NroLados.Value = 0;
            txt_NroNaves.Value = 0;
            txt_NroCamas.Value = 0;
            txt_NroCuadros.Value = 0;
            txt_CantidadPlantas.Value = 0;
            txt_AreaBloqueM2.Value = 0;
        }
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_DatosCultivo.Visible = false;
            pnl_GenerarMapaCultivo.Visible = false;
            pnl_CargarMapaCultivo.Visible = false;
        }

        protected void CargarDatos()
        {
            LogicClient client = new LogicClient();
            SGF_CampoCultivo _campoCultivo = client.MapaCultivo_ObtenerPorID(new Guid(hdn_CampoCultivoID.Value));
            if (_campoCultivo != null)
            {
                cmb_Sucursal.SelectedValue = _campoCultivo.ProveedorID.ToString();
                txt_NombreCultivo.Text = _campoCultivo.Nombre;
                txt_Direccion.Text = _campoCultivo.Direccion;
                txt_AreaCultivo.Value = (double)_campoCultivo.Aream2;
                txt_NroPlantas.Value = _campoCultivo.CantidadPlantas;
                txt_Descripcion.Text = _campoCultivo.Descripcion;
                txt_Estado.Text = ObtenerNombreEstado(_campoCultivo.Estado);
                if (_campoCultivo.SGF_CultivoArea.Count > 0)
                {
                    chk_GenerarMapa.Visible = false;
                    chk_GenerarMapa.Checked = false;
                    chk_GenerarMapa_Click(null, null);
                    pnl_GenerarMapaCultivo.Visible = false;
                    pnl_CargarMapaCultivo.Visible = true;
                    ListAreaCultivo = _campoCultivo.SGF_CultivoArea;
                    dl_AreasTotales.DataSource = ListAreaCultivo;
                    dl_AreasTotales.DataBind();
                    tb_ListAreas.Visible = true;
                }
                //else
                //    chk_GenerarMapa.Visible = false;
            }
        }

        private void Grabar()
        {
            #region Validaciones
            String Message = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (cmb_Sucursal.SelectedValue.ToString() == Guid.Empty.ToString())
            {
                Message += "Debe seleccionar la Sucursal." + Environment.NewLine;
                sb.AppendLine("* Debe seleccionar la Sucursal");
                sb.AppendLine(" ");
                //VerMensaje("INFORMACIÓN", "info", "info", "Debe seleccionar la Sucursal");
                //return;
                //Utils.MessageBox(this, "", "Debe seleccionar la Sucursal", "info");

            }
            if (txt_NombreCultivo.Text == "")
            {
                Message += "Debe ingresar el Nombre." + Environment.NewLine;
                sb.AppendLine("* Debe ingresar el Nombre");
                sb.AppendLine(" ");
                //VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Nombre");
                //return;
            }
            if (txt_AreaCultivo.Value == 0)
            {
                Message += "Debe ingresar el Área Total del Cultivo." + Environment.NewLine;
                sb.AppendLine("* Debe ingresar el Área Total del Cultivo");
                sb.AppendLine(" ");
                //VerMensaje("INFORMACIÓN", "info", "info", "Debe ingresar el Área Total del Cultivo");
                //return;
            }
            string result = sb.ToString();
            if (Message != string.Empty)
            {
                VerMensaje("INFORMACIÓN", "info", "info", result);
                //Utils.MessageBox(this, "", Message, "info");
                return;
            }

            #endregion
            SGF_CampoCultivo _campoCultivo = new SGF_CampoCultivo();
            _campoCultivo.CampoCultivoID = new Guid(hdn_CampoCultivoID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_CampoCultivoID.Value);
            _campoCultivo.ProveedorID = cmb_Sucursal.SelectedValue;
            _campoCultivo.Nombre = txt_NombreCultivo.Text;
            _campoCultivo.Direccion = txt_Direccion.Text;
            _campoCultivo.Aream2 = (decimal)txt_AreaCultivo.Value;
            _campoCultivo.CantidadPlantas = (int)txt_NroPlantas.Value;
            _campoCultivo.Descripcion = txt_Descripcion.Text;
            _campoCultivo.UsuarioRegistro = Me.Usuario.NombreUsuario;
            _campoCultivo.FechaRegistro = DateTime.Now;
            _campoCultivo.Estado = 1;
            _campoCultivo.EmpresaID = Me.Usuario.EmpresaID;
            //if (chk_GenerarMapa.Checked == true)
            //   if (tr_AddArea.Visible == true)
            _campoCultivo.SGF_CultivoArea = ListAreaCultivo;

            LogicClient client = new LogicClient();
            client.MapaCultivo_Grabar(_campoCultivo, Utils.getIP(), Utils.getHostName(Utils.getIP()));//, Utils.getIP(), Utils.getHostName(Utils.getIP()));          
            VerMensaje("INFORMACIÓN", "info", "info", "Datos del Mapa de Cultivo Registrados correctamente");
            Cancelar();
        }

        protected void rbt_TipoTerreno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbt_TipoTerreno.SelectedValue == "1")
            {
                pnl_CargarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivo.Visible = true;
                pnl_GenerarMapaCultivoIrregular.Visible = false;
            }
            else
            if (rbt_TipoTerreno.SelectedValue == "2")
            {
                pnl_CargarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivoIrregular.Visible = true;
            }
            else
            {
                pnl_CargarMapaCultivo.Visible = true;
                pnl_GenerarMapaCultivo.Visible = false;
                pnl_GenerarMapaCultivoIrregular.Visible = false;
            }
        }

        protected void btn_Pruebalb_Click(object sender, EventArgs e)
        {
            string id1 = "1", id2 = "00000000-0000-0000-0000-000000000000";
            string script = "function f() { AbrirModalMapaCultivo('" + id1 + "','" + id2 + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void lbt_MostrarArea_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string commandArgument = clickedButton.CommandArgument;
            string script = "function f() { AbrirModalMapaCultivo('1','" + commandArgument + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void lbt_MostrarBloque_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string commandArgument = clickedButton.CommandArgument;
            string script = "function f() { AbrirModalMapaCultivo('2','" + commandArgument + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void lbt_MostrarLado_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string commandArgument = clickedButton.CommandArgument;
            string script = "function f() { AbrirModalMapaCultivo('3','" + commandArgument + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void lbt_MostrarNave_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string commandArgument = clickedButton.CommandArgument;
            string script = "function f() { AbrirModalMapaCultivo('4','" + commandArgument + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void lbt_MostrarCama_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string commandArgument = clickedButton.CommandArgument;
            string script = "function f() { AbrirModalMapaCultivo('5','" + commandArgument + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void lbt_MostrarCuadro_Click(object sender, EventArgs e)
        {
            LinkButton clickedButton = (LinkButton)sender;
            string commandArgument = clickedButton.CommandArgument;
            string script = "function f() { AbrirModalMapaCultivo('6','" + commandArgument + "'); Sys.Application.remove_load(f); };  Sys.Application.add_load(f); ";
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", script, true);
        }

        protected void dlNaves_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            LinkButton lbt_MostrarNaveLA = (LinkButton)e.Item.FindControl("lbt_MostrarNaveLA");
            LinkButton lbt_MostrarNaveLB = (LinkButton)e.Item.FindControl("lbt_MostrarNaveLB");
            #endregion
            #region Asignacion de valores en controles
            SGF_CultivoNave item = (SGF_CultivoNave)e.Item.DataItem;
            lbt_MostrarNaveLA.Visible = VisualizarBotones(item.CultivoNaveID, 1, 1);
            lbt_MostrarNaveLB.Visible = VisualizarBotones(item.CultivoNaveID, 1, 2);
            lbt_MostrarNaveLA.Text = item.Nombre;
            lbt_MostrarNaveLB.Text = item.Nombre;
            //Text = '<%# Eval("Nombre") %>'
            //    LA-- - Visible = '<%# VisualizarBotones(Guid.Parse(Eval("CultivoNaveID".ToString()).ToString()),1,1) 
            //    LB----Visible = '<%# VisualizarBotones(Guid.Parse(Eval("CultivoNaveID".ToString()).ToString()),1,2)  %>'
            #endregion
        }

        protected void dlCamas_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            LinkButton lbt_MostrarCamaLA = (LinkButton)e.Item.FindControl("lbt_MostrarCamaLA");
            LinkButton lbt_MostrarCamaLB = (LinkButton)e.Item.FindControl("lbt_MostrarCamaLB");
            #endregion
            #region Asignacion de valores en controles
            SGF_CultivoCama item = (SGF_CultivoCama)e.Item.DataItem;
            lbt_MostrarCamaLA.Visible = VisualizarBotones(item.CultivoNaveID, 2, 1);
            lbt_MostrarCamaLB.Visible = VisualizarBotones(item.CultivoNaveID, 2, 2);
            lbt_MostrarCamaLA.Text = item.Nombre;
            lbt_MostrarCamaLB.Text = item.Nombre;
            if (item.Estado == 0)
            {
                lbt_MostrarCamaLA.BackColor = Color.Gray;
                lbt_MostrarCamaLB.BackColor = Color.Gray;
            }
            else
            {
                lbt_MostrarCamaLA.BackColor = item.Color == null ? Color.White : System.Drawing.ColorTranslator.FromHtml(item.Color);
                lbt_MostrarCamaLB.BackColor = item.Color == null ? Color.White : System.Drawing.ColorTranslator.FromHtml(item.Color);
            }
            //Text='<%# Eval("Nombre") %>'
            //lbt_MostrarCamaLA.Visible = '<%# VisualizarBotones(Guid.Parse(Eval("CultivoNaveID".ToString()).ToString()),2,1)  %>'
            //lbt_MostrarCamaLB.Visible='<%# VisualizarBotones(Guid.Parse(Eval("CultivoNaveID".ToString()).ToString()),2,2)  %>'
            #endregion
        }

        protected void dlCuadros_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            LinkButton lbt_MostrarCuadro = (LinkButton)e.Item.FindControl("lbt_MostrarCuadro");
            #endregion
            #region Asignacion de valores en controles
            SGF_CultivoCuadro item = (SGF_CultivoCuadro)e.Item.DataItem;
            lbt_MostrarCuadro.BackColor = item.Color == null ? Color.White : System.Drawing.ColorTranslator.FromHtml(item.Color);
            lbt_MostrarCuadro.Text = item.Nombre;
            if (item.Estado == 0)
                lbt_MostrarCuadro.BackColor = Color.Gray;
            else
                lbt_MostrarCuadro.BackColor = item.Color == null ? Color.White : System.Drawing.ColorTranslator.FromHtml(item.Color);
            //Text='<%# Eval("Nombre") %>'
            //lbt_MostrarCamaLA.Visible = '<%# VisualizarBotones(Guid.Parse(Eval("CultivoNaveID".ToString()).ToString()),2,1)  %>'
            //lbt_MostrarCamaLB.Visible='<%# VisualizarBotones(Guid.Parse(Eval("CultivoNaveID".ToString()).ToString()),2,2)  %>'
            #endregion
        }

        protected void btn_RetornoVentana_Click(object sender, ImageClickEventArgs e)
        {
            //if (hdn_CampoCultivoID.Value == "") return;
            //  CargarDatos();
            //txt_CedulaPersona.Text = hdn_PersonaIdentificacion.Value;
            //txt_NombrePersona.Text = hdn_PersonaNombre.Value;
            ////VerMensaje("INFORMACIÓN", "info", "info", "La actividad seleccionada ya se encuentra agregada debe escoger otra Actividad");
            ////return;
        }

        protected void dl_AreasTotales_ItemCommand(object source, DataListCommandEventArgs e)
        {
            LogicClient client = new LogicClient();
            switch (e.CommandName)
            {
                case "mostrar":
                    Guid ID = new Guid(e.CommandArgument.ToString());
                    hdn_CultivoAreaID.Value = ID.ToString();
                    var _bloques = client.CultivoBloque_ObtenerPorAreaID(ID);

                    dlAreas.DataSource = null;
                    dlAreas.DataBind();

                    dl_BloquesTotales.DataSource = _bloques.OrderBy(x => x.Orden);
                    dl_BloquesTotales.DataBind();

                    field_Bloques.Visible = true;
                    pnl_CargarMapaCultivo.Visible = true;
                    pnl_GenerarMapaCultivo.Visible = false;
                    pnl_GenerarMapaCultivoIrregular.Visible = false;
                    break;
            }
        }

        protected void dl_AreasTotales_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            HiddenField hdn_CultivoArea_ID = (HiddenField)e.Item.FindControl("hdn_CultivoArea_ID");
            RadLabel lbl_NombreArea = (RadLabel)e.Item.FindControl("lbl_NombreArea");
            RadNumericTextBox txt_Aream2 = (RadNumericTextBox)e.Item.FindControl("txt_Aream2");
            RadNumericTextBox txt_NroPlantas = (RadNumericTextBox)e.Item.FindControl("txt_NroPlantas");
            RadImageButton btn_MostrarBloques = (RadImageButton)e.Item.FindControl("btn_MostrarBloques");
            #endregion
            #region Asignacion de valores en controles
            SGF_CultivoArea item = (SGF_CultivoArea)e.Item.DataItem;
            hdn_CultivoArea_ID.Value = item.CultivoAreaID.ToString();
            lbl_NombreArea.Text = item.Nombre.ToUpper();
            //txt_Aream2.Value = (double)item.Aream2;
            //txt_NroPlantas.Value = item.CantidadPlantas;
            btn_MostrarBloques.Text = "";// item.Nombre.ToUpper();
            btn_MostrarBloques.ToolTip = "Visualizar Bloques del " + item.Nombre.ToUpper();
            //btn_MostrarBloques.SingleClickText = "";
            btn_MostrarBloques.SingleClickText = "Listar Bloques del " + item.Nombre.ToUpper() + "...";
            btn_MostrarBloques.BorderColor = Color.DarkBlue;
            btn_MostrarBloques.BorderStyle = BorderStyle.Double;
            #endregion
        }

        protected void dl_BloquesTotales_ItemCommand(object source, DataListCommandEventArgs e)
        {
            LogicClient client = new LogicClient();
            switch (e.CommandName)
            {
                case "mostrar":
                    Guid ID = new Guid(e.CommandArgument.ToString());
                    Guid _AreaID = new Guid(hdn_CultivoAreaID.Value);
                    var _area = client.CultivoArea_ObtenerPorID(new Guid(hdn_CultivoAreaID.Value));
                    var _bloq = client.CultivoBloque_ObtenerTodoPorID(ID);
                    var _resultado = ArmarBloque(_area, _bloq);
                    dlAreas.DataSource = _resultado;
                    dlAreas.DataBind();
                    pnl_CargarMapaCultivo.Visible = true;
                    pnl_GenerarMapaCultivo.Visible = false;
                    pnl_GenerarMapaCultivoIrregular.Visible = false;
                    dlAreas.Enabled = true;
                    break;
            }
        }

        private List<SGF_CultivoArea> ArmarBloque(SGF_CultivoArea _area, SGF_CultivoBloque _bloque)
        {
            LogicClient client = new LogicClient();
            List<SGF_CultivoArea> _tArea = new List<SGF_CultivoArea>();
            List<SGF_CultivoBloque> _tBloque = new List<SGF_CultivoBloque>();
            List<SGF_CultivoLado> _tLado = new List<SGF_CultivoLado>();
            List<SGF_CultivoNave> _tNave = new List<SGF_CultivoNave>();
            List<SGF_CultivoCama> _tCama = new List<SGF_CultivoCama>();
            List<SGF_CultivoCuadro> _tCuadro = new List<SGF_CultivoCuadro>();

            _tBloque.Add(_bloque);
            _area.SGF_CultivoBloque = _tBloque;
            _tArea.Add(_area);
            foreach (SGF_CultivoArea _itemArea in _tArea)
            {
                foreach (SGF_CultivoBloque _itemBloque in _tBloque)
                {
                    _tLado = client.CultivoLado_ObtenerPorBloqueID(_itemBloque.CultivoBloqueID);
                    foreach (SGF_CultivoLado _itemLado in _tLado)
                    {
                        _tNave = client.CultivoNave_ObtenerPorLadoID(_itemLado.CultivoLadoID);
                        foreach (SGF_CultivoNave _itemNave in _tNave)
                        {
                            _tCama = client.CultivoCama_ObtenerPorNaveID(_itemNave.CultivoNaveID);
                            foreach (SGF_CultivoCama _itemCama in _tCama)
                            {
                                _tCuadro = client.CultivoCuadro_ObtenerPorCamaID(_itemCama.CultivoCamaID);
                                foreach (SGF_CultivoCuadro _itemCuadro in _tCuadro)
                                {

                                }
                                _itemCama.SGF_CultivoCuadro = _tCuadro;
                            }
                            _itemNave.SGF_CultivoCama = _tCama;
                        }
                        _itemLado.SGF_CultivoNave = _tNave;
                    }
                    _itemBloque.SGF_CultivoLado = _tLado;
                }
                _itemArea.SGF_CultivoBloque = _tBloque;
            }
            ListAreaCultivo = _tArea;
            return _tArea;
        }

        protected void dl_BloquesTotales_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.DataItem == null) return;
            #region Declaracion de controles
            RadLabel lbl_NombreBloque = (RadLabel)e.Item.FindControl("lbl_NombreBloque");
            HiddenField hdn_CultivoBloqueID = (HiddenField)e.Item.FindControl("hdn_CultivoBloqueID");
            RadImageButton btn_MostrarBloque = (RadImageButton)e.Item.FindControl("btn_MostrarBloque");
            #endregion
            #region Asignacion de valores en controles
            SGF_CultivoBloque item = (SGF_CultivoBloque)e.Item.DataItem;
            lbl_NombreBloque.Text = item.Nombre.ToUpper();
            hdn_CultivoBloqueID.Value = item.CultivoAreaID.ToString();
            btn_MostrarBloque.Text = "";// item.Nombre.ToUpper();
            btn_MostrarBloque.ToolTip = "Visualizar Mapa de Cultivo del " + item.Nombre.ToUpper();
            //btn_MostrarBloque.SingleClickText = "";
            btn_MostrarBloque.SingleClickText = "Generar Mapa de Cultivo del " + item.Nombre.ToUpper() + "...";
            btn_MostrarBloque.BorderColor = Color.DarkBlue;
            btn_MostrarBloque.BorderStyle = BorderStyle.Double;
            #endregion
        }

        protected void lnk_AddArea_Click(object sender, EventArgs e)
        {
            pnl_CargarMapaCultivo.Visible = false;
            pnl_GenerarMapaCultivo.Visible = true;
            pnl_GenerarMapaCultivoIrregular.Visible = false;
            tr_AddArea.Visible = true;
            btn_GenerarBloques.Visible = false;
            btn_GenerarCampo.Visible = true;
            lbl_GenerarMapaCultivo.Text = "AGREGAR ÁREAS AL MAPA DE CULTIVO TOTAL";
            limpiarControlesGenerarMap();

        }

        protected void lnk_AddBloque_Click(object sender, EventArgs e)
        {
            pnl_CargarMapaCultivo.Visible = false;
            pnl_GenerarMapaCultivo.Visible = true;
            pnl_GenerarMapaCultivoIrregular.Visible = false;
            tr_AddArea.Visible = false;
            btn_GenerarBloques.Visible = true;
            btn_GenerarCampo.Visible = false;
            lbl_GenerarMapaCultivo.Text = "AGREGAR BLOQUES AL MAPA DE CULTIVO TOTAL";
            limpiarControlesGenerarMap();
        }

        protected void btn_GenerarCampoBloque_Click(object sender, EventArgs e)
        {
            pnl_CargarMapaCultivo.Visible = true;
            List<SGF_CultivoArea> _areaCultivo = new List<SGF_CultivoArea>();
            List<SGF_CultivoBloque> _bloqueCultivo = new List<SGF_CultivoBloque>();
            List<SGF_CultivoLado> _ladoCultivo = new List<SGF_CultivoLado>();
            List<SGF_CultivoNave> _naveCultivo = new List<SGF_CultivoNave>();
            List<SGF_CultivoCama> _camaCultivo = new List<SGF_CultivoCama>();
            List<SGF_CultivoCuadro> _cuadroCultivo = new List<SGF_CultivoCuadro>();
            SGF_Clasificador _tmpClasificador = new SGF_Clasificador();
            ListAreaCultivo = new List<SGF_CultivoArea>();
            LogicClient client = new LogicClient();
            DateTime _fechaRegistro = DateTime.Now;
            int _numBloques = client.CultivoBloque_ContarBloquePorCampoID(new Guid(hdn_CampoCultivoID.Value));

            try
            {
                for (int i = 1; i <= txt_NroAreas.Value; i++)
                {
                    _tmpClasificador = new SGF_Clasificador();
                    _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoAreas).First(x => x.Valor == i.ToString());
                    SGF_CultivoArea _area = new SGF_CultivoArea();
                    _area.CampoCultivoID = Guid.NewGuid();
                    _area.CultivoAreaID = Guid.NewGuid();
                    _area.AreaID = _tmpClasificador.ClasificadorID;
                    _area.Nombre = _tmpClasificador.Nombre;// "AREA " + i.ToString();
                    _area.Orden = i;
                    _area.FechaRegistro = _fechaRegistro;
                    _area.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _area.Aream2 = 0;
                    _area.CantidadPlantas = 0;
                    _bloqueCultivo = new List<SGF_CultivoBloque>();
                    // int contBloques = 0;
                    for (int j = 1; j <= txt_NroBloques.Value; j++)
                    {
                        //   contBloques = contBloques + 1;
                        _numBloques = _numBloques + 1;
                        _tmpClasificador = new SGF_Clasificador();
                        _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoBloques).First(x => x.Valor == _numBloques.ToString());
                        SGF_CultivoBloque _bloque = new SGF_CultivoBloque();
                        _bloque.CultivoBloqueID = Guid.NewGuid();
                        _bloque.CultivoAreaID = _area.CultivoAreaID;
                        _bloque.BloqueID = _tmpClasificador.ClasificadorID;
                        _bloque.Nombre = _tmpClasificador.Nombre;// "BLOQUE " + j.ToString();
                        _bloque.Orden = _tmpClasificador.Orden;
                        _bloque.FechaRegistro = _fechaRegistro;
                        _bloque.UsuarioRegistro = Me.Usuario.NombreUsuario;
                        _bloque.Aream2 = 0;
                        _bloque.CantidadPlantas = 0;
                        _ladoCultivo = new List<SGF_CultivoLado>();
                        int contLados = 0;
                        int contCama = 0;
                        for (int k = 1; k <= txt_NroLados.Value; k++)
                        {
                            int contNaves = 0;
                            contLados = contLados + 1;
                            _tmpClasificador = new SGF_Clasificador();
                            _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoLados).First(x => x.Valor == contLados.ToString());
                            SGF_CultivoLado _lado = new SGF_CultivoLado();
                            _lado.CultivoLadoID = Guid.NewGuid();
                            _lado.CultivoBloqueID = _bloque.CultivoBloqueID;
                            _lado.Orden = k;
                            _lado.LadoID = _tmpClasificador.ClasificadorID;
                            _lado.Nombre = _tmpClasificador.Nombre;// "LADO " + k.ToString();
                            _lado.FechaRegistro = _fechaRegistro;
                            _lado.UsuarioRegistro = Me.Usuario.NombreUsuario;
                            _lado.Aream2 = 0;
                            _lado.CantidadPlantas = 0;
                            _naveCultivo = new List<SGF_CultivoNave>();
                            for (int m = 1; m <= txt_NroNaves.Value; m++)
                            {
                                contNaves = contNaves + 1;
                                _tmpClasificador = new SGF_Clasificador();
                                _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoNaves).First(x => x.Valor == contNaves.ToString());
                                SGF_CultivoNave _nave = new SGF_CultivoNave();
                                _nave.CultivoNaveID = Guid.NewGuid();
                                _nave.CultivoLadoID = _lado.CultivoLadoID;
                                _nave.Orden = m;
                                _nave.NaveID = _tmpClasificador.ClasificadorID;
                                _nave.Nombre = _tmpClasificador.Nombre;// "NAVE " + m.ToString();
                                _nave.FechaRegistro = _fechaRegistro;
                                _nave.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                _nave.Aream2 = 0;
                                _nave.CantidadPlantas = 0;
                                _camaCultivo = new List<SGF_CultivoCama>();
                                for (int n = 1; n <= txt_NroCamas.Value; n++)
                                {
                                    contCama = contCama + 1;
                                    _tmpClasificador = new SGF_Clasificador();
                                    _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoCamas).First(x => x.Valor == contCama.ToString());
                                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                                    _cama.CultivoCamaID = Guid.NewGuid();
                                    _cama.CultivoNaveID = _nave.CultivoNaveID;
                                    _cama.Orden = n;
                                    _cama.CamaID = _tmpClasificador.ClasificadorID;
                                    _cama.Nombre = _tmpClasificador.Nombre;// "CAMA " + n.ToString();
                                    _cama.FechaRegistro = _fechaRegistro;
                                    _cama.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                    _cama.Aream2 = 0;
                                    _cama.CantidadPlantas = 0;
                                    _cama.VariedadSembrada = Guid.Empty;
                                    _cama.Color = "White";
                                    _cuadroCultivo = new List<SGF_CultivoCuadro>();
                                    for (int p = 1; p <= txt_NroCuadros.Value; p++)
                                    {
                                        _tmpClasificador = new SGF_Clasificador();
                                        _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoCuadros).First(x => x.Valor == p.ToString());
                                        SGF_CultivoCuadro _cuadro = new SGF_CultivoCuadro();
                                        _cuadro.CultivoCuadroID = Guid.NewGuid();
                                        _cuadro.CultivoCamaID = _nave.CultivoNaveID;
                                        _cuadro.CuadroID = _tmpClasificador.ClasificadorID;
                                        _cuadro.Orden = p;
                                        _cuadro.Nombre = _tmpClasificador.Nombre;// "Cuadro " + p.ToString();
                                        _cuadro.FechaRegistro = _fechaRegistro;
                                        _cuadro.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                        _cuadro.Aream2 = 0;
                                        _cuadro.CantidadPlantas = 0;
                                        _cuadro.Color = "White";
                                        _cuadroCultivo.Add(_cuadro);
                                    }
                                    _cama.SGF_CultivoCuadro = _cuadroCultivo;
                                    _camaCultivo.Add(_cama);
                                }
                                _nave.SGF_CultivoCama = _camaCultivo;
                                _naveCultivo.Add(_nave);
                            }
                            _lado.SGF_CultivoNave = _naveCultivo;
                            _ladoCultivo.Add(_lado);
                        }
                        _bloque.SGF_CultivoLado = _ladoCultivo;
                        _bloqueCultivo.Add(_bloque);
                    }
                    _area.SGF_CultivoBloque = _bloqueCultivo;
                    ListAreaCultivo.Add(_area);
                }
                dlAreas.DataSource = ListAreaCultivo;
                dlAreas.DataBind();
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "No se pudo generar el Campo de Cultivo");
            }
        }

        protected void btn_GenerarBloques_Click(object sender, EventArgs e)
        {
            pnl_CargarMapaCultivo.Visible = true;
            List<SGF_CultivoArea> _areaCultivo1 = new List<SGF_CultivoArea>();
            List<SGF_CultivoBloque> _bloqueCultivo = new List<SGF_CultivoBloque>();
            List<SGF_CultivoLado> _ladoCultivo = new List<SGF_CultivoLado>();
            List<SGF_CultivoNave> _naveCultivo = new List<SGF_CultivoNave>();
            List<SGF_CultivoCama> _camaCultivo = new List<SGF_CultivoCama>();
            List<SGF_CultivoCuadro> _cuadroCultivo = new List<SGF_CultivoCuadro>();
            SGF_Clasificador _tmpClasificador = new SGF_Clasificador();
            ListAreaCultivo = new List<SGF_CultivoArea>();
            LogicClient client = new LogicClient();
            DateTime _fechaRegistro = DateTime.Now;
            txt_NroAreas.Value = 1;
            #region Validaciones
            if (txt_NroAreas.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Áreas debe ser mayor a 0.", "info");
                txt_NroAreas.Focus();
                return;
            }
            if (txt_NroBloques.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Bloques debe ser mayor a 0.", "info");
                txt_NroBloques.Focus();
                return;
            }
            if (txt_NroLados.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Lados debe ser mayor a 0.", "info");
                txt_NroLados.Focus();
                return;
            }
            if (txt_NroNaves.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Naves debe ser mayor a 0.", "info");
                txt_NroNaves.Focus();
                return;
            }
            if (txt_NroCamas.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Camas debe ser mayor a 0.", "info");
                txt_NroCamas.Focus();
                return;
            }
            if (txt_NroCuadros.Value == 0)
            {
                Utils.MessageBox(this, "", "El Nro. de Cuadros debe ser mayor a 0.", "info");
                txt_NroCuadros.Focus();
                return;
            }
            if (txt_CantidadPlantas.Value == 0)
            {
                Utils.MessageBox(this, "", "La cantidad de Plantas por Bloque debe ser mayor a 0.", "info");
                txt_CantidadPlantas.Focus();
                return;
            }
            if (txt_AreaBloqueM2.Value == 0)
            {
                Utils.MessageBox(this, "", "El Área del Bloque debe ser mayor a 0.", "info");
                txt_AreaBloqueM2.Focus();
                return;
            }
            if ((txt_NroNaves.Value * txt_NroCamas.Value) > int.Parse(txt_NroCamas.MaxValue.ToString()))
            {
                Utils.MessageBox(this, "", "El Nro. de Camas por Nave es " + (txt_NroNaves.Value * txt_NroCamas.Value).ToString() + ", cantidad que supera el máximo permitido que es " + hdn_CultivoCama.Value, "info");
                txt_AreaBloqueM2.Focus();
                return;
            }
            decimal _sumAream2 = 0, _sumPlantas = 0;
            int _numBloques = client.CultivoBloque_ContarBloquePorCampoID(new Guid(hdn_CampoCultivoID.Value));
            var _areaC = client.CultivoArea_ObtenerPorID(new Guid(hdn_CultivoAreaID.Value));
            #endregion
            try
            {
                for (int i = 1; i <= txt_NroAreas.Value; i++)
                {
                    //_tmpClasificador = new SGF_Clasificador();
                    //_tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoAreas).First(x => x.Valor == i.ToString());
                    SGF_CultivoArea _area = new SGF_CultivoArea();
                    _area.CampoCultivoID = new Guid(hdn_CampoCultivoID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_CampoCultivoID.Value);
                    _area.CultivoAreaID = new Guid(hdn_CultivoAreaID.Value) == Guid.Empty ? Guid.NewGuid() : new Guid(hdn_CultivoAreaID.Value);
                    _area.AreaID = _areaC.AreaID;// _tmpClasificador.ClasificadorID;
                    _area.Nombre = _areaC.Nombre;// _tmpClasificador.Nombre;// "AREA " + i.ToString();
                    _area.Orden = _areaC.Orden;// _tmpClasificador.Orden;
                    _area.FechaRegistro = _fechaRegistro;
                    _area.UsuarioRegistro = Me.Usuario.NombreUsuario;
                    _area.Aream2 = _areaC.Aream2;// (decimal)(txt_AreaBloqueM2.Value * txt_NroBloques.Value);
                    _area.CantidadPlantas = _areaC.CantidadPlantas;// (int)(txt_CantidadPlantas.Value * txt_NroBloques.Value);
                    _area.Estado = (int)Enums.Estado.Activo;
                    _sumAream2 = (decimal)(_sumAream2 + _area.Aream2);
                    _sumPlantas = (decimal)(_sumPlantas + _area.CantidadPlantas);
                    _bloqueCultivo = new List<SGF_CultivoBloque>();
                    //int contBloques = 0;
                    for (int j = 1; j <= txt_NroBloques.Value; j++)
                    {
                        //  contBloques = contBloques + 1;
                        _numBloques = _numBloques + 1;
                        _tmpClasificador = new SGF_Clasificador();
                        _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoBloques).First(x => x.Valor == _numBloques.ToString());
                        SGF_CultivoBloque _bloque = new SGF_CultivoBloque();
                        _bloque.CultivoBloqueID = Guid.NewGuid();
                        _bloque.CultivoAreaID = _area.CultivoAreaID;
                        _bloque.BloqueID = _tmpClasificador.ClasificadorID;
                        _bloque.Nombre = _tmpClasificador.Nombre;// "BLOQUE " + j.ToString();
                        _bloque.Orden = _tmpClasificador.Orden;
                        _bloque.FechaRegistro = _fechaRegistro;
                        _bloque.UsuarioRegistro = Me.Usuario.NombreUsuario;
                        _bloque.Aream2 = (decimal)txt_AreaBloqueM2.Value;
                        _bloque.CantidadPlantas = (int)txt_CantidadPlantas.Value;
                        _bloque.Estado = (int)Enums.Estado.Activo;
                        _bloque.FechaInicioBloque = _fechaRegistro;
                        _ladoCultivo = new List<SGF_CultivoLado>();
                        int contLados = 0;
                        int contCama = 0;
                        for (int k = 1; k <= txt_NroLados.Value; k++)
                        {
                            int contNaves = 0;
                            contLados = contLados + 1;
                            _tmpClasificador = new SGF_Clasificador();
                            _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoLados).First(x => x.Valor == contLados.ToString());
                            SGF_CultivoLado _lado = new SGF_CultivoLado();
                            _lado.CultivoLadoID = Guid.NewGuid();
                            _lado.CultivoBloqueID = _bloque.CultivoBloqueID;
                            _lado.Orden = k;
                            _lado.LadoID = _tmpClasificador.ClasificadorID;
                            _lado.Nombre = _tmpClasificador.Nombre;// "LADO " + k.ToString();
                            _lado.FechaRegistro = _fechaRegistro;
                            _lado.UsuarioRegistro = Me.Usuario.NombreUsuario;
                            _lado.Aream2 = (decimal)(_bloque.Aream2 / (decimal)txt_NroLados.Value);
                            _lado.CantidadPlantas = (int)(Math.Round((double)(txt_CantidadPlantas.Value / txt_NroLados.Value)));
                            _lado.Estado = (int)Enums.Estado.Activo;
                            _naveCultivo = new List<SGF_CultivoNave>();
                            for (int m = 1; m <= txt_NroNaves.Value; m++)
                            {
                                contNaves = contNaves + 1;
                                _tmpClasificador = new SGF_Clasificador();
                                _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoNaves).First(x => x.Valor == contNaves.ToString());
                                SGF_CultivoNave _nave = new SGF_CultivoNave();
                                _nave.CultivoNaveID = Guid.NewGuid();
                                _nave.CultivoLadoID = _lado.CultivoLadoID;
                                _nave.Orden = m;
                                _nave.NaveID = _tmpClasificador.ClasificadorID;
                                _nave.Nombre = _tmpClasificador.Nombre;// "NAVE " + m.ToString();
                                _nave.FechaRegistro = _fechaRegistro;
                                _nave.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                _nave.Aream2 = _lado.Aream2 / (decimal)txt_NroNaves.Value;
                                _nave.CantidadPlantas = (int)(Math.Round((double)(_lado.CantidadPlantas / (decimal)txt_NroNaves.Value)));
                                _nave.Estado = (int)Enums.Estado.Activo;
                                _camaCultivo = new List<SGF_CultivoCama>();
                                //for (int n = 1; n <= (txt_NroCamas.Value) / (txt_NroLados.Value); n++)
                                for (int n = 1; n <= txt_NroCamas.Value; n++)
                                {
                                    contCama = contCama + 1;
                                    _tmpClasificador = new SGF_Clasificador();
                                    _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoCamas).First(x => x.Valor == contCama.ToString());
                                    SGF_CultivoCama _cama = new SGF_CultivoCama();
                                    _cama.CultivoCamaID = Guid.NewGuid();
                                    _cama.CultivoNaveID = _nave.CultivoNaveID;
                                    _cama.Orden = n;
                                    _cama.CamaID = _tmpClasificador.ClasificadorID;
                                    _cama.Nombre = _tmpClasificador.Nombre;// "CAMA " + n.ToString();
                                    _cama.FechaRegistro = _fechaRegistro;
                                    _cama.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                    _cama.Aream2 = _nave.Aream2 / (decimal)txt_NroCamas.Value;
                                    _cama.CantidadPlantas = (int)(Math.Round((double)(_nave.CantidadPlantas / (decimal)txt_NroCamas.Value)));
                                    _cama.VariedadSembrada = Guid.Empty;
                                    _cama.Estado = (int)Enums.Estado.Activo;
                                    _cama.Responsable = Guid.Empty;
                                    _cama.Color = "White";
                                    _cuadroCultivo = new List<SGF_CultivoCuadro>();
                                    for (int p = 1; p <= txt_NroCuadros.Value; p++)
                                    {
                                        _tmpClasificador = new SGF_Clasificador();
                                        _tmpClasificador = ListClasificadorTemporal.Where(x => x.TipoClasificadorID == TipoClasificador.CultivoCuadros).First(x => x.Valor == p.ToString());
                                        SGF_CultivoCuadro _cuadro = new SGF_CultivoCuadro();
                                        _cuadro.CultivoCuadroID = Guid.NewGuid();
                                        _cuadro.CultivoCamaID = _cama.CultivoCamaID;
                                        _cuadro.CuadroID = _tmpClasificador.ClasificadorID;
                                        _cuadro.Orden = p;
                                        _cuadro.Nombre = _tmpClasificador.Nombre;// "Cuadro " + p.ToString();
                                        _cuadro.FechaRegistro = _fechaRegistro;
                                        _cuadro.UsuarioRegistro = Me.Usuario.NombreUsuario;
                                        _cuadro.Aream2 = _cama.Aream2 / (decimal)txt_NroCuadros.Value;
                                        _cuadro.CantidadPlantas = (int)(Math.Round((double)(_cama.CantidadPlantas / (decimal)txt_NroCuadros.Value))); ;
                                        _cuadro.Estado = (int)Enums.Estado.Activo;
                                        _cuadro.Color = "White";
                                        _cuadroCultivo.Add(_cuadro);
                                    }
                                    _cama.SGF_CultivoCuadro = _cuadroCultivo;
                                    _camaCultivo.Add(_cama);
                                }
                                _nave.SGF_CultivoCama = _camaCultivo;
                                _naveCultivo.Add(_nave);
                            }
                            _lado.SGF_CultivoNave = _naveCultivo;
                            _ladoCultivo.Add(_lado);
                        }
                        _bloque.SGF_CultivoLado = _ladoCultivo;
                        _bloqueCultivo.Add(_bloque);
                    }
                    _area.SGF_CultivoBloque = _bloqueCultivo;
                    ListAreaCultivo.Add(_area);
                }
                dlAreas.DataSource = ListAreaCultivo;
                dlAreas.DataBind();
                txt_NroPlantas.Value = (double)_sumPlantas;
                txt_AreaCultivo.Value = (double)_sumAream2;
                dlAreas.Enabled = false;
            }
            catch (Exception ex)
            {
                VerMensaje("INFORMACIÓN", "info", "info", "No se pudo generar el Campo de Cultivo");
            }
        }

        protected void btn_GenerarCancelar_Click(object sender, EventArgs e)
        {
            pnl_CargarMapaCultivo.Visible = false;
            pnl_GenerarMapaCultivo.Visible = false;
            pnl_GenerarMapaCultivoIrregular.Visible = false;
            tr_AddArea.Visible = false;
            btn_GenerarBloques.Visible = false;
            btn_GenerarCampo.Visible = false;
            chk_GenerarMapa.Checked = false;
        }
    }
}