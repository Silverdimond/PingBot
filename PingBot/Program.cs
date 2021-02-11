using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace PingBot
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("HEY SHITASS WANNA SEE ME SPEEDRUN");

            MainAsync().GetAwaiter().GetResult();
        }

        private static DiscordClient discord;
        private static Config config;

        private static async Task MainAsync()
        {
            config = await Config.GetAsync();
            discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = config.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            CommandsNextExtension commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = config.Prefix
            });

            commands.RegisterCommands<PingCommand>();

            await discord.ConnectAsync(new DiscordActivity("with shit python code", ActivityType.Competing));

            await Task.Delay(-1);
        }
    }
}