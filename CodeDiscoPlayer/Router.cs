using System;
using System.Diagnostics;
using System.IO;

namespace CodeDiscoPlayer
{
    public class Router : IDisposable
    {
        private bool _disposing;
        private readonly Player _player;
        private string _word = "";

        public Router(Player player)
        {
            _player = player;
        }

        private void Play(Stream stream, bool loop)
        {
            _player.Play(stream, loop);
        }

        private void Stop()
        {
            _player.Stop();
        }

        public void RouteKey(char key)
        {
            var routeKey = char.ToLowerInvariant(key);

            Debug.WriteLine("routing key:" + routeKey);

            switch (ScanBuffer(routeKey))
            {
                case Keywords.Var: Play(Resources.rain_01, false); return;
                case Keywords.Function: Play(Resources.looperman_l_0448131_0072104_megapaul_penne_mit_tomatensauce_75bpm_am, true); return;
                case Keywords.Lambda: Play(Resources.wololo, false); return;
            }

            switch (routeKey)
            {
                case '.': Play(Resources.dollarsign, false); return;
                case ';': Play(Resources.semicolon_oh_yeah_1, false); return;
                case '{': Play(Resources.CurleyBracketsLoop, true); return;
                case '}': Stop(); return;
                case '(': Play(Resources.roundbrackets, false); return;
                case ')': Stop(); return;
                case '\r': Stop(); return;
                case '=': Play(Resources.doublequote, false); return;
                case ' ': Play(Resources.highhat, false); return;
                case '"': Play(Resources.singlequote, false); return;
                case '[': Play(Resources.squareBrackets, true); return;
                case ']': Stop(); return;
            }
        }

        private Keywords ScanBuffer(char routeKey)
        {
            _word += routeKey;

            if (_word.Equals("var "))
            {
                _word = ""; return Keywords.Var;
            }

            if (_word.Equals("function "))
            {
                _word = ""; return Keywords.Function;
            }

            if (_word.Equals("=> "))
            {
                _word = ""; return Keywords.Lambda;
            }

            if (routeKey == ' ')
            {
                _word = "";
            }

            return Keywords.None;
        }

        public void Dispose()
        {
            if (_disposing) return;
            _disposing = true;
            _player.Dispose();
        }

        private enum Keywords
        {
            None = 0,
            Var,
            Function,
            Lambda
        }
    }
}

