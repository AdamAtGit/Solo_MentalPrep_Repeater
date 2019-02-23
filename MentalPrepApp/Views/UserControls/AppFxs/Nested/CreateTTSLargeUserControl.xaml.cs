using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MentalPrepApp.Views.UserControls.AppFxs.Nested
{
    public sealed partial class CreateTTSLargeUserControl : UserControl
    {
        public string CurrentUserName { get; set; }
        public int CurrentUserId { get; set; }
        public int SelectedTitleId { get; set; }
        public int EditTitleId { get; set; }
        public int DeleteTitleId { get; set; }
        //public List<Title> TitleListIds { get; set; }

        //Speech Synth and Recogn
        public string SpeechInputResult { get; set; }

        public CreateTTSLargeUserControl()
        {
            this.InitializeComponent();

            //TODO: ARS-Get user's Name for Interactive.
            CurrentUserName = "Adam";

            // Wire up Start Recognizing Routed Event Handlers
            btnRecognitionTtsRawBigAsync.Click += new RoutedEventHandler(StartRecognizing_Click);

            // Wire up Synthesis Test Plays Routed Event Handlers
            btnTestPlayBig.Click += new RoutedEventHandler(TestPlayBigAsync_Click);

            // Wire up Add Title Save Changes Async to ORMs
            btnAddTitleBigAsync.Click += new RoutedEventHandler(AddTitleBigAsync_Click);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ClearAddBoxesBig_Click(object sender, RoutedEventArgs e)
        {
            boxAddTitleNameBig.Text = "";
            boxTtsRawBig.Text = "";

            boxAddTitleNameBig.IsReadOnly = false;
            boxTtsRawBig.IsReadOnly = false;
            boxAddTitleNameBig.Focus(FocusState.Pointer);
            btnAddTitleBigAsync.IsEnabled = true;
        }

        private async void StartRecognizing_Click(object sender, RoutedEventArgs e)
        {
            //test change for commi
            // Create an instance of SpeechRecognizer.
            var speechRecognizer = new SpeechRecognizer();

            // Set timeout settings.
            speechRecognizer.Timeouts.InitialSilenceTimeout = TimeSpan.FromSeconds(8.0);
            speechRecognizer.Timeouts.BabbleTimeout = TimeSpan.FromSeconds(6.0);
            speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(3.2);

            // Compile the dictation grammar by default.
            await speechRecognizer.CompileConstraintsAsync();

            //Inform user it's now listening
            tbStatus.Text = "...listening";
            SolidColorBrush darkGray = new SolidColorBrush(Colors.DarkGray);
            btnRecognitionTtsRawBigAsync.BorderBrush = new SolidColorBrush(Colors.DarkOrange);
            // Start recognition.
            SpeechRecognitionResult speechRecognitionResult =
                        await speechRecognizer.RecognizeAsync();
            //await speechRecognizer.RecognizeWithUIAsync();



            // Do something with the recognition result.
            SpeechInputResult = speechRecognitionResult.Text;
            boxTtsRawBig.Text += SpeechInputResult;

            //Inform user it has finished listening and to click the Mic to continue
            tbStatus.Text = "listening stopped. Tap the Mic to continue";
            btnRecognitionTtsRawBigAsync.BorderBrush = darkGray;

            //var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
            //await messageDialog.ShowAsync();
        }

        private async void TestPlayBigAsync_Click(object sender, RoutedEventArgs e)
        {
            string ttsRaw = "no Text to Speak of!";
            string tag = (sender as Button).Tag.ToString();

            if (tag == "btnTestPlayBig")
            {
                ttsRaw = boxTtsRawBig.Text.Trim();
            }
            try
            {
                await this.SpeakTextAsync(ttsRaw, this.uiMediaElement);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
            }
            Debug.WriteLine("ttsraw: " + ttsRaw);
        }

        private async void AddTitleBigAsync_Click(object sender, RoutedEventArgs e)
        {
            string ttsRaw;
            string tag = (sender as Button).Tag.ToString();
            if (tag == "btnAddTitleBigAsync")
            {
                #region validation user input checking (if,else if)
                if (String.IsNullOrEmpty(boxAddTitleNameBig.Text.Trim()))
                {

                    ttsRaw = CurrentUserName + "," +
                        "some belive, that one should give the title a name before adding it.";
                    boxAddTitleNameBig.Focus(FocusState.Pointer);
                    try
                    {
                        await this.SpeakTextAsync(ttsRaw, this.uiMediaElement);
                    }
                    catch (Exception ex)
                    { Debug.WriteLine(ex.Message.ToString()); }

                    return;
                }
                else if (String.IsNullOrEmpty(boxTtsRawBig.Text.Trim()))
                {
                    ttsRaw = "Confucious say" + "." +
                        ", there are times when it helps to give it something" +
                        " to speak before adding it.";
                    boxTtsRawBig.Focus(FocusState.Pointer);
                    try
                    {
                        await this.SpeakTextAsync(ttsRaw, this.uiMediaElement);
                    }
                    catch (Exception ex)
                    { Debug.WriteLine(ex.Message.ToString()); }

                    return;
                }
                #endregion
                //using (var context = new PRSappContext())
                //{
                //    var title = new Title
                //    {
                //        //TitleId = Auto Generated - Primary Key                  
                //        TitleName = boxAddTitleNameBig.Text,
                //        TtsRaw = boxTtsRawBig.Text,
                //        UserId = CurrentUserId,
                //        DirPath = "In Database"
                //        //Titles = new List<Title>
                //        //{
                //        //    new Title
                //        //    {
                //        //        TitleName = NewTitleTitleName.Text,
                //        //        TtsRaw = NewTitleTtsRaw.Text
                //        //    },
                //        //}
                //    };
                //    context.Titles.Add(title);
                //    //Async Save 
                //    await context.SaveChangesAsync();

                //    #region refresh Titles and Titles Details of Title just Added
                //    var usersTitles =
                //           from t in context.Titles
                //           where t.UserId == CurrentUserId
                //           orderby t.TitleId descending
                //           select t;

                //    List<Title> refreshedUsersTitles = usersTitles.ToList();
                //    //ShowTitlesListView.ItemsSource = refreshedUsersTitles;

                //    //validation logic
                //    btnAddTitleBigAsync.IsEnabled = false;
                //    #endregion
                //    #region validation logic
                //    boxAddTitleNameBig.IsReadOnly = true;
                //    boxTtsRawBig.IsReadOnly = true;
                //    btnTestPlayBig.Focus(FocusState.Pointer);
                //    #endregion
                //}
                #region  refresh Titles and Title Details of Title just Added
                //TODO: ARS- Get last PK Id Inserted
                //using (var context = new PRSappContext())
                //{
                //    var usersTitle =
                //         (from t in context.Titles
                //              // where t.UserId == CurrentUserId
                //              // select t;

                //          where t.UserId == CurrentUserId
                //          orderby t.TitleId descending
                //          select t);//.FirstOrDefault();

                //    var takeOne = usersTitle.Take(1);
                //    List<Title> refreshedUsersTitles = takeOne.ToList();

                //    //TitleDetailsListView.ItemsSource = refreshedUsersTitles;
                //}
                #endregion
            }
        }
        #region Speech
        async Task<IRandomAccessStream> SynthesizeTextToSpeechAsync(string text)
        {
            // Windows.Storage.Streams.IRandomAccessStream
            IRandomAccessStream stream = null;

            // Windows.Media.SpeechSynthesis.SpeechSynthesizer
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                // Windows.Media.SpeechSynthesis.SpeechSynthesisStream
                stream = await synthesizer.SynthesizeTextToStreamAsync(text);
            }

            return (stream);
        }

        async Task SpeakTextAsync(string text, MediaElement mediaElement)
        {
            IRandomAccessStream stream = await this.SynthesizeTextToSpeechAsync(text);

            await mediaElement.PlayStreamAsync(stream, true);
        }

    }
    /////Ends MainPage partial Class and starts a static 'Top Level'(non-nested) class in same NameSpace
    //This below static class is an extension method for MediaElement
    static class MediaElementExtensions
    {
        public static async Task PlayStreamAsync(
          this MediaElement mediaElement,
          IRandomAccessStream stream,
          bool disposeStream = true)
        {
            // bool is irrelevant here, just using this to flag task completion.
            TaskCompletionSource<bool> taskCompleted = new TaskCompletionSource<bool>();

            // Note that the MediaElement needs to be in the UI tree for events
            // like MediaEnded to fire.
            RoutedEventHandler endOfPlayHandler = (s, e) =>
            {
                if (disposeStream)
                {
                    stream.Dispose();
                }
                taskCompleted.SetResult(true);
            };
            mediaElement.MediaEnded += endOfPlayHandler;

            mediaElement.SetSource(stream, string.Empty);
            mediaElement.Play();

            await taskCompleted.Task;
            mediaElement.MediaEnded -= endOfPlayHandler;
        }
    }
    #endregion
}