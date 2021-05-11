
namespace Calculating
{
    /// <summary>
    /// Creates instances of commands
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// Creates selected command instance
        /// </summary>
        /// <param name="operation">Operation type</param>
        /// <param name="mathUnit">Math unit</param>
        /// <returns>Command instance</returns>
        public static Command Create(string operation,MathUnit mathUnit)
        {
            Command command = null;
            switch (operation)
            {
                case "+":
                    command = new AdditionCommand(mathUnit);
                    break;
                case "-":
                    command = new SubtractCommand(mathUnit);
                    break;
                case "*":
                    command = new MultiplicationCommand(mathUnit);
                    break;
                case "/":
                    command = new DivisionCommand(mathUnit);
                    break;
                case "pow":
                    command = new PowCommand(mathUnit);
                    break;
                case "extract":
                    command = new ExtractCommand(mathUnit);
                    break;
                case "round":
                    command = new RoundCommand(mathUnit);
                    break;
                case "exp":
                    command = new ExpCommand(mathUnit);
                    break;
                case "log":
                    command = new LogCommand(mathUnit);
                    break;
            }
            return command;
        }
    }
}
