using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gym_Management_System
{
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();
        }

        // Correct Connection String
        SqlConnection Con = new SqlConnection(
            @"Data Source=ALIF\SQLEXPRESS;
              Initial Catalog=GymDb;
              Integrated Security=True");

        private void AddMember_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check empty fields
            if (NameTb.Text == "" || PhoneTb.Text == "" ||
                AmountTb.Text == "" || AgeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();

                    // Correct SQL Query
                    String query = "INSERT INTO MemberTbl VALUES('"
                                   + NameTb.Text + "','"
                                   + PhoneTb.Text + "','"
                                   + GenderCb.SelectedItem.ToString() + "',"
                                   + AgeTb.Text + ","
                                   + AmountTb.Text + ",'"
                                   + TimingCb.SelectedItem.ToString() + "')";

                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Member Successfully Added");

                    Con.Close();
                    AmountTb.Text = "";
                    AgeTb.Text = "";
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCb.Text = "";
                    TimingCb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AmountTb.Text = "";
            AgeTb.Text = "";
            NameTb.Text = "";
            PhoneTb.Text = "";
            GenderCb.Text= "";
            TimingCb.Text = "";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }
    }
}