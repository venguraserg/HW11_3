﻿using BL.Controllers;
using BL.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW11_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller userController;
        private Client selectedClient;
        public MainWindow(Controller userController)
        {
            InitializeComponent();
            selectedClient = new Client();
            this.userController = userController;
            TB_User.Text = $"Пользователь: {userController.CurentUser.Name}";
            TB_UserStatus.Text = $"Статус: {userController.CurentUser.GetType().Name}";

            ListView_Clients.ItemsSource = this.userController.Clients;


            Surname.IsEnabled = userController.CurentUser is not Consultant;
            Name.IsEnabled = userController.CurentUser is not Consultant;
            Patronymic.IsEnabled = userController.CurentUser is not Consultant;
            PassNumber.IsEnabled = userController.CurentUser is not Consultant;
            BTN_Add.IsEnabled = userController.CurentUser is not Consultant;
            BTN_Delete.IsEnabled = false;
            BTN_Change.IsEnabled = false;

            



        }

        private void ListView_Clients_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ListView_Clients.SelectedItem is Client item)
            {
                Surname.Text = item.Surname;
                Name.Text = item.Name;
                Patronymic.Text = item.Patronymic;
                PhoneNumber.Text = item.PhoneNumber;
                PassNumber.Text = item.PassNumber;
            }
            else
            {
                Surname.Text = "";
                Name.Text = "";
                Patronymic.Text = "";
                PhoneNumber.Text = "";
                PassNumber.Text = "";
            }
            if (ListView_Clients.SelectedItems.Count > 0)
                {
                    BTN_Change.IsEnabled = true;
                    BTN_Delete.IsEnabled = userController.CurentUser is Consultant ? false : true;
                }
                else
                {
                    BTN_Change.IsEnabled = false;
                    BTN_Delete.IsEnabled = false;
                }

            
        }

        private void BTN_Add_Click(object sender, RoutedEventArgs e)
        {
            if ((Surname.Text == "") || (Name.Text == "") || (Patronymic.Text == "") || (PhoneNumber.Text == "") || (PassNumber.Text == "")) 
            {                
                Surname.Background = Surname.Text=="" ? Brushes.Orchid : Brushes.Transparent;
                Name.Background = Name.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                Patronymic.Background = Patronymic.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                PhoneNumber.Background = PhoneNumber.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                PassNumber.Background = PassNumber.Text == "" ? Brushes.Orchid : Brushes.Transparent;
                MessageBox.Show("Не все поля заполнены");

            }
            else
            {
                Surname.Background = Brushes.Transparent;
                Name.Background = Brushes.Transparent;
                Patronymic.Background = Brushes.Transparent;
                PhoneNumber.Background = Brushes.Transparent;
                PassNumber.Background = Brushes.Transparent;

                var tempSurname = Surname.Text.Trim();
                var tempName = Name.Text.Trim();
                var tempPatronymic = Patronymic.Text.Trim();
                var tempPhoneNumber = PhoneNumber.Text.Trim();
                var tempPassNumber = PassNumber.Text.Trim();
                if (userController.AddClient(tempSurname, tempName, tempPatronymic, tempPhoneNumber, tempPassNumber))
                {
                    MessageBox.Show("Данные успешно добавлены");
                }
                else
                {
                    MessageBox.Show("Не возможно добавить данные\nвероятно такой клиент уже существует!");
                }
            }            
        }

        private void BTN_Change_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Delete_Click(object sender, RoutedEventArgs e)
        {
            var q = ListView_Clients.SelectedItem as Client;
            //userController.DeleteClient(ListView_Clients.SelectedItem as Client);
        }

        private void BTN_Auth_Click(object sender, RoutedEventArgs e)
        {
            WindowAuth authWin = new();
            authWin.Show();
            this.Close();
        }
    }
}
