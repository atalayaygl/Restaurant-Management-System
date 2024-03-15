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

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Muslugu taktim, kategoriler bu fonksiyon ile cagirilacak
        public static String[] GetCategorysArray()
        {
            string mehmedin = "DESKTOP-ISC3MCL\\SQLEXPRESS";
            string sqlBaglantisi;
            //sql baglantisi kuruldu
            string bilgisayarAdi = Environment.MachineName;
            if (Environment.MachineName == mehmedin) {
                sqlBaglantisi = $"Data Source={mehmedin};Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            else
            {
                sqlBaglantisi = $"Data Source={bilgisayarAdi};Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            //kategori isimleri cekilmesi icin sql komutu set edildi
            string query = "SELECT DISTINCT FoodCategory FROM DbFood.dbo.FoodMenu";

            //kategori isimlerini tutacak string tanimi
            List<string> categories = new List<string>();

            //sorgu operasyonu basladi
            using (SqlConnection conn = new SqlConnection(sqlBaglantisi))
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //Kategori isimlerini oku
                    while (reader.Read())
                    {
                        string category = reader["FoodCategory"].ToString();
                        categories.Add(category);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                }
            }

            //kategoriler geri donduruldu
            return categories.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 yeniForm = new Form2();
            yeniForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
