using Enums;
using Interface;
using Model;
using UnityEngine;
using View;


namespace Controller
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        private FlashLightUiText _flashLightUiText;
        private FlashLightUiBar _flashLightUiBar;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
            _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
        }

        public void Execute()
        {
            _flashLightUiText.Text = _flashLightModel.BatteryChargeCurrent;
            _flashLightUiText.Color = _flashLightModel.GetColorBattery;
            _flashLightUiBar.Fill = _flashLightModel.BatteryPercentChargeCurrent;
            _flashLightUiBar.SetColor(_flashLightModel.GetColorBar);

            if (!IsActive)
            {
                _flashLightModel.ChargeBattery();
                return;
            }

            if (!_flashLightModel.BatteryUsage())
            {
                _flashLightModel.ChargeBattery();
                Off();
            }


            _flashLightModel.Rotation();
        }

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] flashLight)
        {
            if (IsActive) return;
            if (flashLight.Length > 0) _flashLightModel = flashLight[0] as FlashLightModel;
            if (_flashLightModel == null) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On(_flashLightModel);
            _flashLightModel.Switch(FlashLightActiveType.On);
            //_flashLightUiText.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            //_flashLightUiText.SetActive(false);
        }

        public void ChangeBattery(float delta)
        {
            _flashLightModel.ChargeBattery(delta);
        }
        
        #endregion

    }
}