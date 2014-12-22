using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNet.Identity.EF.BigIntId.Tests
{
    [TestClass]
    public class AllTheTests
    {
        private string _appDataDir;

        [TestInitialize]
        public void Init()
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

            using (var ctx = new ApplicationContext())
            {
                ctx.Database.CreateIfNotExists();
            }
        }

        [TestMethod]
        public async Task TestCreateDatabase()
        {
            var uniqueEmail = string.Format("john.doe{0}", Environment.TickCount);

            using (var ctx = new ApplicationContext())
            using (var userManager = new ApplicationUserStore<ApplicationUser>(ctx))
            {
                var user = new ApplicationUser
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

                Assert.IsNotNull(user);
                Assert.AreSame(user.Firstname, "John");
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var ctx = new ApplicationContext())
            {
                ctx.Database.Delete();
            }

            File.Delete(Path.Combine(_appDataDir, "AspNet.Identity.EF.BigIntId.mdf"));
            File.Delete(Path.Combine(_appDataDir, "AspNet.Identity.EF.BigIntId_log.ldf"));
        }
    }
}