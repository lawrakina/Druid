using Controller;
using Controller.TimeRemaining;
using Enums;
using UnityEngine;


namespace Model
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 10;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage;
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _timePutToPool;


        public AmmunitionType Type = AmmunitionType.Bullet;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            _timePutToPool = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
            _timePutToPool.AddTimeRemainingExecute();
            
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition()
        {
            CancelInvoke(nameof(LossOfDamage));

            ToDefault();
            _timePutToPool.RemoveTimeRemainingExecute();
            ServiceLocator.Resolve<PoolController>().PutToPool(this);
        }

        private void ToDefault()
        {
            _curDamage = _baseDamage;
        }

        #endregion
    }
}