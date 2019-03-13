using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tracert
{
    class Program
    {
        private static Tracert traceroute;
        private static string lastHopIP;
        private static int hopsCount;
        private const int maxHopsCount = 20;

        static void Main(string[] args)
        {
            string IPOrDomain = Console.ReadLine();
            traceroute = new Tracert(IPOrDomain);

            hopsCount = 0;
            lastHopIP = "";

            for (int rowCount = 1; rowCount <= maxHopsCount; rowCount++)
            {
                traceroute.InitTTL();

                Console.Write($"{rowCount} ");
                for (int i = 1; i <= 3; i++)
                {
                    traceroute.InitToSend();
                    Console.Write($"{traceroute.sendAndReceive()}");
                }

                if (lastHopIP == traceroute.hopIP)
                    Console.WriteLine("Превышен интервал ожидания для запроса.");
                else
                {
                    Console.WriteLine($" {traceroute.hopIP}");
                    lastHopIP = traceroute.hopIP;
                }

                hopsCount++;
                if (traceroute.hopIP == traceroute.ip.ToString())
                {
                    Console.WriteLine("Трассировка завершена.");
                    Console.ReadKey();
                }
                else if (hopsCount >= maxHopsCount)
                {
                    Console.WriteLine("Достигнуто максимальное количество прыжков.");
                }
            }
        }
    }
}
