using UnityEngine;

public abstract class GameButton : MonoBehaviour
{
    [SerializeField] private ButtonInfo buttonInfo;
    protected ButtonBorder border;
    protected GameButton connected;
    public ButtonInfo ButtonInfo => buttonInfo;
    public bool IsAvailable => connected == null;

    protected virtual void Awake()
    {
        buttonInfo = GetComponent<ButtonInfo>();
        border = GetComponent<ButtonBorder>();
        if (buttonInfo == null)
        {
            Debug.Log($"Button info is null on obj {gameObject.name}");
            enabled = false;
        }

    }

    protected virtual void Start()
    {
        buttonInfo.Button.onClick.AddListener(OnClick);
    }

    protected virtual void OnDestroy()
    {
        if (buttonInfo != null) buttonInfo.Button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (DataHolder.Instance.isGameEnded) return;
        SoundManager.Instance.PlayOnSelect();
        HandleClick();
    }

    protected abstract void HandleClick();

    public void ConnectWith(GameButton other)
    {
        connected = other;
        other.connected = this;
        OnConnected(other);
        other.OnConnected(this);
    }

    public void Disconnect()
    {
        if (connected == null) return;

        GameButton other = connected;
        connected = null;
        OnDisconnected(other);
        other.Disconnect();
    }

    protected virtual void OnConnected(GameButton other) { }
    protected virtual void OnDisconnected(GameButton other)
    {
        border.Hide();
    }
}
