using UnityEngine;

[ExecuteInEditMode]
public class TheGreatSea : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] vertices;
    public int xSize = 10, ySize = 10;
    public float frequency = 0.2f, speed = 0.5f;

    private void Start() {

        mesh = new Mesh();
        mesh.name = "The Great Sea";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        GenerateVertices();
        mesh.vertices = vertices;

        int[]  triangles = new int[xSize * ySize * 6];
        for(int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
            for(int x = 0; x < xSize; x++, ti += 6, vi++) {
                triangles[ti] = vi;

                triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;

                triangles[ti + 2] = vi + 1;
                triangles[ti + 3] = vi + 1;

                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        Vector2[] uvs = new Vector2[vertices.Length];
        for(int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z); // TextureWrapMode is set to repeat
        }
        mesh.uv = uvs;

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update() {
        GenerateVertices();
    }

    private void GenerateVertices() {
        for(int i = 0, y = 0; y <= ySize; y++) {
            for(int x = 0; x <= xSize; x++, i++) {
                vertices[i] = new Vector3(x, Mathf.Sin(frequency * x + speed * Time.time) * Mathf.Sin(frequency * y + speed * Time.time), y);
                // Experiment with the frequency and speed parameters or even  the height function it's self to get some funky oceans 
                
                // Sumbrero ocean:
                // float xc = x - 50;
                // float yc = y - 50;
                // vertices[i] = new Vector3(x, 2f * Mathf.Sin(Mathf.Sqrt(xc*xc + yc*yc) * 0.5f - 5*Time.time) * Mathf.Exp(-xc*xc/100) * Mathf.Exp(-yc*yc/100), y);

                // Multi waves in superposition:
                // vertices[i] = new Vector3(x, Mathf.Sin(frequency * x - 0.2f * speed * Time.time) + Mathf.Sin(frequency * x + speed * Time.time) * Mathf.Sin(frequency * y + speed * Time.time), y);
            }
        }
        mesh.vertices = vertices;
    }
}
