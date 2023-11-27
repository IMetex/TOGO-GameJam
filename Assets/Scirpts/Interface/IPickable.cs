using UnityEngine;

public interface IPickupable
{
    void OnPickup(Transform parent);
    void OnDrop();
}

