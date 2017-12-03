using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Building : SelectableObject
{
    //Building Variables
    public string buildingName;
    public float buildingCreateTime;
    public int buildingHealth;
    public int buildingArmor;
    public List<Vector3> destinationPoints;
    public LineRenderer destinationLineRenderer;

    //AssemblyPoint == Sammelpunkt
    public Vector3 assemblyPoint;

    private void Start()
    {
        destinationLineRenderer = GetComponent<LineRenderer>();
    }

    public virtual void DoAction()
    {
        //Do action
        Debug.Log("Performing action of Building:" + buildingName);
    }
    
    public override void Select(bool selected)
    {
        //DO SELECTION STUFF
        base.Select(selected);
        Debug.Log("Selection stuff of building was called" + selected);
        GetComponentInChildren<Projector>().enabled = selected;
        destinationLineRenderer.enabled = selected;
        if (GetComponentInChildren<Projector>() == null) Debug.Log("Projector not found");
    }

    public override void RightClickAction(Vector3 clickPosition, bool shiftActivated)
    {
        //DO RIGHT CLICK ACTION
        base.RightClickAction(clickPosition, shiftActivated);
        
        if (!shiftActivated)
        {
            destinationPoints.Clear();
            AddNewDestination(clickPosition);
        }
        else
        {
            AddNewDestination(clickPosition);
        }
        SetupLineRenderer();
    }

    private void SetupLineRenderer()
    {
        int vertexCount = destinationPoints.Count;
        destinationLineRenderer.positionCount = vertexCount;
        destinationLineRenderer.SetPositions(destinationPoints.ToArray());
    }

    private void AddNewDestination(Vector3 newDestination)
    {
        if(destinationPoints.Count <= 0)
        {
            destinationPoints.Add(transform.position);
        }
        destinationPoints.Add(newDestination);
    }
}
