using UnityEngine;

public class Hammer : MonoBehaviour, IInventoryItem
{
    public string Name => "Hammer";
    public Sprite Image => _image;
    [SerializeField] private Sprite _image;

    public void OnPickup()
    {
        gameObject.SetActive(false);
        Debug.Log("Hammer picked up.");
    }
}
