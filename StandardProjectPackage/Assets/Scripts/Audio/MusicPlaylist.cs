using System;
using UnityEngine;

namespace Assets.Scripts.Music
{
    [Serializable]
    public class MusicPlaylist
    {
        public string Name;
        public AudioClip[] Tracks;
    }
}
