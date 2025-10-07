using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todo.Common
{
    public class Result
    {
        private bool ok;
        public string Error { get; private set; }
        
        private Result() 
        {
            ok = true;
            Error = string.Empty;
        }

        private Result(string error)
        {
            ok = false;
            Error = error;
        }

        public static TR Evaluate<TR>(Func<Result> f, Func<string, TR> onErr, Func<TR> onOk)
        {
            var r = f();
            if (r.IsErr())
            {
                return onErr(r.Error);
            }
            return onOk();
        }

        public bool IsErr()
        {
            if (this.ok)
            {
                return false;
            }
            return true;
        }

        public bool IsOk()
        {
            if (!this.ok)
            {
                return false;
            }
            return true;
        }

        public string GetErr()
        {
            return this.Error;
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Err(string error)
        {
            return new Result(error);
        }
    }

    public class Result<T> where T : class
    {
        private bool ok;

        public string Error { get; private set; }

        public T? Value { get; private set; }

        public bool IsErr()
        {
            if (this.ok)
            {
                return false;
            }
            return true;
        }

        public bool IsOk()
        {
            if (!this.ok)
            {
                return false;
            }
            return true;
        }

        public string GetErr()
        {
            return this.Error;
        }

        public T? GetVal()
        {
            return this.Value;
        }

        private Result(T val)
        {
            this.Value = val;
            this.ok = true;
            this.Error = string.Empty;
        }

        private Result(string error)
        {
            this.Value = null;
            this.ok = false;
            this.Error = error;
        }

        public static Result<T> Ok(T val)
        {
            return new Result<T>(val);
        }

        public static Result<T> Err(string error)
        {
            return new Result<T>(error);
        }
    }
}
