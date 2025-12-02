
public class CharacterCreator
{
    private Controller _controller;

    public CharacterCreator(Character character, Controller controller, float maxHP,
        params IPhysicBehaviour[] physicBehaviours)
    {
        character.Initialize(maxHP, physicBehaviours);

        controller.Enable();

        _controller = controller;

        foreach (var behaviour in physicBehaviours)
            behaviour.SetRigitbody(character.Rigidbody);
    }

    public Controller GetCharacterController() => _controller;

    public CharacterView GetCharacterView(params IViewBehaviour[] viewBehaviours) => new CharacterView(viewBehaviours);
}