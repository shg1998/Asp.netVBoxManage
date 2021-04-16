using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HttpVBoxManager.Services
{
    public static class CmdLinker
    {
        private static string CMDHelper(string command)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = @"C:\Program Files\Oracle\VirtualBox",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                Arguments = command
            };

            var commandexec = new Process { StartInfo = startInfo };
            commandexec.StartInfo.RedirectStandardOutput = true;
            commandexec.Start();

            commandexec.WaitForExit();

            var result = commandexec.StandardOutput.ReadToEnd();
            return result.ToString();
        }
        public static Dictionary<string, string> AllDevsStatus()
        {
            Dictionary<string, string> ress = new Dictionary<string, string>();
            var result = CMDHelper("/c vboxmanage list vms");
            List<string> vmNames = new List<string>();
            List<string> vms = new List<string>();
            vms = result.Split("\n").ToList();
            foreach (var item in vms)
            {
                var a = item.Split('"').ToList();
                if (a.Count > 1)
                {
                    ress.Add(a[1], getVmStatus(a[1]));
                }

            }


            return ress;
        }

        public static string getVmStatus(string name)
        {
            var result = CMDHelper("/c vboxmanage showvminfo " + name + " | findstr \"running (since\"");

            return result;
        }

        public static string startVM(string name)
        {
            var result = CMDHelper("/c vboxmanage startvm " + name);

            return result;
        }

        public static string PowerOffVM(string name)
        {
            var result = CMDHelper("/c vboxmanage controlvm " + name + " poweroff ");

            return result;
        }

        public static string ModifyVM(string name, int cpu, int memmory)
        {
            string a = "/c vboxmanage modifyvm " + name + " --cpus " + cpu + " --memory " + memmory;
            var result = CMDHelper(a);

            return result;
        }


        public static string CloneVM(string SourceName, string DestName)
        {
            string a = "/c vboxmanage clonevm " + SourceName + " --name=" + DestName + " --register";
            var result = CMDHelper(a);

            return result;
        }

        public static string DeleteVM(string SourceName)
        {
            string a = "/c vboxmanage unregistervm --delete " + SourceName;
            var result = CMDHelper(a);

            return result;
        }

        public static string ExecuteCommandVM(string SourceName, string userName, string password, string command)
        {
            string a = "/c vboxmanage guestcontrol " + SourceName + " run --username " + userName + " --password " + password + " " + command;
            var result = CMDHelper(a);

            return result;
        }

        public static string sendfileFromHostToVM(string vm, string hostPath, string vmPath, string userName, string password)
        {
            string a = "/c vboxmanage guestcontrol " + vm + " copyfrom " + hostPath + " --username " + userName + " --password " + password + " --" +
                "target-directory " + vmPath;
            var result = CMDHelper(a);
            return result;
        }

        public static string sendfileFromVmToVM(string vm, string svmPath, string dvmPath, string userName, string password, string command)
        {
            string a = "/c vboxmanage guestcontrol " + vm + " copyto --target-directory" + dvmPath + " --username " + userName + " --password " + password + " " + svmPath;
            var result = CMDHelper(a);
            return result;
        }


    }
}
