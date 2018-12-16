using System.Collections.Generic;
using System.Linq;

namespace TheTankGame.Core
{
    using System;

    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;
        private readonly TankManager tankManager;
        private readonly IBattleOperator battleOperator;

        public Engine(
            IReader reader,
            IWriter writer,
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;
            this.battleOperator = new TankBattleOperator();
            this.tankManager = new TankManager(battleOperator);
            this.isRunning = false;
        }

        public void Run()
        {
            string input = reader.ReadLine();

            while (input != "Terminate")
            {
                var splitted = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                string command = "";

                if (splitted.Count > 0)
                {
                    command = splitted[0];
                }
                splitted.RemoveAt(0);

                switch (command)
                {
                    case "Vehicle":
                        writer.WriteLine(tankManager.AddVehicle(splitted));
                        break;
                    case "Part":
                        writer.WriteLine(tankManager.AddPart(splitted));
                        break;
                    case "Inspect":
                        writer.WriteLine(tankManager.Inspect(splitted));
                        break;
                    case "Battle":
                        writer.WriteLine(tankManager.Battle(splitted));
                        break;
                }

                input = reader.ReadLine();
            }

            writer.WriteLine(tankManager.Terminate(new List<string>()));
        }
    }
}