using UnityEngine;

public interface IPickupable
{
    void OnPickup(GameObject gameObject);
    void OnDrop(GameObject gameObject);
}

