using Page_Navigation_App.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Page_Navigation_App.View
{
    /// <summary>
    /// Interaction logic for Translate.xaml
    /// </summary>
    public partial class Translate : UserControl
    {
        private GoogleTranslate googleTranslate;
        public Translate()
        {
            InitializeComponent();
            googleTranslate = new GoogleTranslate();
        }

        async private void onClickTranslate(object sender, RoutedEventArgs e)
        {

            string originText = txtOriginText.Text;
            if (originText != "")
            {
                string langIn = cbLangIn.Text;
                string langOut = cbLangOut.Text;
                txtTranslateText.Text = "Loading...";

                string[] res = await googleTranslate.TranslateText(originText, langIn, langOut);
                txtTranslateText.Text = res[0];
            }

        }

        private void onClickVoiceIn(object sender, RoutedEventArgs e)
        {
            try
            {
                string originText = txtOriginText.Text;
                string langIn = cbLangIn.Text;
                if (originText != "")
                {
                    try
                    {
                        string voiceLang = langIn == "auto" ? "en" : langIn;
                        googleTranslate.PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(originText) + "&tl=" + voiceLang + "&total=1&idx=0");
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onClickVoiceOut(object sender, RoutedEventArgs e)
        {
            string translateText = txtTranslateText.Text;
            string langOut = cbLangOut.Text;
            if (translateText != "")
            {
                try
                {
                    string voiceLang = langOut == "auto" ? "en" : langOut;
                    googleTranslate.PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(translateText) + "&tl=" + voiceLang + "&total=1&idx=0");
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
