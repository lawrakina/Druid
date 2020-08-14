using System;
using Enums;
using UnityEngine;
using static UnityEngine.Random;


namespace Model
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _speedRotation = 11f;
        [SerializeField] private float _batteryChargeMax = 20f;
        [SerializeField] private float _percentLowBatteryCharge = 20f;
        [SerializeField] private float _percentFullBatteryCharge = 80f;

        private Light _light;
        private Transform _goFollow;
        private Vector3 _vectorOffset;
        private readonly Color _fullChargeBatteryColor = Color.green;
        private readonly Color _lowChargeBatteryColor = Color.yellow;
        private readonly Color _mediumChargeBatteryColor = Color.black;
        private readonly Color _chargingBatteryColor = Color.blue;
        private Color _currentBatteryColor;

        private readonly Color _lowChargeBarColor = Color.red;
        private readonly Color _normalChargeBarColor = Color.white;
        private Color _currentBarColor;

        #endregion


        #region Properties

        public float BatteryChargeCurrent { get; private set; }

        public float BatteryPercentChargeCurrent => BatteryChargeCurrent / _batteryChargeMax;

        public Color GetColorBattery
        {
            get
            {
                if (!_light.enabled)
                {
                    _currentBatteryColor = _chargingBatteryColor;
                }
                else
                if (BatteryChargeCurrent >= (_percentFullBatteryCharge * 0.01 * _batteryChargeMax))
                {
                    _currentBatteryColor = _fullChargeBatteryColor;
                }
                else if (BatteryChargeCurrent <= (_percentLowBatteryCharge * 0.01 * _batteryChargeMax))
                {
                    _currentBatteryColor = _lowChargeBatteryColor;
                }
                else
                {
                    _currentBatteryColor = _mediumChargeBatteryColor;
                }
                return _currentBatteryColor;
            }
        }

        public Color GetColorBar
        {
            get
            {
                if (BatteryChargeCurrent >= (_percentLowBatteryCharge * 0.01 * _batteryChargeMax))
                    _currentBarColor = _normalChargeBarColor;
                else
                    _currentBarColor = _lowChargeBarColor;
                return _currentBarColor;
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vectorOffset = Transform.position - _goFollow.position;
            BatteryChargeCurrent = _batteryChargeMax;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vectorOffset;
                    Transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vectorOffset;
            Transform.rotation = Quaternion.Lerp(
                Transform.rotation,
                _goFollow.rotation,
                _speedRotation * Time.deltaTime);
        }

        public bool BatteryUsage()
        {
            var result = false;
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                result = true;
            }
            return result;
        }

        public void ChargeBattery()
        {
            if (BatteryChargeCurrent < _batteryChargeMax)
            {
                BatteryChargeCurrent += Time.deltaTime;
            }
        }

        public void ChargeBattery(float delta)
        {
            BatteryChargeCurrent += delta;
            if (BatteryChargeCurrent > _batteryChargeMax)
            {
                BatteryChargeCurrent = _batteryChargeMax;
            }
        }

        #endregion
    }
}