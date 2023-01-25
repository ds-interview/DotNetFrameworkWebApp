using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dot.Service.RegisterUser;
using Microsoft.Graph;

namespace Dot.Service.Login
{
   public interface ILoginService
    {
        bool GetUser(User r);
        void SaveChanges();
        void Insert(User i);
    }
}
