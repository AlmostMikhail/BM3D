using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubsurfaceStudios.MeshOperations;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

public class ExtrudeMeshApp : MonoBehaviour
{
    [SerializeField] ProBuilderMesh cube;
    [SerializeField] Camera camera2;
    Vector3 mousePositionInWorld;
    [SerializeField] Color color5;
    Face faceToExtrude;
    Face[] selectedFaces;
    void Update()
    {
        MousePosition();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            cube.SetSelectedFaces(cube.faces);
            Face[] selectedFace = cube.GetSelectedFaces();
            List<Face> extrudingFace = new List<Face>{cube.faces[1]};
            cube.Extrude(extrudingFace, ExtrudeMethod.IndividualFaces,1);
            foreach(Face face in selectedFace){
                Debug.Log(face.indexes[1]);
            }
        }
    }
    void MousePosition()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Declare a variable to store the hit information
            RaycastHit hit;

            Debug.Log("HI");

            // Check if the ray hits something in the 3D world
            if (Physics.Raycast(ray, out hit))
            {

                // Get the hit point in 3D world coordinates
                mousePositionInWorld = hit.point;

                // Now, mousePositionInWorld contains the position of the mouse in Vector3 format
                Debug.Log("Mouse Position in World: " + mousePositionInWorld);
                // Face faceToExtrude = SelectionPicker.PickFace(camera2, mousePositionInWorld, cube2);
                faceToExtrude = SelectionPicker.PickFace(camera2, mousePosition, cube);
                cube.SetFaceColor(faceToExtrude, color5);
                Debug.Log("Face change color");

                cube.ToMesh(MeshTopology.Quads);
                cube.Refresh();
            }
        }
    }
}
