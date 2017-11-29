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
                Komunikat komunikat2 = new Komunikat();

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



                //pobranie liczby
                int liczba;
                bool dobra;
                bool czy_liczba;
                do
                {
                    dobra = true;
                    Console.WriteLine("Podaj liczbe parzysta naturalna");
                    czy_liczba = int.TryParse(Console.ReadLine(), out liczba);
                    if (liczba < 1 || liczba % 2 != 0 || !czy_liczba)
                    {
                        dobra = false;
                    }
                } while (!dobra);

                //wyslanie liczby
                komunikat.Clear();
                komunikat.SetOp("LiczbaDoPrzedzialu");
                komunikat.SetId(id);
                komunikat.SetLiczba(liczba.ToString());
                sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                udpClient.Send(sendBytes, sendBytes.Length);
                //odebranie ack
                komunikat.Ustaw(udpClient.Receive(ref ServerIP));

                //odebranie liczby prob
                int l_prob;
                komunikat.Ustaw(udpClient.Receive(ref ServerIP));
                l_prob = Convert.ToInt32(komunikat.GetLiczba());

                Console.WriteLine("Liczba prob " + l_prob);
                //wyslanie ack
                komunikat.Clear();
                komunikat.SetOp("ACK");
                komunikat.SetId(id);
                sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                udpClient.Send(sendBytes, sendBytes.Length);

                while (true)
                {

                    if (l_prob <= 0)
                    {
                        Console.WriteLine("Przekroczyles liczbe prob");
                        break;
                    }

                    do
                    {
                        dobra = true;
                        Console.WriteLine("Podaj liczbe");
                        czy_liczba = int.TryParse(Console.ReadLine(), out liczba);
                        if (!czy_liczba)
                        {
                            dobra = false;
                        }
                    } while (!dobra);
                    
                    //przeslanie liczby
                    komunikat.Clear();
                    komunikat.SetOp("Zgadywana");
                    komunikat.SetId(id);
                    komunikat.SetLiczba(liczba.ToString());
                    sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                    udpClient.Send(sendBytes, sendBytes.Length);
                    //odebranie ack
                    komunikat.Ustaw(udpClient.Receive(ref ServerIP));

                    //odebranie odpowiedzi o zgadwyanej liczbie
                    komunikat.Ustaw(udpClient.Receive(ref ServerIP));

                    //wyslanie ack
                    komunikat2.Clear();
                    komunikat2.SetOp("ACK");
                    komunikat2.SetId(id);
                    sendBytes = Encoding.ASCII.GetBytes(komunikat2.GetMsg());
                    udpClient.Send(sendBytes, sendBytes.Length);

                    if (komunikat.GetOdp() == "Tak")
                    {
                        Console.WriteLine("Zgadles");
                        break;
                    }
                    else if (komunikat.GetOdp() == "Mniejsza")
                    {
                        Console.WriteLine("Zgadywana liczba jest mniejsza");
                    }
                    else if (komunikat.GetOdp() == "Wieksza")
                    {
                        Console.WriteLine("Zgadywana liczba jest wieksza");
                    }
                    l_prob--;
                    Console.WriteLine("Liczba prob: " + l_prob);
                }


                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
