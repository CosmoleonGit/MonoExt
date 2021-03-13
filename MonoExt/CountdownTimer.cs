using System;
using System.Collections.Generic;
using System.Text;

namespace MonoExt
{
    public struct CountdownTimer
    {
        public readonly float maxTime;
        public float Time { get; private set; }

        public CountdownTimer(float _maxTime, bool atBeginning = true)
        {
            maxTime = _maxTime;

            if (atBeginning)
                Time = maxTime;
            else
                Time = 0;
        }

        public void Reset() => Time = maxTime;

        public void SetToEnd() => Time = 0;

        public bool Ended => Time == 0;
        
        public bool IfEndedReset()
        {
            bool ret = Ended;
            if (ret) Reset();
            return ret;
        }

        public void Decrement(float amount)
        {
            Time -= amount;
            if (Time < 0) Time = 0;
        }
    }
}
