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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MentalPrepApp.Views.UserControls.AppFxs.Nested
{
    public sealed partial class VoiceCommandsUserControl : UserControl
    {
        public VoiceCommandsUserControl()
        {
            this.InitializeComponent();
        }

        private static uint HResultPrivacyStatementDeclined = 0x80045509;
                 

        /// <summary>
        /// the HResult 0x8004503a typically represents the case where a recognizer for a particular language cannot
        /// be found. This may occur if the language is installed, but the speech pack for that language is not.
        /// See Settings -> Time & Language -> Region & Language -> *Language* -> Options -> Speech Language Options.
        /// </summary>
        private static uint HResultRecognizerNotFound = 0x8004503a;

        private SpeechRecognizer speechRecognizer;
        private IAsyncOperation<SpeechRecognitionResult> recognitionOperation;
        private ResourceContext speechContext;
        private ResourceMap speechResourceMap;
        private bool isPopulatingLanguages = false;

        private async void StartRecognizing_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of SpeechRecognizer.
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

            // Compile the dictation grammar by default.
            await speechRecognizer.CompileConstraintsAsync();

            // Start recognition.
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();

            // Do something with the recognition result.
            var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
            await messageDialog.ShowAsync();
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

    public class AudioCapturePermissions
    {
        // If no microphone is present, an exception is thrown with the following HResult value.
        private static int NoCaptureDevicesHResult = -1072845856;

        /// <summary>
        /// Note that this method only checks the Settings->Privacy->Microphone setting, it does not handle
        /// the Cortana/Dictation privacy check.
        ///
        /// You should perform this check every time the app gets focus, in case the user has changed
        /// the setting while the app was suspended or not in focus.
        /// </summary>
        /// <returns>True, if the microphone is available.</returns>
        public async static Task<bool> RequestMicrophonePermission()
        {
            try
            {
                // Request access to the audio capture device.
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
                settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
                settings.MediaCategory = MediaCategory.Speech;
                MediaCapture capture = new MediaCapture();

                await capture.InitializeAsync(settings);
            }
            catch (TypeLoadException)
            {
                // Thrown when a media player is not available.
                var messageDialog = new Windows.UI.Popups.MessageDialog("Media player components are unavailable.");
                await messageDialog.ShowAsync();
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                // Thrown when permission to use the audio capture device is denied.
                // If this occurs, show an error or disable recognition functionality.
                return false;
            }
            catch (Exception exception)
            {
                // Thrown when an audio capture device is not present.
                if (exception.HResult == NoCaptureDevicesHResult)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("No Audio Capture devices are present on this system.");
                    await messageDialog.ShowAsync();
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
    }

}
