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
            //FFXiMain.dll+
            try
            {
                Process polProcess = Process.GetProcessesByName(PROCESS_NAME)[0];
                mem = new MemorySharp(polProcess);
                IntPtr ff11BaseAddress;

                ff11BaseAddress = mem[MODULE_NAME].BaseAddress;

                if (mem.IsRunning)
                {
                    string playerName = mem.ReadString(ff11BaseAddress + 0x4D52E0, Encoding.Default, false, 10);
                    string serverName = mem.ReadString(ff11BaseAddress + 0x4D52F0, Encoding.Default, false, 15) ;
                    short partyCount = mem.Read<byte>(ff11BaseAddress + 0x628CC3, false);
                    //0x97703E level sync
                    short mainJobLevel = mem.Read<byte>(ff11BaseAddress + 0x9B7F76, false);
                    short subJobLevel = mem.Read<byte>(ff11BaseAddress + 0x97CBE0, false);
                    short mainJobID = mem.Read<byte>(ff11BaseAddress + 0x9B7F78, false);
                    short subJobID = mem.Read<byte>(ff11BaseAddress + 0x9B8014, false);
                    short zoneID = BitConverter.ToInt16(mem.Read<byte>(ff11BaseAddress + 0x62842A, 2,false));

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
