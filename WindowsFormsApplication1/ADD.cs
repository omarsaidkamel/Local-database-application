using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ADD : Form
    { 
        SqlConnection cn =new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\database\Omar DB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        SqlCommand cmd;
        string imgloc;
        public ADD()
        {
           InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && comboBox1.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("You Must Fill Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show("You Sure You Want To Add !", "Adding", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    try
                    {
                        byte[] img = null;
                        FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);
                        string sql = "INSERT INTO Table4  ([MemberShip No.], Description, NAME, [National ID], [Call Sign], Address, [MemberShip Date], [Member Photo],[Membership fees]) VALUES ('" + textBox1.Text + "', '" + comboBox1.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "',@img,'" + textBox2.Text + "')";
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                            cmd = new SqlCommand(sql, cn);
                            cmd.Parameters.Add(new SqlParameter("@img", img));
                            cmd.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Added sucssefully", "Adding", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox1.Clear(); comboBox1.Text = null; textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
                            pictureBox1.Image = null; textBox2.Clear();
                        }
                    }
                    catch
                    {
                        string sql = "INSERT INTO Table4  ([MemberShip No.], Description, NAME, [National ID], [Call Sign], Address, [MemberShip Date],[Membership fees]) VALUES ('" + textBox1.Text + "', '" + comboBox1.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "','" + textBox2.Text + "')";
                        if (cn.State != ConnectionState.Open)
                        {
                            cn.Open();
                            cmd = new SqlCommand(sql, cn);
                            cmd.ExecuteNonQuery();
                            cn.Close();
                            MessageBox.Show("Added sucssefully", "Adding", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox2.Clear(); textBox1.Clear(); comboBox1.Text = null; textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
                        }

                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter="JPG Files(*.JPG|*.JPG|GIF Files(*.gif)|*.gif | All Files(*.*)|*.*";
            openFileDialog1.Title=("Select Member Picture");
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            imgloc = openFileDialog1.FileName;
            pictureBox1.ImageLocation = imgloc;
        }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
