using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using masterdata.Models;
using masterdata.Interfaces;


namespace masterdata.Services
{
    public class BackgroundServiceCleanTask : BackgroundServiceClean
    {
        private readonly ILogger Logger;
        private readonly ICleanHrOU _CleanDAta;
        private readonly IFaManager _FaManager;
        private readonly IOuCCAssociation _CCAssociation;
        private readonly IAssociationData _AssociationData;
        private readonly IFAData _Fadata;


        public BackgroundServiceCleanTask(ILoggerFactory loggerFactory,
                                          ICleanHrOU cleandata,
                                          IFaManager famanager,
                                          IOuCCAssociation cCAssociation,
                                          IAssociationData associationData,
                                          IFAData fAData)
        {
            this.Logger = loggerFactory.CreateLogger<BackgroundServiceCleanTask>();
            this._CleanDAta = cleandata;
            this._FaManager = famanager;
            this._CCAssociation = cCAssociation;
            this._AssociationData = associationData;
            this._Fadata = fAData;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                DateTime start = DateTime.Now;
                Logger.LogDebug($"Cleaner has started --> {start}");

                try
                {
                    if (!string.IsNullOrEmpty(_FaManager.GetCurrFA().caller))
                    {
                        if (_FaManager.GetCurrFA().status.Equals(FaConstants.pending))
                        {

                            _FaManager.ToRunning();
                            List<CleanHrOU> HrOUList = _CleanDAta.RetrieveDataByFa(_FaManager.GetCurrFA().idFa);

                            if (HrOUList != null && HrOUList.Count > 0)
                            {
                                if (HrOUList.Count > 100)
                                {

                                    List<List<CleanHrOU>> splittedList = new List<List<CleanHrOU>>();

                                    int threadNumber = 3;
                                    int nSize = Convert.ToInt32(Math.Ceiling((decimal)HrOUList.Count / threadNumber));

                                    for (int i = 0; i < HrOUList.Count; i += nSize) // split main list into 3 with "even" numbers of ou
                                    {
                                        splittedList.Add(HrOUList.GetRange(i, Math.Min(nSize, HrOUList.Count - i)));
                                    }

                                    List<CleanBwCC> bwccList1 = new List<CleanBwCC>();
                                    List<CleanBwCC> bwccList2 = new List<CleanBwCC>();
                                    List<CleanBwCC> bwccList3 = new List<CleanBwCC>();

                                    ThreadClean tc1 = new ThreadClean(splittedList[0], _CCAssociation, Logger, _AssociationData, _FaManager);
                                    ThreadClean tc2 = new ThreadClean(splittedList[1], _CCAssociation, Logger, _AssociationData, _FaManager);
                                    ThreadClean tc3 = new ThreadClean(splittedList[2], _CCAssociation, Logger, _AssociationData, _FaManager);

                                    Console.WriteLine("Main thread: Threads started.");
                                    Thread t1 = new Thread(new ThreadStart(tc1.ThreadProc));
                                    Thread t2 = new Thread(new ThreadStart(tc2.ThreadProc));
                                    Thread t3 = new Thread(new ThreadStart(tc3.ThreadProc));

                                    t1.Start();
                                    Logger.LogDebug($"First thread has started: {DateTime.Now}");

                                    t2.Start();
                                    Logger.LogDebug($"Second thread has started: {DateTime.Now}");

                                    t3.Start();
                                    Logger.LogDebug($"Third thread has started: {DateTime.Now}");

                                    t1.Join();
                                    Logger.LogDebug($"First thread has ended: {DateTime.Now}");

                                    t2.Join();
                                    Logger.LogDebug($"Second thread has ended: {DateTime.Now}");

                                    t3.Join();
                                    Logger.LogDebug($"Third thread has ended: {DateTime.Now}");

                                    if (!_FaManager.GetCurrFA().status.Equals(FaConstants.Failed))
                                    {
                                        _FaManager.ToCompleted();
                                    }

                                    Logger.LogDebug($"Minutes taken to compute: {DateTime.Now.Subtract(start).Minutes}");

                                }
                                else
                                {

                                    List<CleanBwCC> bwccList = new List<CleanBwCC>();

                                    ThreadClean tc1 = new ThreadClean(HrOUList, _CCAssociation, Logger, _AssociationData, _FaManager);
                                    Thread t1 = new Thread(new ThreadStart(tc1.ThreadProc));

                                    t1.Start();
                                    Logger.LogDebug($"Single thread has started --> {DateTime.Now}");

                                    t1.Join();
                                    Logger.LogDebug($"Single thread has ended --> {DateTime.Now}");

                                    if (!_FaManager.GetCurrFA().status.Equals(FaConstants.Failed))
                                    {
                                        _FaManager.ToCompleted();
                                    }

                                    Logger.LogDebug($"Minutes taken to compute: {DateTime.Now.Subtract(start).Minutes}");

                                }
                            }
                            else //if hrou is null -> fail
                            {
                                _FaManager.ToInterrupted();
                            }
                        }
                    }
                    _Fadata.DeleteFaOnCascade();
                }
                catch (Exception ex)
                {
                    _FaManager.ToInterrupted();
                    Logger.LogError($"Error while processing cleaning's UO: {ex.Message}");
                }
                Logger.LogDebug($"Cleaner is on hold {DateTime.Now}");
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}