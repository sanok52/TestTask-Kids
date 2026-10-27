using UnityEngine;
using VContainer;

public class ScrollMouseController : MonoBehaviour
{
    [SerializeField] private ScrollRoot scrollRoot;
    [SerializeField] private bool pcModeInEditor = true;

    private Camera inputCamera;
    private IObjectResolver objectResolver;
    private DragDropModel dragDropModel;
    private DragDropSettings dragDropSettings;

    [Inject]
    private void Construct(IObjectResolver objectResolver, DragDropModel dragDropModel, DragDropSettings dragDropSettings, Camera camera)
    {
        this.objectResolver = objectResolver;
        this.dragDropModel = dragDropModel;
        this.dragDropSettings = dragDropSettings;

        if (camera == null)
            camera = Camera.main;

        inputCamera = camera;
    }

    private void Update()
    {
        if (scrollRoot == null || inputCamera == null)
            return;

        if (dragDropModel != null && dragDropModel.IsDrag)
        {
            if (scrollRoot.IsScrolling)
                scrollRoot.EndScroll();

            return;
        }

#if UNITY_EDITOR
        if (pcModeInEditor)
        {
            UpdatePCInput();
            return;
        }
#endif

#if UNITY_STANDALONE
    UpdatePCInput();
#elif UNITY_ANDROID || UNITY_IOS
        UpdateMobileInput();
#endif
    }

    private void UpdatePCInput()
    {
        Vector2 mousePos = inputCamera.ScreenToWorldPoint(Input.mousePosition);
        var key = dragDropSettings.KeyCodeHold;

        if (Input.GetKeyDown(key))        
            scrollRoot.TryStartScroll(mousePos);        
        else if (Input.GetKey(key))        
            scrollRoot.UpdateScrollPosition(mousePos);        
        else if (Input.GetKeyUp(key))        
            scrollRoot.EndScroll();        
    }

    private void UpdateMobileInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        Vector2 worldPos = inputCamera.ScreenToWorldPoint(touch.position);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                scrollRoot.TryStartScroll(worldPos);
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                scrollRoot.UpdateScrollPosition(worldPos);
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                scrollRoot.EndScroll();
                break;
        }
    }

}