namespace abc.player;

internal interface ISequencer : IDisposable
{
    public void Start();
    public void Stop();
}
