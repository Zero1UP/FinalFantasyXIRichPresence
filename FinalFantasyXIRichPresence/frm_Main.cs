using Binarysharp.MemoryManagement;
using DiscordRPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace FinalFantasyXIRichPresence
{
    public partial class frm_Main : Form
    {
        private const string PROCESS_NAME = "pol";
        private const string MODULE_NAME = "FFXiMain.dll";
        MemorySharp mem = null;
        public DiscordRpcClient client;
        bool sessionStarted = false;

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
                Process polProcess = Process.GetProcessesByName(PROCESS_NAME)[0];
                mem = new MemorySharp(polProcess);
                IntPtr ff11BaseAddress;

                ff11BaseAddress = mem[MODULE_NAME].BaseAddress;

                if (mem.IsRunning)
                {
                    string playerName = mem.ReadString(ff11BaseAddress + 0x4CF6E0, Encoding.Default, false, 10);
                    string serverName = mem.ReadString(ff11BaseAddress + 0x4CF6F0, Encoding.Default, false, 15);
                    short partyCount = mem.Read<short>(ff11BaseAddress + 0x62312B, false);
                    short mainJobLevel = mem.Read<short>(ff11BaseAddress + 0x9ADE06, false);
                    short subJobLevel = mem.Read<short>(ff11BaseAddress + 0x977040, false);
                    short mainJobID = mem.Read<byte>(ff11BaseAddress + 0x9ADE08, false);
                    short subJobID = mem.Read<byte>(ff11BaseAddress + 0x97703F, false);
                    short zoneID = mem.Read<byte>(ff11BaseAddress + 0x622892, false);
                    
                    setPresence(serverName, mainJobLevel, subJobLevel, playerName, partyCount,mainJobID,subJobID,zoneID);
                    return;
                }
                client.SetPresence(defaultPresence);
                sessionStarted = false;
            }
            catch (Exception ex)
            {

                client.SetPresence(defaultPresence);
                sessionStarted = false;
            }

        }
    }
}
