using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZYSoft.Comm
{
    public class ReturnValue
    {
        private bool _errorFlag;
        private string _errorID;
        private DataSet _resultDataSet;
        private int _Count;

        public ReturnValue()
        {
            _errorFlag = false;
            _errorID = string.Empty;
            _resultDataSet = null;
            _Count = 0;
        }

        public ReturnValue(bool ErrorFlag)
        {
            this._errorFlag = ErrorFlag;
        }

        public ReturnValue(bool ErrorFlag, string ErrorID)
        {
            this._errorFlag = ErrorFlag;
            this._errorID = ErrorID;
        }

        public ReturnValue(bool ErrorFlag, string ErrorID, DataSet ResultDataSet)
        {
            this._errorFlag = ErrorFlag;
            this._errorID = ErrorID;
            this._resultDataSet = ResultDataSet;
        }

        public ReturnValue(bool ErrorFlag, string ErrorID, DataSet ResultDataSet, int Count)
        {
            this._errorFlag = ErrorFlag;
            this._errorID = ErrorID;
            this._resultDataSet = ResultDataSet;
            this._Count = Count;
        }

        /// <summary>
        /// ErrorFlag
        /// </summary>
        public bool ErrorFlag
        {
            get { return _errorFlag; }
            set { _errorFlag = value; }
        }

        /// <summary>
        /// ErrorID
        /// </summary>
        public string ErrorID
        {
            get { return _errorID; }
            set { _errorID = value; }
        }

        /// <summary>
        /// ResultDataSet
        /// </summary>
        public DataSet ResultDataSet
        {
            get { return _resultDataSet; }
            set { _resultDataSet = value; }
        }

        /// <summary>
        /// Count
        /// </summary>
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
    }
}
