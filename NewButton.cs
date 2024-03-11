using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public bool isFull { get; set; }
        public string FoodName { get; set; }
        public float HalfPrice { get; set; }
        public float FullPrice { get; set; }
        public string Category { get; set; }
        public int Adet { get; set; }

        // Yeni bir olay
        public event EventHandler CustomClick;

        // Yeni bir metot
        public void CustomMethod()
        {
            //Console.WriteLine("CustomButton'a özel bir metot çağrıldı.");
        }

        // Güncellenmiş kurucu metod
        public CustomButton(string foodName, float halfPrice, float fullPrice, string category, int adet=1, bool isFull=true)
        {
            // Değerleri özelliklere atama
            this.isFull = isFull;
            FoodName = foodName;
            HalfPrice = halfPrice;
            FullPrice = fullPrice;
            Category = category;
            Adet = 1;

            // Tüm yemekler tam olarak atanır
            this.isFull = true;

            this.Width = 100;
            this.Height = 50;

        }

        // Yeni tıklama olayı
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            isFull = true;
            Console.WriteLine($"FoodName: {FoodName}  Tam Fiyat: {FullPrice}  Tam mı ?:{isFull} Kategorisi ne :{Category}");
            AddInfoToListBox();

            if (CustomClick != null)
                CustomClick(this, e);
        }
        private void AddInfoToListBox()
        {
            // NewButton formunu bul
            NewMain newButton = this.FindForm() as NewMain;

            // NewButton bulunamazsa işlemi sonlandır
            if (newButton == null)
                return;

            // Bilgileri sağ tarafa ekleyen fonksiyonu çağır
            newButton.AddInfoToRightPanel(FoodName, Adet, FullPrice,isFull);
        }

    }


    public partial class NewMain : Form
    {
        private TextBox totalAmountTextBox;

        private ListBox infoListBox;
        CustomButton[] allButtons;

        public string[] categories = { "Tümü","Çorba Çeşitleri", "Sebze Çeşitleri", "Bakliyat Yemekleri","Ciğer Yemekleri","Kıymalı Yemekler","Pilav Makarna Çeşitleri",
            "Meze Çeşitleri","Tatlı Çeşitleri","Et Yemekleri","Tavuk Çeşitleri","Köfte Çeşitleri","İçecek Çeşitleri" };
        public NewMain()
        {
            InitializeComponent();
        }

        private void NewButton_Load(object sender, EventArgs e)
        {
            CreateCategoriesButtons();
            CreateCustomButton();
            CreateRightPanel();
            InitializeTotalAmountTextBox();
        }
        private void InitializeTotalAmountTextBox()
        {
            totalAmountTextBox = new TextBox();
            totalAmountTextBox.Dock = DockStyle.Bottom;
            totalAmountTextBox.Multiline = true;
            totalAmountTextBox.ReadOnly = true;
            this.Controls.Add(totalAmountTextBox);
        }

        private void CreateRightPanel()
        {
            infoListBox = new ListBox();
            infoListBox.Dock = DockStyle.Right;
            infoListBox.Width = 300;
            infoListBox.Height = 10;

            infoListBox.DoubleClick += InfoListBox_DoubleClick;

            this.Controls.Add(infoListBox);
        }
        public void AddInfoToRightPanel(string foodName, int adet, float fullPrice, bool isFull)
        {
            string info = $"{foodName}: Adet - {adet},Tam - {isFull}, Fiyat - {fullPrice:C}";
            infoListBox.Items.Add(new CustomListBoxItem(foodName, adet, fullPrice));
            UpdateTotalAmount();
        }
        private void UpdateTotalAmount()
        {
            float totalAmount = CalculateTotalAmount(); // Bu fonksiyonu kendi hesaplamalarınıza göre düzenleyin
            totalAmountTextBox.Text = $"Toplam Fiyat: {totalAmount:C}";
        }
        private float CalculateTotalAmount()
        {
            float totalAmount = 0;

            foreach (CustomListBoxItem item in infoListBox.Items)
            {
                totalAmount += (item.FullPrice*item.Adet);
            }

            return totalAmount;
        }
        private void InfoListBox_DoubleClick(object sender, EventArgs e)
        {
            // Seçili öğeyi al
            CustomListBoxItem selectedItem = infoListBox.SelectedItem as CustomListBoxItem;

            // Seçili öğe varsa adetini arttır
            if (selectedItem != null)
            {
                selectedItem.Adet++;
                UpdateListBoxItem(selectedItem);
            }
        }
        private void UpdateListBoxItem(CustomListBoxItem item)
        {
            int selectedIndex = infoListBox.SelectedIndex;

            // Seçili öğeyi kaldır
            infoListBox.Items.RemoveAt(selectedIndex);

            // Güncellenmiş öğeyi ekleyin
            infoListBox.Items.Insert(selectedIndex, item);
            UpdateTotalAmount();
        }
        public class CustomListBoxItem
        {
            public string FoodName { get; set; }
            public int Adet { get; set; }
            public float FullPrice { get; set; }
            public CustomListBoxItem(string foodName, int adet, float fullPrice)
            {
                FoodName = foodName;
                Adet = adet;
                FullPrice = fullPrice;
            }
            public override string ToString()
            {
                return $"{FoodName}: Adet - {Adet}, Fiyat - {FullPrice:C}";
            }
        }
        private void CreateCustomButton()
        {
            FlowLayoutPanel flowLayoutPanel;
            flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Bottom; // Sadece alt kısmını kaplamasını sağlar
            flowLayoutPanel.Height = 200;

            // İstediğiniz kadar CustomButton nesnesini oluşturun
            CustomButton button1 = new CustomButton("Et", 5.99f, 9.99f, "Et Yemekleri");
            CustomButton button2 = new CustomButton("Tatlı", 6.99f, 10.99f, "Tatlı Çeşitleri");
            CustomButton button3 = new CustomButton("İçecek", 7.99f, 11.99f, "İçecek Çeşitleri");

            // Tüm butonları bir dizi içinde toplamak
            allButtons = new CustomButton[] { button1, button2, button3 };

            // Butonları FlowLayoutPanel'e ekle
            foreach (var button in allButtons)
            {
                button.Text = button.FoodName;
                flowLayoutPanel.Controls.Add(button);
            }

            // Forma FlowLayoutPanel'i ekle
            this.Controls.Add(flowLayoutPanel);
        }
        private void CreateCategoriesButtons()
        {
            // FlowLayoutPanel oluştur
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Top;



            // Kategori butonlarını oluştur ve FlowLayoutPanel'e ekle
            foreach (string category in categories)
            {
                Button categoryButton = new Button();
                categoryButton.Text = category;
                categoryButton.Size = new System.Drawing.Size(100, 50);
                categoryButton.Click += CategoryButton_Click;
                flowLayoutPanel.Controls.Add(categoryButton);
            }


            // Forma FlowLayoutPanel'i ekle
            this.Controls.Add(flowLayoutPanel);
        }


        private void CategoryButton_Click(object sender, EventArgs e)
        {
            // Kategori butonuna tıklandığında çalışacak kodu buraya ekleyin
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string categoryName = clickedButton.Text;
                //Showhide fonskiyonuyla text ile eşit olmayanları hidela
                ShowHideButtons(categoryName);

            }
        }

        private void ShowHideButtons(string desiredCategory)
        {
            foreach (CustomButton button in allButtons)
            {
                if(desiredCategory== categories[0])
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
