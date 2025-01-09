using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SGF.Site.SGF_Service;
using Telerik.Web.UI;

namespace SGF.Site.Controles
{
    public partial class Control_Multiple : System.Web.UI.UserControl
    {
        #region Variables y Propiedades
        public Guid TipoClasificador
        {
            get
            {
                if (ViewState[this.ClientID + "TipoClasificador"] == null)
                    ViewState[this.ClientID + "TipoClasificador"] = Guid.Empty;
                return (Guid)ViewState[this.ClientID + "TipoClasificador"];
            }
            set
            {
                ViewState[this.ClientID + "TipoClasificador"] = value;
            }
        }
        public List<SGF_Clasificador> DataSource
        {
            get
            {
                return (List<SGF_Clasificador>)ViewState[this.ClientID + "DataSource"];
            }
        }
        public List<SGF_Formulario> DataSourceFormulario
        {
            get
            {
                return (List<SGF_Formulario>)ViewState[this.ClientID + "DataSourceFormulario"];
            }
        }
        #endregion

        #region General
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CargarControl(int _order)
        {

            LogicClient client = new LogicClient();
            ViewState[this.ClientID + "DataSource"] = client.Clasificador_ObtenerPorTipoClasificador(TipoClasificador).ToList();
            if (_order == 1)
                chk_Clasificadores.DataSource = this.DataSource.OrderBy(t => t.Nombre);
            else
                chk_Clasificadores.DataSource = this.DataSource.OrderByDescending(t => t.Nombre);
            chk_Clasificadores.DataTextField = "Nombre";
            chk_Clasificadores.DataValueField = "ClasificadorID";
            chk_Clasificadores.DataBind();

            if (_order == 1)
                rbt_Clasificador.DataSource = this.DataSource.OrderBy(t => t.Nombre);
            else
                rbt_Clasificador.DataSource = this.DataSource.OrderByDescending(t => t.Nombre);
            rbt_Clasificador.DataTextField = "Nombre";
            rbt_Clasificador.DataValueField = "ClasificadorID";
            rbt_Clasificador.DataBind();
        }

        public void CargarControlFormulario(int _order)
        {

            LogicClient client = new LogicClient();
            ViewState[this.ClientID + "DataSourceFormulario"] = client.Formulario_ObtenerPorModuloID(TipoClasificador).ToList();
            if (_order == 1)
                chk_Clasificadores.DataSource = this.DataSourceFormulario.OrderBy(t => t.Nombre);
            else
                chk_Clasificadores.DataSource = this.DataSourceFormulario.OrderByDescending(t => t.Nombre);
            chk_Clasificadores.DataTextField = "Nombre";
            chk_Clasificadores.DataValueField = "FormularioID";
            chk_Clasificadores.DataBind();

            if (_order == 1)
                rbt_Clasificador.DataSource = this.DataSourceFormulario.OrderBy(t => t.Nombre);
            else
                rbt_Clasificador.DataSource = this.DataSourceFormulario.OrderByDescending(t => t.Nombre);
            rbt_Clasificador.DataTextField = "Nombre";
            rbt_Clasificador.DataValueField = "FormularioID";
            rbt_Clasificador.DataBind();
        }
        public void LimpiarControl()
        {
            foreach (ListItem item in chk_Clasificadores.Items)
                item.Selected = false;
            foreach (ListItem item in rbt_Clasificador.Items)
                item.Selected = false;
        }
        public void HabilitaControles(Boolean IsEnable)
        {
            foreach (ListItem item in chk_Clasificadores.Items)
                item.Enabled = IsEnable;

            foreach (ListItem item in rbt_Clasificador.Items)
                item.Enabled = IsEnable;
        }
        public void VisualizarControl(int Tipo)
        {
            switch (Tipo)
            {
                case 1:
                    chk_Clasificadores.Visible = true;
                    rbt_Clasificador.Visible = false;
                    break;
                case 2:
                    chk_Clasificadores.Visible = false;
                    rbt_Clasificador.Visible = true;
                    break;
                case 3:
                    chk_Clasificadores.Visible = true;
                    rbt_Clasificador.Visible = true;
                    break;
                default:
                    chk_Clasificadores.Visible = false;
                    rbt_Clasificador.Visible = false;
                    break;
            }
        }
        #endregion

