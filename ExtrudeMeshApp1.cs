using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SubsurfaceStudios.MeshOperations;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.ProBuilder.Shapes;

public class ExtrudeMeshApp1 : MonoBehaviour
{
    //[SerializeField] ProBuilderMesh proBuilderMesh;
    [SerializeField] Material material;
    [SerializeField] Material material2;
    [SerializeField] Color color;
    [SerializeField] ProBuilderMesh cube;
    private Color vertexColor = Color.red;
    ProBuilderMesh cube2;
    [SerializeField] Camera camera2;
    [SerializeField] Rect rect;
    Vector3 mousePositionInWorld;
    [SerializeField] Color color5;
    Face faceToExtrude;
    // Start is called before the first frame update
    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
         {*/
        //SetTheColor();
        MousePosition();  
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
        /* // List<Edge> newList = new List<Edge>();
         //Edge edge = new Edge(1,2);
         Vector3 size = new Vector3(1f, 1f, 1f);
         cube = ShapeGenerator.GenerateCube(PivotLocation.Center, size);
         Face face = new Face(new List<int>() { 0, 1, 2, 3, 4, 5 });
         cube.SetMaterial(cube.faces,material);
         cube.SetFaceColor(face, material2.color);
         // IList<Face> faces = cube.faces;

         // newList.Add(edge);
         //Material selectedMaterial = new Material(material);
         Debug.Log("SetColor");
         //proBuilderMesh.SetSelectedEdges(new List<Edge>() {edge});
         //proBuilderMesh.SetMaterial(proBuilderMesh.faces,selectedMaterial);
         //Bevel.BevelEdges(proBuilderMesh,);
         // cube.ToMesh();
         cube.Refresh();
         // ExtrudeElements.Extrude(proBuilderMesh, proBuilderMesh.faces, ExtrudeMethod.IndividualFaces, 1);*/
    }
    /*void SetTheColor()
    {

        color5 = Color.red;
        //cube.SetFaceColor(cube.faces[1], color5);
        Vector3 size = new Vector3(1f, 1f, 1f);
        if (cube2 == null)
        {
            cube2 = ShapeGenerator.GenerateCube(PivotLocation.Center, size);
            cube2.ExtrudeFaces(ExtrudeMethod.IndividualFaces, 1, 1);
        }


        cube2.SetMaterial(cube2.faces, material);
        Color color6 = Color.yellow;
        for (int i = 0; i < 2; i++)
        {
            cube2.SetFaceColor(cube2.faces[i], color6);

        }




        //cube2.Extrude(cube2.faces, ExtrudeMethod.IndividualFaces, 1);


    }*/

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
