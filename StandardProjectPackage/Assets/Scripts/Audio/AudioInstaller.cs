using System.ComponentModel;
using Assets.Scripts.Audio;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Assets.Scripts.Music
{
    [CreateAssetMenu(fileName = "MusicInstaller", menuName = "ScriptObject/Playlist", order = 1)]
    public class AudioInstaller : ScriptableObjectInstaller
    {
        public MusicPlaylist[] PlayList;
        public AudioMixer Mixer;
        

        public override void InstallBindings()
        {
            //Music
            Container.Bind<AudioSource>().FromGameObject();
            Container.Bind<MusicPlaylist[]>().FromInstance(PlayList).AsSingle();
            Container.Bind<AudioMixer>().FromInstance(Mixer).AsSingle();
            Container.Bind<IInitializable>().To<MusicPlayer>().AsSingle();
            Container.Bind<ITickable>().To<MusicPlayer>().AsSingle().NonLazy();
            Container.Bind<VolumeController>().AsSingle();
        }
    }
}
