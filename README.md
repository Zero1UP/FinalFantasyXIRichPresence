# Final Fantasy XI Rich Presence for Discord

This will display:
Server - Player Name (Current location)
Main Job and level
Sub Job and level
Current party count

![RichPresence](https://i.imgur.com/ncRwvMc.png)

## How to use
Starting with this new update, I have moved the data collection part of this program to be an addon for Windower 4. To use this addon simply download the latest release and copy the folder into windowerFolder/addons.
You will also need to make an edit to scripts/init.txt. You will need to add the following line: 
```
lua load ffxiplayerinfodrp
```
You will then need to run 'FinalFantasyXIRichPresence.exe' from the addons/ffxiplayerinfodrp folder whenever you want to display the rich presence data on discord.


# Requirements
Windower 4
https://www.windower.net/
.NET 5 is required and can be downloaded here:
https://dotnet.microsoft.com/download/dotnet/5.0/runtime
