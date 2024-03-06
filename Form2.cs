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

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPLACE;Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("insert into DbFood.dbo.FoodMenu values(@FoodName,@HalfPrice,@FullPrice,@FoodCategory)", con);
            //try and catch
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@FoodName", FoodName.Text);
                cmd.Parameters.AddWithValue("@HalfPrice", float.Parse(HalfPrice.Text));
                cmd.Parameters.AddWithValue("@FullPrice", float.Parse(FullPrice.Text));
                cmd.Parameters.AddWithValue("@FoodCategory", FoodCategory.SelectedItem);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Saved!");
            }
            catch (Exception err)
            {
                MessageBox.Show(" "+err);
            }
            RefreshListBox();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshListBox();
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedItem = listBox1.SelectedItem.ToString();
                string[] parts = selectedItem.Split(' ');

                // Parçalanan değerleri ilgili TextBox'lara yerleştirin
                if (parts.Length >= 4)
                {
                    FoodName.Text = parts[0];
                    HalfPrice.Text = parts[1];
                    FullPrice.Text = parts[2];
                    FoodCategory.Text = parts[3];
                }
            }

        }

        private void update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPLACE;Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Update DbFood.dbo.FoodMenu set HalfPrice = @HalfPrice,FullPrice = @FullPrice,FoodCategory = @FoodCategory where FoodName = @FoodName", con);

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@HalfPrice", float.Parse(HalfPrice.Text));
                cmd.Parameters.AddWithValue("@FullPrice", float.Parse(FullPrice.Text));
                cmd.Parameters.AddWithValue("@FoodCategory", FoodCategory.Text);
                cmd.Parameters.AddWithValue("@FoodName", FoodName.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Yemeğin ismi değiştirilemez, silip yeniden ekleyin. Tamsayı değerler giriniz.");
            }
            RefreshListBox();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPLACE;Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("Delete DbFood.dbo.FoodMenu where FoodName = @Foodname", con);
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@HalfPrice", float.Parse(HalfPrice.Text));
                cmd.Parameters.AddWithValue("@FullPrice", float.Parse(FullPrice.Text));
                cmd.Parameters.AddWithValue("@FoodCategory", FoodCategory.Text);
                cmd.Parameters.AddWithValue("@FoodName", FoodName.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Boyle bir yemek yok!");
            }
            RefreshListBox();
        }
        private void RefreshListBox()
        {
            // ListBox'ı temizle
            listBox1.Items.Clear();

            // Yeniden doldur
            SqlConnection con = new SqlConnection("Data Source=LAPLACE;Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand cmd = new SqlCommand("select * from DbFood.dbo.FoodMenu", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // Her bir sütunu satır olarak birleştirin
                        row += reader[i].ToString() + " ";
                    }
                    // Oluşturulan satırı ListBox'a ekleyin
                    listBox1.Items.Add(row);
                }
                reader.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(" " + err);
            }
        }
    }   
 }
