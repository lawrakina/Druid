using System.Collections.Generic;


namespace Controller.TimeRemaining
{
    public static partial class TimeRemainingExtensions
    {
        #region Fields
        
        private static readonly List<ITimeRemaining> _timeRemainingsExecute = new List<ITimeRemaining>(63);
        private static readonly List<ITimeRemaining> _timeRemainingsFixedExecute = new List<ITimeRemaining>(63);
        
        #endregion
        
        
        #region Properties

        public static List<ITimeRemaining> TimeRemainingsExecute => _timeRemainingsExecute;
        public static List<ITimeRemaining> TimeRemainingsFidexExecute => _timeRemainingsFixedExecute;
        
        #endregion
        
        
        #region Execute

        public static void AddTimeRemainingExecute(this ITimeRemaining value)
        {
            if (_timeRemainingsExecute.Contains(value))
            {
                return;
            }

            value.CurrentTime = value.Time;
            _timeRemainingsExecute.Add(value);
        }

        public static void RemoveTimeRemainingExecute(this ITimeRemaining value)
        {
            if (!_timeRemainingsExecute.Contains(value))
            {
                return;
            }
            _timeRemainingsExecute.Remove(value);
        }
        
        #endregion
        
        
        #region FixedExecute

        public static void AddTimeRemainingFixedExecute(this ITimeRemaining value)
        {
            if (_timeRemainingsFixedExecute.Contains(value))
            {
                return;
            }

            value.CurrentTime = value.Time;
            _timeRemainingsFixedExecute.Add(value);
        }

        public static void RemoveTimeRemainingFixedExecute(this ITimeRemaining value)
        {
            if (!_timeRemainingsFixedExecute.Contains(value))
            {
                return;
            }
            _timeRemainingsFixedExecute.Remove(value);
        }
        
        #endregion
    }
}