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
        String bilgisayarAdi = Environment.MachineName;
        String sqlBaglantisi;
        

        public Form2()
        {
            
            if (bilgisayarAdi == "DESKTOP-ISC3MCL")
            {
                sqlBaglantisi = $"Data Source=DESKTOP-ISC3MCL\\SQLEXPRESS;Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            else
            {
                sqlBaglantisi = $"Data Source={bilgisayarAdi};Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }


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
            SqlConnection con = new SqlConnection(sqlBaglantisi);
            SqlCommand cmd = new SqlCommand("insert into DbFood.dbo.FoodMenu values(@FoodName,@HalfPrice,@FullPrice,@FoodCategory)", con);
            //try and catch
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@FoodName", FoodName.Text);
                cmd.Parameters.AddWithValue("@HalfPrice", float.Parse(HalfPrice.Text));
                cmd.Parameters.AddWithValue("@FullPrice", float.Parse(FullPrice.Text));
                cmd.Parameters.AddWithValue("@FoodCategory", FoodCategory.SelectedItem);
                if(FoodCategory.SelectedItem == null || FoodName.Text == null)
                {
                    throw new Exception("Kategori ve İsim Boş Bırakılamaz!!");

                }
                
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Successfully Saved!");

            }
            catch (Exception err)
            {
                MessageBox.Show(" ");
            }
            RefreshListBox();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshListBox();
        }



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.TopIndex = listBox1.Items.Count - 1;
            if (listBox1.SelectedItem != null)
            {
                string veri = listBox1.SelectedItem.ToString();

                // İlk bulunan sayının indeksini bulma
                int ilkSayiIndex = -1;
                for (int i = 0; i < veri.Length; i++)
                {
                    if (char.IsDigit(veri[i]))
                    {
                        ilkSayiIndex = i;
                        break;
                    }
                }

                // Son bulunan sayının indeksini bulma
                int sonSayiIndex = -1;
                for (int i = veri.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(veri[i]))
                    {
                        sonSayiIndex = i;
                        break;
                    }
                }

                // Yemek adı
                string yemekAdi = veri.Substring(0, ilkSayiIndex).Trim();

                // Yarım fiyat ve tam fiyatı al
                string fiyatlar = veri.Substring(ilkSayiIndex, sonSayiIndex - ilkSayiIndex+1).Trim();
                string[] fiyatParcalar = fiyatlar.Split(' ');
                string yarimFiyat = fiyatParcalar[0];
                string tamFiyat = fiyatParcalar[1];
                string kategori = veri.Substring(sonSayiIndex + 1).Trim();
               
                
                    FoodName.Text = yemekAdi;
                    HalfPrice.Text = yarimFiyat;
                    FullPrice.Text = tamFiyat;
                    String foodCategoryChoice = kategori;
                    
                    switch (foodCategoryChoice)
                    {
                        case "Et Yemekleri":
                            FoodCategory.SelectedIndex = 0;
                            break;
                        case "Tavuk Çesitleri":
                            FoodCategory.SelectedIndex = 1;
                            break;
                        case "Köfte Çesitleri":
                            FoodCategory.SelectedIndex = 2;
                            break;
                        case "Sebze Yemekleri":
                            FoodCategory.SelectedIndex = 3;
                            break;
                        case "Makarna Çesitleri":
                            FoodCategory.SelectedIndex = 4;
                            break;
                        case "Mezeler":
                            FoodCategory.SelectedIndex = 5;
                            break;
                        case "Çorbalar":
                            FoodCategory.SelectedIndex = 6;
                            break;
                        case "Tatli":
                            FoodCategory.SelectedIndex = 7;
                            break;
                        case "Içecek":
                            FoodCategory.SelectedIndex = 8;
                            break;
                        default:
                            // Eğer foodCategoryChoice belirtilen herhangi bir kategoriyle eşleşmiyorsa, FoodCategory ComboBox'ını temizleyin
                            FoodCategory.SelectedIndex = -1;
                            break;
                    }
                    

                
            }
        }


        private void update_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlBaglantisi);
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
            SqlConnection con = new SqlConnection(sqlBaglantisi);
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
            SqlConnection con = new SqlConnection(sqlBaglantisi);
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

        private void search_Click(object sender, EventArgs e)
        {
            // Bağlantı dizesi
            string connectionString = sqlBaglantisi;

            // Kullanıcı tarafından girilen metni al
            string searchText = searchString.Text.Trim();

            // Bağlantıyı oluştur
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Sorgu metni
                string query = "SELECT [FoodName], [HalfPrice], [FullPrice], [FoodCategory] FROM DbFood.dbo.FoodMenu WHERE FoodName LIKE @FoodName";

                // Komut oluştur
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Parametre ekle
                    cmd.Parameters.AddWithValue("@FoodName", "%" + searchText + "%");

                    // Bağlantıyı aç
                    con.Open();

                    // Veri okuyucuyu oluştur
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // ListBox temizle
                        listBox1.Items.Clear();

                        // Her bir satırı oku ve ListBox'a ekle
                        while (reader.Read())
                        {
                            // Formatlı bir şekilde tüm alanları ListBox'a ekle
                            string item = string.Format("{0} {1} {2} {3}",
                                                         reader["FoodName"].ToString(),
                                                         reader["HalfPrice"].ToString(),
                                                         reader["FullPrice"].ToString(),
                                                         reader["FoodCategory"].ToString());

                            listBox1.Items.Add(item);
                        }

                    }
                }
            }
        }

        private void clearInputs_Click(object sender, EventArgs e)
        {
            FoodName.Text = "";
            HalfPrice.Text = "";
            FullPrice.Text = "";
            FoodCategory.SelectedItem = null;
            
        }
    }
}
