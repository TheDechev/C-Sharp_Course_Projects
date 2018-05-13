using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class LogicUtils
    {
        public static T EnumParse<T>(string i_StrToEnum)
        {
            int enumVal = int.Parse(i_StrToEnum);
            T userInput = (T)Enum.ToObject(typeof(T), enumVal);

            if (!Enum.IsDefined(typeof(T), enumVal))
            {
                throw new GarageLogic.ValueOutOfRangeException("Wrong Input");
            }

            return userInput;
        }
    }
}
