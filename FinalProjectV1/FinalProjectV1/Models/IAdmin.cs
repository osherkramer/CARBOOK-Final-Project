using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    interface IAdmin : IUser
    {
        void changeUsersEmail(IUser user, String email);
        void changeUsersName(IUser user, String firstLastName);
        void changeUsersPassword(IUser user, String password);
        IUser addNewUser(IUser user);
        bool updateOwnerCar(Car car, IUser user);
        List<HistoryItem> getCarHistory(Car car);
        List<Car> searchCar(Search search);
        double updateCarCalculate(Car car);
        void updateAllCarsCalculate();
        bool defineCarForSale(Car car, IUser user);
        void sendMessgetoUser(IUser user, String message);
    }
}
