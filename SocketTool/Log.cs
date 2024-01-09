using SocketTool.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTool
{
    internal class Log
    {        
        // シングルトン
        static private Log _instance = null;
        static public Log GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Log();
            }
            return _instance;
        }
        private Log()
        {
        }

        static private log4net.ILog _logger = null;
        static public void  Init()
        {
            _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            _logger.Info("_logger logging started");
        }


        static public void Info(string message)
        {
            Log._logger.Info(message);
        }
        static public void Debug(string message)
        {
            Log._logger.Debug(message);
        }
        static public void Warn(string message)
        {
            Log._logger.Warn(message);
        }
        static public void Error(string message, Exception ex = null)
        {
            if (ex == null) {
                Log._logger.Error(message);
            }
            else
            {
                Log._logger.Error(message, ex);
            }
        }
    }
}
