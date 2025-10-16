using LibAtem.Net;
using LibAtem.Commands;
using LibAtem.Commands.MixEffects;


namespace FC_AtemTallyServer.Services
{
    internal class AtemDiscoveryService
    {

        private string _ProgramInputs;
        public string ProgramInputs
        {
            get => _ProgramInputs;
            set
            {
                if (_ProgramInputs != value)
                {
                    _ProgramInputs = value;
                    ProgramChanged();
                }
            }
        }

        private string _PreviewInputs;
        public string PreviewInputs
        {
            get => _PreviewInputs;
            set
            {
                if (_PreviewInputs != value)
                {
                    _PreviewInputs = value;
                    PreviewChanged();
                }
            }
        }

        public Action ProgramChanged { get; set; }
        public Action PreviewChanged { get; set; }

        private readonly AtemClient _client;

        public AtemDiscoveryService()
        {
            _client = new AtemClient("192.168.1.104");
        }

        public void Start()
        {
            _client.OnReceive += OnCommand;
        }

        private void OnCommand(object sender, IReadOnlyList<ICommand> commands)
        {
            foreach (ICommand cmd in commands)
            {
                try
                {
                    if (cmd is ProgramInputGetCommand)
                        HandleProgram(cmd as ProgramInputGetCommand);
                    else if (cmd is PreviewInputGetCommand)
                        HandlePreview(cmd as PreviewInputGetCommand);
                }
                catch (Exception e)
                {
                    Console.WriteLine("T: {0}", e);
                }
            }
        }

        private void HandleProgram(ProgramInputGetCommand cmd)
        {
            ProgramInputs = cmd.Source.ToString();
        }

        private void HandlePreview(PreviewInputGetCommand cmd)
        {
            PreviewInputs = cmd.Source.ToString();
        }
    }
}
