using UnityEngine;

public class ChunkEditor : MonoBehaviour
{
    public ChunkManager manager;

    public float brushSize = 3;
    public float brushStrength = 0.5f;

    private RaycastHit raycastHit;

    private float counter;

    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 0.05f)
        {
            counter = 0;
            bool mb1 = Input.GetMouseButton(0),
                mb2 = Input.GetMouseButton(1);
            if ((mb1 || mb2) && Physics.Raycast(GameObject.FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition), out raycastHit))
            {
                manager.Edit(new Vector3Int(Mathf.RoundToInt(raycastHit.point.x),
                    Mathf.RoundToInt(raycastHit.point.y),
                    Mathf.RoundToInt(raycastHit.point.z)),
                    brushSize, brushStrength * (mb1 ? 1 : -1));
            }
        }

    }
}
