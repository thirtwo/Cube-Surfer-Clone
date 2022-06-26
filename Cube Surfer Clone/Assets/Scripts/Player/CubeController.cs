using UnityEngine;

public class CubeController : MonoBehaviour
{
    private int blockIndex = 0;
    private Transform trailRenderer;
    [SerializeField] private GameObject collectParticle;
    [SerializeField] private GameObject collectText;
    private void Awake()
    {
        Physics.gravity = new Vector3(0, -25, 0);
        trailRenderer = GetComponentInChildren<TrailRenderer>().transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if (obj.layer == 6)//collectible Layer
        {
            var collectible = obj.GetComponent<CollectibleCube>();
            if (collectible != null && !collectible.IsCollected)
            {
                collectible.IsCollected = true;
                Collect(collision.transform);
            }
        }
        if (obj.layer == 7) // obstacle layer
        {
            var obstacle = obj.GetComponent<ObstacleCube>();
            if (obstacle != null && !obstacle.IsHit)
            {
                obstacle.IsHit = true;
                if (blockIndex > 0)
                {
                    Drop();
                }
                else
                {
                    GameManager.FinishGame(false);
                }
            }
        }
        if (obj.layer == 8) // finish layer
        {
            var finish = obj.GetComponent<FinishCube>();
            if (finish != null && !finish.IsHit)
            {
                if (blockIndex > 0)
                {
                    finish.IsHit = true;
                    Drop();
                }
                else
                {
                    GameManager.FinishGame(true);
                }
            }
        }
    }
    private void Collect(Transform obj)
    {
        obj.transform.position = transform.position - transform.up * blockIndex++;
        Instantiate(collectParticle, transform.position, Quaternion.identity);
        Instantiate(collectText, transform.position + Vector3.right, Quaternion.identity);
        transform.position += Vector3.up;
        trailRenderer.position += Vector3.down;
        obj.SetParent(transform);
    }
    private void Drop()
    {
        transform.GetChild(transform.childCount - 1).SetParent(null);
        trailRenderer.position += Vector3.up;
        blockIndex--;
    }
}
