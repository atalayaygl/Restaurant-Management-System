namespace WindowsFormsApp3
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FoodName = new System.Windows.Forms.TextBox();
            this.FullPrice = new System.Windows.Forms.TextBox();
            this.HalfPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FoodCategory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.update = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.searchString = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.Button();
            this.clearInputs = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FoodName
            // 
            this.FoodName.Location = new System.Drawing.Point(129, 21);
            this.FoodName.Margin = new System.Windows.Forms.Padding(4);
            this.FoodName.Name = "FoodName";
            this.FoodName.Size = new System.Drawing.Size(160, 22);
            this.FoodName.TabIndex = 0;
            this.FoodName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FullPrice
            // 
            this.FullPrice.Location = new System.Drawing.Point(129, 123);
            this.FullPrice.Margin = new System.Windows.Forms.Padding(4);
            this.FullPrice.Name = "FullPrice";
            this.FullPrice.Size = new System.Drawing.Size(160, 22);
            this.FullPrice.TabIndex = 1;
            this.FullPrice.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // HalfPrice
            // 
            this.HalfPrice.Location = new System.Drawing.Point(129, 75);
            this.HalfPrice.Margin = new System.Windows.Forms.Padding(4);
            this.HalfPrice.Name = "HalfPrice";
            this.HalfPrice.Size = new System.Drawing.Size(160, 22);
            this.HalfPrice.TabIndex = 2;
            this.HalfPrice.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Yemeğin Adı:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tam Fiyatı";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Yarım Fiyatı";
            // 
            // FoodCategory
            // 
            this.FoodCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FoodCategory.FormattingEnabled = true;
            this.FoodCategory.Items.AddRange(new object[] {
            "Et Yemekleri",
            "Tavuk Çeşitleri",
            "Köfte Çeşitleri",
            "Sebze Yemekleri",
            "Makarna Çeşitleri",
            "Mezeler",
            "Çorbalar",
            "Tatlı",
            "İçecek"});
            this.FoodCategory.Location = new System.Drawing.Point(129, 181);
            this.FoodCategory.Margin = new System.Windows.Forms.Padding(4);
            this.FoodCategory.Name = "FoodCategory";
            this.FoodCategory.Size = new System.Drawing.Size(160, 24);
            this.FoodCategory.TabIndex = 6;
            this.FoodCategory.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 185);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kategorisi";
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(189, 222);
            this.save.Margin = new System.Windows.Forms.Padding(4);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(100, 28);
            this.save.TabIndex = 8;
            this.save.Text = "Kaydet";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // listBox1
            // 
            this.listBox1.CausesValidation = false;
            this.listBox1.ColumnWidth = 20;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(333, 40);
            this.listBox1.MultiColumn = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(495, 228);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 11;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(65, 257);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(100, 28);
            this.update.TabIndex = 12;
            this.update.Text = "Duzenle";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.Red;
            this.delete.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.delete.Location = new System.Drawing.Point(65, 222);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(99, 28);
            this.delete.TabIndex = 13;
            this.delete.Text = "Sil";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // searchString
            // 
            this.searchString.Location = new System.Drawing.Point(728, 12);
            this.searchString.Name = "searchString";
            this.searchString.Size = new System.Drawing.Size(100, 22);
            this.searchString.TabIndex = 14;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(647, 11);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 15;
            this.search.Text = "Ara";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // clearInputs
            // 
            this.clearInputs.Location = new System.Drawing.Point(189, 257);
            this.clearInputs.Name = "clearInputs";
            this.clearInputs.Size = new System.Drawing.Size(100, 28);
            this.clearInputs.TabIndex = 16;
            this.clearInputs.Text = "Temizle";
            this.clearInputs.UseVisualStyleBackColor = true;
            this.clearInputs.Click += new System.EventHandler(this.clearInputs_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 394);
            this.Controls.Add(this.clearInputs);
            this.Controls.Add(this.search);
            this.Controls.Add(this.searchString);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.update);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FoodCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HalfPrice);
            this.Controls.Add(this.FullPrice);
            this.Controls.Add(this.FoodName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FoodName;
        private System.Windows.Forms.TextBox FullPrice;
        private System.Windows.Forms.TextBox HalfPrice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FoodCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.TextBox searchString;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Button clearInputs;
    }
}