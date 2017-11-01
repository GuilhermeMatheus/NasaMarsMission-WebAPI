using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Entity.ModelConstraints
{
    public interface IConstrainable<TModel>
    {
        ConstraintValidator<TModel> ConstraintValidator { get; }
    }
}