        #region Seleccionador Múltiple - CheckBox
        public List<DescriptorIndividual> DevolverMarcados()
        {
            List<DescriptorIndividual> seleccionados = new List<DescriptorIndividual>();
            foreach (ListItem item in chk_Clasificadores.Items)
                seleccionados.Add(new DescriptorIndividual()
                {
                    ClasificadorID = new Guid(item.Value),
                    EstaSeleccionado = item.Selected,
                });

            return seleccionados;
        }
        public void AsignarMarcados(List<DescriptorIndividual> seleccionados)
        {
            foreach (ListItem item in chk_Clasificadores.Items)
                if (seleccionados.Count(f => f.ClasificadorID == new Guid(item.Value) && f.EstaSeleccionado) != 0)
                    item.Selected = true;
            //chk_Clasificadores.DataBind();
        }
        public void SeleccionarTodo()
        {
            foreach (ListItem item in chk_Clasificadores.Items)
                item.Selected = true;
        }
        public int ValidarSeleccionados()
        {
            int numero = 0;
            foreach (ListItem item in chk_Clasificadores.Items)
            {
                if (item.Selected == true)
                    numero = numero + 1;
            }
            return numero;
        }

        public bool ValidarSeleccionOtros(Guid idOtros)
        {
            bool seleccion = false;
            foreach (ListItem item in chk_Clasificadores.Items)
            {
                if (item.Selected == true && new Guid(item.Value) == idOtros)
                    seleccion = true;
            }
            return seleccion;
        }

        public int ContarItems()
        {
            return chk_Clasificadores.Items.Count;
        }

        public void setAttribute(int _cellP, int _cellS, int _col)
        {
            chk_Clasificadores.CellPadding = _cellP;
            chk_Clasificadores.CellSpacing = _cellS;
            chk_Clasificadores.RepeatColumns = _col;
        }
        public void setPostBack(int _type, bool _value)
        {
            switch (_type)
            {
                case 1:
                    chk_Clasificadores.AutoPostBack = _value;
                    break;
                case 2:
                    rbt_Clasificador.AutoPostBack = _value;
                    break;
            }
        }

        protected void chk_Clasificadores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Seleccionador Único - RadioButton
        public List<DescriptorIndividual> DevolverMarcadosRbt()
        {
            List<DescriptorIndividual> seleccionados = new List<DescriptorIndividual>();
            foreach (ListItem item in rbt_Clasificador.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(new DescriptorIndividual()
                    {
                        ClasificadorID = new Guid(item.Value),
                        EstaSeleccionado = item.Selected,
                    });
                }
            }
            return seleccionados;
        }
        public void AsignarMarcadosRbt(List<DescriptorIndividual> seleccionados)
        {
            foreach (ListItem item in rbt_Clasificador.Items)
                if (seleccionados.Count(f => f.ClasificadorID == new Guid(item.Value) && f.EstaSeleccionado) != 0)
                    item.Selected = true;
        }
        public int ValidarSeleccionadosRbt()
        {
            int numero = 0;
            foreach (ListItem item in rbt_Clasificador.Items)
            {
                if (item.Selected == true)
                    numero = numero + 1;
            }
            return numero;
        }

        public string DevolverNombreMarcadoRbt()
        {
            foreach (ListItem item in rbt_Clasificador.Items)
            {
                if (item.Selected)
                {
                    return item.Text;
                }
            }
            return "";
        }

        public void setDireccion(int _type)
        {
            switch (_type)
            {
                case 1:
                    chk_Clasificadores.RepeatDirection = RepeatDirection.Horizontal;
                    rbt_Clasificador.RepeatDirection = RepeatDirection.Horizontal;
                    break;
                case 2:
                    chk_Clasificadores.RepeatDirection = RepeatDirection.Vertical;
                    rbt_Clasificador.RepeatDirection = RepeatDirection.Vertical;
                    break;
            }
        }

        #endregion

    }

    public class DescriptorIndividual
    {
        public Guid ClasificadorID { get; set; }
        public Boolean EstaSeleccionado { get; set; }
        public String Otra { get; set; }
    }
}