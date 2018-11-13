using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyKeeper_2
{

    public partial class AddActionWindow : Form
    {
        private MoneyKeeperGeneral MKG;
        private int CountOfAddedNames = 0;
        private double totalOrderPrice = 0;

        private List<string> OrderedNames;
        private List<int> OrderedCounts;
        private List<double> OrderedPrices;
        private List<double> OrderedPayed;

        public AddActionWindow(MoneyKeeperGeneral MKG)
        {
            this.MKG = MKG;
            InitializeComponent();
        }

        private void setVisibility(bool ADL, bool AD, bool ASL, bool AS,
                                   bool CONL, bool CON, bool PNL, bool PNE,
                                   bool PNS, bool PCL, bool PC, bool PPL,
                                   bool PP, bool PL, bool P, bool APN,
                                   bool CLL, bool CLN, bool CL, bool C,
                                   bool DC, bool DB, bool ACB)
        {
            ActionDateLabel.Visible = ADL;
            ActionDate.Visible = AD;

            ActionStateLabel.Visible = ASL;
            ActionState.Visible = AS;

            CountOfNamesLabel.Visible = CONL;
            CountOfNames.Visible = CON;

            ProductNameLabel.Visible = PNL;
            ProductNameEarn.Visible = PNE;
            ProductNameSpend.Visible = PNS;

            ProductCountLabel.Visible = PCL;
            ProductCount.Visible = PC;

            ProductPriceLabel.Visible = PPL;
            ProductPrice.Visible = PP;

            PayedLabel.Visible = PL;
            Payed.Visible = P;

            AddProductName.Visible = APN;

            PersonLabel.Visible = CLL;
            PersonName.Visible = CLN;

            CommentLabel.Visible = CL;
            Comment.Visible = C;

            DeleteCheck.Visible = DC;
            DeleteButton.Visible = DB;

            AddClientButton.Visible = ACB;
        }

        private void ActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ActionType.SelectedIndex)
            {
                case 0: //EarnMoneyAction
                    setVisibility(true, true, true, true, true, true, true, true, false, true, true, true, 
                                  true, true, true, true, true, true, true, true, true, true, true);
                    Payed.Enabled = false;
                    ReadyButton.Enabled = false;
                    PersonLabel.Text = "Клиент:";
                    break;

                case 1: //SpendMoneyAction
                    setVisibility(true, true, false, false, true, true, true, false, true, true, true, true, 
                                  true, false, false, true, false, false, true, true, true, true, false);
                    Payed.Enabled = false;
                    ReadyButton.Enabled = false;
                    PersonLabel.Text = "Клиент:";
                    break;

                case 2: //Return Withdraw
                    setVisibility(true, true, false, false, false, false, false, false, false, false, false, false, 
                                  false, true, true, false, true, true, true, true, false, false, true);

                    Payed.Enabled = true;
                    ReadyButton.Enabled = true;
                    PersonLabel.Text = "Клиент:";
                    break;

                case 3: //Salary
                    setVisibility(true, true, false, false, false, false, false, false, false, false, false, false, 
                                  false, true, true, false, true, true, true, true, false, false, true);

                    Payed.Enabled = true;
                    ReadyButton.Enabled = true;
                    PersonLabel.Text = "Работник:";
                    break;
            }
        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            switch (ActionType.SelectedIndex)
            {
                case 0: //EarnMoneyAction

                    if (ProductNameEarn.Items.Count <= 0)
                    {
                        MessageBox.Show("Ранее не было добавлено ни одного наименования продукта! Событие не добавлено!");
                        return;
                    }

                    if (PersonName.Items.Count <= 0)
                    {
                        MessageBox.Show("Ранее не было зарегистрировано ни одного клиента! Событие не добавлено!");
                        return;
                    }

                    Order orderEarn = new Order(MKG.MoneyActions.MaxOrderID + 1);

                    for (int i = 0; i < CountOfNames.Value; i++)
                    {
                        MoneyAction MAEarn = new EarnMoneyAction(ActionType.Items[ActionType.SelectedIndex].ToString(),
                                                                 MKG.MoneyActions.MaxActionID + i + 1,
                                                                 MKG.MoneyActions.MaxOrderID + 1,
                                                                 ActionDate.Value,
                                                                 ActionState.Items[ActionState.SelectedIndex].ToString(),
                                                                 OrderedNames[i],
                                                                 OrderedCounts[i],
                                                                 OrderedPrices[i],
                                                                 OrderedPayed[i],
                                                                 PersonName.Text,
                                                                 Comment.Text);

                        orderEarn.AddAction(MAEarn);
                    }

                    MKG.MoneyActions.AddOrder(orderEarn); 
                    break;

                case 1: //SpendMoneyAction

                    Order orderSpend = new Order(MKG.MoneyActions.MaxOrderID + 1);

                    for (int i = 0; i < CountOfNames.Value; i++)
                    {
                        MoneyAction MASpend = new SpendMoneyAction(ActionType.Items[ActionType.SelectedIndex].ToString(),
                                                                   MKG.MoneyActions.MaxActionID + i + 1,
                                                                   MKG.MoneyActions.MaxOrderID + 1,
                                                                   ActionDate.Value,
                                                                   OrderedNames[i],
                                                                   OrderedCounts[i],
                                                                   OrderedPrices[i],
                                                                   Comment.Text);

                        orderSpend.AddAction(MASpend);
                    }

                    MKG.MoneyActions.AddOrder(orderSpend);
                    break;

                case 2: //Return Withdraw

                    if (Payed.Value == 0)
                    {
                        MessageBox.Show("Нулевое значение недопустимо!");
                        return;
                    }

                    Order orderReturnWithdraw = new Order(MKG.MoneyActions.MaxOrderID + 1);

                    MoneyAction MAWithdraw = new ReturnWithdraw(MKG.MoneyActions.MaxActionID + 1,
                                                                MKG.MoneyActions.MaxOrderID + 1,
                                                                ActionDate.Value,
                                                                Convert.ToDouble(Payed.Value),
                                                                PersonName.Text,
                                                                Comment.Text);

                    orderReturnWithdraw.AddAction(MAWithdraw);
                    MKG.MoneyActions.AddOrder(orderReturnWithdraw);
                    break;

                case 3: //Salary

                    if (Payed.Value == 0)
                    {
                        MessageBox.Show("Нулевое значение недопустимо!");
                        return;
                    }

                    Order orderSalary = new Order(MKG.MoneyActions.MaxOrderID + 1);

                    MoneyAction MASalary = new Salary(MKG.MoneyActions.MaxActionID + 1,
                                                      MKG.MoneyActions.MaxOrderID + 1,
                                                      ActionDate.Value,
                                                      Convert.ToDouble(Payed.Value),
                                                      PersonName.Text,
                                                      Comment.Text);

                    orderSalary.AddAction(MASalary);
                    MKG.MoneyActions.AddOrder(orderSalary);
                    break;
            }

            MKG.Save(Constants.ORDERS_INFO_PATH,
                     Constants.NAMES_INFO_PATH,
                     Constants.CLIENTS_INFO_PATH);

            this.Close();
        }

        private void AddActionWindow_Load(object sender, EventArgs e)
        {
            Constants.ScaleForm(this, Constants.SCALE_X, Constants.SCALE_Y);
            ActionType.SelectedIndex = 0;
            ActionState.SelectedIndex = 0;

            for (int i = 0; i < MKG.ProductNames.CountOfNominations; i++)
                ProductNameEarn.Items.Add(MKG.ProductNames[i]);

            if (ProductNameEarn.Items.Count > 0)
                ProductNameEarn.SelectedIndex = 0;

            for (int i = 0; i < MKG.ClientNames.CountOfClients; i++)
                PersonName.Items.Add(MKG.ClientNames[i].Name);

            if (PersonName.Items.Count > 0)
                PersonName.SelectedIndex = 0;

            OrderedNames = new List<string>();
            OrderedCounts = new List<int>();
            OrderedPrices = new List<double>();
            OrderedPayed = new List<double>();
        }

        private void CountOfNames_ValueChanged(object sender, EventArgs e)
        {
            AddProductName.Text = "Добавить наименование. Осталось: " + CountOfNames.Value;
        }

        private void AddProductName_Click(object sender, EventArgs e)
        {
            if (ActionType.SelectedIndex == 0 && ActionState.SelectedIndex == 0)
                Payed.Value = (decimal)ProductCount.Value * ProductPrice.Value;

            if (CountOfAddedNames == 0)
            {
                ActionType.Enabled = false;
                CountOfNames.Enabled = false;
                ActionState.Enabled = false;
                DeleteButton.Enabled = true;
            }

            OrderedNames.Add(ActionType.SelectedIndex == 0? ProductNameEarn.Text : ProductNameSpend.Text);
            OrderedCounts.Add((int)ProductCount.Value);
            OrderedPrices.Add(Convert.ToDouble(ProductPrice.Value));
            OrderedPayed.Add(Convert.ToDouble(Payed.Value));

            double actionPrice = Convert.ToDouble(ProductPrice.Value * ProductCount.Value);
            string ProductInfo = (ActionType.SelectedIndex == 0 ? ProductNameEarn.Text : ProductNameSpend.Text) + ": " + ProductCount.Value + " x " + ProductPrice.Value + " = " + actionPrice + "\n";
            SummaryOrder.Text += ProductInfo;
            DeleteCheck.Items.Add(ProductInfo);
            totalOrderPrice += actionPrice;

            ProductNameSpend.Text = "";
            ProductCount.Value = 0;
            ProductPrice.Value = 0;
            Payed.Value = 0;

            CountOfAddedNames++;
            AddProductName.Text = "Добавить наименование. Осталось: " + (CountOfNames.Value - CountOfAddedNames).ToString();

            if (CountOfAddedNames == CountOfNames.Value)
            {
                AddProductName.Enabled = false;
                ReadyButton.Enabled = true;
                SummaryOrder.Text += "Итого: " + totalOrderPrice.ToString();
            }
        }

        private void ActionState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActionType.SelectedIndex != 0)
                return;

            if (ActionState.SelectedIndex == 0)
            {
                Payed.Value = (decimal)ProductCount.Value * ProductPrice.Value;
                Payed.Enabled = false;
            }
            else
                Payed.Enabled = true;
        }

        private void ProductParams_ValueChanged(object sender, EventArgs e)
        {
            if (ActionType.SelectedIndex != 0)
                return;

            Payed.Maximum = (decimal)ProductCount.Value * ProductPrice.Value;

            if (ActionState.SelectedIndex == 0)
                Payed.Value = (decimal)ProductCount.Value * ProductPrice.Value;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (DeleteCheck.Items.Count == 0 || DeleteCheck.SelectedIndex == -1)
                return;

            double actionPrice = OrderedCounts[DeleteCheck.SelectedIndex] * OrderedPrices[DeleteCheck.SelectedIndex];
            totalOrderPrice -= actionPrice;

            OrderedNames.RemoveAt(DeleteCheck.SelectedIndex);
            OrderedCounts.RemoveAt(DeleteCheck.SelectedIndex);
            OrderedPrices.RemoveAt(DeleteCheck.SelectedIndex);
            OrderedPayed.RemoveAt(DeleteCheck.SelectedIndex);
            DeleteCheck.Items.RemoveAt(DeleteCheck.SelectedIndex);
            
            CountOfAddedNames--;

            string ProductInfo = "";
            for (int i = 0; i < OrderedNames.Count; i++)
                ProductInfo += OrderedNames[i] + ": " + OrderedCounts[i] + " x " + OrderedPrices[i] + " = " + OrderedCounts[i] * OrderedPrices[i] + "\n";

            SummaryOrder.Text = ProductInfo;
            AddProductName.Text = "Добавить наименование. Осталось: " + (CountOfNames.Value - CountOfAddedNames).ToString();

            AddProductName.Enabled = true;
            if (CountOfAddedNames == 0)
                DeleteButton.Enabled = false;
            ReadyButton.Enabled = false;
        }

        private void AddClientButton_Click(object sender, EventArgs e)
        {
            AddClientForm ACF = new AddClientForm(MKG, true);
            ACF.ShowDialog();
            PersonName.Items.Add(MKG.ClientNames[MKG.ClientNames.CountOfClients - 1].Name);
        }
    }
}
