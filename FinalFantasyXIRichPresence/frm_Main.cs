using DiscordRPC;
using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace FinalFantasyXIRichPresence
{
    public partial class frm_Main : Form
    {
        private const string OFFSET_FILE_PATH = ".\\data\\data.json";
        public DiscordRpcClient client;
        bool sessionStarted = false;
        PlayerData pData = new PlayerData();
        private RichPresence defaultPresence = new RichPresence()
        {
            Details = "Not Currently logged in.",
            State = "N/A",
            Assets = new Assets()
            {
                LargeImageKey = "main",
                LargeImageText = "Final Fantasy XI",
                SmallImageKey = "main"
            },
            Timestamps = null

        };
        private RichPresence presence = new RichPresence();
        public frm_Main()
        {
            InitializeComponent();
            client = new DiscordRpcClient("875985842450083850");
            client.Initialize();
            if (!File.Exists(OFFSET_FILE_PATH))
            {
                client.SetPresence(presence);
                return;
            }
            pData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(OFFSET_FILE_PATH));

            client.SetPresence(presence);
        }

        private void setPresence(string serverName, short mainJobLevel,short subJobLevel, string playerName, int partyCount,short mainJobID,short subJobID,short zoneID)
        {
            presence.Details = serverName + " - " + playerName + " (" + Collections.Zones[zoneID] + ")";
            presence.State = Collections.Jobs[mainJobID] + ": " + mainJobLevel.ToString() + " / " + Collections.Jobs[subJobID] + ": " + subJobLevel.ToString() ;
            presence.Assets = new Assets()
            {
                LargeImageKey = "main",
                LargeImageText = "Final Fantasy XI",
                SmallImageKey = "main"
            };
            presence.Party = new Party()
            {
                Size = partyCount,
                Max = 6,
                ID = new Guid().ToString(),
                Privacy = Party.PrivacySetting.Private
            };
            presence.HasParty();

            if (!sessionStarted)
            {
                presence.Timestamps = new Timestamps()
                {
                    Start = DateTime.UtcNow
                };

                sessionStarted = true;
            }
            client.SetPresence(presence);
        }
        private void tmr_ProcessCheck_Tick(object sender, EventArgs e)
        {
            try
            {                
                pData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(OFFSET_FILE_PATH));
                setPresence("N/A", pData.main_job_level, pData.sub_job_level, pData.name, pData.party_count, pData.main_jobId, pData.sub_jobId, pData.zone_id);

            }
            catch (Exception ex)
            {
                client.SetPresence(defaultPresence);
                sessionStarted = false;
            }

        }
    }
}
