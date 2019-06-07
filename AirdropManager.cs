using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

using Logger = Rocket.Core.Logging.Logger;
using Random = UnityEngine.Random;

namespace crythesly.AirdropManager
{
    public class AirdropManager : RocketPlugin<AirdropManagerConfiguration>
    {
        public static AirdropManager Instance;

        private DateTime? lastdrop;
        private DateTime? lastdropn;

        private bool NodesAreLoaded;
        public int time;
        public int timen;

        public static List<AirdropNode> Nodes { get; set; }

        protected override void Load()
        {
            Instance = this;
            NodesAreLoaded = false;
            Logger.LogWarning("Airdrop Manager by CryTheSly (Fixed by Community of RocketMod) has been loaded");
            timen = Random.Range(Configuration.Instance.TimedDropTimeMin, Configuration.Instance.TimedDropTimeMax);
            time = Random.Range(Configuration.Instance.MassAirdropTimeMin, Configuration.Instance.MassAirdropTimeMax);
            Level.onLevelLoaded += LoadNodes;
        }

        public void LoadNodes(int level)
        {
            if (level > Level.BUILD_INDEX_SETUP && Level.info != null)
            {
                Nodes = GetNodes();
                NodesAreLoaded = true;
            }
        }

        protected override void Unload() => Logger.LogWarning("Airdrop Manager by CryTheSly (Fixed by Community of RocketMod) has been Unloaded");

        public void FixedUpdate()
        {
            if (Configuration.Instance.TimedDropEnabled && NodesAreLoaded)
                CheckTimeDrop();
            if (Configuration.Instance.MassAirdropEnabled && NodesAreLoaded)
                CheckMassDrop();
        }


        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "massdrop_coming", "Mass Airdrop Incoming!" },
            { "timedrop_coming", "Airdrop Incoming!" }
        };

        public void MassDrop()
        {
            time = Configuration.Instance.MassAirdropTimeRangeEnabled ? Random.Range(Configuration.Instance.MassAirdropTimeMin, Configuration.Instance.MassAirdropTimeMax) : Configuration.Instance.MassAirdropInterval;
            UnturnedChat.Say(Translate("massdrop_coming"), UnturnedChat.GetColorFromName(Configuration.Instance.MassDropColor, Color.green));
            for (byte i = 0; i < Configuration.Instance.MassAirdropCount; i++)
            {
                AirdropNode airdropNode = Nodes[Random.Range(0, Nodes.Count)];
                LevelManager.airdrop(airdropNode.point, airdropNode.id, Provider.modeConfigData.Events.Airdrop_Speed);
            }
        }

        public List<AirdropNode> GetNodes()
        {
            FieldInfo fi = typeof(LevelManager).GetField("airdropNodes", BindingFlags.NonPublic | BindingFlags.Static);
            return (List<AirdropNode>)fi.GetValue(null);
        }

        public void TimeDrop()
        {
            LevelManager.airdropFrequency = 0;
            UnturnedChat.Say(Translate("timedrop_coming"), UnturnedChat.GetColorFromName(Configuration.Instance.TimedDropColor, Color.green));
        }

        private void CheckMassDrop()
        {
            if (lastdrop == null || (DateTime.Now - lastdrop.Value).TotalSeconds > time)
            {
                lastdrop = DateTime.Now;
                MassDrop();
            }
        }
        private void CheckTimeDrop()
        {
            if (lastdropn == null || (DateTime.Now - lastdropn.Value).TotalSeconds > timen)
            {
                lastdropn = DateTime.Now;
                TimeDrop();
                timen = Configuration.Instance.TimedDropTimeRangeEnabled ? UnityEngine.Random.Range(Configuration.Instance.TimedDropTimeMin, Configuration.Instance.TimedDropTimeMax) : Configuration.Instance.TimedDropInterval;
            }
        }
    }
}
