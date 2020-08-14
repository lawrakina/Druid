using System.Collections.Generic;
using Interface;
using Model;
using UnityEngine;


namespace Controller
{
    public class PoolController 
    {
        #region Fields

        private Dictionary<string, LinkedList<BaseObjectScene>> _poolsDictionary;
        private Transform _deactivatedObjectsParent;

        #endregion


        #region Methods

        public void Init(Transform pooledObjectsContainer)
        {
            // Debug.Log($"PoolController.Init; _poolsDictionary Create new Dictionary<string, LinkedList<BaseObjectScene>>()");
            _deactivatedObjectsParent = pooledObjectsContainer;
            _poolsDictionary = new Dictionary<string, LinkedList<BaseObjectScene>>();
        }
        
        public BaseObjectScene GetFromPool(BaseObjectScene prefab)
        {
            // Debug.Log($"_poolsDictionary: {_poolsDictionary.Count}");
            if (!_poolsDictionary.ContainsKey(prefab.name))
            {
                // Debug.Log($"Create new LinkedList<BaseObjectScene>");
                _poolsDictionary[prefab.name] = new LinkedList<BaseObjectScene>();
            }

            BaseObjectScene result;

            if (_poolsDictionary[prefab.name].Count > 0)
            {
                // Debug.Log($"_poolsDictionary[{prefab.name}].Count = {_poolsDictionary.Count}");
                result = _poolsDictionary[prefab.name].First.Value;
                _poolsDictionary[prefab.name].RemoveFirst();
                result.SetDefault();
                result.SetActive(true);
                // Debug.Log($"return {result.name}");
                return result;
            }

            // Debug.Log($"Create new BaseObjectScene.Instantiate(prefab): {prefab.name}");
            result = BaseObjectScene.Instantiate(prefab);
            result.name = prefab.name;

            return result;
        }

        public void PutToPool(BaseObjectScene target)
        {
            _poolsDictionary[target.name].AddFirst(target);
            target.transform.parent = _deactivatedObjectsParent;
            target.SetActive(false);
            // Debug.Log($"PoolController.PutToPool; pool.Count: {_poolsDictionary[target.name].Count}");
        }

        #endregion
    }
}