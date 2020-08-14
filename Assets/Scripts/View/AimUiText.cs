using Model;
using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public sealed  class AimUiText : MonoBehaviour
    {
        #region Fields

        private Aim[] _aims;
        private Text _text;
        private int _countPoint;

        #endregion

        
        #region UnityMethods

        private void Awake()
        {
            _aims = FindObjectsOfType<Aim>();
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            foreach (var aim in _aims)
            {
                aim.OnPointChange += UpdatePoint;
            }
        }

        private void OnDisable()
        {
            foreach (var aim in _aims)
            {
                aim.OnPointChange -= UpdatePoint;
            }
        }

        #endregion

        
        #region Methods

        private void UpdatePoint()
        {
            var pointTxt = "очков";
            ++_countPoint;
            if (_countPoint >= 5) pointTxt = "очков";
            else if (_countPoint == 1) pointTxt = "очко";
            else if (_countPoint < 5) pointTxt = "очка";
            _text.text = $"Вы заработали {_countPoint} {pointTxt}";
        }

        #endregion
    }
}