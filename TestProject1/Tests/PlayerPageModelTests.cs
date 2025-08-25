using NUnit.Framework;
using FantasyCricketApp.Data;
using FantasyCricketApp.Models;
using FantasyCricketApp.Pages.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace FantasyCricketApp.Tests
{
    [TestFixture]
    public class PlayerPageModelTests
    {
        // Reset repository before each PageModel test
        [SetUp]
        public void ResetRepository()
        {
            var playersField = typeof(PlayerRepository)
                .GetField("Players", BindingFlags.NonPublic | BindingFlags.Static);

            var list = (List<Player>)playersField.GetValue(null);
            list.Clear();
            list.Add(new Player { Id = 1, Name = "Virat Kohli", Team = "RCB", Role = "Batsman", Price = 7.0M });
            list.Add(new Player { Id = 2, Name = "Jasprit Bumrah", Team = "MI", Role = "Bowler", Price = 8.5M });
        }

        [Test]
        public void IndexModel_OnGet_PopulatesPlayers()
        {
            var model = new IndexModel();
            model.OnGet();

            Assert.IsNotNull(model.Players);
            Assert.AreEqual(2, model.Players.Count);
        }

        [Test]
        public void CreateModel_OnPost_InvalidModel_ReturnsPage()
        {
            var model = new CreateModel();
            // Simulate validation failure
            model.ModelState.AddModelError("Name", "Required");

            var result = model.OnPost();
            Assert.IsInstanceOf<PageResult>(result);
        }

        [Test]
        public void CreateModel_OnPost_ValidModel_AddsAndRedirects()
        {
            var model = new CreateModel
            {
                Player = new Player
                {
                    Name = "David Warner",
                    Team = "SRH",
                    Role = "Batsman",
                    Price = 6.0M
                }
            };

            var result = model.OnPost();
            Assert.IsInstanceOf<RedirectToPageResult>(result);

            var redirect = (RedirectToPageResult)result;
            Assert.AreEqual("Index", redirect.PageName);

            var all = PlayerRepository.GetAll();
            Assert.IsTrue(all.Any(p => p.Name == "David Warner"));
        }

        [Test]
        public void EditModel_OnGet_NonExistingId_RedirectsToIndex()
        {
            var model = new EditModel();
            var result = model.OnGet(999);

            Assert.IsInstanceOf<RedirectToPageResult>(result);
            Assert.AreEqual("Index", ((RedirectToPageResult)result).PageName);
        }

        [Test]
        public void EditModel_OnGet_ExistingId_ReturnsPage()
        {
            var model = new EditModel();
            var result = model.OnGet(1);

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Virat Kohli", model.Player.Name);
        }

        [Test]
        public void EditModel_OnPost_InvalidModel_ReturnsPage()
        {
            var model = new EditModel
            {
                Player = new Player { Id = 1 } // Name missing
            };
            model.ModelState.AddModelError("Name", "Required");

            var result = model.OnPost();
            Assert.IsInstanceOf<PageResult>(result);
        }

        [Test]
        public void EditModel_OnPost_ValidModel_UpdatesAndRedirects()
        {
            var model = new EditModel
            {
                Player = new Player
                {
                    Id = 2,
                    Name = "Jasprit Sharma",
                    Team = "MI",
                    Role = "Bowler",
                    Price = 8.5M
                }
            };

            var result = model.OnPost();
            Assert.IsInstanceOf<RedirectToPageResult>(result);
            Assert.AreEqual("Index", ((RedirectToPageResult)result).PageName);

            var updated = PlayerRepository.GetById(2);
            Assert.AreEqual("Jasprit Sharma", updated.Name);
        }

        [Test]
        public void DetailsModel_OnGet_NonExistingId_RedirectsToIndex()
        {
            var model = new DetailsModel();
            var result = model.OnGet(999);

            Assert.IsInstanceOf<RedirectToPageResult>(result);
            Assert.AreEqual("Index", ((RedirectToPageResult)result).PageName);
        }

        [Test]
        public void DetailsModel_OnGet_ExistingId_ReturnsPage()
        {
            var model = new DetailsModel();
            var result = model.OnGet(1);

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Virat Kohli", model.Player.Name);
        }

        [Test]
        public void DeleteModel_OnGet_NonExistingId_RedirectsToIndex()
        {
            var model = new DeleteModel();
            var result = model.OnGet(999);

            Assert.IsInstanceOf<RedirectToPageResult>(result);
            Assert.AreEqual("Index", ((RedirectToPageResult)result).PageName);
        }

        [Test]
        public void DeleteModel_OnGet_ExistingId_ReturnsPageAndLoadsPlayer()
        {
            var model = new DeleteModel();
            var result = model.OnGet(2);

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Jasprit Bumrah", model.Player.Name);
        }

        [Test]
        public void DeleteModel_OnPost_DeletesAndRedirects()
        {
            var model = new DeleteModel();
            var result = model.OnPost(1);

            Assert.IsInstanceOf<RedirectToPageResult>(result);
            Assert.AreEqual("Index", ((RedirectToPageResult)result).PageName);

            Assert.IsNull(PlayerRepository.GetById(1));
        }
    }
}
