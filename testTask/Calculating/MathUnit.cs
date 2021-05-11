using System;
using System.Collections.Generic;

namespace Calculating
{
    /// <summary>
    /// Performs mathmatical operations
    /// </summary>
    public class MathUnit
    {
        private double initial;

        /// <summary>
        /// Initial value
        /// </summary>
        public double Initial { get => initial; set { initial = value; Result = value; } }

        /// <summary>
        /// Result value
        /// </summary>
        public double Result { get; set; }

        /// <summary>
        /// Commands supplied by math unit
        /// </summary>
        public readonly Dictionary<string,CommandTypes> ExistingCommands  = new Dictionary<string, CommandTypes>{ { "+", CommandTypes.Binary },
            { "-", CommandTypes.Binary }, { "*", CommandTypes.Binary }, { "/", CommandTypes.Binary }, { "pow", CommandTypes.Binary },
            { "extract", CommandTypes.Binary }, { "round", CommandTypes.Binary }, { "exp", CommandTypes.Unary },{ "log", CommandTypes.Unary } };
        
        /// <summary>
        /// Runs binary command
        /// </summary>
        /// <param name="operationType">Type of operation</param>
        /// <param name="value">Command parameter</param>
        public void RunBinary(string operationType, double value)
        {
            switch (operationType)
            {
                case "+":
                    Result += value;
                    break;
                case "-":
                    Result -= value;
                    break;
                case "*":
                    Result *= value;
                    break;
                case "/":
                    Result /= value;
                    break;
                case "pow":
                    Result = Math.Pow(Result,value);
                    break;
                case "extract":
                    Result = Math.Pow(Result,1/value);
                    break;
                case "round":
                    Result = Math.Round(Result,(int)value);
                    break;
            }
        }

        /// <summary>
        /// Runs command without parameter
        /// </summary>
        /// <param name="operationType">Operation type</param>
        public void RunUnary(string operationType)
        {
            switch (operationType)
            {
                case "exp":
                    Result = Math.Exp(Result);
                    break;
                case "log":
                    Result = Math.Log(Result);
                    break;
            }
        }
    }
}
