using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // DATABASE CONNECTION
        SqlConnection Con = new SqlConnection(
           @"Data Source=ALIF\SQLEXPRESS;
             Initial Catalog=GymDb;
             Integrated Security=True;
             TrustServerCertificate=True");

        // LOGIN BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            // CHECK EMPTY
            if (UidTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }

            // ADMIN LOGIN
            else if (UidTb.Text == "Admin" && PassTb.Text == "Admin")
            {
                MainForm mainform = new MainForm();
                mainform.Show();
                this.Hide();
            }

            // MEMBER LOGIN
            else
            {
                try
                {
                    Con.Open();

                    string query =
                        "SELECT COUNT(*) FROM MemberTbl " +
                        "WHERE MName='" + UidTb.Text +
                        "' AND MPassword='" + PassTb.Text + "'";

                    SqlDataAdapter sda =
                        new SqlDataAdapter(query, Con);

                    DataTable dt = new DataTable();

                    sda.Fill(dt);

                    // LOGIN SUCCESS
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MainForm mainform = new MainForm();
                        mainform.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Username Or Password");
                    }

                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        // RESET BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            UidTb.Text = "";
            PassTb.Text = "";
        }

        // EXIT LABEL
        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // SHOW PASSWORD CHECKBOX
        private void ShowPassCb_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPassCb.Checked)
            {
                PassTb.UseSystemPasswordChar = false;
            }
            else
            {
                PassTb.UseSystemPasswordChar = true;
            }
        }

        // FORM LOAD
        private void Login_Load(object sender, EventArgs e)
        {
            PassTb.UseSystemPasswordChar = true;
        }

        // REGISTER BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            this.Hide();
        }

        // EMPTY EVENTS (TO REMOVE ERRORS)
        private void PassTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}