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
 * LiczbaDoPrzedzialu
 * PrzeslanieProb
  
 
 * Odpowiedz  
 * 
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

                UDPserwer serwer = new UDPserwer();
                Komunikat komunikat = new Komunikat();

                serwer.firsRecive(ref udpServer);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            Console.ReadKey();
        }
    }
}
