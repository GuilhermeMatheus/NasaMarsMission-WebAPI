using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Nasa.Mission.Mars.Entity.ModelConstraints
{
    public class Constraint
    {
        public Action<object> ValidatorFunc { get; private set; }

        /// <summary>
        /// Represents a model Constraint
        /// </summary>
        /// <param name="validatorAction"> Represents an actions that receives model future value and must throws if in invalid state </param>
        public Constraint(Action<object> validatorAction) =>
            ValidatorFunc = validatorAction ?? throw new ArgumentNullException(nameof(validatorAction));

        public void ThrowIfInvalidState(object value) =>
            ValidatorFunc(value);
    }
}
