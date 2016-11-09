using Rocket.API;

namespace crythesly.AirdropManager
{
    public class AirdropManagerConfiguration : IRocketPluginConfiguration
    {
        public bool MassAirdropEnabled;
        public int MassAirdropInterval;
        public bool MassAirdropTimeRangeEnabled;
        public int MassAirdropTimeMin;
        public int MassAirdropTimeMax;
        public float MassAirdropDuration;
        public string MassDropColor;
        public bool TimedDropEnabled;
        public int TimedDropInterval;
        public bool TimedDropTimeRangeEnabled;
        public int TimedDropTimeMin;
        public int TimedDropTimeMax;
        public string TimedDropColor;


        public void LoadDefaults()
        {
            MassAirdropEnabled = true;
            MassAirdropInterval = 3600;
            MassAirdropTimeRangeEnabled = false;
            MassAirdropTimeMin = 3000;
            MassAirdropTimeMax = 3600;
            MassAirdropDuration = 1f;
            MassDropColor = "Magenta";
            TimedDropEnabled = true;
            TimedDropInterval = 1200;
            MassAirdropTimeRangeEnabled = false;
            TimedDropTimeMin = 600;
            TimedDropTimeMax = 1200;
            TimedDropColor = "Magenta";
        }
    }
}
