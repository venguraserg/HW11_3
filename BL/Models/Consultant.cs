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
    /// Класс Консультанта
    /// </summary>
    public class Consultant : IUserInteface
    {
        private Guid _id;
        private string _name;
        private string _status;

        public Guid Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Status { get { return _status; } set { _status = value; } }

        /// <summary>
        /// Конструкторы
        /// </summary>
        public Consultant() { }
        public Consultant(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = "consultant";
        }
        public Consultant(Guid id, string name, string status)
        {
            _id = id;
            _name = name;
            _status = status;
        }



        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passNumber"></param>
        /// <returns></returns>
        public Client AddClient(string surname, string name, string patronymic, string phoneNumber, string passNumber)
        {
            return null;
        }
       
        /// <summary>
        /// получение всех клиентов
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        public ObservableCollection<Client> GetAllClient(List<Client> clients)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Обновление клиента
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
            client.PhoneNumber = phoneNumber;
            client.Change = new Change(DateTime.Now, "phone Number", ModifiType.Update, this);
            return client;
        }
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <returns></returns>
        public bool DeleteClient()
        {
            return false;
        }

    }
}
