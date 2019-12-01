﻿using Arium.Interfaces;
using System;

namespace Arium.Exceptions
{
    public class PathNotFoundException : Exception
    {
        public PathNotFoundException(INavigable origin, INavigable destination)
            : base($"No path was found between \"{origin.GetType().FullName}\" and \"{destination.GetType().FullName}\".")
        {
        }
    }
}