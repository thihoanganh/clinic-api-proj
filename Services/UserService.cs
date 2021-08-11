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
    public class UserService : IUserService
    {
        private readonly ClinicDbContext _db;
        public IConfiguration _config { get; }
        public UserService(ClinicDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
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
                var existUser = _db.Users.Where(s => s.Username == username).FirstOrDefault();
                if (existUser != null)
                {
                    bool verified = BCrypt.Net.BCrypt.Verify(password, existUser.Password);
                    if (verified)
                    {
                        //login verified
                        //generate token for that user
                        var jwtHelper = new JwtAuthHelper(_config);
                        var token = jwtHelper.GenerateJwttoken(existUser);
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

        public int CreateUser(User user)
        {
            try
            {
                if (user != null)
                {
                    if (IsUserExist(user.Username) == null) // user doesnt exist
                    {
                        // create user
                        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                        _db.Users.Add(user);
                        _db.SaveChanges();
                        return user.Id;
                    }
                }
                return -1;

            }
            catch (Exception)
            {

                return -1;
            }
        }

        public User IsUserExist(string username)
        {
            var user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            return user == null ? null : user;
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
                var user = _db.Users.Where(u => u.Id == id).FirstOrDefault();
                if (user == null) return -1;
                else
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                    return id;
                }
            }
            catch (Exception)
            {

                return -1;
            }

        }

        public User Find(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                return user;
            }

            return null;

        }

        public List<User> Find(string term)
        {
            // search by username
            var users = _db.Users.Where(s => s.Username.Contains(term)).ToList();
            if (users == null)
            {
                //if null 
                // search by fullname
                return _db.Users.Where(u => u.FullName.Contains(term)).ToList();
            }
            else
            {
                return users;
            }
        }

        public (List<User> users, int totalPage, int totalUsers) FindAll(int page)
        {
            try
            {
                var Size = 5;
                var TotalUsers = _db.Users.Count();
                var TotalPage = (int)Math.Ceiling(((double)TotalUsers / Size));
                return (_db.Users.OrderByDescending(a => a.Id).Skip((page - 1) * Size).Take(Size).ToList(), TotalPage, TotalUsers);
            }
            catch (Exception)
            {
                return (null, -1, -1);
                throw;
            }
        }

        /// <summary>
        /// return object already update information
        /// else return null
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public bool Update(User user)
        {
            var dbUser = _db.Users.Find(user.Id);
            if (dbUser == null) return false;
            else
            {
                _db.Entry(dbUser).CurrentValues.SetValues(user);
                _db.SaveChanges();
                return true;
            }

        }
        public bool UpdateUserPassword(string username, string newPw)
        {
            var user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            else
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPw);
                _db.SaveChanges();
                return true;
            }
        }
    }
}

