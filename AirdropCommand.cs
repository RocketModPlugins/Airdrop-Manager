using Rocket.API;
using System.Collections.Generic;
using Rocket.Unturned.Chat;
using UnityEngine;

namespace crythesly.AirdropManager
{
    public class AirdropCommmand : IRocketCommand

    {
        public void Execute(IRocketPlayer caller, params string[] command)
        {
            AirdropManager.Instance.TimeDrop();
        }

        public string Help
        {
            get { return "Airdrop"; }
        }

        public string Name
        {
            get { return "airdrop"; }
        }

        public string Syntax
        {
            get { return "<player>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>() { }; }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "crythesly.airdrop" };
            }
        }


        public AllowedCaller AllowedCaller
        {
            get { return Rocket.API.AllowedCaller.Both; }
        }
    }
}
