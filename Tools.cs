﻿using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using StockMaster.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static StockMaster.Windows.Pages.CompanyPage;
using static StockMaster.Windows.Pages.PortfolioPage;
using static StockMaster.Windows.SectorPage;
using static StockMaster.Windows.StockPage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using User = StockMaster.Entitys.User;

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

            var existingUser = StockContext.GetAllUsers().SingleOrDefault(u => u.Email == email);

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

        public static User GetCurrentUserById(int idUser)
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.Users.
                    Where(u => u.UserId == idUser).
                    FirstOrDefault();
            }
        }

        public static int GetIdPortfolio(int idUser)
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.Portfolios.
                    Where(u => u.UserId == idUser).
                    Include(s => s.PortfolioId).
                    Select(s => s.PortfolioId).FirstOrDefault();
            }
        }

        public static List<PortfolioViewModel> GetCurrentPortfolio(int idPortfolio)
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.PortfolioStocks
                  .Where(s => s.PortfolioId == idPortfolio)
                 .Include(s => s.StockId) // Подгружаем сектор
                 .Select(s => new PortfolioViewModel
                 {
                     Name = s.Stock.Symbol,
                     BuyPrice = s.Stock.CurrentPrice,
                     Quantity = s.Quantity,
                     Sum = s.PurchasePrice * s.Quantity // Предполагаем, что Sector имеет свойство Name
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

        public static List<UserViewModel> GetAllUsers()
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.Users
                 .Select(u => new UserViewModel
                 {
                     Username = u.Username,
                     Email = u.Email, // Предполагаем, что Sector имеет свойство Name
                     Role = u.Role.ToString()
                 })
                 .ToList();
            }
        }
        public static List<CompanyViewModel> GetAllCompany()
        {
            using (var context = new StockContext()) // Замените на ваш контекст
            {
                return context.Companies
                 .Select(s => new CompanyViewModel
                 {
                     Name = s.Name,
                     Sector = s.Industry, // Предполагаем, что Sector имеет свойство Name
                     Ceo = s.Ceo,
                     Headquarters = s.Headquarters
                 })
                 .ToList();
            }
        }
        public static int GetStockId(string name)
        {
            var allStock = StockContext.GetAllStocks().SingleOrDefault(s => s.Symbol == name);

            return allStock.StockId;
        }

        public static int GetUserIdByEmail(string login)
        {
            User user = StockContext.GetAllUsers().
                Where(u => u.Email == login).
                SingleOrDefault();

            if (user == null)
                throw new Exception("User is null!!");

            return user.UserId;
        }


        public static Portfolio GetUserPortfolio(int userId)
        {

            var portfolio = StockContext.GetAllPortfolios().SingleOrDefault(p => p.UserId == userId);

            if (portfolio == null)
                throw new Exception("Portfolio is null!!");

            return portfolio; // Возвращает ID портфолио или null, если не найдено
        }




        public static bool IsUserExist(string login, string password)
        {
            var user = StockContext.GetAllUsers().SingleOrDefault(u => u.Email == login);

            if (user != null && user.Password == password)
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

        public static bool isDataForUserCorrect(string login, string password,string email)
        {


            if (!IsValidEmail(email))
            {
                MessageBox.Show("Некорректный адрес электронной почты!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (login.Length <= 5)
            {
                MessageBox.Show("Логин должен содержать более 5 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов, включать заглавные и строчные буквы, цифры и специальные символы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!IsUniqEmail(email))
            {
                MessageBox.Show("Такой пользователь уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public static void RegisterUser(string login, string email, string password)
        {
            User newUser = CreateUser(login, email, password);

            StockContext.AddNewUser(newUser);

            CreateEmptyPortfolio(newUser.UserId);
        }

        private static void CreateEmptyPortfolio(int userId)
        {
            var newPortfolio = new Portfolio
            {
                
                UserId = userId,
                PortfolioName = "Портфолио номер:" + userId, // Название портфолио по умолчанию
                CreatedAt = DateTime.Now,
                TotalValue = 0 // Начальная стоимость
            };

            StockContext.AddNewPortfolio(newPortfolio); // Предположим, у вас есть этот метод в StockContext
                                                        // Сохраняем изменения в базе данных
        }

    }
}
