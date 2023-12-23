using Microsoft.WindowsAPICodePack.Net;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
            Console.WriteLine("Network MAC Address:");
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
            Console.WriteLine("Network New MAC Address:");
            Console.WriteLine(str.Replace("-", ""));
            var networks = NetworkListManager.GetNetworks(NetworkConnectivityLevels.Connected); foreach (Network network in networks)
            {
                ChangeMacAddress(network.Name, str);
                break;
            }
            Console.WriteLine("done");
            Console.ReadLine();
        }
        public static void ChangeMacAddress(string networkname, string macaddress)
        {
            string readtext = File.ReadAllText("setup.ps1");
            string readtextreplaced = readtext.Replace("networkname", networkname).Replace("macaddress", macaddress);
            File.WriteAllText("powershell.exe", readtextreplaced);
            ProcessStartInfo startInfo = new ProcessStartInfo("setup.ps1");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Verb = "runas";
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
            File.WriteAllText("setup.ps1", readtext);
        }
    }
}