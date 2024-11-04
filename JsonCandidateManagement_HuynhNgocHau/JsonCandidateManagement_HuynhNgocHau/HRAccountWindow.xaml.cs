using Candidate_BO;
using Candidate_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for HRAccountWindow.xaml
    /// </summary>
    public partial class HRAccountWindow : Window
    {
        private Hraccount Nowaccount = new Hraccount();
        private readonly IHRAccountService hRAccountService;
        public HRAccountWindow()
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();
        }
        public HRAccountWindow(Hraccount hraccount)
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();
            this.Nowaccount = hraccount;
            switch (Nowaccount.MemberRole)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    this.Close();
                    break;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
        private void loadDataInit()
        {
            this.DataGrid.ItemsSource = hRAccountService.GetAllHRAccounts();

            // Tạo danh sách các role với label
            var roleList = new List<object>()
            {
                new { MemberRole = 1, RoleName = "Admin" },
                new { MemberRole = 2, RoleName = "Manager" },
                new { MemberRole = 3, RoleName = "HR" }
            };

            this.cmbRole.ItemsSource = roleList;
            this.cmbRole.DisplayMemberPath = "RoleName";    // Hiển thị tên role
            this.cmbRole.SelectedValuePath = "MemberRole";  // Giá trị thực sự của role

            // Clear các trường input
            txtEmail.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            cmbRole.SelectedValue = null;
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string? Name = txtFullName.Text;
            int? Role = (int?)cmbRole.SelectedValue;
            var listHrAccounts = hRAccountService.GetHraccountsByNameOrRole(Name, Role);
            this.DataGrid.ItemsSource = listHrAccounts;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
            if (row != null)
            {
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string Email = ((TextBlock)RowColumn.Content).Text;
                Hraccount account = hRAccountService.GetHraccountByEmail(Email);
                if (account != null)
                {
                    txtEmail.Text = account.Email;
                    txtFullName.Text = account.FullName;
                    txtPassword.Text = account.Password;
                    cmbRole.SelectedValue = account.MemberRole;
                }
            }
        }

        private void bntClose_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow window = new MenuWindow(Nowaccount);
            window.Show();
            this.Close();
        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            if (email == "")
            {
                MessageBox.Show("Choose 1 HRAccount to delete !");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this HRAccount ?",
                                                      "Confirm Delete",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                if (email.Length > 0 && hRAccountService.DeleteHRAccount(email))
                {
                    MessageBox.Show("Delete successful");
                    loadDataInit();
                }
                else
                {
                    MessageBox.Show("Something wrong !");
                }
            }
        }

        private void bntAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("All fields are required!");
                return;
            }
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                MessageBox.Show("Invalid email format!");
                return;
            }
            if (txtFullName.Text.Length <= 6 || !IsFullNameCapitalized(txtFullName.Text))
            {
                MessageBox.Show("Full Name must be greater than 6 characters and each word must begin with a capital letter!");
                return;
            }
            Hraccount newAccount = new Hraccount
            {
                Email = txtEmail.Text,
                FullName = txtFullName.Text,
                Password = txtPassword.Text,
                MemberRole = (int)cmbRole.SelectedValue,
            };
            if (hRAccountService.AddHRAccount(newAccount))
            {
                MessageBox.Show("Create account successful!");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Create account unsuccessful!");
            }
        }
        private bool IsFullNameCapitalized(string fullName)
        {
            string[] words = fullName.Split(' ');
            foreach (string word in words)
            {
                if (string.IsNullOrEmpty(word) || !char.IsUpper(word[0]))
                {
                    return false;
                }
            }
            return true;
        }
        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || cmbRole.SelectedValue == null)
            {
                MessageBox.Show("All fields are required!");
                return;
            }
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEmail.Text, emailPattern))
            {
                MessageBox.Show("Invalid email format!");
                return;
            }
            if (txtFullName.Text.Length <= 6 || !IsFullNameCapitalized(txtFullName.Text))
            {
                MessageBox.Show("Full Name must be greater than 6 characters and each word must begin with a capital letter!");
                return;
            }
            Hraccount Account = new Hraccount
            {
                Email = txtEmail.Text,
                FullName = txtFullName.Text,
                Password = txtPassword.Text,
                MemberRole = (int)cmbRole.SelectedValue,
            };
            if (hRAccountService.UpdateHRAccount(Account))
            {
                MessageBox.Show("Update account successful!");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Update account unsuccessful!");
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }
    }
}
