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

        public static List<string> listaDiaSemana = new List<string>();
        public static List<int> diaUtil = new List<int>();
        public static TimeSpan compensacao = TimeSpan.Parse("00:00");
        public static TimeSpan horasExtras = TimeSpan.Parse("00:00");
        public static TimeSpan totalHoras = TimeSpan.Parse("00:00");
        public TimeSpan limiteBancoDia = TimeSpan.Parse("02:00");
        public TimeSpan limiteJornadaDia = TimeSpan.Parse("10:00:00");
        public static TimeSpan bancoHrMes = TimeSpan.Parse("0:00");
        public static List<int> diaFalta = new List<int>();
        
        static string valorTotal = "0";
        static int diasTrabalhados = 0;
        static decimal vlHoraExtra = 0.00M;
        static decimal comissao = 0.00M;

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
            limpaForm();

            try
            {
                DataTable dtPeriodo = new DataTable();
                dtPeriodo.Columns.Add("Código", typeof(string));
                dtPeriodo.Columns.Add("Descrição", typeof(string));
                dtPeriodo.Columns.Add("Referência", typeof(string));
                dtPeriodo.Columns.Add("Proventos", typeof(string));
                dtPeriodo.Columns.Add("Desconto", typeof(string));

                string[] data = new string[2];
                data = periodo.Split('/');

                valorTotal = "0";
                diasTrabalhados = 0;
                vlHoraExtra = 0.00M;
                horasExtras = TimeSpan.Parse("00:00");                
                int diasMes = DateTime.DaysInMonth(Convert.ToInt32(data[1]), Convert.ToInt32(data[0]));
                int domingosMes = 0;              

                listaPonto = controle.PesquisaPonto(usuario.id, data[0], data[1]);                

                if (listaPonto.Count != 0)
                {                    

                    for (int i = 0; i < diasMes; i++)
                    {
                        if (DateTime.Parse((i + 1).ToString() + "/" + cmbPeriodo.SelectedValue.ToString()).DayOfWeek.Equals(DayOfWeek.Sunday))
                        {
                            domingosMes++;
                        }
                    }

                    if (listaPonto.Count + domingosMes < diasMes)
                    {
                        MessageBox.Show("O mês selecionado não possui todos os lançamentos, favor corrigir e tentar novamente.","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {
                        diaUtil = new List<int>();
                        //totalHoras = TimeSpan.Parse("00:00");

                        foreach (CtrlPonto value in listaPonto)
                        {
                            string entrada = "00:00:00";
                            string almoco = "00:00:00";
                            string retorno = "00:00:00";
                            string saida = "00:00:00";
                            string horaDia = "00:00:00";
                            string horaExtra = "00:00:00";
                            string hrExtra = "00:00:00";                                                        

                            if (value.Entrada.HasValue)
                            {
                                entrada = (value.Entrada).ToString();
                            }

                            if (value.Saida_Almoco.HasValue)
                            {
                                almoco = (value.Saida_Almoco).ToString();
                            }

                            if (value.Retorno_Almoco.HasValue)
                            {
                                retorno = (value.Retorno_Almoco).ToString();
                            }

                            if (value.Saida.HasValue)
                            {
                                saida = (value.Saida).ToString();
                            }

                            if (value.Falta)
                            {
                                listaDiaSemana.Add(value.Dt_Ponto.DayOfWeek.ToString());
                            }

                            if (TimeSpan.Parse(value.Compensacao.ToString()) > TimeSpan.Parse("00:00"))
                            {
                                compensacao = compensacao + TimeSpan.Parse(value.Compensacao.ToString());
                            }

                            if (TimeSpan.Parse(almoco) - TimeSpan.Parse(entrada) > TimeSpan.Parse("00:00"))
                            {
                                horaDia = (TimeSpan.Parse(almoco) - TimeSpan.Parse(entrada)).ToString();

                                if (TimeSpan.Parse(saida) - (TimeSpan.Parse(retorno)) > TimeSpan.Parse("00:00"))
                                {
                                    horaDia = ((TimeSpan.Parse(almoco) - TimeSpan.Parse(entrada)) + TimeSpan.Parse(saida) - (TimeSpan.Parse(retorno))).ToString();
                                }
                                diasTrabalhados++;
                            }
                            else if (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada) > TimeSpan.Parse("00:00"))
                            {
                                horaDia = (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada)).ToString();
                                diasTrabalhados++;
                            }

                            if (TimeSpan.Parse(horaDia) != TimeSpan.Parse("08:00"))
                            {
                                horaExtra = (TimeSpan.Parse(horaDia) - TimeSpan.Parse("08:00")).ToString();

                                if (TimeSpan.Parse(horaDia) - TimeSpan.Parse("08:00") > TimeSpan.Parse("00:00"))
                                {
                                    if (TimeSpan.Parse(horaExtra) > limiteBancoDia)
                                    {
                                        hrExtra = (TimeSpan.Parse(horaExtra) - limiteBancoDia).ToString();
                                        bancoHrMes = bancoHrMes + limiteBancoDia;
                                    }
                                    else
                                    {
                                        bancoHrMes = bancoHrMes + TimeSpan.Parse(horaExtra);
                                    }
                                }
                            }

                            if (value.Falta)
                            {
                                bool teste = true;
                                foreach (int valor in diaFalta)
                                {
                                    if (value.Dt_Ponto.Day == valor)
                                    {
                                        teste = false;
                                    }
                                }
                                if (teste)
                                {
                                    diaFalta.Add(value.Dt_Ponto.Day);
                                }
                            }

                            horasExtras = horasExtras + TimeSpan.Parse(hrExtra);
                            totalHoras = totalHoras + TimeSpan.Parse(horaDia);

                            diaUtil.Add(value.Dt_Ponto.Day);
                        }

                        int diasPorSemana = 6;

                        if (diaFalta.Count() == 0 && !valorTotal.Equals("") && diaUtil.Count >= diasPorSemana)
                        {
                            valorTotal = (Convert.ToDecimal(valorTotal) + diaUtil.Count / diasPorSemana * Convert.ToDecimal(usuario.salario) / diasMes).ToString("0.00");
                        }
                        else if (diaFalta.Count() == 1 && diaUtil.Count >= diasPorSemana && !valorTotal.Equals(""))
                        {
                            valorTotal = (Convert.ToDecimal(valorTotal) + (diaUtil.Count / diasPorSemana - 1) * Convert.ToDecimal(usuario.salario) / diasMes).ToString("0.00");
                        }
                        else if (diaFalta.Count() > 1 && diaUtil.Count >= (diasPorSemana * 2) && !valorTotal.Equals(""))
                        {
                            int j = 0;
                            for (int i = 0; i < diaUtil.Count; i++)
                            {
                                if (diaUtil[i] == diaFalta[j])
                                {
                                    if (diaFalta.Count > j + 1)
                                    {
                                        if (diaFalta[j + 1] - diaFalta[j] <= diasPorSemana)
                                        {
                                            if (listaDiaSemana[j].Equals("Wednesday") && listaDiaSemana[j + 1].Equals("Monday")
                                                || listaDiaSemana[j].Equals("Thursday") && listaDiaSemana[j + 1].Equals("Monday")
                                                || listaDiaSemana[j].Equals("Thursday") && listaDiaSemana[j + 1].Equals("Tuesday")
                                                || listaDiaSemana[j].Equals("Friday") && listaDiaSemana[j + 1].Equals("Monday")
                                                || listaDiaSemana[j].Equals("Friday") && listaDiaSemana[j + 1].Equals("Tuesday")
                                                || listaDiaSemana[j].Equals("Friday") && listaDiaSemana[j + 1].Equals("Wednesday")
                                                || listaDiaSemana[j].Equals("Saturday") && listaDiaSemana[j + 1].Equals("Monday")
                                                || listaDiaSemana[j].Equals("Saturday") && listaDiaSemana[j + 1].Equals("Tuesday")
                                                || listaDiaSemana[j].Equals("Saturday") && listaDiaSemana[j + 1].Equals("Wednesday")
                                                || listaDiaSemana[j].Equals("Saturday") && listaDiaSemana[j + 1].Equals("Thursday"))
                                            {
                                                valorTotal = (Convert.ToDecimal(valorTotal) + (diaUtil.Count / diasPorSemana - 1) * Convert.ToDecimal(usuario.salario / diasMes)).ToString("0.00");
                                            }
                                        }
                                        else
                                        {
                                            valorTotal = (Convert.ToDecimal(valorTotal) + (diaUtil.Count / diasPorSemana - 1) * Convert.ToDecimal(usuario.salario / diasMes)).ToString("0.00");
                                        }
                                        j++;
                                    }
                                }
                            }
                        }

                        double vlHora = Convert.ToDouble(usuario.salario / diasMes) / 8;
                        vlHoraExtra = Convert.ToDecimal((Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(usuario.salario) / diasMes) / 8) + ((Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(usuario.salario) / diasMes) / 8)) * 0.50M)).ToString("0.00"));
                        valorTotal = (Convert.ToDouble(valorTotal) + (totalHoras.TotalHours - (horasExtras + bancoHrMes).TotalHours) * Math.Round(vlHora, 2) + (Convert.ToDouble(vlHoraExtra) * horasExtras.TotalHours)).ToString("0.00");
                    }
                }

                //PROVENTOS
                dtPeriodo.Rows.Add("01", "Salário Mensal", (diasTrabalhados + domingosMes).ToString(), Convert.ToDecimal((usuario.salario/diasMes) * (diasTrabalhados + domingosMes)).ToString("0.00"), "");

                if (horasExtras != TimeSpan.Parse("00:01:00"))
                {
                    dtPeriodo.Rows.Add("02", "Horas Extras (50%)", horasExtras.TotalHours, (Convert.ToDouble(vlHoraExtra) * horasExtras.TotalHours).ToString("0.00"), "");
                }

                if(controle.PesquisaGerenciamento(1).comissao > 0)
                {
                    comissao = 0.00M;
                    comissao = buscaComissão(Convert.ToDateTime("01" + "/" + cmbPeriodo.SelectedValue.ToString()), Convert.ToDateTime(diasMes.ToString() + "/" + cmbPeriodo.SelectedValue.ToString()) );
                    dtPeriodo.Rows.Add("03", "Remuneração Variável", controle.PesquisaGerenciamento(1).comissao.ToString(), comissao.ToString(), "");
                }                               

                //DESCONTOS
                if(Convert.ToDecimal(valorTotal) < 1693.72M)
                {
                    dtPeriodo.Rows.Add("05", "INSS", "8", "", (Convert.ToDecimal(valorTotal) * 0.08M).ToString("0.00"));
                }
                else if (Convert.ToDecimal(valorTotal) >= 1693.73M && Convert.ToDecimal(valorTotal) < 2822.90M)
                {
                    dtPeriodo.Rows.Add("05", "INSS", "9", "", (Convert.ToDecimal(valorTotal) * 0.09M).ToString("0.00"));
                }
                else if (Convert.ToDecimal(valorTotal) >= 2822.90M && Convert.ToDecimal(valorTotal) <= 5645.80M)
                {
                    dtPeriodo.Rows.Add("05", "INSS", "11", "", (Convert.ToDecimal(valorTotal) * 0.11M).ToString("0.00"));
                }
                else
                {
                    dtPeriodo.Rows.Add("05", "INSS", "TETO", "", "621,04");
                }



                dgvRelatorio.DataSource = dtPeriodo;

                dgvRelatorio.Columns[0].Width = 100;
                dgvRelatorio.Columns[1].Width = 350;
                dgvRelatorio.Columns[2].Width = 140;
                dgvRelatorio.Columns[3].Width = 100;
                dgvRelatorio.Columns[4].Width = 100;
            }
            catch
            {

            }
        }

        private decimal buscaComissão(DateTime inicio, DateTime final)
        {
            decimal valorComissao = 0.00M;

            List<Vendas> vendasPeriodo = new List<Vendas>();
            vendasPeriodo = controle.pesquisaVendasPeriodo(inicio, final);

            foreach (Vendas value in vendasPeriodo)
            {
                if(usuario.id == value.id_Usuario)
                {
                    valorComissao = valorComissao + Convert.ToDecimal(value.comissao);
                }                
            }

            return valorComissao;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {            
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnImprime.Enabled = true;
                carregaFolha(cmbPeriodo.SelectedValue.ToString());
            }
            catch
            {

            }
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
            cmbPeriodo.Enabled = false;
            cmbPeriodo.DataSource = null;
            cmbLogin.SelectedValue = "";
            btnImprime.Enabled = false;
            limpaForm();
            FolhaPg form = new FolhaPg(user);
            form.Show();
            Dispose();
        }

        private void limpaForm()
        {
            diaUtil = new List<int>();
            diaFalta = new List<int>();

            listaDiaSemana = new List<string>();
            compensacao = TimeSpan.Parse("00:00");
            horasExtras = TimeSpan.Parse("00:00");
            totalHoras = TimeSpan.Parse("00:00");
            bancoHrMes = TimeSpan.Parse("0:00");
            valorTotal = "0";
            diasTrabalhados = 0;
            vlHoraExtra = 0.00M;
            comissao = 0.00M;
        }
    }
}
