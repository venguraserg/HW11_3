using BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IUserInteface
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Status { get; set; }
        Client AddClient(string surname, string name, string patronymic, string phoneNumber, string passNumber);
        ObservableCollection<Client> GetAllClient(List<Client> clients);
        Client UpdateClient(string surname, string name, string patronymic, string phoneNumber, string passNumber, Client client);
        bool DeleteClient();
    }
}
