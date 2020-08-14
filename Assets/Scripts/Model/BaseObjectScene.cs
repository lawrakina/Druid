using UnityEngine;


namespace Model
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Fields

        private int _layer;
        private Color _color;
        private bool _isVisible;

        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public Transform Transform;

        #endregion


        #region Properties

        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        public int Layer
        {
            get => _layer;

            set
            {
                _layer = value;
                AskLayer(transform, value);
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                AskColor(transform, _color);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RendererSetActive(transform);
                if (transform.childCount <= 0) return;
                foreach (Transform t in transform)
                {
                    RendererSetActive(t);
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = GetComponent<Transform>();
        }

        #endregion


        #region Methods

        public void DisableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }

        public void EnableRigidBody(float force)
        {
            EnableRigidBody();
            Rigidbody.AddForce(transform.forward * force);
        }

        public void EnableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
        }

        /// <summary>
        /// Замораживает или размораживает физическую трансформацию объекта
        /// </summary>
        /// <param name="rigidbodyConstraints">Трансформацию которую нужно заморозить</param>
        public void ConstraintsRigidBody(RigidbodyConstraints rigidbodyConstraints)
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.constraints = rigidbodyConstraints;
            }
        }

        public void SetActive(bool value)
        {
            IsVisible = value;

            var tempCollider = GetComponent<Collider>();
            if (tempCollider)
            {
                tempCollider.enabled = value;
            }
        }
        
        public void SetDefault()
        {
            Transform.position = Vector3.zero;
            Transform.rotation = Quaternion.identity;
            Rigidbody.velocity = Vector3.zero;

            var child = GetComponentInChildren<TrailRenderer>();
            if(child)
                child.Clear();
        }

        #endregion

        
        #region PrivateMethods

        private void AskLayer(Transform obj, int lvl)
        {
            obj.gameObject.layer = lvl;
            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskLayer(d, lvl);
            }
        }

        private void RendererSetActive(Transform renderer)
        {
            var component = renderer.gameObject.GetComponent<Renderer>();
            if (component)
                component.enabled = _isVisible;
        }

        private void AskColor(Transform obj, Color color)
        {
            if (obj.TryGetComponent<Renderer>(out var renderer))
            {
                foreach (var curMaterial in renderer.materials)
                {
                    curMaterial.color = color;
                }
            }

            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskColor(d, color);
            }
        }

        #endregion
    }
}