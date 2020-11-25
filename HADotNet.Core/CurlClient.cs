using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using Newtonsoft.Json;

namespace HADotNet.Core
{
    internal class CurlClient
    {
        internal static async Task<T> Get<T>(string path) where T : class
        {
            var uriPath = new Uri(ClientFactory.InstanceAddress, path);

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(TimeSpan.FromSeconds(15));

                var result = await Cli.Wrap("curl.exe")
                    .WithArguments($"-s -H \"Authorization: Bearer {ClientFactory.ApiKey}\" {uriPath}")
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync(cts.Token);

                if (result.ExitCode == 0 && !string.IsNullOrEmpty(result.StandardOutput))
                {
                    return JsonConvert.DeserializeObject<T>(result.StandardOutput);
                }

                throw new Exception($"Unexpected exitcode {result.ExitCode} from CURL API execution on {uriPath}." +
                                    $"\r\nContent: [{result.StandardOutput}]" +
                                    $"\r\nError: {result.StandardError}");
            }
        }
    }
}
