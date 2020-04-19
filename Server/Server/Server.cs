using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Server
    {
        Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket handler;

        List<Player> players = new List<Player>();
        int numOfPlayers = 2;
        byte[] buffer = new byte[1024];
        EndPoint clientAdress;

        public Server()
        {
            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, 903));
            listenerSocket.Listen(1);
        }

        public void Start()
        {
            Thread myThread = new Thread(new ThreadStart(PlayCasino));
            try
            {
                while (true)
                {
                    Console.WriteLine("Ожидаю подключение главного сервера...");
                    handler = listenerSocket.Accept();
                    Console.WriteLine("Успешное подключение!");
                    clientAdress = handler.RemoteEndPoint;
                    //Получаем данные
                    Console.WriteLine("Жду ответ от сервера!");
                    handler.Receive(buffer);
                    string recieveData = Encoding.ASCII.GetString(buffer);
                    Console.Write("Полученный текст: " + recieveData + "\n\n");

                    //Обрабатываем полученные данные
                    string stringResponse;
                    if (players.Count != numOfPlayers)
                    {
                        Player newPlayer = ParseResponce(recieveData);
                        Console.WriteLine("Новый учасник: Токен: {0}, Ставка: {1}, Позиция: {2}", newPlayer.token, newPlayer.bet, newPlayer.place);
                        players.Add(newPlayer);
                        Console.WriteLine("Участник добавлен");
                        stringResponse = "200";
                    }
                    else
                    {
                        stringResponse = "300";
                        Console.WriteLine("Участник не добавлен, места закончились");
                    }

                    //Отправляем результат
                    byte[] response = Encoding.ASCII.GetBytes(stringResponse);
                    handler.Send(response);
                    Console.WriteLine("Ответ на добавление участника отправлен");

                    if (players.Count == numOfPlayers && myThread.IsAlive == false) {
                        myThread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private void Recieve()
        {

        }

        private void Send()
        {

        }

        private Player ParseResponce(string response)
        {
            string[] splitted = response.Split(' ');
            return new Player(splitted[2], splitted[3], int.Parse(splitted[4]));

        }

        private void PlayCasino()
        {
            Thread.Sleep(20000);
            Roulette roulette = new Roulette(players);
            sendGameResults(roulette.Play());
            
        }

        private void sendGameResults(List<string> results)
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            foreach (string result in results)
            {
                Socket recieverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                recieverSocket.Connect("0.tcp.ngrok.io", 12742);
                byte[] response = Encoding.ASCII.GetBytes(result);
                recieverSocket.Send(response);
                recieverSocket.Shutdown(SocketShutdown.Both);
                recieverSocket.Close();
                Console.WriteLine("Результат отправлен");
            }
            players.Clear();
        }
    }
}
