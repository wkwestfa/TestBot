In order to get the bot setup in your server you're going to want to go to this following link and add it to you server:
https://discordapp.com/oauth2/authorize?client_id=634781455909781504&permissions=0&scope=bot

After you've done that, you will need to run the bot locally.  You can do this by unzipping the bot file and navigating to:
Emeraldbot > bin > Debug > EmeraldBot.exe

A couple of other notes:

Right now the prefix to all commands is '$', but you can change this by opening the following file in notepad:
Emeraldbot > bin > Debug > Reso > config.json
Change the 'cmdPrefix' variable from '$' to whatever you want.

For Minecraft rewards you will want to open the following file in notepad:
Emeraldbot > bin > Debug > Reso > config.json
Here you can see the structure of the rewards with is (InvitesRequired, Reward). 
Make sure the reward is exactly like the Minecraft items say it should be, or else it will not work.
You can add as many tiers to this and the bot will automatically begin tracking them.




