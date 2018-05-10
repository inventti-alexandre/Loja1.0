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
    public partial class DialogoAbertura : Form
    {
        public TextBox TextBox1 = new TextBox();
        public TextBox TextBox2 = new TextBox();

        public DialogoAbertura()
        {
            InitializeComponent();
            txtValor.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal result = 0.00M;

            if (txtValor.Equals(""))
            {
                MessageBox.Show("É necessário o preenchimento do valor existênte no caixa no momento da abertura, caso ñão haja numerário no caixa digite \"0\"","Ação Inválida");
            }
            else if (!decimal.TryParse(txtValor.Text, out result))
            {
                MessageBox.Show("Para o preenchimnto do troco , utilize o formato decimal, \"XXX,XX\" ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Gestao.valorAbertura = txtValor.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
