using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class ExtrudeMeshApp4 : MonoBehaviour
{
    [SerializeField] ProBuilderMesh cube;
    [SerializeField] Camera camera2;
    Vector3 mousePositionInWorld;
    Face faceToExtrude;
    bool isDragging;
    private Vector3 dragStartPosition;
    List<Face> extrudingFace = new List<Face>();

    void Update()
    {
        MousePosition();
    }

    void MousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            dragStartPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                mousePositionInWorld = hit.point;
                faceToExtrude = SelectionPicker.PickFace(camera2, mousePosition, cube);
                if (faceToExtrude != null)
                {
                    cube.SetSelectedFaces(cube.faces);
                    extrudingFace.Clear();
                    extrudingFace.Add(faceToExtrude);

                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            if (faceToExtrude != null)
            {
                Vector3 dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float distanceBetween = Vector3.Distance(mousePositionInWorld, dragEndPosition);
                float extrusionAmount = distanceBetween * 0.5f;
                Debug.Log("Extrusion distance: " + extrusionAmount);

                cube.Extrude(extrudingFace, ExtrudeMethod.FaceNormal, extrusionAmount);
                isDragging = false;
                cube.ToMesh(MeshTopology.Quads);
                cube.Refresh();
            }
        }
    }
}
