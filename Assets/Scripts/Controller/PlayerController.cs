

using Helper;
using Interface;
using Model;
using UnityEngine;
using View;

namespace Controller
{
    public sealed class PlayerController : BaseController, IInitialization, IExecute
    {
        #region Fields

        private readonly IMotor _motor;
        private PlayerModel _playerModel;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            base.On();
            _playerModel = Object.FindObjectOfType<PlayerModel>();
        }

        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (_playerModel.Hp <= 0)
            {
                Off();
            }

            _motor.Move();

            UiInterface.PlayerXpUiText.Text = _playerModel.Hp;
            UiInterface.PlayerXpUiBar.Fill = _playerModel.PercentXp;
        }

        #endregion
    }
}