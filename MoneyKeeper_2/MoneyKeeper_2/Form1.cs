using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;

namespace MoneyKeeper_2
{
    public partial class MoneyKeeperMainForm : Form
    {
        private MoneyKeeperGeneral MKG;
        private Regex PasswordRG = new Regex("^[A-Za-z0-9_]+$", RegexOptions.Singleline);
        private Regex LoginRG = new Regex("^[A-Za-z0-9_]+$", RegexOptions.Singleline);

        private static int DELTA_BUTTONS_LOC = (int)(-60 * Constants.SCALE_Y);
        private static int DELTA_FORM_HEIGHT = (int)(-90 * Constants.SCALE_Y);

        public MoneyKeeperMainForm()
        {
            InitializeComponent();
        }

        private void MoneyKeeperMainForm_Load(object sender, EventArgs e)
        {
            Constants.ScaleForm(this, Constants.SCALE_X, Constants.SCALE_Y);

            MKG = new MoneyKeeperGeneral();
            MKG.Load(Constants.ORDERS_INFO_PATH, 
                     Constants.NAMES_INFO_PATH,
                     Constants.CLIENTS_INFO_PATH);
        }

        private void MoneyKeeperMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MKG.Save(Constants.ORDERS_INFO_PATH,
                     Constants.NAMES_INFO_PATH,
                     Constants.CLIENTS_INFO_PATH);
        }

        private void AddActionButton_Click(object sender, EventArgs e)
        {
            AddActionWindow AAW = new AddActionWindow(MKG);
            AAW.ShowDialog();
        }

        private void ActionViewerButton_Click(object sender, EventArgs e)
        {
            ActionViewerForm AVF = new ActionViewerForm(MKG);
            AVF.ShowDialog();
        }

        private void AddNominationButton_Click(object sender, EventArgs e)
        {
            AddNominationForm ANF = new AddNominationForm(MKG, true);
            ANF.ShowDialog();
        }

        private void RemoveNominationButton_Click(object sender, EventArgs e)
        {
            AddNominationForm ANF = new AddNominationForm(MKG, false);
            ANF.ShowDialog();
        }

        private void AddClientButton_Click(object sender, EventArgs e)
        {
            AddClientForm ACF = new AddClientForm(MKG, true);
            ACF.ShowDialog();
        }

        private void RemoveClientButton_Click(object sender, EventArgs e)
        {
            AddClientForm ACF = new AddClientForm(MKG, false);
            ACF.ShowDialog();
        }

        private void RemoveActionButton_Click(object sender, EventArgs e)
        {
            RemoveActionWindow RAW = new RemoveActionWindow(MKG, true);
            RAW.ShowDialog();
        }

        private void RemoveOrderButton_Click(object sender, EventArgs e)
        {
            RemoveActionWindow RAW = new RemoveActionWindow(MKG, false);
            RAW.ShowDialog();
        }

        private void Autorize_Click(object sender, EventArgs e)
        {
            if (LoginRG.Match(Login.Text).ToString() == "")
            {
                MessageBox.Show("Логин должен состоять из символов A - Z, a - z, 0 - 9, _");
                return;
            }

            if (PasswordRG.IsMatch(Password.Text).ToString() == "")
            {
                MessageBox.Show("Пароль должен состоять из символов A - Z, a - z, 0 - 9, _");
                return;
            }

            string StrData = "LOGIN=" + Login.Text + "&PASSWORD=" + Password.Text + "&DATETIME=" + DateTime.Now;
            byte[] data = Encoding.UTF8.GetBytes(StrData);

            try
            {
                HttpWebRequest HRQ = HttpWebRequest.CreateHttp("http://jewelfilter.zzz.com.ua/autorization/auto.php");
                HRQ.Method = "POST";
                HRQ.ContentType = "application/x-www-form-urlencoded";
                HRQ.ContentLength = data.Length;

                Stream strw = HRQ.GetRequestStream();
                strw.Write(data, 0, data.Length);
                strw.Close();

                HttpWebResponse HTPR = HRQ.GetResponse() as HttpWebResponse;

                StreamReader str = new StreamReader(HTPR.GetResponseStream());
                string result = str.ReadToEnd();
                str.Close();
                HTPR.Close();

                if (result.IndexOf("FAULT") != -1)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }

                if (result.IndexOf("OK") != -1)
                {
                    MessageBox.Show("Авторизация успешна!");

                    LoginLabel.Visible = false;
                    Login.Visible = false;

                    PasswordLabel.Visible = false;
                    Password.Visible = false;

                    Autorize.Visible = false;

                    AddActionButton.Enabled = true;
                    ActionViewerButton.Enabled = true;
                    AddNominationButton.Enabled = true;
                    RemoveNominationButton.Enabled = true;
                    AddClientButton.Enabled = true;
                    RemoveClientButton.Enabled = true;
                    RemoveActionButton.Enabled = true;
                    RemoveOrderButton.Enabled = true;

                    MoveButton(AddActionButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(ActionViewerButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(AddNominationButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(RemoveNominationButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(AddClientButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(RemoveClientButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(RemoveActionButton, 0, DELTA_BUTTONS_LOC);
                    MoveButton(RemoveOrderButton, 0, DELTA_BUTTONS_LOC);

                    this.Height += DELTA_FORM_HEIGHT;
                }
            }
            catch { MessageBox.Show("Нет подключения к Интернету!"); }
        }

        private void MoveButton(Button B, int dX, int dY)
        {
            B.Location = new Point(B.Location.X + dX, B.Location.Y + dY);
            B.Anchor = AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Bottom & AnchorStyles.Top;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            MKG.Save(Constants.ORDERS_INFO_PATH,
                     Constants.NAMES_INFO_PATH,
                     Constants.CLIENTS_INFO_PATH);

            MessageBox.Show("Сохранено!");
        }
    }
}
