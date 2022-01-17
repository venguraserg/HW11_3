using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    /// <summary>
    /// Класс Клиента
    /// </summary>
    public class Client
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string PassNumber { get; set; }
        public Change Change { get; set; }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Client() { }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id"></param>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passNumber"></param>
        /// <param name="change"></param>
        public Client(Guid id, string surname, string name, string patronymic, string phoneNumber, string passNumber, Change change)
        {
            Id = id;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            PassNumber = passNumber;
            Change = change;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="patronymic"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="passNumber"></param>
        /// <param name="user"></param>
        public Client(string surname, string name, string patronymic, string phoneNumber, string passNumber, IUserInteface user)
        {
            Id = Guid.NewGuid();
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            PassNumber = passNumber;
            Change = new Change(DateTime.Now, "All fild", ModifiType.Created, user);
        }

    }
}
