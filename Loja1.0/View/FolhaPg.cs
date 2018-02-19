using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loja1._0.Control;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class FolhaPg : Form
    {
        private Model.Usuarios user;
        private Controle controle = new Controle();
        static List<CtrlPonto> listaPonto = new List<CtrlPonto>();
        static Model.Usuarios usuario = new Model.Usuarios();
        static List<Model.Usuarios> listaUsuarios = new List<Model.Usuarios>();
        public static List<string> listaData = new List<string>();

        public FolhaPg(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
            carregaLogin();
        }

        private void carregaLogin()
        {
            listaUsuarios = controle.PesquisaGeralUser();
            cmbLogin.DataSource = listaUsuarios;
            cmbLogin.ValueMember = "Nome";
            cmbLogin.SelectedValue = "";
        }

        private void carregaFolha(string periodo)
        {
            try
            {
                string[] data = new string[2];
                data = periodo.Split('/');

                listaPonto = controle.PesquisaPonto(usuario.id, data[0], data[1]);

                DataTable dtPeriodo = new DataTable();
                dtPeriodo.Columns.Add("Código", typeof(string));
                dtPeriodo.Columns.Add("Descrição", typeof(string));
                dtPeriodo.Columns.Add("Referência", typeof(string));
                dtPeriodo.Columns.Add("Proventos", typeof(string));
                dtPeriodo.Columns.Add("Desconto", typeof(string));
            }
            catch
            {

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {            
            btnImprime.Enabled = true;
            carregaFolha(cmbPeriodo.SelectedText);
        }

        private void carregaPeriodo()
        {
            TimeSpan aux = (DateTime.Now - Convert.ToDateTime(usuario.dt_Inclusao));

            int diferencaMes = Convert.ToInt32(Convert.ToInt32(aux.Days) / 30);
            diferencaMes++;

            int diferencaAno = diferencaMes / 12;
            diferencaAno++;

            int j = 0;
            listaData = new List<string>();
            listaData.Add("");

            for (int i = 0; i <= diferencaMes; i++)
            {
                if (Convert.ToDateTime(usuario.dt_Inclusao).AddMonths(i).Month <= DateTime.Today.Month || Convert.ToDateTime(usuario.dt_Inclusao).AddYears(j).Year < DateTime.Today.Year)
                {
                    listaData.Add(Convert.ToDateTime(usuario.dt_Inclusao).AddMonths(i).Month.ToString() + "/" + Convert.ToDateTime(usuario.dt_Inclusao).AddYears(j).Year.ToString());
                    if ((Convert.ToDateTime(usuario.dt_Inclusao).AddMonths(i).Month) % 12 == 0 && i > 0)
                    {
                        j++;
                    }
                }
            }
            cmbPeriodo.DataSource = listaData;
            cmbPeriodo.ValueMember = "";
        }

        private void cmbLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                usuario = controle.PesquisaUserNome(cmbLogin.SelectedValue.ToString());
                if (usuario != null)
                {
                    cmbPeriodo.Enabled = true;
                    carregaPeriodo();
                }
            }
            catch
            {

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            btnImprime.Enabled = false;
            cmbPeriodo.Enabled = false;
            cmbPeriodo.DataSource = null;
        }
    }
}
