using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{ 
    public partial class search : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\database\OmarDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        SqlCommand cmd;
        SqlDataReader dr;
        string imgloc;      
        byte[] img;
        public search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT Table4.*, [MemberShip No.]FROM Table4 WHERE  ([MemberShip No.] = '"+textBox1.Text+"')";
            if (textBox1.Text == "") { MessageBox.Show("You Must Fill The Data.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if(cn.State!=ConnectionState.Open)
                cmd = new SqlCommand(sql, cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                textBox2.Text = dr[0].ToString();
                comboBox1.Text = dr[1].ToString();
                textBox4.Text = dr[2].ToString();
                textBox5.Text = dr[3].ToString();
                textBox6.Text = dr[4].ToString();
                textBox7.Text = dr[5].ToString();
                textBox8.Text = dr[6].ToString();
                textBox3.Text = dr[8].ToString();
                try
                {  
                    img = (byte[])(dr[7]);
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                    cn.Close();
                    dr.Close();
                }
                catch
                {
                  
                    pictureBox1.Image = null;
                        cn.Close();
                        dr.Close();
                }
               cn.Close();
               dr.Close();
                }
                 else
                 {
                  MessageBox.Show("This Data isn't Exist !","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                  textBox1.Clear();
                 cn.Close();
                 dr.Close();
                }

            }
        }        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    cmd = new SqlCommand("UPDATE Table4 SET [MemberShip No.] ='"+textBox2.Text+"', Description ='"+comboBox1.Text+"', [National ID] ='"+textBox5.Text+"', NAME ='"+textBox4.Text+"', [Call Sign] ='"+textBox6.Text+"', Address ='"+textBox7.Text+"', [MemberShip Date] ='"+textBox8.Text+"', [Member Photo] =@img, [Membership fees] ='"+textBox3.Text+"'WHERE  ([MemberShip No.] = '"+textBox1.Text+"')", cn);
                    cmd.Parameters.Add(new SqlParameter("@img", img));
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Update Succsefully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear(); textBox2.Clear(); comboBox1.Text = null; textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); ; pictureBox1.Image = null; textBox3.Clear();
                }
            }
            catch
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    cmd = new SqlCommand("UPDATE Table4 SET [MemberShip No.] ='" + textBox2.Text + "', Description ='" + comboBox1.Text + "', [National ID] ='" + textBox5.Text + "', NAME ='" + textBox4.Text + "', [Call Sign] ='" + textBox6.Text + "', Address ='" + textBox7.Text + "', [MemberShip Date] ='" + textBox8.Text + "',[Membership fees] ='" + textBox3.Text + "'WHERE  ([MemberShip No.] = '" + textBox1.Text + "')", cn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Update Succsefully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear(); textBox2.Clear(); comboBox1.Text = null; textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox3.Clear() ;

                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG Files(*.JPG|*.JPG|GIF Files(*.gif)|*.gif | All Files(*.*)|*.*";
            openFileDialog1.Title = ("Select Member Picture");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imgloc = openFileDialog1.FileName;
                pictureBox1.ImageLocation = imgloc;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This Member !", "Delete Members", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                cn.Open();
                cmd = new SqlCommand("DELETE FROM Table4 WHERE([MemberShip No.]='"+textBox2.Text+"')",cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Delete Is Done.","Delete Member",MessageBoxButtons.OK,MessageBoxIcon.Information);
                textBox1.Clear(); textBox2.Clear(); comboBox1.Text = null; textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); ; pictureBox1.Image = null; textBox3.Clear();
            }
            else { }
        }

    
    }
}
