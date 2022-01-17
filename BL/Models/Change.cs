using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public enum ModifiType
    {
        Created = 0,
        Update = 1
            //Deleted = 2 //Походу лишнее
    }

    /// <summary>
    /// класс Логирования изменений
    /// </summary>
    public class Change
    {
        private DateTime changeClient;
        private string modifiedData;
        private ModifiType modifiedType;
        private IUserInteface user;

        public DateTime ChangeDatetime { get => changeClient; set => changeClient = value; }
        public string ModifiedData { get => modifiedData; set => modifiedData = value; }
        public ModifiType ModifieType { get => modifiedType; set => modifiedType = value; }
        public IUserInteface User { get => user; set => user = value; }
        
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Change() 
        {
            ChangeDatetime = DateTime.Now;
            ModifiedData = "Create All Fields";
            ModifieType = ModifiType.Created;
            User = new Administrator();
            //Save();
        }
        /// <summary>
        /// Конструктор 2
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="modifiedData"></param>
        /// <param name="modifiType"></param>
        /// <param name="user"></param>
        public Change(DateTime dateTime, string modifiedData, ModifiType modifiType, IUserInteface user)
        {
            ChangeDatetime = dateTime;
            ModifiedData = modifiedData;
            ModifieType = modifiType;
            User = user;
            Save();
        }
        /// <summary>
        /// переопределенный ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Date change - {ChangeDatetime.ToShortDateString()} - {ChangeDatetime.ToShortTimeString()} | {ModifiedData} | {ModifieType.ToString()} | {User.Name}";
        }
        /// <summary>
        /// Метод сохранения в лог
        /// </summary>
        private void Save()
        {
            StreamWriter writer = new StreamWriter("log.txt",true);
            writer.WriteLine(this.ToString());
            writer.Close();

        }
    }
}
