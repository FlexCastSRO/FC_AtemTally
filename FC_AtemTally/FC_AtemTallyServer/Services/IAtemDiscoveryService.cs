namespace FC_AtemTallyServer.Services
{
    internal interface IAtemDiscoveryService
    {

        public int ExternalInputsCount { get; set; }

        public string PreviewInputs { get; set; }

        public string ProgramInputs { get; set; }

        public Action? ExternalInputsCountChanged { get; set; }

        public Action? ProgramChanged { get; set; }
        public Action? PreviewChanged { get; set; }
        public Action? AtemConnected { get; set; }
        public Action? AtemDisconnected { get; set; }

        public void Init(string atemIpAddress);
        public void Connect();
    }
}
