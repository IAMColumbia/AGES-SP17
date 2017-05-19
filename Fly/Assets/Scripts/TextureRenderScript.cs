using UnityEngine;

public class TextRenderScript : MonoBehaviour
{
    //[SerializeField]
    public Texture[] textures;
    //[SerializeField]
    public float changeInterval = 0.33F;
    // [SerializeField]
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (textures.Length == 0)
            return;

        int index = Mathf.FloorToInt(Time.time / changeInterval);
        index = index % textures.Length;
        rend.material.mainTexture = textures[index];
    }
}