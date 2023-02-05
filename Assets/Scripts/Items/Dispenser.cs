using UnityEngine;

public class Dispenser : Item
{
    [SerializeField] private GameObject seedPrefab;

    protected override void Interact()
    {
        dispenseSeed();
    }

    private void dispenseSeed()
    {
        Instantiate(seedPrefab, this.transform.position + Vector3.up * 2, Quaternion.identity);
    }
}
