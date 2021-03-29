using Rocket.API;

namespace CarlockPick
{
    public class Config : IRocketPluginConfiguration
    {
        public int FailChance, ReaminingTime;
        public bool DeleteKey;
        public string ReaminingTimeMessage, FailMessage, UnLockMessage, UsingPermission, PermissionMessage;
        public void LoadDefaults()
        {
            FailChance = 20;
            ReaminingTime = 5;
            DeleteKey = true;
            ReaminingTimeMessage = "{color=yellow}Remaining time for unlock :{/color} %REAMININGTIME%";
            FailMessage = "{color=red}Unfortunately you couldn't unlock it :({/color}";
            UnLockMessage = "{color=white}The lock is unlocked!{/color}";
            UsingPermission = "mixy.carlock";
        }
    }
}
