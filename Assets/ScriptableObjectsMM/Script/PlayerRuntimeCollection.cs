using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new RuntimeCofllection", menuName = ("ScriptableObjects/RuntimeCollections/Players"),order =(3))]
public class PlayerRuntimeCollection : ScriptableObject
{
    private List<PlayerController> collection = new List<PlayerController>();
    public List<PlayerController> GetPlayerList()
    {
        return collection;
    }
    public void AddToCollection(PlayerController _player)
    {
        collection.Add(_player);
    }
    public void RemoveFromCollection(PlayerController _player)
    {
        collection.Remove(_player);
    }
    public void ClearCollection()
    {
        for (int i = 0; i < collection.Count; i++)
        {
            Destroy(collection[i].gameObject);
            collection.Remove(collection[i]);
        }
    }
}
