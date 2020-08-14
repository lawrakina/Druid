using Model;
using View;

namespace Controller
{
    public abstract class BaseController
    {
        #region Properties

        protected UiInterface UiInterface { get; }
        
        public bool IsActive { get; private set; }
        
        #endregion

        
        protected BaseController()
        {
            UiInterface = new UiInterface();
        }


        #region Methods

        

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(params BaseObjectScene[] obj)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch(params BaseObjectScene[] obj)
        {
            if (!IsActive)
            {
                On(obj);
            }
            else
            {
                Off();
            }
        }

        #endregion
    }
}