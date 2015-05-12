using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXTextControl;

namespace tx_spell_autotext
{
    public partial class SuggestionPanel : UserControl
    {
        public SuggestionPanel()
        {
            InitializeComponent();
        }

        public List<string> Suggestions 
        {
            get
            {
                return Suggestions;
            }
            set
            {
                int counter = 0;

                // create a label control for each suggestion
                foreach (string suggestions in value)
                {
                    Label lblSuggestion = new Label();

                    if(counter == 0)
                        lblSuggestion.BackColor = System.Drawing.Color.Green;
                    else
                        lblSuggestion.BackColor = System.Drawing.Color.LightGray;

                    lblSuggestion.ForeColor = System.Drawing.Color.White;
                    lblSuggestion.Location = new System.Drawing.Point(5, 5);
                    lblSuggestion.Margin = new System.Windows.Forms.Padding(0, 5, 5, 0);
                    lblSuggestion.Size = new System.Drawing.Size(134, 30);
                    lblSuggestion.Text = suggestions;
                    lblSuggestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    lblSuggestion.Cursor = Cursors.Hand;

                    // add event handlers
                    lblSuggestion.MouseLeave += lblSuggestion_MouseLeave;
                    lblSuggestion.MouseEnter += lblSuggestion_MouseEnter;
                    lblSuggestion.Click += lblSuggestion_Click;

                    // all the label to the panel
                    flowLayoutPanel1.Controls.Add(lblSuggestion);
                    counter++;
                }
            }
        }

        // trigger event when tile label is clicked
        void lblSuggestion_Click(object sender, EventArgs e)
        {
            TileClick(this, new TileClickEventArgs(((Label)sender).Text));
        }

        // change the background color
        void lblSuggestion_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.Green;
        }

        void lblSuggestion_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).BackColor = Color.LightGray;
        }

        public void Clear()
        {
            flowLayoutPanel1.Controls.Clear();
        }

        // event when tile panels are clicked
        public delegate void TileClickHandler(object sender, TileClickEventArgs e);
        public event TileClickHandler TileClick;

        public class TileClickEventArgs : EventArgs
        {
            public readonly string Text;

            public TileClickEventArgs(string text)
            {
                Text = text;
            }
        }
    }
}
