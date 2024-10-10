public class PickableObject : InteractableObject
{
    public ItemScriptObject itemSO;

    protected override void Interact()
    {
        InventoryManager.Instance.AddItem(itemSO);
        Destroy(this.gameObject);
    }
}
