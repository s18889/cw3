﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    interface IEnrolmentUp
    {
        
            public IActionResult UpEnrolment(enrolmentUp enrolment);

       

    }
}
