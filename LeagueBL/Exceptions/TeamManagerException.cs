using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBL.Exceptions {
    public class TeamManagerException : Exception {
        public TeamManagerException(string message) : base(message) {
        }

        public TeamManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
