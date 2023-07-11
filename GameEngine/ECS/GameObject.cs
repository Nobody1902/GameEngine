namespace GameEngine.ECS;

public class GameObject
{
    public string name { get; private set; }
    public Guid uuid { get; init; } 
    public bool enabled { get; private set; }
    public bool _destroyed = false;

    public event EventHandler? startEvent;
    public event EventHandler? enableEvent;
    public event EventHandler? disableEvent;
    public event EventHandler? destroyEvent;

    public Transform transform { get; init; }

    private HashSet<Component> _components { get; init; }

    public GameObject(string name)
    {
        this.name = name;
        this.uuid = Guid.NewGuid();
        this.enabled = true;
        _components = new();

        transform = AddComponent<Transform>();
    }
    public GameObject(string name, bool enabled)
    {
        this.name = name;
        this.uuid = Guid.NewGuid();
        this.enabled = enabled;

        _components = new();

        transform = AddComponent<Transform>();
    }

    public override bool Equals(object? obj)
    {
        if(obj == null || obj is not GameObject) return false;
        return ((GameObject)obj).uuid == this.uuid;
    }

    #region Events
    protected virtual void OnStart()
    {
        startEvent?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void OnEnable()
    {
        enableEvent?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void OnDisable()
    {
        disableEvent?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void OnDestroy()
    {
        destroyEvent?.Invoke(this, EventArgs.Empty);
    }
    #endregion

    public void Update()
    {
        foreach (var component in _components)
        {
            component.Update();
        }
    }

    public T AddComponent<T>() where T : Component
    {
        T component = (T)Activator.CreateInstance(typeof(T), new object[] { this }) ?? throw new Exception("Error adding component.");
        _components.Add(component);
        return component;
    }
    public T GetComponent<T>() where T : Component
    {
        foreach (Component component in _components)
        {
            if (component is T)
            {
                return (T)component;
            }
        }
        throw new Exception($"{name} has no component {typeof(T).Name}");
    }

    public bool HasComponent<T>() where T : Component
    {
        foreach (Component component in _components)
        {
            if (component is T)
            {
                return true;
            }
        }
        return false;
    }

    public void SetEnabled(bool enabled)
    {
        this.enabled = enabled;
        if (this.enabled)
        {
            OnEnable();
        }
        else
        {
            OnDisable();
        }
    }
    public void Destroy()
    {
        _destroyed = true;
        OnDestroy();

    }
}
