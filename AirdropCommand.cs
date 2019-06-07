using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Chat;
using UnityEngine;

namespace crythesly.AirdropManager
{
    public class AirdropCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "airdrop";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command) => AirdropManager.Instance.TimeDrop();
    }
}
