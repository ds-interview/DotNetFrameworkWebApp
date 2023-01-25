using Dot.Repo;
using Dot.Service.RegisterUser;
using Microsoft.Graph;

namespace Dot.Service.Login
{
    
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> _repoPresenter;

        public LoginService(IRepository<User> repoRegister)
        {
            this._repoPresenter = repoRegister;
        }
        public bool GetUser(User r)
        {
         return  _repoPresenter.Query().Filter(x => x.Username == r.Username && x.Password == r.Password).Get().Any();

        }

        public void Insert(User i)
        {
            _repoPresenter.Insert(i);
        }

        public void SaveChanges()
        {
            _repoPresenter.SaveChanges();
        }
    }
}
