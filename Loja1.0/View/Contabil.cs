﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Loja1._0.Model;

namespace Loja1._0
{
    public partial class Contabil : Form
    {
        private Model.Usuarios user;

        public Contabil(Model.Usuarios user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
