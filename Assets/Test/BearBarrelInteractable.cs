using UnityEngine;

public class BearBarrelInteractable : ZoomInteractable
{
    [SerializeField] GameObject _miniGameUI;


    protected override void Awake()
    {
        base.Awake();

        if (_miniGameUI == null)
        {
            Debug.LogWarning("Bear Interactable " + gameObject.name + " doesn't have the UI!");
            enabled = false;
            return;
        }

        HideUI();

        EventManager.AddListener<PathFinishedEvent>(ShowUI);

    }


    private void ShowUI(PathFinishedEvent evt)
    {
        if (evt.Interactable == this)
        {
            _miniGameUI.SetActive(true);
        }
    }

    private void HideUI()
    {
        _miniGameUI.SetActive(false);
    }


    public void StopInteraction()
    {
        ZoomOut();
        HideUI();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PathFinishedEvent>(ShowUI);

    }
}
