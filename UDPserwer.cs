using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;



namespace serwer
{
    class UDPserwer
    {
        IPEndPoint Client1 = new IPEndPoint(IPAddress.Any, 0);
        IPEndPoint Client2 = new IPEndPoint(IPAddress.Any, 0);
        String Id1 = "pierwszy";
        String Id2 = "drugi";
        IPEndPoint reciveEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private int l1, l2;
        Komunikat komunikat = new Komunikat();


        public void firsRecive(ref UdpClient udpServer)
        {
            //1
            Byte[] receiveBytes = udpServer.Receive(ref Client1);
            komunikat.Ustaw(receiveBytes);

            //ACK
            komunikat.Clear();
            komunikat.SetOp("ACK");
            Byte[] sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
            udpServer.Send(sendBytes, sendBytes.Length, Client1);
            //Nadanie ID
            komunikat.Clear();
            komunikat.SetOp("ID");
            komunikat.SetId(Id1);
            sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
            udpServer.Send(sendBytes, sendBytes.Length, Client1);
            //Odebranie ACK
            receiveBytes = udpServer.Receive(ref Client1);
            komunikat.Ustaw(receiveBytes);

            //2
            receiveBytes = udpServer.Receive(ref Client2);
            komunikat.Ustaw(receiveBytes);

            //ACK
            komunikat.Clear();
            komunikat.SetOp("ACK");
            sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
            udpServer.Send(sendBytes, sendBytes.Length, Client2);
            //ID
            komunikat.Clear();
            komunikat.SetOp("ID");
            komunikat.SetId(Id2);
            sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
            udpServer.Send(sendBytes, sendBytes.Length, Client2);
            //Odebranie ACK
            receiveBytes = udpServer.Receive(ref Client2);
            komunikat.Ustaw(receiveBytes);

            //odebranie przdzialu
            for (int i = 0; i < 2; i++)
            {
                komunikat.Ustaw(udpServer.Receive(ref reciveEndPoint));
                if (reciveEndPoint.Address.ToString() == Client1.Address.ToString() && reciveEndPoint.Port == Client1.Port && komunikat.GetId() == Id1)
                {
                    l1 = Convert.ToInt32(komunikat.GetLiczba());
                    Console.WriteLine("L1 = " + l1);
                    komunikat.Clear();
                    komunikat.SetOp("ACK");
                    sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                    udpServer.Send(sendBytes, sendBytes.Length, Client1);
                }
                else if (reciveEndPoint.Address.ToString() == Client2.Address.ToString() && reciveEndPoint.Port == Client2.Port && komunikat.GetId() == Id2)
                {
                    l2 = Convert.ToInt32(komunikat.GetLiczba());
                    Console.WriteLine("L2 = " + l2);
                    komunikat.Clear();
                    komunikat.SetOp("ACK");
                    sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
                    udpServer.Send(sendBytes, sendBytes.Length, Client2);
                }
            }

            int liczba_prob = (l1 + l2) / 2;
            Console.WriteLine("Liczba prob "+liczba_prob);
            //Wyslanie przedzialu
            komunikat.Clear();
            komunikat.SetOp("PrzeslanieProb");
            komunikat.SetLiczba(liczba_prob.ToString());
            sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
            udpServer.Send(sendBytes, sendBytes.Length, Client1);
            //Odebranie ACK
            receiveBytes = udpServer.Receive(ref Client1);
            komunikat.Ustaw(receiveBytes);

            //Wyslanie przedzialu
            komunikat.Clear();
            komunikat.SetOp("PrzeslanieProb");
            komunikat.SetLiczba(liczba_prob.ToString());
            sendBytes = Encoding.ASCII.GetBytes(komunikat.GetMsg());
            udpServer.Send(sendBytes, sendBytes.Length, Client2);
            //Odebranie ACK
            receiveBytes = udpServer.Receive(ref Client2);
            komunikat.Ustaw(receiveBytes);
        }
        public void Recive(ref UdpClient udpServer)
        {
            komunikat.Ustaw(udpServer.Receive(ref reciveEndPoint));
            if (reciveEndPoint.Address.ToString() == Client1.Address.ToString() && reciveEndPoint.Port == Client1.Port && komunikat.GetId() == Id1)
            {

            }
            else if (reciveEndPoint.Address.ToString() == Client2.Address.ToString() && reciveEndPoint.Port == Client2.Port && komunikat.GetId() == Id2)
            {

            }
        }
    }
}
