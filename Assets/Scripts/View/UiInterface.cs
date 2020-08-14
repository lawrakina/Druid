using UnityEngine;


namespace View
{
    public sealed class UiInterface
    {
        #region Fields

        private FlashLightUiText _flashLightUiText;
        private FlashLightUiBar _flashLightUiBar;
        private WeaponUiText _weaponUiText;
        private SelectionObjMessageUi _selectionObjMessageUi;
        private PlayerXpUiText _playerXpUiText;
        private PlayerXpUiBar _playerXpUiBar;

        #endregion


        #region Properties

        public FlashLightUiText LightUiText
        {
            get
            {
                if (!_flashLightUiText)
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                return _flashLightUiText;
            }
        }

        public FlashLightUiBar FlashLightUiBar
        {
            get
            {
                if (!_flashLightUiBar)
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                return _flashLightUiBar;
            }
        }
        
        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                return _weaponUiText;
            }
        }

        public SelectionObjMessageUi SelectionObjMessageUi
        {
            get
            {
                if (!_selectionObjMessageUi)
                    _selectionObjMessageUi = Object.FindObjectOfType<SelectionObjMessageUi>();
                return _selectionObjMessageUi;
            }
        }
        
        public PlayerXpUiText PlayerXpUiText
        {
            get
            {
                if (!_playerXpUiText)
                    _playerXpUiText = Object.FindObjectOfType<PlayerXpUiText>();
                return _playerXpUiText;
            }
        }
        
        public PlayerXpUiBar PlayerXpUiBar
        {
            get
            {
                if (!_playerXpUiBar)
                    _playerXpUiBar = Object.FindObjectOfType<PlayerXpUiBar>();
                return _playerXpUiBar;
            }
        }


        #endregion
    }
}