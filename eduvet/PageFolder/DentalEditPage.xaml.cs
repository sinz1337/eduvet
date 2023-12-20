﻿using eduvet.ClassFolder;
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
    /// Логика взаимодействия для DentalEditPage.xaml
    /// </summary>
    public partial class DentalEditPage : Page
    {
        SqlConnection sqlConnection = new SqlConnection
                          (ConnectClass.ConnectString);
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;
        public DentalEditPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From " +
                    "dbo.DentalView " +
                    $"Where " +
                    $"IdDental='{GlobalVariableClass.IdDental}'",
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
                    "dbo.[Dental] Set " +
                    $"CabDental='{CabTB.Text}' " +
                    $"WHERE IdDental='{GlobalVariableClass.IdDental}'",
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