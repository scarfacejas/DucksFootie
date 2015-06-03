using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using DucksFootie.Entities;

namespace DucksFootie.DataAccess
{
    public class PlayerAccess
    {
        private static int _newPlayerId = -1;
        private static readonly object _lockObject = new object();
        private List<Player> _players = null;
        private ISavable<List<Player>> Savable { get; set; }

        public PlayerAccess(ISavable<List<Player>> savable)
        {
            Savable = savable;
            _players = Savable.Read() ?? new List<Player>();

            if (_newPlayerId != -1) return;

            lock (_lockObject)
                if (_newPlayerId == -1)
                    _newPlayerId = _players.Any() ? _players.Max(p => p.UserId) : 0;
        }

        public int GetNewPlayerId()
        {
            lock (_lockObject)
                return ++_newPlayerId;
        }

        public void Add(Player player)
        {
            Save(player);
        }

        public void Update(Player player)
        {
            Save(player);
        }

        public void Remove(Player player)
        {
            _players.Remove(player);

            Savable.Save(_players);
        }

        public Player Get(int userId)
        {
            var player = _players.SingleOrDefault(p => p.UserId == userId);

            return player;
        }

        public IEnumerable<Player> GetAll()
        {
            return _players.Where(p => p.Active);
        }

        //public static IEnumerable<GamePlayer> GetAllGamePlayers()
        //{
        //    var players = new List<GamePlayer>();

        //    lock (_lockObject)
        //        Players.ForEach(p => players.Add(new GamePlayer { UserId = p.UserId, Name = p.Name, Email = p.Email }));

        //    return players;
        //}

        //private static string _playerPath;

        //public static string PlayerPath
        //{
        //    get
        //    {
        //        if (_playerPath == null)
        //            _playerPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Diagnostics.Process.GetCurrentProcess().ProcessName, PLAYER_FILE);

        //        return _playerPath;
        //    }
        //}

        //private static List<Player> Players
        //{
        //    get { return _players; }
        //}

        private void Save(Player player)
        {
            // If the player already exists just delete - re-adding at the end
            var currentPlayer = _players.SingleOrDefault(p => p.Equals(player));

            if (currentPlayer != null)
                _players.Remove(currentPlayer);

            _players.Add(player);

            Savable.Save(_players);
        }
    }
}
