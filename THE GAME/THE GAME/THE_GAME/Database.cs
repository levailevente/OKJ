using System;
using System.Data.SQLite;

namespace THE_GAME
{
    static class Database
    {
        static SQLiteConnection dbConnection;
        static SQLiteCommand command;
        static SQLiteDataReader reader;
        static string sql = "";

        static Database()
        {
            ConnectToDatabase();
        }

        static void ConnectToDatabase()
        {
            dbConnection = new SQLiteConnection("Data Source=database.db3;Version=3;");
            dbConnection.Open();
        }


        public static string GetTiles(int id)
        {
            ConnectToDatabase();
            sql = "SELECT Tiles FROM Maps WHERE ID = " + id;
            command = new SQLiteCommand(sql, dbConnection);
            reader = command.ExecuteReader();
            reader.Read();
            string map = Convert.ToString(reader["Tiles"]);
            reader.Close();
            dbConnection.Close();
            return map;
        }

        public static string GetObjects(int id)
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

        public static void Save(int id, string name, string date, int mapid, string position, int hp)
        {
            ConnectToDatabase();
            sql =
                "UPDATE Saves SET Name = @name, DateTime = @date, MapID = @mapid, Position=@PositionString, HP=@hp WHERE ID = " +
                id;
            command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.AddWithValue("name", name);
            command.Parameters.AddWithValue("date", date);
            command.Parameters.AddWithValue("mapid", mapid);
            command.Parameters.AddWithValue("PositionString", position);
            command.Parameters.AddWithValue("hp", hp);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public static string[] Load(int id)
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
