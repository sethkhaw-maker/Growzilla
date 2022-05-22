using UnityEngine;

public class DeleteInSeconds : MonoBehaviour
{
    [SerializeField] float _lifetime = 1f;
    void Start() => Invoke(nameof(SelfDestruct), _lifetime);
    void SelfDestruct() => Destroy(this.gameObject);
}
