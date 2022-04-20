using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Exceptions {
    public class TransferManagerException : Exception {
        public TransferManagerException(string message) : base(message) {
        }

        public TransferManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
