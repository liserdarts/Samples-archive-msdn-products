using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http;
using HousingApplicationWeb.Models;

namespace HousingApplicationWeb.Controllers
{
    public class StudentController : ODataController
    {
        public Student Get([FromODataUri] string key)
        {
            Student student = new Student();
            student.StudentId = key;
            
            //This code just assigns a random number of completed credits
            Random r = new Random();
            student.CompletedCredits = r.Next(0, 150);

            return student;
        }
    }
}
