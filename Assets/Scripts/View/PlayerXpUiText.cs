using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public sealed class PlayerXpUiText : MonoBehaviour
    {
        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        #endregion

        
        #region Properties

        public float Text
        {
            set => _text.text = $"{value:0}%";
        }

        public Color Color
        {
            set => _text.color = value;
        }     

        #endregion

        
        #region Methods
        
        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        #endregion
    }
}