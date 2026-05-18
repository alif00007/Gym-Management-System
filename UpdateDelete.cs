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
    public partial class UpdateDelete : Form
    {
        public UpdateDelete()
        {
            InitializeComponent();
        }

        // DATABASE CONNECTION
        SqlConnection Con = new SqlConnection(
           @"Data Source=ALIF\SQLEXPRESS;
             Initial Catalog=GymDb;
             Integrated Security=True;
             TrustServerCertificate=True");

        // MEMBER KEY
        int key = 0;

        // LOAD DATA IN GRIDVIEW
        private void populate()
        {
            try
            {
                Con.Open();

                string query = "SELECT * FROM MemberTbl";

                SqlDataAdapter sda = new SqlDataAdapter(query, Con);

                DataSet ds = new DataSet();

                sda.Fill(ds);

                MemberDGV.DataSource = ds.Tables[0];

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

        // FORM LOAD
        private void UpdateDelete_Load(object sender, EventArgs e)
        {
            populate();

            PasswordTb.UseSystemPasswordChar = true;
        }

        // SHOW SELECTED ROW DATA
        private void MemberDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = MemberDGV.Rows[e.RowIndex];

                key = Convert.ToInt32(row.Cells[0].Value.ToString());

                NameTb.Text = row.Cells[1].Value.ToString();

                PhoneTb.Text = row.Cells[2].Value.ToString();

                GenderCB.Text = row.Cells[3].Value.ToString();

                AgeTb.Text = row.Cells[4].Value.ToString();

                AmountTb.Text = row.Cells[5].Value.ToString();

                TimingCB.Text = row.Cells[6].Value.ToString();

                // CLEAR PASSWORD FIELD
                PasswordTb.Text = "";
            }
        }

        // UPDATE BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            if (key == 0 ||
                NameTb.Text == "" ||
                PhoneTb.Text == "" ||
                AgeTb.Text == "" ||
                AmountTb.Text == "" ||
                GenderCB.Text == "" ||
                TimingCB.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query =
                        "UPDATE MemberTbl SET " +
                        "MName='" + NameTb.Text + "'," +
                        "MPhone='" + PhoneTb.Text + "'," +
                        "MGen='" + GenderCB.Text + "'," +
                        "MAge=" + AgeTb.Text + "," +
                        "MAmount=" + AmountTb.Text + "," +
                        "MTiming='" + TimingCB.Text + "' " +
                        "WHERE MID=" + key + ";";

                    SqlCommand cmd = new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Member Updated Successfully");

                    Con.Close();

                    populate();
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
            NameTb.Text = "";
            PhoneTb.Text = "";
            GenderCB.Text = "";
            AgeTb.Text = "";
            AmountTb.Text = "";
            TimingCB.Text = "";
            PasswordTb.Text = "";

            key = 0;
        }

        // DELETE BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Member To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query =
                        "DELETE FROM MemberTbl WHERE MID=" + key + ";";

                    SqlCommand cmd =
                        new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Member Deleted Successfully");

                    Con.Close();

                    populate();

                    // CLEAR FIELDS AFTER DELETE
                    NameTb.Text = "";
                    PhoneTb.Text = "";
                    GenderCB.Text = "";
                    AgeTb.Text = "";
                    AmountTb.Text = "";
                    TimingCB.Text = "";
                    PasswordTb.Text = "";

                    key = 0;
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

        // BACK BUTTON
        private void button4_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();

            mainform.Show();

            this.Hide();
        }

        // EXIT LABEL
        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // SHOW PASSWORD CHECKBOX
        private void ShowPassCb_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPassCb.Checked)
            {
                PasswordTb.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTb.UseSystemPasswordChar = true;
            }
        }

        // SET PASSWORD BUTTON
        private void SetPass_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select Member First");
            }
            else if (PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Password");
            }
            else
            {
                try
                {
                    Con.Open();

                    string query =
                        "UPDATE MemberTbl SET MPassword='" +
                        PasswordTb.Text +
                        "' WHERE MID=" + key;

                    SqlCommand cmd =
                        new SqlCommand(query, Con);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Password Updated Successfully");

                    Con.Close();

                    // CLEAR PASSWORD AFTER SAVING
                    PasswordTb.Text = "";
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

        // EMPTY EVENTS
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}