using System;
using DucksFootie.DataAccess;
using DucksFootie.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DucksFootie.Tests
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void Player_Equality_players_with_equal_userIDs_are_equal()
        {
            var player1 = new Player { UserId = 1, Email = "-1@j.com", Name = "user-1" };
            var player2 = new Player { UserId = 1, Email = "2@j.com", Name = "user12" };
            var player3 = new Player { UserId = 2, Email = "2@j.com", Name = "user12" };

            Assert.AreEqual(player1, player2);
            Assert.AreNotEqual(player2, player3);
        }
    }
}
