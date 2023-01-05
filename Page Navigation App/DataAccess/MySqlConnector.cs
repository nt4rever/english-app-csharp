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
using System.Windows;

namespace Page_Navigation_App.DataAccess
{
    public class MySqlConnector
    {
        //private readonly string Constr = "Server=159.223.86.230;Database=english;Uid=english;Pwd=;";
        private readonly string Constr = "Server=localhost;Database=english;Uid=root;Pwd=;";
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
                if (user == null) return null;
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

        public bool UpdateUser(User user)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();

                if (string.IsNullOrEmpty(user.Password))
                {
                    string sql = "UPDATE users SET name = @Name WHERE id = @Id";
                    connection.Execute(sql, new
                    {
                        user.Name,
                        user.Id
                    });
                }
                else
                {
                    string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                     password: user.Password,
                     salt: GetStoredSalt(),
                     prf: KeyDerivationPrf.HMACSHA256,
                     iterationCount: 10000,
                     numBytesRequested: 256 / 8));
                    string sql = "UPDATE users SET name = @Name, password= @Password WHERE id = @Id";
                    connection.Execute(sql, new
                    {
                        user.Name,
                        user.Id,
                        Password = hashedPassword
                    });
                }
                return true;
            }
            catch
            {
                return false;
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

        public List<Vocab> GetVocabs(int userId)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                var vocab = connection.Query<Vocab>("SELECT * FROM vocabularies WHERE user_id = @userId ORDER BY id DESC", new
                {
                    userId
                });
                return vocab.ToList();
            }
            catch
            {
                return new List<Vocab>();
            }
        }

        public bool InsertVocab(Vocab vo)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                var rowsAffected = connection.Execute(
                   "INSERT INTO vocabularies (name, type, meaning, note, user_id) VALUES (@Name, @Type, @Meaning, @Note, @UserId)",
                   vo
                 );
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteVocab(Vocab vo)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                connection.Execute("DELETE FROM vocabularies WHERE id = @Id", new
                {
                    vo.Id
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateVocab(Vocab vo)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                connection.Execute("UPDATE vocabularies SET name = @Name, type = @Type, meaning = @Meaning, note = @Note WHERE id = @Id", vo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Vocab> GetVocabsForLearn(int num, int userId)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                var vocab = connection.Query<Vocab>("SELECT * FROM vocabularies WHERE user_id = @userId ORDER BY RAND() LIMIT @Num"
                    , new { Num = num, userId });
                return vocab.ToList();
            }
            catch
            {
                return new List<Vocab>();
            }
        }


        public Statistic GetInfoDashboard(int userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using var connection = new MySqlConnection(Constr);
                connection.Open();
                int vocabCount = connection.ExecuteScalar<int>("SELECT COUNT(*) as Num FROM vocabularies WHERE user_id = @userId", new { userId });
                var result = connection.QueryFirstOrDefault<(int score, int question)>(
                    "SELECT SUM(score) as score, SUM(question) as question FROM quizz_achievements WHERE user_id = @userId",
                    new { userId }
                );
                int scoreCount = result.score;
                int questionCount = result.question;
                var s = connection.Query<ChartElement>("SELECT SUM(score) as score, SUM(question) as question, DATE(time) as date FROM quizz_achievements WHERE user_id = @userId AND time BETWEEN @startDate AND @endDate GROUP BY DATE(time)", new
                {
                    userId,
                    startDate = startDate,
                    endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59).AddSeconds(59)
                });
                return new Statistic
                {
                    VocabCount = vocabCount,
                    ScoreCount = scoreCount,
                    QuestionCount = questionCount,
                    ChartElements = s.ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Statistic
                {
                    VocabCount = 0,
                    ScoreCount = 0,
                    QuestionCount = 0
                };
            }
        }
        private static byte[] GetStoredSalt()
        {
            // Replace this with your own code to retrieve the stored salt from your database or other storage location
            return new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };
        }
    }
}
