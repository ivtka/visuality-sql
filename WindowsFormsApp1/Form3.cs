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
    public partial class Form3 : Form
    {
        private MySqlConnection connection;
        private string operation;
        private Dictionary<string, Action> operations;
        public Form3(MySqlConnection connection, string operation)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
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
                string query = $"insert into abonement(type, price, ups) values ('{this.comboBox1.GetItemText(comboBox1.SelectedItem)}', '{this.textBox1.Text}', '{this.textBox2.Text}')";
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
                string query = $"update abonement set price='{this.textBox2.Text}', ups='{this.textBox1.Text}' where type='{this.comboBox1.GetItemText(comboBox1.SelectedItem)}'";
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

        void delete_to_db()
        {
            try
            {
                string query = $"delete from abonement where type='{this.comboBox1.GetItemText(comboBox1.SelectedItem)}'";
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
