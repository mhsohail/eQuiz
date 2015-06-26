using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eQuiz.Startup))]
namespace eQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
