using System;
/*using System.Collections.Generic;
using System.ComponentModel;*/
using System.Data;
/*using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HuynhPhuocTruongK60_6051071134
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-FTHMR7QM\SQLEXPRESS;
                                                    Initial Catalog=DemoCRUD;
                                                    Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            StudentRecordData.DataSource = dt;
        }

        private bool IsValidData()
        {
            if(TxHName.Text == string.Empty || TxtHName.Text == string.Empty || TxtAdress.Text == string.Empty || string.IsNullOrEmpty(TxtPhone.Text) || string.IsNullOrEmpty(TxtRoll.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!", "Lỗi dữ liệu", MessageBoxDefaultButton.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("IMSERT INTO StudentsTb VALUES" + "(@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                const.Close();
                GetStudentsRecord();
            }
        }

        public int StudentID;
        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs)
        {
            StudentID = Convert.ToInt32(StudentRecordData_CellClick.Rows[0].Cell[0].Value);
            TxtHName.Text = StudentRecordData.SelectedRow[0].Cells[1].Value.ToString();
            TxtHName.Text = StudentRecordData.SelectedRow[0].Cells[2].Value.ToString();
            TxtHName.Text = StudentRecordData.SelectedRow[0].Cells[3].Value.ToString();
            TxtHName.Text = StudentRecordData.SelectedRow[0].Cells[4].Value.ToString();
            TxtHName.Text = StudentRecordData.SelectedRow[0].Cells[5].Value.ToString();
        }

        private void UpdateButton_Click(object ssender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET" + "Name = @Name, FatherName = @FatherName, " + "RollNumber = @RollNumber, Address = @Address, " + "Mobile = @Mobile WHERE StudentID = @ID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                const.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
                ResetData();

            }
            else
            {
                MessageBox.Show("Cập nhập bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                const.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhập bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /* private void Form1_Load(object sender, EventArgs e)
         {

         }

         private void button1_Click(object sender, EventArgs e)
         {

         }

         private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {

         }*/
    }
}
