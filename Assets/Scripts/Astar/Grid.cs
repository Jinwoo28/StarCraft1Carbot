using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid gridinstance = null;

    private void Awake()
    {
        if (null == gridinstance)
        {
            gridinstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject groundPrefab;
    GameObject parentGrid;

    public Vector2 gridWorldSize;

    public Node start_;
    public Node end;

    Node[,] grid;

    private void Start()
    {
        CreateGrid();
    }

    private void Update()
    {
        SetObstacle();
    }

    public void CreateGrid()
    {
        if (parentGrid != null) Destroy(parentGrid);
        parentGrid = new GameObject("parentGrid");
        grid = new Node[(int)gridWorldSize.x, (int)gridWorldSize.y];

        Vector3 worldBottomLeft = Vector3.zero - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < (int)gridWorldSize.x; x++)
        {
            for (int y = 0; y < (int)gridWorldSize.y; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x + 0.5f) + Vector3.forward * (y + 0.5f);   //0�� �������� ��, �찡 ���� index���� ������ ���� 0.5�� ���Ѵ�.
                GameObject obj = Instantiate(groundPrefab, worldPoint, Quaternion.Euler(90, 0, 0));
                obj.transform.parent = parentGrid.transform;
                grid[x, y] = new Node(obj, true, x, y);
            }
        }
    }

    public Node NodePoint(Vector3 rayPosition)
    {
        //���̿� ���� ���� node�� ã�� ��ȯ�ϱ� ���� �Լ�
        int x = (int)(rayPosition.x + gridWorldSize.x / 2);     //index��ȣ��...?
        int y = (int)(rayPosition.z + gridWorldSize.y / 2);

        //Debug.Log("X : " + x + " Y : " + y);
        return grid[x, y];
    }

    public void ObstacleNode(Vector3 Obstacle)
    {
        int x = (int)(Obstacle.x + gridWorldSize.x / 2);
        int y = (int)(Obstacle.z + gridWorldSize.y / 2);
        grid[x, y].ChangeNode = false;
    }

    public void ExitObstacleNode(Vector3 Obstacle)
    {
        int x = (int)(Obstacle.x + gridWorldSize.x / 2);
        int y = (int)(Obstacle.z + gridWorldSize.y / 2);
        grid[x, y].ChangeNode = true;
    }

    public List<Node> GetNeighbours(Node node)  //������Ƽ�� �������� ������ ���� �� �̵� ������ ��带 ��ȯ
    {
        List<Node> neighbours = new List<Node>();
        int[,] temp = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } }; //���� ��带 �������� ���� �¿�
                                                                    // 0    1
                                                                    // 1    0
                                                                    // 0   -1
                                                                    //-1    0
        bool[] walkableUDLR = new bool[4];  // �̵����������� ���ε� �����¿�� 4���� �غ��Ѵ�

        //�����¿��� ��� ���� ���
        for (int i = 0; i < 4; i++)
        {
            int checkX = node.gridX + temp[i, 0];
            int checkY = node.gridY + temp[i, 1];
            if (checkX >= 0 && checkX < (int)gridWorldSize.x && checkY >= 0 && checkY < (int)gridWorldSize.y)
            {  //�˻� ��尡 ��ü ���ȿ� ��ġ�� �ִ��� �˻�

                if (grid[checkX, checkY].walkable) walkableUDLR[i] = true; //�ش� ��忡 ��ֹ��� ���� �� �� �ִٸ� �ش� bool ���� true�� ����
                //��ֹ��� �ִ� ����� false�� �߰�

                neighbours.Add(grid[checkX, checkY]);   //�̿� ��忡 �߰�
            }
        }

        //�밢���� ��带 ���
        for (int i = 0; i < 4; i++)
        {
            if (walkableUDLR[i] || walkableUDLR[(i + 1) % 4])
            {
                int checkX = node.gridX + temp[i, 0] + temp[(i + 1) % 4, 0];
                int checkY = node.gridY + temp[i, 1] + temp[(i + 1) % 4, 1];
                if (checkX >= 0 && checkX < (int)gridWorldSize.x && checkY >= 0 && checkY < (int)gridWorldSize.y)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
        //�̿����� ���� List�� ��ȯ
    }


    public void SetObstacle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Node node = RayCast();
            if (node != null) //���콺 ����Ʈ�� ���� ���� ���� �ƴ϶�� return
            {
                if (node.start || node.end)      // Ray�� ���� ��尡 ����, ������ �Ǵ� ���������̰ų� �� �����̸� ����/���� ��ġ�� ������ �� �ִ�.
                    StartCoroutine("SwitchStartEnd", node);
                else
                    StartCoroutine("ChangeWalkable", node); //�����̳� ���� �ƴ϶�� ��ֹ��� ����
            }
            return;
        }

    }

    public Node RayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ī�޶� �������� ���콺 ��ġ�� Ray�߻�
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject obj = hit.collider.gameObject; //���� hit�� ������ ��ȯ
                                                      //  Debug.Log(obj.name);
                                                      //  Debug.Log(obj.transform.position);
            return Grid.gridinstance.NodePoint(obj.transform.position);  // ������ ����� x,y ������ grid[x,y]�� ã��
        }
        return null; // ���� collider�� ������ null ��ȯ
    }


    IEnumerator SwitchStartEnd(Node node)   // �巡�׷� ��ŸƮ ���� ��ġ ����
    {
        //node = ���콺 ����Ʈ�� ���� node
        bool start = node.start;    // �� ��尡 start���� 
        Node nodeOld = node;    //noldOld�� ���콺 ����Ʈ�� ���� node�� ����

        while (Input.GetMouseButton(0))
        {
            node = RayCast();
            if (node != null && node != nodeOld)
            {
                if (start && !node.end)
                {
                    node.ChangeStart = true;
                    start_ = node;
                    nodeOld.ChangeStart = false;
                    nodeOld = node;
                }
                else if (!start && !node.start)
                {
                    node.ChangeEnd = true;
                    end = node;
                    nodeOld.ChangeEnd = false;
                    nodeOld = node;
                }
            }
            yield return null;
        }
    }

    IEnumerator ChangeWalkable(Node node)
    {
        bool walkable = !node.walkable; // ���� �Ұ��� �ݴ�� ��ȯ

        while (Input.GetMouseButton(0)) //���콺 ��ư�� ������ ���� ��� ����
        {
            node = RayCast();
            if (node != null && !node.start && !node.end) //�ش� ��尡 �־�� �ϰ�, ���۰� ������ �ƴ� �� ����
            {
                node.ChangeNode = walkable;
            }
            yield return null;
        }
    }

}
