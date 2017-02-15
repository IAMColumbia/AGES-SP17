

using UnityEngine;

public interface IHeavyExplodableObject 
{
    // Interface for heavy (high mass) rigidbodies that need to explode.
    // We have to use a toned down explosion for general cases, and this high force for
    // heavy object cases.
    void Explode(Vector3 incomingProjectileDirection, Vector3 pointOfExplosionOrigin, float explosionradius);
}
