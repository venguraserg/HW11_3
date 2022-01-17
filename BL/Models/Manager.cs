using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    /// <summary>
    /// Класс Менеджера
    /// </summary>
    public class Manager : IUserInteface
    {
        private Guid _id;
        private string _name;
        private string _status;
        
        public Guid Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Status { get { return _status; } set { _status = value; } }

        public Manager(Guid id, string name, string status)
        {
            _id = id;
            _name = name;
            _status = status;
        }
               
        /// <summary>
        /// Метод добавления клиента
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passNumber"></param>
        /// <returns></returns>
        public Client AddClient(string surname, string name, string patronymic, string phoneNumber, string passNumber)
        {
            return new Client(surname, name, patronymic, phoneNumber, passNumber, this);
        }
        /// <summary>
        /// Метод получения всех клиентов 
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public ObservableCollection<Client> GetAllClient(List<Client> clients)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Метод обновления клиента
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passNumber"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Client UpdateClient(string surname, string name, string patronymic, string phoneNumber, string passNumber, Client client)
        {
            string changeText = string.Empty;
            if (client.Surname != surname) changeText += "surname, ";
            client.Surname = surname;
            if (client.Name != name) changeText += "name, ";
            client.Name = name;
            if (client.Patronymic != patronymic) changeText += "patronymic, ";
            client.Patronymic = patronymic;
            if (client.PhoneNumber != phoneNumber) changeText += "phoneNumber, ";
            client.PhoneNumber = phoneNumber;
            if (client.PassNumber != passNumber) changeText += "passNumber, ";
            client.PassNumber = passNumber;
            client.Change = new Change(DateTime.Now, changeText, ModifiType.Update, this);
            return client;
        }
        /// <summary>
        /// Метод удаления клиента
        /// </summary>
        /// <returns></returns>
        public bool DeleteClient()
        {
            return true;
        }

    }
}
