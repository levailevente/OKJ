using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THE_GAME.menu
{
    class Save
    {
        public static readonly SaveSlot[] save;

        public Save()
        {
            for (int i = 0; i < 4; i++)
            {
                save[i] == new SaveSlot(gomb);
            }
        }
    }
}
