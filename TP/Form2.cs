using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP
{
    public partial class Form2 : Form
    {
        public ComputerGame game { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game = new ComputerGame { Name = textBox1.Text, Genre = textBox2.Text, Level = textBox3.Text, Mark = textBox4.Text };
            DialogResult = DialogResult.OK;
            Close();

        }
    }
}
