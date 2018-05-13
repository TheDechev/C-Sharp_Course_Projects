using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class LogicUtils
    {
        public static T EnumValidation<T>(string i_StrToEnum, string i_KeyStr)
        {
            int enumVal = int.Parse(i_StrToEnum);
            T userInput = (T)Enum.ToObject(typeof(T), enumVal);

            if (!Enum.IsDefined(typeof(T), enumVal))
            {
                throw new ValueOutOfRangeException(String.Format("Invalid {0} entered", i_KeyStr).ToString());
            }

            return userInput;
        }

        public static float NumericValueValidation(string i_UserInput, float i_Maximum)
        {
            float validator;

            if(!float.TryParse(i_UserInput,out validator))
            {
                throw new FormatException("Invalid input, not numbers!");
            }
            else if (validator > i_Maximum || validator < 0)
            {
                throw new ValueOutOfRangeException("Value not in the range ", validator, i_Maximum);
            }
            else
            {
                return validator;
            }
        }     
            
    }
}
