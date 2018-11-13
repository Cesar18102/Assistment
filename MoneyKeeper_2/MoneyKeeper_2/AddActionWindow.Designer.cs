namespace MoneyKeeper_2
{
    partial class AddActionWindow
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
            this.ActionTypeLabel = new System.Windows.Forms.Label();
            this.ActionDateLabel = new System.Windows.Forms.Label();
            this.PersonLabel = new System.Windows.Forms.Label();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.ActionType = new System.Windows.Forms.ComboBox();
            this.ActionDate = new System.Windows.Forms.DateTimePicker();
            this.Comment = new System.Windows.Forms.TextBox();
            this.ReadyButton = new System.Windows.Forms.Button();
            this.ActionStateLabel = new System.Windows.Forms.Label();
            this.ActionState = new System.Windows.Forms.ComboBox();
            this.Payed = new System.Windows.Forms.NumericUpDown();
            this.PayedLabel = new System.Windows.Forms.Label();
            this.CountOfNames = new System.Windows.Forms.NumericUpDown();
            this.CountOfNamesLabel = new System.Windows.Forms.Label();
            this.SummaryOrderLabel = new System.Windows.Forms.Label();
            this.SummaryOrder = new System.Windows.Forms.Label();
            this.ProductNameLabel = new System.Windows.Forms.Label();
            this.ProductCountLabel = new System.Windows.Forms.Label();
            this.ProductPriceLabel = new System.Windows.Forms.Label();
            this.ProductNameEarn = new System.Windows.Forms.ComboBox();
            this.ProductCount = new System.Windows.Forms.NumericUpDown();
            this.ProductPrice = new System.Windows.Forms.NumericUpDown();
            this.ProductNameSpend = new System.Windows.Forms.TextBox();
            this.AddProductName = new System.Windows.Forms.Button();
            this.PersonName = new System.Windows.Forms.ComboBox();
            this.DeleteCheck = new System.Windows.Forms.ComboBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddClientButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Payed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfNames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductPrice)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ActionTypeLabel
            // 
            this.ActionTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ActionTypeLabel.AutoSize = true;
            this.ActionTypeLabel.Location = new System.Drawing.Point(3, 6);
            this.ActionTypeLabel.Name = "ActionTypeLabel";
            this.ActionTypeLabel.Size = new System.Drawing.Size(75, 13);
            this.ActionTypeLabel.TabIndex = 0;
            this.ActionTypeLabel.Text = "Вид события:";
            // 
            // ActionDateLabel
            // 
            this.ActionDateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ActionDateLabel.AutoSize = true;
            this.ActionDateLabel.Location = new System.Drawing.Point(3, 36);
            this.ActionDateLabel.Name = "ActionDateLabel";
            this.ActionDateLabel.Size = new System.Drawing.Size(82, 13);
            this.ActionDateLabel.TabIndex = 1;
            this.ActionDateLabel.Text = "Дата события:";
            // 
            // PersonLabel
            // 
            this.PersonLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PersonLabel.AutoSize = true;
            this.PersonLabel.Location = new System.Drawing.Point(3, 246);
            this.PersonLabel.Name = "PersonLabel";
            this.PersonLabel.Size = new System.Drawing.Size(46, 13);
            this.PersonLabel.TabIndex = 5;
            this.PersonLabel.Text = "Клиент:";
            // 
            // CommentLabel
            // 
            this.CommentLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Location = new System.Drawing.Point(3, 272);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(80, 13);
            this.CommentLabel.TabIndex = 6;
            this.CommentLabel.Text = "Комментарий:";
            // 
            // ActionType
            // 
            this.ActionType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionType.FormattingEnabled = true;
            this.ActionType.Items.AddRange(new object[] {
            "Доход",
            "Расход",
            "Возврат долга",
            "Зарплата"});
            this.ActionType.Location = new System.Drawing.Point(155, 3);
            this.ActionType.Name = "ActionType";
            this.ActionType.Size = new System.Drawing.Size(186, 21);
            this.ActionType.TabIndex = 1;
            this.ActionType.SelectedIndexChanged += new System.EventHandler(this.ActionType_SelectedIndexChanged);
            // 
            // ActionDate
            // 
            this.ActionDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ActionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ActionDate.Location = new System.Drawing.Point(155, 30);
            this.ActionDate.Name = "ActionDate";
            this.ActionDate.Size = new System.Drawing.Size(186, 20);
            this.ActionDate.TabIndex = 2;
            // 
            // Comment
            // 
            this.Comment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Comment.Location = new System.Drawing.Point(155, 269);
            this.Comment.Multiline = true;
            this.Comment.Name = "Comment";
            this.Comment.Size = new System.Drawing.Size(185, 71);
            this.Comment.TabIndex = 11;
            // 
            // ReadyButton
            // 
            this.ReadyButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ReadyButton.Enabled = false;
            this.ReadyButton.Location = new System.Drawing.Point(155, 346);
            this.ReadyButton.Name = "ReadyButton";
            this.ReadyButton.Size = new System.Drawing.Size(185, 23);
            this.ReadyButton.TabIndex = 12;
            this.ReadyButton.Text = "Готово!";
            this.ReadyButton.UseVisualStyleBackColor = true;
            this.ReadyButton.Click += new System.EventHandler(this.ReadyButton_Click);
            // 
            // ActionStateLabel
            // 
            this.ActionStateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ActionStateLabel.AutoSize = true;
            this.ActionStateLabel.Location = new System.Drawing.Point(3, 59);
            this.ActionStateLabel.Name = "ActionStateLabel";
            this.ActionStateLabel.Size = new System.Drawing.Size(44, 13);
            this.ActionStateLabel.TabIndex = 16;
            this.ActionStateLabel.Text = "Статус:";
            // 
            // ActionState
            // 
            this.ActionState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ActionState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActionState.FormattingEnabled = true;
            this.ActionState.Items.AddRange(new object[] {
            "Оплачено",
            "Отсрочка"});
            this.ActionState.Location = new System.Drawing.Point(155, 56);
            this.ActionState.Name = "ActionState";
            this.ActionState.Size = new System.Drawing.Size(186, 21);
            this.ActionState.TabIndex = 3;
            this.ActionState.SelectedIndexChanged += new System.EventHandler(this.ActionState_SelectedIndexChanged);
            // 
            // Payed
            // 
            this.Payed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Payed.DecimalPlaces = 2;
            this.Payed.Location = new System.Drawing.Point(156, 188);
            this.Payed.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.Payed.Name = "Payed";
            this.Payed.Size = new System.Drawing.Size(185, 20);
            this.Payed.TabIndex = 8;
            // 
            // PayedLabel
            // 
            this.PayedLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PayedLabel.AutoSize = true;
            this.PayedLabel.Location = new System.Drawing.Point(4, 190);
            this.PayedLabel.Name = "PayedLabel";
            this.PayedLabel.Size = new System.Drawing.Size(64, 13);
            this.PayedLabel.TabIndex = 18;
            this.PayedLabel.Text = "Заплачено:";
            // 
            // CountOfNames
            // 
            this.CountOfNames.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CountOfNames.Location = new System.Drawing.Point(155, 83);
            this.CountOfNames.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.CountOfNames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CountOfNames.Name = "CountOfNames";
            this.CountOfNames.Size = new System.Drawing.Size(185, 20);
            this.CountOfNames.TabIndex = 4;
            this.CountOfNames.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CountOfNames.ValueChanged += new System.EventHandler(this.CountOfNames_ValueChanged);
            // 
            // CountOfNamesLabel
            // 
            this.CountOfNamesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CountOfNamesLabel.AutoSize = true;
            this.CountOfNamesLabel.Location = new System.Drawing.Point(3, 85);
            this.CountOfNamesLabel.Name = "CountOfNamesLabel";
            this.CountOfNamesLabel.Size = new System.Drawing.Size(146, 13);
            this.CountOfNamesLabel.TabIndex = 20;
            this.CountOfNamesLabel.Text = "Количество наименований:";
            // 
            // SummaryOrderLabel
            // 
            this.SummaryOrderLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SummaryOrderLabel.AutoSize = true;
            this.SummaryOrderLabel.Location = new System.Drawing.Point(13, 5);
            this.SummaryOrderLabel.Name = "SummaryOrderLabel";
            this.SummaryOrderLabel.Size = new System.Drawing.Size(40, 13);
            this.SummaryOrderLabel.TabIndex = 22;
            this.SummaryOrderLabel.Text = "Итого:";
            // 
            // SummaryOrder
            // 
            this.SummaryOrder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SummaryOrder.AutoSize = true;
            this.SummaryOrder.Location = new System.Drawing.Point(59, 5);
            this.SummaryOrder.Name = "SummaryOrder";
            this.SummaryOrder.Size = new System.Drawing.Size(0, 13);
            this.SummaryOrder.TabIndex = 23;
            // 
            // ProductNameLabel
            // 
            this.ProductNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductNameLabel.AutoSize = true;
            this.ProductNameLabel.Location = new System.Drawing.Point(3, 112);
            this.ProductNameLabel.Name = "ProductNameLabel";
            this.ProductNameLabel.Size = new System.Drawing.Size(86, 13);
            this.ProductNameLabel.TabIndex = 2;
            this.ProductNameLabel.Text = "Наименование:";
            // 
            // ProductCountLabel
            // 
            this.ProductCountLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductCountLabel.AutoSize = true;
            this.ProductCountLabel.Location = new System.Drawing.Point(3, 138);
            this.ProductCountLabel.Name = "ProductCountLabel";
            this.ProductCountLabel.Size = new System.Drawing.Size(69, 13);
            this.ProductCountLabel.TabIndex = 3;
            this.ProductCountLabel.Text = "Количество:";
            // 
            // ProductPriceLabel
            // 
            this.ProductPriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductPriceLabel.AutoSize = true;
            this.ProductPriceLabel.Location = new System.Drawing.Point(3, 164);
            this.ProductPriceLabel.Name = "ProductPriceLabel";
            this.ProductPriceLabel.Size = new System.Drawing.Size(107, 13);
            this.ProductPriceLabel.TabIndex = 4;
            this.ProductPriceLabel.Text = "Номинальная цена:";
            // 
            // ProductNameEarn
            // 
            this.ProductNameEarn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductNameEarn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProductNameEarn.FormattingEnabled = true;
            this.ProductNameEarn.Location = new System.Drawing.Point(155, 109);
            this.ProductNameEarn.Name = "ProductNameEarn";
            this.ProductNameEarn.Size = new System.Drawing.Size(186, 21);
            this.ProductNameEarn.TabIndex = 5;
            // 
            // ProductCount
            // 
            this.ProductCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductCount.Location = new System.Drawing.Point(155, 136);
            this.ProductCount.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.ProductCount.Name = "ProductCount";
            this.ProductCount.Size = new System.Drawing.Size(185, 20);
            this.ProductCount.TabIndex = 6;
            this.ProductCount.ValueChanged += new System.EventHandler(this.ProductParams_ValueChanged);
            // 
            // ProductPrice
            // 
            this.ProductPrice.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductPrice.DecimalPlaces = 2;
            this.ProductPrice.Location = new System.Drawing.Point(155, 162);
            this.ProductPrice.Maximum = new decimal(new int[] {
            -159383553,
            46653770,
            5421,
            0});
            this.ProductPrice.Name = "ProductPrice";
            this.ProductPrice.Size = new System.Drawing.Size(185, 20);
            this.ProductPrice.TabIndex = 7;
            this.ProductPrice.ValueChanged += new System.EventHandler(this.ProductParams_ValueChanged);
            // 
            // ProductNameSpend
            // 
            this.ProductNameSpend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProductNameSpend.Location = new System.Drawing.Point(155, 109);
            this.ProductNameSpend.Name = "ProductNameSpend";
            this.ProductNameSpend.Size = new System.Drawing.Size(185, 20);
            this.ProductNameSpend.TabIndex = 5;
            this.ProductNameSpend.Visible = false;
            // 
            // AddProductName
            // 
            this.AddProductName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AddProductName.Location = new System.Drawing.Point(3, 214);
            this.AddProductName.Name = "AddProductName";
            this.AddProductName.Size = new System.Drawing.Size(337, 23);
            this.AddProductName.TabIndex = 9;
            this.AddProductName.Text = "Добавить наименование. Осталось: 1\r\n";
            this.AddProductName.UseVisualStyleBackColor = true;
            this.AddProductName.Click += new System.EventHandler(this.AddProductName_Click);
            // 
            // PersonName
            // 
            this.PersonName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.PersonName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PersonName.FormattingEnabled = true;
            this.PersonName.Location = new System.Drawing.Point(154, 243);
            this.PersonName.Name = "PersonName";
            this.PersonName.Size = new System.Drawing.Size(186, 21);
            this.PersonName.TabIndex = 10;
            // 
            // DeleteCheck
            // 
            this.DeleteCheck.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DeleteCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeleteCheck.FormattingEnabled = true;
            this.DeleteCheck.Location = new System.Drawing.Point(3, 186);
            this.DeleteCheck.Name = "DeleteCheck";
            this.DeleteCheck.Size = new System.Drawing.Size(232, 21);
            this.DeleteCheck.TabIndex = 24;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(3, 214);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(232, 23);
            this.DeleteButton.TabIndex = 25;
            this.DeleteButton.Text = "Удалить наименование";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddClientButton
            // 
            this.AddClientButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AddClientButton.Location = new System.Drawing.Point(3, 242);
            this.AddClientButton.Name = "AddClientButton";
            this.AddClientButton.Size = new System.Drawing.Size(232, 23);
            this.AddClientButton.TabIndex = 26;
            this.AddClientButton.Text = "Добавить клиента";
            this.AddClientButton.UseVisualStyleBackColor = true;
            this.AddClientButton.Click += new System.EventHandler(this.AddClientButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.ActionTypeLabel);
            this.panel1.Controls.Add(this.ActionDateLabel);
            this.panel1.Controls.Add(this.ProductNameLabel);
            this.panel1.Controls.Add(this.ProductCountLabel);
            this.panel1.Controls.Add(this.PersonName);
            this.panel1.Controls.Add(this.ProductPriceLabel);
            this.panel1.Controls.Add(this.AddProductName);
            this.panel1.Controls.Add(this.PersonLabel);
            this.panel1.Controls.Add(this.CommentLabel);
            this.panel1.Controls.Add(this.ActionType);
            this.panel1.Controls.Add(this.CountOfNames);
            this.panel1.Controls.Add(this.ActionDate);
            this.panel1.Controls.Add(this.CountOfNamesLabel);
            this.panel1.Controls.Add(this.ProductNameEarn);
            this.panel1.Controls.Add(this.Payed);
            this.panel1.Controls.Add(this.ProductCount);
            this.panel1.Controls.Add(this.PayedLabel);
            this.panel1.Controls.Add(this.ProductPrice);
            this.panel1.Controls.Add(this.ActionState);
            this.panel1.Controls.Add(this.Comment);
            this.panel1.Controls.Add(this.ActionStateLabel);
            this.panel1.Controls.Add(this.ReadyButton);
            this.panel1.Controls.Add(this.ProductNameSpend);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 378);
            this.panel1.TabIndex = 27;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.DeleteCheck);
            this.panel2.Controls.Add(this.SummaryOrderLabel);
            this.panel2.Controls.Add(this.AddClientButton);
            this.panel2.Controls.Add(this.SummaryOrder);
            this.panel2.Controls.Add(this.DeleteButton);
            this.panel2.Location = new System.Drawing.Point(370, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 378);
            this.panel2.TabIndex = 28;
            // 
            // AddActionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(620, 399);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AddActionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление события";
            this.Load += new System.EventHandler(this.AddActionWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Payed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountOfNames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductPrice)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ActionTypeLabel;
        private System.Windows.Forms.Label ActionDateLabel;
        private System.Windows.Forms.Label PersonLabel;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.ComboBox ActionType;
        private System.Windows.Forms.DateTimePicker ActionDate;
        private System.Windows.Forms.TextBox Comment;
        private System.Windows.Forms.Button ReadyButton;
        private System.Windows.Forms.Label ActionStateLabel;
        private System.Windows.Forms.ComboBox ActionState;
        private System.Windows.Forms.NumericUpDown Payed;
        private System.Windows.Forms.Label PayedLabel;
        private System.Windows.Forms.NumericUpDown CountOfNames;
        private System.Windows.Forms.Label CountOfNamesLabel;
        private System.Windows.Forms.Label SummaryOrderLabel;
        private System.Windows.Forms.Label SummaryOrder;
        private System.Windows.Forms.Label ProductNameLabel;
        private System.Windows.Forms.Label ProductCountLabel;
        private System.Windows.Forms.Label ProductPriceLabel;
        private System.Windows.Forms.ComboBox ProductNameEarn;
        private System.Windows.Forms.NumericUpDown ProductCount;
        private System.Windows.Forms.NumericUpDown ProductPrice;
        private System.Windows.Forms.TextBox ProductNameSpend;
        private System.Windows.Forms.Button AddProductName;
        private System.Windows.Forms.ComboBox PersonName;
        private System.Windows.Forms.ComboBox DeleteCheck;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddClientButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}