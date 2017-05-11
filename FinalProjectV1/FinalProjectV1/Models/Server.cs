using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Server : IServer
    {
        public bool addHistoryItem(Car car, HistoryItem historyItem)
        {
            throw new NotImplementedException();
        }

        public void AddNewCar(Car car)
        {
            throw new NotImplementedException();
        }

        public IUser AddNewUser(IUser user, UserTypeEnum userType)
        {
            throw new NotImplementedException();
        }

        public double CalculateGradeCar(Car car)
        {
            throw new NotImplementedException();
        }

        public bool DefineCarForSale(Car car, IUser user)
        {
            throw new NotImplementedException();
        }

        public string GenerateTempPassword(Car car)
        {
            throw new NotImplementedException();
        }

        public double GetGradeCar(Car car)
        {
            throw new NotImplementedException();
        }

        public List<HistoryItem> GetHistory(Car car)
        {
            throw new NotImplementedException();
        }

        public IUser Login(string user, string password)
        {
            throw new NotImplementedException();
        }

        public List<Car> Search(Search search)
        {
            throw new NotImplementedException();
        }

        public bool sendMail(IUser user, string message)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
