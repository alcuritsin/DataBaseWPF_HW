using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using DataBaseLib.DataModel;
using Microsoft.Data.Sqlite;

namespace DataBaseLib
{
    public class DataBase
    {
        private SqliteConnection _db;
        private SqliteCommand _command;

        public DataBase()
        {
            var config = DbConfig.Import("db_config.json");

            _db = new SqliteConnection();
            _db.ConnectionString = config.ToString();

            _command = new SqliteCommand { Connection = _db };
        }

        public ObservableCollection<Account> GetAllAccounts()
        {
            _db.Open();

            var sql = @"SELECT table_account.id as 'id', login, password, is_active, role, table_role.id as 'role_id' FROM table_account JOIN table_account_role ON table_account.id = table_account_role.account_id JOIN table_role ON table_account_role.role_id = table_role.id";
            _command.CommandText = sql;
            var result = _command.ExecuteReader();
            if (!result.HasRows) throw new Exception("Данные в БД не найдены");

            var accounts = new ObservableCollection<Account>();
            while (result.Read())
            {
                var account = new Account
                {
                    Id = result.GetInt32("id"),
                    Login = result.GetString("login"),
                    Password = result.GetString("password"),
                    IsActive = result.GetBoolean("is_active"),
                    Role = new Role
                    {
                        Id = result.GetInt32("role_id"),
                        Name = result.GetString("role")
                    }
                };

                accounts.Add(account);
            }

            result.Close();
            _db.Close();

            return accounts;
        }

        public ObservableCollection<Role> GetAllRoles()
        {
            _db.Open();

            var roles = new ObservableCollection<Role>();

            var sql = "SELECT id, role FROM table_role";
            _command.CommandText = sql;
            var res = _command.ExecuteReader();

            if (!res.HasRows) throw new Exception("Данные в БД не найдены");

            while (res.Read())
            {
                var role = new Role
                {
                    Id = res.GetInt32("id"),
                    Name = res.GetString("role")
                };
                
                roles.Add(role);
            }

            res.Close();
            _db.Close();

            return roles;
        }

        public ObservableCollection<User> GetAllUsers()
        {
            _db.Open();

            var users = new ObservableCollection<User>();

            var sql = "SELECT id, first_name, last_name FROM table_user";
            _command.CommandText = sql;
            SqliteDataReader res = _command.ExecuteReader();

            if (!res.HasRows) throw new Exception("Данные в БД не найдены");

            while (res.Read())
            {
                var user = new User
                {
                    Id = res.GetInt32("id"),
                    FirstName = res.GetString("first_name"),
                    LastName = res.GetString("last_name")
                };
                
                users.Add(user);
            }
            res.Close();
            _db.Close();

            for (int i = 0; i < users.Count; i++)
            {
                _db.Open();

                sql = $"SELECT email FROM table_email WHERE user_id = '{users[i].Id}'";
                _command.CommandText = sql;
                var result = _command.ExecuteReader();

                var emails = new List<string>();

                while (result.Read())
                {
                    var email = result.GetString("email");
                    emails.Add(email);
                }

                users[i].ListOfEmail = emails;

                result.Close();

                _db.Close();
            }

            return users;
        }
    }
}