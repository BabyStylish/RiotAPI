using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public double Hp { get; set; }
        public double Hpperlevel { get; set; }
        public double Mp { get; set; }
        public double Mpperlevel { get; set; }
        public double Movespeed { get; set; }
        public double Armor { get; set; }
        public double Armorperlevel { get; set; }
        public double Spellblock { get; set; }
        public double Spellblockperlevel { get; set; }
        public double Attackrange { get; set; }
        public double Hpregen { get; set; }
        public double Hpregenperlevel { get; set; }
        public double Mpregen { get; set; }
        public double Mpregenperlevel { get; set; }
        public double Crit { get; set; }
        public double Critperlevel { get; set; }
        public double Attackdamage { get; set; }
        public double Attackdamageperlevel { get; set; }
        public double Attackspeedperlevel { get; set; }
        public double Attackspeed { get; set; }
    }

    public class Root
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string Version { get; set; }
        public Dictionary<string,Champion> Data { get; set; }
    }
}
