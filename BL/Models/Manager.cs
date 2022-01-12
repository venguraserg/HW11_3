﻿using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
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
               

        public Client AddClient(string surname, string name, string patronymic, string phoneNumber, string passNumber)
        {
            return new Client(surname, name, patronymic, phoneNumber, passNumber);
        }
        public ObservableCollection<Client> GetAllClient(List<Client> clients)
        {
            throw new NotImplementedException();
        }
        public Client UpdateClient(string surname, string name, string patronymic, string phoneNumber, string passNumber, Client client)
        {
            client.Surname = surname;
            client.Name = name;
            client.Patronymic = patronymic;
            client.PhoneNumber = phoneNumber;
            client.PassNumber = passNumber;
            return client;
        }
        public bool DeleteClient()
        {
            return true;
        }

    }
}
