﻿using BL.Interfaces;
using BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Controllers
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    public class Controller : IDataLoadSave
    {
        // приватные поля для определения пути файлов хранения данных
        private static readonly string USER_FILE_NAME = "users.json";
        private static readonly string CLIENT_FILE_NAME = "clients.json";

        public ObservableCollection<IUserInteface> Users { get; set; }

        public IUserInteface CurentUser { get; set; }
        public bool IsNewUser { get; } = false;

        public ObservableCollection<Client> Clients { get; set; }





        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        public Controller(string userName)
        {
            this.Users = LoadUser();

            CurentUser = Users.SingleOrDefault(u => u.Name == userName);
            if (CurentUser == null)
            {
                CurentUser = new Consultant(userName);
                Users.Add(CurentUser);
                IsNewUser = true;

            }
            Clients = Load<Client>(CLIENT_FILE_NAME);
            Save(USER_FILE_NAME, Users);
        }


        /// <summary>
        /// Получение списка всех пользователей из файла
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<IUserInteface> LoadUser()
        {
            ObservableCollection<IUserInteface> tempUsers = new ObservableCollection<IUserInteface>();
            var loadData = Load<DessUser>(USER_FILE_NAME);

            if (loadData.Count < 1) { tempUsers.Add(new Administrator()); }

            for (int i = 0; i < loadData.Count; i++)
            {
                switch (loadData[i].Status)
                {
                    case "consultant":
                        tempUsers.Add(new Consultant(loadData[i].Id, loadData[i].Name, loadData[i].Status));
                        break;
                    case "manager":
                        tempUsers.Add(new Manager(loadData[i].Id, loadData[i].Name, loadData[i].Status));
                        break;
                    case "admin":
                        tempUsers.Add(new Administrator(loadData[i].Id, loadData[i].Name, loadData[i].Status));
                        break;
                    default:
                        break;
                }
            }
            return tempUsers;

        }

        /// <summary>
        /// Автозаполнение клиентов для теста
        /// </summary>
        /// <param name="number"></param>
        public void ClientAutofill(int number)
        {
            Clients = new ObservableCollection<Client>();
            for (int i = 0; i < number; i++)
            {
                string tempGuid = Guid.NewGuid().ToString();
                string[] stringMassive = tempGuid.Split(new char[] { '-' });

                Clients.Add(new Client(Guid.NewGuid(), stringMassive[0], stringMassive[1], stringMassive[2], stringMassive[3], stringMassive[4])); ;
            }
            Save(CLIENT_FILE_NAME,Clients);
        }

        public void AddClient(string tempSurname, string tempName, string tempPatronymic, string tempPhoneNumber, string tempPassNumber)
        {
            var newUser = CurentUser.AddClient(tempSurname, tempName, tempPatronymic, tempPhoneNumber, tempPassNumber);
            var tempUser = Clients.FirstOrDefault(i => i.Id == newUser.Id);
            if(newUser != null || tempUser!=null)
            {
                Clients.Add(newUser);
                Save(CLIENT_FILE_NAME, Clients);
            }
        }


        /// <summary>
        /// Метод смены типа консультанта на клиента
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ChangeUserStatus(string name)
        {
            if (CurentUser is Administrator)
            {
                int index;
                IUserInteface user = Users.SingleOrDefault(u => u.Name == name);
                if (user != null)
                {
                    index = Users.IndexOf(user);

                    switch (user.Status)
                    {
                        case "consultant":
                            Users[index] = new Manager(user.Id, user.Name, "manager");
                            break;
                        case "manager":
                            Users[index] = new Consultant(user.Id, user.Name, "consultant");
                            break;
                        default:
                            break;
                    }

                    Save(USER_FILE_NAME,Users);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Десериализация данных их файла
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ObservableCollection<T> Load<T>(string fileName)
        {
            ObservableCollection<T> tempItems = new ObservableCollection<T>();

            if (File.Exists(fileName) == false)
            {
                using (File.Create(fileName)) { };
                ObservableCollection<T> tempItemList = new();
                return tempItemList;
            }
            else
            {
                string json = File.ReadAllText(fileName);
                if (string.IsNullOrEmpty(json)) return new ObservableCollection<T>();
                var items = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
                return items;
            }
        }

        /// <summary>
        /// Сериализация данных их файла
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="items"></param>
        public void Save<T>(string fileName, ObservableCollection<T> items)
        {
            if (File.Exists(fileName) == false)
            {
                using (File.Create(fileName)) { };
            }

            string json = JsonConvert.SerializeObject(items);
            File.WriteAllText(fileName, json);
        }
    }
}
