using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiotAPI
{
    public partial class MoreStats : Form
    {
        Camille.RiotGames.MatchV5.Match match;
        public MoreStats(Camille.RiotGames.MatchV5.Match match)
        {   
            this.match = match;
            InitializeComponent();

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private async void MoreStats_Load(object sender, EventArgs e)
        {
            //Check participant's teams - Blue/Red
            //Check participant's lane
            var map = match.Info.MapId.ToString();
            if(map == "SUMMONERS_RIFT")
            {
                for (int i = 0; i <= 9; i++)
                {
                    var teamColor = match.Info.Participants[i].TeamId.ToString();
                    var summonerName = match.Info.Participants[i].SummonerName;
                    var championName = match.Info.Participants[i].ChampionName;
                    var teamPosition = match.Info.Participants[i].Lane;
                    var teamrole = match.Info.Participants[i].TeamPosition;
                    var item0 = match.Info.Participants[i].Item0;
                    var item1 = match.Info.Participants[i].Item1;
                    var item2 = match.Info.Participants[i].Item2;
                    var item3 = match.Info.Participants[i].Item3;
                    var item4 = match.Info.Participants[i].Item4;
                    var item5 = match.Info.Participants[i].Item5;
                    var item6 = match.Info.Participants[i].Item6;

                    if (teamColor == "Blue")
                    {
                        if (teamPosition == "TOP")
                        {
                            lblBlueMember1.Text = summonerName;
                            imgBlueChamp1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1B1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2B1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3B1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4B1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5B1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "JUNGLE")
                        {
                            lblBlueMember2.Text = summonerName;
                            imgBlueChamp2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "MIDDLE")
                        {
                            lblBlueMember3.Text = summonerName;
                            imgBlueChamp3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "BOTTOM")
                        {
                            lblBlueMember4.Text = summonerName;
                            imgBlueChamp4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "UTILITY")
                        {
                            lblBlueMember5.Text = summonerName;
                            imgBlueChamp5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                    }
                    else
                    {
                        if (teamPosition == "TOP")
                        {
                            lblRedMember1.Text = summonerName;
                            imgRedChamp1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1R1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2R1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3R1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4R1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5R1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "JUNGLE")
                        {
                            lblRedMember2.Text = summonerName;
                            imgRedChamp2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1R2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2R2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3R2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4R2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5R2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "MIDDLE")
                        {
                            lblRedMember3.Text = summonerName;
                            imgRedChamp3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1R3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2R3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3R3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4R3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5R3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "BOTTOM")
                        {
                            lblRedMember4.Text = summonerName;
                            imgRedChamp4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1R4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2R4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3R4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4R4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5R4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "UTILITY")
                        {
                            lblRedMember5.Text = summonerName;
                            imgRedChamp5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                            if (item0 != 0)
                                imgitem1R5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item0 + ".png");
                            if (item1 != 0)
                                imgitem2R5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item1 + ".png");
                            if (item2 != 0)
                                imgitem3R5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item2 + ".png");
                            if (item3 != 0)
                                imgitem4R5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item3 + ".png");
                            if (item4 != 0)
                                imgitem5R5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item4 + ".png");
                            if (item5 != 0)
                                imgitem6B5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/item/" + item5 + ".png");
                        }
                    }
                }
            }
            else
            {

            }
        }
            
    }
}
