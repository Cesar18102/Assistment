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
    public partial class AddClientForm : Form
    {
        private MoneyKeeperGeneral MKG;
        private bool addingMode;
        public Client AddedClient;

        public AddClientForm(MoneyKeeperGeneral MKG, bool AddingMode)
        {
            this.MKG = MKG;
            this.addingMode = AddingMode;

            InitializeComponent();

            if (!addingMode)
            {
                this.Text = "Удаление клиента";
                ReadyButton.Text = "Удалить клиента";
            }
        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            if (ClientName.Text == "")
            {
                MessageBox.Show("Введите имя клиента!");
                return;
            }

            if (addingMode)
            {
                if (MKG.ClientNames.AddClient(new Client(ClientName.Text)))
                {
                    MKG.Save(Constants.ORDERS_INFO_PATH,
                             Constants.NAMES_INFO_PATH,
                             Constants.CLIENTS_INFO_PATH);

                    MessageBox.Show("Клиент успешно добален!");

                    if (MessageBox.Show("Хотите добавить еще одного клиента?", "Добавление клиентов", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        ClientName.Text = "";
                    else
                        this.Close();
                }
                else
                    MessageBox.Show("Клиент с таким именем уже существует");
            }
            else
            {
                if (MKG.ClientNames.RemoveClient(new Client(ClientName.Text)))
                {
                    MKG.Save(Constants.ORDERS_INFO_PATH,
                             Constants.NAMES_INFO_PATH,
                             Constants.CLIENTS_INFO_PATH);

                    MessageBox.Show("Клиент успешно удален!"); // аннулировать сделки?

                    if (MessageBox.Show("Хотите удалить еще одного клиента?", "Удаление клиентов", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        ClientName.Text = "";
                    else
                        this.Close();
                }
                else
                    MessageBox.Show("Клиента с таким именем не существует");
            }
        }
    }
}
