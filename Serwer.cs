using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


/*
 * Operacje
 * GetID - prosba klienta o nadanie id
 * ACK - potwierdzenie
 * ID - nadanie id
 * LiczbaDoPrzedzialu - klient przesyla liczbe do przedzialu
 * PrzeslanieProb - serwer przesyla liczbe prob
 * Zgadywana - klient przesyla zgadywana liczbe
 * OdpSerwera - odpowiedz serwera (mniejsza wieksza itp)
  
 
 * Odpowiedz  
 * Tak - klient zgadl
 * Mniejsza - liczba zgadywana jest mniejsza od podanej
 * Wieksza - liczba zgadywana jest wieksza od podanej
 *      
     */

namespace serwer
{
    class Serwer
    {
        static void Main(string[] args)
        {

            try
            {
                int port = 55123;
                UdpClient udpServer = new UdpClient(55123);

                string hostName = Dns.GetHostName(); // pobieranie nazwy 
                Console.WriteLine("Nazwa serwera: " + hostName);

                // uzyskanie adresu IP 
                string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
                Console.WriteLine("Adres IP:" + ip);

                UDPserwer serwer = new UDPserwer();
                Komunikat komunikat = new Komunikat();

                serwer.Start(ref udpServer);
                while (true)
                {
                    serwer.Recive(ref udpServer);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            Console.ReadKey();
        }
    }
}
