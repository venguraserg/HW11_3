using BL.Controllers;
using BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для WindowAuth.xaml
    /// </summary>
    public partial class WindowAuth : Window
    {
        
        public WindowAuth()
        {
            InitializeComponent();
            
        }
        private void BTN_Auth_Click(object sender, RoutedEventArgs e)
        {
            string tempName = TB_UserName.Text;
            Controller userController = new Controller(tempName);
            if (userController.IsNewUser) { MessageBox.Show("Вы новый пользователь\nваш статус - консультант\nдля изменения обратитесь к Администратору\nили войдите под Admin"); }
            if (userController.Clients.Count == 0)
            {
                var result = MessageBox.Show("Список клиентов пуст, заволнить автоматически?", "???", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    userController.ClientAutofill(15);
                }
            }

            if (userController.CurentUser is Administrator)
            {
                var windowAdmin = new WindowAdmin(userController);
                windowAdmin.Show();
                
            }
            else
            {

                var mainWindow = new MainWindow(userController);
                mainWindow.Show();
                


            }
            
            this.Close();
        }
    }
}
