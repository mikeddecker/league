using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueDL.Exceptions {
    public class SpelerRepoADOException : Exception {
        public SpelerRepoADOException(string message) : base(message) {
        }

        public SpelerRepoADOException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
