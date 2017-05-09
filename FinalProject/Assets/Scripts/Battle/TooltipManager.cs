using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    [SerializeField]
    Text tooltip;
    [SerializeField]
    Vector3 tooltipMouseOffset;

    void Update()
    {
        tooltip.transform.position = Input.mousePosition + tooltipMouseOffset;
    }
}
