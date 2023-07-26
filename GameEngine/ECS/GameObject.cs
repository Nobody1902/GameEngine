using Newtonsoft.Json;

namespace GameEngine.ECS;

public class GameObject
{
    public string name { get; private set; }
    public Guid uuid { get; init; }
    public bool enabled { get; private set; }

    [JsonIgnore]
    public bool _destroyed { get; private set; } = false;

    public event EventHandler? startEvent;
    public event EventHandler? enableEvent;
    public event EventHandler? disableEvent;
    public event EventHandler? destroyEvent;

    [JsonIgnore]
    public Transform transform { get; private set; }
    public HashSet<Component> _components { get; private set; }

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
    public GameObject(string name, Guid uuid)
    {
        this.name = name;
        this.uuid = uuid;
        this.enabled = true;

        _components = new();

        transform = AddComponent<Transform>();
    }
    public GameObject(string name, Guid uuid, bool enabled)
    {
        this.name = name;
        this.uuid = uuid;
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
        if(HasComponent<T>())
        {
            return GetComponent<T>();
        }
        T component = (T)Activator.CreateInstance(typeof(T), new object[] { this }) ?? throw new Exception("Error adding component.");
        bool added = _components.Add(component);
        if(added == false)
        {
            throw new Exception($"{name} already has {typeof(T).Name}");
        }
        return component;
    }
    public T GetComponent<T>() where T : Component
    {
        if(_components == null)
        {
            _components = new();
            transform = AddComponent<Transform>();
        }

        foreach (Component component in _components)
        {
            if (component is T comp)
            {
                return comp;
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
    public void SetName(string name)
    {
        this.name = name;
    }
    public void Destroy()
    {
        _destroyed = true;

        OnDestroy();
    }
    public override string ToString()
    {
        string str = $$"""GameObject({{name}}:{{enabled}}){ """;
        return str + _components.ToString() + "}";
    }

    public static GameObject FindUUID(Guid uuid)
    {


        foreach(GameObject obj in Scene.Current.gameObjects)
        {
            if(obj.uuid == uuid)
            {
                return obj;
            }
        }

        return null;
    }
}
