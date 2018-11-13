using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Drawing.Printing;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;

namespace MoneyKeeper_2
{
    public partial class ActionViewerForm : Form
    {
        private Word.Application Application;
        private Word.Document Document;
        private Word.Range R;
        private Word.Table T;
        private Object missingObj = System.Reflection.Missing.Value;
        private Object trueObj = true;
        private Object falseObj = false;
        private int RESIZE_DELTA;
        private const int MONEY_TABLE_HEIGHT = 403;
        private const int PRINT_TABLE_HEIGHT = 291;
        private const int PANEL_LOC_Y = 421;
        private const int SPLITTER_HEIGHT = 10;
        private List<int> SelectedDGWCellsIndexesMain = new List<int>();
        private List<int> SelectedDGWCellsIndexesPrint = new List<int>();


        private MoneyKeeperGeneral MKG;
        private List<OrderTable.ActionFits> PredicateList;

        public const int DATA_MODE_NO_SUMMARY = 0;
        public const int DATA_MODE_CLIENT = 1;
        public const int DATA_MODE_PRODUCT = 2;
        public const int DATA_MODE_PERIOD = 3;
        public const int DATA_MODE_DAYS = 4;

        private RadioButton ClientRB;
        private RadioButton ProductRB;
        private RadioButton DateRB;
        private RadioButton DayRB;

        public ActionViewerForm(MoneyKeeperGeneral MKG)
        {
            this.MKG = MKG;
            PredicateList = new List<OrderTable.ActionFits>();
            InitializeComponent();
        }

        private void ActionViewerForm_Load(object sender, EventArgs e)
        {
            RESIZE_DELTA = (int)(-(967 * Constants.SCALE_Y - this.Height) / 2);
            Constants.ScaleForm(this, Constants.SCALE_X, Constants.SCALE_Y);
            ActionTypeValue.SelectedIndex = 0;
            ActionState.SelectedIndex = 0;

            for (int i = 0; i < MKG.ProductNames.CountOfNominations; i++)
                ProductNameValue.Items.Add(MKG.ProductNames[i]);

            if (ProductNameValue.Items.Count > 0)
                ProductNameValue.SelectedIndex = 0;

            for (int i = 0; i < MKG.ClientNames.CountOfClients; i++)
                PersonValue.Items.Add(MKG.ClientNames[i].Name);

            if (PersonValue.Items.Count > 0)
                PersonValue.SelectedIndex = 0;

            UpdateDataGrid(new List<int>(new int[] { DATA_MODE_NO_SUMMARY }));
            moneyActionDataGrid.FirstDisplayedScrollingRowIndex = moneyActionDataGrid.Rows.Count - 1;
        }

