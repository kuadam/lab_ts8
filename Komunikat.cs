using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serwer
{
    class Komunikat
    {

        private
            String op, id, odp, liczba, czas;

        public void Ustaw(Byte[] s)
        {
            op = id = odp = liczba = czas = "";
            String temp = Encoding.ASCII.GetString(s);
            int i = 0;


            while (temp[i] != '>')
                i++;
            i++;
            while (temp[i] != '<')
            {
                op += temp[i];
                i++;
            }

            while (temp[i] != '>')
                i++;
            i++;
            while (temp[i] != '<')
            {
                odp += temp[i];
                i++;
            }

            while (temp[i] != '>')
                i++;
            i++;
            while (temp[i] != '<')
            {
                id += temp[i];
                i++;
            }

            while (temp[i] != '>')
                i++;
            i++;
            while (temp[i] != '<')
            {
                liczba += temp[i];
                i++;
            }

            while (temp[i] != '>')
                i++;
            i++;
            while (temp[i] != '<')
            {
                czas += temp[i];
                i++;
            }
        }

        public String GetOp()
        {
            return op;
        }
        public void SetOp(String op)
        {
            this.op = op;
        }

        public String GetId()
        {
            return id;
        }

        public void SetId(String id)
        {
            this.id = id;
        }
        public String GetOdp()
        {
            return odp;
        }

        public void SetOdp(String odp)
        {
            this.odp = odp;
        }
        public String GetLiczba()
        {
            return liczba;
        }
        public void SetLiczba(String liczba)
        {
            this.liczba = liczba;
        }

        public void Set(String op, String odp, String id, String liczba)
        {
            SetOp(op);
            SetOdp(odp);
            SetId(id);
            SetLiczba(liczba);
        }

        public String GetMsg()
        {
            return "Operacja>" + op + "<Odpowiedz>" + odp + "<Identyfikator>" + id + "<Dane>" + liczba + "<Czas>" + DateTime.Now.ToString("T") + "<";
        }

        public void Clear()
        {
            op = id = odp = liczba = czas = "";
        }

    }
}
