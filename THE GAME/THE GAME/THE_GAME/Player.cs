using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THE_GAME
{
    class Player
    {
        string name;
        int hp;
        public int Hp
        {
            get { return hp; }
            set
            {
                if (value < 1) hp = 3;
                hp = value;
            }
        }

        int xp, lvl;
        Save currentSave;

        public Player(string name, int hp, int xp, int lvl, Save currentSave)
        {
            this.name = name;
            this.hp = 3;
            this.xp = 0;
            this.lvl = 0;
            this.currentSave = currentSave; 
        }


    }
}
