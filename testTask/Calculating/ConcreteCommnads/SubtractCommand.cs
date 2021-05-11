﻿
namespace Calculating
{
    /// <summary>
    /// Represents subtract command
    /// </summary>
    public class SubtractCommand : Command
    {
        /// <summary>
        /// Creates instance of the SubtractCommand class
        /// </summary>
        /// <param name="mathUnit">Math unit</param>
        public SubtractCommand(MathUnit mathUnit)
        {
            this.MathUnit = mathUnit;
        }

        /// <summary>
        /// Command type
        /// </summary>
        public override CommandTypes CommandType => CommandTypes.Binary;

        /// <summary>
        /// Executes command
        /// </summary>
        public override void Execute()
        {
            initialValue = MathUnit.Result;
            MathUnit.RunBinary("-", ParameterValue);
        }

        /// <summary>
        /// Cancels command
        /// </summary>
        public override void Cancel()
        {
            MathUnit.Result = initialValue;
        }

        /// <summary>
        /// Gets copy of the command
        /// </summary>
        /// <returns>Copry of the command</returns>
        public override object Clone()
        {
            var command = (Command)this.MemberwiseClone();
            command.MathUnit = this.MathUnit;
            return command;
        }

        /// <summary>
        /// Gets string representation of the command
        /// </summary>
        /// <returns>String representation of the command</returns>
        public override string ToString()
        {
            return "-";
        }
    }
}
