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
    /// Логика взаимодействия для StaffEditPage.xaml
    /// </summary>
    public partial class StaffEditPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection
                          (ConnectClass.ConnectString);
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        public StaffEditPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From " +
                    "dbo.StaffView " +
                    $"Where " +
                    $"IdStaff='{GlobalVariableClass.IdStaff}'",
                    sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                NameTB.Text = sqlDataReader[1].ToString();
                SurnameTB.Text = sqlDataReader[2].ToString();
                MiddleNameTB.Text = sqlDataReader[3].ToString();
                PhoneTB.Text = sqlDataReader[4].ToString();
                DateDP.Text = sqlDataReader[5].ToString();
                AddressTB.Text = sqlDataReader[6].ToString();
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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Update " +
                    "dbo.[Staff] Set " +
                    $"NameStaff='{NameTB.Text}'," +
                    $"SurnameStaff='{SurnameTB.Text}'," +
                    $"MiddleNameStaff='{MiddleNameTB.Text}'," +
                    $"PhoneStaff='{PhoneTB.Text}'," +
                    $"DateOfBirthStaff='{DateDP.Text}'," +
                    $"AddressStaff='{AddressTB.Text}' " +
                    $"WHERE IdStaff='{GlobalVariableClass.IdStaff}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB("Данные о сотруднике " +
                    "сохранены");
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
