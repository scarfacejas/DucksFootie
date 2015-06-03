using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DucksFootie.DataAccess;
using DucksFootie.Entities;
using Common;
using System.IO;
using Common.Interfaces;

namespace DucksFootie.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private ISavable<List<Game>> SavableGame { get; set; }
        private ISavable<List<Player>> SavablePlayer { get; set; }

        public GameController(ISavable<List<Game>> savableGame, ISavable<List<Player>> savablePlayer)
        {
            SavableGame = savableGame;
            SavablePlayer = savablePlayer;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Game/Details/5

        public ActionResult Details(DateTime date)
        {
            var da = new GameAccess(SavableGame);
            var game = da.Get(date);

            return View(game);
        }

        //
        // GET: /Game/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Game/Create

        [HttpPost]
        public ActionResult Create(Game game)
        {
            try
            {
                var da = new GameAccess(SavableGame);
                da.Save(game);

                return RedirectToAction("Details", game.Date);
            }
            catch
            {
                return View();
            }
        }

        [ChildActionOnly]
        public ActionResult ShowPlayers(IEnumerable<GamePlayer> players)
        {
            var da = new PlayerAccess(SavablePlayer);
            var boundPlayer = players ?? da.GetAll();

            //return PartialView("../Player/_PlayersPartial", boundPlayer);
            return PartialView("../Player/_Players", boundPlayer);
        }

        //
        // GET: /Game/Edit/5

        public ActionResult Edit(DateTime date)
        {
            return View();
        }

        //
        // POST: /Game/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Game/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Game/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
