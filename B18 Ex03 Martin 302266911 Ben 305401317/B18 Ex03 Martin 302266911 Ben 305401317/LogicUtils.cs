﻿using System;

namespace Ex03.GarageLogic
{
    public static class LogicUtils
    {
        public static T EnumValidation<T>(string i_EnumToStr, string i_KeyStr)
        {
            int enumVal = int.Parse(i_EnumToStr);
            T userInput = (T)Enum.ToObject(typeof(T), enumVal);

            if (!Enum.IsDefined(typeof(T), enumVal))
            {
                throw new ValueOutOfRangeException(string.Format("Invalid {0} entered", i_KeyStr).ToString());
            }

            return userInput;
        }

        public static float NumericValueValidation(string i_InputValue, float i_Maximum)
        {
            float validator;

            if(!float.TryParse(i_InputValue, out validator))
            {
                throw new FormatException("Invalid input, not numbers!");
            }
            else if (validator > i_Maximum || validator < 0)
            {
                throw new ValueOutOfRangeException(string.Format("Value out of range. Maximum amount is {0} ", i_Maximum));
            }
            else
            {
                return validator;
            }
        }

        public static float NumericValueValidation(string i_InputValue)
        {
            float validator;

            if (!float.TryParse(i_InputValue, out validator))
            {
                throw new FormatException("Invalid input, not numbers!");
            }
            else
            {
                return validator;
            }
        }
    }
}
