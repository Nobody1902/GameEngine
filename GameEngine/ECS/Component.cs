using Newtonsoft.Json;

namespace GameEngine.ECS;

public class Component
{

    [JsonIgnore]
    public GameObject gameObject { get; init; }

    [JsonIgnore]
    public Transform transform { get { return gameObject.transform; } }

    public Component(GameObject gameObject)
    {
        this.gameObject = gameObject;

        // Subscribe to the gamObject's events
        gameObject.startEvent += Start;
        gameObject.destroyEvent += OnDestroy;
        gameObject.enableEvent += OnEnable;
        gameObject.disableEvent += OnDisable;

        Awake();
    }
    #region Events
    private void Start(object? sender, EventArgs e)
    {
        Start();
    }
    private void OnDestroy(object? sender, EventArgs e)
    {
        OnDestroy();
    }
    private void OnEnable(object? sender, EventArgs e)
    {
        OnEnable();
    }
    private void OnDisable(object? sender, EventArgs e)
    {
        OnDisable();
    }
    #endregion

    public virtual void Awake() { }
    public virtual void Start() { }

    public virtual void Update() { }
    public virtual void OnDestroy() { }
    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
}