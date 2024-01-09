using SocketTool.Config;
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
            Log.Init();
            Log.Info("Application Started****************************");

            string ScenarioDef_name = "ScenarioDef.txt";

            try
            {
                if (args.Length > 0)
                {
                    JsonCommDef.GetInstance().SetInit_machine_no(args[0]);
                }
                if (args.Length > 1)
                {
                    JsonCommDef.GetInstance().SetInit_segment_no(args[1]);
                }
                if (args.Length > 2)
                {
                    ScenarioDef_name = args[2];
                }


                string wcd = System.AppDomain.CurrentDomain.BaseDirectory;
                JsonCommDef.GetInstance().ReadJson(wcd + "config\\CommDef.json");
                JsonDataDef.GetInstance().ReadJson(wcd + "config\\CommDataDef.json");
                ScenarioDef.GetInstance().ReadJson(wcd + $"config\\{ScenarioDef_name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error(ex.Message, ex);
                return;
            }


            Application.Run(new FormMain());
        }
    }
}
