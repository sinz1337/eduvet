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

namespace eduvet.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для DentalAddPage.xaml
    /// </summary>
    public partial class DentalAddPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection
                                          (ConnectClass.ConnectString);
        SqlCommand sqlCommand;
        public DentalAddPage()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CabTB.Text))
            {
                MBClass.ErrorMB("Введите кабинет");
                CabTB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.Dental " +
                        "(CabDental)" +
                        $"Values ('{CabTB.Text}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();

                    MBClass.InfoMB("Успешно добавлено");
                    this.NavigationService.Navigate(new Uri("PageFolder/DentalPage.xaml", UriKind.Relative));
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
