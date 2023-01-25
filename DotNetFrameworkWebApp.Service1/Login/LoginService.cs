using System.Linq;
using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.Repo;

namespace DotNetFrameworkWebApp.Service
{

    public class LoginService : ILoginService
    {
        private readonly IRepository<Login> _repoPresenter;

        public LoginService(IRepository<Login> repoRegister)
        {
            this._repoPresenter = repoRegister;
        }
        public bool GetUser(Login r)
        {
            return _repoPresenter.Query().Filter(x => x.UserName == r.UserName && x.Password == r.Password).Get().Any();

        }

        public void Insert(Login i)
        {
            _repoPresenter.Insert(i);
        }

        public void SaveChanges()
        {
            _repoPresenter.SaveChanges();
        }
    }
}
