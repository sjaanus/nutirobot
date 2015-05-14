using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakaProject.Exceptions {
    public class InformativeException : Exception {
        public InformativeException(string message) : base(message) { }
    }
}