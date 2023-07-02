using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //MakePolygon(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 1));
        // MakeQuad(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1));
        //MakeSubmeshQuad(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1));

        //TRS
        //quad object size 
        const float width = 1;
        const float height = 1;
        //gameObject 생성
        Mesh mesh = MakeQuad(new Vector3(-width / 2, 0, -height / 2), new Vector3(-width / 2, 0, height / 2), new Vector3(width / 2, 0, -height / 2), new Vector3(width / 2, 0, height / 2)).GetComponent<MeshFilter>().mesh;
        GameObject go = new GameObject("extruded Mesh");
        Material mt = new Material(Shader.Find("Standard"));
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>().material = mt;

        //변환값을 저장할 새로운 위치 리스트
        List<Vector3> newVert = new List<Vector3>();

        //mesh의 vertices의 갯수 만큼 반복
        foreach (Vector3 v in mesh.vertices)
        {
            Matrix4x4 fx;
            Matrix4x4 fxT;
            Matrix4x4 fxR;
            Matrix4x4 fxS;

            //변환행렬곱 수행 
            fxT = Translate(new Vector3(2f, 0f, 0f));
            fxR = Rotate(new Vector3(45f, 0f, 0f));
            fxS = Scale(new Vector3(0.5f, 2f, 2f));
            //변환행렬 합성 (x축으로 2만큼 평행이동, x축기준 45도 회전, x축기준 0.5배 scale)
            fx = fxT * fxR * fxS;
            newVert.Add(fx.MultiplyPoint(v));//벡터를 정점(x,y,z,w)으로서 곱한다는 의미이다.
           // newVert.Add(fx*v); 이렇게 하게되면 44행렬과 벡터3을 곱하게 됨으로 4열을 이용하는 transform의 변환겂을 얻을 수 없다.
        }
        //mesh의 위치정보 갱신
        mesh.vertices = newVert.ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject MakePolygon(Vector3 p1, Vector3 p2, Vector3 p3)//폴리곤을 그리는 함수
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
        mt.mainTexture = (Texture)Resources.Load("Texture/1941165_0");//메터리얼에 texture 할당(Texture소스가 없음으로 null)
        mesh.vertices = vertices;//아까 받은 정점들을 그리기 위해 Mesh에 할당
        mesh.triangles = tris;//면의 방향을 결정해주는 요소를 Mesh에 할당
        mesh.uv = uvs;//UV 값을 Mesh에 할당
        mf.mesh = mesh;//그리고 그렇게 생성된 Mesh를 MeshFilter를 통해 그림
        mr.material = mt;//메터리얼 값도 MeshRenderer를 통해 보여준다

        return go;//마지막으로 생성된 오브젝트를 리턴
    }
    public GameObject MakeQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)//쿼드를 그리는 함수
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
    public GameObject MakeSubmeshQuad(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Vector3 p5, Vector3 p6)//서브메쉬 커드를 그리는 함수
    {
        GameObject go = new GameObject("subQuad");
        Mesh mesh = new Mesh();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        Material[] mts = new Material[2];

        mts[0] = new Material(Shader.Find("Standard"));
        mts[1] = new Material(Shader.Find("Standard"));
        mts[0].mainTexture = (Texture)Resources.Load("Texture/corodinateChecker");
        mts[1].mainTexture = (Texture)Resources.Load("Texture/corodinate");
        mts[1].color = Color.green;// 두 폴리곤의 다른 점을 봐야함으로 색을 넣는다.

        Vector3[] vertices = { p1, p2, p3, p4, p5, p6 };
        Vector2[] uvs = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };

        int[] subTris1 = { 0, 2, 1 };//2개의 폴리곤을 그릴 것이기 때문에 따로 설정해준다.
        int[] subTris2 = { 4, 3, 5 };
        mesh.vertices = vertices;
        mesh.subMeshCount = 2;//2개의 폴리곤을 그릴 것이기 때문에 2로 설정
        mesh.SetTriangles(subTris1, 0);
        mesh.SetTriangles(subTris2, 1);
        mesh.uv = uvs;

        mf.mesh = mesh;
        mr.materials = mts;
        return go;
    }
    //vector의 이동
    public static Matrix4x4 Translate(Vector3 aPosition)
    {
        //Matrix4x4.identity;는 단위 행렬을 의미한다.
        var m = Matrix4x4.identity; // 1   0   0   x
        m.m03 = aPosition.x;        // 0   1   0   y
        m.m13 = aPosition.y;        // 0   0   1   z
        m.m23 = aPosition.z;        // 0   0   0   1
        return m;
    }
    //vector의 회전
    public static Matrix4x4 RotateX(float aAngleRad)
    {
        Matrix4x4 m = Matrix4x4.identity;     //  1   0   0   0 
        m.m11 = m.m22 = Mathf.Cos(aAngleRad); //  0  cos -sin 0
        m.m21 = Mathf.Sin(aAngleRad);         //  0  sin  cos 0
        m.m12 = -m.m21;                       //  0   0   0   1
        return m;
    }
    public static Matrix4x4 RotateY(float aAngleRad)
    {
        Matrix4x4 m = Matrix4x4.identity;     // cos  0  sin  0
        m.m00 = m.m22 = Mathf.Cos(aAngleRad); //  0   1   0   0
        m.m02 = Mathf.Sin(aAngleRad);         //-sin  0  cos  0
        m.m20 = -m.m02;                       //  0   0   0   1
        return m;
    }
    public static Matrix4x4 RotateZ(float aAngleRad)
    {
        Matrix4x4 m = Matrix4x4.identity;     // cos -sin 0   0
        m.m00 = m.m11 = Mathf.Cos(aAngleRad); // sin  cos 0   0
        m.m10 = Mathf.Sin(aAngleRad);         //  0   0   1   0
        m.m01 = -m.m10;                       //  0   0   0   1
        return m;
    }
    //최종 회전 결과
    public static Matrix4x4 Rotate(Vector3 aEulerAngles)
    {
        var rad = aEulerAngles * Mathf.Deg2Rad;//오일러 값으로 받은 것을 라디안으로 변환 
        return RotateY(rad.y) * RotateX(rad.x) * RotateZ(rad.z);
    }
    //vector의 확대/축소
    public static Matrix4x4 Scale(Vector3 aScale)
    {
        var m = Matrix4x4.identity; //  sx   0   0   0
        m.m00 = aScale.x;           //   0  sy   0   0
        m.m11 = aScale.y;           //   0   0  sz   0
        m.m22 = aScale.z;           //   0   0   0   1
        return m;
    }
}
