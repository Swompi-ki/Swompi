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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            int Count = 0;
            string query = "select count(*) from user where login ='" + logBox.Text + "' and password = '" + pasBox.Text + "';";
            MySqlConnection conn = DBUtils.GetDBConnection();
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.CommandTimeout = 60;
            try
            {
                conn.Open();
                Count = Convert.ToInt32(cmDB.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка авторизации. Попробуйте еще раз.");
                MessageBox.Show(ex.Message);
            }
            if (Count > 0)
            {
                Item Win = new Item();
                Win.Show();
                this.Hide();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
