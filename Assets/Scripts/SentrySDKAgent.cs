using System.Collections.Generic;
using Sentry;
using Sentry.Unity;
using UnityEngine;

namespace Sofunny.BiuBiuBiu2.Util {
    public class SentrySDKAgent {
        public static SentrySDKAgent Instance {
            get {
                if (instance == null) {
                    instance = new SentrySDKAgent();
                }
                return instance;
            }
        }

        private string sceneSign;
        private string playerId;
        private string uid;
        private string openId;
        private string warId;
        private string playMode;
        private string networkVersion;
        private string host;
        private int port;

        private string clientDsn = "";
        private string serverDsn = "";
        
        private Dictionary<string, object> extra = new Dictionary<string, object>();
        private static SentrySDKAgent instance;
        private static bool isInit = false;

        public void Init(string buildName, string showGameVersion, long networkVersion) {
            if (isInit) {
                return;
            }

            this.networkVersion = networkVersion.ToString();
            var dsn = clientDsn;
#if BUILD_SERVER
            dsn = serverDsn;
#endif
            isInit = true;
            SentryInitialization.Init(dsn, buildName, showGameVersion);
            UpdateSentrySDKData();
        }

        public void SetScene(string sign) {
            sceneSign = sign;
            UpdateSentrySDKData();
        }
        
        public void SetMode(string mode) {
            this.playMode = mode;
            UpdateSentrySDKData();
        }
        
        public void SetIPPort(string host, int port) {
            this.host = host;
            this.port = port;
            UpdateSentrySDKData();
        }
        
        public void SetWarId(string warId) {
            this.warId = warId;
            UpdateSentrySDKData();
        }
        
        public void SetPlayerId(long playerId) {
            this.playerId = playerId.ToString();
            UpdateSentrySDKData();
        }
        
        public void SetUID(string uid) {
            this.uid = uid;
            UpdateSentrySDKData();
        }
        
        public void SetOpenId(string openId) {
            this.openId = openId;
            UpdateSentrySDKData();
        }
        
        public void SetExtra(string key, object value) {
            if (extra.ContainsKey(key)) {
                extra[key] = value;
            } else {
                extra.Add(key, value);
            }

            UpdateSentrySDKData();
        }

        private void UpdateSentrySDKData() {
            if (!isInit) {
                return;
            }

            SentrySdk.ConfigureScope((Scope scope) => {
                foreach (var kv in extra) {
                    scope.SetExtra(kv.Key, kv.Value);
                }

                scope.SetExtra("unityVersion", Application.unityVersion);
                scope.SetTag("playerId", playerId);
                scope.SetTag("scene", SystemInfo.deviceUniqueIdentifier);
                scope.SetTag("sceneSign", sceneSign);
                scope.SetTag("networkVersion", networkVersion);
                scope.SetTag("openId", openId);
                scope.SetTag("warId", warId);
                scope.SetTag("playMode", playMode);
                scope.SetTag("host", host);
                scope.SetTag("port", port.ToString());
                
#if BUILD_SERVER
                scope.User = new SentryUser() {
                    Id = warId
                };
#else
                scope.User = new SentryUser() {
                    Id = uid,
                };
#endif
            });
        }
    }
}
