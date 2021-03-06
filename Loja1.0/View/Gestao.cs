﻿using System;
using System.Windows.Forms;
using Loja1._0.Model;
using FiscalPrinterBematech;
using Loja1._0.View;
using Loja1._0.Control;
using System.Collections.Generic;

namespace Loja1._0
{
    public partial class Gestao : Form
    {
        private Model.Usuarios user;
        private Controle controle = new Controle();
        public TextBox txtChequePrimeiro = new TextBox();
        public TextBox txtChequeUltimo = new TextBox();
        public static string valorAbertura;
        public Gerenciamento gerencia = new Gerenciamento();

        public Gestao(Model.Usuarios user)
        {
            try
            {
                this.user = user;
                InitializeComponent();
                gerencia = controle.PesquisaGerenciamento(1);
                if (gerencia != null)
                {
                    preencheDados(gerencia);
                }
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

        private void btnAbertura_Click(object sender, EventArgs e)
        {
            ShowMyDialogBox();
            BemaFI32.Bematech_FI_AberturaDoDia(valorAbertura,"");
        }

        private void btnAliquota_Click(object sender, EventArgs e)
        {
            if (gerencia != null)
            {
                //Configuração inicial Impressora fiscal
                BemaFI32.Bematech_FI_AlteraSimboloMoeda("R");
                BemaFI32.Bematech_FI_ProgramaAliquota(gerencia.tributacao.ToString(), 0);
                BemaFI32.Bematech_FI_LinhasEntreCupons(0);
                BemaFI32.Bematech_FI_EspacoEntreLinhas(0);
                //Configuração pagamentos
                BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("C.Crédito", "1");
                BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("Cheque", "0");
                BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("Débito", "1");
                BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("Pré-Pago", "0");
            }
            else
            {
                MessageBox.Show("Para atribuição de aliquota é necessário salvar as definições antes, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowMyDialogBox()
        {
            DialogoAbertura testDialog = new DialogoAbertura();

            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            testDialog.Dispose();
        }

        private void btnFechamento_Click(object sender, EventArgs e)
        {
            int numVendas = 0;
            decimal valorVendas = 0.00M;
            decimal lucroVendas = 0.00M;
            decimal dinheiroVendas = 0.00M;
            decimal tefVistaVendas = 0.00M;
            decimal prePagoVendas = 0.00M;
            decimal tefPrazoVendas = 0.00M;
            string movimentoDia = "";
            string relaçãoProdutos = "";
            List<Vendas> vendasDia = new List<Vendas>();
            List<string> produtosVendidos = new List<string>();

            Contabil contabil = new Contabil(user);
            contabil.carregaMovimentos(DateTime.Today, DateTime.Today);
            Contabilidade contabilidade = controle.PesquisaContabilidadeById(1);

            /*List<Movimentos> listaMovimentosDia = new List<Movimentos>();
            listaMovimentosDia = controle.PesquisaMovPeriodo(DateTime.Today, DateTime.Today);
            */

            numVendas = controle.QuantidadeVendasDia(DateTime.Today);
            valorVendas = Convert.ToDecimal(controle.ValorTotalVendasDia(DateTime.Today));            
            lucroVendas = Convert.ToDecimal(contabilidade.lucro);
            dinheiroVendas = controle.PesquisaMovDiaTipo(DateTime.Today, 19);
            tefVistaVendas = controle.PesquisaMovDiaTipo(DateTime.Today, 33);
            prePagoVendas = controle.PesquisaMovDiaTipo(DateTime.Today, 30);
            tefPrazoVendas = controle.PesquisaMovDiaTipo(DateTime.Today, 18) + controle.PesquisaMovDiaTipo(DateTime.Today, 4);

            vendasDia = controle.PesquisaVendasDia(DateTime.Today);
            foreach(Vendas value in vendasDia)
            {
                List<Vendas_Produtos> produtosVenda = new List<Vendas_Produtos>();
                produtosVenda = controle.PesquisaProdutosVenda(value.id);

                foreach(Vendas_Produtos result in produtosVenda)
                {
                    Model.Produtos produtoVendido = new Model.Produtos();
                    produtoVendido = controle.PesquisaProdutoId(Convert.ToInt32(result.id_produto));

                    produtosVendidos.Add("<br />&nbsp Venda Nº" + result.id_venda + "; produto: " + produtoVendido.desc_produto + "; quantidade: " + result.quantidade);
                }
            }

            foreach (string value in produtosVendidos)
            {
                relaçãoProdutos = relaçãoProdutos + value; 
            }

            movimentoDia = "<br /><br />&nbsp Número de vendas realizadas: " + numVendas +
                            "<br />&nbsp Valor total das vendas: R$" + valorVendas +
                            "<br />&nbsp Lucro total: R$" + lucroVendas +
                            "<br />" +
                            "<br />&nbsp Descriminação do total de vendas: " +
                            "<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Valor total à em dinheiro: R$" + dinheiroVendas +
                            "<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Valor total à vista(tef): R$" + tefVistaVendas +
                            "<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Valor total pré-pago: R$" + prePagoVendas +
                            "<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Valor total prazo(tef): R$" + tefPrazoVendas +
                            "<br />" +
                            "<br />Relação de Produtos vendidos:" +
                            "<br />" + relaçãoProdutos;

            Email email = new Email();
            email.EnviaEmailMovimento(movimentoDia);

            BemaFI32.Bematech_FI_FechamentoDoDia();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            btnAlterar.Enabled = false;
            btnSalvar.Enabled = true;
            pnlBasico.Enabled = true;
            pnlCredito.Enabled = true;
            pnlCheque.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {                
                if (validaCampos())
                {
                    btnAlterar.Enabled = true;
                    btnSalvar.Enabled = false;
                    pnlBasico.Enabled = false;
                    pnlCredito.Enabled = false;
                    pnlCheque.Enabled = false;

                    if (gerencia == null)
                    {
                        gerencia = new Gerenciamento();
                        controle.SalvaGerenciamento(gerencia);
                        gerencia.id = 1;
                    }                    
                    gerencia.lucroMinimo = Convert.ToDecimal(txtLucroMin.Text);
                    gerencia.tributacao = Convert.ToDecimal(txtTributacao.Text);
                    gerencia.comissao = Convert.ToDecimal(txtComissao.Text);
                    gerencia.autoDescPerc = Convert.ToDecimal(txtPercDescAuto.Text);
                    gerencia.autoDescValor = Convert.ToDecimal(txtValorDescAuto.Text);
                    gerencia.maxDescPerc = Convert.ToInt32(txtDescMaximo.Text);

                    gerencia.jurosPrazo2x = Convert.ToDecimal(txtJurosCC2.Text);
                    gerencia.jurosPrazo3x = Convert.ToDecimal(txtJurosCC3.Text);
                    gerencia.jurosPrazo4x = Convert.ToDecimal(txtJurosCC4.Text);
                    gerencia.jurosPrazo5x = Convert.ToDecimal(txtJurosCC5.Text);
                    gerencia.jurosPrazo6x = Convert.ToDecimal(txtJurosCC6.Text);
                    gerencia.jurosPrazo7x = Convert.ToDecimal(txtJurosCC7.Text);
                    gerencia.jurosPrazo8x = Convert.ToDecimal(txtJurosCC8.Text);
                    gerencia.jurosPrazo9x = Convert.ToDecimal(txtJurosCC9.Text);
                    gerencia.jurosPrazo10x = Convert.ToDecimal(txtJurosCC10.Text);
                    gerencia.jurosPrazo11x = Convert.ToDecimal(txtJurosCC11.Text);
                    gerencia.jurosPrazo12x = Convert.ToDecimal(txtJurosCC12.Text);

                    gerencia.jurosCheque1x = Convert.ToDecimal(txtJurosCheque1.Text);
                    gerencia.jurosCheque2x = Convert.ToDecimal(txtJurosCheque2.Text);
                    gerencia.jurosCheque3x = Convert.ToDecimal(txtJurosCheque3.Text);
                    gerencia.jurosCheque4x = Convert.ToDecimal(txtJurosCheque4.Text);
                    gerencia.jurosCheque5x = Convert.ToDecimal(txtJurosCheque5.Text);
                    gerencia.jurosCheque6x = Convert.ToDecimal(txtJurosCheque6.Text);
                    gerencia.jurosCheque7x = Convert.ToDecimal(txtJurosCheque7.Text);
                    gerencia.jurosCheque8x = Convert.ToDecimal(txtJurosCheque8.Text);
                    gerencia.jurosCheque9x = Convert.ToDecimal(txtJurosCheque9.Text);
                    gerencia.jurosCheque10x = Convert.ToDecimal(txtJurosCheque10.Text);
                    gerencia.jurosCheque11x = Convert.ToDecimal(txtJurosCheque11.Text);
                    gerencia.jurosCheque12x = Convert.ToDecimal(txtJurosCheque12.Text);
                    controle.SalvaAtualiza();
                }
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gerencia = controle.PesquisaGerenciamento(1);
            if (gerencia != null)
            {
                preencheDados(gerencia);
            }
            else
            {
                txtLucroMin.Text = "";
                txtTributacao.Text = "";
                txtComissao.Text = "";
                txtPercDescAuto.Text = "";
                txtValorDescAuto.Text = "";
                txtDescMaximo.Text = "";

                txtJurosCC2.Text = "";
                txtJurosCC3.Text = "";
                txtJurosCC4.Text = "";
                txtJurosCC5.Text = "";
                txtJurosCC6.Text = "";
                txtJurosCC7.Text = "";
                txtJurosCC8.Text = "";
                txtJurosCC9.Text = "";
                txtJurosCC10.Text = "";
                txtJurosCC11.Text = "";
                txtJurosCC12.Text = "";

                txtJurosCheque1.Text = "";
                txtJurosCheque2.Text = "";
                txtJurosCheque3.Text = "";
                txtJurosCheque4.Text = "";
                txtJurosCheque5.Text = "";
                txtJurosCheque6.Text = "";
                txtJurosCheque7.Text = "";
                txtJurosCheque8.Text = "";
                txtJurosCheque9.Text = "";
                txtJurosCheque10.Text = "";
                txtJurosCheque11.Text = "";
                txtJurosCheque12.Text = "";
            }
            btnAlterar.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = true;

            pnlBasico.Enabled = false;
            pnlCheque.Enabled = false;
            pnlCredito.Enabled = false;
        }

        private bool validaCampos()
        {
            try
            {
                if (txtLucroMin.Text.Equals("") || Convert.ToDecimal(txtLucroMin.Text) < 5 || Convert.ToDecimal(txtLucroMin.Text) > 90)
                {
                    MessageBox.Show("O campo referente ao lucro minímo deve ser um número entre 5 e 90", "Ação Inválida");
                    return false;
                }
                else if (txtDescMaximo.Text.Equals("") || txtDescMaximo.Text.Equals("0"))
                {
                    txtDescMaximo.Text = "100";
                }
                else if (txtTributacao.Text.Equals(""))
                {
                    MessageBox.Show("O campo referente a tributação deve ser preenchido com o valor da carga tributária ou percentual do imposto", "Ação Inválida");
                    return false;
                }
                else if (txtComissao.Text.Equals(""))
                {
                    txtComissao.Text = "0";
                }
                else if (txtPercDescAuto.Text.Equals(""))
                {
                    txtPercDescAuto.Text = "0";
                }
                else if (Convert.ToDecimal(txtPercDescAuto.Text) > 100 - Convert.ToDecimal(txtLucroMin.Text) || Convert.ToDecimal(txtPercDescAuto.Text) > Convert.ToDecimal(txtDescMaximo.Text))
                {
                    MessageBox.Show("O campo referente ao desconto fornecido no PDV deve ser inferior ao desconto maxímo permitido e inferior ao percentual que deve compor o lucro minímo", "Ação Inválida");
                    return false;
                }
                else if (txtValorDescAuto.Text.Equals(""))
                {
                    txtValorDescAuto.Text = "0";
                }
                else if (txtJurosCC2.Text.Equals("") || txtJurosCC3.Text.Equals("") || txtJurosCC4.Text.Equals("") || txtJurosCC5.Text.Equals("") || txtJurosCC6.Text.Equals("") || txtJurosCC7.Text.Equals("") || txtJurosCC8.Text.Equals("") || txtJurosCC9.Text.Equals("") || txtJurosCC10.Text.Equals("") || txtJurosCC11.Text.Equals("") || txtJurosCC12.Text.Equals(""))
                {
                    MessageBox.Show("Todos os campos referentes aos juros de cartão devem ser preenchidos com valores válidos, entre 0 e 100", "Ação Inválida");
                    return false;
                }
                else if (txtJurosCheque1.Text.Equals("") || txtJurosCheque2.Text.Equals("") || txtJurosCheque3.Text.Equals("") || txtJurosCheque4.Text.Equals("") || txtJurosCheque5.Text.Equals("") || txtJurosCheque6.Text.Equals("") || txtJurosCheque7.Text.Equals("") || txtJurosCheque8.Text.Equals("") || txtJurosCheque9.Text.Equals("") || txtJurosCheque10.Text.Equals("") || txtJurosCheque11.Text.Equals("") || txtJurosCheque12.Text.Equals(""))
                {
                    MessageBox.Show("Todos os campos referentes aos juros de cheque devem ser preenchidos com valores válidos, entre 0 e 100", "Ação Inválida");
                    return false;
                }
                else
                {
                    return true;
                }
                return false;
            }
            catch
            {
                MessageBox.Show("Erro não identificado, por favor, tente novamente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void preencheDados(Gerenciamento gerencia)
        {
            txtLucroMin.Text = gerencia.lucroMinimo.ToString();
            txtTributacao.Text = gerencia.tributacao.ToString();
            txtComissao.Text = gerencia.comissao.ToString();
            txtPercDescAuto.Text = gerencia.autoDescPerc.ToString();
            txtValorDescAuto.Text = gerencia.autoDescValor.ToString();
            txtDescMaximo.Text = gerencia.maxDescPerc.ToString();

            txtJurosCC2.Text = gerencia.jurosPrazo2x.ToString();
            txtJurosCC3.Text = gerencia.jurosPrazo3x.ToString();
            txtJurosCC4.Text = gerencia.jurosPrazo4x.ToString();
            txtJurosCC5.Text = gerencia.jurosPrazo5x.ToString();
            txtJurosCC6.Text = gerencia.jurosPrazo6x.ToString();
            txtJurosCC7.Text = gerencia.jurosPrazo7x.ToString();
            txtJurosCC8.Text = gerencia.jurosPrazo8x.ToString();
            txtJurosCC9.Text = gerencia.jurosPrazo9x.ToString();
            txtJurosCC10.Text = gerencia.jurosPrazo10x.ToString();
            txtJurosCC11.Text = gerencia.jurosPrazo11x.ToString();
            txtJurosCC12.Text = gerencia.jurosPrazo12x.ToString();

            txtJurosCheque1.Text = gerencia.jurosCheque1x.ToString();
            txtJurosCheque2.Text = gerencia.jurosCheque2x.ToString();
            txtJurosCheque3.Text = gerencia.jurosCheque3x.ToString();
            txtJurosCheque4.Text = gerencia.jurosCheque4x.ToString();
            txtJurosCheque5.Text = gerencia.jurosCheque5x.ToString();
            txtJurosCheque6.Text = gerencia.jurosCheque6x.ToString();
            txtJurosCheque7.Text = gerencia.jurosCheque7x.ToString();
            txtJurosCheque8.Text = gerencia.jurosCheque8x.ToString();
            txtJurosCheque9.Text = gerencia.jurosCheque9x.ToString();
            txtJurosCheque10.Text = gerencia.jurosCheque10x.ToString();
            txtJurosCheque11.Text = gerencia.jurosCheque11x.ToString();
            txtJurosCheque12.Text = gerencia.jurosCheque12x.ToString();
        }
    }
}
