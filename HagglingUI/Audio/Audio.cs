namespace HagglingUI.Audio;

public class Audio : IAudio
{
    public bool PlayAudio()
    {
        Console.WriteLine("PlayAudio called");
        return false;
    }

    public bool PauseAudio()
    {
        Console.WriteLine("PauseAudio called");
        return false;
    }
    
    public bool StopAudio()
    {
        Console.WriteLine("StopAudio called");
        return false;
    }
}