using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class ProfileViewModel(UsersModel user, int followingCount)
    {
        public UsersModel user { get; set; } = user;
        public int followingCount { get; set; } = followingCount;
    }
}
