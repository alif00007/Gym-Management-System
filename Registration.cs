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
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(
           @"Data Source=ALIF\SQLEXPRESS;
              Initial Catalog=GymDb;
              Integrated Security=True;
              TrustServerCertificate=True");


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            if (NameTb.Text == "" ||
                PhoneTb.Text == "" ||
                PasswordTb.Text == "" ||
                ConfirmPasswordTb.Text == "" ||
                GenderCb.Text == "" ||
                MembershipCb.Text == "" ||
                AgeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }

            // PASSWORD MATCH CHECK
            else if (PasswordTb.Text != ConfirmPasswordTb.Text)
            {
                MessageBox.Show("Passwords Do Not Match");
            }

            else
            {
                try
                {
                    Con.Open();

                    // CHECK EXISTING PHONE NUMBER
                    string checkQuery =
                        "SELECT COUNT(*) FROM MemberTbl WHERE MPhone='" +
                        PhoneTb.Text + "'";

                    SqlDataAdapter sda =
                        new SqlDataAdapter(checkQuery, Con);

                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("Phone Number Already Exists");
                    }
                    else
                    {
                        // INSERT DATA
                        string query =
                            "INSERT INTO MemberTbl " +
                            "(MName, MPhone, MPassword, MGen, MTiming, MAge, MAmount) " +
                            "VALUES('" +
                            NameTb.Text + "','" +
                            PhoneTb.Text + "','" +
                            PasswordTb.Text + "','" +
                            GenderCb.SelectedItem.ToString() + "','" +
                            MembershipCb.SelectedItem.ToString() + "'," +
                            AgeTb.Text + ",0)";

                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Registration Successful");

                        Con.Close();

                        // OPEN LOGIN FORM
                        Login log = new Login();
                        log.Show();
                        this.Hide();
                    }
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

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegisterTb_Click(object sender, EventArgs e)
        {

        }
    }
}
