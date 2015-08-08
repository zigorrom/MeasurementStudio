using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
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

namespace MeasurementStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //recognizer = new SpeechRecognizer();
            //recognizer2 = new SpeechRecognizer();
            //Choices colors = new Choices();
            ////colors.Add(new string[] {  });
            //colors.Add("blue", "red");

            //var commands = new Choices();
            //commands.Add("move", "measure");
            //// Create a GrammarBuilder object and append the Choices object.
            //GrammarBuilder gb = new GrammarBuilder();
            //var gb2 = new GrammarBuilder();

            //gb.Append(colors);

            //gb2.Append(commands);
            //// Create the Grammar instance and load it into the speech recognition engine.
            //Grammar g = new Grammar(gb);
            //Grammar g2 = new Grammar(gb2);

            //recognizer.LoadGrammar(g);
            //recognizer2.LoadGrammar(g2);
            //// Register a handler for the SpeechRecognized event.
            //recognizer.SpeechRecognized +=
            //  new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            //recognizer2.SpeechRecognized += recognizer2_SpeechRecognized;
        }

        //void recognizer2_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    MessageBox.Show("Speech recognized[recognizer2]: " + e.Result.Text);
        //}

        //private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    MessageBox.Show("Speech recognized[recognizer1]: " + e.Result.Text);
        //}
        //SpeechRecognizer recognizer;
        //SpeechRecognizer recognizer2;

    }

}
