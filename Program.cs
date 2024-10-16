using DSharpPlus;
using DSharpPlus.Entities;
using dotenv.net;
DotEnv.Load();

string? token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");
if (string.IsNullOrWhiteSpace(token)) {
    Console.WriteLine("PLEASE SET 'DISCORD_TOKEN'");
    Environment.Exit(1);
    return;
}

DiscordConfiguration config = new() {
    Token = token,
    Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
};
DiscordClient client = new(config);

client.MessageCreated += async (client, events) => {
    if (events.Message.Author.Id == client.CurrentUser.Id) return;

    if (events.Message.Content.Equals("!ping", StringComparison.OrdinalIgnoreCase)) {
        await events.Message.RespondAsync($"Pong! ゲートウェイのpingは {client.Ping}msでした。");
    }
};

DiscordActivity status = new("with fire", ActivityType.Playing);

await client.ConnectAsync(status, UserStatus.Online);

await Task.Delay(-1);