using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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

namespace remorse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<char, string> morseCodeDictionary = new Dictionary<char, string>
        {
            {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
            {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
            {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
            {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
            {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
            {'Z', "--.."}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
            {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."},
            {'0', "-----"}
        };

        private Dictionary<string, char> morseCodeToAlphabetDictionary = new Dictionary<string, char>
        {
            {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'}, {".", 'E'},
            {"..-.", 'F'}, {"--.", 'G'}, {"....", 'H'}, {"..", 'I'}, {".---", 'J'},
            {"-.-", 'K'}, {".-..", 'L'}, {"--", 'M'}, {"-.", 'N'}, {"---", 'O'},
            {".--.", 'P'}, {"--.-", 'Q'}, {".-.", 'R'}, {"...", 'S'}, {"-", 'T'},
            {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'}, {"-.--", 'Y'},
            {"--..", 'Z'}, {".----", '1'}, {"..---", '2'}, {"...--", '3'}, {"....-", '4'},
            {".....", '5'}, {"-....", '6'}, {"--...", '7'}, {"---..", '8'}, {"----.", '9'},
            {"-----", '0'}
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputBox.Text.ToUpper();

            string morseCode = ConvertToMorseCode(input);

            OutputBox.Text = morseCode;
        }

        private string ConvertToMorseCode(string input)
        {
            string morseCode = "";
            foreach (char c in input)
            {
                if (morseCodeDictionary.ContainsKey(c))
                {
                    morseCode += morseCodeDictionary[c] + " ";
                }
                else if (c == ' ')
                {
                    morseCode += " ";
                }
            }
            return morseCode;
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            string line = OutputBox.Text;
            using (StreamWriter sw = new StreamWriter("SavedFile.txt"))
            {
                sw.Write(line);
            }
            MessageBox.Show("File saved!");
        }

        private void ReadFileButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Text = "";
            List<string> list = new List<string>();
            ReadBtn.IsEnabled = false;
            ConvertBtn.IsEnabled = false;
            SaveBtn.IsEnabled = false;
            PlayBtn.IsEnabled = true;
            using (StreamReader sr = new StreamReader("MorseCodeTest.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }

            foreach (string morseCode in list) 
            {
                InputBox.Text += morseCode + " ";
            }

            string translation = "";
            foreach (string text in list)
            {
                string[] morseWords = text.Split(' ');
                foreach (string morseWordPart in morseWords)
                {
                    if (morseCodeToAlphabetDictionary.ContainsKey(morseWordPart))
                    {
                        translation += morseCodeToAlphabetDictionary[morseWordPart];
                    }
                    else if (morseWordPart == "")
                    {
                        translation += " ";
                    }
                }
                translation += " ";
            }
            OutputBox.Text = translation;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            for (int X = 0; X < InputBox.Text.Length; X++)
            {
                if (InputBox.Text[X] == '.')
                    Console.Beep(3000, 200);
                else if (InputBox.Text[X] == '-')
                    Console.Beep(3000, 500);
                else if (InputBox.Text[X] == ' ')
                    Console.Beep(50, 50);
            }
        }
    }
}
