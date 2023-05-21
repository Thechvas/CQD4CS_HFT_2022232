using ConsoleTools;
using CQD4CS_HFT_2022232.Models;
using System;
using System.Linq;

namespace CQD4CS_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestService rest = new RestService("http://localhost:36286/", typeof(Festival).Name);
            CrudService crud = new CrudService(rest);

            var festivalSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List<Festival>())
                .Add("Create", () => crud.Create<Festival>())
                .Add("Delete", () => crud.Delete<Festival>())
                .Add("Update", () => crud.Update<Festival>())
                .Add("Exit", ConsoleMenu.Close);

            var artistSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List<Artist>())
                .Add("Create", () => crud.Create<Artist>())
                .Add("Delete", () => crud.Delete<Artist>())
                .Add("Update", () => crud.Update<Artist>())
                .Add("Exit", ConsoleMenu.Close);

            var songSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => crud.List<Song>())
                .Add("Create", () => crud.Create<Song>())
                .Add("Delete", () => crud.Delete<Song>())
                .Add("Update", () => crud.Update<Song>())
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Festivals", () => festivalSubMenu.Show())
                .Add("Artists", () => artistSubMenu.Show())
                .Add("Songs", () => songSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();



        }
    }
}
