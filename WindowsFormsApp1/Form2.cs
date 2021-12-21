using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private MySqlConnection connection;

        public Form2(MySqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"insert into clients(name, surname, Cl_id, No_docs) values ('{this.textBox1.Text}', '{this.textBox2.Text}', '{this.textBox3.Text}', '{this.textBox4.Text}')";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Save Data");
                while (reader.Read())
                {
                }
                connection.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
