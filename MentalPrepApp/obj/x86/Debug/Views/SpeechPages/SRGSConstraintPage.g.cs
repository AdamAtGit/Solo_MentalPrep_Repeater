﻿#pragma checksum "C:\Users\Flazz\source\repos\FebOn\April_Master\SoloApps\MentalPrepApp\MentalPrepApp\Views\SpeechPages\SRGSConstraintPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E6A871A51C2E0D1345539C7956925716"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MentalPrepApp.Views.SpeechPages
{
    partial class SRGSConstraintPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\SpeechPages\SRGSConstraintPage.xaml line 13
                {
                    this.RootGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // Views\SpeechPages\SRGSConstraintPage.xaml line 78
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element3 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element3).Click += this.MfiNavToSedIb_Click;
                }
                break;
            case 4: // Views\SpeechPages\SRGSConstraintPage.xaml line 70
                {
                    this.resultTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // Views\SpeechPages\SRGSConstraintPage.xaml line 65
                {
                    this.colorRectangle = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 6: // Views\SpeechPages\SRGSConstraintPage.xaml line 67
                {
                    this.colorCircle = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 7: // Views\SpeechPages\SRGSConstraintPage.xaml line 49
                {
                    this.btnRecognizeWithUI = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnRecognizeWithUI).Click += this.RecognizeWithUI_Click;
                }
                break;
            case 8: // Views\SpeechPages\SRGSConstraintPage.xaml line 56
                {
                    this.btnRecognizeWithoutUI = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnRecognizeWithoutUI).Click += this.RecognizeWithoutUI_Click;
                }
                break;
            case 9: // Views\SpeechPages\SRGSConstraintPage.xaml line 60
                {
                    this.listenWithoutUIButtonText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // Views\SpeechPages\SRGSConstraintPage.xaml line 44
                {
                    this.cbLanguageSelection = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.cbLanguageSelection).SelectionChanged += this.cbLanguageSelection_SelectionChanged;
                }
                break;
            case 11: // Views\SpeechPages\SRGSConstraintPage.xaml line 23
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element11 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element11).Click += this.NavBacktoMain_Click;
                }
                break;
            case 12: // Views\SpeechPages\SRGSConstraintPage.xaml line 24
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element12 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element12).Click += this.NavToPlayPage_Click;
                }
                break;
            case 13: // Views\SpeechPages\SRGSConstraintPage.xaml line 25
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element13 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element13).Click += this.NavToManagePage_Click;
                }
                break;
            case 14: // Views\SpeechPages\SRGSConstraintPage.xaml line 26
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element14 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element14).Click += this.NavBack_Click;
                }
                break;
            case 15: // Views\SpeechPages\SRGSConstraintPage.xaml line 27
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element15 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element15).Click += this.NavForward_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

