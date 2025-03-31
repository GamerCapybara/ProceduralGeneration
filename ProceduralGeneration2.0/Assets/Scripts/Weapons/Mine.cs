using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private float timeToLive = 2f;
    public GameObject Explosion;

    private void Start()
    {
        StartCoroutine(Boom());
    }

    private IEnumerator Boom()
    {
        yield return new WaitForSeconds(timeToLive);
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
