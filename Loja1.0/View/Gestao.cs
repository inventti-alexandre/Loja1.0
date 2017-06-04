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

namespace Loja1._0
{
    public partial class Gestao : Form
    {
        private Model.Usuarios user;

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

        private void button1_Click(object sender, EventArgs e)
        {
            //Configuração inicial Impressora fiscal
            BemaFI32.Bematech_FI_AlteraSimboloMoeda("R");
            BemaFI32.Bematech_FI_ProgramaAliquota("10,38", 0);            
            BemaFI32.Bematech_FI_LinhasEntreCupons(1);
            BemaFI32.Bematech_FI_EspacoEntreLinhas(5);
            BemaFI32.Bematech_FI_ProgramaArredondamento();
        }
    }
}
