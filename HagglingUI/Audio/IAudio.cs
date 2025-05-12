namespace HagglingUI.Audio;

public interface IAudio
{
    /// <summary>
    /// Plays an embedded music
    /// Continues playing if it was previously playing or paused
    /// </summary>
    /// <returns>True if it could start playing, False if it was already playing or another error occurred.</returns>
    bool PlayAudio();

    /// <summary>
    /// Pauses the currently playing audio
    /// </summary>
    /// <returns>True if it could pause the audio, False if it's not running or another error occurred.</returns>
    bool PauseAudio();

    /// <summary>
    /// Stops the currently playing audio
    /// This will also reset the audio to the beginning
    /// </summary>
    /// <returns>True if it could stop the audio, False if the audio isn't running or another error occurred.</returns>
    bool StopAudio();
}