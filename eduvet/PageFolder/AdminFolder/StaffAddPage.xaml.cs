using eduvet.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eduvet.PageFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для StaffAddPage.xaml
    /// </summary>
    public partial class StaffAddPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection
                                          (ConnectClass.ConnectString);
        SqlCommand sqlCommand;
        public StaffAddPage()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTB.Text))
            {
                MBClass.ErrorMB("Введите имя");
                NameTB.Focus();
            }
            else if(string.IsNullOrWhiteSpace(SurnameTB.Text))
            {
                MBClass.ErrorMB("Введите фамилию");
                SurnameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(MiddleNameTB.Text))
            {
                MBClass.ErrorMB("Введите отчество");
                MiddleNameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PhoneTB.Text))
            {
                MBClass.ErrorMB("Введите номер телефона");
                PhoneTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(AddressTB.Text))
            {
                MBClass.ErrorMB("Введите адрес");
                AddressTB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.Staff " +
                        "(NameStaff,SurnameStaff,MiddleNameStaff," +
                        "PhoneStaff,DateOfBirthStaff,AddressStaff)" +
                        $"Values ('{NameTB.Text}','{SurnameTB.Text}','{MiddleNameTB.Text}'," +
                        $"'{PhoneTB.Text}','{DateDP.Text}','{AddressTB.Text}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();

                    MBClass.InfoMB("Успешно добавлено");
                    this.NavigationService.Navigate(new Uri("PageFolder/AdminFolder/StaffListPage.xaml", UriKind.Relative));
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
