using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DucksFootie.Entities;
using Common.Interfaces;

namespace DucksFootie.DataAccess
{
    public class GameAccess
    {
        private List<Game> _games = null;
        private ISavable<List<Game>> Savable { get; set; }

        public GameAccess(ISavable<List<Game>> savable)
        {
            Savable = savable;
            _games = Savable.Read() ?? new List<Game>();
        }

        public Game Get(DateTime date)
        {
            return _games.SingleOrDefault(g => g.Date == date);
        }

        public IEnumerable<Game> GetAll()
        {
            return _games;
        }

        public void Save(Game game)
        {
            // If the player already exists just delete - re-adding at the end
            var currentGame = _games.SingleOrDefault(g => g.Equals(game));

            if (currentGame != null)
                _games.Remove(currentGame);

            _games.Add(game);

            Savable.Save(_games);
        }

        public void Remove(Game game)
        {
            var currentGame = _games.SingleOrDefault(g => g.Equals(game));

            if (currentGame != null)
                _games.Remove(currentGame);

            Savable.Save(_games);
        }

        //private static string _gamesPath;

        //public static string GamesPath
        //{
        //    get
        //    {
        //        if (_gamesPath == null)
        //            _gamesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Diagnostics.Process.GetCurrentProcess().ProcessName, GAMES_FILE);

        //        return _gamesPath;
        //    }
        //}

        //private static List<Game> Games
        //{
        //    get { return _games; }
        //}
    }
}
