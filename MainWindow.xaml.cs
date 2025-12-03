using Music_Store.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Music_Store
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Employees currentEmployee;
        private string connectionString;
        private Connection db;
        public MainWindow()
        {
            InitializeComponent();
            frame.Navigate(new Pages.Auth(currentEmployee));
        }
        public MainWindow(Employees employee, string connectionString)
        {
            InitializeComponent();
            this.currentEmployee = employee;
            this.connectionString = connectionString;
            this.db = new Connection();
            this.Title = $"Music Store - {employee.full_name} ({employee.position})";
        }


        //private void SetupUIByRole()
        //{
        //    // Скрываем/показываем элементы в зависимости от роли
        //    switch (currentEmployee.position.ToLower())
        //    {
        //        case "кассир":
        //            // Только продажи и возвраты
        //            tabAdmin.Visibility = Visibility.Collapsed;
        //            tabManager.Visibility = Visibility.Collapsed;
        //            tabCashier.Visibility = Visibility.Visible;
        //            break;

        //        case "менеджер":
        //            // Управление товарами + продажи
        //            tabAdmin.Visibility = Visibility.Collapsed;
        //            tabManager.Visibility = Visibility.Visible;
        //            tabCashier.Visibility = Visibility.Visible;
        //            break;

        //        case "администратор":
        //            // Все вкладки доступны
        //            tabAdmin.Visibility = Visibility.Visible;
        //            tabManager.Visibility = Visibility.Visible;
        //            tabCashier.Visibility = Visibility.Visible;
        //            break;
        //    }
        //}

        //private void LoadData()
        //{
        //    try
        //    {
        //        // Загружаем товары (с правами текущей роли)
        //        string query = "SELECT * FROM product";
        //        var products = db.ExecuteWithRole(query, connectionString);

        //        // Привязываем данные к DataGrid
        //        dataGridProducts.ItemsSource = products.DefaultView;

        //        // Обновляем статус
        //        statusBar.Text = $"Загружено {products.Rows.Count} товаров";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
        //    }
        //}
    }
}
