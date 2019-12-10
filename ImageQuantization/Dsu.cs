class DSU
{

    public int[] p;// O(1)
    public int[] rank;//O(1)


    public DSU()
    {

        p = new int[100005]; //O(1)
        rank = new int[100005];//O(1)
    }

    public int FindParent(int node)
    {
        if (p[node] == node)  //O(1)
            return node;  //O(1)
        return p[node] = FindParent(p[node]);  //O(log*(n))
    }

    public void connect(int node1, int node2)
    {
        int p1 = FindParent(node1), p2 = FindParent(node2); //both take theta of (1)
        if (rank[p1] >= rank[p2]) //O(1)
        {
            p[p2] = p1; //O(1)
            rank[p1] += rank[p2]; //O(1)
        }
        else
        {
            p[p1] = p2; //theta (1)
            rank[p2] += rank[p1];//theta(1)
        }
    }

    public void init()
    {
        //theta(100005) so takes O(1)
        for (int i = 0; i < 100005; i++)
        {
            //theta(1)
            rank[i] = 1;
            //theta(1)
            p[i] = i;
        }
    }
}
