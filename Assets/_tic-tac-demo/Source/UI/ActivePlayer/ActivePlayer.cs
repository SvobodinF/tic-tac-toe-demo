using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _marker;

    public void SetActive(bool isActive)
    {
        _marker.SetActive(isActive);
    }
}
