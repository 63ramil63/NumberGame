using UnityEngine;

public class FirstRowButton : GameButton
{
    public int Lane => transform.GetSiblingIndex();

    protected override void HandleClick()
    {
        if (IsAvailable)
        {
            FirstRowButton previous = DataHolder.Instance.selectedButton;
            if (previous != null && previous != this) previous.border.Hide();

            DataHolder.Instance.selectedButton = this;
            border.Show(GameCanvas.Instance.GetLaneColor(Lane));
        }
        else
        {
            Disconnect();
        }
    }

    protected override void OnConnected(GameButton other)
    {
        DataHolder.Instance.selectedButton = null;
    }

    protected override void OnDisconnected(GameButton other)
    {
        base.OnDisconnected(other);
        DataHolder.Instance.selectedButton = null;
    }
}