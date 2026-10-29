using EducationCenter3.Models;
using System;
using System.Collections.Generic;
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

namespace EducationCenter3.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        EducationCenterContext context = new EducationCenterContext();
        private int failedAttemptCount;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }


        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBox.Show("Все поля обязательны для заполнения. Заполните поле \"Логин\"", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MessageBox.Show("Все поля обязательны для заполнения. Заполните поле \"Пароль\"", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User? user = context.Users.FirstOrDefault(u => u.Login == LoginTb.Text
            && u.Password == PasswordPb.Password);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден. Проверьте правильность ввода логина или пароля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                failedAttemptCount++;

                return;
            }
            else
            {
                if (user.Employee.IsActive == false)
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к координатору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                CaptchaWindow captchaWindow = new CaptchaWindow();
                if (captchaWindow.ShowDialog() == true)
                {
                    MessageBox.Show("Вы успешно авторизовались", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    //ОТКРЫВАЕМ ОКНО В ЗАВИСИМОСТИ ОТ РОЛИ
                    if (IsRememberCb.IsChecked == true)
                    {
                        Properties.Settings.Default.UserLogin = LoginTb.Text;
                        Properties.Settings.Default.UserPassword = PasswordPb.Password;
                    }
                    else
                    {
                        Properties.Settings.Default.UserLogin = "";
                        Properties.Settings.Default.UserPassword = "";
                    }
                    Properties.Settings.Default.Save();

                    if (user.RoleId == 1)
                    {
                        CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
                        coordinatorWindow.Show();

                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Капча собрана не верно. Попробуйте ещё раз.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);

                    failedAttemptCount++;

                    if (failedAttemptCount >= 3)
                    {
                        user.Employee.IsActive = false;
                        context.SaveChanges();

                        MessageBox.Show("Вы заблокированы. Обратитесь к координатору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    return;
                }
            }
        }
    }
}
