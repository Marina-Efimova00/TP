using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace TP
{
    public partial class Form1 : Form
    {
        List<ComputerGame> games = new List<ComputerGame>();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                games.Add(form.game);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = games;
            }
        }

        private void buttonLoadXML_Click(object sender, EventArgs e)
        {
            loadFromFile("test.txt");

        }
        void loadFromFile(string path)
        {
            dataGridView1.Columns.Clear();
            XmlSerializer serializer = new XmlSerializer(typeof(List<ComputerGame>));
            using (Stream fStream = new FileStream(path, FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(fStream))
                {
                    var buffGame = (List<ComputerGame>)serializer.Deserialize(reader);
                    if (buffGame != null)
                    {
                        games = buffGame;
                        dataGridView1.DataSource = games;
                        MessageBox.Show("Загружено");
                    }
                    else
                    {
                        Console.WriteLine("Ошибочка");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveToFile("test.txt");
        }
        void saveToFile(string path)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<ComputerGame>));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, games);
                MessageBox.Show("Сохранено");
            }
        }

       

        private void buttonRequest_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            List<ComputerGame> reguest = (from g in games where g.Genre == textBox1.Text select g).ToList();
            dataGridView1.DataSource = reguest;
        }

        private void buttonGroupLambda_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            List<ComputerGame> request = games.Where(g => g.Genre == textBox1.Text).OrderBy(g => g.Name).ToList(); ;
            dataGridView1.DataSource = request;
        }

        private void buttonGroup_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            var request = games.GroupBy(g => g.Genre);
            dataGridView1.DataSource = request;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Add("Genre", "Genre");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Level", "Level");
            dataGridView1.Columns.Add("Mark", "Mark");
            foreach (IGrouping<string, ComputerGame> reg in request)
            {
                Console.WriteLine(reg.Key);
                dataGridView1.Rows.Add(reg.Key);
                foreach (var r in reg)
                {
                    dataGridView1.Rows.Add(r.Genre, r.Name, r.Level, r.Mark);
                }
            }
        }

        private void buttonGenre_Click(object sender, EventArgs e)
        {
            var res = games
              .GroupBy(x => x.Genre)
              .Select(x => new
              {
                  NameG = x.Key,
                  Count = x.Count()
              });
            textBox2.Text = res.Single(x => x.Count == res.Max(y => y.Count)).NameG.ToString();
        }
    }
}
