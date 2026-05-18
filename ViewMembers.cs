using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class ViewMembers : Form
    {
        public ViewMembers()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(
            @"Data Source=ALIF\SQLEXPRESS;
              Initial Catalog=GymDb;
              Integrated Security=True");
        private void populate()
        {
            Con.Open();
            string query = "select * from MemberTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MemberDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ViewMembers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void filterbyName()
        {
            Con.Open();

            string query = "select * from MemberTbl where MName='" + SearchMember.Text + "'";

            SqlDataAdapter sda = new SqlDataAdapter(query, Con);

            SqlCommandBuilder builder = new SqlCommandBuilder(sda);

            var ds = new DataSet();

            sda.Fill(ds);

            MemberDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            filterbyName();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
