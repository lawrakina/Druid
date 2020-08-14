using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class PlayerXpUiBar : MonoBehaviour
    {
        #region Fields

        private Image _bar;
        

        #endregion

        
        #region UnityMethods

        private void Awake()
        {
            _bar = GetComponent<Image>();
        }

        #endregion


        #region Methods

        public float Fill
        {
            set => _bar.fillAmount = value;
        }
        
        #endregion
    }
}