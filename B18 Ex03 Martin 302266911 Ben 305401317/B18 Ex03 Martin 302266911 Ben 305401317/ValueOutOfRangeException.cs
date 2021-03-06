﻿using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get
            {
                return this.m_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return this.m_MinValue;
            }
        }

        public ValueOutOfRangeException() : base("The amount exceeds the maximum range")
        {
        }

        public ValueOutOfRangeException(string i_Message) : base(i_Message)
        {
        }

        public ValueOutOfRangeException(string i_Message, float i_CurrentAmount, float i_MaxAmount)
            : base(string.Format("{0}. Avalilable amount to add is: {1} ", i_Message, Math.Abs(i_MaxAmount - i_CurrentAmount)))
        {
            this.InitializeValues(i_CurrentAmount, i_MaxAmount);
        }

        public ValueOutOfRangeException(float i_CurrentAmount, float i_MaxAmount) :
            base(string.Format("The amount exceeds the avalilable amount to add: {1} ", i_CurrentAmount, Math.Abs(i_MaxAmount - i_CurrentAmount)))
        {
            this.InitializeValues(i_CurrentAmount, i_MaxAmount);
        }

        private void InitializeValues(float i_CurrentAmount, float i_MaxAmount)
        {
            this.m_MaxValue = i_MaxAmount;
            this.m_MinValue = i_CurrentAmount;
        }
    }
}
