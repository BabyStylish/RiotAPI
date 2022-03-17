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
                    if (teamColor == "Blue")
                    {
                        if (teamPosition == "TOP")
                        {
                            lblBlueMember1.Text = summonerName;
                            imgBlueChamp1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "JUNGLE")
                        {
                            lblBlueMember2.Text = summonerName;
                            imgBlueChamp2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "MIDDLE")
                        {
                            lblBlueMember3.Text = summonerName;
                            imgBlueChamp3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "BOTTOM")
                        {
                            lblBlueMember4.Text = summonerName;
                            imgBlueChamp4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "UTILITY")
                        {
                            lblBlueMember5.Text = summonerName;
                            imgBlueChamp5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                    }
                    else
                    {
                        if (teamPosition == "TOP")
                        {
                            lblRedMember1.Text = summonerName;
                            imgRedChamp1.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "JUNGLE")
                        {
                            lblRedMember2.Text = summonerName;
                            imgRedChamp2.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "MIDDLE")
                        {
                            lblRedMember3.Text = summonerName;
                            imgRedChamp3.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "BOTTOM")
                        {
                            lblRedMember4.Text = summonerName;
                            imgRedChamp4.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
                        }
                        else if (teamPosition == "BOTTOM" && teamrole == "UTILITY")
                        {
                            lblRedMember5.Text = summonerName;
                            imgRedChamp5.LoadAsync("http://ddragon.leagueoflegends.com/cdn/12.5.1/img/champion/" + championName + ".png");
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
