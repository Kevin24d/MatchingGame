using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame.VentanaExtra
{
    public partial class VentanaCreditos : Form
    {
        public VentanaCreditos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            home.Show();
            Hide();
        }

        private void VentanaCreditos_Load(object sender, EventArgs e)
        {
            string videour1 = "https://drive.google.com/file/d/1Oc5zCt0iP-ro3kIQs7jWOdboxkYYgEM7/view?usp=share_link";
            webBrowser1.Navigate(videour1);
        }
    }
}
