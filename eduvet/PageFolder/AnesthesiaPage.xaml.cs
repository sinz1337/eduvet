using eduvet.ClassFolder;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AnesthesiaPage.xaml
    /// </summary>
    public partial class AnesthesiaPage : Page
    {
        DGClass dGClass;
        SqlConnection sqlConnection =
           new SqlConnection(ConnectClass.ConnectString);
        public AnesthesiaPage()
        {
            InitializeComponent();
            dGClass = new DGClass(AnesthesiaListDG);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.[Anesthesia]");
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.Anesthesia " +
                $"WHERE IdAnesthesia Like '%{SearchTB.Text}%' " +
                $"or CabAnesthesia Like '%{SearchTB.Text}%'");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PageFolder/AnesthesiaAddPage.xaml", UriKind.Relative));
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (AnesthesiaListDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выбирите для редактирования");
            }
            else
            {
                try
                {
                    GlobalVariableClass.IdAnesthesia =
                        Int32.Parse(dGClass.SelectId());
                    this.NavigationService.Navigate(new Uri("PageFolder/AnesthesiaEditPage.xaml", UriKind.Relative));
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)AnesthesiaListDG.SelectedItem;
            row.Delete();

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand =
                     new SqlCommand($"DELETE FROM dbo.Anesthesia WHERE IdAnesthesia = " +
                     $"'%{AnesthesiaListDG.SelectedIndex}%'",
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
