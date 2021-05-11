using System;

namespace Calculating
{
    /// <summary>
    /// Commands types
    /// </summary>
    public enum CommandTypes
    {
        Unary,
        Binary
    }

    /// <summary>
    /// Represents command
    /// </summary>
    public abstract class Command : ICloneable
    {
        /// <summary>
        /// Math unit
        /// </summary>
        public MathUnit MathUnit;

        /// <summary>
        /// Value before before command execution
        /// </summary>
        protected double initialValue;

        /// <summary>
        /// Command type
        /// </summary>
        public abstract CommandTypes CommandType { get; }

        /// <summary>
        /// Command parameter
        /// </summary>
        public double ParameterValue;

        /// <summary>
        /// Executes command
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Cancels command
        /// </summary>
        public abstract void Cancel();

        /// <summary>
        /// Gets copy of the command
        /// </summary>
        /// <returns>Copry of the command</returns>
        public abstract object Clone();
    }
}
