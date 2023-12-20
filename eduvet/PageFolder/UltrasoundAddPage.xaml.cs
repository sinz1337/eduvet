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
    /// Логика взаимодействия для UltrasoundAddPage.xaml
    /// </summary>
    public partial class UltrasoundAddPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection
                                          (ConnectClass.ConnectString);
        SqlCommand sqlCommand;
        public UltrasoundAddPage()
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
                    sqlCommand = new SqlCommand("Insert Into dbo.Ultrasound " +
                        "(CabUltrasound)" +
                        $"Values ('{CabTB.Text}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();

                    MBClass.InfoMB("Успешно добавлено");
                    this.NavigationService.Navigate(new Uri("PageFolder/UltrasoundPage.xaml", UriKind.Relative));
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
