using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using OfficeOpenXml;

namespace WindowsFormsApp3
{
    public class CustomButton : Button
    {
        
        // Diğer özellikler
        public bool Selected { get; set; }
        public string FoodName { get; set; }
        public float HalfPrice { get; set; }
        public float FullPrice { get; set; }
        public string Category { get; set; }
        public bool IsFull { get; set; }
        public int Adet { get; set; }

        // Yeni bir olay
        public event EventHandler CustomClick;

        // Yeni bir metot
        public void CustomMethod()
        {
            //Console.WriteLine("CustomButton'a özel bir metot çağrıldı.");
        }

        // Güncellenmiş kurucu metod
        public CustomButton(string foodName, float halfPrice, float fullPrice, string category, int adet=1)
        {
            // Değerleri özelliklere atama
            FoodName = foodName;
            HalfPrice = halfPrice;
            FullPrice = fullPrice;
            Category = category;
            Adet = 1;

            this.Width = 100;
            this.Height = 50;
            this.BackColor = Color.Wheat;
            this.ForeColor = Color.Black;
            Font = new Font("Arial", 10, FontStyle.Bold);
        }

        // Yeni tıklama olayı
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Selected = !Selected;

            if (Selected)
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
            }
            else 
            {
                this.BackColor = Color.Wheat;
                this.ForeColor = Color.Black;
            }

            Font = new Font("Arial", 10, FontStyle.Bold);


