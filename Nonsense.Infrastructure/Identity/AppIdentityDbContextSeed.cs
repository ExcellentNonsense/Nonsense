using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.Identity {

    public class AppIdentityDbContextSeed {

        public static async Task Seed(UserManager<ApplicationUser> userManager) {
            if (!userManager.Users.Any()) {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "Nonsense.Infrastructure.SeedData.ApplicationUsers.csv";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8)) {
                        string line;
                        string password = "Secret_pass#1";

                        while ((line = reader.ReadLine()) != null) {
                            string[] values = line.Split(',');

                            var user = new ApplicationUser {
                                UserName = values[0],
                                Email = values[1]
                            };

                            var result = await userManager.CreateAsync(user, password);
                        }
                    }
                }
            }
        }
    }
}
