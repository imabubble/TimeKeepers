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
        public MainWindow()
        { 
            InitializeComponent();
            totalScoreBox.Text = "00";
            scoreBox.Text = "00";
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += Timer_Tick;
            timer.Start();
            hundredthsOfSecondsToGo = 10 * 100;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            hundredthsOfSecondsToGo--;
            timeTextBlock.Text = (hundredthsOfSecondsToGo / 100F).ToString("00.00");
            if (hundredthsOfSecondsToGo == 0)
            {
                timer.Stop();
                timeTextBlock.Text = "Time's up!";
            }

        }

        private void Stop_Reset_Timer()
        {
            timer.Stop();
            timer.Start();
            hundredthsOfSecondsToGo = 10 * 100;

        }

        private void stopGameButton_Click(object sender, RoutedEventArgs e)
        {
            // 
            timer.Stop();
            timeTextBlock.Text = "Game Stopped";
            
        }

        private void pushButton_Click(object sender, RoutedEventArgs e)
        {
            addScore();
            Stop_Reset_Timer();
            
        }

        public void addScore()
        {
            score = (hundredthsOfSecondsToGo) % 100;
            Console.WriteLine(hundredthsOfSecondsToGo);
            Console.WriteLine(score);
            scoreBox.Text = score.ToString();
            totalScore += score;
            totalScoreBox.Text = totalScore.ToString();
        }

    }
}
