using NUnit.Framework;

namespace Cardinal.PwnedApi.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var client = new PwnedClient();
            var result = client.IsPasswordPwned("admin123");
        }
    }
}