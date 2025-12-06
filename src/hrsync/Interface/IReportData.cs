using System;

using hrsync.Models;

namespace hrsync.Interface
{
    public interface IReportData
    {
        void AddNewReport(Report r);
        Report GetLastReport();
        Report GetLastUtilReport();
    }
}