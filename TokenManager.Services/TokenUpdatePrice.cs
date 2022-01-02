using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TokenManager.Core.Logger;
using TokenManager.Core.Utils;

namespace TokenManager.Services
{
    public partial class TokenUpdatePrice : ServiceBase
    {
        public Timer timer;
        public int timeRequest = 300000; // 5 mins
        bool isBusy = false;
        public TokenUpdatePrice()
        {
            InitializeComponent();
            timer = new Timer();
            timeRequest = Const.UpdateTimePrice;
            timer.Interval = timeRequest;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        protected override void OnStart(string[] args)
        {
            Logger.WriteLog(Logger.LogType.Info, "#================Start running service==============#");
            timer.Start();
        }

        protected override void OnStop()
        {
            Logger.WriteLog(Logger.LogType.Info, "#================Service stopped==============#");
            timer.Stop();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Process();
        }

        void Process()
        {
            try
            {
                if (isBusy) return;
                isBusy = true;
                Process process = new Process();
                process.Run();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, "TokenUpdatePrice Process error:" + ex.Message);
            }
            finally
            {
                isBusy = false;
            }
        }
    }
}
