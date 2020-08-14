using Controller;
using Controller.TimeRemaining;

namespace Model
{
    public sealed class Gun : Weapon
    {
        #region Fields

        

        #endregion


        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            
            var tempAmmunition = ServiceLocator.Resolve<PoolController>().GetFromPool(Ammunition) as Ammunition;
            tempAmmunition.transform.position = _barrel.position;
            tempAmmunition.transform.rotation = _barrel.rotation;
            tempAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemainingExecute();
        }

        #endregion
    }
    
    public sealed class Laser : Weapon
    {
        #region Fields

        

        #endregion


        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            
            var tempAmmunition = ServiceLocator.Resolve<PoolController>().GetFromPool(Ammunition) as Ammunition;
            tempAmmunition.transform.position = _barrel.position;
            tempAmmunition.transform.rotation = _barrel.rotation;
            tempAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemainingExecute();
        }

        #endregion
    }
}