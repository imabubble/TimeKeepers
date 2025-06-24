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
using System.Diagnostics;

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
        int totalScore1 = 0;
        Era era;
        PlayerPlaying playerPlaying;
        bool timerRunning = false;
        int roundTracker;
        DateTime datetime;
        public MainWindow()
        { 
            InitializeComponent();

            totalScoreBox.Text = "00";
            totalScoreBox1.Text = "00";
            scoreBox.Text = "00";
            scoreBox1.Text = "00";
            timeTextBlock.Text = "10:00";
            timeTextBlock1.Text = "10:00";

            // instantiate playing state
            era = Era.era1;
            playerPlaying = PlayerPlaying.player1Playing;
            roundTracker = 1;

            // instatiate timer
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(10);
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
            if (timerRunning)
            {
                while (roundTracker <= 3)
                {
                    era = Era.era1;
                    RunRounds();
                }
                while ((roundTracker >4) && (roundTracker <= 6))
                {
                    era = Era.era2;
                    RunRounds();
                }
                while ((roundTracker > 6) && (roundTracker <= 9))
                {
                    era = Era.era3;
                    RunRounds();
                }
                if (roundTracker == 10)
                {
                    era = Era.final;
                    RunRounds();
                }
            }
        }

        private void RunRounds()
        {
            int elapsedHundredthsOfSeconds = (int)(datetime - DateTime.Now).TotalMilliseconds / 10;
            hundredthsOfSecondsToGo = 10 * 100 + elapsedHundredthsOfSeconds;
            // the counters go down together when it's the final round
            if (era == Era.final)
            {
                timeTextBlock.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");
                timeTextBlock1.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");
                if (hundredthsOfSecondsToGo <= 0)
                {

                    timeTextBlock.Text = "Time's up!";       
                    timeTextBlock1.Text = "Time's up!";
                    addScore();
                }
            }
            else
            {
                if (playerPlaying == PlayerPlaying.player1Playing)
                {
                    timeTextBlock.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");

                    if (hundredthsOfSecondsToGo <= 0)
                    {

                        timeTextBlock.Text = "Time's up!";
                        addScore();
                        Stop_Reset_Timer();
                        Switch_Player();
                    }
                }
                else
                {
                    timeTextBlock1.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");

                    if (hundredthsOfSecondsToGo <= 0)
                    {
                        timeTextBlock1.Text = "Time's up!";
                        addScore();
                        Stop_Reset_Timer();
                        Switch_Player();
                    }
                }
            }
        }

        private void Stop_Reset_Timer()
        {
            timer.Stop();
            datetime = DateTime.Now;
            timer.Start();
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
            // starts the game if it's not running
            if (!timerRunning)
            { 
                datetime = DateTime.Now;
                timer.Start();
                timerRunning = true;
            }
            else
            {
                timer.Stop();
                timerRunning = false;
                timeTextBlock.Text = "Game Paused";
                timeTextBlock1.Text = "Game Paused";
            }
            
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


        private void addScore()
        {
            // Calculates score to be distance from the next whole integer
            score = (hundredthsOfSecondsToGo) % 100;
            if (score > 50) { score = 100 - score; }
            // adds the score to whomever's turn it is
            if (hundredthsOfSecondsToGo <= 0) { score = 50; }
            if (era == Era.final)
            {
                scoreBox.Text = score.ToString();
                scoreBox1.Text = score.ToString();
                totalScore += score;
                totalScore1 += score;
                totalScoreBox.Text = totalScore.ToString();
                totalScoreBox1.Text = totalScore1.ToString();
            }
            else {
                if (playerPlaying == PlayerPlaying.player1Playing)
                {
                    scoreBox.Text = score.ToString();
                    totalScore += score;
                    totalScoreBox.Text = totalScore.ToString();
                }
                else
                {
                    scoreBox1.Text = score.ToString();
                    totalScore1 += score;
                    totalScoreBox1.Text = totalScore1.ToString();
                }
                // increments the roundTracker
                roundTracker++;
            }
        }

        private void eraChecker()
        {
            // put brushes in here to indicate what era/round it is
        }
    }
}