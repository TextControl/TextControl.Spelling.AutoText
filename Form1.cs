using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXTextControl;
using TXTextControl.Proofing;

namespace tx_spell_autotext
{
    public partial class Form1 : Form
    {
        // create a new suggestion panel
        SuggestionPanel suggestionPanel = new SuggestionPanel();
        int iDpiX = 15;

        public Form1()
        {
            InitializeComponent();
            iDpiX = (int)(1440 / textControl1.CreateGraphics().DpiX);
            
            // set the panel invisible and it to the Controls
            // collection of TextControl
            suggestionPanel.Visible = false;
            textControl1.Controls.Add(suggestionPanel);
            suggestionPanel.TileClick += suggestionPanel_TileClick;
        }

        // if a suggestion tile is clicked, replace the misspelled word
        // with the selected tile text
        void suggestionPanel_TileClick(object sender, SuggestionPanel.TileClickEventArgs e)
        {
            textControl1.MisspelledWords.Remove(textControl1.MisspelledWords.GetItem(), e.Text);
            suggestionPanel.Visible = false;
        }

        private void DisplaySuggestionPanel()
        {
            MisspelledWord word = textControl1.MisspelledWords.GetItem();

            // return, if there is no misspelled word at the current input position
            if (word == null || textControl1.Selection.Start > word.Start + word.Length)
            {
                suggestionPanel.Visible = false;
                return;
            }

            // create suggestions for the misspelled word
            txSpellChecker1.CreateSuggestions(word.Text, 6);

            if (txSpellChecker1.Suggestions.Count > 0)
            {
                try
                {
                    suggestionPanel.Clear();

                    // create a list of suggestions
                    List<string> lSuggestions = new List<string>();

                    foreach (Suggestion suggestion in txSpellChecker1.Suggestions)
                    {
                        lSuggestions.Add(suggestion.Text);
                    }

                    suggestionPanel.Suggestions = lSuggestions;

                    // calculate the position for the suggestion panel
                    Rectangle wordPosition = textControl1.TextChars[word.Start].Bounds;

                    suggestionPanel.Location = new Point(
                        ((wordPosition.Left - textControl1.ScrollLocation.X) / iDpiX) * textControl1.ZoomFactor / 100,
                        ((wordPosition.Bottom - textControl1.ScrollLocation.Y) / iDpiX) * textControl1.ZoomFactor / 100);

                    // display the panel user control
                    suggestionPanel.Visible = true;
                }
                catch { }
            }
            else
                suggestionPanel.Visible = false;
        }

        private void textControl1_Changed(object sender, EventArgs e)
        {
            DisplaySuggestionPanel();
        }

        private void textControl1_InputPositionChanged(object sender, EventArgs e)
        {
            DisplaySuggestionPanel();
        }

        private void textControl1_Zoomed(object sender, EventArgs e)
        {
            DisplaySuggestionPanel();
        }
    }
}
