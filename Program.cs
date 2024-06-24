using System.Speech.Recognition;
using System.Numerics;

SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

string[] words =
[
    "Search for a match",
    "Cancel search for a match",
    "Lock in Brimstone",
    "Lock in Cypher",
    "Lock in Gekko",
    "Lock in Iso",
    "Lock in Jett",
    "Lock in Neon",
    "Exit Lock In"
];

foreach (var word in words)
{
    Console.WriteLine(word);
}
Console.WriteLine("");

Grammar grammar = new Grammar(new GrammarBuilder(new Choices(words)));
recognizer.LoadGrammar(grammar);

recognizer.SpeechRecognized += recognizer_SpeechRecognized;

recognizer.SetInputToDefaultAudioDevice();

recognizer.RecognizeAsync(RecognizeMode.Multiple);

while (true)
{
    Console.ReadLine();
}

async void recognizer_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
{
    Console.WriteLine("Recognized text: " + e.Result.Text + " " + e.Result.Confidence * 100 + "%");
    if (e.Result.Confidence * 100 > 90f)
    {
        if (e.Result.Text == "Exit Lock In") System.Environment.Exit(0);
        if (e.Result.Text == "Search for a match") { await Valorant.Party.StartQueue(); }
        if (e.Result.Text == "Cancel search for a match") { await Valorant.Party.LeaveQueue(); }
        
        string character = e.Result.Text.Remove(0, 8);

        switch (character)
        {
            case "Brimstone":
                await Valorant.Pregame.SelectCharacter(Valorant.Agents.Brimstone);
                await Valorant.Pregame.LockCharacter(Valorant.Agents.Brimstone); 
                break;
            
            case "Cypher":
                await Valorant.Pregame.SelectCharacter(Valorant.Agents.Cypher);
                await Valorant.Pregame.LockCharacter(Valorant.Agents.Cypher); 
                break;
            
            case "Iso":
                await Valorant.Pregame.SelectCharacter(Valorant.Agents.Iso);
                await Valorant.Pregame.LockCharacter(Valorant.Agents.Iso); 
                break;
            
            case "Jett":
                await Valorant.Pregame.SelectCharacter(Valorant.Agents.Jett);
                await Valorant.Pregame.LockCharacter(Valorant.Agents.Jett); 
                break;
            
            case "Neon":
                await Valorant.Pregame.SelectCharacter(Valorant.Agents.Neon);
                await Valorant.Pregame.LockCharacter(Valorant.Agents.Neon); 
                break;
            
            case "Gekko":
                await Valorant.Pregame.SelectCharacter(Valorant.Agents.Gekko);
                await Valorant.Pregame.LockCharacter(Valorant.Agents.Gekko); 
                break;
        }
    }
}
