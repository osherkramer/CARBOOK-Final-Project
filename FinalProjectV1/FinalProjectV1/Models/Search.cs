using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Search
    {
        private String producer;
        private String type;
        private String subType;
        private DateTime startDate;
        private DateTime endDate;
        private GearBoxTypeEnum gear;
        private String City;
        private int startPrice;
        private int endPrice;
        private String text;
        public String getProducer()
        {
            return producer;
        }
        public void setProducer(String producer)
        {
            this.producer = producer;
        }
        public String getType()
        {
            return type;
        }
        public void setType(String type)
        {
            this.type = type;
        }
        public String getSubType()
        {
            return subType;
        }
        public void setSubType(String subType)
        {
            this.subType = subType;
        }
        public DateTime getStartDate()
        {
            return startDate;
        }
        public void setStartDate(DateTime startDate)
        {
            this.startDate = startDate;
        }
        public DateTime getEndDate()
        {
            return endDate;
        }
        public void setEndDate(DateTime endDate)
        {
            this.endDate = endDate;
        }
        public GearBoxTypeEnum getGear()
        {
            return gear;
        }
        public void setGear(GearBoxTypeEnum gear)
        {
            this.gear = gear;
        }
        public String getCity()
        {
            return City;
        }
        public void setCity(String city)
        {
            City = city;
        }
        public int getStartPrice()
        {
            return startPrice;
        }
        public void setStartPrice(int startPrice)
        {
            if (startPrice < 0)
            {
                startPrice = 0;
            }
            this.startPrice = startPrice;
        }
        public int getEndPrice()
        {
            if (endPrice < 0)
            {
                endPrice = 9999999;
            }
            return endPrice;
        }
        public void setEndPrice(int endPrice)
        {
            this.endPrice = endPrice;
        }
        public String getText()
        {
            return text;
        }
        public void setText(String text)
        {
            this.text = text;
        }
    }
}
