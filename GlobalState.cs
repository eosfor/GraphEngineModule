using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trinity.Storage.Composite;
using Trinity.Utilities;
using Trinity.Storage;
using FanoutSearch;
using Trinity;
using Trinity.Configuration;
using Serialize.Linq.Serializers;
using Trinity.Network;

namespace GraphEngineModule
{
    internal class GlobalState
    {
        private static GlobalState _instance;
        private bool _isInitialized = false;

        private GlobalState() {

        }

        public static GlobalState Instance {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalState();
                }

                return _instance;

            }
        }

        public void Initialize(string LogDirectory, string StorageRoot, bool LogEchoOnConsole)
        {
            if (!_isInitialized )
            {
                TrinityConfig.LogEchoOnConsole = LogEchoOnConsole;
                LoggingConfig.Instance.LogDirectory = LogDirectory;
                StorageConfig.Instance.StorageRoot = StorageRoot;

                Global.Initialize();

                LambdaDSL.SetDialect("MAG", "StartFrom", "VisitNode", "FollowEdge", "Action");
                FanoutSearchModule.SetQueryTimeout(-1);
                TrinityServer server = new TrinityServer();
                server.RegisterCommunicationModule<FanoutSearchModule>();
                server.Start();

                _isInitialized = true;
            }

        }

        public void Initialize(string Path)
        {
            throw new NotImplementedException();
        }
        public bool IsInitialized { get { return _isInitialized; } }
    }

}
