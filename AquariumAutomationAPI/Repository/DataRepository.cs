using AquariumAutomationAPI.Context;
using AquariumAutomationAPI.Models;
using Dapper;
using System.Data;

namespace AquariumAutomationAPI.Repository
{
    public class DataRepository:IDataRepository
    {
        protected readonly DataContext _dataContext;
        private readonly Dictionary<string, string> _sqlCommandStore = new Dictionary<string, string>
        {
            ["GetUserByEmail"]= @$"AquariumAutomationApp.dbo.uspGetUserByEmail",
            ["RegisterUserToDb"]= @$"AquariumAutomationApp.dbo.uspRegisterUser"
        };

        public DataRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public User? GetUserByEmail(string Email)
        {
            string storedProcedure = _sqlCommandStore["GetUserByEmail"];
            List<User> users = _dataContext.ExecuteStoredProcedure<User>(storedProcedure, new { Email }).ToList();

            if (users.Count == 0)
            {
                return null;
            }
            else
            {
                return users[0];
            }
        }
        public User RegisterUserToDb(User registerUser)
        {

            using (var connection = _dataContext.CreateConnection())
            {
                string storedProcedure = _sqlCommandStore["RegisterUserToDb"];

                var parameters = new DynamicParameters();
                parameters.Add("@UserFirstName", registerUser.UserFirstName);
                parameters.Add("@UserLastname", registerUser.UserLastName);
                parameters.Add("@UserEmail", registerUser.UserEmail);
                parameters.Add("@UserPhoneNumber", registerUser.PhoneNumber);
                parameters.Add("@UserTypeId", registerUser.UserTypeId);
                parameters.Add("@UserCreatedDate", registerUser.UserCreatedOnDate);
                parameters.Add("@PasswordHash", registerUser.PasswordHash);
                parameters.Add("@PasswordSalt", registerUser.PasswordSalt);
                parameters.Add("@AccountCreatedDate", registerUser.AccountCreatedDate);
                parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@AccountId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                registerUser.UserId = parameters.Get<int>("@UserId");
                registerUser.AccountId = parameters.Get<int>("@AccountId");

            }
            return registerUser;
        }
    }
}
