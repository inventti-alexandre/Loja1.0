using Loja1._0.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Loja1._0.Control;

namespace Loja1._0
{
    public partial class ControleHoras : Form
    {
        Controle controle = new Controle();
        private Model.Usuarios user;
        public TimeSpan qntHoras;
        public static List<string> listaDiaSemana = new List<string>();
        public static int anoSelect;
        public static int diaCompensar;
        public static Model.Usuarios usuario;
        public static List<int> diaFalta = new List<int>();
        public static List<Model.Usuarios> listaUser = new List<Model.Usuarios>();
        public static List<CtrlPonto> listaPonto = new List<CtrlPonto>();
        public static CtrlPonto PontoDia = new CtrlPonto();
        public static List<string> listaData = new List<string>();

        public static TimeSpan compensacao = TimeSpan.Parse("00:00");
        public static TimeSpan horasExtras = TimeSpan.Parse("00:00");
        public static TimeSpan totalHoras = TimeSpan.Parse("00:00");

        public TimeSpan limiteBancoDia = TimeSpan.Parse("02:00");
        public TimeSpan limiteJornadaDia = TimeSpan.Parse("10:00:00");

        public ControleHoras(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
            carregaUser(user);
            btnExcluir.Enabled = false;
        }

        private void carregaPontoPeriodo(string periodo)
        {
            try
            {
                txtBancoHoras.Text = "0:00";
                horasExtras = TimeSpan.Parse("0:00");
                totalHoras = TimeSpan.Parse("0:00");
                TimeSpan bancoHrMes = TimeSpan.Parse("0:00");

                List<int> diaUtil = new List<int>();

                string[] data = new string[2];
                data = periodo.Split('/');

                usuario = controle.PesquisaUserNome(cmbFuncionarios.SelectedValue.ToString());
                listaPonto = controle.PesquisaPonto(usuario.id, data[0], data[1]);

                DataTable dtPeriodo = new DataTable();
                dtPeriodo.Columns.Add("Dia", typeof(string));
                dtPeriodo.Columns.Add("Mês", typeof(string));
                dtPeriodo.Columns.Add("Dia Semana", typeof(string));
                dtPeriodo.Columns.Add("Entrada", typeof(string));
                dtPeriodo.Columns.Add("Almoço", typeof(string));
                dtPeriodo.Columns.Add("Retorno", typeof(string));
                dtPeriodo.Columns.Add("Saída", typeof(string));
                dtPeriodo.Columns.Add("Tempo Efetivo", typeof(string));
                dtPeriodo.Columns.Add("Horas Excedentes", typeof(string));
                dtPeriodo.Columns.Add("Observação", typeof(string));

                txtTotal.Text = "0,00";
                txtBancoHoras.Text = "0:00";

                foreach (CtrlPonto value in listaPonto)
                {
                    string diaSemana = "";

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
                    }
                    else if (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada) > TimeSpan.Parse("00:00"))
                    {
                        horaDia = (TimeSpan.Parse(saida) - TimeSpan.Parse(entrada)).ToString();
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

                    if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Sunday)
                    {
                        diaSemana = "Domingo";
                    }
                    else if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Monday)
                    {
                        diaSemana = "Segunda";
                    }
                    else if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Tuesday)
                    {
                        diaSemana = "Terça";
                    }
                    else if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Wednesday)
                    {
                        diaSemana = "Quarta";
                    }
                    else if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Thursday)
                    {
                        diaSemana = "Quinta";
                    }
                    else if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Friday)
                    {
                        diaSemana = "Sexta";
                    }
                    else if (value.Dt_Ponto.DayOfWeek == DayOfWeek.Saturday)
                    {
                        diaSemana = "Sábado";
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

                    dtPeriodo.Rows.Add(value.Dt_Ponto.Day.ToString(), value.Dt_Ponto.Month.ToString(), diaSemana, entrada, almoco, retorno, saida, horaDia, horaExtra, value.Observacao);                    

                    horasExtras = horasExtras + TimeSpan.Parse(hrExtra);
                    totalHoras = totalHoras + TimeSpan.Parse(horaDia);

                    diaUtil.Add(value.Dt_Ponto.Day);
                }

                txtBancoHoras.Text = usuario.bancoHoras.ToString();

                txtHorasExtras.Text = horasExtras.ToString();
                txtTotalHoras.Text = totalHoras.ToString();                
                txtTotal.Text = ((Convert.ToDouble((totalHoras.TotalHours) - (horasExtras + bancoHrMes).TotalHours)) * Convert.ToDouble(txtVlHora.Text) + (Convert.ToDouble(txtVlHorasExtras.Text) * Convert.ToDouble(horasExtras.TotalHours))).ToString("0.00");

                int diasPorSemana = 6;

                if (diaFalta.Count() == 0 && !txtTotal.Text.Equals("") && diaUtil.Count >= diasPorSemana)
                {
                    txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) + diaUtil.Count / diasPorSemana * Convert.ToDecimal(txtVlDia.Text)).ToString("0.00");
                }
                else if (diaFalta.Count() == 1 && diaUtil.Count >= diasPorSemana && !txtTotal.Text.Equals(""))
                {
                    txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) + (diaUtil.Count / diasPorSemana - 1) * Convert.ToDecimal(txtVlDia.Text)).ToString("0.00");
                }
                else if (diaFalta.Count() > 1 && diaUtil.Count >= (diasPorSemana * 2) && !txtTotal.Text.Equals(""))
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
                                        txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) + (diaUtil.Count / diasPorSemana - 1) * Convert.ToDecimal(txtVlDia.Text)).ToString("0.00");
                                    }
                                }
                                else
                                {
                                    txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) + (diaUtil.Count / diasPorSemana - 1) * Convert.ToDecimal(txtVlDia.Text)).ToString("0.00");
                                }
                                j++;
                            }
                        }
                    }
                }

                dgvPonto.DataSource = dtPeriodo;

                dgvPonto.Columns[0].Width = 40;
                dgvPonto.Columns[1].Width = 40;
                dgvPonto.Columns[2].Width = 140;
                dgvPonto.Columns[3].Width = 100;
                dgvPonto.Columns[4].Width = 100;
                dgvPonto.Columns[5].Width = 100;
                dgvPonto.Columns[6].Width = 100;
                dgvPonto.Columns[7].Width = 100;
                dgvPonto.Columns[8].Width = 100;
                dgvPonto.Columns[9].Width = 900;                        
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void carregaUser(Model.Usuarios usuario)
        {
            try
            {
                if (usuario.id_Perfil == 1)
                {
                    btnExcluir.Visible = true;
                    carregaComboBox();
                    cmbFuncionarios.SelectedValue = "";

                }
                else if (usuario.id_Perfil == 2)
                {
                    btnExcluir.Visible = true;
                    carregaComboBox(2);
                    cmbFuncionarios.SelectedValue = "";

                }
                else if (usuario.id_Perfil >= 3)
                {
                    rdbFalta.Enabled = false;
                    rdbCompensacao.Enabled = false;
                    carregaComboBox(user);
                    cmbPeriodo.Enabled = true;
                }
            }
            catch
            {

            }
        }

        private void carregaComboBox(Model.Usuarios user)
        {
            listaUser = new List<Model.Usuarios>();
            listaUser.Add(user);
            preencheComboBox(listaUser);
        }

        private void carregaComboBox(int id_Perfil)
        {
            listaUser = new List<Model.Usuarios>();
            listaUser.Add(null);
            listaUser = controle.PesquisaUserPerfilId(id_Perfil);
            preencheComboBox(listaUser);
        }

        private void carregaComboBox()
        {
            listaUser = new List<Model.Usuarios>();
            listaUser.Add(null);
            listaUser = controle.PesquisaGeralUser();
            preencheComboBox(listaUser);
        }

        private void preencheComboBox(List<Model.Usuarios> listaUser)
        {
            cmbFuncionarios.DataSource = listaUser;
            cmbFuncionarios.ValueMember = "Nome";
        }

        private void cmbFuncionarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (user.id_Perfil >= 3)
            {
                btnLimpar_Click(sender, e);
                usuario = user;
                cmbPeriodo.Enabled = true;
                carregaPeriodo(user.dt_Inclusao);

            }
            else
            {
                usuario = controle.PesquisaUserNome(cmbFuncionarios.SelectedValue.ToString());
                if (usuario != null)
                {
                    cmbPeriodo.Enabled = true;
                    carregaPeriodo(usuario.dt_Inclusao);
                    btnLimpar_Click(sender, e);
                }
            }
        }

        private void carregaPeriodo(DateTime? dt_Inclusao)
        {
            TimeSpan aux = (DateTime.Now - Convert.ToDateTime(dt_Inclusao));

            int diferencaMes = Convert.ToInt32(Convert.ToInt32(aux.Days) / 30);
            diferencaMes++;

            int diferencaAno = diferencaMes / 12;
            diferencaAno++;

            int j = 0;
            listaData = new List<string>();
            listaData.Add("");

            for (int i = 0; i <= diferencaMes; i++)
            {
                if (Convert.ToDateTime(dt_Inclusao).AddMonths(i).Month <= DateTime.Today.Month || Convert.ToDateTime(dt_Inclusao).AddYears(j).Year < DateTime.Today.Year)
                {               
                    listaData.Add(Convert.ToDateTime(dt_Inclusao).AddMonths(i).Month.ToString() + "/" + Convert.ToDateTime(dt_Inclusao).AddYears(j).Year.ToString());
                    if ((Convert.ToDateTime(dt_Inclusao).AddMonths(i).Month) % 12 == 0 && i > 0)
                    {
                        j++;
                    }
                }
            }
            cmbPeriodo.DataSource = listaData;
            cmbPeriodo.ValueMember = "";

            carregaComboAno(diferencaAno, dt_Inclusao);
        }

        private void carregaComboAno(int diferencaAno, DateTime? dt_Inclusao)
        {
            listaData = new List<string>();
            listaData.Add("");

            for (int i = 0; i < diferencaAno; i++)
            {
                listaData.Add(Convert.ToDateTime(dt_Inclusao).AddYears(i).Year.ToString());
            }
            cmbAno.DataSource = listaData;
            cmbAno.ValueMember = "";
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            if (!cmbPeriodo.SelectedItem.Equals(""))
            {
                string[] data = new string[2];
                data = cmbPeriodo.SelectedValue.ToString().Split('/');
                int diasMes = Convert.ToInt32(DateTime.DaysInMonth(Convert.ToInt32(data[1]), Convert.ToInt32(data[0])));

                txtSalario.Text = usuario.salario.ToString();
                txtVlDia.Text = (Convert.ToDecimal(txtSalario.Text) / diasMes).ToString("0.00");
                txtVlHora.Text = (Convert.ToDecimal(txtVlDia.Text) / 8).ToString("0.00");
                txtVlHorasExtras.Text = (Convert.ToDecimal(txtVlHora.Text) + ((Convert.ToDecimal(txtVlHora.Text)) * 0.50M)).ToString("0.00");

                if (user.id_Perfil < 3)
                {
                    cmbAno.Enabled = true;                    
                }
                else
                {
                    PanelTipo.Enabled = true;
                    btnIncluir.Enabled = true;                    
                }

                carregaPontoPeriodo(cmbPeriodo.SelectedValue.ToString());

                btnLimpar.Enabled = true;
            }
        }

        private void cmbLancaNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAno.Enabled = true;
        }

        private void carregaDiasMes(int mes, string ano)
        {
            int dias = Convert.ToInt32(DateTime.DaysInMonth(Convert.ToInt32(ano), mes));
            List<int> listaDias = new List<int>();
            for (int i = 1; i <= dias; i++)
            {
                listaDias.Add(i);
            }
            cmbDia.DataSource = listaDias;
            cmbDia.ValueMember = "";
        }

        private void cmbAno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAno.SelectedIndex > 0)
            {
                cmbMes.Enabled = true;
            }
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMes.SelectedIndex > 0)
            {
                PanelTipo.Enabled = true;
                cmbDia.Enabled = true;
                txtHora.Enabled = true;
                btnIncluir.Enabled = true;
                btnExcluir.Enabled = true;
                carregaDiasMes(cmbMes.SelectedIndex, cmbAno.SelectedValue.ToString());
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            btnIncluir.Enabled = false;
            btnExcluir.Enabled = false;
            cmbAno.SelectedItem = "";
            cmbMes.SelectedItem = "";
            cmbMes.DataSource = null;
            cmbMes.Enabled = false;
            cmbDia.DataSource = null;
            cmbDia.Enabled = false;
            PanelTipo.Enabled = false;
            rdbEntrada.Checked = true;
            txtHora.Enabled = false;
            txtHora.Text = "";
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            string alteracao = "";
            bool proibido = false;

            if (user.id_Perfil < 3)
            {
                DateTime dataPonto = Convert.ToDateTime(cmbDia.SelectedItem.ToString() + "/" + cmbMes.SelectedIndex.ToString() + "/" + cmbAno.SelectedItem.ToString());

                if (dataPonto.DayOfWeek != 0)
                {
                    CtrlPonto ponto = new CtrlPonto();

                    if (txtHora.Text.Equals(""))
                    {
                        MessageBox.Show("Formato de horas incorreto, favor utilizar \"HH:MM\"", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        if (controle.PesquisaPontoDia(usuario.id, cmbMes.SelectedIndex.ToString(), cmbAno.SelectedItem.ToString(), cmbDia.SelectedItem.ToString()) == null)
                        {
                            controle.SalvaPonto(ponto);
                            ponto.Dt_Ponto = dataPonto;
                            ponto.Id_User = usuario.id;
                            ponto.Entrada = TimeSpan.Parse("00:00");
                            ponto.Saida_Almoco = TimeSpan.Parse("00:00");
                            ponto.Retorno_Almoco = TimeSpan.Parse("00:00");
                            ponto.Saida = TimeSpan.Parse("00:00");
                            ponto.Compensacao = TimeSpan.Parse("00:00");
                        }
                        else
                        {
                            ponto = controle.PesquisaPontoDia(usuario.id, cmbMes.SelectedIndex.ToString(), cmbAno.SelectedItem.ToString(), cmbDia.SelectedItem.ToString());
                            proibido = true;
                        }

                        if (rdbEntrada.Checked)
                        {
                            TimeSpan result = new TimeSpan();
                            if (TimeSpan.TryParse(txtHora.Text, out result))
                            {
                                ponto.Falta = false;
                                ponto.Entrada = TimeSpan.Parse(txtHora.Text);
                                alteracao = "Entrada : " + ponto.Entrada.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                GerarLog(ponto, alteracao);

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                        }
                        else if (rdbAlmoco.Checked)
                        {
                            TimeSpan result = new TimeSpan();
                            if (TimeSpan.TryParse(txtHora.Text, out result))
                            {
                                ponto.Falta = false;
                                ponto.Saida_Almoco = TimeSpan.Parse(txtHora.Text);
                                alteracao = "Saída Alm. : " + ponto.Saida_Almoco.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                GerarLog(ponto, alteracao);

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                        }
                        else if (rdbRetorno.Checked)
                        {
                            TimeSpan result = new TimeSpan();
                            if (TimeSpan.TryParse(txtHora.Text, out result))
                            {
                                ponto.Falta = false;
                                ponto.Retorno_Almoco = TimeSpan.Parse(txtHora.Text);
                                alteracao = "Retorno Alm. : " + ponto.Retorno_Almoco.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                GerarLog(ponto, alteracao);

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                        }
                        else if (rdbSaida.Checked)
                        {
                            TimeSpan result = new TimeSpan();
                            if (TimeSpan.TryParse(txtHora.Text, out result))
                            {
                                ponto.Falta = false;
                                ponto.Saida = TimeSpan.Parse(txtHora.Text);
                                alteracao = "Saída : " + ponto.Saida.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                GerarLog(ponto, alteracao);

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                        }

                        else if (rdbCompensacao.Checked && !proibido)
                        {
                            TimeSpan result = new TimeSpan();
                            if (TimeSpan.TryParse(txtHora.Text, out result))
                            {
                                if (TimeSpan.Parse(txtHora.Text) <= TimeSpan.Parse(txtBancoHoras.Text))
                                {
                                    ponto.Compensacao = ponto.Compensacao + TimeSpan.Parse(txtHora.Text);
                                    ponto.Observacao = "Falta Compensada";
                                    alteracao = ponto.Observacao;

                                    controle.SalvaAtualiza();

                                    corrigeBancoHoras(TimeSpan.Parse(txtHora.Text));

                                    CompensaDia(TimeSpan.Parse(txtHora.Text));

                                    controle.SalvaAtualiza();

                                    GerarLog(ponto, alteracao);

                                    btnLimpar_Click(sender, e);
                                    btnExibir_Click(sender, e);
                                }
                                else
                                {
                                    MessageBox.Show("As horas a serem compensadas não podem exceder a quantidade do banco de horas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("É necessário o preenchimento correto da quantidade de horas a serem compensadas,  favor utilizar \"HH:MM\"", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (rdbFalta.Checked && !proibido)
                        {
                            if (ponto.Falta == false)
                            {
                                ponto.Falta = true;
                                alteracao = "Falta";
                                ponto.Observacao = alteracao;

                                controle.SalvaAtualiza();

                                GerarLog(ponto, alteracao);

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                        }
                        else if (proibido)
                        {
                            MessageBox.Show("Não é possível lançar falta/compensação em datas com lançamento anterior", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Não é possível bater ponto nos domingos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CtrlPonto ponto = new CtrlPonto();

                if (controle.PesquisaPontoDia(usuario.id, DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString(), DateTime.Today.Day.ToString()) == null)
                {
                    controle.SalvaPonto(ponto);
                    ponto.Dt_Ponto = DateTime.Today.Date;
                    ponto.Id_User = usuario.id;
                    ponto.Entrada = TimeSpan.Parse("00:00");
                    ponto.Saida_Almoco = TimeSpan.Parse("00:00");
                    ponto.Retorno_Almoco = TimeSpan.Parse("00:00");
                    ponto.Saida = TimeSpan.Parse("00:00");
                    ponto.Compensacao = TimeSpan.Parse("00:00");
                }
                else
                {
                    ponto = controle.PesquisaPontoDia(usuario.id, DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString(), DateTime.Today.Day.ToString());
                }

                if (rdbEntrada.Checked)
                {
                    if (ponto.Entrada == TimeSpan.Parse("00:00"))
                    {
                        if (ponto.Saida_Almoco == TimeSpan.Parse("00:00") && ponto.Retorno_Almoco == TimeSpan.Parse("00:00") && ponto.Saida == TimeSpan.Parse("00:00"))
                        {
                            ponto.Entrada = DateTime.Now.TimeOfDay;
                            alteracao = "Entrada : " + ponto.Entrada.ToString();
                            controlaBancoHoras(ponto);

                            controle.SalvaAtualiza();

                            btnLimpar_Click(sender, e);
                            btnExibir_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Efetuado ponto para evento posterior na data atual, solicite correção junto a gerência", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Já efetuado o ponto para o evento e data referido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (rdbAlmoco.Checked)
                {
                    if (ponto.Saida_Almoco == TimeSpan.Parse("00:00"))
                    {
                        if (ponto.Entrada != TimeSpan.Parse("00:00"))
                        {
                            if (ponto.Retorno_Almoco == TimeSpan.Parse("00:00") && ponto.Saida == TimeSpan.Parse("00:00"))
                            {
                                ponto.Saida_Almoco = DateTime.Now.TimeOfDay;
                                alteracao = "Saída Alm. : " + ponto.Saida_Almoco.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Efetuado ponto para evento posterior na data atual, solicite correção junto a gerência", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não efetuado ponto do evento \"Entrada\" na data atual, efetue o lançamento ou solicite correção junto a gerência", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Já efetuado o ponto para o evento e data referido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (rdbRetorno.Checked)
                {
                    if (ponto.Retorno_Almoco == TimeSpan.Parse("00:00"))
                    {
                        if (ponto.Entrada != TimeSpan.Parse("00:00") && ponto.Saida_Almoco != TimeSpan.Parse("00:00"))
                        {
                            if (ponto.Saida == TimeSpan.Parse("00:00"))
                            {
                                ponto.Falta = false;
                                ponto.Retorno_Almoco = DateTime.Now.TimeOfDay;
                                alteracao = "Retorno Alm. : " + ponto.Retorno_Almoco.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Efetuado ponto para evento posterior na data atual, solicite correção junto a gerência", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Pendente ponto do evento \"Saída Almoço\" na data atual, efetue o lançamento ou solicite correção junto a gerência", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Já efetuado o ponto para o evento e data referido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (rdbSaida.Checked)
                {
                    if (ponto.Saida == TimeSpan.Parse("00:00"))
                    {
                        if (ponto.Entrada != TimeSpan.Parse("00:00") && ponto.Saida_Almoco == TimeSpan.Parse("00:00") && ponto.Retorno_Almoco == TimeSpan.Parse("00:00")
                            || ponto.Entrada != TimeSpan.Parse("00:00") && ponto.Saida_Almoco != TimeSpan.Parse("00:00") && ponto.Retorno_Almoco != TimeSpan.Parse("00:00"))
                        {
                            if ((ponto.Saida - ponto.Entrada > limiteJornadaDia && ponto.Saida_Almoco == TimeSpan.Parse("00:00")) || ((ponto.Saida_Almoco - ponto.Entrada) + (ponto.Saida - ponto.Retorno_Almoco) > limiteJornadaDia && ponto.Saida_Almoco != TimeSpan.Parse("00:00")))
                            {
                                MessageBox.Show("A tentativa de bater ponto excede o limite máximo diário, comunique a direção e solicite a correção", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                ponto.Saida = DateTime.Now.TimeOfDay;
                                alteracao = "Saída : " + ponto.Saida.ToString();
                                controlaBancoHoras(ponto);

                                controle.SalvaAtualiza();

                                btnLimpar_Click(sender, e);
                                btnExibir_Click(sender, e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Pendente ponto do evento \"Entrada\" ou \"Retorno Almoço\" na data atual, efetue o lançamento ou solicite correção junto a gerência", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Já efetuado o ponto para o evento e data referido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void GerarLog(CtrlPonto ponto, string alteracao)
        {
            string data = "";
            if (cmbDia.SelectedValue.ToString().Equals(""))
            {
                data = DateTime.Today.ToShortDateString();
            }
            else
            {
                data = cmbDia.SelectedValue.ToString() + "/" + cmbMes.SelectedIndex.ToString() + "/" + cmbAno.SelectedValue.ToString();
            }

            LogPonto log = new LogPonto();
            controle.salvarLog(log);

            log.id_Ponto = ponto.Id;
            log.id_User = usuario.id;
            log.dt_Alteracao = DateTime.Now;
            log.valor = alteracao + ", Dt: " + data;

            controle.SalvaAtualiza();
        }

        private void corrigeBancoHoras(TimeSpan bancoHrs)
        {
            string[] data = new string[2];
            data = cmbPeriodo.SelectedValue.ToString().Split('/');

            listaPonto = controle.PesquisaPonto(usuario.id, data[0], data[1]);

            if (bancoHrs <= TimeSpan.Parse(usuario.bancoHoras.ToString()))
            {
                usuario.bancoHoras = usuario.bancoHoras - bancoHrs;
                bancoHrs = TimeSpan.Parse("0:00");
                controle.SalvaAtualiza();
            }
        }

        private void controlaBancoHoras(CtrlPonto ponto)
        {
            string horaDia = "00:00";
            string horaExtra = "00:00";

            if (ponto.Saida_Almoco - ponto.Entrada > TimeSpan.Parse("00:00"))
            {
                horaDia = (ponto.Saida_Almoco - ponto.Entrada).ToString();

                if (ponto.Saida - ponto.Retorno_Almoco > TimeSpan.Parse("00:00"))
                {
                    horaDia = ((ponto.Saida_Almoco - ponto.Entrada) + ponto.Saida - (ponto.Retorno_Almoco)).ToString();
                }
            }
            else if (ponto.Saida - ponto.Entrada > TimeSpan.Parse("00:00"))
            {
                horaDia = (ponto.Saida - ponto.Entrada).ToString();
            }

            if (TimeSpan.Parse(horaDia) != TimeSpan.Parse("08:00"))
            {
                horaExtra = (TimeSpan.Parse(horaDia) - TimeSpan.Parse("08:00")).ToString();

                if (TimeSpan.Parse(horaDia) - TimeSpan.Parse("08:00") > TimeSpan.Parse("00:00"))
                {
                    if (TimeSpan.Parse(horaExtra) > limiteBancoDia)
                    {
                        usuario.bancoHoras = usuario.bancoHoras + limiteBancoDia;
                        controle.SalvaAtualiza();
                    }
                    else
                    {
                        usuario.bancoHoras = usuario.bancoHoras + TimeSpan.Parse(horaExtra);
                        controle.SalvaAtualiza();
                    }
                }
            }
        }

        private void CompensaDia(TimeSpan qntHoras)
        {
            CtrlPonto ponto = controle.PesquisaPontoDia(usuario.id, cmbMes.SelectedIndex.ToString(), cmbAno.SelectedItem.ToString(), cmbDia.SelectedItem.ToString());

            ponto.Saida = ponto.Saida + qntHoras;

            if (ponto.Saida >= TimeSpan.Parse("08:00"))
            {
                ponto.Falta = false;
            }
            else
            {
                ponto.Falta = true;
            }

            controle.SalvaAtualiza();

        }

        private void cmbPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExibir.Enabled = true;
        }

        private bool validaHora()
        {
            TimeSpan result = TimeSpan.Parse("00:00");
            if (TimeSpan.TryParse(txtHora.Text, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (cmbDia.SelectedItem.Equals(""))
            {
                MessageBox.Show("Favor preencher os campos data completamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                string dia = cmbDia.SelectedValue.ToString();
                string mes = cmbMes.SelectedIndex.ToString();
                string ano = cmbAno.SelectedValue.ToString();
                int user_id = 0;

                user_id = controle.PesquisaUserNome(cmbFuncionarios.SelectedValue.ToString()).id;

                CtrlPonto pontoEscolhido = new CtrlPonto();
                pontoEscolhido = controle.PesquisaPontoDia(user_id, mes, ano, dia);

                TimeSpan bancoHr = TimeSpan.Parse("00:00");
                string tempo = "";

                if (pontoEscolhido != null)
                {

                    if (pontoEscolhido.Entrada < pontoEscolhido.Saida_Almoco)
                    {
                        tempo = (pontoEscolhido.Saida_Almoco - pontoEscolhido.Entrada).ToString();
                        bancoHr = bancoHr + TimeSpan.Parse(tempo);

                        if (pontoEscolhido.Saida_Almoco < pontoEscolhido.Saida && pontoEscolhido.Saida_Almoco < pontoEscolhido.Retorno_Almoco)
                        {
                            tempo = (pontoEscolhido.Retorno_Almoco - pontoEscolhido.Saida).ToString();
                            bancoHr = bancoHr + TimeSpan.Parse(tempo);
                        }
                    }
                    else
                    {
                        if (pontoEscolhido.Entrada < pontoEscolhido.Saida)
                        {
                            tempo = (pontoEscolhido.Saida - pontoEscolhido.Entrada).ToString();
                            bancoHr = bancoHr + TimeSpan.Parse(tempo);
                        }
                    }

                    if (bancoHr > TimeSpan.Parse("8:00:00"))
                    {
                        bancoHr = bancoHr - TimeSpan.Parse("8:00:00");
                        if (bancoHr > TimeSpan.Parse("2:00:00"))
                        {
                            bancoHr = TimeSpan.Parse("2:00:00");
                        }
                    }
                    else
                    {
                        bancoHr = TimeSpan.Parse("00:00:00");
                    }

                    usuario.bancoHoras = usuario.bancoHoras - bancoHr;
                    controle.SalvaAtualiza();

                    controle.ExcluirCtrlPonto(pontoEscolhido);
                    controle.SalvaAtualiza();

                    LogPonto logPonto = new LogPonto();
                    controle.salvarLog(logPonto);
                    logPonto.dt_Alteracao = DateTime.Now;
                    logPonto.id_Ponto = pontoEscolhido.Id;
                    logPonto.id_User = user.id;
                    logPonto.valor = "Excl: DT-" + pontoEscolhido.Dt_Ponto.ToShortDateString() + ",FUNC-" + cmbFuncionarios.SelectedValue.ToString();
                    controle.SalvaAtualiza();
                }
            }
            btnLimpar_Click(sender, e);
            carregaPontoPeriodo(cmbPeriodo.SelectedValue.ToString());
        }

        private void dgvPonto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selecionaLinha();
        }

        private void selecionaLinha()
        {
            try
            {
                int user_id = controle.PesquisaUserNome(cmbFuncionarios.SelectedValue.ToString()).id;
                string[] periodo;
                char delimiter = '/';
                periodo = cmbPeriodo.SelectedValue.ToString().Split(delimiter);

                CtrlPonto ponto = new CtrlPonto();
                ponto = controle.PesquisaPontoDia(user_id, periodo[0], periodo[1], dgvPonto.SelectedCells[0].Value.ToString());

                if (ponto != null)
                {
                    cmbDia.Enabled = true;
                    cmbMes.Enabled = true;
                    cmbAno.Enabled = true;

                    cmbDia.ResetText();
                    cmbMes.ResetText();
                    cmbAno.ResetText();

                    cmbAno.SelectedItem = ponto.Dt_Ponto.Year.ToString();                    
                    cmbMes.SelectedIndex = ponto.Dt_Ponto.Month;
                    cmbDia.SelectedIndex = ponto.Dt_Ponto.Day - 1;

                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
