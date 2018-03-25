using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using  System.IO;

namespace THE_GAME
{
    public class Database
    {
        SQLiteConnection dbConnection;
        SQLiteCommand command;
        SQLiteDataReader reader;
        string sql = "";

        public Database()
        {
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            dbConnection = new SQLiteConnection("Data Source=database.db3;Version=3;");
            dbConnection.Open();
        }


        public string GetTiles(int id)
        {
            ConnectToDatabase();
            sql = "SELECT Tiles FROM Maps WHERE ID = " + id;
            command = new SQLiteCommand(sql, dbConnection);
            reader = command.ExecuteReader();
            reader.Read();
            string map= Convert.ToString(reader["Tiles"]);
            reader.Close();
            dbConnection.Close();
            return map;
        }

        public string GetObjects(int id)
        {
            ConnectToDatabase();
            sql = "SELECT Objects from Maps WHERE ID = " + id;
            command = new SQLiteCommand(sql, dbConnection);
            reader = command.ExecuteReader();
            reader.Read();
            string map = Convert.ToString(reader["Objects"]);
            reader.Close();
            dbConnection.Close();
            return map;
        }

        public void Save(int id, string name, string date, int mapid, string position,int hp)
        {
            ConnectToDatabase();
            sql = "UPDATE Saves SET Name = @name, DateTime = @date, MapID = @mapid, Position=@position, HP=@hp WHERE ID = "+id;
            command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("date", date);
            command.Parameters.AddWithValue("mapid", mapid);
            command.Parameters.AddWithValue("position", position);
            command.Parameters.AddWithValue("hp", hp);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public string[] Load(int id)
        {
            ConnectToDatabase();
            string[] result = new string[5];
            sql = "SELECT Name,DateTime, MapID, Position, HP FROM Saves WHERE ID = " + id;
            command = new SQLiteCommand(sql, dbConnection);
            reader = command.ExecuteReader();
            reader.Read();
            result[0] = Convert.ToString(reader["Name"]);
            result[1] = Convert.ToString(reader["DateTime"]);
            result[2] = Convert.ToString(reader["MapID"]);
            result[3] = Convert.ToString(reader["Position"]);
            result[4] = Convert.ToString(reader["HP"]);
            reader.Close();
            dbConnection.Close();
            return result;
        }
    }
}
