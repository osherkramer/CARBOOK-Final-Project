using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class BuyerUser : IBuyerUser
    {
        public List<HistoryItem> getCarHistory(Car car)
        {
            return null;
        }

        public int getCarID()
        {
            throw new NotImplementedException();
        }

        public bool login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool registerSuccess(bool flag)
        {
            throw new NotImplementedException();
        }

        public int verifyTempPassword()
        {
            throw new NotImplementedException();
        }
    }
}
