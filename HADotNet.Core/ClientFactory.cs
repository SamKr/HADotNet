using System;

namespace HADotNet.Core
{
    /// <summary>
    /// Provides a factory which can instantiate API clients (useful in DI scenarios, for example).
    /// </summary>
    public static class ClientFactory
    {
        internal static Uri InstanceAddress { get; set; }
        internal static string ApiKey { get; set; }
        internal static bool UseCurl { get; set; }

        /// <summary>
        /// Gets whether or not the Client Factory has been initialized.
        /// </summary>
        public static bool IsInitialized { get; internal set; }

        /// <summary>
        /// Initializes the client factory with the specified <paramref name="instanceAddress" /> and <paramref name="apiKey" /> which are forwarded to clients instantiated from this factory.
        /// </summary>
        /// <param name="instanceAddress">The Home Assistant base instance address (do not include /api/).</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        /// <param name="useCurl">Activates the use of curl instead of RestSharp (requires curl to be accessible through PATH)</param>
        public static void Initialize(Uri instanceAddress, string apiKey, bool useCurl = false)
        {
            InstanceAddress = instanceAddress;
            ApiKey = apiKey;
            UseCurl = useCurl;
            IsInitialized = true;
        }

        /// <summary>
        /// Initializes the client factory with the specified <paramref name="instanceAddress" /> and <paramref name="apiKey" /> which are forwarded to clients instantiated from this factory.
        /// </summary>
        /// <param name="instanceAddress">The Home Assistant base instance address (do not include /api/).</param>
        /// <param name="apiKey">The Home Assistant long-lived access token.</param>
        /// <param name="useCurl">Activates the use of curl instead of RestSharp (requires curl to be accessible through PATH)</param>
        public static void Initialize(string instanceAddress, string apiKey, bool useCurl = false) => Initialize(new Uri(instanceAddress), apiKey, useCurl);

        /// <summary>
        /// Resets the Client Factory to its initial state (not initialized).
        /// </summary>
        public static void Reset()
        {
            InstanceAddress = null;
            ApiKey = null;
            UseCurl = false;
            IsInitialized = false;
        }

        /// <summary>
        /// Retrieves a new instance of a client by type, preconfigured with the same <see cref="InstanceAddress" /> and <see cref="ApiKey" /> as this <see cref="ClientFactory" /> (from the last time <see cref="Initialize(Uri, string)" /> was called).
        /// </summary>
        /// <typeparam name="TClient">The type of client to get.</typeparam>
        /// <exception cref="Exception">Thrown if this <see cref="ClientFactory" /> is not initialized (call <see cref="Initialize(Uri, string)" /> first).</exception>
        /// <returns>A new instance of the specified <typeparamref name="TClient" /> type.</returns>
        public static TClient GetClient<TClient>() where TClient : BaseClient => (TClient)Activator.CreateInstance(typeof(TClient), InstanceAddress, ApiKey);
    }
}
