using Rocket.API;
using System.Collections.Generic;

namespace crythesly.AirdropManager
{
        public class MassAirdropCommand : IRocketCommand

        {
            public void Execute(IRocketPlayer caller, params string[] command)
            {
                AirdropManager.Instance.MassDrop();
            }

            public string Help
            {
                get { return "Mass Airdrop"; }
            }

            public string Name
            {
            get { return "massdrop"; }
            }

            public string Syntax
            {
                get { return "<player>"; }
            }

            public List<string> Aliases
            {
            get { return new List<string>() { "mdrop", "massairdrop", "airdropmass", "dropmass", "mairdrop" }; }
            }

            public List<string> Permissions
            {
                get
                {
                    return new List<string>() { "crythesly.massdrop" };
                }
            }


            public AllowedCaller AllowedCaller
            {
                get { return Rocket.API.AllowedCaller.Both; }
            }
        }
    }
