﻿using GameSrv.Player;
using SystemModule.Enums;

namespace GameSrv.GameCommand.Commands {
    /// <summary>
    /// 删除指定行会名称
    /// </summary>
    [Command("DelGuild", "删除指定行会名称", help: "行会名称", 10)]
    public class DelGuildCommand : GameCommand {
        [ExecuteCommand]
        public void Execute(string[] @Params, PlayObject PlayObject) {
            if (@Params == null) {
                return;
            }
            string sGuildName = @Params.Length > 0 ? @Params[0] : "";
            if (M2Share.ServerIndex != 0) {
                PlayObject.SysMsg("只能在主服务器上才可以使用此命令删除行会!!!", MsgColor.Red, MsgType.Hint);
                return;
            }
            if (string.IsNullOrEmpty(sGuildName)) {
                PlayObject.SysMsg(Command.CommandHelp, MsgColor.Red, MsgType.Hint);
                return;
            }
            if (M2Share.GuildMgr.DelGuild(sGuildName)) {
                World.WorldServer.SendServerGroupMsg(Messages.SS_206, M2Share.ServerIndex, sGuildName);
            }
            else {
                PlayObject.SysMsg("没找到" + sGuildName + "这个行会!!!", MsgColor.Red, MsgType.Hint);
            }
        }
    }
}