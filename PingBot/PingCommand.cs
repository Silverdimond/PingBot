using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace PingBot
{
    internal class PingCommand : BaseCommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx) => await new DiscordMessageBuilder().WithReply(ctx.Message.Id).WithContent($"🏓 Pong! {ctx.Client.Ping}ms").SendAsync(ctx.Channel);
    }
}
