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
    /// Логика взаимодействия для CaptchaWindow.xaml
    /// </summary>
    public partial class CaptchaWindow : Window
    {
        Image selectedFragment;
        public CaptchaWindow()
        {
            InitializeComponent();
        }

        private void CaptchaLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CaptchaLb.SelectedItem is Image image)
            {
                CaptchaLb.Items.Remove(image);
                CaptchaFragmentsLb.Items.Add(image);
            }
        }

        private void CaptchaFragmentsLb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CaptchaFragmentsLb.SelectedItem is Image image)
            {
                CaptchaFragmentsLb.Items.Remove(image);
                CaptchaLb.Items.Add(image);
            }
            if (CaptchaLb.Items.Count == 6)
            {
                string key = "123456";
                string result = "";

                foreach (var item in CaptchaLb.Items)
                {
                    result += (item as Image).Tag;
                }
                DialogResult = key == result ? true : false;
            }
        }
    }
}
