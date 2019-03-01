using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MentalPrepApp.Views.UserControls.AppFxs.Nested
{
    public sealed partial class WeatherCommandsBarUserControl : UserControl
    {
        public WeatherCommandsBarUserControl()
        {
            this.InitializeComponent();
        }

        private async void WeatherSearch_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of SpeechRecognizer.
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

            // Listen for audio input issues.
            // speechRecognizer.RecognitionQualityDegrading += speechRecognizer_RecognitionQualityDegrading;

            // Add a web search grammar to the recognizer.
            var webSearchGrammar = new Windows.Media.SpeechRecognition.SpeechRecognitionTopicConstraint(Windows.Media.SpeechRecognition.SpeechRecognitionScenario.WebSearch, "webSearch");

            speechRecognizer.UIOptions.AudiblePrompt = "Say what you want to search for...";
            speechRecognizer.UIOptions.ExampleText = @"Ex. 'weather for London'";
            speechRecognizer.Constraints.Add(webSearchGrammar);

            // Compile the constraint.
            await speechRecognizer.CompileConstraintsAsync();

            // Start recognition.
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
            //await speechRecognizer.RecognizeWithUIAsync();

            // Do something with the recognition result.
            var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
            await messageDialog.ShowAsync();
        }
    }
}
