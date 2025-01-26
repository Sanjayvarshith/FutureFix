using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    // Indicator icon
    public Image img;
    // The target (location, enemy, etc..)
    public Transform target;
    // UI Text to display the distance
    public Text meter;
    // To adjust the position of the icon
    public Vector3 offset;
    // Minimum distance for the arrow to vanish permanently
    public float vanishDistance = 15f;
    // Next script to activate
    public MissionWaypoint nextMissionWaypoint;

    // Flag to check if the arrow has vanished
    private bool hasVanished = false;

    private void Update()
    {
        // Calculate the distance between the player and the target
        float distance = Vector3.Distance(target.position, transform.position);

        // Check if the arrow should vanish
        if (!hasVanished && distance < vanishDistance)
        {
            // img.enabled = false;
            // meter.enabled = false;
            hasVanished = true; // Mark as vanished

            // Activate the next mission waypoint script, if specified
            if (nextMissionWaypoint != null)
            {
                nextMissionWaypoint.enabled = true;
            }
            else
            {
                // If there is no next mission waypoint, deactivate the icon
                img.enabled = false;
                meter.enabled = false;
            }

            // Deactivate this script
            enabled = false;
            return;
        }

        // If the arrow has vanished, do not display it again
        if (hasVanished)
        {
            return;
        }

        // Giving limits to the icon so it sticks on the screen
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        // Convert position from 3D world point to 2D screen point
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        // Check if the target is behind us
        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        // Clamp X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Update the marker's position
        img.transform.position = pos;
        meter.text = ((int)distance).ToString() + "m";
    }
}
