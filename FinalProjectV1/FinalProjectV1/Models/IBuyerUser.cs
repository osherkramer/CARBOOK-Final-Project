using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    interface IBuyerUser
    {
        int verifyTempPassword();
        int getCarID();
        bool registerSuccess(bool flag);
        List<HistoryItem> getCarHistory(Car car);
        bool login(String username, String password);
    }
}
