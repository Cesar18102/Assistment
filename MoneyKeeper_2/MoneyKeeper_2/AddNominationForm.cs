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
    public partial class AddNominationForm : Form
    {
        private MoneyKeeperGeneral MKG;
        private bool addingMode;

        public AddNominationForm(MoneyKeeperGeneral MKG, bool AddingMode)
        {
            this.MKG = MKG;
            this.addingMode = AddingMode;

            InitializeComponent();

            if (!addingMode)
            {
                this.Text = "Удаление наименования";
                ReadyButton.Text = "Удалить товар";
            }
        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            if (ProductName.Text == "")
            {
                MessageBox.Show("Введите название товаара!");
                return;
            }

            if (addingMode)
            {
                if (MKG.ProductNames.AddNomination(ProductName.Text))
                {
                    MKG.Save(Constants.ORDERS_INFO_PATH,
                             Constants.NAMES_INFO_PATH,
                             Constants.CLIENTS_INFO_PATH);

                    MessageBox.Show("Наименование успешно добавлено!");

                    if (MessageBox.Show("Хотите добавить еще одно наименование?", "Добавление наименований", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        ProductName.Text = "";
                    else
                        this.Close();
                }
                else
                    MessageBox.Show("Наименование уже существует, наименование не было добавлено!");
            }
            else
            {
                if (MKG.ProductNames.RemoveNomination(ProductName.Text))
                {
                    MKG.Save(Constants.ORDERS_INFO_PATH,
                             Constants.NAMES_INFO_PATH,
                             Constants.CLIENTS_INFO_PATH);

                    MessageBox.Show("Наименование успешно удалено!");

                    if (MessageBox.Show("Хотите удалить еще одно наименование?", "Удаление наименований", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        ProductName.Text = "";
                    else
                        this.Close();
                }
                else
                    MessageBox.Show("Наименования не существует!");
            }
        }
    }
}
