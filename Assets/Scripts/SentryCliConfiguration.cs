using Sentry.Unity;
using UnityEngine;

public class SentryCliConfiguration : SentryCliOptionsConfiguration {
    public override void Configure(SentryCliOptions cliOptions) {
#if BUILD_SERVER
        cliOptions.Project = "";
        cliOptions.Organization = "";
        cliOptions.Auth = "";
#else
        cliOptions.Project = "";
        cliOptions.Organization = "";
        cliOptions.Auth = "";
#endif
        
        Debug.Log($"Project:{cliOptions.Project}");
    }
}
