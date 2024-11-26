using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*from gpt asking: I am creating a character in unity 3d editor but it is a 2d character. I create a tilemap already and the player has a 2d rigidbody and a 2d box collider.
I want to make sure that the player can't leave the tilemap bounds. How would I do this?*/
public class ConstrainPlayerWithinBounds : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector2 minBounds; // Minimum bounds (bottom-left corner)
    private Vector2 maxBounds; // Maximum bounds (top-right corner)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateBounds();
    }

    void CalculateBounds()
    {
        // Get the cell bounds of the tilemap
        BoundsInt cellBounds = tilemap.cellBounds;

        // Convert the cell bounds to world space
        Vector3 worldMin = tilemap.CellToWorld(cellBounds.min);
        Vector3 worldMax = tilemap.CellToWorld(cellBounds.max);

        // Set min and max bounds
        minBounds = new Vector2(worldMin.x, worldMin.y);
        maxBounds = new Vector2(worldMax.x, worldMax.y);
    }

    void LateUpdate()
    {
        // Get the player's current position
        Vector2 position = rb.position;

        // Clamp the position within the calculated bounds
        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        // Update the player's position
        rb.position = position;
    }
}

