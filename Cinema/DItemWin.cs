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

namespace Cinema
{
    public partial class DItemWin : Form
    {
        public string modeS = "";
        int item;
        void setMode(string mode)
        {
            if (mode == "add")
            {
                Del.Text = "Добавить";
            }
            else if (mode == "change")
            {
                Del.Text = "Изменить";
                string Info = "select name, date_and_time  from show_times where date_and_time =" + item.ToString() + ";";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmInfo = new MySqlCommand(Info, conn);
                MySqlDataReader inRead;
                cmInfo.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    inRead = cmInfo.ExecuteReader();
                    if (inRead.HasRows)
                    {
                        while (inRead.Read())
                        {
                            textBox1.Text = inRead.GetString(0);
                            textBox2.Text = inRead.GetString(1);
                            textBox3.Text = inRead.GetString(2);
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void getNames(ComboBox Box)
        {
            string query = "select date_and_time from show_times;";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            MySqlDataReader rd;
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        string row = rd.GetString(0);
                        Box.Items.Add(row);
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DItemWin(string mode, int id)
        {
            InitializeComponent();
            getNames(textBox2);
            modeS = mode;
            item = id;
            setMode(mode);
        }

        private void getNames(TextBox textBox2)
        {
            throw new NotImplementedException();
        }

        private void Del_Click(object sender, EventArgs e)
        {
           
                string query = "insert into show_times(name, date_and_time) values('" + textBox1.Text + "', '" + textBox2.Text + "');";
                MySqlConnection conn = DBUtils.GetDBConnection();
                MySqlCommand cmDB = new MySqlCommand(query, conn);
                cmDB.CommandTimeout = 60;
                try
                {
                    conn.Open();
                    MySqlDataReader rd = cmDB.ExecuteReader();
                    conn.Close();

                    string queryF = "insert into hall(name) values('" + textBox3.Text + "');";
                    MySqlCommand cmDBF = new MySqlCommand(queryF, conn);
                    try
                    {
                        conn.Open();
                        MySqlDataReader rdF = cmDBF.ExecuteReader();
                        conn.Close();
                        Item Win = new Item();
                        Win.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
        }
        private void back_Click(object sender, EventArgs e)
        {
            Item Win = new Item();
            Win.Show();
            this.Hide();
        }

    }
}

           