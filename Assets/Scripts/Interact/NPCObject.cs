using UnityEngine.AI;

public class NPCObject : InteractableObject
{
    public string name;
    public string[] contentList;

    protected override void Interact()
    {
        DialogUI.Instance.Show(name, contentList);
        print("Interacting with NPC");
    }
}
