using BL.Controllers;
using BL.Interfaces;
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

namespace HW11_3
{
    /// <summary>
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        private Controller userController;

        public WindowAdmin(Controller userController)
        {
            InitializeComponent();
            this.userController = userController;
            this.Title = $"{userController.CurentUser.Name} - {userController.CurentUser.Status}";
            ListView_Users.ItemsSource = userController.Users;
        }

        private void BTN_Change_Click(object sender, RoutedEventArgs e)
        {
            var item = ListView_Users.SelectedItem as IUserInteface;
            if (item != null)
            {
                userController.ChangeUserStatus(item.Name);
            }
            

        }

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {
            WindowAuth authWin = new();
            authWin.Show();
            this.Close();
        }
    }
}
