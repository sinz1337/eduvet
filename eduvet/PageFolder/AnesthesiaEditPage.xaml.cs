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
    /// Логика взаимодействия для AnesthesiaEditPage.xaml
    /// </summary>
    public partial class AnesthesiaEditPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection
                          (ConnectClass.ConnectString);
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        public AnesthesiaEditPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From " +
                    "dbo.AnesthesiaView " +
                    $"Where " +
                    $"IdAnesthesia='{GlobalVariableClass.IdAnesthesia}'",
                    sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                CabTB.Text = sqlDataReader[1].ToString();
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
                    "dbo.[Anesthesia] Set " +
                    $"CabAnesthesia='{CabTB.Text}' " +
                    $"WHERE IdAnesthesia='{GlobalVariableClass.IdAnesthesia}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMB("Данные " +
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
