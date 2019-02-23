using MentalPrepApp.Classes.Extensions;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Media.SpeechRecognition;

namespace MentalPrepApp.Views.UserControls.AppFxs
{
    public sealed partial class RepeaterUserControl : UserControl
    {
        //Repeater Dispatcher Timer
        DispatcherTimer repeatDispTimer = new DispatcherTimer();
        TimeSpan interval;//hh, mm, ss
        int timesToTick;
        int i = 0;
        int repetitions;
       
        //Media Output Async 
        string ttsRaw = string.Empty;

        //Speech User Settings
      
        public string VoiceGender = "female";

        public RepeaterUserControl()
        {
            this.InitializeComponent();
            TimerSetUp();
            cboVoiceGender.SelectedIndex = cboVoiceGender.Items.Count - 1;
        }

        public void TimerSetUp()
        {
            repeatDispTimer.Tick += RepeatDispTimer_Tick;
            Debug.WriteLine("TimerSetUp: " + repeatDispTimer.IsEnabled.ToString());
        }

        #region another timer with slider hooked to it.
        //async Task<int> StopTestAsync(int _reps)
        //{
        //    int reps = _reps + 1;
        //    int totalTime = 0;
        //    double ticksPlusIntervalSecs = 0;
        //    double intervals;

        //    DispatcherTimer dt = new DispatcherTimer();
        //    dt.Interval = new TimeSpan(0, 0, Convert.ToInt16(boxInterval.Text));
        //    intervals = Convert.ToInt16(boxInterval.Text);
        //    dt.Start();
        //    dt.Tick += Dt_Tick;
        //    for (int ticks = 0; ticks < reps; ticks++)
        //    {
        //        ticksPlusIntervalSecs = ticksPlusIntervalSecs + (ticks + intervals);
        //        Debug.WriteLine("dt.interval: " + dt.Interval.ToString());
        //        Debug.WriteLine("ticks: " + ticks.ToString());
        //        Debug.WriteLine("ticksPlusIntervalSecs accruing: "
        //                                        + ticksPlusIntervalSecs.ToString());
        //    }   
        //    //if (reps > Convert.ToInt16(boxRepetitions.Text))
        //    //{
        //    //    dt.Stop();
        //    //}
        //    Debug.WriteLine("\ndt.IsEnabled: " + dt.IsEnabled.ToString());

        //    totalTime = Convert.ToInt16(ticksPlusIntervalSecs);
        //    return (totalTime);
        //}

        //private void Dt_Tick(object sender, object e)
        //{
        //    SdrSpeakAsyncProgress.Value = SdrSpeakAsyncProgress.Value + 10;
        //    Debug.WriteLine("\n\nSdrSpeakAsyncProgress.Value = + 1: "
        //                                    + SdrSpeakAsyncProgress.Value.ToString());
        //}


        //private async void TgbCommandModeOn_Click(object sender, RoutedEventArgs e)
        //{          
        //    TgbCommandModeOn.Foreground = new SolidColorBrush(Colors.DodgerBlue);
        //    int reps;
        //    reps = Convert.ToInt16(boxRepetitions.Text);         
        //    await StopTestAsync(reps);
        //    TgbCommandModeOn.Foreground = new SolidColorBrush(Colors.DarkOrange);
        //}
        #endregion
        #region stop async method from https://stackoverflow.com/questions/15614991/simply-stop-an-async-method
        ////bool playing;
        //bool keepdoing = true;
        //int count = 10;
        //string text;
        //private async void  TgbCommandModeOff_Click(object sender, RoutedEventArgs e)
        //{
        //    if (cts == null)
        //    {
        //        cts = new CancellationTokenSource();
        //        try
        //        {
        //            await DoSomethingAsync("my text", cts.Token);
        //        }
        //        catch (OperationCanceledException)
        //        {
        //        }
        //        finally
        //        {
        //            cts = null;
        //        }
        //    }
        //    else
        //    {
        //        cts.Cancel();
        //        cts = null;
        //    }
        //}

        //private async void DoSomethingAsync()
        //{
        //    playing = true;
        //    for (int i = 0; keepdoing; count++)
        //    {
        //        await DoSomethingAsync(text, token);
        //    }
        //    playing = false;
        //}

        //private async Task DoSomethingAsync(string text, CancellationToken token)
        //{
        //    TgbCommandModeOff.IsChecked = true;
        //    for (int i = 0; ; i++)
        //    {
        //        token.ThrowIfCancellationRequested();
        //        await DoSomethingAsync(text, token);
        //    }
        //    TgbCommandModeOff.IsChecked = false;
        //}
        #endregion

        private void RepeatDispTimer_Tick(object sender, object e)
        {
            Debug.WriteLine("RepeatDispTimer_Tick\n" + "i = " + i.ToString());
            Debug.WriteLine(repeatDispTimer.IsEnabled.ToString());
            Debug.WriteLine(timesToTick.ToString());
            BtnRepeatMediaOutAsync_Click(this, new RoutedEventArgs());
        }

