using UnityEngine;

public class CanvasChanger : MonoBehaviour
{
    public GameObject canvas1; // Reference to the first canvas
    public GameObject canvas2; // Reference to the second canvas
    private int currentIndex = 2;
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public GameObject box4;
    public GameObject next;

    public void Canvas2()
    {
        // Disable the first canvas
        canvas1.SetActive(false);

        // Enable the second canvas
        box2.SetActive(false);
        box3.SetActive(false);
        box4.SetActive(false);
        next.SetActive(true);
        canvas2.SetActive(true);
        currentIndex = 2;

    }

    public void Canvas1()
    {
        // Disable the second canvas
        canvas2.SetActive(false);

        // Enable the first canvas
        canvas1.SetActive(true);
        currentIndex = 2;
    }
 // Tracks the current active box


    public void ShowNextBox()
    {
        if(currentIndex == 2){
            box2.SetActive(true);
            currentIndex++;
        }
        else if(currentIndex == 3){
            box3.SetActive(true);
            currentIndex++;
        }
        else if(currentIndex == 4){
            box4.SetActive(true);
            next.SetActive(false);
            currentIndex=2;
        }
    }
}
