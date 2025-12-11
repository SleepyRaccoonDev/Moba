
public class CharacterCreator
{
    public Controller Create(Character character, Controller controller, float maxHP, float ForceMove, float forceRotate)
    {
        character.Initialize(maxHP, new PhysicMover(character.Rigidbody, ForceMove), new PhysicRotator(character.Rigidbody, forceRotate));

        controller.Enable();

        return controller;
    }

    public CharacterView GetCharacterView(params IViewBehaviour[] viewBehaviours) => new CharacterView(viewBehaviours);
}