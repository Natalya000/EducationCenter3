using EducationCenter3.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для CoordinatorWindow.xaml
    /// </summary>
    public partial class CoordinatorWindow : Window
    {
        EducationCenterContext context = new EducationCenterContext();

        List<Role> roles = new List<Role>();
        List<User> users = new List<User>();
        
        public CoordinatorWindow()
        {
            InitializeComponent();

            users = context.Users
                .Include(u => u.Employee)
                .Include(u => u.Role)
                .ToList();
            roles = context.Roles.ToList();

            UsersLv.ItemsSource = users;

            ItemCountTbl.Text = $"Записей найдено: {users.Count}";

            LoadFilters();
        }

        private void StartFiltersSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            List<User> filteredUsers = users;

            string searchText = SearchByNameTb.Text;
            int selectedRoleId = 0;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredUsers = filteredUsers.Where(u => u.Employee.FullName.ToLower().Contains(searchText.ToLower())).ToList();
            }
            selectedRoleId = Convert.ToInt32(FilterByRoleCmb.SelectedValue);

            if (selectedRoleId > 0)
            {
                filteredUsers = filteredUsers.Where(u => u.RoleId == selectedRoleId).ToList();
            }

            if (filteredUsers.Count > 0)
            {
                UsersLv.Visibility = Visibility.Visible;
            }
            else
            {
                UsersLv.Visibility = Visibility.Hidden;
            }
            ItemCountTbl.Text = $"Записей найдено: {filteredUsers.Count}";

            UsersLv.ItemsSource = filteredUsers;
        }

        private void LoadFilters()
        {
            roles.Insert(0, new Role() { Name = "Все роли" });

            FilterByRoleCmb.DisplayMemberPath = "Name";
            FilterByRoleCmb.SelectedValuePath = "Id";
            FilterByRoleCmb.ItemsSource = roles;

            PositionCmb.DisplayMemberPath = "Name";
            PositionCmb.SelectedValuePath = "Id";
            PositionCmb.ItemsSource = context.Positions.ToList();
            PositionCmb.SelectedIndex = 0;

            DepartmentCmb.DisplayMemberPath = "Name";
            DepartmentCmb.SelectedValuePath = "Id";
            DepartmentCmb.ItemsSource = context.Departments.ToList();
            DepartmentCmb.SelectedIndex = 0;

            RoleCmb.DisplayMemberPath = "Name";
            RoleCmb.SelectedValuePath = "Id";
            RoleCmb.ItemsSource = context.Roles.ToList();
            RoleCmb.SelectedIndex = 0;
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTb.Text) ||
                    string.IsNullOrWhiteSpace(EmailTb.Text) ||
                    string.IsNullOrWhiteSpace(PhoneTb.Text) ||
                    string.IsNullOrWhiteSpace(NoteTb.Text) ||
                    string.IsNullOrWhiteSpace(LoginTb.Text) ||
                    string.IsNullOrWhiteSpace(PasswordPb.Text) ||
                    HireDateDp.SelectedDate.Value == null)
            {
                MessageBox.Show("Все поля обязательны для заполнения. Проверьте заполнение полей", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    if (users.Any(u => u.Login == LoginTb.Text))
                    {
                        MessageBox.Show("Указанный логин уже используется. Придумайте новый.", "Предупреждение",
                            MessageBoxButton.OK, MessageBoxImage.Warning);

                        return;
                    }

                    Employee employee = new Employee()
                    {
                        FullName = FullNameTb.Text,
                        PositionId = (int)PositionCmb.SelectedValue,
                        DepartmentId = (int)DepartmentCmb.SelectedValue,
                        Email = EmailTb.Text,
                        Phone = PhoneTb.Text,
                        HireDate = DateOnly.FromDateTime(HireDateDp.SelectedDate.Value),
                        IsActive = IsActiveCb.IsChecked.Value,
                        Note = NoteTb.Text,
                    };
                    context.Employees.Add(employee);
                    context.SaveChanges();

                    User user = new User()
                    {
                        EmployeeId = employee.Id,
                        Login = LoginTb.Text,
                        Password = PasswordPb.Text,
                        RoleId = (int)RoleCmb.SelectedValue,
                    };
                    context.Users.Add(user);
                    context.SaveChanges();

                    MessageBox.Show("Пользователь успешно добавлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                    UsersLv.ItemsSource = users = context.Users.ToList();
                    ItemCountTbl.Text = $"Записей найдено: {users.Count}";

                    ClearFields();
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"{exception.Message}", "Ошибка", MessageBoxButton.OK, 
                        MessageBoxImage.Error);
                }

                
            }
        }

        void ClearFields()
        {
            EmailTb.Clear();
            FullNameTb.Clear();
            LoginTb.Clear();
            NoteTb.Clear();
            PhoneTb.Clear();
            PasswordPb.Clear();
            HireDateDp.SelectedDate = null;
            DepartmentCmb.SelectedIndex = 0;
            PositionCmb.SelectedIndex = 0;
            RoleCmb.SelectedIndex = 0;
        }
        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
        }

        private void UsersLv_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DataContext = UsersLv.SelectedItem as User;
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = null;

            ClearFields();
        }
    }
}
