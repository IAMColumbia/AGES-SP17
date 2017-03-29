using UnityEngine;
using System.Collections;

public class VertFix : MonoBehaviour {

    [SerializeField]
    FireControlVertebraeEnemy target;

    public void ShootParallel()
    {
        target.ShootParallel();
    }

    public void ShootAngled()
    {
        target.ShootAngled();
    }
}
