using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Drawing;


namespace ConsoleApplication1
{
    interface ICarBoard
    {
        List<Car> DisplayBoard(Search search);
        bool MarkAsFavoriteČ(Car car, IUser user, bool isFavorite);
        List<Car> DisplayFavoriteCars(IUser user); // How to base on cookie?
        bool SendGeneratedCode(Car car, String code);
       List<Image> getCarImage(Car car);
    }
}
