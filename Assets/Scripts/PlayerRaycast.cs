using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public Transform target;
    public float lineWidth = 0.01f;

    private Transform player;
    private Renderer renderer;
    private LineRenderer line;


    private Material materialDefault;
    public Material materialNew;


    private void Start()
    {
        player = GetComponent<Transform>();
        renderer = GetComponent<Renderer>();
        materialDefault = renderer.material;

        line = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        SetLineColor(Color.red);
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
    }


    private void SetLineColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    private void SetBallDefault()
    {
        renderer.material = materialDefault;
    }

    private void SetBallNew()
    {
        renderer.material = materialNew;
    }

    private void Update()
    {

        float distance = 10.0f + Vector3.Distance(target.position, player.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, distance))
        {
            if (hit.collider.gameObject == target.gameObject)
            {
                Debug.LogWarning("CAN SEE");
                SetBallNew();
            }
            else
            {
                SetBallDefault();
            }
        }
        else
        {
            SetBallDefault();
        }

        line.SetPosition(0, transform.position);
        if (hit.collider != null)
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, transform.position + (target.position - transform.position).normalized * distance);
        }
    }
}

