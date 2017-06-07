using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loja1._0.Model;
using FiscalPrinterBematech;
using Loja1._0.View;
using Loja1._0.Control;

namespace Loja1._0
{
    public partial class Gestao : Form
    {
        private Model.Usuarios user;
        private Controle controle = new Controle();
        public TextBox txtChequePrimeiro = new TextBox();
        public TextBox txtChequeUltimo = new TextBox();
        public static string valorAbertura;

        public Gestao(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
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
            //Configuração inicial Impressora fiscal
            BemaFI32.Bematech_FI_AlteraSimboloMoeda("R");
            BemaFI32.Bematech_FI_ProgramaAliquota("10,38", 0);
            BemaFI32.Bematech_FI_LinhasEntreCupons(0);
            BemaFI32.Bematech_FI_EspacoEntreLinhas(0);
            BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("C.Crédito", "1");
            BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("Cheque", "0");
            BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("Débito", "1");
            BemaFI32.Bematech_FI_ProgramaFormaPagamentoMFD("Pré-Pago", "0");
            BemaFI32.Bematech_FI_ProgramaArredondamento();

        }

        public void ShowMyDialogBox()
        {
            DialogoAbertura testDialog = new DialogoAbertura();

            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {

            }
            else
            {
                this.txtChequePrimeiro.Text = "Cancelled";
                this.txtChequeUltimo.Text = "Cancelled";
            }
            testDialog.Dispose();
        }

        private void btnFechamento_Click(object sender, EventArgs e)
        {
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
            btnAlterar.Enabled = true;
            btnSalvar.Enabled = false;
            pnlBasico.Enabled = false;
            pnlCredito.Enabled = false;
            pnlCheque.Enabled = false;

            if (validaCampos())
            {

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            string FormasPagamento = new string(' ',3017);
            BemaFI32.Analisa_iRetorno(BemaFI32.Bematech_FI_VerificaFormasPagamento(ref FormasPagamento));
        }

        private bool validaCampos()
        {
            return true;
        }
    }
}
