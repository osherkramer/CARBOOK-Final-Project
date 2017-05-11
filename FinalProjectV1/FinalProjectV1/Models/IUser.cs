using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectV1
{
    interface IUser
    {
        void changeEmail(String email);
        void changeName(String firstName, String LastName);
        void changePassword(String password);
        List<HistoryItem> getCarHistory(Car car);
    }
}
