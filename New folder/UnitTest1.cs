using Xunit;

namespace Azure.TableStorage.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var s = new MessageEntity();

            var xxx = s.Serialize();
        }
    }
}
