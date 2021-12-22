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
        private string operation;
        private Dictionary<string, Action> operations;

        public Form2(MySqlConnection connection, string operation)
        {
            InitializeComponent();
            this.connection = connection;
            this.operation = operation;
            operations = new Dictionary<string, Action>
            {
                { "add", add_to_db },
                { "update", update_to_db },
                { "delete", delete_to_db }
            };
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
            operations[operation]();
        }

        void add_to_db()
        {
            try
            {
                string query = $"insert into clients(name, surname, Cl_id, No_docs) values ('{this.textBox1.Text}', '{this.textBox2.Text}', '{this.textBox3.Text}', '{this.textBox4.Text}')";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Inserted Data");
                while (reader.Read())
                {
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void update_to_db()
        {
            try
            {
                string query = $"update clients set name='{this.textBox1.Text}', surname='{this.textBox2.Text}', Cl_id='{this.textBox3.Text}', No_docs='{this.textBox4.Text}' where Cl_id='{this.textBox3.Text}'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Updated Data");
                while (reader.Read()) { }
                connection.Close();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void delete_to_db()
        {
            try
            {
                string query = $"delete from clients where Cl_id='{this.textBox3.Text}'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                MessageBox.Show("Updated Data");
                while (reader.Read()) { }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
