using Dapper;
using MySql.Data.MySqlClient;
using Page_Navigation_App.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using User = Page_Navigation_App.DataAccess.Model.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Formats.Asn1.AsnWriter;

namespace Page_Navigation_App.DataAccess
{
    public class MySqlConnector
    {
        private readonly string Constr = "Server=159.223.86.230;Database=english;Uid=english;Pwd=3ZsmHN8AjDYSjB3Y;";
        public async Task<IEnumerable<QuizQuestion>> GetQuizQuestionsAsync(int num = 5)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                await connection.OpenAsync();
                return await connection.QueryAsync<QuizQuestion>(
                    "SELECT * FROM quiz_questions ORDER BY RAND() LIMIT @Num", new { Num = num });
            }
            catch
            {
                return new ObservableCollection<QuizQuestion>();
            }
        }

        public User GetUser(string email, string password)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                var user = connection.QuerySingleOrDefault<User>(
                    "SELECT * FROM users WHERE email = @Email",
                    new { Email = email });
                bool isValid = user.Password == Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: password,
                        salt: GetStoredSalt(),
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));
                if (isValid)
                    return user;
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public bool Register(User user)
        {
            try
            {
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                     password: user.Password,
                     salt: GetStoredSalt(),
                     prf: KeyDerivationPrf.HMACSHA256,
                     iterationCount: 10000,
                     numBytesRequested: 256 / 8));
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                string sql = "INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password)";
                var parameters = new { user.Name, user.Email, Password = hashedPassword };
                connection.Execute(sql, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<QuizzAchievement>> GetAchievements(int userId)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                await connection.OpenAsync();
                var achievements = await connection.QueryAsync<QuizzAchievement>(
                    "SELECT * FROM quizz_achievements WHERE user_id = @UserId",
                    new { UserId = userId });
                return achievements.ToList();
            }
            catch
            {
                return new List<QuizzAchievement>();
            }
        }

        public bool InsertAchievement(QuizzAchievement achievement)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                var rowsAffected = connection.Execute(
                   "INSERT INTO quizz_achievements (score, question, time, user_id) VALUES (@Score, @Question, @Time, @UserId)",
                   achievement
                 );
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static byte[] GetStoredSalt()
        {
            // Replace this with your own code to retrieve the stored salt from your database or other storage location
            return new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        }
    }
}
