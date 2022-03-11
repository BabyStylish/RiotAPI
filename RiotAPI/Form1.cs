using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Camille.RiotGames;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;

namespace RiotAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
    
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Connection to Riot Services
            var riotApi = RiotGamesApi.NewInstance("RGAPI-0db89d06-248b-4ddc-864b-66330e63fcdb");

            //Get summoner info - txtSearch
            var summoner = riotApi.SummonerV4().GetBySummonerName(Camille.Enums.PlatformRoute.EUW1, txtSearch.Text);
            if(summoner == null)
            {
                MessageBox.Show("Summoner not found with name: " + txtSearch.Text);
                return;
            }

            //get SummonerID
            var accId = summoner.Id;
            Console.WriteLine(accId);

            //Get Summoner info based on SummonerID
            //Dinamically Search for rank image that corresponds to Summoner rank
            //Load image from filepath rankImgPath and Stretch
            var league = riotApi.LeagueV4().GetLeagueEntriesForSummoner(Camille.Enums.PlatformRoute.EUW1, accId);
            var soloqueue = league.Where(l => l.QueueType == Camille.Enums.QueueType.RANKED_SOLO_5x5).FirstOrDefault();
            if (soloqueue != null)
            {
                var rank = UppercaseFirst(soloqueue.Tier.ToString().ToLower());
                Console.WriteLine(rank);
                var rankImgPath = "D:\\VS_Repo\\RiotAPI\\RiotAPI\\Ranks\\Emblem_" + rank + ".png";
                imgRank.Image = Image.FromFile(rankImgPath);
            }

            //Lp Display
            lblLP.Text = league[0].LeaguePoints.ToString() + " LP";

            //Champion mastery handling
            imgLoad(riotApi, summoner);

            //Get summoner Icon ID
            //Load icon into picturebox and stretch
            var iconID = summoner.ProfileIconId;
            string linkIcon = "https://raw.communitydragon.org/12.4/game/assets/ux/summonericons/profileicon" + iconID + ".png";
            imgSummonerIcon.Load(linkIcon);

            //Mastery Points
            var masteryPoints = league[0].LeaguePoints;
            Console.WriteLine(masteryPoints);


            //Win and loss label
            lblWins.Text = league[0].Wins + " W";
            lblLosses.Text = league[0].Losses + " L";

            //Summoner Level and Username
            lblLevel.Text = summoner.SummonerLevel.ToString();
            lblUsername.Text = txtSearch.Text;

            //Match History  
            var matchID = riotApi.MatchV5().GetMatchIdsByPUUID(Camille.Enums.RegionalRoute.EUROPE, summoner.Puuid);

            foreach (string i in matchID)
            {
                Console.WriteLine(i);
                var match = riotApi.MatchV5().GetMatch(Camille.Enums.RegionalRoute.EUROPE, i.ToString());
                var gameDuration = Convert.ToInt32(match.Info.GameDuration);
                var gameDate = match.Info.GameCreation;
                lblGameDuration.Text = gameDuration.ToString() + gameDate.ToString();

                lblchampName.Text = txtSearch.Text;

                var gameMode = match.Info.GameMode;
                
                var gameType = match.Info.GameType;

                var map = match.Info.MapId.ToString();
                lblMap.Text = map;

                var participant = match.Info.Participants.Where(p => p.SummonerId == summoner.Id).First();

                var championLevel = participant.ChampLevel;
                lblchampLevel.Text = championLevel.ToString();

                var championPlayed = participant.ChampionName;
                lblchampName.Text = championPlayed.ToString();


                //Champion icon
                var championImagePath = "http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championPlayed.ToString() + ".png";
                try
                {
                    champImage.Load(championImagePath);
                }
                catch (Exception ex)
                {
                        MessageBox.Show(ex.Message);
                        continue;
                }

                var mapName = match.Info.MapId.ToString();

                //KDA
                var kills = participant.Kills;
                var deaths = participant.Deaths;
                var assists = participant.Assists;
                lblKDA.Text = kills.ToString() + "/" + deaths.ToString() + "/" + assists.ToString();

                //Gold Earned
                var goldEarned = participant.GoldEarned;
                lblGold.Text = goldEarned.ToString();

                //Summoner Spells
                //Using Methods SummonerSpellDLoad and SummonerSpellFLoad
                var summonerDid = participant.Summoner1Id;
                var summonerFid = participant.Summoner2Id;

                summonerSpellLoad(summonerDid, imgSummonerD);
                summonerSpellLoad(summonerFid, imgSummonerF);


                //Items and loading icon
                var item0 = participant.Item0;
                var item1 = participant.Item1;
                var item2 = participant.Item2;
                var item3 = participant.Item3;
                var item4 = participant.Item4;
                var item5 = participant.Item5;
                var item6 = participant.Item6;
                if(item0 != 0)
                    imgItem1.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                if(item1 != 0)
                    imgItem2.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                if (item2 != 0)
                    imgItem3.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                if (item3 != 0)
                    imgItem4.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                if (item4 != 0)
                    imgItem5.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                if (item5 != 0)
                    imgItem6.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                if (item6 != 0)
                    imgItem7.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item6 + ".png");


                //Vision Score
                var visionScore = participant.VisionScore;
                lblVisionScore.Text = visionScore.ToString();

                //Wards Killed and Placed
                var wardsKilled = participant.WardsKilled;
                var wardsPlaced = participant.WardsPlaced;

                //Win/Loss Image Load
                var winloss = participant.Win;
                if (winloss == true)
                {
                    //victory
                    var victoryImg = "D:\\VS_Repo\\RiotAPI\\RiotAPI\\WinDefeat\\victorylol.jpg";
                    imgWinDefeat.Image = Image.FromFile(victoryImg);
                }
                else
                {
                    //Defeat
                    var defeatImg = "D:\\VS_Repo\\RiotAPI\\RiotAPI\\WinDefeat\\defeat1.png";
                    imgWinDefeat.Image = Image.FromFile(defeatImg);
                }

            }

        }

        private void imgLoad(RiotGamesApi riotApi, Camille.RiotGames.SummonerV4.Summoner summoner)
        {
            var masteries = riotApi.ChampionMasteryV4().GetAllChampionMasteries(Camille.Enums.PlatformRoute.EUW1, summoner.Id);
            Console.WriteLine(summoner.Id);
            for (var i = 0; i < 3; i++)
            {
                var topChamps = masteries[i];
                var champID = (int)topChamps.ChampionId;
                var champ = champions[champID.ToString()];
                string linkChamp = "http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + champ.Id + ".png";
                if (i == 0)
                {
                    imgTopChamp1.Load(linkChamp);
                }
                else if (i == 1)
                {
                    imgTopChamp2.Load(linkChamp);
                }
                else if (i == 2)
                {
                    imgTopChamp3.Load(linkChamp);
                }

            }
        }

        static string UppercaseFirst(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        static string Lowercase(string s)
        {
            return s.ToLower();
        }

        private void summonerSpellLoad(int id, PictureBox image)
        {
            switch (id)
            {
                case 1:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerBoost.png");
                    break;
                case 3:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerExhaust.png");
                    break;
                case 4:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerFlash.png");
                    break;
                case 6:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerHaste.png");
                    break;
                case 7:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerHeal.png");
                    break;
                case 11:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerSmite.png");
                    break;
                case 12:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerTeleport.png");
                    break;
                case 13:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerMana.png");
                    break;
                case 14:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerDot.png");
                    break;
                case 21:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerBarrier.png");
                    break;
                case 32:
                    image.Load("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/spell/SummonerSnowball.png");
                    break;
            }
        }


        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if(txtSearch.Text == "Summoner Search...")
            {
                txtSearch.Text = "";
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Summoner Search...";
            }
        }

        string path = "D:\\VS_Repo\\RiotAPI\\RiotAPI\\Img";

        public void button1_Click(object sender, EventArgs e)
        {
            var folder = new FolderBrowserDialog();
            folder.ShowDialog();
            path = folder.SelectedPath;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(path))
            {
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists)
            {
                FileInfo[] files = di.GetFiles().Where(f => f.Extension == ".jpg").ToArray();
                if (files.Length > 0)
                {
                    var random = new Random();
                    int fileNumber = random.Next(files.Length);
                    this.tabControl1.TabPages[0].BackgroundImage = Image.FromFile(files[fileNumber].FullName);
                    this.tabControl1.TabPages[0].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        Dictionary<string, Champion> champions = new Dictionary<string, Champion>();
        private void Form1_Load(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/data/en_US/champion.json").Result;
                if(response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);
                    foreach(var champion in result.Data.Values)
                    {
                        champions.Add(champion.Key, champion);
                    }
                }
            }

            if (String.IsNullOrEmpty(path))
            {
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);

            if (di.Exists)
            {
                FileInfo[] files = di.GetFiles().Where(f => f.Extension == ".jpg").ToArray();
                if (files.Length > 0)
                {
                    var random = new Random();
                    int fileNumber = random.Next(files.Length);
                    this.tabControl1.TabPages[0].BackgroundImage = Image.FromFile(files[fileNumber].FullName);
                    this.tabControl1.TabPages[0].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(this, new EventArgs());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        int c = 1;

        public void btnNextGame_Click(object sender, EventArgs e)
        {
            c = c++;
            if(c >= 20)
            {
                c = 20;
            }
            c.ToString();

        }

        private void btnMoreStat_Click(object sender, EventArgs e)
        {
            MoreStats ms = new MoreStats();
            ms.MdiParent = this;
            ms.Show();
        }
    }
}
