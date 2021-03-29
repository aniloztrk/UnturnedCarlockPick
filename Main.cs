using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace CarlockPick
{
    public class Main : RocketPlugin<Config>
    {
        protected override void Load()
        {
            VehicleManager.onVehicleLockpicked += LockPicked;
        }
        protected override void Unload()
        {
            VehicleManager.onVehicleLockpicked -= LockPicked;
        }
        private void LockPicked(InteractableVehicle vehicle, Player instigatingPlayer, ref bool allow)
        {
            var unturnedPlayer = UnturnedPlayer.FromPlayer(instigatingPlayer);
            if (unturnedPlayer.HasPermission(Configuration.Instance.UsingPermission))
            {
                if (vehicle.isLocked)
                { 
                    allow = false;
                    System.Random rastgele = new System.Random();
                    int sayi = rastgele.Next(1, 101);
                    if (sayi <= Configuration.Instance.FailChance)
                    {   
                        StartCoroutine(Fail(unturnedPlayer));
                    }
                    else
                    {
                        StartCoroutine(Unlock(unturnedPlayer, vehicle));
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                unturnedPlayer.GiveItem(1353, 1);
                UnturnedChat.Say(unturnedPlayer, Configuration.Instance.PermissionMessage.Replace('{', '<').Replace('}', '>'));
            }
        }
        public IEnumerator<WaitForSeconds> Unlock(UnturnedPlayer player, InteractableVehicle vehicle)
        {
            for (var kalanSure = 3; kalanSure > 0; kalanSure--)
            {
                ChatManager.serverSendMessage(Configuration.Instance.ReaminingTimeMessage.Replace('{', '<').Replace('}', '>').Replace("%REAMININGTIME%", kalanSure.ToString()), Color.white, player.SteamPlayer(), player.SteamPlayer(), EChatMode.SAY, "https://i.hizliresim.com/X40NAA.png", true);
                yield return new WaitForSeconds(1f);
            }
            VehicleManager.ServerSetVehicleLock(vehicle, vehicle.lockedOwner, vehicle.lockedGroup, false);
            if (Configuration.Instance.DeleteKey == false)
            {
                player.GiveItem(1353, 1);
            }
            ChatManager.serverSendMessage(Configuration.Instance.UnLockMessage.Replace('{', '<').Replace('}', '>'), Color.white, player.SteamPlayer(), player.SteamPlayer(), EChatMode.SAY, "https://i.hizliresim.com/X40NAA.png", true);
        }
        public IEnumerator<WaitForSeconds> Fail(UnturnedPlayer player)
        {
            for (var kalanSure = 3; kalanSure > 0; kalanSure--)
            {
                ChatManager.serverSendMessage(Configuration.Instance.ReaminingTimeMessage.Replace('{', '<').Replace('}', '>').Replace("%REAMININGTIME%", kalanSure.ToString()), Color.white, player.SteamPlayer(), player.SteamPlayer(), EChatMode.SAY, "https://i.hizliresim.com/X40NAA.png", true);
                yield return new WaitForSeconds(1f);
            }
            ChatManager.serverSendMessage(Configuration.Instance.FailMessage.Replace('{', '<').Replace('}', '>'), Color.white, player.SteamPlayer(), player.SteamPlayer(), EChatMode.SAY, "https://i.hizliresim.com/X40NAA.png", true);
        }
    }
}
