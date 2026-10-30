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
    /// Логика взаимодействия для CoordinatorWindow.xaml
    /// </summary>
    public partial class CoordinatorWindow : Window
    {
        EducationCenterContext context = new EducationCenterContext();

        List<Role> roles = new List<Role>();
        
        public CoordinatorWindow()
        {
            InitializeComponent();

            roles = context.Roles.ToList();

            UsersLv.ItemsSource = context.Users.ToList();
        }

        private void StartFiltersSearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadFilters()
        {
            roles.Insert(0, new Role() { Name = "Все роли" });

            FilterByRoleCmb.ItemsSource = context.Roles.ToList();
        }
    }
}
