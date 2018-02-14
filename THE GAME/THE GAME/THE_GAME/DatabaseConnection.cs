using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Data.SQLite;

namespace THE_GAME
{
    public class DatabaseConnection
    {
        SQLiteConnection connection;

        public DatabaseConnection()
        {
            this.connection=new SQLiteConnection("Data Source = database.db3; Version = 3;" );
        }
    }
}