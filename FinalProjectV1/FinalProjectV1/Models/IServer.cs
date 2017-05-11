using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectV1
{
    interface IServer
    {
        void AddNewCar(Car car);
        double CalculateGradeCar(Car car);
       double GetGradeCar(Car car);

        IUser Login(String user, String password);
        IUser AddNewUser(IUser user, UserTypeEnum userType);

        List<Car> Search(Search search);
       List<HistoryItem> GetHistory(Car car);
    
        bool addHistoryItem(Car car, HistoryItem historyItem);
        bool sendMail(IUser user, String message);
        bool UpdateUser(IUser user);
        bool DefineCarForSale(Car car, IUser user);
       String GenerateTempPassword(Car car);
    }
}
