using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeMAC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String firstMacAddress = System.Net.NetworkInformation.NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up && nic.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
            Console.WriteLine("Network MAC Address :");
            Console.WriteLine(firstMacAddress);
            string str = firstMacAddress.Substring(0, 2);
            string[] stritems = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            Random rand = new Random();
            int index = rand.Next(stritems.Length);
            str = str + "-" + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + "-" + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + "-" + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + "-" + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + "-" + stritems[index];
            index = rand.Next(stritems.Length);
            str = str + stritems[index];
            index = rand.Next(stritems.Length);
            Console.WriteLine("Network New MAC Address :");
            Console.WriteLine(str);
            ChangeMacAddress(str);
            Console.WriteLine("done");
        }
        public static void ChangeMacAddress(string macadress)
        {
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardOutput = true,
                Arguments = $"Set-NetAdapter -InterfaceDescription 'Realtek*' -MacAddress '{macadress}'",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            var process = Process.Start(start);
            process.WaitForExit();
        }
    }
}