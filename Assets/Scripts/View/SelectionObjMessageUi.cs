using UnityEngine;
using UnityEngine.UI;


namespace View
{
    public sealed  class SelectionObjMessageUi : MonoBehaviour
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


        #region Methods

        public string Text
        {
            set => _text.text = $"{value}";
        }

        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        }

        #endregion
    }
}