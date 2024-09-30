public class PickableObject : InteractableObject
{
    public ItemScriptObject itemSO;

    protected override void Interact()
    {
        print("Interacting with pickableobject");
    }
}
