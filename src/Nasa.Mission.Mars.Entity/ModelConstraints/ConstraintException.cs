using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Entity.ModelConstraints
{
    public class ConstraintException : Exception
    {
        public ConstraintException(string message) 
            : base(message) { }

        public ConstraintException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
