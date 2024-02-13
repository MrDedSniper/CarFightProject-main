using UnityEngine;

public class RaycastShootHitPointTracker : MonoBehaviour
{
    private static Vector3 _lastHitPoint;
    public ParticleSystem hitParticle; // Ссылка на партикл

    public static Vector3 LastHitPoint
    {
        get { return _lastHitPoint; }
    }

    public static void SetLastHitPoint(Vector3 hitPoint)
    {
        _lastHitPoint = hitPoint;
        var instance = FindObjectOfType<RaycastShootHitPointTracker>();
        instance.GetParticleOnHit();
    }

    private void GetParticleOnHit()
    {
        if (hitParticle != null)
        {
            Instantiate(hitParticle, _lastHitPoint, Quaternion.identity);
        }
    }
}