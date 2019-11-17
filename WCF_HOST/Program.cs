using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP_WCF;
using System.ServiceModel;

namespace WCF_HOST
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(SOP_WCF.TodoService)))
                {
                    host.Open();
                    Console.WriteLine("A szerver elindult " + DateTime.Now.ToString() + " időpontban!");
                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ismeretlen hiba: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
