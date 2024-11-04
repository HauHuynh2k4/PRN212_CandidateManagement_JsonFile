using Candidate_BO;
using Candidate_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JsonCandidateManagement_HuynhNgocHau
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IHRAccountService hRAccountService;
        public LoginWindow()
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            Hraccount account = hRAccountService.GetHraccountByEmail(txtEmail.Text);
            if (account != null && account.Password.Equals(txtPassword.Text))
            {
                this.Hide();
                MenuWindow menuWindow = new MenuWindow(account);
                menuWindow.Show();
            }
            else
            {
                MessageBox.Show("Login Faild");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Hide();
        }
    }
}
