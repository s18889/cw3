﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public interface IDBService
    {

        public IEnumerable<Student> GetStudents();
        bool checkPas(string[] credentials);
        public void putKay(String kay);
        public bool testKay(String kay);
    }
}
