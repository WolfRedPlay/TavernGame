using Core.Shared;

namespace UI
{
    public class UI_Shelf : UI_Interactable
    {
        public void OpenUI()
        {
            _rootObject.SetActive(true);
        }
    }
}