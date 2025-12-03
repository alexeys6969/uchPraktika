using Music_Store.Classes;
using System;
using System.Collections.Generic;
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

namespace Music_Store.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        Employees employee;
        Connection connect = new Connection();
        public Auth(Employees _employee)
        {
            InitializeComponent();
            employee = _employee;
        }

        private void authorization(object sender, RoutedEventArgs e)
        {
            string Login = login.Text.Trim();
            string Password = password.Password;

            // Проверяем заполнение полей
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 1. Проверяем логин/пароль через sa подключение
                DataRow userData = connect.CheckLogin(Login, Password);

                if (userData != null)
                {
                    // 2. Получаем должность пользователя
                    string position = userData["position"].ToString();

                    // 3. Получаем строку подключения для этой роли
                    string roleConnectionString = connect.GetRoleConnection(position);

                    // 4. Проверяем, что подключение с этими правами работает
                    if (connect.TestRoleConnection(roleConnectionString))
                    {
                        // 5. Заполняем объект employee данными пользователя
                        employee.employee_id = Convert.ToInt32(userData["employee_id"]);
                        employee.full_name = userData["full_name"].ToString();
                        employee.position = position;
                        employee.login = userData["login_employee"].ToString();
                        employee.password = userData["password_hash"].ToString(); // храним хеш

                        MessageBox.Show($"Добро пожаловать, {employee.full_name}!\nРоль: {employee.position}",
                                      "Успешный вход",
                                      MessageBoxButton.OK, MessageBoxImage.Information);

                        // 6. Открываем главное окно с правильными правами
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: нет доступа к базе данных с вашей ролью",
                                      "Ошибка прав",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль",
                                  "Ошибка входа",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
