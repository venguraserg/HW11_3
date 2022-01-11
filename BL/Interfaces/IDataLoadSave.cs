﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    interface IDataLoadSave
    {
        void Save<T>(string fileName, ObservableCollection<T> items);
        public ObservableCollection<T> Load<T>(string fileName);
    }
}
