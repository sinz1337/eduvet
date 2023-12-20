using eduvet.ClassFolder;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    /// Логика взаимодействия для TablesPage.xaml
    /// </summary>
    public partial class TablesPage : Page
    {
        DGClass dGClass;
        SqlConnection sqlConnection =
           new SqlConnection(ConnectClass.ConnectString);
        public TablesPage()
        {
            InitializeComponent();
            dGClass = new DGClass(TableListDG);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.[Table]");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PageFolder/TablesAddPage.xaml", UriKind.Relative));
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("SELECT * FROM dbo.Table " +
                $"WHERE IdTable Like '%{SearchTB.Text}%' " +
                $"or CabTable Like '%{SearchTB.Text}%'");
        }

        private void EditMI_Click(object sender, RoutedEventArgs e)
        {
            if (TableListDG.SelectedItem == null)
            {
                MBClass.ErrorMB("Выбирите для редактирования");
            }
            else
            {
                try
                {
                    GlobalVariableClass.IdTable =
                        Int32.Parse(dGClass.SelectId());
                    this.NavigationService.Navigate(new Uri("PageFolder/TablesEditPage.xaml", UriKind.Relative));
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)TableListDG.SelectedItem;
            row.Delete();

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"DELETE FROM dbo.Table WHERE IdTable = '%{TableListDG.SelectedIndex}%'", sqlConnection);
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
