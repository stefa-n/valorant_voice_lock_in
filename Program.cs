using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Numerics;

if(!OperatingSystem.IsWindows())
    System.Environment.Exit(1);

[DllImport("user32")]
static extern int SetCursorPos(int x, int y);

[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
const int MOUSEEVENTF_LEFTDOWN = 0x02;
const int MOUSEEVENTF_LEFTUP = 0x04;

SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

string[] words =
{
    "Lock in Brimstone",
    "Lock in Cypher",
    "Lock in Gecko",
    "Lock in Iso",
    "Lock in Jett",
    "Lock in Neon",
    "Exit Lock In"
};

Vector2[] positions =
{
    new Vector2(626, 838),
    new Vector2(708, 848),
    new Vector2(796, 842),
    new Vector2(873, 842),
    new Vector2(961, 833),
    new Vector2(1046, 844),
};

Grammar grammar = new Grammar(new GrammarBuilder(new Choices(words)));
recognizer.LoadGrammar(grammar);

recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

recognizer.SetInputToDefaultAudioDevice();

recognizer.RecognizeAsync(RecognizeMode.Multiple);

while (true)
{
    Console.ReadLine();
}

void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
{
    Console.WriteLine("Recognized text: " + e.Result.Text + " " + e.Result.Confidence * 100 + "%");
    if (e.Result.Confidence * 100 > 95f)
    {
        if (e.Result.Text == "Exit Lock In") System.Environment.Exit(0); // checks if the user said the exit keyword

        int charIndex = -1;

        for (int i = 0; i < words.Length; i++)
        {
            if (e.Result.Text == words[i])
                charIndex = i;
        }
        
        SetCursorPos((int)positions[charIndex].X, (int)positions[charIndex].Y);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        
        System.Threading.Thread.Sleep(100);
        
        SetCursorPos(942, 730);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        
        System.Environment.Exit(0);
    }
}