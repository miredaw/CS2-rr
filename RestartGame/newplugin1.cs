using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using Microsoft.Extensions.Logging;

namespace RestartGame;

public class newplugin1 : BasePlugin
{
    public override string ModuleName => "Restart Game";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "Mireda";
    public override string ModuleDescription => "bringing back !rr and !r1 to cs2";

    public override void Load(bool hotReload)
    {
        Logger.LogInformation("-----> Loaded Restart Game by Mireda!");
    }

    public override void Unload(bool hotReload)
    {
        Logger.LogInformation("-----> UnLoaded Restart Game by Mireda!");
    }
    public static CCSGameRules GetGameRules()
    {
        var gameRulesEntities = Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules");
        var gameRules = gameRulesEntities.First().GameRules;

        if (gameRules == null)
        {
            throw new Exception($"Game rules not found!");
        }

        return gameRules;
    }
    public static void TerminateRound(RoundEndReason roundEndReason)
    {
        // TODO: once this stops crashing on windows use it there too
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            GetGameRules().TerminateRound(0.1f, roundEndReason);
        }
    }

    [ConsoleCommand("css_r1", "!r1 cmd")]
    [RequiresPermissions("@css/kick")]
    public void OnRestartgamer1(CCSPlayerController? player, CommandInfo commandInfo)
    {

        Server.ExecuteCommand($"mp_restartgame 1");

        commandInfo.ReplyToCommand("\x0C [Mireda] \x06---> Game Has Been Restarted.");
    }

    [ConsoleCommand("css_rr", "!rr cmd")]
    [RequiresPermissions("@css/kick")]
    public void OnRestartgamerr(CCSPlayerController? player, CommandInfo commandInfo)
    {

        //Server.ExecuteCommand($"mp_restartgame 1");

        TerminateRound(RoundEndReason.RoundDraw);

        commandInfo.ReplyToCommand("\x0C [Mireda] \x06---> Round Has Been Restarted.");
    }
}