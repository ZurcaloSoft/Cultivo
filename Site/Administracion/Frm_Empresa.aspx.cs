using SGF.Site.SGF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SGF.Site.Administracion
{
    public partial class Frm_Empresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Me.ValidarSesion();
            if (!IsPostBack)
            {
                SGF_Site master = (SGF_Site)Page.Master;
                master.VisibilityMenuItem = (int)Enums.ModuloIndex.Administracion;
                Cargar_Grupo(cmb_TipoGrupoBuscar);
                //Cargar_Grupo(cmb_TipoGrupo);
            }
        }
        protected string ObtenerNombreEstado(Int32 Estado)
        {
            return Utils.ObtenerEstado(Estado);
        }
        private void Cargar_Grupo(RadComboBox rcbGrupo)
        {
            //LogicClient client = new LogicClient();
            //List<SGF_Grupo> entidad = client.Grupo_ObtenerTodo();
            //rcbGrupo.DataSource = entidad.OrderBy(t => t.Nombre);
            //rcbGrupo.DataValueField = "GrupoID";
            //rcbGrupo.DataTextField = "Nombre";
            //rcbGrupo.DataBind();
            //rcbGrupo.SelectedValue = Guid.Empty.ToString();
        }
        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            LogicClient client = new LogicClient();
            if (txt_RucEmpresa.Text != string.Empty && new Guid(cmb_TipoGrupoBuscar.SelectedValue) != Guid.Empty)
            {
                llenarGrid();
            }
            else
            {
                if (txt_RucEmpresa.Text != string.Empty)
                {
                    gv_Empresa.DataSource = client.Empresa_ObtenerPorRUC(txt_RucEmpresa.Text);
                }
                else
                {
                    //  gv_Empresa.DataSource = client.Empresa_ObtenerGrupoID(new Guid(cmb_TipoGrupoBuscar.SelectedValue));
                }
            }
            gv_Empresa.DataBind();
        }
        protected void btn_limpiar_Click(object sender, EventArgs e)
        {
            txt_RucEmpresa.Text = "";
        }
        protected void btn_Nuevo_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarControles();
            hdn_EmpresaID.Value = "0";
            pnl_Buscador.Visible = false;
            pnl_Datos.Visible = true;
        }
        protected void LimpiarControles()
        {
            hdn_EmpresaID.Value = null;
            gv_Empresa.DataSource = null;
            gv_Empresa.DataBind();
            txt_Ruc.Text = "";
            cmb_EmpresaTipo.Text = "0";
            txt_EmpresaNombreRazon.Text = "";
            txt_EmpresaNombreComercial.Text = "";
            txt_RepresentanteNombre.Text = "";
            txt_RepresentanteCed.Text = "";
            txt_ContadorRuc.Text = "";
            txt_ContadorNombre.Text = "";
            txt_ContadorRegistro.Text = "";
            txt_Telefono.Text = "";
            txt_Celular.Text = "";
            txt_Email.Text = "";
            txt_Ciudad.Text = "";
            txt_Direccion.Text = "";
            chk_Grupo.Checked = false;
            cmb_EmpresaGrupo.Text = "0";
            chk_ContribEspecial.Checked = false;
            txt_ContribEspecial.Text = "";
            txt_Estado.Text = "";
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
                case "Eliminar":
                    Eliminar();
                    break;
            }
        }
        private void Cancelar()
        {
            LimpiarControles();
            pnl_Buscador.Visible = true;
            pnl_Datos.Visible = false;
        }

        private void Eliminar()
        {
            LogicClient client = new LogicClient();
            client.Empresa_Eliminar(hdn_EmpresaID.Value, "", Utils.getIP(), Utils.getHostName(Utils.getIP()), Me.Usuario.NombreUsuario);
            Utils.MessageBox(this, "", "Registro eliminado.", "info");
            Cancelar();
            llenarGrid();
            gv_Empresa.DataBind();
        }
        private void Grabar()
        {
            string Message = "";

            if (txt_Ruc.Text == "")
            {
                Message += "Debe ingresar el RUC de la Empresa." + Environment.NewLine;
            }
            if (cmb_EmpresaTipo.SelectedValue == "0")
            {
                Message += "Debe seleccionar el Tipo de Persona" + Environment.NewLine;
            }
            if (txt_EmpresaNombreRazon.Text == "")
            {
                Message += "Debe ingresar la Razón Social de la Empresa." + Environment.NewLine;
            }

            if (Message != string.Empty)
            {
                Utils.MessageBox(this, "", "" + Message, "info");
                return;
            }

            try
            {
                CO_EMPRESA newEmpresa = new CO_EMPRESA();
                newEmpresa.CO_EMP_RUC = hdn_EmpresaID.Value == "0" ? txt_Ruc.Text : hdn_EmpresaID.Value;
                newEmpresa.CO_EMP_RUC = txt_Ruc.Text;
                newEmpresa.CO_EMP_TIP_IDE = cmb_EmpresaTipo.SelectedValue;
                newEmpresa.CO_EMP_RAZ_SOC = txt_EmpresaNombreRazon.Text;
                newEmpresa.CO_EMP_NOM = txt_EmpresaNombreComercial.Text;
                newEmpresa.CO_EMP_NOM_REP = txt_RepresentanteNombre.Text;
                newEmpresa.CO_EMP_REP_IDE = txt_RepresentanteCed.Text;
                newEmpresa.CO_EMP_RUC_CON = txt_ContadorRuc.Text;
                newEmpresa.CO_EMP_NOM_CON = txt_ContadorNombre.Text;
                newEmpresa.CO_EMP_REG_CON = txt_ContadorRegistro.Text;
                newEmpresa.CO_EMP_TEL = txt_Telefono.Text;
                newEmpresa.CO_EMP_FAX = txt_Celular.Text;
                newEmpresa.CO_EMP_MAI = txt_Email.Text;
                newEmpresa.CO_EMP_CIU = txt_Ciudad.Text;
                newEmpresa.CO_EMP_DIR = txt_Direccion.Text;
                newEmpresa.ES_GRUPO_EMPRESA = chk_Grupo.Checked;
                if (chk_Grupo.Checked)
                    newEmpresa.EMPRESA_PADRE = cmb_EmpresaGrupo.SelectedValue;
                newEmpresa.CO_CONTRIBUYENTE_ESPECIAL = chk_ContribEspecial.Checked;
                newEmpresa.CO_CONTRIBUYENTE_NUMERO = txt_ContribEspecial.Text;
                newEmpresa.CO_ESTADO = 1;

                LogicClient client = new LogicClient();
                client.Empresa_Grabar(newEmpresa, Me.Usuario.NombreUsuario, Utils.getIP(), Utils.getHostName(Utils.getIP()));
                Utils.MessageBox(this, "", "Datos Registrado correctamente.", "info");
                Cancelar();
                llenarGrid();
                gv_Empresa.DataBind();
            }
            catch (Exception ex)
            {
                Utils.MessageBox(this, "", "Hubo un problema al momento de grabar el registro.", "error");
            }
        }
        protected void llenarGrid()
        {
            LogicClient client = new LogicClient();
            var _parametro = client.Empresa_ObtenerTodo();
            gv_Empresa.DataSource = _parametro.Count() > 0 ? _parametro.OrderBy(x => x.CO_EMP_RAZ_SOC).ToList() : _parametro;
        }
        protected void gv_Empresa_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editar":
                    hdn_EmpresaID.Value = e.CommandArgument.ToString();
                    pnl_Buscador.Visible = false;
                    pnl_Datos.Visible = true;
                    CargarDatos();
                    break;
            }
        }
        protected void CargarDatos()
        {
            if (hdn_EmpresaID.Value == "") return;
            LogicClient client = new LogicClient();
            CO_EMPRESA _empresa = client.Empresa_ObtenerPorRUC(hdn_EmpresaID.Value);
            if (_empresa != null)
            {
                txt_Ruc.Text = _empresa.CO_EMP_RUC;
                cmb_EmpresaTipo.SelectedValue = _empresa.CO_EMP_TIP_IDE;
                txt_EmpresaNombreRazon.Text = _empresa.CO_EMP_RAZ_SOC;
                txt_EmpresaNombreComercial.Text = _empresa.CO_EMP_NOM;
                txt_RepresentanteNombre.Text = _empresa.CO_EMP_NOM_REP;
                txt_RepresentanteCed.Text = _empresa.CO_EMP_REP_IDE;
                txt_ContadorRuc.Text = _empresa.CO_EMP_RUC_CON;
                txt_ContadorNombre.Text = _empresa.CO_EMP_NOM_CON;
                txt_ContadorRegistro.Text = _empresa.CO_EMP_REG_CON;
                txt_Telefono.Text = _empresa.CO_EMP_TEL;
                txt_Celular.Text = _empresa.CO_EMP_FAX;
                txt_Email.Text = _empresa.CO_EMP_MAI;
                txt_Ciudad.Text = _empresa.CO_EMP_CIU;
                txt_Direccion.Text = _empresa.CO_EMP_DIR;
                chk_Grupo.Checked = _empresa.ES_GRUPO_EMPRESA == null ? false : (bool)_empresa.ES_GRUPO_EMPRESA;
                if (chk_Grupo.Checked)
                    cmb_EmpresaGrupo.SelectedValue = _empresa.EMPRESA_PADRE;
                chk_ContribEspecial.Checked = _empresa.CO_CONTRIBUYENTE_ESPECIAL == null ? false: (bool)_empresa.CO_CONTRIBUYENTE_ESPECIAL;
                txt_ContribEspecial.Text = _empresa.CO_CONTRIBUYENTE_NUMERO;
                txt_Estado.Text = ObtenerNombreEstado((int)_empresa.CO_ESTADO);
            }
        }
        protected void gv_Empresa_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            llenarGrid();
        }

        protected void chk_Grupo_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Grupo.Checked)
            {
                lbl_EmpresaPadre.Visible = true;
                cmb_EmpresaGrupo.Visible = true;
            }
            else
            {
                lbl_EmpresaPadre.Visible = false;
                cmb_EmpresaGrupo.Visible = false;
                cmb_EmpresaGrupo.SelectedValue = "0";
            }
        }

        protected void chk_ContribEspecial_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ContribEspecial.Checked)
            {
                lbl_ContribEspecial.Visible = true;
                txt_ContribEspecial.Visible = true;
            }
            else
            {
                lbl_ContribEspecial.Visible = false;
                txt_ContribEspecial.Visible = false;
                txt_ContribEspecial.Text = "";
            }
        }
    }
}