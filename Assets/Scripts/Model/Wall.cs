using Interface;

namespace Model
{
    public sealed class Wall : Environment, ISelectObj
    {
        #region ISelectObj

        public string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}