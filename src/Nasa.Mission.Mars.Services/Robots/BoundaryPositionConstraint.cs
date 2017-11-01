using Nasa.Mission.Mars.Entity;
using Nasa.Mission.Mars.Entity.ModelConstraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Services.Robots
{
    public class BoundaryPositionConstraint : Constraint
    {
        public BoundaryPositionConstraint(Position boundaryPosition)
            : base(GetValidationFunc(boundaryPosition)) { }

        private static Action<object> GetValidationFunc(Position boundary) =>
            _ => {
                var position = (Position)_;
                if (position.X > boundary.X || position.X < 0)
                    throw new ConstraintException("Position cannot exceed X-axis boundary.");

                if (position.Y > boundary.Y || position.Y < 0)
                    throw new ConstraintException("Position cannot exceed Y-axis boundary.");
            };
    }
}
