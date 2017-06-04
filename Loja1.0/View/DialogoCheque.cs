using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0.View
{
    public partial class DialogoCheque : Form
    {
        public TextBox TextBox1 = new TextBox();
        public TextBox TextBox2 = new TextBox();

        public DialogoCheque()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtChequePrimeiro.Equals(""))
            {
                MessageBox.Show("É necessário o preenchimento da numeração do primeiro cheque, e recomendável a do último, caso exista","Ação Inválida");
            }
            else
            {
                Caixa.chequePrim = txtChequePrimeiro.Text;
                Caixa.chequeUlt = txtChequeUltimo.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
