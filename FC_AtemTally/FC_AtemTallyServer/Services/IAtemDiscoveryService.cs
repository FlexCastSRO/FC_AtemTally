using LibAtem.Commands;
using LibAtem.Commands.Settings;

namespace FC_AtemTallyServer.Services
{
    internal interface IAtemDiscoveryService
    {
        public Action? AtemConnected { get; set; }
        public Action? AtemDisconnected { get; set; }
        public Action<List<InputPropertiesGetCommand>>? ExternalInputsLoaded { get; set; }
        public event Action<TallyByInputCommand>? TallyStatusChanged;
        public event Action<string>? ErrorMessageChanged;

        public void Init(string atemIpAddress);
        public void Connect();
    }
}
