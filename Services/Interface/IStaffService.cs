using Clinic_Web_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic_Web_Api.Services.Interface
{
    public interface IStaffService
    {
        int CreateStaff(Staff staff);
        int Delete(int id);
        bool Update(Staff staff);
        Staff Find(int id);
        List<Staff> Find(string term);
        List<Staff> FindAll();
        string Login(string username, string password);


    }
}
