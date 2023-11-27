using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform _bagTransform;
    public List<GameObject> playerBanknoteList = new List<GameObject>();

    private Vector3 banknoteSize;
    public float banknoteSpacing = 0.1f;

    public void AddBanknoteToBag(GameObject banknote)
    {
        banknote.transform.SetParent(_bagTransform, true);
        CalculateObjectSize(banknote);
        float yPos = CalculateNewYPosition();
        banknote.transform.localRotation = Quaternion.identity;
        banknote.transform.localPosition = Vector3.zero;
        banknote.transform.localPosition = new Vector3(0, yPos, 0);
        playerBanknoteList.Add(banknote);
    }

    public float CalculateNewYPosition()
    {
        float newYPos = banknoteSize.y * playerBanknoteList.Count + banknoteSpacing * playerBanknoteList.Count;
        return newYPos;
    }

    private void CalculateObjectSize(GameObject gameObject)
    {
        if (banknoteSize == Vector3.zero)
        {
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            banknoteSize = meshRenderer.bounds.size;
        }
    }
}