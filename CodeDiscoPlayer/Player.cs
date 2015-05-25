using System;
using System.IO;
using System.Media;

namespace CodeDiscoPlayer
{
    public class Player : IDisposable
    {
        private readonly SoundPlayer _soundPlayer = new SoundPlayer();

        public void Stop()
        {
            _soundPlayer.Stop();
        }

        public void Play(Stream stream, bool loop)
        {
            stream.Position = 0;

            _soundPlayer.Stream = stream;

            if (loop) _soundPlayer.PlayLooping();
            else {_soundPlayer.Play();}
        }

        private bool _disposing;
        public void Dispose()
        {
            if (_disposing) return;
            _disposing = true;
            _soundPlayer.Dispose();
        }
    }
}