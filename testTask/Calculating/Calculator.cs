using System.Collections.Generic;

namespace Calculating
{
    /// <summary>
    /// Represents calculator
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// History of cammands executions
        /// </summary>
        public Stack<Command> History = new Stack<Command>();

        /// <summary>
        /// Available in calculator commands
        /// </summary>
        protected Dictionary<string, Command> commands = new Dictionary<string, Command>();

        /// <summary>
        /// Math unit
        /// </summary>
        public MathUnit MathUnit;

        /// <summary>
        /// Creates the instance of Calculator class
        /// </summary>
        /// <param name="mathUnit">Math unit</param>
        public Calculator(MathUnit mathUnit)
        {
            this.MathUnit = mathUnit;
            commands.Add("+", new AdditionCommand(mathUnit));
            commands.Add("-", new SubtractCommand(mathUnit));
            commands.Add("*", new MultiplicationCommand(mathUnit));
            commands.Add("/", new DivisionCommand(mathUnit));
        }

        /// <summary>
        /// Runs command
        /// </summary>
        /// <param name="operation">Operation type</param>
        /// <param name="value">Command parameter</param>
        /// <returns>Reuslt of command execution</returns>
        public double Run(string operation,double value = 0.0)
        {
            var command = GetCommand(operation);
            if (command!= null)
            {
                command.ParameterValue = value;
                command.MathUnit = MathUnit;
                command.Execute();
                History.Push(command);
            }
            return MathUnit.Result;
        }

        /// <summary>
        /// Resets history and result
        /// </summary>
        /// <returns>Reseted result</returns>
        public double Reset()
        {
            History.Clear();
            MathUnit.Result = 0;
            return MathUnit.Result;
        }

        /// <summary>
        /// Cancels last command
        /// </summary>
        /// <returns>Result after cancellation</returns>
        public double Undo()
        {
            if (History.Count != 0)
            {
                var command = History.Pop();
                command.Cancel();
            }
            return MathUnit.Result;
        }

        /// <summary>
        /// Adds new command to calculator
        /// </summary>
        /// <param name="operation">Operation type</param>
        /// <returns>Result of addition</returns>
        public bool AddCommand(string operation)
        {
            var additionResult = false;
            if (!commands.ContainsKey(operation))
            {
                var command = CommandFactory.Create(operation, MathUnit);
                if (command != null)
                {
                    commands.Add(operation, command);
                    additionResult = true;
                }
            }
            return additionResult;
        }

        /// <summary>
        /// Removes command from calculator
        /// </summary>
        /// <param name="operation">Operation type</param>
        /// <returns>Reuslt of removing</returns>
        public bool RemoveCommand(string operation)
        {
            return commands.Remove(operation) == true ? true : false;
        }

        /// <summary>
        /// Gets selected command
        /// </summary>
        /// <param name="operation">Operation type</param>
        /// <returns>Command</returns>
        private Command GetCommand(string operation)
        {
            if (commands.ContainsKey(operation))
            {
                return (Command)commands[operation].Clone();
            }
            else
            {
                return null;
            }
        }
    }
}
