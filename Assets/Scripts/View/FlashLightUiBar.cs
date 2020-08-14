using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public sealed class FlashLightUiBar : MonoBehaviour
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

        public void SetActive(bool value)
        {
            _bar.gameObject.SetActive(value);
        }
		
        public void SetColor(Color col)
        {
            _bar.color = col;
        }

        #endregion
    }
}