using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoList_.Startup))]
namespace ToDoList_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
