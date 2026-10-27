using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DragDropController : ITickable
{
    private Camera camera;
    private DragDropSettings settings;
    private DragDropModel model;

    [Inject]
    public DragDropController(DragDropSettings settings, DragDropModel model, Camera camera = null)
    {
        this.settings = settings;
        this.model = model;

        if (camera == null)
            camera = Camera.main;
        this.camera = camera;
    }

    public void OnButtonDown()
    {
        model.OnButtonDown();
    }

    public void Tick()
    {
        ModelTick();

        TouchPhase? keyInfo = GetKeyInfo();

        if (keyInfo != null)
        {
            switch (keyInfo.Value)
            {
                case TouchPhase.Began:
                    OnButtonDown();
                    break;
                case TouchPhase.Ended:
                    OnButtonUp();
                    break;
                default:
                    break;
            }
        }
    }

    private TouchPhase? GetKeyInfo()
    {
#if UNITY_STANDALONE
    return GetPCInput();
#elif UNITY_EDITOR
        return settings.PCModeInEditor
            ? GetPCInput()
            : GetMobileInput();
#elif UNITY_ANDROID || UNITY_IOS
    return GetMobileInput();
#else
    return null;
#endif
    }

    private TouchPhase? GetPCInput()
    {
        if (Input.GetKeyDown(settings.KeyCodeHold))
            return TouchPhase.Began;

        if (Input.GetKeyUp(settings.KeyCodeHold))
            return TouchPhase.Ended;

        if (Input.GetKey(settings.KeyCodeHold))
            return TouchPhase.Stationary;

        return null;
    }

    private TouchPhase? GetMobileInput()
    {
        if (Input.touchCount > 0)
            return Input.GetTouch(0).phase;

        return null;
    }

    public void ModelTick()
    {
        model.OnTick(camera.ScreenToWorldPoint(Input.mousePosition));
    }

    public void OnButtonUp()
    {
        model.OnButtonUp();
    }
}
