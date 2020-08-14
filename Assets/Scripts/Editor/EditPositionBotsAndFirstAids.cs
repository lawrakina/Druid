using UnityEditor;
using UnityEngine;

namespace Editor
{
    
    [CustomEditor(typeof(EditPositionBotsAndFirstAids))]
    public class EditPositionBotsAndFirstAidsEdit : UnityEditor.Editor
    {
    }

    public class EditPositionBotsAndFirstAids : MonoBehaviour
    {
        #region Fields

        public int _botsCount = 10;
        public int _firstAidsCount = 10;
        public int _mineCount = 10;
        public int _botsOffsetPosition = 5;
        public int _firstAidsOffsetPosition = 5;
        public int _mineOffsetPosition = 5;
        public GameObject _botPrefab;
        public GameObject _firstAidsPrefab;
        public GameObject _minePrefab;

        private Transform _rootPosition;

        #endregion
    }
}
