using Cardinal.AspNetCore.Utils;

namespace Cardinal.AspNetCore.DemoApi
{
    public class Program : BaseProgram
    {
        public static void Main(string[] args)
        {
            Initialize<Startup>("1.0.0.0", "Sample App",  args);
        }
    }
}
