using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Common;

namespace Domain.MoviePersonality
{
    public sealed class MoviePersonRole : Entity
    {
        public MoviePersonRole(Guid id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; } = null!;
    }
}
