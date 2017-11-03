using Nasa.Mission.Mars.Entity.ModelConstraints;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using Nasa.Mission.Mars.Entity.Utils;

namespace Nasa.Mission.Mars.Entity.ModelConstraints
{
    public class ConstraintValidator<TModel>
    {
        private readonly Dictionary<string, List<Constraint>> _constraintByProperty;

        public ConstraintValidator() =>
            _constraintByProperty = new Dictionary<string, List<Constraint>>();

        public void AddConstraint(Expression<Func<TModel, object>> accessor, Constraint constraint)
        {
            ValidateConstraintArguments(accessor, constraint);
            var propertyName = accessor.GetPropertyName().Name;

            AddConstraintOfProperty(propertyName, constraint);
        }
        
        public void AddUniqueConstraint(Expression<Func<TModel, object>> accessor, Constraint constraint)
        {
            ValidateConstraintArguments(accessor, constraint);
            var propertyName = accessor.GetPropertyName().Name;
            var cType = constraint.GetType();
            var propertyAlreadyContainsThisTypeOfConstraint =
                GetConstraintsOfProperty(propertyName).Any(_ => _.GetType().Equals(cType));

            if (propertyAlreadyContainsThisTypeOfConstraint)
                throw new ConstraintException($"Constraint of Type {cType} for {typeof(TModel)}::{propertyName} must be unique.");

            AddConstraintOfProperty(propertyName, constraint);
        }

        private static void ValidateConstraintArguments(Expression<Func<TModel, object>> accessor, Constraint constraint)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));

            if (constraint == null)
                throw new ArgumentNullException(nameof(constraint));
        }

        public IEnumerable<Constraint> GetConstraintsOfProperty([CallerMemberName] string propertyName = null) =>
            _constraintByProperty.TryGetValue(propertyName, out List<Constraint> list)
                ? list.AsEnumerable()
                : Enumerable.Empty<Constraint>();

        public void ThrowIfInvalidState(object value, [CallerMemberName] string propertyName = null)
        {
            var cts = GetConstraintsOfProperty(propertyName);
            foreach (var item in cts)
                item.ValidatorFunc(value);
        }

        private void AddConstraintOfProperty(string propertyName, Constraint constraint)
        {
            if (_constraintByProperty.TryGetValue(propertyName, out List<Constraint> list))
                list.Add(constraint);
            else
                _constraintByProperty.Add(
                    propertyName,
                    new List<Constraint> { constraint });
        }
    }
}
