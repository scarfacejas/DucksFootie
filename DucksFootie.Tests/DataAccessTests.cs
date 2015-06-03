using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Interfaces;
using DucksFootie.DataAccess;
using DucksFootie.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DucksFootie.Tests
{
    [TestClass]
    public class DataAccessTests
    {
        private ISavable<List<Game>> _savableGames;
        private ISavable<List<Player>> _savablePlayers;

        [TestInitialize]
        public void SetUp()
        {
            _savableGames = new SaveToFile<List<Game>>("savableTestGames.dft");
            _savablePlayers = new SaveToFile<List<Player>>("savableTestPlayers.dft");
        }

        [TestCleanup]
        public void CleanUp()
        {
            _savableGames.Delete();
            _savablePlayers.Delete();
        }

        [TestMethod]
        public void Game_should_save_game()
        {
            var game = Helper.CreateGames(3, 2);

            var da = new GameAccess(_savableGames);

            Game game0 = game[0];

            da.Save(game0);

            Game returnedGame = da.Get(game0.Date);

            Assert.AreSame(game0, returnedGame);
        }

        [TestMethod]
        public void Game_should_save_games_then_new_game()
        {
            var games = Helper.CreateGames(4, 3);

            var da = new GameAccess(_savableGames);

            games.ForEach(da.Save);

            var gameCount = da.GetAll().Count();

            Assert.AreEqual(4, gameCount);
        }

        [TestMethod]
        public void UpdatePlayer_overwrites_player_rather_than_duplicating()
        {
            Player player1 = Helper.CreatePlayer(1);
            var originalName = player1.Name;

            var da = new PlayerAccess(_savablePlayers);

            da.Add(player1);

            var player2 = Helper.CreatePlayer(1);
            var updateName = originalName + "_updated";

            player2.Name = updateName;

            da.Add(player2);

            Assert.AreNotEqual(player1.Name, player2.Name);

            var returnedPlayer = da.Get(1);

            Assert.AreEqual(updateName, returnedPlayer.Name);
            Assert.AreEqual(1, da.GetAll().Count(p => p.UserId == 1));
        }

        [TestMethod]
        public void RemovedPlayer_should_remove_player()
        {
            Player player1 = Helper.CreatePlayer(1);
            Player player2 = Helper.CreatePlayer(2);
            Player player3 = Helper.CreatePlayer(3);

            var da = new PlayerAccess(_savablePlayers);

            da.Add(player1);
            da.Add(player2);
            da.Add(player3);

            da.Remove(player2);

            Assert.AreEqual(2, da.GetAll().Count());
            Assert.IsFalse(da.GetAll().Any(p => p.UserId == 2));
            Assert.IsNull(da.Get(2));
        }

        [TestMethod]
        public void AddPlayer_should_add_player()
        {
            Player player = Helper.CreatePlayer(45);

            var da = new PlayerAccess(_savablePlayers);

            da.Add(player);

            Player returnedPlayer = da.Get(45);

            Assert.AreEqual(player, returnedPlayer);
        }

        private static class Helper
        {
            public static List<Game> CreateGames(int numGames, int numPlayers)
            {
                var games = new List<Game>();

                for (int i = 0; i < numGames; i++)
                {
                    var game = CreateGame(numPlayers, i);

                    games.Add(game);
                }

                return games;
            }

            public static Game CreateGame(int numPlayers, int dateOffset)
            {
                var game = new Game { Date = DateTime.Today.AddDays(dateOffset) };
                var players = new List<GamePlayer>();

                for (int i = 0; i < numPlayers; i++)
                {
                    var player = CreatePlayer(i);
                    players.Add(player);
                }

                game.Players = players;

                return game;
            }

            public static GamePlayer CreatePlayer(int playerId)
            {
                var name = string.Format("player{0}", playerId);
                var status = playerId % 2 == 0 ? Status.Accepeted : Status.Declined;
                var player = new GamePlayer { Email = string.Format("{0}@test.com", name), Name = name, UserId = playerId, Playing = status };

                return player;
            }
        }
    }
}
