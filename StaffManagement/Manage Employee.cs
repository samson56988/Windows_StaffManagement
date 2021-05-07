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

namespace StaffManagement
{
    public partial class Manage_Employee : Form
    {
        public Manage_Employee()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-J3DHBNP\SQLEXPRESS;Initial Catalog=Staffmanagement;Integrated Security=True");
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtempID.Text == "" || txtempname.Text == "" || Txt_Phone.Text == "" || txtempaddress.Text == "")
            {

                MessageBox.Show("Missing Information");

            }
            else
            {

                try
                {
                    Con.Open();

                    string query = "insert into Employee values('" + txtempID.Text + "','" + txtempname.Text + "','" + txtempaddress.Text + "','" + employeePosition.SelectedItem.ToString() + "','" + DOB.Value.Date + "','" + Txt_Phone.Text + "','" + Employee_EDUCATION.SelectedItem.ToString() + "','" + employeegender.SelectedItem.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added Successfully");

                    Con.Close();
                    populate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void populate()
        {

            Con.Open();
            string query = "Select * from Employee";

            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDT.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void Manage_Employee_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void EmployeeDT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtempID.Text = EmployeeDT.SelectedRows[0].Cells[0].Value.ToString();
            txtempname.Text = EmployeeDT.SelectedRows[0].Cells[1].Value.ToString();
            txtempaddress.Text = EmployeeDT.SelectedRows[0].Cells[2].Value.ToString();

            employeePosition.Text = EmployeeDT.SelectedRows[0].Cells[3].Value.ToString();
            Txt_Phone.Text = EmployeeDT.SelectedRows[0].Cells[5].Value.ToString();
            Employee_EDUCATION.Text = EmployeeDT.SelectedRows[0].Cells[6].Value.ToString();
            employeegender.Text = EmployeeDT.SelectedRows[0].Cells[7].Value.ToString();
        }
    }
}
