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
    public partial class RemoveActionWindow : Form
    {
        private MoneyKeeperGeneral MKG;
        private bool IsAction;

        public RemoveActionWindow(MoneyKeeperGeneral MKG, bool IsAction)
        {
            this.MKG = MKG;
            this.IsAction = IsAction;

            InitializeComponent();

            if (!IsAction)
            {
                IDLabel.Text = "ID заказа:";
                ReadyButton.Text = "Удалить заказ!";
            }
        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            if (IsAction)
            {
                if (MKG.MoneyActions.RemoveAction((int)ID.Value))
                {
                    MKG.Save(Constants.ORDERS_INFO_PATH,
                             Constants.NAMES_INFO_PATH,
                             Constants.CLIENTS_INFO_PATH);

                    MessageBox.Show("Событие удалено!");
                }
                else
                {
                    MessageBox.Show("События с таким ID не существует");
                    return;
                }
            }
            else
            {
                if (MKG.MoneyActions.RemoveOrder((int)ID.Value))
                {
                    MKG.Save(Constants.ORDERS_INFO_PATH,
                             Constants.NAMES_INFO_PATH,
                             Constants.CLIENTS_INFO_PATH);

                    MessageBox.Show("Заказ удален!");
                }
                else
                {
                    MessageBox.Show("Заказа с таким ID не существует");
                    return;
                }
            }

            if (MessageBox.Show("Хотите удалить еще " + (IsAction ? "одно событие?" : "один заказ?"), "Удаление событий / заказов", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                ID.Value = 1;
            else
                this.Close();
        }
    }
}
