using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //MakePolygon(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 1));
        MakeQuad(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1));

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject MakePolygon(Vector3 p1, Vector3 p2, Vector3 p3)//생성할 폴리곤의 각각의 점에 대한 벡터
    {
        GameObject go = new GameObject("Poligon");//Poligon이라는 이름을 지정하여 GameObjcet를 생성
        Mesh mesh = new Mesh();//Poligon을 그리기 위한 Mesh 생성
        MeshFilter mf = go.AddComponent<MeshFilter>();//GameObject에 MeshFilter를 추가
        MeshRenderer mr = go.AddComponent<MeshRenderer>();//GameObject에 MeshRenderer를 추가
        Material mt = new Material(Shader.Find("Standard"));//Shader에 Standerd를 찾아 메터리얼 생성

        Vector3[] vertices = { p1, p2, p3 };//함수에 들어온 각각의 정점을 Vecter3에 할당
        Vector2[] uvs = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };//UV를 이용하기 위해 각각의 정점에 할당할 UV값 설정

        int[] tris = { 0, 2, 1 };//시계방향으로 그릴것인지 결정-면이 위를 봄
                                 //int[] tris={0,1,2}; //이 경우 역방향으로 생성-면이 아래를 봄

        //이것이 중요한 이유는 면이 바라보는 뱡향이 위인지 아래인지 결정하기 때문이다.
        mt.mainTexture = (Texture)Resources.Load("Texture/1941165_0");//메터리얼에 texture 할당
        mesh.vertices = vertices;//아까 받은 정점들을 그리기 위해 Mesh에 할당
        mesh.triangles = tris;//면의 방향을 결정해주는 요소를 Mesh에 할당
        mesh.uv = uvs;//UV 값을 Mesh에 할당
        mf.mesh = mesh;//그리고 그렇게 생성된 Mesh를 MeshFilter를 통해 그림
        mr.material = mt;//메터리얼 값도 MeshRenderer를 통해 보여준다

        return go;//마지막으로 생성된 오브젝트를 리턴
    }
    public GameObject MakeQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        GameObject go = new GameObject("Quad");
        Mesh mesh = new Mesh();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        Material mt = new Material(Shader.Find("Standard"));
        mt.mainTexture = (Texture)Resources.Load("Texture/corodinateChecker");

        Vector3[] vertices = { p1, p2, p3, p4 };
        Vector2[] uvs = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

        int[] tris = { 0, 1, 2, 2, 1, 3 };//사각형을 만들기 위해 두개의 삼각형을 이용한다.
       // int[] tris = { 0, 1, 2, 2, 3, 1 }; 이런식으로 두개의 삼각형을 서로 다르게 뒤집어 그릴수도 있다.
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mf.mesh = mesh;
        mr.material = mt;

        return go;
    }
}
