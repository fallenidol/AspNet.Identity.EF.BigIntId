using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNet.Identity.IntegerKeys.Test
{
    [TestClass]
    public class AllTheTests
    {
        private static string _appDataDir;

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            #region find app_data

            var dir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);
            while (_appDataDir == null)
            {
                var dirs = dir.Parent.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);

                var appDataDir =
                    dirs.FirstOrDefault(
                        info => info.Name.Equals("App_Data", StringComparison.InvariantCultureIgnoreCase));

                if (appDataDir != null)
                {
                    _appDataDir = appDataDir.FullName;
                }
                else
                {
                    dir = dir.Parent;
                }
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", _appDataDir);

            #endregion find app_data

            var files = new DirectoryInfo(_appDataDir).EnumerateFiles("*.*");
            foreach (var f in files)
            {
                File.Delete(f.FullName);
            }

            using (var db = new IdentityContextWithIntKeys())
            {
                db.Database.CreateIfNotExists();
            }

            using (var db = new IdentityContext())
            {
                db.Database.CreateIfNotExists();
            }

            using (var db = new IdentityContextCustomUser())
            {
                db.Database.CreateIfNotExists();
            }
        }

        [TestMethod]
        public async Task TestCreateDatabase()
        {
            var uniqueEmail = string.Format("john.doe{0}", Environment.TickCount);

            using (var ctx = new IdentityContextWithIntKeys())
            using (var userManager = new IdentityUserStore<IdentityUser>(ctx))
            {
                var user = new IdentityUser
                {
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true
                };

                await userManager.CreateAsync(user);

                ctx.SaveChanges();

                user = await userManager.FindByEmailAsync(user.Email);

                Assert.AreEqual(typeof(int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreEqual(1, user.Id);
            }

            using (var ctx = new IdentityContextCustomUser())
            using (var userManager = new IdentityUserStore<CustomUser>(ctx))
            {
                var user = new CustomUser
                {
                    Firstname = "John",
                    Surname = "Doe",
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true
                };

                await userManager.CreateAsync(user);

                ctx.SaveChanges();

                user = await userManager.FindByEmailAsync(user.Email);

                Assert.AreEqual(typeof(int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreSame(user.Firstname, "John");
                Assert.AreEqual(1, user.Id);
            }

            using (var ctx = new IdentityContext())
            using (var userManager = new IdentityUserStore<IdentityUser>(ctx))
            {
                var user = new IdentityUser
                {
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true
                };

                await userManager.CreateAsync(user);

                ctx.SaveChanges();

                user = await userManager.FindByEmailAsync(user.Email);

                Assert.AreEqual(typeof(int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreEqual(1, user.Id);
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            using (var db = new IdentityContextWithIntKeys())
            {
                db.Database.Delete();
            }

            using (var db = new IdentityContext())
            {
                db.Database.Delete();
            }

            using (var db = new IdentityContextCustomUser())
            {
                db.Database.Delete();
            }

            var files = new DirectoryInfo(_appDataDir).EnumerateFiles("*.*");
            foreach (var f in files)
            {
                File.Delete(f.FullName);
            }
        }

    }
}