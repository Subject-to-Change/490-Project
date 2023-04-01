using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTiler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float tileX = 1;
    [SerializeField] private float tileY = 1;
    Mesh mesh;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mesh = GetComponent<MeshFilter>().mesh;
        mat.mainTextureScale = new Vector2((mesh.bounds.size.x *
transform.localScale.x) / 1 * tileX, (mesh.bounds.size.y * transform.localScale.y) / 1 * tileY);
    }

    void Update()
    {

    }
}