        private async void BtnRepeatMediaOutAsync_Click(object sender, RoutedEventArgs e)
        {         
            if (TgsRepeats.IsOn)
            {
              //  repeatDispTimer.Tick -= RepeatDispTimer_Tick;
              //  TimerSetUp();
                BtnRepeatMediaOutAsync.Visibility = Visibility.Collapsed;
                BtnStopPauseRepeatMediaOutAsync.Visibility = Visibility.Visible;
                stpStatus.Visibility = Visibility.Visible;
                tbStatus.Text = (i + 1).ToString();
                repetitions = Convert.ToInt32(boxRepetitions.Text.Trim());
                if (i == 0)
                {
                    int intervalinMins = Convert.ToInt32(boxInterval.Text.Trim());
                    interval = new TimeSpan(0, intervalinMins, 0);
                    repeatDispTimer.Interval = interval;
                    timesToTick = (repetitions - 1);
                }

                BtnRepeatMediaOutAsync.Foreground = new SolidColorBrush(Windows.UI.Colors.Orange);
                ttsRaw = boxTtsRaw.Text.Trim();
                try
                {
                    await SpeakTextAsync(ttsRaw, uiMediaElement);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message.ToString());
                }

                // Start Repeater Timer
                repeatDispTimer.Start();
                Debug.WriteLine("BtnRepeatMediaOutAsync_Click " + timesToTick.ToString());
                Debug.WriteLine("i = " + i.ToString());

                // Stop timer when reps are complete
                i++;
                if (i > timesToTick)
                {
                    tbStatus.Text = "0";
                    repeatDispTimer.Stop();
                  //  repeatDispTimer.Tick -= RepeatDispTimer_Tick;
                    BtnStopPauseRepeatMediaOutAsync.Visibility = Visibility.Collapsed;
                    BtnRepeatMediaOutAsync.Visibility = Visibility.Visible;
                    BtnRepeatMediaOutAsync.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);                  
                    stpStatus.Visibility = Visibility.Collapsed;
                    i = 0;
                }
                Debug.WriteLine(repeatDispTimer.IsEnabled.ToString());
            }
            else
            {
                //Debug.Write("Hit tgsReapeats.IsOn//when is false");
                BtnRepeatMediaOutAsync.Foreground = new SolidColorBrush(Windows.UI.Colors.Orange);
                ttsRaw = boxTtsRaw.Text.Trim();
                try
                {
                    await SpeakTextAsync(ttsRaw, uiMediaElement);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message.ToString());
                }
            }        
        }

        #region User Speech Settings
        private void cboVoiceGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VoiceGender = cboVoiceGender.SelectedValue.ToString();
        }
        #endregion
        #region Speech
        async Task<IRandomAccessStream> SynthesizeTextToSpeechAsync(string text)
        {
            // Windows.Storage.Streams.IRandomAccessStream
            IRandomAccessStream stream = null;
          
            // Windows.Media.SpeechSynthesis.SpeechSynthesizer
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                //// Select the US English voice.
                //if (VoiceGender == "female")
                //{

                //   // synthesizer.Voice.Gender.voi = Windows.Media.SpeechSynthesis.VoiceGender.Female;
                //    synthesizer.Options.AudioVolume = 100;
                //}

                //Volume Setting
                //synthesizer.Options.AudioVolume = +100;

                // Windows.Media.SpeechSynthesis.SpeechSynthesisStream
                stream = await synthesizer.SynthesizeTextToStreamAsync(text);
            }
            return (stream);
        }

        async Task SpeakTextAsync(string text, MediaElement mediaElement)
        {
            //TODO: ARS use link below to stop async tasks
            //https://stackoverflow.com/questions/15614991/simply-stop-an-async-method

            //if(TgsRepeats.IsOn)
            //{
            //    BtnStopPauseRepeatMediaOutAsync.Visibility = Visibility.Visible;
            //    BtnRepeatMediaOutAsync.Visibility = Visibility.Collapsed;
            //    stpStatus.Visibility = Visibility.Visible;
            //}
            IRandomAccessStream stream = await this.SynthesizeTextToSpeechAsync(text);

            await mediaElement.PlayStreamAsync(stream, true);
        }
        #endregion

        private void BtnStopPauseRepeatMediaOutAsync_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            tbStatus.Text = "0";
            repeatDispTimer.Stop();
            Debug.WriteLine("tnStopPauseRepeatMediaOutAsync_Click: " 
                + repeatDispTimer.IsEnabled.ToString());
            //repeatDispTimer.Tick -= RepeatDispTimer_Tick;
            Debug.WriteLine("\ntnStopPauseRepeatMediaOutAsync_Click\n after -=  : "
                + repeatDispTimer.IsEnabled.ToString()); 
            if(TgsRepeats.IsOn)
            { 
                BtnRepeatMediaOutAsync.Visibility = Visibility.Visible;
                BtnStopPauseRepeatMediaOutAsync.Visibility = Visibility.Collapsed;
                stpStatus.Visibility = Visibility.Collapsed;
            }
            BtnRepeatMediaOutAsync.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);           
        }       
    }
}
