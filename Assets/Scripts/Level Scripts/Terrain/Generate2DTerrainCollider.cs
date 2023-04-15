using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate2DTerrainCollider : MonoBehaviour
{
    public Terrain my_terrain;
    public float zPlane = 0f;
 
    private PolygonCollider2D my_collider;
 
    void Start()
    {
        // get collider
        my_collider = GetComponent<PolygonCollider2D> ();
        
        // generate and set collider
        my_collider.SetPath (0, IntersectPlane().ToArray());
    }
 
    /// <summary>
    /// Generates a list of Vector2s representing where the zPlane intersects the terrain.
    /// </summary>
    /// <returns>List of points</returns>
    private List<Vector2> IntersectPlane()
    {
        // get x values
        Vector3 terrainPos = my_terrain.GetPosition();
        TerrainData terrainData = my_terrain.terrainData;
        int pointCount = terrainData.heightmapResolution;
        float pointStepSize = terrainData.size.x/pointCount;
        List<Vector2> intersectPoints = new List<Vector2> (pointCount + 2);
        float minY = 0f;
 
        // perform sampling
        for (int x = 0; x < pointCount; x++) {
            float xPos = x * pointStepSize;
            float y = my_terrain.SampleHeight(new Vector3(xPos + terrainPos.x, 0, zPlane));
            minY = Mathf.Min (minY, y);
            intersectPoints.Add(new Vector2(xPos, y));
        }
 
        minY -= 5f;
 
        // Add botton right and bottom left points to insure convex.
        intersectPoints.Add (new Vector2 (pointCount * pointStepSize, minY));
        intersectPoints.Add (new Vector2 (0, minY));
 
        return intersectPoints;
    }
}
