using System;
using Microsoft.Extensions.Logging;
using masterdata.Interfaces;
using masterdata.Models;

namespace masterdata.Helpers
{
    public class FaManager : IFaManager
    {
        private readonly ILogger _logger;
        private static FunctionalAck _currFunctionalAck;
        private readonly IFAData _FaData;

        public FaManager(ILoggerFactory loggerFactory, IFAData faData)
        {
            _logger = loggerFactory.CreateLogger<FaManager>();
            _currFunctionalAck = new FunctionalAck();
            this._FaData = faData;

        }

        public bool CanStart(FunctionalAck fa)
        {

            bool canstart = false;
            if (String.IsNullOrEmpty(_currFunctionalAck.caller))
            {
                SetCurrFa(fa);
                canstart = true;
                _logger.LogInformation(string.Format("Request by {0} for cleaning was accepted", _currFunctionalAck.caller));
            }
            else if (_currFunctionalAck.status.Equals(FaConstants.running)
                    || _currFunctionalAck.status.Equals(FaConstants.pending)
                    || _currFunctionalAck.status.Equals(FaConstants.uploading))
            {
                canstart = false;
                _logger.LogInformation(string.Format("Request by {0} for a new cleaning was denied", _currFunctionalAck.caller));
            }
            else if (_currFunctionalAck.status == FaConstants.ended || _currFunctionalAck.status == FaConstants.Failed)
            {
                SetCurrFa(fa);
                canstart = true;
                _logger.LogInformation(string.Format("Request by {0} for cleaning was accepted", _currFunctionalAck.caller));
            }

            return canstart;
        }
        public void ToRunning()
        {
            _FaData.UpdateFaDbStatus(_currFunctionalAck, "Running");
        }
        public void ToInterrupted()
        {
            _FaData.UpdateFaDbStatus(_currFunctionalAck, "Failed");
        }
        public void ToCompleted()
        {
            _FaData.UpdateFaDbStatus(_currFunctionalAck, "Ended");
        }

        public FunctionalAck GetCurrFA()
        {
            return _currFunctionalAck;
        }

        public void SetCurrFa(FunctionalAck fa)
        {
            _currFunctionalAck = fa;
        }
    }
}