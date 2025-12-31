using UnityEngine;

public class Maze_CA : MonoBehaviour
{
    public int mSeed;

    public struct Cell
    {
        public int state;
        public int connectDir;
        public int inviteDir;
        public int[] neighbours;
    }

    public Cell[,] map;
    public int[,] mapFinal;
    [SerializeField] int cycles;

    int GlobalSeedCount;
   
    public void MazeAwake(int seed)
    {
        mSeed = seed;
        Random.InitState(mSeed);
        map = new Cell[51,51];
        mapFinal = new int[102, 102];
        for (int i = 0; i < 51; i++)
        {
            for (int j = 0; j < 51; j++)
            {
                map[i, j].state = 0;
                map[i, j].neighbours = new int[4] { 5, 5, 5, 5 };
                map[i, j].connectDir = 4;
            }
        }
        for (int i = 0; i < 102; i++)
        {
            for (int j = 0; j < 102; j++)
            {
                mapFinal[i,j] = 1;
            }
        }
        PickSeed();
        for (int i = 0; i < cycles; i++)
        { GlobalSeedCount=Cycle(); }
        for (int i = 0; i < 51; i = i + 1)
        {
            for(int j=0; j < 51; j = j + 1)
            {
                if(map[i, j].state == 0)
                {
                    mapFinal[2 * i, 2 * j] = 1;
                }
                if (map[i, j].state == 3)
                {
                    mapFinal[2 * i, 2 * j] = 0;
                }
                if (map[i, j].connectDir == 0) { mapFinal[(2 * i), (2 * j) + 1] = 0; }
                if (map[i, j].connectDir == 1) { mapFinal[(2 * i) + 1, (2 * j)] = 0; }
                if (map[i, j].connectDir == 2) { mapFinal[(2 * i), (2 * j) - 1] = 0; }
                if (map[i, j].connectDir == 3) { mapFinal[(2 * i) - 1, (2 * j)] = 0; }
            }
        }
        for (int i = 0; i < 102; i = i + 1)
        {
            for (int j = 0; j < 102; j = j + 1)
            {
                if(i == 1 || i == 0|| j == 1 || j == 0 || i == 101 || i == 100 || j == 101 || j == 100)
                {
                    mapFinal[i,j] = 0;
                }
            }
        }
        for(int i = 46;i <56; i++)
        {
            for (int j = 46; j < 56; j++)
            {
                mapFinal[i, j] = 0;
            }
        }
    }

    int Cycle()
    {
        int seedCount = 0;
        for (int i = 1; i < 50; i++)
        {
            for (int j = 1; j < 50; j++)
            {
                if (map[i, j].state == 0)
                {
                    //if (map[i, j+1].state == 2 && map[i, j + 1].inviteDir == 2)
                    //{
                    //    map[i, j].connectDir = 0;
                    //    map[i,j].state = 1;
                    //}
                    //if (map[i+1, j].state == 2 && map[i+1, j].inviteDir == 3)
                    //{
                    //    map[i, j].connectDir = 1;
                    //    map[i, j].state = 1;
                    //}
                    //if (map[i, j - 1].state == 2 && map[i, j - 1].inviteDir == 0)
                    //{
                    //    map[i, j].connectDir = 2;
                    //    map[i, j].state = 1;
                    //}
                    //if (map[i-1, j].state == 2 && map[i-1, j].inviteDir == 1)
                    //{
                    //    map[i, j].connectDir = 3;
                    //    map[i, j].state = 1;
                    //}
                }
                if (map[i, j].state == 1)
                {
                    seedCount++;
                    int neighbourCount = 0;
                    if (map[i, j + 1].state == 0) { map[i, j].neighbours[0] = 0; neighbourCount++; }
                    else { map[i, j].neighbours[0] = 4; }
                    if (map[i + 1, j].state == 0) { map[i, j].neighbours[1] = 1; neighbourCount++; }
                    else { map[i, j].neighbours[1] = 4; }
                    if (map[i, j - 1].state == 0) { map[i, j].neighbours[2] = 2; neighbourCount++; }
                    else { map[i, j].neighbours[2] = 4; }
                    if (map[i - 1, j].state == 0) { map[i, j].neighbours[3] = 3; neighbourCount++; }
                    else { map[i, j].neighbours[3] = 4; }

                    bool dirPickBool = false;
                    int dirpick;


                    if (Random.Range(0, 100) > 100)
                    {
                        if (map[i, j].neighbours[(map[i, j].connectDir + 2) % 2] != 4)
                        {
                            map[i, j].inviteDir = map[i, j].neighbours[(map[i, j].connectDir + 2) % 2];
                            dirPickBool = true;
                        }
                    }



                    while (dirPickBool == false)
                    {
                        if (neighbourCount == 0) { dirPickBool = true; }
                        dirpick = Random.Range(0, 4);
                        if (map[i, j].neighbours[dirpick] != 4)
                        {
                            map[i, j].inviteDir = map[i, j].neighbours[dirpick];
                            dirPickBool = true;



                        }
                    }
                    if (neighbourCount == 0)
                    {
                        map[i, j].state = 3;
                    }
                    else
                    {
                        map[i, j].state = 2;
                    }
                }
                if (map[i, j].state == 2)
                {
                    if (map[i, j].inviteDir == 0) { map[i, j + 1].state = 1; map[i, j + 1].connectDir = 2; }
                    if (map[i, j].inviteDir == 1) { map[i + 1, j].state = 1; map[i + 1, j].connectDir = 3; }
                    if (map[i, j].inviteDir == 2) { map[i, j - 1].state = 1; map[i, j - 1].connectDir = 0; }
                    if (map[i, j].inviteDir == 3) { map[i - 1, j].state = 1; map[i - 1, j].connectDir = 1; }



                    if (Random.Range(0, 100) > 5)
                    {
                        map[i, j].state = 3;
                    }
                    else { map[i, j].state = 1; }
                }
                if (map[i, j].state == 3)
                {
                    //int seedCount = 0;
                    bool isValid = false;
                    //for (int k = 1; k < 50; k++)
                    //{
                    //    for (int l = 1; l < 50; l++)
                    //    {
                    //        if (map[k, l].state == 1)
                    //        {
                    //            seedCount++;
                    //        }

                    //    }

                    //}
                    // Debug.Log(seedCount);
                    //Debug.Log(GlobalSeedCount);
                    if(seedCount ==0)
                    {
                        for(int k = 0; k < 4; k++)
                        {
                            if (map[i, j].neighbours[k] != 4)
                            {
                                isValid = true;
                            }
                        }
                       if (isValid&&(Random.Range(0,100)<=5))
                        {
                            map[i,j].state = 1;
                       }
                     }

                }
            }
            }
        return seedCount;
    }

    void PickSeed()
    {
        //Random.InitState(mSeed);
        //Cell seedTest = map[Random.Range(0, 51), Random.Range(0, 51)];
        int posX = Random.Range(0, 52);
        int posY = Random.Range(0, 52);
        if (map[posX, posY].state == 0)
        {
            map[posX, posY].state = 1;
        }
        else { PickSeed(); }
       
    }
}
