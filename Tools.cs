using StockMaster.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static StockMaster.Windows.SectorPage;
using static StockMaster.Windows.StockPage;

namespace StockMaster
{
    public static class Tools
    {
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidName(string name)
        {
            string pattern = @"^[A-Za-zА-Яа-яЁё\s]+$";
            return Regex.IsMatch(name, pattern);
        }

        public static bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        public static bool IsUniqEmail(string email)
        {

            var existingUser = StockContext.getAllUsers().SingleOrDefault(u => u.Email == email);

            if (existingUser == null) 
                return true;

            return false;
        }

        public static List<StockViewModel> GetAllStock()
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.Stocks
                 .Include(s => s.Sector) // Подгружаем сектор
                 .Select(s => new StockViewModel
                 {
                     Name = s.Symbol,
                     Sector = s.Sector.Name, // Предполагаем, что Sector имеет свойство Name
                     BuyPrice = s.CurrentPrice
                 })
                 .ToList();
            }
        }

        public static List<SectorViewModel> GetAllSector()
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.Sectors
                 .Select(s => new SectorViewModel
                 {
                     Name = s.Name,
                     Desription = s.Description, // Предполагаем, что Sector имеет свойство Name
                     CountCompanyis = s.Stocks.Count()
                 })
                 .ToList();
            }
        }

        public static bool IsUserExist(string login, string password)
        {
            var user = StockContext.getAllUsers().SingleOrDefault(u => u.Email== login);

            if(user!= null && user.Password == password) 
                return true;
            return false;

        }
        public static User CreateUser(string login, string email, string password)
        {
            return new User
            {
                Username = login,
                Email = email,
                Password = password, // Обязательно хэшируйте пароль перед сохранением в БД
                Role = 2 // Устанавливаем роль по умолчанию
            };
        }

    }
}
