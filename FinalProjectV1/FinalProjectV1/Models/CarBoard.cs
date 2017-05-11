using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectV1
{
    public class CarBoard : ICarBoard
    {
        public List<Image> getCarImage(Car car)
        {
            throw new NotImplementedException();
        }

        List<Car> ICarBoard.DisplayBoard(Search search)
        {
            throw new NotImplementedException();
        }

        List<Car> ICarBoard.DisplayFavoriteCars(IUser user)
        {
            throw new NotImplementedException();
        }

        List<Image> ICarBoard.getCarImage(Car car)
        {
            throw new NotImplementedException();
        }

        bool ICarBoard.MarkAsFavoriteČ(Car car, IUser user, bool isFacvorite)
        {
            throw new NotImplementedException();
        }

        bool ICarBoard.SendGeneratedCode(Car car, string code)
        {
            throw new NotImplementedException();
        }
    }
}
