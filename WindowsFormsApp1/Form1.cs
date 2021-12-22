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
    public partial class Form1 : Form
    {
        private readonly string server = "localhost";
        private readonly string database = "test";
        private readonly string uid = "root";
        private readonly string password = "";
        private string table = "";
        private MySqlConnection connection;
        private MySqlDataAdapter mySqlDataAdapter;

        public Form1()
        {
            InitializeComponent();
            connection = new MySqlConnection($"server={server};database={database};uid={uid};password={password}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter($"select * from {table}", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                dataGridView1.DataSource = DS.Tables[0];

                this.CloseConnection();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.GetItemText(comboBox1.SelectedItem);
            table = selectedItem;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable changes = ((DataTable)dataGridView1.DataSource).GetChanges();

            if (changes != null)
            {
                MySqlCommandBuilder mcb = new MySqlCommandBuilder(mySqlDataAdapter);
                mySqlDataAdapter.UpdateCommand = mcb.GetUpdateCommand();
                mySqlDataAdapter.Update(changes);
                ((DataTable)dataGridView1.DataSource).AcceptChanges();
            }
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            } catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            } catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (table == "clients")
            {
                try
                {
                    connection.Open();
                    string name = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    string Cl_id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    string query = $"DELETE FROM clients WHERE Cl_id={Cl_id}";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (table == "abonement")
            {
                Form3 form3 = new Form3(this.connection, "add");
                form3.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (table == "clients")
            {
                Form2 form2 = new Form2(this.connection, "update");
                form2.ShowDialog();
            }
            else if (table == "abonement")
            {
                Form3 form3 = new Form3(this.connection, "update");
                form3.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteFromTable(DeleteQuery());
        }

        private string DeleteQuery()
        {
            switch (table)
            {
                case "clients":
                    string Cl_id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    return $"DELETE FROM clients WHERE Cl_id={Cl_id}"; ;
                case "abonement":
                    string type = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    return $"DELETE FROM abonement WHERE type='{type}'";
                default:
                    return "";
            }
        }

        private void DeleteFromTable(string query)
        {
            try
            {
                connection.Open();
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}
