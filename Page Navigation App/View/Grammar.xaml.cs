using Page_Navigation_App.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Grammar.xaml
    /// </summary>
    public partial class Grammar : UserControl
    {
        private GrammarCheck grammarCheck;
        private GoogleTranslate googleTranslate;
        private List<Match> matchText;
        public Grammar()
        {
            InitializeComponent();
            grammarCheck = new GrammarCheck();
            googleTranslate = new GoogleTranslate();
        }

        async private void OnCheckGrammar(object sender, RoutedEventArgs e)
        {
            string text = txtSpell.Text;
            if (text != "")
            {
                try
                {
                    var res = await grammarCheck.SpellCheck(text);
                    tableSpell.Items.Clear();
                    matchText = res.matches;
                    res.matches.ForEach(e =>
                    {
                        string replace = "";
                        e.replacements.ForEach(r => replace += r.value + " ");
                        tableSpell.Items.Add(new ItemDataGrid { Message = e.message, Replacement = replace });

                    }
                   );

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tableSpell_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var currentRowIndex = tableSpell.Items.IndexOf(tableSpell.CurrentItem);
                Match currentText = matchText[currentRowIndex];
                txtSpell.Focus();
                txtSpell.Select(currentText.offset, currentText.length);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void onClickVoice(object sender, RoutedEventArgs e)
        {
            string text = txtSpell.Text;
            if (text != "")
            {
                try
                {
                    googleTranslate.PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(text) + "&tl=en&total=1&idx=0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }

    class ItemDataGrid
    {
        public string Message { get; set; }
        public string Replacement { get; set; }
    }
}
