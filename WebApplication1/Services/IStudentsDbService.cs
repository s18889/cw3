﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStudentsDbService 
    {
        public IActionResult StrudentEnrolment(AddStudent student);
        bool StudentExists(string dbs);
    }
}
