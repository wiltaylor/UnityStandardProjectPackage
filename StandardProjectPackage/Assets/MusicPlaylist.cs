using UnityEngine;
using System.Collections;
using System.Linq;

public class MusicPlaylist : MonoBehaviour
{
    public string Name;
    public AudioClip[] Tracks;

    private int _index = -1;

    public bool IsTrackInList(string trackName)
    {
        return Tracks.Any(t => t.name == trackName);
    }

    public AudioClip GetNextTrack()
    {
        if (Tracks.Length == 0)
            return null;

        _index++;

        if (_index >= Tracks.Length)
            _index = 0;

        return Tracks[0];
    }
}
