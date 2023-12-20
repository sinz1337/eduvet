using eduvet.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для DentalPage.xaml
    /// </summary>
    public partial class DentalPage : Page
    {
        DGClass dGClass;
        SqlConnection sqlConnection =
           new SqlConnection(ConnectClass.ConnectString);
        public DentalPage()
        {
            InitializeComponent();
            dGClass = new DGClass(DentalListDG);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.[Dental]");
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.Dental " +
                $"WHERE IdDental Like '%{SearchTB.Text}%' " +
                $"or CabDental Like '%{SearchTB.Text}%'");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PageFolder/DentalAddPage.xaml", UriKind.Relative));
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (DentalListDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выбирите для редактирования");
            }
            else
            {
                try
                {
                    GlobalVariableClass.IdDental =
                        Int32.Parse(dGClass.SelectId());
                    this.NavigationService.Navigate(new Uri("PageFolder/DentalEditPage.xaml", UriKind.Relative));
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)DentalListDG.SelectedItem;
            row.Delete();

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand =
                     new SqlCommand($"DELETE FROM dbo.Dental WHERE IdDental = " +
                     $"'%{DentalListDG.SelectedIndex}%'",
                     sqlConnection);
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
