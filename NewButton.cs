using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        }

        // Yeni tıklama olayı
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.BackColor = Color.Red;
            Selected = true;          

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
            leftFlowLayout.FlowDirection = FlowDirection.TopDown;
            leftFlowLayout.BackColor = Color.LightBlue; // Sol tarafın arka plan rengi

            buttonsLabel=new Label();
            buttonsLabel.Text = "Tümü";
            buttonsLabel.Dock = DockStyle.Left;
            buttonsLabel.Font= new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonsLabel.AutoSize= true;
            buttonsLabel.TabIndex=3;
            leftFlowLayout.Controls.Add(buttonsLabel);

            /////////////////////////////////////////sağ taraf
            rightFlowLayout = new FlowLayoutPanel();
            rightFlowLayout.Dock = DockStyle.Right;
            rightFlowLayout.Width = (int)(Width * rightPercentage-10);
            rightFlowLayout.FlowDirection = FlowDirection.LeftToRight;
            rightFlowLayout.BackColor = Color.LightGreen; // Sağ tarafın arka plan rengi


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
            bottomFlowLayout.BackColor= Color.DarkGray;

            Button editButton = new Button();
            editButton.Text = "Ürün Düzenle";
            editButton.Width = 100; // Genişlik
            editButton.Height = 50; // Yükseklik
            editButton.Font = new Font("Arial", 12, FontStyle.Bold);
            editButton.BackColor = Color.Red;
            editButton.Click += EditButton_Clicked;
            bottomFlowLayout.Controls.Add(editButton);

            adetComboBox = new ComboBox();
            adetComboBox.Width = 100;
            adetComboBox.Height = 50;
            for (int i = 1; i <= 20; i++)
            {
                adetComboBox.Items.Add(i.ToString());
            }


            Panel emptySpace = new Panel();
            emptySpace.Width = 100; // Boşluk genişliği

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

            Panel emptySpace2 = new Panel();
            emptySpace2.Width = 100; // Boşluk genişliği

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


            Panel emptySpace3 = new Panel();
            emptySpace3.Width = 100; // Boşluk genişliği


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

            // Butonlara tıklama olayları ekleyin (isterseniz)
            // ... diğer butonlar için de ekleyebilirsiniz.

            bottomFlowLayout.Controls.Add(adetComboBox);
            bottomFlowLayout.Controls.Add(emptySpace);

            bottomFlowLayout.Controls.Add(halfButton);
            bottomFlowLayout.Controls.Add(fullButton);
            bottomFlowLayout.Controls.Add(emptySpace2);

            bottomFlowLayout.Controls.Add(totalFiyat);
            bottomFlowLayout.Controls.Add(totalAmountTextBox);

            bottomFlowLayout.Controls.Add(emptySpace3);

            bottomFlowLayout.Controls.Add(cashButton);
            bottomFlowLayout.Controls.Add(creditButton);

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
            rightFlowLayout.Width = (int)(Width * rightPercentage);
            billListBox.Width = (int)(Width * rightPercentage);
            billListBox.Height= (int)(Height * rightPercentage);
            //bottomFlowLayout.Width = (int)(Width * leftPercentage);
        }


        private void EditButton_Clicked(object sender, EventArgs e)
        {
            Form2 yeniForm = new Form2();
            yeniForm.Show();
        }


        private void FullButton_Click(object sender, EventArgs e)
        {
            foreach (CustomButton customButton in allButtons)
            {
                if (customButton.Selected==true)
                {
                    AddInfoToRightPanel( customButton.FoodName,customButton.Adet= int.Parse(adetComboBox.Text),true,customButton.HalfPrice,customButton.FullPrice);
                }
                customButton.Selected = false;
                customButton.BackColor = Color.White;
            }

        }

        private void HalfButton_Click(object sender, EventArgs e)
        {
            foreach (CustomButton customButton in allButtons)
            {
                if (customButton.Selected == true)
                {
                    AddInfoToRightPanel(customButton.FoodName, customButton.Adet= int.Parse(adetComboBox.Text), false, customButton.HalfPrice, customButton.FullPrice);
                }
                customButton.Selected = false;
                customButton.BackColor = Color.White;
            }
        }

        private void CashButton_Clicked(object sender, EventArgs e)
        {

        }
        private void CreditButton_Clicked(object sender, EventArgs e)
        {

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


            //infoListBox.DoubleClick += InfoListBox_DoubleClick;

            rightFlowLayout.Controls.Add(billListBox);
        }
        public void AddInfoToRightPanel(string foodName, int adet, bool isFull,float hprice ,float fprice)
        {
            //string info = $"{foodName}: Adet - {adet},Tam - {isFull}, Fiyat - {fprice:C}";
            billListBox.Items.Add(new CustomListBoxItem(foodName, adet, isFull,hprice ,fprice));
            UpdateTotalAmount();
        }
        private void UpdateTotalAmount()
        {
            float totalAmount = CalculateTotalAmount(); // Bu fonksiyonu kendi hesaplamalarınıza göre düzenleyin
            totalAmountTextBox.Text = $"{totalAmount:C}";
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
        private void InfoListBox_DoubleClick(object sender, EventArgs e)
        {
            // Seçili öğeyi al
            CustomListBoxItem selectedItem = billListBox.SelectedItem as CustomListBoxItem;

            // Seçili öğe varsa adetini arttır
            if (selectedItem != null)
            {
                selectedItem.Adet++;
                UpdateListBoxItem(selectedItem);
            }
        }
        private void UpdateListBoxItem(CustomListBoxItem item)
        {
            int selectedIndex = billListBox.SelectedIndex;

            // Seçili öğeyi kaldır
            billListBox.Items.RemoveAt(selectedIndex);

            // Güncellenmiş öğeyi ekleyin
            billListBox.Items.Insert(selectedIndex, item);
            UpdateTotalAmount();
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
                return $"{FoodName}: - {Adet}- Tam mı?{IsFull}- {HalfPrice:C} - {FullPrice:C}  ";
            }
        }
        private void CreateCustomButton()
        {
            foreach (var button in allButtons)
            {
                button.Text = button.FoodName;
                leftFlowLayout.Controls.Add(button);
            }

        }
        private void FetchDataFromDatabase()
        {
            string mehmedin = "DESKTOP-ISC3MCL\\SQLEXPRESS";

            //sql baglantisi kuruldu
            string bilgisayarAdi = Environment.MachineName;
            string connectionString = $"Data Source={mehmedin};Integrated Security=True;Connection Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
                categoryButton.Click += CategoryButton_Click;
                rightFlowLayout.Controls.Add(categoryButton);
            }
            CreateRightListBox();

        }


        private void CategoryButton_Click(object sender, EventArgs e)
        {
            // Kategori butonuna tıklandığında çalışacak kodu buraya ekleyin
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string categoryName = clickedButton.Text;
                buttonsLabel.Text = categoryName;
                ShowHideButtons(categoryName);

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
    }
}
