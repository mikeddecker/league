using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueDL.Exceptions {
    public class TeamRepoADOException : Exception {
        public TeamRepoADOException(string message) : base(message) {
        }

        public TeamRepoADOException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
