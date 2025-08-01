using UnityEngine;

public class TestIneractable : MonoBehaviour, Interactable
{
    public void Interact()
    {
        print("Interacted with " + gameObject.name);
    }
}
