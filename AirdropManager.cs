using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System;
using UnityEngine;

namespace crythesly.AirdropManager
{
    public class AirdropManager : RocketPlugin<AirdropManagerConfiguration>
    {
        public static AirdropManager Instance;
        private DateTime? lastdrop = null;
        private DateTime? lastdropn = null;
        private DateTime? started = null;
        private bool dropNow = false;
        public int time;
        public int timen;
        public bool mtr = false;
        public bool tdr = false;
        ChatManager chat;

        void FixedUpdate()
        {
            if (Configuration.Instance.MassAirdropTimeRangeEnabled == false)
            {
                mtr = false;
            }
            else
            {
                mtr = true;
            }
            if (Configuration.Instance.TimedDropTimeRangeEnabled == false)
            {
                tdr = false;
            }
            else
            {
                tdr = true;
            }
            if (Configuration.Instance.TimedDropEnabled == true && Level.isLoaded)
            {
                CheckTimeDrop();
            }
            if (Configuration.Instance.MassAirdropEnabled == true && Level.isLoaded)
            {
                if (Configuration.Instance.MassAirdropDuration > 2f)
                {
                    Configuration.Instance.MassAirdropDuration = 2f;
                }
                if (dropNow == true)
                {
                    if ((DateTime.Now - started.Value).TotalSeconds < Configuration.Instance.MassAirdropDuration)
                    {
                        LevelManager.airdropFrequency = 0;
                    }
                    else
                    {
                        dropNow = false;
                    }
                }
                else
                {
                    CheckMassDrop();
                }
            }
        }
        protected override void Load()
        {
            Instance = this;
            chat = new ChatManager();
            Rocket.Core.Logging.Logger.LogWarning("Airdrop Manager by CryTheSly has been loaded");
            timen = UnityEngine.Random.Range(Configuration.Instance.TimedDropTimeMin, Configuration.Instance.TimedDropTimeMax);
            time = UnityEngine.Random.Range(Configuration.Instance.MassAirdropTimeMin, Configuration.Instance.MassAirdropTimeMax);
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.LogWarning("Airdrop Manager by CryTheSly has been Unloaded");
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                {"massdrop_comming","Mass Airdrop Incoming!"},
                {"timedrop_comming","Airdrop Incoming!"},
                };
            }
        }

        public void MassDrop()
        {
            if (mtr == true)
            {
                time = UnityEngine.Random.Range(Configuration.Instance.MassAirdropTimeMin, Configuration.Instance.MassAirdropTimeMax);
            }
            else
            {
                time = Configuration.Instance.MassAirdropInterval;
            }
            UnturnedChat.Say(Translate("massdrop_comming"), UnturnedChat.GetColorFromName(Configuration.Instance.MassDropColor, Color.green));
            started = DateTime.Now;
            dropNow = true;
        }
        public void TimeDrop()
        {
            LevelManager.airdropFrequency = 0;
            UnturnedChat.Say(Translate("timedrop_comming"), UnturnedChat.GetColorFromName(Configuration.Instance.TimedDropColor, Color.green));
        }

        private void CheckMassDrop()
        {
            try
            {
                if (State == Rocket.API.PluginState.Loaded && (lastdrop == null || ((DateTime.Now - lastdrop.Value).TotalSeconds > time)))
                {
                    lastdrop = DateTime.Now;
                    MassDrop();
                }
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.LogException(ex);
            }
        }
        private void CheckTimeDrop()
        {
            try
            {
                if (State == Rocket.API.PluginState.Loaded && (lastdropn == null || ((DateTime.Now - lastdropn.Value).TotalSeconds > timen)))
                {
                    lastdropn = DateTime.Now;
                    TimeDrop();
                    if (tdr == true)
                    {
                        timen = UnityEngine.Random.Range(Configuration.Instance.TimedDropTimeMin, Configuration.Instance.TimedDropTimeMax);
                    }
                    else
                    {
                        timen = Configuration.Instance.TimedDropInterval;
                    }
                }
            }
            catch (Exception ex)
            {
                Rocket.Core.Logging.Logger.LogException(ex);
            }
        }
    }
}
