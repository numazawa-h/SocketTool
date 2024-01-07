﻿using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTool
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (args.Length > 0)
            {
                JsonCommDef.GetInstance().SetInit_machine_no(args[0]);
                if (args.Length > 1)
                {
                    JsonCommDef.GetInstance().SetInit_segment_no(args[1]);
                }
            }
            string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                JsonCommDef.GetInstance().ReadJson(wcd + "config\\CommDef.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CommDef.jsonの読み込み失敗({ex.Message})");
                return;
            }
            try
            {
                JsonDataDef.GetInstance().ReadJson(wcd + "config\\CommDataDef.json");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CommDataDef.jsonの読み込み失敗({ex.Message})");
                return;
            }


            Application.Run(new FormMain());
        }
    }
}
