using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace code
{
    public class ConferenceRepository
    {
        private SqliteConnection connection;
        
        public ConferenceRepository(string databasePath)
        {
            this.connection = new SqliteConnection($"Data Source={databasePath}");
        }

        public int Add(Conference conference)
        {
            connection.Open();
            SqliteCommand commandAddConf = connection.CreateCommand();
            commandAddConf.CommandText =
            @"
            INSERT INTO conferences (name, theme, description, time, place, creator_id, state)
            VALUES ($name, $theme, $description, $time, $place, $creator_id, $state);

            SELECT last_insert_rowid(); 
            ";
            commandAddConf.Parameters.AddWithValue("$name", conference.name);
            commandAddConf.Parameters.AddWithValue("$theme", conference.theme);
            commandAddConf.Parameters.AddWithValue("$description", conference.description);
            commandAddConf.Parameters.AddWithValue("$time", conference.dateAndTime.ToString());
            commandAddConf.Parameters.AddWithValue("$place", conference.place);
            commandAddConf.Parameters.AddWithValue("$creator_id", conference.creatorId);
            commandAddConf.Parameters.AddWithValue("$state", conference.state.ToString());

            long id = (long)commandAddConf.ExecuteScalar();
            connection.Close();

            if(id < 1)
            {
                return (int)id;
            }

            connection.Open();
            foreach(User participant in conference.participants)
            {
                SqliteCommand commandAddRelation = connection.CreateCommand();
                commandAddRelation.CommandText =
                @"
                INSERT INTO conferences_users (conference_id, user_id)
                VALUES ($conference_id, $user_id);
                SELECT last_insert_rowid(); 
                ";
                commandAddRelation.Parameters.AddWithValue("$conference_id", id);
                commandAddRelation.Parameters.AddWithValue("$user_id", participant.id);
                id = (long)commandAddRelation.ExecuteScalar();                                                         // DELETE
            }
            
            connection.Close();
            return (int)id;
        }

        public List<Conference> GetAllConferencesOfTheUserWasInvitedTo(int userId)
        {
            List<int> confIds = new List<int>();
            List<Conference> conferences = new List<Conference>();

            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" SELECT * FROM conferences_users WHERE user_id = $user_id ";
            command.Parameters.AddWithValue("$user_id", userId);
            SqliteDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                int confId = int.Parse(reader.GetString(0));
                confIds.Add(confId);
            }            
            reader.Close();
            connection.Close();

            foreach(int confId in confIds)
            {
                conferences.Add(this.FindById(confId));
            }
            return conferences;
        }

        public List<Conference> GetAllConferencesOfTheUser(int creatorId)
        {
            List<Conference> conferences = new List<Conference>();

            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" SELECT * FROM conferences WHERE creator_id = $creator_id ";
            command.Parameters.AddWithValue("$creator_id", creatorId);
            SqliteDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                int id = int.Parse(reader.GetString(0));
                string name = reader.GetString(1);
                string theme = reader.GetString(2);
                string description = reader.GetString(3);
                DateTime dateAndTime = DateTime.Parse(reader.GetString(4));
                string place = reader.GetString(5);
                string state = reader.GetString(7);                                                                         // DELETE
                Conference conference = new Conference(id, name, theme, description, dateAndTime, place, creatorId, state);
                conferences.Add(conference);
            }
            reader.Close();
            connection.Close();

            for(int i = 0; i < conferences.Count; i++)
            {
                UserRepository userRep = new UserRepository(this.connection);
                conferences[i].participants = userRep.GetAllParticipantsOfTheConference(conferences[i].id);
            }

            return conferences;
        }

        public Conference FindById(int id)
        {
            Conference conference = null;

            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" SELECT * FROM conferences WHERE id = $id ";
            command.Parameters.AddWithValue("$id", id);
            SqliteDataReader reader = command.ExecuteReader();
            
            if(reader.Read())
            {
                string name = reader.GetString(1);
                string theme = reader.GetString(2);
                string description = reader.GetString(3);
                DateTime dateAndTime = DateTime.Parse(reader.GetString(4));
                string place = reader.GetString(5);
                int creatorId = int.Parse(reader.GetString(6));
                string state = reader.GetString(7);
                conference = new Conference(id, name, theme, description, dateAndTime, place, creatorId, state);
            }            
            reader.Close();
            connection.Close();

            if(conference != null)
            {
                UserRepository userRep = new UserRepository(this.connection);
                conference.participants = userRep.GetAllParticipantsOfTheConference(id);
            }
            
            return conference;
        }

        public bool Update(Conference conference) 
        {
            connection.Open();
            SqliteCommand command = connection.CreateCommand();
            command.CommandText =
            @" 
            UPDATE conferences
            SET name = $name, theme = $theme, description = $description, time = $time, place = $place, creator_id = $creator_id, state = $state
            WHERE id = $id; 
             ";
            command.Parameters.AddWithValue("$name", conference.name);
            command.Parameters.AddWithValue("$theme", conference.theme);
            command.Parameters.AddWithValue("$description", conference.description);
            command.Parameters.AddWithValue("$time", conference.dateAndTime.ToString());
            command.Parameters.AddWithValue("$place", conference.place);
            command.Parameters.AddWithValue("$creator_id", conference.creatorId);
            command.Parameters.AddWithValue("$state", conference.state.ToString());
            command.Parameters.AddWithValue("$id", conference.id);
            int nChanged = command.ExecuteNonQuery();
            connection.Close();

            if (nChanged == 0)
            {
                return false;
            }

            connection.Open();
            foreach(User participant in conference.participants)
            {
                SqliteCommand commandCount = connection.CreateCommand();
                commandCount.CommandText = 
                @"SELECT COUNT(*) FROM conferences_users WHERE conference_id = $conference_id AND user_id = $user_id ";
                commandCount.Parameters.AddWithValue("$conference_id", conference.id);
                commandCount.Parameters.AddWithValue("$user_id", participant.id);
                long count = (long)commandCount.ExecuteScalar();

                if(count == 0)
                {
                    SqliteCommand commandAddRelation = connection.CreateCommand();
                    commandAddRelation.CommandText =
                    @"
                    INSERT INTO conferences_users (conference_id, user_id)
                    VALUES ($conference_id, $user_id);

                    SELECT last_insert_rowid(); 
                    ";
                    commandAddRelation.Parameters.AddWithValue("$conference_id", conference.id);
                    commandAddRelation.Parameters.AddWithValue("$user_id", participant.id);
                    commandAddRelation.ExecuteScalar();
                }
            }
            
            connection.Close();
            return true;
        }
    }
}