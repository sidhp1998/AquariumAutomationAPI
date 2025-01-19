﻿using AquariumAutomationAPI.Context;
using AquariumAutomationAPI.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace AquariumAutomationAPI.Repository
{
    public class DataRepository:IDataRepository
    {
        protected readonly DataContext _dataContext;
        private readonly Dictionary<string, string> _sqlCommandStore = new Dictionary<string, string>
        {
            ["GetUserByEmail"] = @$"AquariumAutomationApp.dbo.uspGetUserByEmail",
            ["RegisterUserToDb"] = @$"AquariumAutomationApp.dbo.uspRegisterUser",
            ["CreateNewAquariumToDb"] = $@"AquariumAutomationApp.dbo.uspCreateAquarium",
            ["GetAquariumInfoById"] = $@"AquariumAutomationApp.dbo.uspGetAquariumInfoById"
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
        public User? RegisterUserToDb(User registerUser)
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

        public Aquarium? CreateNewAquariumToDb(Aquarium aquarium)
        {
            if(aquarium == null) return null;

            using (var connection = _dataContext.CreateConnection())
            {
                string storedProcedure = _sqlCommandStore["CreateNewAquariumToDb"];
                
                var parameters = new DynamicParameters();
                parameters.Add("@AquariumName", aquarium.AquariumName);
                parameters.Add("@AquariumDescription", aquarium.AquariumDescription);
                parameters.Add("@UserId",aquarium.UserId);
                parameters.Add("@IsActive", aquarium.IsActive);
                parameters.Add("@AquariumComments", aquarium.AquariumComments);
                parameters.Add("@AquariumCreatedDate", aquarium.AquariumCreatedDate);
                parameters.Add("@AquariumFixedPropertyComments", aquarium.AquariumFixedPropertyComments);
                parameters.Add("@Length", aquarium.Length);
                parameters.Add("@Width", aquarium.Width);
                parameters.Add("@Height", aquarium.Height);
                parameters.Add("@AquariumFixedPropertyCreatedDate", aquarium.AquariumFixedPropertyCreatedDate);
                parameters.Add("@AquariumId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                parameters.Add("@AquariumFixedPropertyId", dbType: DbType.Int64, direction: ParameterDirection.Output);
                
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                aquarium.AquariumId = (int)parameters.Get<Int64>("@AquariumId");
                aquarium.AquariumFixedPropertyId = (int)parameters.Get<Int64>("@AquariumFixedPropertyId");

                return aquarium;
            }
        }

        public Aquarium? GetAquariumInfoById(int AquariumId)
        {
            string storedProcedure = _sqlCommandStore["GetAquariumInfoById"];
            List<Aquarium> aquariums = _dataContext.ExecuteStoredProcedure<Aquarium>(storedProcedure,new {AquariumId}).ToList();
            if(aquariums.Count == 0)
            {
                return null;
            }
            else
            {
                return aquariums[0];
            }
        }

    }
}
