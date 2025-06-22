using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TimeKeepersConcept
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int hundredthsOfSecondsToGo;
        int score = 0;
        int totalScore = 0;
        Era era;
        PlayerPlaying playerPlaying;
        int roundTracker;
        public MainWindow()
        { 
            InitializeComponent();

            totalScoreBox.Text = "00";
            totalScoreBox1.Text = "00";
            scoreBox.Text = "00";
            scoreBox1.Text = "00";
            timeTextBlock.Text = "10:00";
            timeTextBlock1.Text = "10:00";

            era = Era.era1;
            playerPlaying = PlayerPlaying.player1Playing;
            roundTracker = 1;

            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += Timer_Tick;
            timer.Start();
            hundredthsOfSecondsToGo = 10 * 100;
        }

        enum PlayerPlaying
        {
            player1Playing,
            player2Playing
        };

        enum Era
        {
            era1,
            era2,
            era3,
            final
        };

        private void Timer_Tick(object sender, EventArgs e)
        {
            hundredthsOfSecondsToGo--;
            if (playerPlaying == PlayerPlaying.player1Playing)
            {
                timeTextBlock.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");

                if (hundredthsOfSecondsToGo == 0)
                {
                    timer.Stop();
                    timeTextBlock.Text = "Time's up!";
                }
            }
            else
            {
                timeTextBlock1.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");

                if (hundredthsOfSecondsToGo == 0)
                {
                    timer.Stop();
                    timeTextBlock1.Text = "Time's up!";
                }
            }
        }

        private void Stop_Reset_Timer()
        {
            timer.Stop();
            timer.Start();
            hundredthsOfSecondsToGo = 10 * 100;

        }

        public void Switch_Player()
        {
            if (playerPlaying == PlayerPlaying.player1Playing)
            {
                playerPlaying = PlayerPlaying.player2Playing;
            }
            else { playerPlaying = PlayerPlaying.player1Playing; }
        }

        private void stopGameButton_Click(object sender, RoutedEventArgs e)
        {
            // 
            timer.Stop();
            timeTextBlock.Text = "Game Stopped";
            
        }

        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            // When you click on player1's button, it either does nothing,
            // or adds score if it's the player 1's turn
            if (playerPlaying == PlayerPlaying.player2Playing) { return; }
            else
            {
                addScore();
                Stop_Reset_Timer();
                Switch_Player();
            }
            
        }
        private void pushButton1_Click(object sender, RoutedEventArgs e)
        {
            // When you click on player1's button, it either does nothing,
            // or adds score if it's the player 1's turn
            if (playerPlaying == PlayerPlaying.player1Playing) { return; }
            else
            {
                addScore();
                Stop_Reset_Timer();
                Switch_Player();
            }

        }


        public void addScore()
        {
            // Calculates score to be distance from the next whole integer
            score = (hundredthsOfSecondsToGo) % 100;
            if (score > 50) { score = 100 - score; }
            // adds the score to whomever's turn it is
            if (playerPlaying == PlayerPlaying.player1Playing)
            {
                scoreBox.Text = score.ToString();
                totalScore += score;
                totalScoreBox.Text = totalScore.ToString();
            }
            else
            {
                scoreBox1.Text = score.ToString();
                totalScore += score;
                totalScoreBox1.Text = totalScore.ToString();
            }
            // increments the roundTracker
            roundTracker++;
        }

    }
}