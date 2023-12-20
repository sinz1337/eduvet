using eduvet.ClassFolder;
using eduvet.WindowFolder.AdminFolder;
using eduvet.WindowFolder.StaffFolder;
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
using System.Windows.Shapes;

namespace eduvet.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(ConnectClass.ConnectString);
        SqlDataReader sqlDataReader;
        SqlCommand sqlCommand;
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(LoginTB.Text) &&
                    !string.IsNullOrWhiteSpace(PasswordPB.Password))
                {
                    LoginBtn_Click(sender, e);
                }
                else
                {
                    if (LoginTB.IsFocused)
                        PasswordPB.Focus();
                    else
                        LoginTB.Focus();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTB.Focus();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMB("Введите логин");
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMB("Введите пароль");
                PasswordPB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Select PasswordUser, IdUser From dbo.[User] " +
                        $"Where LoginUser='{LoginTB.Text}'", sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    sqlDataReader.Read();

                    if (sqlDataReader[0].ToString() != PasswordPB.Password)
                    {
                        MBClass.ErrorMB("Не правильный пароль!");
                        PasswordPB.Focus();
                    }
                    else
                    {

                        switch (sqlDataReader[1].ToString())
                        {
                            case "1":
                                MBClass.InfoMB("Администрастратор");
                                new AdminWindow().Show();
                                break;
                            case "2":
                                MBClass.InfoMB("Сотрудник");
                                new StaffWindow().Show();
                                break;
                        }
                    }

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

        private void CloseIMG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB();
        }
    }
}
