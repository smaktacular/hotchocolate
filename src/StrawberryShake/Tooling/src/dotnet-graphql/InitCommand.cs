using McMaster.Extensions.CommandLineUtils;
using StrawberryShake.Tools.OAuth;

namespace StrawberryShake.Tools
{
    public static class InitCommand
    {
        public static void Build(CommandLineApplication init)
        {
            init.Description = "Initialize project and download schema";

            CommandArgument uriArg = init.Argument(
                "uri",
                "The URL to the GraphQL endpoint.",
                c => c.IsRequired());

            CommandOption pathArg = init.Option(
                "-p|--Path",
                "The directory where the client shall be located.",
                CommandOptionType.SingleValue);

            CommandOption nameArg = init.Option(
                "-n|--clientName",
                "The GraphQL client name.",
                CommandOptionType.SingleValue);

            CommandOption jsonArg = init.Option(
                "-j|--json",
                "Console output as JSON.",
                CommandOptionType.NoValue);

            CommandOption headersArg = init.Option(
                "-x|--headers",
                "Custom headers used in request to Graph QL server." +
                "Can be used mulitple times. Example: --headers key1=value1 --headers key2=value2",
                CommandOptionType.MultipleValue);

            AuthArguments authArguments = init.AddAuthArguments();

            init.OnExecuteAsync(cancellationToken =>
            {
                var arguments = new InitCommandArguments(
                    uriArg,
                    pathArg,
                    nameArg,
                    authArguments,
                    headersArg);
                InitCommandHandler handler =
                    CommandTools.CreateHandler<InitCommandHandler>(jsonArg);
                return handler.ExecuteAsync(arguments, cancellationToken);
            });
        }
    }
}
