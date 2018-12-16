using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace TheTankGame.Core
{
    using System.Collections.Generic;

    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            inputParameters = inputParameters.ToList();
            string command = inputParameters[0];
            inputParameters.RemoveAt(0);

            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == inputParameters[0]);

            var methodInfo = type.GetMethods().FirstOrDefault(m => m.Name == command);

            var result = methodInfo.Invoke(methodInfo, inputParameters.ToArray()).ToString();

            return result;
        }
    }
}