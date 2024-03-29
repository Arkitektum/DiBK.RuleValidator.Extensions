﻿using System.Linq;

namespace DiBK.RuleValidator.Extensions
{
    public class RuleHelper
    {
        public static bool AllEqual(params object[] values)
        {
            if (values.Length == 1)
                return true;

            var first = values[0];
            var others = values.Skip(1);

            return others.All(otherValue => otherValue != null ? otherValue.Equals(first) : otherValue == first);
        }
    }
}