        private void DataPush(string number, string id, string orderId,
                              string actionType, string date, string client, 
                              string productName, string productCount, string productPrice,
                              string totalPrice, string earned, string spent,
                              string withdraw, string state, string comment, DataGridView dgw, params Color[] colors)
        {
            dgw.Rows.Add();
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_NUMBER].Value = number;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_ID].Value = id;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_ORDER_ID].Value = orderId;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_TYPE].Value = actionType;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_DATE].Value = date;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_STATE].Value = state;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_PRODUCT_NAME].Value = productName;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_PRODUCT_COUNT].Value = productCount;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_PRODUCT_PRICE].Value = productPrice;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_TOTAL_PRICE].Value = totalPrice;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_EARNED_MONEY].Value = earned;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_SPENT_MONEY].Value = spent;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_DEBT].Value = withdraw;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_CLIENT].Value = client;
            dgw.Rows[dgw.RowCount - 1].Cells[Constants.COLUMN_NAME_ACTION_COMMENT].Value = comment;

            for (int i = 0; i < dgw.ColumnCount && i < colors.Length; i++)
                dgw.Rows[dgw.RowCount - 1].Cells[i].Style.BackColor = colors[i];
        }

        private void UpdateDataGrid(List<int> modes)
        {
            moneyActionDataGrid.Rows.Clear();
            List<MoneyAction> SelectedActions = MKG.MoneyActions.GetListOfSelectedActions(PredicateList.ToArray());

            List<PersonSummary> SummaryByClient = new List<PersonSummary>();
            List<ProductSummary> SummaryByProduct = new List<ProductSummary>();

            DateSummary SummaryByPeriod = new DateSummary(ActionDateLower.Value.Date, ActionDateUpper.Value.Date);

            int DaysCount = (int)(ActionDateUpper.Value.Date - ActionDateLower.Value.Date).TotalDays + 1;
            List<DateSummary> SummaryByDays = new List<DateSummary>();
            for (int i = 0; i < DaysCount; i++)
                SummaryByDays.Add(new DateSummary(ActionDateLower.Value.Date.AddDays(i), ActionDateLower.Value.Date.AddDays(i + 1)));

            for (int i = 0; i < SelectedActions.Count; i++)
            {
                bool personAdded = false;
                foreach (PersonSummary personSum in SummaryByClient)
                    if (personSum.Person == SelectedActions[i].Person &&
                        SelectedActions[i].ActionTypeDefinition != Constants.ACTION_TYPE_DEF_SPENDING)
                    {
                        personSum.AddAction(SelectedActions[i]);
                        personAdded = true;
                        break;
                    }

                if (!personAdded && SelectedActions[i].ActionTypeDefinition != Constants.ACTION_TYPE_DEF_SPENDING)
                {
                    PersonSummary personSum = new PersonSummary(SelectedActions[i].Person);
                    personSum.AddAction(SelectedActions[i]);
                    SummaryByClient.Add(personSum);
                }//Summary by client

                bool productAdded = false;
                foreach (ProductSummary productSum in SummaryByProduct)
                    if (productSum.ProductName == SelectedActions[i].ProductName)
                    {
                        productSum.AddAction(SelectedActions[i]);
                        productAdded = true;
                        break;
                    }

                if (!productAdded)
                {
                    ProductSummary productSum = new ProductSummary(SelectedActions[i].ProductName);
                    productSum.AddAction(SelectedActions[i]);
                    SummaryByProduct.Add(productSum);
                }//Summary by product

                SummaryByPeriod.AddAction(SelectedActions[i]);//summary by period

                int DayIndex = (int)(SelectedActions[i].Date - ActionDateLower.Value.Date).TotalDays;

                if (DayIndex >= 0 && DayIndex < SummaryByDays.Count)
                {
                    if (SummaryByDays[DayIndex] == null)
                        SummaryByDays[DayIndex] = new DateSummary(SelectedActions[i].Date, SelectedActions[i].Date.AddDays(1));
                    else
                        SummaryByDays[DayIndex].AddAction(SelectedActions[i]);
                }


                DataPush((i + 1).ToString(),
                         SelectedActions[i].ID.ToString(),
                         SelectedActions[i].OrderID.ToString(),
                         SelectedActions[i].ActionTypeDefinition,
                         SelectedActions[i].Date.ToShortDateString(),
                         SelectedActions[i].Person,
                         SelectedActions[i].ProductName,
                         SelectedActions[i].ProductCount.ToString(),
                         SelectedActions[i].ProductPrice.ToString(),
                         SelectedActions[i].IsEarning ? SelectedActions[i].TotalPrice.ToString() : "0",
                         SelectedActions[i].Earned.ToString(),
                         SelectedActions[i].Spent.ToString(),
                         (SelectedActions[i].TotalPrice - SelectedActions[i].Payed).ToString(),
                         SelectedActions[i].State,
                         SelectedActions[i].Comment,
                         moneyActionDataGrid,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                         SelectedActions[i].IsEarning ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                         SelectedActions[i].TotalPrice - SelectedActions[i].Payed <= 0 ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                         SelectedActions[i].State == "" ? Constants.NORMAL_CELL_COLOR : (SelectedActions[i].State == Constants.STATE_DEBT || SelectedActions[i].State == Constants.STATE_SALDO ? Constants.DEBT_COLOR : Constants.NO_DEBT_COLOR),
                         Constants.NORMAL_CELL_COLOR);
            }

            DateTime MinDTClient = SummaryByClient.Count == 0 ? new DateTime() : SummaryByClient[0].dateStart;
            DateTime MaxDTClient = SummaryByClient.Count == 0 ? new DateTime() : SummaryByClient[0].dateFinish;

            DateTime MinDTProduct = SummaryByClient.Count == 0 ? new DateTime() : SummaryByProduct[0].dateStart;
            DateTime MaxDTProduct = SummaryByClient.Count == 0 ? new DateTime() : SummaryByProduct[0].dateFinish;

            if (modes.IndexOf(DATA_MODE_CLIENT) != -1)
            {
                moneyActionDataGrid.Rows.Add();
                DataPush("Итог", "по", "клиентам", "", "", "", "", "", "", "", "", "", "", "", "", moneyActionDataGrid);
                moneyActionDataGrid.Rows.Add();

                for (int i = 0; i < SummaryByClient.Count; i++)
                {
                    if (MinDTClient > SummaryByClient[i].dateStart)
                        MinDTClient = SummaryByClient[i].dateStart;

                    if (MaxDTClient < SummaryByClient[i].dateFinish)
                        MaxDTClient = SummaryByClient[i].dateFinish;
                }

                for (int i = 0; i < SummaryByClient.Count; i++)
                    DataPush((i + 1).ToString(),
                             String.Join("; ", SummaryByClient[i].IDs),
                             String.Join("; ", SummaryByClient[i].OrderIDs),
                             SummaryByClient[i].ActionTypeDefinition,
                             "c " + MinDTClient.ToShortDateString() + " по " + MaxDTClient.ToShortDateString(),
                             SummaryByClient[i].Person,
                             String.Join("; \r\n", SummaryByClient[i].ProductNames),
                             String.Join("; \r\n", SummaryByClient[i].ProductCounts),
                             GetPricesRange(SummaryByClient[i].MinProductPrices, SummaryByClient[i].MaxProductPrices),
                             SummaryByClient[i].TotalPrice.ToString(),
                             SummaryByClient[i].Earned.ToString(),
                             "0",
                             (SummaryByClient[i].TotalPrice - SummaryByClient[i].Earned).ToString(),
                             SummaryByClient[i].State,
                             "",
                             moneyActionDataGrid,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             SummaryByClient[i].IsEarning ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             SummaryByClient[i].TotalPrice - SummaryByClient[i].Earned <= 0 ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                             SummaryByClient[i].State == Constants.STATE_SALDO || SummaryByClient[i].State == Constants.STATE_DEBT ? Constants.DEBT_COLOR : 
                             SummaryByClient[i].State == Constants.STATE_OWN_DEBT? Constants.MINUS_DEBT_COLOR : Constants.NO_DEBT_COLOR, Constants.NORMAL_CELL_COLOR);
            }

            if (modes.IndexOf(DATA_MODE_PRODUCT) != -1)
            {
                moneyActionDataGrid.Rows.Add();
                DataPush("Итог", "по", "продуктам", "", "", "", "", "", "", "", "", "", "", "", "", moneyActionDataGrid);
                moneyActionDataGrid.Rows.Add();

                for (int i = 0; i < SummaryByProduct.Count; i++)
                {
                    if (MinDTProduct > SummaryByProduct[i].dateStart)
                        MinDTProduct = SummaryByProduct[i].dateStart;

                    if (MaxDTProduct < SummaryByProduct[i].dateFinish)
                        MaxDTProduct = SummaryByProduct[i].dateFinish;
                }

                for (int i = 0; i < SummaryByProduct.Count; i++)
                    DataPush((i + 1).ToString(),
                             String.Join("; ", SummaryByProduct[i].IDs),
                             String.Join("; ", SummaryByProduct[i].OrderIDs),
                             SummaryByProduct[i].ActionTypeDefinition,
                             "c " + MinDTProduct.ToShortDateString() + " по " + MaxDTProduct.ToShortDateString(),
                             String.Join("; \r\n", SummaryByProduct[i].Persons),
                             SummaryByProduct[i].ProductName,
                             SummaryByProduct[i].ProductCount.ToString(),
                             GetPricesRange(new double[] { SummaryByProduct[i].MinProductPrice }.ToList(), new double[] { SummaryByProduct[i].MaxProductPrice }.ToList()),
                             SummaryByProduct[i].IsEaring ? SummaryByProduct[i].TotalPrice.ToString() : "0",
                             SummaryByProduct[i].Earned.ToString(),
                             SummaryByProduct[i].Spent.ToString(),
                             (SummaryByProduct[i].TotalPrice - (SummaryByProduct[i].IsEaring ? SummaryByProduct[i].Earned : SummaryByProduct[i].Spent)).ToString(),
                             "",
                             "",
                             moneyActionDataGrid,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             SummaryByProduct[i].IsEaring ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             SummaryByProduct[i].TotalPrice - (SummaryByProduct[i].IsEaring ? SummaryByProduct[i].Earned : SummaryByProduct[i].Spent) <= 0 ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR);
            }

            if (modes.IndexOf(DATA_MODE_PERIOD) != -1)
            {
                moneyActionDataGrid.Rows.Add();
                DataPush("Итог", "за", "период", "с " + SummaryByPeriod.dateStart.ToShortDateString(),
                         "по " + SummaryByPeriod.dateFinish.ToShortDateString(), "", "", "", "", "",
                         "", "", "", "", "", moneyActionDataGrid);
                moneyActionDataGrid.Rows.Add();

                DataPush("1",
                         String.Join("; ", SummaryByPeriod.IDs_earned),
                         String.Join("; ", SummaryByPeriod.OrderIDs_earned),
                         "",
                         "c " + SummaryByPeriod.dateStart.ToShortDateString() + " по " + SummaryByPeriod.dateFinish.ToShortDateString(),
                         String.Join("; \r\n", SummaryByPeriod.Persons_earned),
                         String.Join("; \r\n", SummaryByPeriod.ProductNames_earned),
                         String.Join("; \r\n", SummaryByPeriod.ProductCounts_earned),
                         GetPricesRange(SummaryByPeriod.MinProductPrices_earned, SummaryByPeriod.MaxProductPrices_earned),
                         SummaryByPeriod.TotalPrice_earned.ToString(),
                         SummaryByPeriod.Earned.ToString(),
                         SummaryByPeriod.TotalPrice_spend.ToString(),
                         (SummaryByPeriod.TotalPrice_earned - SummaryByPeriod.Earned).ToString(),
                         "",
                         "Остаток: " + (SummaryByPeriod.Earned - SummaryByPeriod.TotalPrice_spend).ToString(),
                         moneyActionDataGrid,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                         SummaryByPeriod.TotalPrice_earned - SummaryByPeriod.Earned <= 0 ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                         Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR);
            }

            if (modes.IndexOf(DATA_MODE_DAYS) != -1)
            {
                moneyActionDataGrid.Rows.Add();
                DataPush("Итог", "за", "период", "с " + ActionDateLower.Value.Date.ToShortDateString(),
                         "по " + ActionDateUpper.Value.Date.ToShortDateString(), "поденно", "", "", "", "",
                         "", "", "", "", "", moneyActionDataGrid);
                moneyActionDataGrid.Rows.Add();

                for (int i = 0; i < SummaryByDays.Count; i++)
                {
                    DataPush("1",
                             String.Join("; ", SummaryByDays[i].IDs_earned),
                             String.Join("; ", SummaryByDays[i].OrderIDs_earned),
                             "",
                             "c " + SummaryByDays[i].dateStart.ToShortDateString() + " по " + SummaryByDays[i].dateFinish.ToShortDateString(),
                             String.Join("; \r\n", SummaryByDays[i].Persons_earned),
                             String.Join("; \r\n", SummaryByDays[i].ProductNames_earned),
                             String.Join("; \r\n", SummaryByDays[i].ProductCounts_earned),
                             GetPricesRange(SummaryByDays[i].MinProductPrices_earned, SummaryByDays[i].MaxProductPrices_earned),
                             SummaryByDays[i].TotalPrice_earned.ToString(),
                             SummaryByDays[i].Earned.ToString(),
                             SummaryByDays[i].TotalPrice_spend.ToString(),
                             (SummaryByDays[i].TotalPrice_earned - SummaryByDays[i].Earned).ToString(),
                             "",
                             "Остаток: " + (SummaryByDays[i].Earned - SummaryByDays[i].TotalPrice_spend).ToString(),
                             moneyActionDataGrid,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR,
                             SummaryByDays[i].TotalPrice_earned - SummaryByDays[i].Earned <= 0 ? Constants.NO_DEBT_COLOR : Constants.DEBT_COLOR,
                             Constants.NORMAL_CELL_COLOR, Constants.NORMAL_CELL_COLOR);
                }
            }
            ColorSelectedRow(moneyActionDataGrid, SelectedDGWCellsIndexesMain);
        }

        private void UpdatePredicateList()
        {
            PredicateList.Clear();

            if(ActionIDLabel.Checked)
                PredicateList.Add(action => action.ID.ToString() == IDValue.Text);

            if(OrderIDLabel.Checked)
                PredicateList.Add(action => action.OrderID.ToString() == OrderIDValue.Text);

            if (ActionTypeLabel.Checked)
                PredicateList.Add(action => (ActionTypeValue.Items.Count == 0? false : (action.ActionTypeDefinition == ActionTypeValue.Items[ActionTypeValue.SelectedIndex].ToString())));

            if (ActionDateLabel.Checked)
                PredicateList.Add(action => action.Date >= ActionDateLower.Value.Date && action.Date <= ActionDateUpper.Value.Date);

            if (ActionStateLabel.Checked)
                PredicateList.Add(action => action.State == ActionState.Items[ActionState.SelectedIndex].ToString());

            if (ProductNameLabel.Checked)
                PredicateList.Add(action => action.ProductName == ProductNameValue.Items[ProductNameValue.SelectedIndex].ToString());

            if (TotalPriceLabel.Checked)
                PredicateList.Add(action => action.TotalPrice >= (int)TotalPriceLower.Value && action.TotalPrice <= (int)TotalPriceUpper.Value);

            if (PayedLabel.Checked)
                PredicateList.Add(action => (double)action.TotalPrice * 100 / action.Payed >= (int)PayedLower.Value &&
                                            (double)action.TotalPrice * 100 / action.Payed <= (int)PayedUpper.Value);

            if (PersonLabel.Checked)
                PredicateList.Add(action => action.Person == PersonValue.Items[PersonValue.SelectedIndex].ToString());
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void TotalPriceBorder_ValueChanged(object sender, EventArgs e)
        {
            TotalPriceLower.Maximum = TotalPriceUpper.Value;
            TotalPriceUpper.Minimum = TotalPriceLower.Value;

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void PayedBorder_ValueChanged(object sender, EventArgs e)
        {
            PayedLower.Maximum = PayedUpper.Value;
            PayedUpper.Minimum = PayedLower.Value;

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void DateBordersChanged(object sender, EventArgs e)
        {
            ActionDateLower.MaxDate = ActionDateUpper.Value;
            ActionDateUpper.MinDate = ActionDateLower.Value;

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private List<int> SummaryUpdate()
        {
            List<int> modes = new List<int>();

            if ((SettingsPresetLabel.Checked && ClientRB.Checked) || (!SettingsPresetLabel.Checked && SummaryByClientLabel.Checked))
                modes.Add(DATA_MODE_CLIENT);

            if ((SettingsPresetLabel.Checked && ProductRB.Checked) || (!SettingsPresetLabel.Checked && SummaryByNameLabel.Checked))
                modes.Add(DATA_MODE_PRODUCT);

            if ((SettingsPresetLabel.Checked && DateRB.Checked) || (!SettingsPresetLabel.Checked && SummaryByDateLabel.Checked))
                modes.Add(DATA_MODE_PERIOD);

            if ((SettingsPresetLabel.Checked && DayRB.Checked) || (!SettingsPresetLabel.Checked && SummaryByDaysLabel.Checked))
                modes.Add(DATA_MODE_DAYS);

            return modes;
        }

        private void setChecked(bool id, bool orderId, bool actionType, bool date, 
                                bool state, bool productName, bool totalPrice, 
                                bool payedPercent, bool client)
        {
            ActionIDLabel.Checked = id;
            OrderIDLabel.Checked = orderId;
            ActionTypeLabel.Checked = actionType;
            ActionDateLabel.Checked = date;
            ActionStateLabel.Checked = state;
            ProductNameLabel.Checked = productName;
            TotalPriceLabel.Checked = totalPrice;
            PayedLabel.Checked = payedPercent;
            PersonLabel.Checked = client;
        }

        private void setEnabled(bool id, bool orderId, bool actionType, bool date, 
                                bool state, bool productName, bool totalPrice, 
                                bool payedPercent, bool client)
        {
            ActionIDLabel.Enabled = id;
            OrderIDLabel.Enabled = orderId;
            ActionTypeLabel.Enabled = actionType;
            ActionDateLabel.Enabled = date;
            ActionStateLabel.Enabled = state;
            ProductNameLabel.Enabled = productName;
            TotalPriceLabel.Enabled = totalPrice;
            PayedLabel.Enabled = payedPercent;
            PersonLabel.Enabled = client;
        }

        private void SetDefaultPreset()
        {
            setEnabled(true, true, true, true, true, true, true, true, true);
            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void SummaryByClientLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsPresetLabel.Checked && ClientRB.Checked)
            {
                setChecked(false, false, false, ActionDateLabel.Checked, false, false, false, false, PersonLabel.Checked);
                setEnabled(false, false, false, true, false, false, false, false, true);
            }

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void SummaryByNameLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsPresetLabel.Checked && ProductRB.Checked)
            {
                setChecked(false, false, false, ActionDateLabel.Checked, false, ProductNameLabel.Checked, false, false, false);
                setEnabled(false, false, false, true, false, true, false, false, false);
            }

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void SummaryByDateLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsPresetLabel.Checked && DateRB.Checked)
            {
                setChecked(false, false, false, ActionDateLabel.Checked, false, false, false, false, false);
                setEnabled(false, false, false, true, false, false, false, false, false);
            }

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void SummaryByDaysLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsPresetLabel.Checked && DayRB.Checked)
            {
                setChecked(false, false, false, ActionDateLabel.Checked, false, false, false, false, false);
                setEnabled(false, false, false, true, false, false, false, false, false);
            }

            UpdatePredicateList();
            UpdateDataGrid(SummaryUpdate());
        }

        private void SettingsPresetLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (SettingsPresetLabel.Checked)
            {
                ClientRB = new RadioButton();
                ClientRB.Text = "Результат по клиентам";
                ClientRB.Location = SummaryByClientLabel.Location;
                ClientRB.Size = SummaryByClientLabel.Size;
                ClientRB.CheckedChanged += SummaryByClientLabel_CheckedChanged;
                SummaryByClientLabel.Visible = false;
                this.Controls.Add(ClientRB);

                ProductRB = new RadioButton();
                ProductRB.Text = "Результат по продуктам";
                ProductRB.Location = SummaryByNameLabel.Location;
                ProductRB.Size = SummaryByNameLabel.Size;
                ProductRB.CheckedChanged += SummaryByNameLabel_CheckedChanged;
                SummaryByNameLabel.Visible = false;
                this.Controls.Add(ProductRB);

                DateRB = new RadioButton();
                DateRB.Text = "Результат по промежутку времени";
                DateRB.Location = SummaryByDateLabel.Location;
                DateRB.Size = SummaryByDateLabel.Size;
                DateRB.CheckedChanged += SummaryByDateLabel_CheckedChanged;
                SummaryByDateLabel.Visible = false;
                this.Controls.Add(DateRB);

                DateRB = new RadioButton();
                DateRB.Text = "Результат поденно";
                DateRB.Location = SummaryByDaysLabel.Location;
                DateRB.Size = SummaryByDaysLabel.Size;
                DateRB.CheckedChanged += SummaryByDaysLabel_CheckedChanged;
                SummaryByDaysLabel.Visible = false;
                this.Controls.Add(DayRB);

                ClientRB.Checked = true;
            }
            else
            {
                this.Controls.Remove(ClientRB);
                this.Controls.Remove(ProductRB);
                this.Controls.Remove(DateRB);
                this.Controls.Remove(DayRB);

                SummaryByClientLabel.Visible = true;
                SummaryByNameLabel.Visible = true;
                SummaryByDateLabel.Visible = true;
                SummaryByDaysLabel.Visible = true;

                SetDefaultPreset();
            }
        }

        private void AddToPrintTableSelectedButton_Click(object sender, EventArgs e)
        {
            List<int> toPrint = new List<int>();
            foreach (DataGridViewCell i in moneyActionDataGrid.SelectedCells)
                if (!toPrint.Contains(i.RowIndex))
                    toPrint.Add(i.RowIndex);

            for (int i = toPrint.Count - 1; i >= 0; i--)
            {
                PrintDataGrid.Rows.Add();
                for (int j = 0; j < moneyActionDataGrid.Rows[i].Cells.Count; j++)
                {
                    PrintDataGrid.Rows[PrintDataGrid.RowCount - 1].Cells[j].Value = moneyActionDataGrid.Rows[toPrint[i]].Cells[j].Value;
                    PrintDataGrid.Rows[PrintDataGrid.RowCount - 1].Cells[j].Style.BackColor = moneyActionDataGrid.Rows[toPrint[i]].Cells[j].Style.BackColor == Constants.SELECT_CELL_COLOR ?
                                                                                              Constants.NORMAL_CELL_COLOR : moneyActionDataGrid.Rows[toPrint[i]].Cells[j].Style.BackColor;
                }
            }

            ColorSelectedRow(PrintDataGrid, SelectedDGWCellsIndexesPrint);
        }

        private void PrintReadyButton_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            FileStream FS = File.Create(Environment.CurrentDirectory + "/Data/Print.doc");
            FS.Close();

            Application = new Word.Application();
            Object PrintPath = Environment.CurrentDirectory + "/Data/Print.doc";

            Document = Application.Documents.Add(ref PrintPath, ref missingObj, ref missingObj, ref missingObj);
            Document.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
            R = Document.Sections[1].Range;

            int CountOfVisibleCols = 0;
            for (int i = 0; i < PrintDataGrid.ColumnCount; i++)
                if (PrintDataGrid.Columns[i].Visible)
                    CountOfVisibleCols++;

            T = Document.Tables.Add(R, PrintDataGrid.RowCount + 1, CountOfVisibleCols, missingObj, missingObj);
            T.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            T.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

            for (int i = 0, k = 1; i < PrintDataGrid.ColumnCount; i++)
                if (PrintDataGrid.Columns[i].Visible)
                {
                    T.Rows[1].Cells[k].Width = PrintDataGrid.Columns[i].Width / 2f;
                    T.Rows[1].Cells[k++].Range.Text = PrintDataGrid.Columns[i].HeaderText;
                }

            for (int i = 0; i < PrintDataGrid.RowCount; i++)
                for (int j = 0, k = 1; j < PrintDataGrid.ColumnCount; j++)
                    if (PrintDataGrid.Columns[j].Visible)
                    {
                        T.Rows[i + 2].Cells[k].Width = PrintDataGrid.Columns[j].Width / 2f;
                        T.Rows[i + 2].Cells[k].Range.Text = PrintDataGrid.Rows[i].Cells[j].Value == null ? "" : PrintDataGrid.Rows[i].Cells[j].Value.ToString();

                        if (PrintDataGrid.Rows[i].Cells[j].Style.BackColor == Constants.NO_DEBT_COLOR)
                            T.Rows[i + 2].Cells[k].Shading.BackgroundPatternColor = Word.WdColor.wdColorGreen;
                        else if (PrintDataGrid.Rows[i].Cells[j].Style.BackColor == Constants.DEBT_COLOR)
                            T.Rows[i + 2].Cells[k].Shading.BackgroundPatternColor = Word.WdColor.wdColorRed;
                        else if (PrintDataGrid.Rows[i].Cells[j].Style.BackColor == Constants.MINUS_DEBT_COLOR)
                            T.Rows[i + 2].Cells[k].Shading.BackgroundPatternColor = Word.WdColor.wdColorBlue;

                        k++;
                    }

            Document.Save();
            Document.Close(missingObj, missingObj, missingObj);
            this.UseWaitCursor = false;
        }

        public string GetPricesRange(List<double> MinPrices, List<double> MaxPrices)
        {
            if(MinPrices.Count != MaxPrices.Count)
                return "";

            if (MinPrices.Count == 0)
                return "";

            string result = "";

            for (int i = 0; i < MinPrices.Count - 1; i++)
                result += (MinPrices[i] == MaxPrices[i]? MinPrices[i].ToString() : (MinPrices[i] + " - " + MaxPrices[i])) + "; ";

            return result + (MinPrices[MinPrices.Count - 1] == MaxPrices[MinPrices.Count - 1]? MinPrices[MinPrices.Count - 1].ToString() : 
                             MinPrices[MinPrices.Count - 1] + " - " + MaxPrices[MaxPrices.Count - 1]);
        }

        private void UpdatePrintTableVisibility(string columnName, object CB)
        {
            if (PrintDataGrid.Columns[columnName] == null || !CB.GetType().Equals(typeof(CheckBox)))
                return;

            if (!(CB as CheckBox).Checked)
                PrintDataGrid.Columns[columnName].Visible = false;
            else
                PrintDataGrid.Columns[columnName].Visible = true;
        }

        private void NumberPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_NUMBER_PRINT, sender);
        }

        private void IDPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_ID_PRINT, sender);
        }

        private void OrderIDPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_ORDER_ID_PRINT, sender);
        }

        private void TypePrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_TYPE_PRINT, sender);
        }

        private void DatePrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_DATE_PRINT, sender);
        }

        private void ActionStatePrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_STATE_PRINT, sender);
        }

        private void ActionProductNamePrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_PRODUCT_NAME_PRINT, sender);
        }

        private void ActionProductCountPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_PRODUCT_COUNT_PRINT, sender);
        }

        private void ActionProductPricePrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_PRODUCT_PRICE_PRINT, sender);
        }

        private void ActionTotalPricePrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_TOTAL_PRICE_PRINT, sender);
        }

        private void EarnedPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_EARNED_MONEY_PRINT, sender);
        }

        private void SpentPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_SPENT_MONEY_PRINT, sender);
        }

        private void DebtPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_DEBT_PRINT, sender);
        }

        private void ClientPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_CLIENT_PRINT, sender);
        }

        private void ActionCommentPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePrintTableVisibility(Constants.COLUMN_NAME_ACTION_COMMENT_PRINT, sender);
        }

        private void PrintTableRemoveRows_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> RowsToRemove = new List<int>();

            foreach (DataGridViewCell i in PrintDataGrid.SelectedCells)
                if (!RowsToRemove.Contains(i.RowIndex))
                    RowsToRemove.Add(i.RowIndex);

            foreach (int i in RowsToRemove)
                if (i < PrintDataGrid.RowCount)
                    PrintDataGrid.Rows.RemoveAt(i);
        }

        private void ActionViewerForm_Resize(object sender, EventArgs e)
        {
            int NEW_MONEY_TABLE_HEIGHT = (int)(MONEY_TABLE_HEIGHT * Constants.SCALE_Y + RESIZE_DELTA);
            moneyActionDataGrid.Height = NEW_MONEY_TABLE_HEIGHT;

            int NEW_PANEL_LOC = (int)(PANEL_LOC_Y * Constants.SCALE_Y + RESIZE_DELTA);
            panel1.Location = new Point(panel1.Location.X, NEW_PANEL_LOC);
            panel2.Location = new Point(panel2.Location.X, NEW_PANEL_LOC);
            panel3.Location = new Point(panel3.Location.X, NEW_PANEL_LOC);
            panel4.Location = new Point(panel4.Location.X, NEW_PANEL_LOC);
            AddToPrintTableSelectedButton.Location = new Point(AddToPrintTableSelectedButton.Location.X, NEW_PANEL_LOC);

            int NEW_PRINT_TABLE_HEIGHT = (int)(PRINT_TABLE_HEIGHT * Constants.SCALE_Y + RESIZE_DELTA);
            PrintDataGrid.Height = NEW_PRINT_TABLE_HEIGHT;

            int NEW_SPLITTER_HEIGHT = (int)(SPLITTER_HEIGHT * Constants.SCALE_Y);
            panel5.Location = new Point(panel1.Location.X, panel1.Location.Y + panel1.Height + NEW_SPLITTER_HEIGHT);
            PrintDataGrid.Location = new Point(panel5.Location.X, panel5.Location.Y + panel5.Height + NEW_SPLITTER_HEIGHT);
            PrintReadyButton.Location = new Point(PrintReadyButton.Location.X, PrintDataGrid.Location.Y + PrintDataGrid.Height + NEW_SPLITTER_HEIGHT);
        }

        private void moneyActionDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ColorSelectedRow(moneyActionDataGrid, SelectedDGWCellsIndexesMain);
        }

        private void PrintDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ColorSelectedRow(PrintDataGrid, SelectedDGWCellsIndexesPrint);
        }

        private void ColorSelectedRow(DataGridView DGW, List<int> SelectedIndexes)
        {
            if (SelectedIndexes.Count != 0)
            {
                foreach (int i in SelectedIndexes)
                    if (i >= 0 && i < DGW.RowCount)
                        foreach (DataGridViewCell cell in DGW.Rows[i].Cells)
                            if (cell.Style.BackColor == Constants.SELECT_CELL_COLOR)
                                cell.Style.BackColor = Constants.NORMAL_CELL_COLOR;
                SelectedIndexes.Clear();
            }

            foreach (DataGridViewCell i in DGW.SelectedCells)
                if (!SelectedIndexes.Contains(i.RowIndex))
                {
                    foreach (DataGridViewCell cell in i.OwningRow.Cells)
                        if (cell.Style.BackColor == Constants.NORMAL_CELL_COLOR)
                            cell.Style.BackColor = Constants.SELECT_CELL_COLOR;
                    SelectedIndexes.Add(i.RowIndex);
                }
        }
    }
}
