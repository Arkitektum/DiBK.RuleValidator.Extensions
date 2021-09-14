﻿using System;

namespace DiBK.RuleValidator.Extensions
{
    public class GeometryFromGMLException : Exception
    {
        public GeometryFromGMLException()
        {
        }

        public GeometryFromGMLException(string message) : base(message)
        {
        }

        public GeometryFromGMLException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
