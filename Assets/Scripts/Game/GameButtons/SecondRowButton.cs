using UnityEngine;

public class SecondRowButton : GameButton
{
    private UIConnection connection;

    protected override void HandleClick()
    {
        FirstRowButton selected = DataHolder.Instance.selectedButton;
        if (selected == null) return;

        if (!IsAvailable) Disconnect();
        ConnectWith(selected);

        CheckWin();
    }

    protected override void OnConnected(GameButton other)
    {
        var first = (FirstRowButton)other;

        DataHolder.Instance.CurrentResult += other.ButtonInfo.Number * ButtonInfo.Number;

        connection = GameCanvas.Instance.CreateConnection(
            (RectTransform)other.transform, (RectTransform)transform, first.Lane);

        border.Show(GameCanvas.Instance.GetLaneColor(first.Lane));
    }

    protected override void OnDisconnected(GameButton other)
    {
        base.OnDisconnected(other);

        DataHolder.Instance.CurrentResult -= other.ButtonInfo.Number * ButtonInfo.Number;

        if (connection != null)
        {
            Destroy(connection.gameObject);
            connection = null;
        }
    }

    private void CheckWin()
    {
        if (GameInitializer.Instance.EndResult != DataHolder.Instance.CurrentResult) return;

        GameTimer.Instance.StopTimer();
        DataHolder.Instance.isGameEnded = true;
        GameCanvas.Instance.SlashSign.SetText("");
        GameCanvas.Instance.RetryButton.gameObject.SetActive(true);

        if (SaveStorage.TrySetBest(GameInfo.CountOfNumbers, GameTimer.Instance.Elapsed))
        {
            // todo win animation
        }
    }
}
