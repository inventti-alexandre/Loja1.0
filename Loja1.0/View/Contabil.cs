using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Loja1._0.Model;
using Loja1._0.Control;
using System.Linq;

namespace Loja1._0
{
    public partial class Contabil : Form
    {
        private Model.Usuarios user;
        private Controle controle = new Controle();
        public List<Movimentos> listaMovimentos = new List<Movimentos>();
        public List<Tipos_Movimentacao> listaTiposMov = new List<Tipos_Movimentacao>();
        public static bool comboCarregado = false;
        public static Contabilidade contabilidade = new Contabilidade();
        private static DataTable dtAtivoCirc = new DataTable();
        private static DataTable dtAtivoNaoCirc = new DataTable();
        private static DataTable dtPassCirc = new DataTable();
        private static DataTable dtPassNaoCirc = new DataTable();
        private static DataTable dtPatrLiq = new DataTable();      
        private static List<classMovimento> listaStruct = new List<classMovimento>();

        public Contabil(Model.Usuarios user)
        {
            try
            {
                this.user = user;
                InitializeComponent();

                comboCarregado = false;

                carregaComboMovimento();
                zerarContabilidade();

                if (controle.PesquisaContabilidade().Count != 0)
                {
                    carregaMovimentos(DateTime.Today.AddYears(-5), DateTime.Today.AddYears(5));
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaComboMovimento()
        {
            listaTiposMov = new List<Tipos_Movimentacao>();
            listaTiposMov = controle.PesquisaTiposMovimento();
            
            cmbMovimento.DataSource = listaTiposMov;
            cmbMovimento.ValueMember = "descricao";
            cmbMovimento.SelectedValue = "";
            comboCarregado = true;
        }

        private void zerarContabilidade()
        {
            contabilidade = new Contabilidade();
            if (controle.PesquisaContabilidade().Count == 0)
            {
                controle.SalvaContabilidade(contabilidade);
                contabilidade.id = 1;
            }
            else
            {
                contabilidade = controle.PesquisaContabilidadeById(1);
            }
            contabilidade.a_pagar_cp = 0;
            contabilidade.a_pagar_lp = 0;
            contabilidade.a_receber_cp = 0;
            contabilidade.a_receber_lp = 0;
            contabilidade.banco = 0;
            contabilidade.caixa = 0;
            contabilidade.cap_soc_integ = 0;
            contabilidade.cap_soc_n_integ = 0;
            contabilidade.clientes = 0;
            contabilidade.despesas = 0;
            contabilidade.estoques = 0;
            contabilidade.fornecedores = 0;
            contabilidade.imposto_recolher = 0;
            contabilidade.lucro = 0;
            contabilidade.maquinario = 0;
            contabilidade.moveis_utensilios = 0;
            contabilidade.pro_labore = 0;
            contabilidade.imovel = 0;
            contabilidade.salarios = 0;
            contabilidade.veiculos = 0;
            controle.SalvaAtualiza();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnLimpar_Click(sender, e);
            Inicial form = new Inicial(user);
            form.Show();
            Dispose();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {            
            try
            {
                if (validaCampos())
                {
                    Movimentos movimento = new Movimentos();
                    controle.SalvarMovimento(movimento);
                    movimento.data = Convert.ToDateTime(txtData.Text);
                    movimento.desc = txtDescricao.Text;
                    movimento.valor = Convert.ToDecimal(txtValor.Text);
                    movimento.id_tipo = controle.PesquisaCompletaIDTipoMov(cmbMovimento.Text, cmbOrigem.Text, cmbFormaPg.Text);
                    controle.SalvaAtualiza();
                    pnlAtualizar.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validaCampos()
        {
            if(DateTime.TryParse(txtData.Text, out DateTime result))
            {
                if(!txtDescricao.Text.Equals("") && txtDescricao.Text.Length >= 5)
                {
                    if ((!txtValor.Text.Any(char.IsNumber) || !txtValor.Text.Any(char.IsPunctuation)))
                    {
                        MessageBox.Show("O campo \"Valor\" deve ser preenchido com o formato \"XX.XX\".");
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("O campo \"Descrição\" deve estar preenchido e conter no minímo 5 caracteres, por favor, corrija e tente novamente", "Ação inválida");
                }
            }
            else
            {
                MessageBox.Show("O campo \"Data\" foi preenchido com formato inválido, por favor, corrija e tente novamente", "Ação inválida");               
            }
            return false;

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpaCombos();
            comboCarregado = false;
            carregaComboMovimento();
            pnlAtualizar.Enabled = true;
        }

        private void carregaMovimentos(DateTime inicio, DateTime fim)
        {
            try
            {                
                listaMovimentos = new List<Movimentos>();
                listaMovimentos = controle.PesquisaMovPeriodo(inicio, fim);

                if (listaMovimentos.Count > 0)
                {
                    listaStruct = new List<classMovimento>();
                    int j = 0;
                    for (int i = 0; i < listaMovimentos.Count; i++)
                    {
                        if (i == 0)
                        {
                            classMovimento mov = new classMovimento();
                            listaStruct.Add(mov);
                            listaStruct[i].id_tipo = Convert.ToInt32(listaMovimentos[i].id_tipo);
                            listaStruct[i].conta = listaMovimentos[i].Tipos_Movimentacao.conta;
                            listaStruct[i].valor = Convert.ToDecimal(listaMovimentos[i].valor);
                        }
                        else if (listaStruct[j].id_tipo == listaMovimentos[i].id_tipo)
                        {
                            listaStruct[j].valor = listaStruct[j].valor + Convert.ToDecimal(listaMovimentos[i].valor);
                        }
                        else
                        {
                            j++;
                            classMovimento mov = new classMovimento();
                            listaStruct.Add(mov);
                            listaStruct[j].id_tipo = Convert.ToInt32(listaMovimentos[i].id_tipo);
                            listaStruct[j].conta = listaMovimentos[i].Tipos_Movimentacao.conta;
                            listaStruct[j].valor = Convert.ToDecimal(listaMovimentos[i].valor);
                        }
                    }
                    j = 0;

                    foreach (classMovimento value in listaStruct)
                    {
                        if (value.conta.Equals("AtvCirc"))
                        {
                            atualizaAtvCirc(value);
                        }
                        else if (value.conta.Equals("AtvNaoCirc"))
                        {
                            atualizaAtvNCirc(value);
                        }
                        else if (value.conta.Equals("PassCirc"))
                        {
                            atualizaPassCirc(value);
                        }
                        else if (value.conta.Equals("PassNaoCirc"))
                        {
                            atualizaPassNCirc(value);
                        }
                        else if (value.conta.Equals("PatrLiq"))
                        {
                            atualizaPatrLiq(value);
                        }
                    }
                    instanciarDataTable();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void instanciarDataTable()
        {
            try
            {
                DataTable dtAtivoCirc = new DataTable();
                DataTable dtAtivoNaoCirc = new DataTable();
                DataTable dtPassCirc = new DataTable();
                DataTable dtPassNaoCirc = new DataTable();
                DataTable dtPatrLiq = new DataTable();

                dtAtivoCirc.Columns.Add("movimento", typeof(string));
                dtAtivoCirc.Columns.Add("valor", typeof(decimal));
                dtAtivoNaoCirc.Columns.Add("movimento", typeof(string));
                dtAtivoNaoCirc.Columns.Add("valor", typeof(decimal));
                dtPassCirc.Columns.Add("movimento", typeof(string));
                dtPassCirc.Columns.Add("valor", typeof(decimal));
                dtPassNaoCirc.Columns.Add("movimento", typeof(string));
                dtPassNaoCirc.Columns.Add("valor", typeof(decimal));
                dtPatrLiq.Columns.Add("movimento", typeof(string));
                dtPatrLiq.Columns.Add("valor", typeof(decimal));

                //Criando o DataTable de Ativo Circulante
                if (contabilidade.caixa != 0)
                {
                    dtAtivoCirc.Rows.Add("Caixa......................................................", contabilidade.caixa);
                }
                if (contabilidade.banco != 0)
                {
                    dtAtivoCirc.Rows.Add("Banco......................................................", contabilidade.banco);
                }
                if (contabilidade.estoques != 0)
                {
                    dtAtivoCirc.Rows.Add("Estoque....................................................", contabilidade.estoques);
                }
                if (contabilidade.a_receber_cp != 0)
                {
                    dtAtivoCirc.Rows.Add("Duplicatas a Receber.......................................", contabilidade.a_receber_cp);
                }
                if (contabilidade.maquinario != 0)
                {
                    dtAtivoCirc.Rows.Add("Maquinário.................................................", contabilidade.maquinario);
                }
                if (contabilidade.moveis_utensilios != 0)
                {
                    dtAtivoCirc.Rows.Add("Ferramentas e Móveis.......................................", contabilidade.moveis_utensilios);
                }

                //Criando o DataTable de Ativo Não Circulante
                if (contabilidade.a_receber_lp != 0)
                {
                    dtAtivoNaoCirc.Rows.Add("Duplicatas a Receber Longo Prazo...........................", contabilidade.a_receber_lp);
                }
                if (contabilidade.veiculos != 0)
                {
                    dtAtivoNaoCirc.Rows.Add("Veículos...................................................", contabilidade.veiculos);
                }
                if (contabilidade.imovel != 0)
                {
                    dtAtivoNaoCirc.Rows.Add("Imóveis....................................................", contabilidade.imovel);
                }

                //Criando o DataTable de Passivo Circulante
                if (contabilidade.despesas != 0)
                {
                    dtPassCirc.Rows.Add("Despesas.......................................................", contabilidade.despesas);
                }
                if (contabilidade.fornecedores != 0)
                {
                    dtPassCirc.Rows.Add("Fornecedores...................................................", contabilidade.fornecedores);
                }
                if (contabilidade.clientes != 0)
                {
                    dtPassCirc.Rows.Add("Crédito de Clientes............................................", contabilidade.clientes);
                }
                if (contabilidade.salarios != 0)
                {
                    dtPassCirc.Rows.Add("Salários.......................................................", contabilidade.salarios);
                }
                if (contabilidade.imposto_recolher != 0)
                {
                    dtPassCirc.Rows.Add("Impostos Devidos...............................................", contabilidade.imposto_recolher);
                }
                if (contabilidade.a_pagar_cp != 0)
                {
                    dtPassCirc.Rows.Add("Contas a Pagar.................................................", contabilidade.a_pagar_cp);
                }

                //Criando o DataTable de Passivo Não Circulante
                if (contabilidade.a_pagar_lp != 0)
                {
                    dtPassNaoCirc.Rows.Add("Contas a Pagar Longo Prazo..............................................", contabilidade.a_pagar_lp);
                }

                //Criando o DataTable de Patrimonio Liquido
                if (contabilidade.lucro != 0)
                {
                    dtPatrLiq.Rows.Add("Lucro..........................................................", contabilidade.lucro);
                }
                if (contabilidade.pro_labore != 0)
                {
                    dtPatrLiq.Rows.Add("Pró-Labore.....................................................", contabilidade.pro_labore);
                }
                if (contabilidade.cap_soc_integ != 0)
                {
                    dtPatrLiq.Rows.Add("Capital Social Integralizado...................................", contabilidade.cap_soc_integ);
                }
                if (contabilidade.cap_soc_n_integ != 0)
                {
                    dtPatrLiq.Rows.Add("Capital Social Não Integralizado...............................", contabilidade.cap_soc_n_integ);
                }

                dgvAtvCirc.DataSource = dtAtivoCirc;
                dgvAtvNCirc.DataSource = dtAtivoNaoCirc;
                dgvPassCirc.DataSource = dtPassCirc;
                dgvPassNCirc.DataSource = dtPassNaoCirc;
                dgvPatrLiq.DataSource = dtPatrLiq;

                dgvAtvCirc.Columns[0].Width = 198;
                dgvAtvCirc.Columns[1].Width = 68;
                dgvAtvNCirc.Columns[0].Width = 198;
                dgvAtvNCirc.Columns[1].Width = 68;
                dgvPassCirc.Columns[0].Width = 200;
                dgvPassCirc.Columns[1].Width = 70;
                dgvPassNCirc.Columns[0].Width = 200;
                dgvPassNCirc.Columns[1].Width = 70;
                dgvPatrLiq.Columns[0].Width = 200;
                dgvPatrLiq.Columns[1].Width = 70;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizaAtvCirc(classMovimento mov)
        {
            try
            {
                if (mov.id_tipo == 12)
                {
                    contabilidade.estoques = contabilidade.estoques + mov.valor;
                    contabilidade.fornecedores = contabilidade.fornecedores + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 18)
                {
                    contabilidade.a_receber_cp = contabilidade.a_receber_cp + mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 19)
                {
                    contabilidade.caixa = contabilidade.caixa + mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 26)
                {
                    contabilidade.a_receber_cp = contabilidade.a_receber_cp + mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 30)
                {
                    contabilidade.clientes = contabilidade.clientes - mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 33)
                {
                    contabilidade.banco = contabilidade.banco + mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 34)
                {
                    contabilidade.banco = contabilidade.banco + mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 35)
                {
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    contabilidade.caixa = contabilidade.caixa + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 65)
                {
                    contabilidade.estoques = contabilidade.estoques - mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 78)
                {
                    contabilidade.a_receber_cp = contabilidade.a_receber_cp - mov.valor;
                    contabilidade.caixa = contabilidade.caixa + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 79)
                {
                    contabilidade.a_receber_cp = contabilidade.a_receber_cp - mov.valor;
                    contabilidade.banco = contabilidade.banco + mov.valor;
                    controle.SalvaAtualiza();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizaAtvNCirc(classMovimento mov)
        {
            try
            {
                if (mov.id_tipo == 4)
                {
                    contabilidade.a_receber_lp = contabilidade.a_receber_lp + mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 13)
                {
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 16)
                {
                    contabilidade.maquinario = contabilidade.maquinario + mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 17)
                {
                    contabilidade.veiculos = contabilidade.veiculos + mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 20)
                {
                    contabilidade.moveis_utensilios = contabilidade.moveis_utensilios + mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 24)
                {
                    contabilidade.a_receber_lp = contabilidade.a_receber_lp + mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 49)
                {
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 50)
                {
                    contabilidade.maquinario = contabilidade.maquinario + mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 51)
                {
                    contabilidade.veiculos = contabilidade.veiculos + mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 52)
                {
                    contabilidade.moveis_utensilios = contabilidade.moveis_utensilios + mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 54)
                {
                    contabilidade.despesas = contabilidade.despesas + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 55)
                {
                    contabilidade.maquinario = contabilidade.maquinario + mov.valor;
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 56)
                {
                    contabilidade.veiculos = contabilidade.veiculos + mov.valor;
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 58)
                {
                    contabilidade.moveis_utensilios = contabilidade.moveis_utensilios + mov.valor;
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 59)
                {
                    contabilidade.moveis_utensilios = contabilidade.moveis_utensilios + mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 60)
                {
                    contabilidade.moveis_utensilios = contabilidade.moveis_utensilios + mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 61)
                {
                    contabilidade.imovel = contabilidade.imovel + mov.valor;
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 62)
                {
                    contabilidade.imovel = contabilidade.imovel + mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 63)
                {
                    contabilidade.imovel = contabilidade.imovel + mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 80)
                {
                    contabilidade.caixa = contabilidade.caixa + mov.valor;
                    contabilidade.a_receber_lp = contabilidade.a_receber_lp - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 81)
                {
                    contabilidade.banco = contabilidade.banco + mov.valor;
                    contabilidade.a_receber_lp = contabilidade.a_receber_lp - mov.valor;
                    controle.SalvaAtualiza();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizaPassCirc(classMovimento mov)
        {
            try
            {
                if (mov.id_tipo == 2)
                {
                    contabilidade.despesas = contabilidade.despesas + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 6)
                {
                    contabilidade.despesas = contabilidade.despesas + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 8)
                {
                    contabilidade.salarios = contabilidade.salarios + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 9)
                {
                    contabilidade.salarios = contabilidade.salarios + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 11)
                {
                    contabilidade.imposto_recolher = contabilidade.imposto_recolher + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 25)
                {
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp + mov.valor;
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 29)
                {
                    contabilidade.clientes = contabilidade.clientes + mov.valor;
                    contabilidade.caixa = contabilidade.caixa + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 46)
                {
                    contabilidade.salarios = contabilidade.salarios - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 47)
                {
                    contabilidade.salarios = contabilidade.salarios - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 53)
                {
                    contabilidade.clientes = contabilidade.clientes + mov.valor;
                    contabilidade.banco = contabilidade.banco + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 67)
                {
                    contabilidade.despesas = contabilidade.despesas - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 68)
                {
                    contabilidade.despesas = contabilidade.despesas - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 69)
                {
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 70)
                {
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 73)
                {
                    contabilidade.fornecedores = contabilidade.fornecedores - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 74)
                {
                    contabilidade.fornecedores = contabilidade.fornecedores - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 75)
                {
                    contabilidade.fornecedores = contabilidade.fornecedores - mov.valor;
                    contabilidade.a_pagar_cp = contabilidade.a_pagar_cp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 76)
                {
                    contabilidade.imposto_recolher = contabilidade.imposto_recolher - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 77)
                {
                    contabilidade.imposto_recolher = contabilidade.imposto_recolher - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atualizaPassNCirc(classMovimento mov)
        {
            try
            {
                if (mov.id_tipo == 22)
                {
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    contabilidade.a_pagar_lp = contabilidade.a_pagar_lp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 82)
                {
                    contabilidade.a_pagar_lp = contabilidade.a_pagar_lp - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 83)
                {
                    contabilidade.a_pagar_lp = contabilidade.a_pagar_lp - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void atualizaPatrLiq(classMovimento mov)
        {
            try
            {
                if (mov.id_tipo == 7)
                {
                    contabilidade.lucro = contabilidade.lucro - mov.valor;
                    contabilidade.pro_labore = contabilidade.pro_labore + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 27)
                {
                    contabilidade.pro_labore = contabilidade.pro_labore - mov.valor;
                    contabilidade.lucro = contabilidade.lucro + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 28)
                {
                    contabilidade.cap_soc_integ = contabilidade.cap_soc_integ + mov.valor;
                    contabilidade.banco = contabilidade.banco + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 32)
                {
                    contabilidade.cap_soc_n_integ = contabilidade.cap_soc_n_integ + mov.valor;
                    contabilidade.a_receber_lp = contabilidade.a_receber_lp + mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 45)
                {
                    contabilidade.pro_labore = contabilidade.pro_labore - mov.valor;
                    contabilidade.banco = contabilidade.banco - mov.valor;
                    controle.SalvaAtualiza();
                }
                else if (mov.id_tipo == 84)
                {
                    contabilidade.pro_labore = contabilidade.pro_labore - mov.valor;
                    contabilidade.caixa = contabilidade.caixa - mov.valor;
                    controle.SalvaAtualiza();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.TryParse(txtDtInicio.Text, out DateTime inicio))
                {
                    if (DateTime.TryParse(txtDtFim.Text, out DateTime fim))
                    {
                        zerarContabilidade();
                        carregaMovimentos(Convert.ToDateTime(txtDtInicio.Text), Convert.ToDateTime(txtDtFim.Text));
                    }
                    else
                    {
                        MessageBox.Show("Os dados no campo data fim não correspondem a uma data valida", "Ação Inválida");
                    }
                }
                else
                {
                    MessageBox.Show("Os dados no campo data inicio não correspondem a uma data valida", "Ação Inválida");
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCarregado)
            {
                listaTiposMov = new List<Tipos_Movimentacao>();
                listaTiposMov = controle.PesquisaTiposMovimento();

                limpaCombos();

                pnlAtualizar.Enabled = false;
                
                cmbOrigem.Enabled = true;
                carregaComboOrigem(cmbMovimento.Text);                
            }
        }        

        private void carregaComboOrigem(string selectedValue)
        {
            comboCarregado = false;
            listaTiposMov = new List<Tipos_Movimentacao>();
            listaTiposMov = controle.PesquisaTiposMovimento(selectedValue);

            cmbOrigem.DataSource = listaTiposMov;
            cmbOrigem.ValueMember = "sub_tipo";
            cmbOrigem.SelectedValue = "";
            comboCarregado = true;

            cmbFormaPg.Enabled = false;
            txtValor.Enabled = false;
            txtData.Enabled = false;
            txtDescricao.Enabled = false;
            cmbFormaPg.Text = "";
            txtValor.Text = "";
            txtData.Text = "";
            txtDescricao.Text = "";
            txtTipo.Text = "";
        }

        private void defineTipo()
        {
            txtTipo.Text = listaTiposMov[0].direcao.ToUpper();
        }

        private void cmbOrigem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCarregado)
            {
                defineTipo();

                txtValor.Text = "";
                txtData.Text = "";
                txtDescricao.Text = "";

                if (controle.PesquisaSubTipoMov(cmbOrigem.Text).Count > 1)
                {
                    cmbFormaPg.Enabled = true;
                    carregaComboSubTipo(cmbOrigem.Text);

                    txtValor.Enabled = false;
                    txtData.Enabled = false;
                    txtDescricao.Enabled = false;
                }
                else
                {
                    cmbFormaPg.Enabled = false;
                    txtValor.Enabled = true;
                    txtData.Enabled = true;
                    txtDescricao.Enabled = true;
                    btnIncluir.Enabled = true;
                }
            }
        }

        private void carregaComboSubTipo(string selectedValue)
        {
            comboCarregado = false;
            listaTiposMov = new List<Tipos_Movimentacao>();
            listaTiposMov = controle.PesquisaSubTipoMov(selectedValue);

            cmbFormaPg.DataSource = listaTiposMov;
            cmbFormaPg.ValueMember = "forma_pag";
            cmbFormaPg.SelectedValue = "";
            comboCarregado = true;
        }

        private void cmbFormaPg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCarregado)
            {
                txtValor.Enabled = true;
                txtData.Enabled = true;
                txtDescricao.Enabled = true;
                btnIncluir.Enabled = true;
            }
        }

        private void limpaCombos()
        {
            btnLimpar.Enabled = true;
            btnIncluir.Enabled = false;

            cmbOrigem.Enabled = false;
            cmbFormaPg.Enabled = false;
            txtValor.Enabled = false;
            txtData.Enabled = false;
            txtDescricao.Enabled = false;
            cmbOrigem.Text = "";
            cmbFormaPg.Text = "";
            txtValor.Text = "";
            txtData.Text = "";
            txtDescricao.Text = "";
            txtTipo.Text = "";
        }
    }
}
