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
    public partial class Item : Form
    {
        // Главное меню, сзаполнениями данными в listView1 автоматически
        void get_Info(ListView List)
        {
            string query = "select show_times.name, show_times.date_and_time, hall.name from show_times join hall ON show_times.ID = hall.ID";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        string[] row = { rd.GetString(0), rd.GetString(1), rd.GetString(2)};
                        var listItem = new ListViewItem(row);
                        List.Items.Add(listItem);
                    }
                }
                List.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Item()
        {
            InitializeComponent();
            get_Info(list);
            this.FormClosing += Items_FormClosing;
        }

        private void Items_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            list.Items.Clear();
            get_Info(list);
        }

        private void add_item_Click(object sender, EventArgs e)
        {
            AddItemWin Win = new AddItemWin();
            this.Hide();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            AddItemWin Win = new AddItemWin();
            this.Hide();
        }

        private void Del_Click(object sender, EventArgs e)
        {
            string query = "delete from articles where show_times = " + list.Items[list.SelectedIndices[0]].Text + ";";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }


}

