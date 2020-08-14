using System.Collections.Generic;
using Interface;
using UnityEngine;


namespace Controller.TimeRemaining
{
    public sealed class TimeRemainingController : IExecute, IFixedExecute
    {
        #region Fields
        
        private readonly List<ITimeRemaining> _timeRemainingsExecute;
        private readonly List<ITimeRemaining> _timeRemainingsFixedExecute;
        
        #endregion

        
        #region ClassLifeCycles

        public TimeRemainingController()
        {
            _timeRemainingsExecute = TimeRemainingExtensions.TimeRemainingsExecute;
            _timeRemainingsFixedExecute = TimeRemainingExtensions.TimeRemainingsFidexExecute;
        }
        
        #endregion

        
        #region IExecute

        public void Execute()
        {
            var time = Time.deltaTime;
            for (var i = 0; i < _timeRemainingsExecute.Count; i++)
            {
                var obj = _timeRemainingsExecute[i];
                obj.CurrentTime -= time;
                if (obj.CurrentTime <= 0.0f)
                {
                    obj?.Method?.Invoke();
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemainingExecute();
                    }
                    else
                    {
                        obj.CurrentTime = obj.Time;
                    }
                }
            }
        }
        
        #endregion

        public void FixedExecute()
        {
            var time = Time.fixedDeltaTime;
            for (var i = 0; i < _timeRemainingsFixedExecute.Count; i++)
            {
                var obj = _timeRemainingsFixedExecute[i];
                obj.CurrentTime -= time;
                if (obj.CurrentTime <= 0.0f)
                {
                    obj?.Method?.Invoke();
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemainingFixedExecute();
                    }
                    else
                    {
                        obj.CurrentTime = obj.Time;
                    }
                }
            }
        }
    }
}