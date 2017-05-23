using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loja1._0.View
{
    public partial class Abertura : Form
    {
        public Abertura()
        {            
            var t = Task.Run(async delegate
            {
                InitializeComponent();
                await Task.Delay(4000);
                this.Hide();
            });
        }
    }
}
