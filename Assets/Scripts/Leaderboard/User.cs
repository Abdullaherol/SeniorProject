using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Leaderboard.UserModel
{
    public partial class User : UserBase { }

    public class UserBase 
    {
        [Parameter("string", "user", 1)]
        public virtual string User { get; set; }
        [Parameter("uint256", "score", 2)]
        public virtual BigInteger Score { get; set; }
    }
}
