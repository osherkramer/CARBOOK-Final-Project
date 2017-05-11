using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApplication1
{
    interface IConstantUser: IUser
    {
        void updateKilometer(Car car, int kilometers);
        void changeCarPrice(Car car, double price);
        void changeCarDescription(String description);
        void changeCarExtras(String extra);
        void changeImage(List<Image> image);
        List<Car> getCars();
        void defineSaleStatus(bool flag);
    }
}
