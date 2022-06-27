using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace code
{
    public class UserRepository
    {
        private SqliteConnection connection;
        
        public UserRepository(string databasePath)
        {
            this.connection = new SqliteConnection($"Data Source={databasePath}");
        }

        public UserRepository(SqliteConnection connection)
        {
            this.connection = connection;
        }

        public int Add(User user)
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @"
            INSERT INTO users (login, password, name, email, phone_number, tg_username, prefered_contact)
            VALUES ($login, $password, $name, $email, $phone_number, $tg_username, $prefered_contact);

            SELECT last_insert_rowid(); 
            ";
            command.Parameters.AddWithValue("$login", user.login);
            command.Parameters.AddWithValue("$password", user.password);
            command.Parameters.AddWithValue("$name", user.name);
            command.Parameters.AddWithValue("$email", user.email);
            command.Parameters.AddWithValue("$phone_number", user.phoneNumber);
            command.Parameters.AddWithValue("$tg_username", user.tgUsername);
            command.Parameters.AddWithValue("$prefered_contact", user.preferedContact);

            long id = (long)command.ExecuteScalar();
            connection.Close();
            return (int)id;
        }

        public User FindByLoginAndPassword(string login, string password)
        {
            User user = null;

            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" SELECT * FROM users WHERE login = $login AND password = $password ";
            command.Parameters.AddWithValue("$login", login);
            command.Parameters.AddWithValue("$password", password);
            SqliteDataReader reader = command.ExecuteReader();
            
            if(reader.Read())
            {
                int id = int.Parse(reader.GetString(0));
                string name = reader.GetString(3);
                string email = reader.GetString(4);
                string phoneNumber = reader.GetString(3);
                string tgUsername = reader.GetString(4);
                string preferedContact = reader.GetString(3);
                user = new User(id, login, password, name, email, phoneNumber, tgUsername, preferedContact);
            }            
            reader.Close();
            connection.Close();
            return user;
        }

        public User FindByLogin(string login)
        {
            User user = null;

            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" SELECT * FROM users WHERE login = $login ";
            command.Parameters.AddWithValue("$login", login);
            SqliteDataReader reader = command.ExecuteReader();
            
            if(reader.Read())
            {
                int id = int.Parse(reader.GetString(0));
                string password = reader.GetString(2);
                string name = reader.GetString(3);
                string email = reader.GetString(4);
                string phoneNumber = reader.GetString(5);
                string tgUsername = reader.GetString(6);
                string preferedContact = reader.GetString(7);
                user = new User(id, login, password, name, email, phoneNumber, tgUsername, preferedContact);
            }            
            reader.Close();
            connection.Close();
            return user;
        }

        public List<User> GetAllParticipantsOfTheConference(int confId)
        {
            List<int> participantsIds = new List<int>();
            List<User> participants = new List<User>();

            connection.Open();
            SqliteCommand commandGetParticipantsIds = connection.CreateCommand();
            commandGetParticipantsIds.CommandText =
            @" SELECT * FROM conferences_users WHERE conference_id = $conference_id ";
            commandGetParticipantsIds.Parameters.AddWithValue("$conference_id", confId);
            SqliteDataReader reader = commandGetParticipantsIds.ExecuteReader();
            
            while(reader.Read())
            {
                int participantId = int.Parse(reader.GetString(1));
                participantsIds.Add(participantId);
            }            
            reader.Close();
            connection.Close();

            foreach(int participantId in participantsIds)
            {
                participants.Add(this.FindById(participantId));
            }

            return participants;
        }

        public User FindById(int id)
        {
            User user = null;

            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" SELECT * FROM users WHERE id = $id ";
            command.Parameters.AddWithValue("$id", id);
            SqliteDataReader reader = command.ExecuteReader();
            
            if(reader.Read())
            {
                string login = reader.GetString(1);
                string password = reader.GetString(2);
                string name = reader.GetString(3);
                string email = reader.GetString(4);
                string phoneNumber = reader.GetString(5);
                string tgUsername = reader.GetString(6);
                string preferedContact = reader.GetString(7);
                user = new User(id, login, password, name, email, phoneNumber, tgUsername, preferedContact);
            }            
            reader.Close();
            connection.Close();
            return user;
        }
    }
}