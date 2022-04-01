using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Exceptions {
    public class TransferException : Exception {
        public TransferException(string message) : base(message) {
        }

        public TransferException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
