using System;
using System.Diagnostics.CodeAnalysis;

using ArticulationManager.Utilities;

namespace ArticulationManager.Entities.Core.Value
{
    public class ArticulationColor : IEquatable<ArticulationColor>
    {
        public const int MinValue = 0;
        public const int MaxValue = 255;
        public int Value { get; }

        public ArticulationColor( int colorValue )
        {
            RangeValidateHelper.ValidateIntRange( colorValue, MinValue, MaxValue );
            Value = colorValue;
        }

        public bool Equals( [AllowNull] ArticulationColor other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value.ToString();
    }
}