using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataFrut.Clases;

namespace DataFrut
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if(Conexion.conectar(txtUser.Text, txtPass.Text))
            {
                this.Hide();
                MainDataFrut mDataFrut = new MainDataFrut();
                mDataFrut.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
