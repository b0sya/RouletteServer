using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Roulette
    {

        List<Player> players;
        string[] points = {"1r", "2b", "3r", "4b", "5r", 
                            "6b", "7r", "8b", "9r", "10b", 
                            "11b", "12r", "13b", "14r", "15b", 
                            "16r", "17b", "18r", "19r", "20b", 
                            "21r", "22b", "23r", "24b", "25r", 
                            "26b", "27r", "28b", "29b", "30r", 
                            "31b", "32r", "33b", "34r", "35b", 
                            "36r"};
        List<string> stringResults = new List<string>();

        public Roulette(List<Player> players)
        {
            this.players = players;
        }

        public List<string> Play()
        {
            Console.WriteLine("Игра началась!");
            Random rnd = new Random();
            int value = rnd.Next(0, points.Length - 1);
            string selectedPoint = points[value];
            Console.WriteLine("Выпало: {0}", selectedPoint);

            for(int index = 0; index < players.Count; index++)
            {
                if (players[index].place == selectedPoint)
                {
                    players[index].SetWin();
                }

                stringResults.Add("gameResults" + " " + players[index].token + " " + players[index].win);
            }

            Console.WriteLine("Игра закончена");
            return stringResults;
        }

    }
}
