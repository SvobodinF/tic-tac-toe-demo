using System;

public class Mark
{
    public MarkStatus MarkStatus => _markStatus;
    public event Action<MarkStatus> OnStatusChangedEvent;

    private MarkStatus _markStatus;

    public Mark()
    {
        ChangeMarkStatus(MarkStatus.EMPTY);
    }

    public void ChangeMarkStatus(MarkStatus status)
    {
        if (_markStatus != MarkStatus.EMPTY && status != MarkStatus.EMPTY)
            return;

        _markStatus = status;
        OnStatusChangedEvent?.Invoke(_markStatus);
    }
}
