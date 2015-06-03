using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Common;
using Common.Interfaces;
using DucksFootie.Entities;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace DucksFootie
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            // Set the resolver for API controllers
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Process.GetCurrentProcess().ProcessName);

            if (!Directory.Exists(root))
                Directory.CreateDirectory(root);

            var playerPath = Path.Combine(root, "players.dft");
            var gamePath = Path.Combine(root, "games.dft");

            container.RegisterType<ISavable<List<Player>>, SaveToFile<List<Player>>>(new InjectionConstructor(playerPath));
            container.RegisterType<ISavable<List<Game>>, SaveToFile<List<Game>>>(new InjectionConstructor(gamePath));

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            

            return container;
        }
    }
}