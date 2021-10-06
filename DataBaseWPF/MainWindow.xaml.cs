using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DataBaseLib;
using DataBaseLib.DataModel;

namespace DataBaseWPF
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Account> _accounts;
        private readonly ObservableCollection<Role> _roles;
        private readonly ObservableCollection<User> _users;
        public MainWindow()
        {
            InitializeComponent();

            var db = new DataBase();
            _accounts = db.GetAllAccounts();
            _roles = db.GetAllRoles();
            _users = db.GetAllUsers();

            ListUsers.ItemsSource = _accounts;
            Select_Role.ItemsSource = _roles;
        }

        private void ListUsers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var account = (Account)ListUsers.SelectedItem;
            var user = _users[account.Id];

            Input_FirstName.Text = user.FirstName;
            Input_LastName.Text = user.LastName;

            foreach (var email in user.ListOfEmail)
            {
                Input_Email.Text += $" {email}; ";
            }
            
            Input_Login.Text = account.Login;
            Input_Password.Password = account.Password;

            Select_Role.SelectedIndex = account.Role.Id - 1;
        }
    }
}