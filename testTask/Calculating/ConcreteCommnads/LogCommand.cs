
namespace Calculating
{
    /// <summary>
    /// Represents natural logarithm command
    /// </summary>
    public class LogCommand : Command
    {

        /// <summary>
        /// Creates instance of the LogCommand class
        /// </summary>
        /// <param name="mathUnit">Math unit</param>
        public LogCommand(MathUnit mathUnit)
        {
            this.MathUnit = mathUnit;
        }

        /// <summary>
        /// Command type
        /// </summary>
        public override CommandTypes CommandType => CommandTypes.Unary;

        /// <summary>
        /// Executes command
        /// </summary>
        public override void Execute()
        {
            initialValue = MathUnit.Result;
            MathUnit.RunUnary("log");
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
        public override string ToString()
        {
            return "log";
        }
    }
}
