using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace klient
{
    class Klient
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Wprowadz IP serwera: ");
                //String ip = "192.168.0.16";
                String ip = "127.0.0.1";
                //ip = Console.ReadLine();
                int port = 55123;
                String id;
                IPEndPoint ServerIP = new IPEndPoint(IPAddress.Parse(ip), port);
                UdpClient udpClient = new UdpClient();
                udpClient.Connect(ServerIP);

                Komunikat komunikat = new Komunikat();

                //porsba od id
                komunikat.SetOp("GetID");
                Byte[] sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                udpClient.Send(sendBytes, sendBytes.Length);

                //odebranie ack
                komunikat.Ustaw(udpClient.Receive(ref ServerIP));

                //odebranie id
                komunikat.Ustaw(udpClient.Receive(ref ServerIP));
                id = komunikat.GetId();

                Console.WriteLine(komunikat.GetId());
                //wyslanie ack
                komunikat.Clear();
                komunikat.SetOp("ACK");
                komunikat.SetId(id);
                sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                udpClient.Send(sendBytes, sendBytes.Length);
                


                //przeslanie liczby
                string temp;
                int liczba;
                do
                {
                    Console.WriteLine("Podaj liczbe wieksza od 0");
                    temp = Console.ReadLine();
                    liczba = Convert.ToInt32(temp);
                } while (liczba < 0);
                

                //wyslanie liczby
                komunikat.Clear();
                komunikat.SetOp("LiczbaDoPrzedzialu");
                komunikat.SetId(id);
                komunikat.SetLiczba(temp);
                sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                udpClient.Send(sendBytes, sendBytes.Length);
                //odebranie ack
                komunikat.Ustaw(udpClient.Receive(ref ServerIP));

                //odebranie liczby prob
                int l_prob;
                komunikat.Ustaw(udpClient.Receive(ref ServerIP));
                l_prob = Convert.ToInt32(komunikat.GetLiczba());

                Console.WriteLine("Liczba prob "+l_prob);
                //wyslanie ack
                komunikat.Clear();
                komunikat.SetOp("ACK");
                komunikat.SetId(id);
                sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                udpClient.Send(sendBytes, sendBytes.Length);


                Console.ReadKey();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
