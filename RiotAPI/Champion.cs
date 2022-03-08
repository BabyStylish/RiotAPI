using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotAPI
{
    public class Champion
    {
        public string Version { get; set; }
        public string Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public Info Info { get; set; }
        public ChampImage Image { get; set; }
        public List<string> Tags { get; set; }
        public string Partype { get; set; }
        public Stats Stats { get; set; }
    }

    public class Info
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Magic { get; set; }
        public int Difficulty { get; set; }
    }

    public class ChampImage
    {
        public string Full { get; set; }
        public string Sprite { get; set; }
        public string Group { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }

    public class Stats
    {
        public int Hp { get; set; }
        public int Hpperlevel { get; set; }
        public int Mp { get; set; }
        public int Mpperlevel { get; set; }
        public int Movespeed { get; set; }
        public int Armor { get; set; }
        public double Armorperlevel { get; set; }
        public int Spellblock { get; set; }
        public double Spellblockperlevel { get; set; }
        public int Attackrange { get; set; }
        public int Hpregen { get; set; }
        public int Hpregenperlevel { get; set; }
        public int Mpregen { get; set; }
        public int Mpregenperlevel { get; set; }
        public int Crit { get; set; }
        public int Critperlevel { get; set; }
        public int Attackdamage { get; set; }
        public int Attackdamageperlevel { get; set; }
        public double Attackspeedperlevel { get; set; }
        public double Attackspeed { get; set; }
    }
}
