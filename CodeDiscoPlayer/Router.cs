using System;
using System.Diagnostics;

namespace CodeDiscoPlayer
{
    public class Router 
    {
        private readonly Player _player;
        private readonly Config _config;
        private string _word = "";

        public Router(Player player, Config config)
        {
            _player = player;
            _config = config;
        }

        private void Play(KeyMap map)
        {
            var stream = Resources.ResourceManager.GetStream(map.StreamResourceName);

            _player.Play(stream, map.Loop);
        }

        private void Stop()
        {
            _player.Stop();
        }

        public void RouteKey(char key)
        {
            var routeKey = char.ToLowerInvariant(key).ToString();

            routeKey = ScanBuffer(routeKey);

            Debug.WriteLine("routing key:" + routeKey);

            if (!_config.KeyMaps.ContainsKey(routeKey)) return;

            var map = _config.KeyMaps[routeKey];

            Play(map);
        }

        private string ScanBuffer(string routeKey)
        {
            _word += routeKey;

            if (_word.Equals("var "))
            {
                _word = ""; return "var";
            }

            if (_word.Equals("function "))
            {
                _word = ""; return "function";
            }

            if (routeKey == " ")
            {
                _word = "";
            }

            return routeKey;
        }
    }
}

