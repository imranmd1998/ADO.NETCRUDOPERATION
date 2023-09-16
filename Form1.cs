using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace Ado.netRevision
{
    public partial class Form1 : Form
    {
        string Constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string getAllEmployeesq;

        public Form1()
        {
            InitializeComponent();
        }


        private void btnselectemployee_Click(object sender, EventArgs e)
        {
            string SqlQuery = "SELECT *FROM [dbo].[Employee] WHERE Id=@ID";
            SqlConnection Con = new SqlConnection(Constr);
            try
            {
                if(!string.IsNullOrEmpty(textBox4.Text))
                //connection string
                {
                    SqlCommand cmd = new SqlCommand(SqlQuery, Con);
                    cmd.Parameters.AddWithValue("@ID", textBox4.Text);
                    Con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox1.Text = reader.GetString(1).ToString();
                        textBox2.Text = reader.GetInt32(2).ToString();
                        textBox3.Text = reader.GetInt32(3).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("emoployee id cannot be left blank..");
                }
               
                //command
               
            }

            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { Con.Close(); }


        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            // ExecuteNonQuery

            SqlConnection Con = new SqlConnection(Constr);
            try
            {
                string addEmployeesql = @"INSERT INTO [dbo].[Employee] 
                                    VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                SqlCommand cmd = new SqlCommand(addEmployeesql, Con);
                Con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Inserted Successfully");
                    FillDataGridView();

                }
                else
                {
                    MessageBox.Show("Record does not  Inserted Successfully");

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

        private void btnUpdate_Click(object sender, EventArgs e)
        { 
            SqlConnection Con=new SqlConnection(Constr);
            try
            {
                string updateEmployeesql = @"Update [dbo].[Employee] 
                                            SET  [Ename]=@Ename,
                                            [Age]=@Age,
                                            [Salary]=@Salary WHERE Id=@ID";                        
                SqlCommand cmd = new SqlCommand(updateEmployeesql, Con);
                cmd.Parameters.AddWithValue("@Ename", textBox1.Text);
                cmd.Parameters.AddWithValue("@Age", textBox2.Text);
                cmd.Parameters.AddWithValue("@Salary", textBox3.Text);
                cmd.Parameters.AddWithValue("@Id", textBox4.Text);
                Con.Open();
                 int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected > 0)
                {
                    MessageBox.Show("record updated successfully");
                    FillDataGridView();
                }
                else
                {
                    MessageBox.Show("record was not  updated");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("ex.message");
            }
            finally
            {
                Con.Close();
            }

        }

       
        private void FillDataGridView()
        {
            SqlConnection con = new SqlConnection(Constr);
            //Data set
            try
            {
                //string getAllEmployeeSql = "SELECT *FROM  Employee";
                //SqlDataAdapter da = new SqlDataAdapter(getAllEmployeeSql, Constr);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //dataGridView1.DataSource = ds.Tables[0];
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                string getAllEmployeesql = "[dbo].[getEmployee]";
                SqlCommand cmd = new SqlCommand(getAllEmployeesq,con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = getAllEmployeesql;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection(Constr);
            try
            {
                string deleteEmployeesql = @"Delete [dbo].[Employee] 
                                             WHERE Id=@ID";
                SqlCommand cmd = new SqlCommand(deleteEmployeesql, Con); 
                cmd.Parameters.AddWithValue("@Id", textBox4.Text);
                Con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("record Deleted successfully");
                    FillDataGridView();
                }
                else
                {
                    MessageBox.Show("record was not  Deleted");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ex.message");
            }
            finally
            {
                Con.Close();
            }
        }
    }
}
