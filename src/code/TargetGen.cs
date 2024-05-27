using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TargetGen : MonoBehaviour
{
    [SerializeField] public control control;

    public GameObject cylinderPrefab; // Prefab of the cylinder
    public int numberOfCylindersMax = 10; // Number of cylinders to generate
    public int numberOfCylindersMin = 4; // Number of cylinders to generate
    public float minDistance = 4f; // Minimum radius of cylinders

    public float gapX = 1;
    public float gapZ = 1;


    private List<GameObject> cylinders = new List<GameObject>();

    private void Start()
    {
        GenerateCylinders();
    }


    private void UpdateCylinders()
    {
        List<GameObject> listToDell = new List<GameObject>();
        foreach (GameObject cylinder in cylinders)
        {
            if(cylinder.IsDestroyed()) {
                listToDell.Add(cylinder);
            }
        }

        foreach(GameObject cylinder in listToDell)
            cylinders.Remove(cylinder);

        if (cylinders.Count < numberOfCylindersMin) { GenerateCylinders(); }
    }

    private void Update()
    {
        UpdateCylinders();
    }

    public void GenerateCylinders()
    {
        for (int i = 0; i < numberOfCylindersMax; i++)
        {
            Vector3 pos = GetRandomPosition();


            bool isValidPosition = CheckValidPosition(pos);
            if (isValidPosition)
            {
                DrawBox(pos, new Quaternion(), cylinderPrefab.transform.localScale, Color.green);
                GameObject newCylinder = Instantiate(cylinderPrefab, pos, gameObject.transform.rotation);
                newCylinder.GetComponent<Target>().Init(control , Random.Range(10, 90));
                newCylinder.transform.SetParent(transform);
                cylinders.Add(newCylinder); // Add the cylinder to the list if it's in a valid position
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 size = gameObject.GetComponent<MeshCollider>().bounds.size;

        float x = Random.Range(-size.x + gapX, size.x - gapX);
        float z = Random.Range(-size.z + gapZ, size.z - gapZ);

        return transform.TransformPoint(new Vector3(x, 0f, z));

    }

    public void DrawBox(Vector3 pos, Quaternion rot, Vector3 scale, Color c)
    {
        // create matrix
        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(pos, rot, scale);

        var point1 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, 0.5f));
        var point2 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, 0.5f));
        var point3 = m.MultiplyPoint(new Vector3(0.5f, -0.5f, -0.5f));
        var point4 = m.MultiplyPoint(new Vector3(-0.5f, -0.5f, -0.5f));

        var point5 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, 0.5f));
        var point6 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, 0.5f));
        var point7 = m.MultiplyPoint(new Vector3(0.5f, 0.5f, -0.5f));
        var point8 = m.MultiplyPoint(new Vector3(-0.5f, 0.5f, -0.5f));

        Debug.DrawLine(point1, point2, c);
        Debug.DrawLine(point2, point3, c);
        Debug.DrawLine(point3, point4, c);
        Debug.DrawLine(point4, point1, c);

        Debug.DrawLine(point5, point6, c);
        Debug.DrawLine(point6, point7, c);
        Debug.DrawLine(point7, point8, c);
        Debug.DrawLine(point8, point5, c);

        Debug.DrawLine(point1, point5, c);
        Debug.DrawLine(point2, point6, c);
        Debug.DrawLine(point3, point7, c);
        Debug.DrawLine(point4, point8, c);

        // optional axis display
       // Debug.DrawRay(m.GetPosition(), m.GetForward(), Color.magenta);
       // Debug.DrawRay(m.GetPosition(), m.GetUp(), Color.yellow);
       // Debug.DrawRay(m.GetPosition(), m.GetRight(), Color.red);
    }

    private bool CheckValidPosition(Vector3 posNew)
    {
        foreach (GameObject otherCylinder in cylinders)
        {
            DrawBox(posNew, new Quaternion(), cylinderPrefab.transform.localScale, Color.cyan);
            if (Vector3.Distance(posNew, otherCylinder.transform.position) < minDistance)
            {
                Debug.DrawLine(posNew, otherCylinder.transform.position, Color.red);
                return false; // Collision detected
            } else
            {
                Debug.DrawLine(posNew, otherCylinder.transform.position, Color.green);
            }
        }
        return true; // No collision detected
    }
}
