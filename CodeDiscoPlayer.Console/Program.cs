namespace CodeDiscoPlayer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var router = new Router(new Player());

            while (true)
            {
                var key = System.Console.ReadKey();

                router.RouteKey(key.KeyChar);
            }
        }
    }
}
