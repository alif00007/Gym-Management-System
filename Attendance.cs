using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class Attendance : Form
    {
        public Attendance()
        {
            InitializeComponent();
        }

        // DATABASE CONNECTION
        SqlConnection Con = new SqlConnection(
            @"Data Source=ALIF\SQLEXPRESS;
              Initial Catalog=GymDb;
              Integrated Security=True;
              TrustServerCertificate=True");

        // LOAD MEMBER NAMES
        private void FillMembers()
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                string query;

                // MEMBER CAN SEE ONLY OWN NAME
                if (Login.UserRole == "Member")
                {
                    query =
                    @"SELECT MName
                      FROM MemberTbl
                      WHERE MName=@Name";
                }
                else
                {
                    // ADMIN CAN SEE ALL MEMBERS
                    query =
                    @"SELECT MName
                      FROM MemberTbl";
                }

                SqlCommand cmd =
                    new SqlCommand(query, Con);

                // PASS LOGGED USER
                if (Login.UserRole == "Member")
                {
                    cmd.Parameters.AddWithValue(
                        "@Name",
                        Login.LoggedUser);
                }

                SqlDataReader rdr =
                    cmd.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Columns.Add("MName", typeof(string));

                dt.Load(rdr);

                // COMBOBOX SETTINGS
                MemberCb.DisplayMember = "MName";

                MemberCb.ValueMember = "MName";

                MemberCb.DataSource = dt;

                // MEMBER AUTO SELECT
                if (Login.UserRole == "Member")
                {
                    MemberCb.SelectedIndex = 0;

                    MemberCb.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        // SHOW ATTENDANCE
        private void populate()
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                string query;

                // MEMBER CAN SEE ONLY OWN ATTENDANCE
                if (Login.UserRole == "Member")
                {
                    query =
                    @"SELECT *
                      FROM AttendanceTbl
                      WHERE MemberName=@Name";
                }
                else
                {
                    // ADMIN CAN SEE ALL
                    query =
                    @"SELECT *
                      FROM AttendanceTbl";
                }

                SqlDataAdapter sda =
                    new SqlDataAdapter(query, Con);

                // PASS LOGGED USER
                if (Login.UserRole == "Member")
                {
                    sda.SelectCommand.Parameters.AddWithValue(
                        "@Name",
                        Login.LoggedUser);
                }

                DataTable dt = new DataTable();

                sda.Fill(dt);

                AttendanceDGV.DataSource = dt;

                // GRID DESIGN
                AttendanceDGV.BorderStyle =
                    BorderStyle.None;

                AttendanceDGV.BackgroundColor =
                    Color.White;

                AttendanceDGV.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                AttendanceDGV.RowHeadersVisible = false;

                AttendanceDGV.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                AttendanceDGV.AllowUserToAddRows = false;

                AttendanceDGV.EnableHeadersVisualStyles = false;

                // HEADER STYLE
                AttendanceDGV.ColumnHeadersDefaultCellStyle.BackColor =
                    Color.Crimson;

                AttendanceDGV.ColumnHeadersDefaultCellStyle.ForeColor =
                    Color.White;

                AttendanceDGV.ColumnHeadersDefaultCellStyle.Font =
                    new Font("Century Gothic", 11, FontStyle.Bold);

                // CELL STYLE
                AttendanceDGV.DefaultCellStyle.BackColor =
                    Color.White;

                AttendanceDGV.DefaultCellStyle.ForeColor =
                    Color.Black;

                AttendanceDGV.DefaultCellStyle.SelectionBackColor =
                    Color.DodgerBlue;

                AttendanceDGV.DefaultCellStyle.SelectionForeColor =
                    Color.White;

                AttendanceDGV.DefaultCellStyle.Font =
                    new Font("Century Gothic", 10);

                AttendanceDGV.RowTemplate.Height = 30;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        // FORM LOAD
        private void Attendance_Load(
            object sender,
            EventArgs e)
        {
            FillMembers();

            populate();

            // FORM COLOR
            this.BackColor = Color.WhiteSmoke;

            // BUTTON DESIGN
            btnCheckIn.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnBack.FlatStyle = FlatStyle.Flat;

            btnCheckIn.BackColor = Color.Crimson;
            btnRefresh.BackColor = Color.Crimson;
            btnBack.BackColor = Color.Crimson;

            btnCheckIn.ForeColor = Color.White;
            btnRefresh.ForeColor = Color.White;
            btnBack.ForeColor = Color.White;

            // COMBOBOX DESIGN
            MemberCb.Font =
                new Font("Century Gothic", 11);

            MemberCb.ForeColor =
                Color.Black;

            MemberCb.BackColor =
                Color.White;
        }

        // CHECK IN BUTTON
        private void btnCheckIn_Click(
            object sender,
            EventArgs e)
        {
            if (MemberCb.Text == "")
            {
                MessageBox.Show(
                    "Select Member");
            }
            else
            {
                try
                {
                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }

                    // CHECK DUPLICATE ENTRY
                    string checkQuery =
                    @"SELECT COUNT(*)
                      FROM AttendanceTbl
                      WHERE MemberName=@Name
                      AND AttendDate=@Date";

                    SqlCommand checkCmd =
                        new SqlCommand(checkQuery, Con);

                    checkCmd.Parameters.AddWithValue(
                        "@Name",
                        MemberCb.Text);

                    checkCmd.Parameters.AddWithValue(
                        "@Date",
                        DateTime.Today);

                    int count =
                        Convert.ToInt32(
                            checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show(
                            "Already Checked In Today");
                    }
                    else
                    {
                        string query =
                        @"INSERT INTO AttendanceTbl
                        (
                            MemberName,
                            AttendDate
                        )
                        VALUES
                        (
                            @Name,
                            @Date
                        )";

                        SqlCommand cmd =
                            new SqlCommand(query, Con);

                        cmd.Parameters.AddWithValue(
                            "@Name",
                            MemberCb.Text);

                        cmd.Parameters.AddWithValue(
                            "@Date",
                            DateTime.Today);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "Attendance Added Successfully");

                        populate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        // REFRESH BUTTON
        private void btnRefresh_Click(
            object sender,
            EventArgs e)
        {
            populate();

            MessageBox.Show(
                "Attendance Refreshed");
        }

        // BACK BUTTON
        private void btnBack_Click(
            object sender,
            EventArgs e)
        {
            MainForm mainform =
                new MainForm();

            mainform.Show();

            this.Hide();
        }

        // EXIT LABEL
        private void label9_Click(
            object sender,
            EventArgs e)
        {
            Application.Exit();
        }
    }
}