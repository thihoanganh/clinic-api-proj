using Clinic_Web_Api.Helpers;
using Clinic_Web_Api.Models;
using Clinic_Web_Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Clinic_Web_Api.Services
{
    public class StaffService : IStaffService
    {
        private readonly ClinicDbContext _db;
        public IConfiguration _config { get; }

        public StaffService(ClinicDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public int CreateStaff(Staff staff)
        {
            try
            {
                if (staff != null)
                {
                    staff.Status = true;
                    staff.Role = _db.Roles.Where(r => r.Id == 2).First();
                    staff.Position = _db.Positions.Where(p => p.Id == staff.PositionId).First();
                    staff.Password = BCrypt.Net.BCrypt.HashPassword(staff.Password);
                    _db.Staff.Add(staff);
                    _db.SaveChanges();
                    return staff.Id;
                }
                return -1;

            }
            catch (Exception)
            {

                return -1;
            }
        }

        /// <summary>
        /// return delete id
        /// else return -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                var staff = _db.Staff.Where(s => s.Id == id).FirstOrDefault();
                if (staff == null) return -1;
                else
                {
                    _db.Staff.Remove(staff);
                    _db.SaveChanges();
                    return id;
                }
            }
            catch (Exception)
            {

                return -1;
            }

        }

        public Staff Find(int id)
        {
            var staff = _db.Staff.Find(id);
            if (staff != null)
            {
                _db.Entry(staff).Reference(s => s.Position).Load();
                _db.Entry(staff).Reference(s => s.Role).Load();
                return staff;
            }

            return null;

        }

        public List<Staff> Find(string term)
        {
            // search by name
            var staffs = _db.Staff.Where(s => s.Name.Contains(term)).Include(s => s.Position).Include(s => s.Role).AsNoTracking().ToList();
            if (staffs == null)
            {
                //if null 
                // search by username
                return _db.Staff.Where(s => s.Username.Contains(term)).Include(s => s.Position).Include(s => s.Role).AsNoTracking().ToList();
            }
            else
            {
                return staffs;
            }
        }

        public List<Staff> FindAll()
        {
            return _db.Staff.Include(s => s.Position).Include(s => s.Role).AsNoTracking().ToList();
        }

        /// <summary>
        /// return object already update information
        /// else return null
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public bool Update(Staff staff)
        {
            var dbStaff = _db.Staff.Find(staff.Id);
            if (dbStaff == null) return false;
            else
            {
                _db.Entry(dbStaff).CurrentValues.SetValues(staff);
                _db.SaveChanges();
                return true;
            }

        }
        /// <summary>
        /// login method will return a token if login verified
        /// else return null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Login(string username, string password)
        {
            try
            {
                var existStaff = _db.Staff.Where(s => s.Username == username).Include(s => s.Role).FirstOrDefault();
                if (existStaff != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(password, existStaff.Password);
                    if (verified)
                    {
                        //login verified
                        //generate token for that user
                        var jwtHelper = new JwtAuthHelper(_config);
                        var token = jwtHelper.GenerateJwttoken(existStaff);
                        return token;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
    }
}

