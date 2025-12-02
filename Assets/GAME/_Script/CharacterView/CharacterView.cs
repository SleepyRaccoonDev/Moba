
public class CharacterView
{
    private IViewBehaviour[] _viewBehaviour;

    public CharacterView(IViewBehaviour[] viewBehaviour)
    {
        _viewBehaviour = viewBehaviour;
    }

    public void UpdateBehaviors()
    {
        foreach (var behaviour in _viewBehaviour)
            behaviour.Perform();
    }
}