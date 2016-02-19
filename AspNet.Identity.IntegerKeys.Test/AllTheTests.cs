using System;
using System.IO;
using System.Linq;
using System.Reflection;
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
            CleanUpDatabases();

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

            using (var db = new IdentityContextWithIntKey())
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

            using (var db = new IdentityContextCustomUser("DefaultConnection3"))
            {
                db.Database.CreateIfNotExists();
            }
        }

        [TestMethod]
        public void TestCreateDatabase()
        {
            var uniqueEmail = string.Format("john.doe{0}", Environment.TickCount);

            using (var ctx = new IdentityContextWithIntKey())
            using (var userManager = new IdentityUserStoreWithIntKey<IdentityUserWithIntKey>(ctx))
            {
                var user = new IdentityUserWithIntKey
                {
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true
                };

                userManager.CreateAsync(user).Wait();

                ctx.SaveChanges();

                user = userManager.FindByEmailAsync(user.Email).Result;

                Assert.AreEqual(typeof (int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreEqual(1, user.Id);
            }

            using (var ctx = new IdentityContextCustomUser())
            using (var userManager = new IdentityUserStoreWithIntKey<CustomUser>(ctx))
            {
                var user = new CustomUser
                {
                    Firstname = "John",
                    Surname = "Doe",
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true,
                    LastUpdatedUtc = DateTime.Now,
                    SomeDate = DateTime.Now
                };

                userManager.CreateAsync(user).Wait();

                ctx.SaveChanges();

                user = userManager.FindByEmailAsync(user.Email).Result;

                Assert.AreEqual(typeof (int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreSame(user.Firstname, "John");
                Assert.AreEqual(1, user.Id);
                Assert.AreEqual(DateTimeKind.Utc, user.LastUpdatedUtc.Kind);
                Assert.AreEqual(DateTimeKind.Utc, user.SomeDate.Value.Kind);
            }

            using (var ctx = new IdentityContext())
            using (var userManager = new IdentityUserStoreWithIntKey<IdentityUserWithIntKey>(ctx))
            {
                var user = new IdentityUserWithIntKey
                {
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true
                };

                userManager.CreateAsync(user).Wait();

                ctx.SaveChanges();

                user = userManager.FindByEmailAsync(user.Email).Result;

                Assert.AreEqual(typeof (int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreEqual(1, user.Id);
            }

            using (var ctx = new IdentityContextCustomUser("DefaultConnection3"))
            using (var userManager = new IdentityUserStoreWithIntKey<CustomUser>(ctx))
            {
                var user = new CustomUser
                {
                    Firstname = "John",
                    Surname = "Doe",
                    Email = uniqueEmail,
                    EmailConfirmed = true,
                    UserName = uniqueEmail,
                    PhoneNumber = (12345L + Environment.TickCount).ToString(),
                    LockoutEnabled = true,
                    LastUpdatedUtc = DateTime.Now,
                    SomeDate = null
                };

                userManager.CreateAsync(user).Wait();

                ctx.SaveChanges();

                user = userManager.FindByEmailAsync(user.Email).Result;

                Assert.AreEqual(typeof (int), user.Id.GetType());
                Assert.IsNotNull(user);
                Assert.AreSame(user.Firstname, "John");
                Assert.AreEqual(1, user.Id);
                Assert.AreEqual(DateTimeKind.Utc, user.LastUpdatedUtc.Kind);
                Assert.AreEqual(false, user.SomeDate.HasValue);

            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            //CleanUpDatabases();
        }

        private static void CleanUpDatabases(bool rollback = false)
        {
            using (var db = new IdentityContextWithIntKey())
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

            using (var db = new IdentityContextCustomUser("DefaultConnection3"))
            {
                db.Database.Delete();
            }

            //var files = new DirectoryInfo(_appDataDir).EnumerateFiles("*.*");
            //foreach (var f in files)
            //{
            //    File.Delete(f.FullName);
            //}
        }
    }
}