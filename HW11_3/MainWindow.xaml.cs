using BL.Controllers;
using BL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HW11_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        private readonly Controller userController;
        
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        public MainWindow(Controller userController)
        {
            InitializeComponent();            
            this.userController = userController;
            TB_User.Text = $"Пользователь: {userController.CurentUser.Name}";
            TB_UserStatus.Text = $"Статус: {userController.CurentUser.GetType().Name}";


            ListView_Clients.ItemsSource = this.userController.Clients;
            
            tb_Surname.IsEnabled = userController.CurentUser is not Consultant;
            tb_Name.IsEnabled = userController.CurentUser is not Consultant;
            tb_Patronymic.IsEnabled = userController.CurentUser is not Consultant;
            tb_PassNumber.IsEnabled = userController.CurentUser is not Consultant;
            BTN_Add.IsEnabled = userController.CurentUser is not Consultant;
            BTN_Delete.IsEnabled = false;
            BTN_Change.IsEnabled = false;


           


        }
        /// <summary>
        /// Действия при изменеии выбора ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_Clients_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ListView_Clients.SelectedItem is Client item)
            {
                tb_Surname.Text = item.Surname;
                tb_Name.Text = item.Name;
                tb_Patronymic.Text = item.Patronymic;
                tb_PhoneNumber.Text = item.PhoneNumber;
                tb_PassNumber.Text = item.PassNumber;
            }
            else
            {
                tb_Surname.Text = "";
                tb_Name.Text = "";
                tb_Patronymic.Text = "";
                tb_PhoneNumber.Text = "";
                tb_PassNumber.Text = "";
            }
            if (ListView_Clients.SelectedItems.Count > 0)
                {
                    BTN_Change.IsEnabled = true;
                    BTN_Delete.IsEnabled = userController.CurentUser is not Consultant;
                }
                else
                {
                    BTN_Change.IsEnabled = false;
                    BTN_Delete.IsEnabled = false;
                }

            
        }
        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            if (InputValidationClientData())
            {
                var tempSurname = tb_Surname.Text.Trim();
                var tempName = tb_Name.Text.Trim();
                var tempPatronymic = tb_Patronymic.Text.Trim();
                var tempPhoneNumber = tb_PhoneNumber.Text.Trim();
                var tempPassNumber = tb_PassNumber.Text.Trim();
                if (userController.AddClient(tempSurname, tempName, tempPatronymic, tempPhoneNumber, tempPassNumber))
                {
                    MessageBox.Show("Данные успешно добавлены");
                    tb_Surname.Text = "";
                    tb_Name.Text = "";
                    tb_Patronymic.Text = "";
                    tb_PhoneNumber.Text = "";
                    tb_PassNumber.Text = "";
                }
                else
                {
                    MessageBox.Show("Не возможно добавить данные\nвероятно такой клиент уже существует!");
                }
            }
            
        }

        private void BTN_Change_Click(object sender, RoutedEventArgs e)
        {
            Client changeClient = ListView_Clients.SelectedItem as Client;
            if(changeClient == null)
            {
                MessageBox.Show("Выберите клиента из списка");
            }
            else
            {
                if (InputValidationClientData())
                {
                    var tempSurname = tb_Surname.Text.Trim();
                    var tempName = tb_Name.Text.Trim();
                    var tempPatronymic = tb_Patronymic.Text.Trim();
                    var tempPhoneNumber = tb_PhoneNumber.Text.Trim();
                    var tempPassNumber = tb_PassNumber.Text.Trim();
                    if (userController.UpdateClient(changeClient, tempSurname, tempName, tempPatronymic, tempPhoneNumber, tempPassNumber))
                    {
                        ListView_Clients.Items.Refresh();
                        MessageBox.Show("Данные успешно изменены");
                        tb_Surname.Text = "";
                        tb_Name.Text = "";
                        tb_Patronymic.Text = "";
                        tb_PhoneNumber.Text = "";
                        tb_PassNumber.Text = "";
                        
                    }
                    else
                    {
                        MessageBox.Show("Не возможно изменить данные!");
                    }
                }

                

            }
           

        }
        /// <summary>
        /// Удаление Клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            var deletedClient = ListView_Clients.SelectedItem as Client;            
            _ = userController.DeleteClient(deletedClient) ? MessageBox.Show("Клиент успешно удален") : MessageBox.Show("Не возможно удалить клиента");
        }
        /// <summary>
        /// Нажатие кнопки смены пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_Auth_Click(object sender, RoutedEventArgs e)
        {
            WindowAuth authWin = new();
            authWin.Show();
            this.Close();
            
        }

        private bool InputValidationClientData() 
        {

            if ((tb_Surname.Text == "") ||
                (tb_Name.Text == "") ||
                (tb_Patronymic.Text == "") ||
                (tb_PhoneNumber.Text == "") ||
                (tb_PassNumber.Text == "") || 
                (tb_PhoneNumber.Text.Length < 7 || tb_PhoneNumber.Text.Length > 15))
            {
                tb_Surname.Background = tb_Surname.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                tb_Name.Background = tb_Name.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                tb_Patronymic.Background = tb_Patronymic.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                tb_PhoneNumber.Background = (tb_PhoneNumber.Text == "" || (tb_PhoneNumber.Text.Length < 7 || tb_PhoneNumber.Text.Length > 12)) ? Brushes.Orchid : Brushes.Transparent;
                tb_PassNumber.Background = tb_PassNumber.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                MessageBox.Show("Не все поля заполнены коррктно");
                return false;
            }
            else
            {
                tb_Surname.Background = Brushes.Transparent;
                tb_Name.Background = Brushes.Transparent;
                tb_Patronymic.Background = Brushes.Transparent;
                tb_PhoneNumber.Background = Brushes.Transparent;
                tb_PassNumber.Background = Brushes.Transparent;

                return true;
            }
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                ListView_Clients.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ListView_Clients.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
