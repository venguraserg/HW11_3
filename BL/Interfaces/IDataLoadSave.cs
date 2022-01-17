using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    /// <summary>
    /// Интерфейс чтения/записи данных
    /// </summary>
    interface IDataLoadSave
    {
        /// <summary>
        /// метод сохранения данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="items"></param>
        void Save<T>(string fileName, ObservableCollection<T> items);
        /// <summary>
        /// метод загрузки данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ObservableCollection<T> Load<T>(string fileName);
    }
}
