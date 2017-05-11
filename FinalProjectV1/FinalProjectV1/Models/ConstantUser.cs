using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FinalProjectV1
{
    class ConstantUser : IConstantUser
    {
        void IConstantUser.changeCarDescription(string description)
        {
            throw new NotImplementedException();
        }

        void IConstantUser.changeCarExtras(string extra)
        {
            throw new NotImplementedException();
        }

        void IConstantUser.changeCarPrice(Car car, double price)
        {
            throw new NotImplementedException();
        }

        void IUser.changeEmail(string email)
        {
            throw new NotImplementedException();
        }

        void IConstantUser.changeImage(List<Image> image)
        {
            throw new NotImplementedException();
        }

        void IUser.changeName(string firstName, string LastName)
        {
            throw new NotImplementedException();
        }

        void IUser.changePassword(string password)
        {
            throw new NotImplementedException();
        }

        void IConstantUser.defineSaleStatus(bool flag)
        {
            throw new NotImplementedException();
        }

        List<HistoryItem> IUser.getCarHistory(Car car)
        {
            throw new NotImplementedException();
        }

        List<Car> IConstantUser.getCars()
        {
            throw new NotImplementedException();
        }

        void IConstantUser.updateKilometer(Car car, int kilometers)
        {
            throw new NotImplementedException();
        }
    }
}
