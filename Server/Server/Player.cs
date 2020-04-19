using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    struct Player
    {
        public string token;
        public int bet;
        public string place;
        public int win;

        public Player(string token, string place, int bet)
        {
            this.token = token;
            this.place = place;
            this.bet = bet;
            win = 0;
        }

        public void SetWin()
        {
            win = bet * 35;
        }
    }
}
