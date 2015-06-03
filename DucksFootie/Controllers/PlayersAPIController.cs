using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using http = System.Web.Http;
using mvc = System.Web.Mvc;
using Common;
using DucksFootie.Entities;
using Common.Interfaces;

namespace DucksFootie.Controllers
{
    public class PlayersAPIController : http.ApiController
    {
        private DucksFootie.DataAccess.PlayerAccess PlayerAccess { get; set; }

        public PlayersAPIController(ISavable<List<Player>> savable)
        {
            PlayerAccess = new DucksFootie.DataAccess.PlayerAccess(savable);
        }

        // GET api/<controller>
        //[mvc.HttpGet]
        public IEnumerable<Player> Get()
        {
            return PlayerAccess.GetAll();
        }

        // GET api/<controller>/5
        //[mvc.HttpGet]
        public Player Get(int id)
        {
            return PlayerAccess.Get(id);
        }

        // POST api/<controller>
        //[mvc.HttpPost]
        public HttpResponseMessage Post(Player player)
        {
            player.UserId = PlayerAccess.GetNewPlayerId();
            PlayerAccess.Add(player);

            var response = Request.CreateResponse<Player>(HttpStatusCode.Created, player);

            var uri = Url.Link("DefaultApi", new { id = player.UserId });

            response.Headers.Location = new Uri(uri);

            return response;
        }

        // PUT api/<controller>/5
        //[mvc.HttpPut]
        public void Put(Player player)
        {
            PlayerAccess.Update(player);
        }

        // DELETE api/<controller>/5
        //[mvc.HttpDelete]
        public void Delete(int id)
        {
            var player = Get(id);

            player.Active = false;

            PlayerAccess.Update(player);
        }
    }
}