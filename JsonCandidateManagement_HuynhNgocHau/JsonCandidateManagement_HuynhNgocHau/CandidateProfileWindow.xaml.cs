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
	/// Interaction logic for CandidateProfileWindow.xaml
	/// </summary>
	public partial class CandidateProfileWindow : Window
	{
		private Hraccount Nowaccount = new Hraccount();
		private ICandidateProfileService profileService;
		private IJobPostingService jobPostingService;
		public CandidateProfileWindow()
		{
			InitializeComponent();
			profileService = new CandidateProfileService();
			jobPostingService = new JobPostingService();
		}

		public CandidateProfileWindow(Hraccount account)
		{
			InitializeComponent();
			profileService = new CandidateProfileService();
			jobPostingService = new JobPostingService();
			this.Nowaccount = account;
			switch (Nowaccount.MemberRole)
			{
				case 1:
					break;
				case 2:
					bntDelete.IsEnabled = false;
					break;
				case 3:
					bntDelete.IsEnabled = false;
					bntUpdate.IsEnabled = false;
					break;
				default:
					this.Close();
					break;
			}
		}
		private void CandidateID_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void bntClose_Click(object sender, RoutedEventArgs e)
		{
			MenuWindow window = new MenuWindow(Nowaccount);
			window.Show();
			this.Close();
		}

		private void bntDelete_Click(object sender, RoutedEventArgs e)
		{
			string candidateID = CandidateID.Text;
			if(candidateID == "")
			{
				MessageBox.Show("Choose 1 CandidateID to delete !");
				return;
			}
			MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Candidate Profile?",
													  "Confirm Delete",
													  MessageBoxButton.YesNo,
													  MessageBoxImage.Warning);

			if (result == MessageBoxResult.Yes)
			{
				if (candidateID.Length > 0 && profileService.DeleteCandidate(candidateID))
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

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DataGrid dataGrid = sender as DataGrid;
			DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
			if (row != null)
			{
				DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
				string id = ((TextBlock)RowColumn.Content).Text;
				CandidateProfile profile = profileService.GetCandidateProfileById(id);
				if (profile != null)
				{
					CandidateID.Text = profile.CandidateID;
					FullName.Text = profile.Fullname;
					BirthDay.SelectedDate = profile.Birthday;
					ImageURL.Text = profile.ProfileURL;
					cmbPostID.SelectedValue = profile.PostingID;
					Description.Text = profile.ProfileShortDescription;
				}
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			loadDataInit();
		}
		private void loadDataInit() {
			this.DataGrid.ItemsSource = profileService.GetAllCandidateProfiles();
			this.cmbPostID.ItemsSource = jobPostingService.GetAllJobPostings();
			this.cmbPostID.DisplayMemberPath = "JobPostingTitle";
			this.cmbPostID.SelectedValuePath = "PostingID";

			CandidateID.Text = "";
			FullName.Text = "";
			BirthDay.Text ="";
			ImageURL.Text = "";
			cmbPostID.Text = "";
			Description.Text = "";
			BirthDay.SelectedDate = null;

		}
		private void JobPostingID_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}

		private void bntAdd_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(CandidateID.Text) ||string.IsNullOrWhiteSpace(FullName.Text) ||
				string.IsNullOrWhiteSpace(BirthDay.Text) ||string.IsNullOrWhiteSpace(ImageURL.Text) ||
				cmbPostID.SelectedValue == null ||string.IsNullOrWhiteSpace(Description.Text))
			{
				MessageBox.Show("All fields are required!");
				return;
			}

			if (FullName.Text.Length <= 12 || !IsFullNameCapitalized(FullName.Text))
			{
				MessageBox.Show("Full Name must be greater than 12 characters and each word must begin with a capital letter!");
				return;
			}

			if (Description.Text.Length < 12 || Description.Text.Length > 200)
			{
				MessageBox.Show("Description must be between 12 and 200 characters!");
				return;
			}

			try
			{
				CandidateProfile candidate = new CandidateProfile
				{
					CandidateID = CandidateID.Text,
					Fullname = FullName.Text,
					Birthday = DateTime.Parse(BirthDay.Text),
					ProfileURL = ImageURL.Text,
					PostingID = cmbPostID.SelectedValue.ToString(),
					ProfileShortDescription = Description.Text
				};
				if (profileService.AddCandidate(candidate))
				{
					MessageBox.Show("Add successful!");
					loadDataInit();
				}
				else
				{
					MessageBox.Show("Add unsuccessful!");
				}
			}
			catch (FormatException)
			{
				MessageBox.Show("Invalid date format for Birthday. Please enter a valid date.");
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
			if (string.IsNullOrWhiteSpace(CandidateID.Text) || string.IsNullOrWhiteSpace(FullName.Text) ||
				string.IsNullOrWhiteSpace(BirthDay.Text) || string.IsNullOrWhiteSpace(ImageURL.Text) ||
				cmbPostID.SelectedValue == null || string.IsNullOrWhiteSpace(Description.Text))
			{
				MessageBox.Show("All fields are required!");
				return;
			}

			if (FullName.Text.Length <= 12 || !IsFullNameCapitalized(FullName.Text))
			{
				MessageBox.Show("Full Name must be greater than 12 characters and each word must begin with a capital letter!");
				return;
			}

			if (Description.Text.Length < 12 || Description.Text.Length > 200)
			{
				MessageBox.Show("Description must be between 12 and 200 characters!");
				return;
			}
			try
			{
				CandidateProfile candidate = new CandidateProfile();
				candidate.CandidateID = CandidateID.Text;
				candidate.Fullname = FullName.Text;
				candidate.Birthday = DateTime.Parse(BirthDay.Text);
				candidate.ProfileURL = ImageURL.Text;
				//candidate.Posting = jobPostingService.GetJobPostingById(cmbPostID.SelectedValue.ToString());
				candidate.PostingID = cmbPostID.SelectedValue.ToString();
				candidate.ProfileShortDescription = Description.Text;
				if (profileService.UpdateCandidate(candidate))
				{
					MessageBox.Show("Update successful !");
					loadDataInit();
				}
				else
				{
					MessageBox.Show("Update unsuccessful !");
				}
			}
			catch (FormatException)
			{
				MessageBox.Show("Invalid date format for Birthday. Please enter a valid date.");
			}
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			string Name = FullName.Text;
			DateTime ? day = BirthDay.SelectedDate;
			var listCandidateProfile = profileService.GetCandidateProfileByNameOrDateTime(Name, day);
			this.DataGrid.ItemsSource = listCandidateProfile;
		}

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
			loadDataInit();
        }
    }
}
