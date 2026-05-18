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
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(
           @"Data Source=ALIF\SQLEXPRESS;
             Initial Catalog=GymDb;
             Integrated Security=True");

        // Fill Member Names In ComboBox
        private void FillName()
        {
            Con.Open();

            SqlCommand cmd = new SqlCommand(
                "Select MName from MemberTbl", Con);

            SqlDataReader rdr;

            rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("MName", typeof(string));

            dt.Load(rdr);

            NameCB.ValueMember = "MName";
            NameCB.DataSource = dt;

            Con.Close();
        }
        private void filterbyName()
        {
            Con.Open();

            string query = "select * from PaymentTbl where PMember='"+SearchName.Text+"'";

            SqlDataAdapter sda = new SqlDataAdapter(query, Con);

            SqlCommandBuilder builder = new SqlCommandBuilder(sda);

            var ds = new DataSet();

            sda.Fill(ds);

            PaymentDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        // Show Payment Table Data
        private void populate()
        {
            Con.Open();

            string query = "select * from PaymentTbl";

            SqlDataAdapter sda = new SqlDataAdapter(query, Con);

            SqlCommandBuilder builder = new SqlCommandBuilder(sda);

            var ds = new DataSet();

            sda.Fill(ds);

            PaymentDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        // Reset Button
        private void button2_Click(object sender, EventArgs e)
        {
            NameCB.Text = "";
            AmountTb.Text = "";
        }

        // Back Button
        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();

            mainform.Show();

            this.Hide();
        }

        // Form Load
        private void Payment_Load(object sender, EventArgs e)
        {
            FillName();
            populate();
        }

        int key = 1;

        // Pay Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (NameCB.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Month-Year Format
                    string payperiode = periode.Value.ToString("MM-yyyy");

                    Con.Open();

                    // Check Existing Payment
                    string checkQuery =
                        "Select count(*) from PaymentTbl where PMember='"
                        +NameCB.Text
                        + "' AND PMonth='"
                        + payperiode + "'";

                    SqlDataAdapter sda =
                        new SqlDataAdapter(checkQuery, Con);

                    DataTable dt = new DataTable();

                    sda.Fill(dt);

                    // If Already Paid
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("Already Paid For This Month");
                    }
                    else
                    {
                        // Insert Payment
                        string query =
   "INSERT INTO PaymentTbl " +
   "(PMonth, PMember, PAmount, PaymentMethod, TransactionId, PaymentStatus) " +
   "VALUES('" +
   payperiode + "','" +
   NameCB.SelectedValue.ToString() + "'," +
   AmountTb.Text + ",'" +
   PaymentMethodCB.Text + "','" +
   TransactionIdTb.Text + "','" +
   StatusCB.Text + "')";

                        SqlCommand cmd =
                            new SqlCommand(query, Con);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Amount Paid Successfully");
                    }

                    Con.Close();

                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            filterbyName();
            SearchName.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}