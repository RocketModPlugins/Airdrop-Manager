using System;
using Rocket.API;
using System.Collections.Generic;

namespace crythesly.AirdropManager
{
    public class MassAirdropCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "massdrop";

        public string Help => "Mass Airdrop";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { "mdrop", "massairdrop", "airdropmass", "dropmass", "mairdrop" };

        public List<string> Permissions => new List<string>() { };

        public void Execute(IRocketPlayer caller, string[] command) => AirdropManager.Instance.MassDrop();
    }
}