            if (CustomClick != null)
                CustomClick(this, e);
        }


    }


    public partial class NewMain : Form
    {
        private ComboBox adetComboBox;
        private TextBox totalAmountTextBox;
        private Label buttonsLabel;


        private FlowLayoutPanel leftFlowLayout;
        private FlowLayoutPanel rightFlowLayout;
        private FlowLayoutPanel bottomFlowLayout;

        private float leftPercentage = 0.5f;
        private float rightPercentage = 0.5f; 
                                             
        private ListBox billListBox;
        //CustomButton[] allButtons;
        private List<CustomButton> allButtons = new List<CustomButton>();
        private List<Button> categoryButtons = new List<Button>();


        //public string[] categories = Form1.GetCategorysArray();
        public NewMain()
        {
            InitializeComponent();
        }

        private void InitializeLayouts()
        {
            ///////////////////////////////sol taraf
            leftFlowLayout = new FlowLayoutPanel();
            leftFlowLayout.Dock = DockStyle.Left;
            leftFlowLayout.Width = (int)(Width * leftPercentage);
            leftFlowLayout.FlowDirection = FlowDirection.LeftToRight;
            leftFlowLayout.BackColor = Color.BurlyWood; // Sol tarafın arka plan rengi

            buttonsLabel=new Label();
            buttonsLabel.Text = "Tümü";
            buttonsLabel.Dock = DockStyle.Left;
            buttonsLabel.Font= new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonsLabel.AutoSize= false;
            buttonsLabel.Height =55;
            buttonsLabel.TabIndex=3;
            buttonsLabel.TextAlign = ContentAlignment.MiddleCenter;
            buttonsLabel.Padding = new Padding(0, (buttonsLabel.Height - buttonsLabel.Font.Height) / 4, 0, 0);


            leftFlowLayout.Controls.Add(buttonsLabel);

            /////////////////////////////////////////sağ taraf
            rightFlowLayout = new FlowLayoutPanel();
            rightFlowLayout.Dock = DockStyle.Right;
            rightFlowLayout.Width = (int)(Width * rightPercentage-10);
            rightFlowLayout.FlowDirection = FlowDirection.LeftToRight;
            rightFlowLayout.BackColor = Color.BurlyWood; // Sağ tarafın arka plan rengi


            Label catagoryLabel = new Label();
            catagoryLabel.Text = "Kategoriler";
            catagoryLabel.Dock = DockStyle.Left;
            catagoryLabel.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            catagoryLabel.AutoSize = true;
            catagoryLabel.TabIndex = 3;
            rightFlowLayout.Controls.Add(catagoryLabel);



            ///////////////////////////Alt taraff


            bottomFlowLayout = new FlowLayoutPanel();
            bottomFlowLayout.Dock = DockStyle.Bottom;
            bottomFlowLayout.FlowDirection = FlowDirection.LeftToRight;
            bottomFlowLayout.BackColor= Color.Wheat;

            Button editButton = new Button();
            editButton.Text = "Ürün Düzenle";
            editButton.Width = 100; // Genişlik
            editButton.Height = 50; // Yükseklik
            editButton.Font = new Font("Arial", 12, FontStyle.Bold);
            editButton.BackColor = Color.White;
            editButton.Click += EditButton_Clicked;
            bottomFlowLayout.Controls.Add(editButton);

            Label adetText = new Label();
            adetText.Text = "Adet";
            adetText.Dock = DockStyle.Left;
            adetText.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            adetText.AutoSize = true;
            adetText.TabIndex = 3;

            adetComboBox = new ComboBox();
            adetComboBox.Width = 100;
            adetComboBox.Height = 50;
            for (int i = 1; i <= 20; i++)
            {
                adetComboBox.Items.Add(i.ToString());
            }


            Panel emptySpace = new Panel();
            emptySpace.Width = 50; // Boşluk genişliği

            Button halfButton = new Button();
            halfButton.Text = "Az";
            halfButton.Width = 100; // Genişlik
            halfButton.Height = 50; // Yükseklik
            halfButton.Font = new Font("Arial", 12, FontStyle.Bold);
            halfButton.Click += HalfButton_Click;

            Button fullButton = new Button();
            fullButton.Text = "Tam";
            fullButton.Width = 100; // Genişlik
            fullButton.Height = 50; // Yükseklik
            fullButton.Font = new Font("Arial", 12, FontStyle.Bold);
            fullButton.Click += FullButton_Click;



            Label totalFiyat = new Label();
            totalFiyat.Text = "Toplam Fiyat";
            totalFiyat.Dock = DockStyle.Left;
            totalFiyat.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalFiyat.AutoSize = true;
            totalFiyat.TabIndex = 3;

            totalAmountTextBox = new TextBox();
            totalAmountTextBox.Width = 200; // Genişlik
            totalAmountTextBox.Height = 50; // Yükseklik
            totalAmountTextBox.ReadOnly=true;
            totalAmountTextBox.Multiline = true;


            Button cashButton = new Button();
            cashButton.Text = "Nakit";
            cashButton.Width = 100; // Genişlik
            cashButton.Height = 50; // Yükseklik
            cashButton.Font = new Font("Arial", 12, FontStyle.Bold);
            cashButton.Click += CashButton_Clicked;


            Button creditButton = new Button();
            creditButton.Text = "Kredi";
            creditButton.Width = 100; // Genişlik
            creditButton.Height = 50; // Yükseklik
            creditButton.Font = new Font("Arial", 12, FontStyle.Bold);
            creditButton.Click += CreditButton_Clicked;

            Button clearButton = new Button();
            clearButton.Text = "Temizle";
            clearButton.Width = 100; // Genişlik
            clearButton.Height = 50; // Yükseklik
            clearButton.Font = new Font("Arial", 12, FontStyle.Bold);
            clearButton.Click += ClearButton_Clicked;

            // Butonlara tıklama olayları ekleyin (isterseniz)
            // ... diğer butonlar için de ekleyebilirsiniz.
            bottomFlowLayout.Controls.Add(adetText);
            bottomFlowLayout.Controls.Add(adetComboBox);
            //bottomFlowLayout.Controls.Add(emptySpace);

            bottomFlowLayout.Controls.Add(halfButton);
            bottomFlowLayout.Controls.Add(fullButton);
            bottomFlowLayout.Controls.Add(emptySpace);

            bottomFlowLayout.Controls.Add(totalFiyat);
            bottomFlowLayout.Controls.Add(totalAmountTextBox);

            //bottomFlowLayout.Controls.Add(emptySpace);

            bottomFlowLayout.Controls.Add(cashButton);
            bottomFlowLayout.Controls.Add(creditButton);

            //bottomFlowLayout.Controls.Add(emptySpace);
            bottomFlowLayout.Controls.Add(clearButton);

            // Diğer FlowLayoutPanel'ları buraya ekleyin



            Controls.Add(leftFlowLayout);
            Controls.Add(rightFlowLayout);
            Controls.Add(bottomFlowLayout);

            // Formun boyutu değiştiğinde sol ve sağ tarafın yüzdelik oranlarını ayarla
            this.Resize += YourForm_Resize;
        }


        private void YourForm_Resize(object sender, EventArgs e)
        {
            // Form boyutu değiştiğinde sol ve sağ tarafın yüzdelik oranlarını ayarla
            leftFlowLayout.Width = (int)(Width * leftPercentage);
            rightFlowLayout.Width = (int)(Width * rightPercentage-10);
            billListBox.Width = (int)(Width * rightPercentage);
            billListBox.Height= (int)(Height * rightPercentage);
            //bottomFlowLayout.Width = (int)(Width * leftPercentage);
        }

        private void ClearButton_Clicked(object sender, EventArgs e) 
        {
            if (billListBox.SelectedItem != null)
            {
                billListBox.Items.Remove(billListBox.SelectedItem);
            }
            UpdateTotalAmount();
        }
        private void EditButton_Clicked(object sender, EventArgs e)
        {
            Form2 yeniForm = new Form2();
            yeniForm.FormClosed += NewFormClosedEventHandler;

            yeniForm.Show();
        }
        private void NewFormClosedEventHandler(object sender, FormClosedEventArgs e)
        {
            // Ana formunuzu yenileme işlemleri burada gerçekleşir
            RefreshMainForm();
        }


        private void FullButton_Click(object sender, EventArgs e)
        {

            foreach (CustomButton customButton in allButtons)
            {
                int adet;
                if (!int.TryParse(adetComboBox.Text, out adet) || adet < 1)
                {
                    adet = 1; // Eğer metin sayıya dönüştürülemezse veya 1'den küçükse, varsayılan olarak adet 1 olarak ayarlanır.
                }

                
                if (customButton.Selected==true)
                {
                    customButton.Adet = adet;
                    AddInfoToRightPanel( customButton.FoodName,customButton.Adet= adet,true,customButton.HalfPrice,customButton.FullPrice);
                    customButton.BackColor = Color.Wheat;
                    customButton.Selected = false;
                }

            }

        }

        private void HalfButton_Click(object sender, EventArgs e)
        {
            foreach (CustomButton customButton in allButtons)
            {
                int adet;
                if (!int.TryParse(adetComboBox.Text, out adet) || adet < 1)
                {
                    adet = 1; // Eğer metin sayıya dönüştürülemezse veya 1'den küçükse, varsayılan olarak adet 1 olarak ayarlanır.
                }

               
                if (customButton.Selected == true)
                {
                    customButton.Adet = adet;
                    AddInfoToRightPanel(customButton.FoodName, customButton.Adet= adet, false, customButton.HalfPrice, customButton.FullPrice);
                    customButton.BackColor = Color.Wheat;
                    customButton.Selected = false;
                }

            }
        }

        // Atalay buraya fonksiyon yazacak

        public static void LogPayment(string total_payment, string payment_type)
        {
            // EPPlus kütüphanesinin lisans bağlamını belirle
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            // Bilgisayarın tarihini al
            DateTime date_today = DateTime.Now;

            // Excel dosyasının yolu
            string folderPath = Path.Combine(Environment.CurrentDirectory, "payments", date_today.ToString("yyyy/MM/dd"));
            Directory.CreateDirectory(folderPath); // Klasörü oluştur
            string excelFilePath = Path.Combine(folderPath, "payments.xlsx");

            // Excel paketini oluştur
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                // Eğer sayfa yoksa yeni bir çalışma sayfası oluştur
                if (worksheet == null)
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add("Payments");
                    // Başlık satırını ekle
                    worksheet.Cells[1, 1].Value = "Saat";
                    worksheet.Cells[1, 2].Value = "Toplam Ödeme";
                    worksheet.Cells[1, 3].Value = "Ödeme Tipi";
                }

                // Bir sonraki boş satırın indisini bul
                int nextRow = worksheet.Dimension?.Rows ?? 1;

                // Verileri yeni satıra yaz
                worksheet.Cells[nextRow + 1, 1].Value = DateTime.Now.ToString("HH:mm:ss");
                worksheet.Cells[nextRow + 1, 2].Value = total_payment;
                worksheet.Cells[nextRow + 1, 3].Value = payment_type;

                // Excel dosyasını kaydet
                excelPackage.Save();
            }
        }


        private void CashButton_Clicked(object sender, EventArgs e)
        {
            LogPayment(totalAmountTextBox.Text, "Nakit");
        }
        private void CreditButton_Clicked(object sender, EventArgs e)
        {
            LogPayment(totalAmountTextBox.Text, "Kart");
        }




        private void NewButton_Load(object sender, EventArgs e)
        {
            FetchDataFromDatabase();
            InitializeLayouts();
            CreateCustomButton();
            CreateCategoriesButtons();

            //CreateRightPanel();
            //InitializeTotalAmountTextBox();
        }

        private void CreateRightListBox()
        {
            billListBox = new ListBox();
            billListBox.Anchor = AnchorStyles.Bottom;
            billListBox.Width = rightFlowLayout.Width;
            billListBox.Height = rightFlowLayout.Height;
            billListBox.Font = new Font("Arial", 10, FontStyle.Bold);

            billListBox.DrawMode = DrawMode.OwnerDrawFixed;
            billListBox.DrawItem += BillListBox_DrawItem;
            //infoListBox.DoubleClick += InfoListBox_DoubleClick;

            rightFlowLayout.Controls.Add(billListBox);
        }


        private void BillListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index >= 0)
            {
                CustomListBoxItem item = billListBox.Items[e.Index] as CustomListBoxItem;
                if (item != null)
                {
                    string foodName = item.FoodName;
                    int adet = item.Adet;
                    bool isFull = item.IsFull;
                    float price = isFull ? item.FullPrice : item.HalfPrice;

                    // isFull değerine göre yazdırılacak metni belirleyin
                    string fullness = isFull ? "Tam" : "Az";

                    string info = $"{foodName,-20} Adet - {adet,5} {fullness,5} - Fiyat - {price:C}";

                    // Metnin yüksekliğini hesaplayın
                    Size textSize = TextRenderer.MeasureText(info, e.Font);

                    // Yatay kaydırmayı hesaplayın
                    int padding = 3;
                    int x = e.Bounds.Left + padding;
                    int y = e.Bounds.Top + padding;

                    // Metni çizin
                    e.Graphics.DrawString(info, e.Font, new SolidBrush(e.ForeColor), x, y);

                    // Bir sonraki öğenin yüksekliğini ekleyin
                    billListBox.ItemHeight = textSize.Height + padding * 2;
                }
            }
        }








        private void AddInfoToRightPanel(string foodName, int adet, bool isFull,float hprice ,float fprice)
        {
            //string info = $"{foodName}: Adet - {adet},Tam - {isFull}, Fiyat - {fprice:C}";
            billListBox.Items.Add(new CustomListBoxItem(foodName, adet, isFull,hprice ,fprice));
            
            UpdateTotalAmount();
        }
        private void UpdateTotalAmount()
        {
            float totalAmount = CalculateTotalAmount(); // Bu fonksiyonu kendi hesaplamalarınıza göre düzenleyin
            totalAmountTextBox.Text = $"{totalAmount:C}";
            totalAmountTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
        }
        private float CalculateTotalAmount()
        {
            float totalAmount = 0;

            foreach (CustomListBoxItem item in billListBox.Items)
            {
                if (item.IsFull==true)
                {
                    totalAmount += (item.FullPrice * item.Adet);
                }
                else
                {
                    totalAmount += (item.HalfPrice * item.Adet);
                }
                
            }

            return totalAmount;
        }


        public class CustomListBoxItem
        {
            public string FoodName { get; set; }
            public int Adet { get; set; }
            public float FullPrice { get; set; }
            public float HalfPrice {  get; set; }
            public bool IsFull {  get; set; }
            public CustomListBoxItem(string foodName, int adet, bool isFull,float halfPrice, float fprice)
            {
                FoodName = foodName;
                Adet = adet;
                IsFull = isFull;
                HalfPrice = halfPrice;
                FullPrice = fprice;

                
                
            }
            

            public override string ToString()
            {
                string a;
                float b;
                if (IsFull==true)
                {
                    a = "Tam;";
                    b = FullPrice;
                }
                else
                {
                    a = "Az";
                    b= HalfPrice;
                }
                return $"{FoodName}:   Adet:{Adet}   {a}   {b:C}  ";
            }
        }
        private  void CreateCustomButton()
        {
            foreach (var button in allButtons)
            {  
               button.Text = button.FoodName;
               button.Font = new Font("Arial", 10, FontStyle.Bold);
               leftFlowLayout.Controls.Add(button);
                
            }
        }

        private void FetchDataFromDatabase()
        {
            

            //sql baglantisi kuruldu
            string bilgisayarAdi = Environment.MachineName;
            string connectionString; 
            if (bilgisayarAdi == "DESKTOP-ISC3MCL")
            {
                connectionString = $"Data Source=DESKTOP-ISC3MCL\\SQLEXPRESS;Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            else
            {
                connectionString = $"Data Source={bilgisayarAdi};Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT * FROM DbFood.dbo.FoodMenu";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string buttonName = reader["FoodName"].ToString();
                                float halfPrice = Convert.ToSingle(reader["HalfPrice"]);
                                float fullPrice = Convert.ToSingle(reader["FullPrice"]);
                                string category = reader["FoodCategory"].ToString();

                                // Kullanıcıdan alınan verileri kullanarak CustomButton nesnelerini oluşturabilirsiniz.
                                CustomButton customButton = new CustomButton(buttonName, halfPrice, fullPrice, category);

                                // Oluşturulan CustomButton nesnesini kullanabilirsiniz.
                                // ...

                                // Oluşturulan CustomButton nesnesini allButtons listesine ekleyin
                                allButtons.Add(customButton);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public static string[] GetCategorysArray()
        {


            // Örnek olarak:
            string[] categoriesFromDatabase = Form1.GetCategorysArray();

            // "Tümü" string'ini ekleyerek yeni bir dizi oluşturun
            string[] categoriesWithAll = new string[categoriesFromDatabase.Length + 1];
            categoriesWithAll[0] = "Tümü";
            Array.Copy(categoriesFromDatabase, 0, categoriesWithAll, 1, categoriesFromDatabase.Length);

            return categoriesWithAll;
        }
        

        private void CreateCategoriesButtons()
        {
            foreach (string category in GetCategorysArray())
            {
                Button categoryButton = new Button();
                categoryButton.Text = category;
                categoryButton.Size = new System.Drawing.Size(100, 50);
                categoryButton.Font = new Font("Arial", 10, FontStyle.Bold);
                categoryButton.Click += CategoryButton_Click;
                rightFlowLayout.Controls.Add(categoryButton);

                // Oluşturulan butonları koleksiyona ekleyin
                categoryButtons.Add(categoryButton);
            }
            CreateRightListBox();
        }


        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string categoryName = clickedButton.Text;
                buttonsLabel.Text = categoryName;
                ShowHideButtons(categoryName);
                clickedButton.BackColor = Color.White;

                // Diğer butonların arka plan rengini Color.Wheat olarak ayarlayın
                foreach (Button button in categoryButtons)
                {
                    if (button != clickedButton)
                    {
                        button.BackColor = Color.Wheat;
                    }
                }
            }
        }


     
        private void ShowHideButtons(string desiredCategory)
        {
            foreach (CustomButton button in allButtons)
            {
                if(desiredCategory== GetCategorysArray()[0])
                {
                    button.Show();
                }
                else
                {
                    if (button.Category == desiredCategory)
                    {
                        button.Show();

                    }
                    else
                    {
                        // Eğer butonun kategorisi, istenilen kategoriyle eşleşmiyorsa, gizle
                        button.Hide();
                    }
                }

            }
        }
        private void RefreshMainForm()
        {
            // Yeni bir ana form oluştur ve göster
            NewMain newForm = new NewMain();
            newForm.Show();

            // Mevcut formu kapat

        }

    }

}
