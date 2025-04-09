using Sentry;
using Sentry.Unity;

public class SentryOptionConfiguration : SentryOptionsConfiguration
{
    public override void Configure(SentryUnityOptions options) {
        var clientDsn = "";
        var serverDsn = "";
#if BUILD_SERVER
        options.Dsn = serverDsn;
#else
        options.Dsn = clientDsn;
#endif
    }
}