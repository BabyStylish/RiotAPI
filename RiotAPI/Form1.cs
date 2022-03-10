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
            var riotApi = RiotGamesApi.NewInstance("RGAPI-9473a366-19a2-4287-b6db-812c328fdd4a");

            //Get summoner info - txtSearch
            var summoner = riotApi.SummonerV4().GetBySummonerName(Camille.Enums.PlatformRoute.EUW1, txtSearch.Text);

            //get SummonerID
            var accId = summoner.Id;
            if(accId == null)
            {
                return;
            }
            Console.WriteLine(accId);

            //Get Summoner info based on SummonerID
            //Dinamically Search for rank image that corresponds to Summoner rank
            //Load image from filepath rankImgPath and Stretch
            var league = riotApi.LeagueV4().GetLeagueEntriesForSummoner(Camille.Enums.PlatformRoute.EUW1, accId);
            var rank = UppercaseFirst(league[0].Tier.ToString().ToLower());
            Console.WriteLine(rank);
            var rankImgPath = "D:\\VS_Repo\\RiotAPI\\RiotAPI\\Ranks\\Emblem_" + rank + ".png";
            imgRank.Image = Image.FromFile(rankImgPath);
            this.imgRank.SizeMode = PictureBoxSizeMode.StretchImage;

            //Lp Display
            lblLP.Text = league[0].LeaguePoints.ToString() + " LP";

            //Champion mastery handling
            imgLoad(riotApi, summoner);

            //Get summoner Icon ID
            //Load icon into picturebox and stretch
            var iconID = summoner.ProfileIconId;
            string linkIcon = "https://raw.communitydragon.org/12.4/game/assets/ux/summonericons/profileicon" + iconID + ".png";
            imgSummonerIcon.Load(linkIcon);
            imgSummonerIcon.SizeMode = PictureBoxSizeMode.StretchImage;

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


                //Repository Exception - Not updated (Doesn't have all champion icons)
                //Ignoring names that don't follow the pattern
                //DB to be implemented
                var championImagePath = "https://raw.communitydragon.org/12.4/game/assets/characters/" + Lowercase(championPlayed.ToString()) + "/hud/" + Lowercase(championPlayed.ToString()) + "_square.png";
                try
                {
                    champImage.Load(championImagePath);
                    champImage.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    try
                    {
                        var championImagePath0 = "https://raw.communitydragon.org/12.4/game/assets/characters/" + Lowercase(championPlayed.ToString()) + "/hud/" + Lowercase(championPlayed.ToString()) + "_square_0.png";
                        champImage.Load(championImagePath0);
                        champImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                        continue;
                    }
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
                var summonerDid = participant.Summoner1Id;
                var summonerFid = participant.Summoner2Id;

                summonerSpellDLoad(summonerDid);
                summonerSpellFLoad(summonerFid);


                //Items
                var item0 = participant.Item0;
                var item1 = participant.Item1;
                var item2 = participant.Item2;
                var item3 = participant.Item3;
                var item4 = participant.Item4;
                var item5 = participant.Item5;
                var item6 = participant.Item6;

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
                    this.imgWinDefeat.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    //Defeat
                    var defeatImg = "D:\\VS_Repo\\RiotAPI\\RiotAPI\\WinDefeat\\defeat1.png";
                    imgWinDefeat.Image = Image.FromFile(defeatImg);
                    this.imgWinDefeat.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                //var team = match.Info.Teams;
                //foreach (var t in team)
                //{
                //    var bans = match.Info.Teams[0].Bans;
                //    foreach (var b in bans)
                //    {
                //        var champName = b.ChampionId.ToString();
                        
                //    }
                //}

            }

        }

        private void imgLoad(RiotGamesApi riotApi, Camille.RiotGames.SummonerV4.Summoner summoner)
        {
            var masteries = riotApi.ChampionMasteryV4().GetAllChampionMasteries(Camille.Enums.PlatformRoute.EUW1, summoner.Id);
            for (var i = 0; i < 3; i++)
            {
                try
                {
                    var topChamps = masteries[i];
                    var champ = topChamps.ChampionId.ToString().ToLower();
                    string linkChamp = "https://raw.communitydragon.org/12.4/game/assets/characters/" + champ.ToString() + "/hud/" + champ.ToString() + "_square.png";
                    if (i == 0)
                    {
                        imgTopChamp1.Load(linkChamp);
                        imgTopChamp1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (i == 1)
                    {
                        imgTopChamp2.Load(linkChamp);
                        imgTopChamp2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (i == 2)
                    {
                        imgTopChamp3.Load(linkChamp);
                        imgTopChamp3.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                catch
                {
                    var topChamps = masteries[i];
                    var champ = topChamps.ChampionId.ToString().ToLower();
                    string linkChamp = "https://raw.communitydragon.org/12.4/game/assets/characters/" + champ.ToString() + "/hud/" + champ.ToString() + "_square_0.png";
                    if (i == 0)
                    {
                        imgTopChamp1.Load(linkChamp);
                        imgTopChamp1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (i == 1)
                    {
                        imgTopChamp2.Load(linkChamp);
                        imgTopChamp2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (i == 2)
                    {
                        imgTopChamp3.Load(linkChamp);
                        imgTopChamp3.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }

            }
        }

        private void summonerSpellDLoad(int id)
        {
            switch (id)
            {
                case 1:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerCleanse.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 3:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerExhaust.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 4:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerFlash.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 6:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerGhost.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 7:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerHeal.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 11:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerSmite.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 12:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerTeleport.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 13:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerClarity.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 14:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerIgnite.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 21:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerBarrier.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 32:
                    imgSummonerD.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerMark.png");
                    this.imgSummonerD.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
            }
        }

        private void summonerSpellFLoad(int id)
        {
            switch (id)
            {
                case 1:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerCleanse.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 3:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerExhaust.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 4:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerFlash.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 6:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerGhost.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 7:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerHeal.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 11:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerSmite.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 12:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerTeleport.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 13:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerClarity.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 14:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerIgnite.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 21:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerBarrier.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case 32:
                    imgSummonerF.Image = Image.FromFile("D:\\VS_Repo\\RiotAPI\\RiotAPI\\SummonerSpells\\SummonerMark.png");
                    this.imgSummonerF.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
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

        private void Form1_Load(object sender, EventArgs e)
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

        //bool test(FileInfo f)
        //{
        //    return f.Extension == "jpg";
        //}
    }
}
