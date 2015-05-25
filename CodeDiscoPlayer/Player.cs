using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace CodeDiscoPlayer
{
    // vlc --sout "#transcode{acodec=s16l,channels=2,samplerate=44100}:std{access=file,mux=wav,dst=OUTPUT}" INPUT

    public class Player
    {
        private SoundEffectInstance _loopPlayer;
        private SoundEffectInstance _soundPlayer;

        public void Stop()
        {
            _soundPlayer.Stop();
            _loopPlayer.Stop();
        }

        public void Play(Stream stream, Loop loop)
        {
            stream.Position = 0;

            switch (loop)
            {
                case Loop.None:
                    //if (_soundPlayer != null) _soundPlayer.Dispose();
                    _soundPlayer = SoundEffect.FromStream(stream).CreateInstance();
                    _soundPlayer.Play();
                    break;

                case Loop.StartLoop:
                    if(_loopPlayer!=null)_loopPlayer.Dispose();
                    _loopPlayer = SoundEffect.FromStream(stream).CreateInstance();
                    _loopPlayer.IsLooped = true;
                    _loopPlayer.Play();
                    break;

                case Loop.StopLoop:
                    if (_loopPlayer != null) _loopPlayer.Stop(false);
                    break;
            }
        }
    }
}