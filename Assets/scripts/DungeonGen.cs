using UnityEngine;

public class DungeonGen : MonoBehaviour
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
    [SerializeField] int roomNumber;

    int[,] interior1;
    int[,] interior2;
    int[,] interior3;
    int[,] interior4;
    int[,] interior5;
    int[,] interior6;


    int GlobalSeedCount;
 

    public void DungeonAwake(int seed)
    {
        mSeed = seed;
        InitInteriors(); //assigns interior arrays
        Random.InitState(mSeed);
        map = new Cell[10, 10]; //initialises room connection array for cellular automata
        mapFinal = new int[100, 100]; //initialises final dungeon array
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                //initialises each cell in room array
                map[i, j].state = 0;
                map[i, j].neighbours = new int[4] { 5, 5, 5, 5 };
                map[i, j].connectDir = 4;
            }
        }
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                mapFinal[i, j] = 999; //initialises each cell in dungeon array
            }
        }
        map[5, 5].state = 1; //sets seed cell in room array

        for (int i = 0; i < cycles; i++)
        { GlobalSeedCount = Cycle(); } //cycle the cellular automaton
        Debug.Log(GlobalSeedCount);
        for (int i = 0; i < 10; i = i + 1)
        {
            for (int j = 0; j < 10; j = j + 1)
            {
                if (map[i, j].state == 0)
                {
                    mapFinal[10 * i, 10 * j] = 999; //maps disconnected cells in room array to null cells in dungeon array
                }
                if (map[i, j].state == 3)
                {
                    int roomPick = Random.Range(1, 7);
                    for (int k = 0; k < 10; k = k + 1)
                    {
                        for (int l = 0; l < 10; l = l + 1)
                        {
                            mapFinal[(10 * i) + k, (10 * j) + l] = 1;//sets dungeon walls
                        }
                    }
                    for (int k = 1; k < 9; k = k + 1)
                    {
                        for (int l = 1; l < 9; l = l + 1)//puts random romm interiors into dungeon array
                        {
                            if (roomPick == 1)
                            {
                                mapFinal[(10 * i) + k, (10 * j) + l] = interior1[k - 1, l - 1];
                            }
                            if (roomPick == 2)
                            {
                                mapFinal[(10 * i) + k, (10 * j) + l] = interior2[k - 1, l - 1];
                            }
                            if (roomPick == 3)
                            {
                                mapFinal[(10 * i) + k, (10 * j) + l] = interior3[k - 1, l - 1];
                            }
                            if (roomPick == 4)
                            {
                                mapFinal[(10 * i) + k, (10 * j) + l] = interior4[k - 1, l - 1];
                            }
                            if (roomPick == 5)
                            {
                                mapFinal[(10 * i) + k, (10 * j) + l] = interior5[k - 1, l - 1];
                            }
                            if (roomPick == 6)
                            {
                                mapFinal[(10 * i) + k, (10 * j) + l] = interior6[k - 1, l - 1];
                            }
                        }
                    }


                }


            }
        }
        for (int i = 0; i < 10; i = i + 1)
        {
            for (int j = 0; j < 10; j = j + 1)
            {

                if (map[i, j].state == 3)//creates door between connected rooms
                {
                    if (map[i, j].connectDir == 0)
                    {
                        mapFinal[(10 * i) + 4, (10 * j) + 9] = 0;
                        mapFinal[(10 * i) + 5, (10 * j) + 9] = 0;
                        if (j < 9)
                        {
                            mapFinal[(10 * i) + 4, (10 * j) + 10] = 0;
                            mapFinal[(10 * i) + 5, (10 * j) + 10] = 0;
                        }
                    }
                    if (map[i, j].connectDir == 1)
                    {
                        mapFinal[(10 * i) + 9, (10 * j) + 4] = 0;
                        mapFinal[(10 * i) + 9, (10 * j) + 5] = 0;
                        if (i < 9)
                        {
                            mapFinal[(10 * i) + 10, (10 * j) + 4] = 0;
                            mapFinal[(10 * i) + 10, (10 * j) + 5] = 0;
                        }
                    }
                    if (map[i, j].connectDir == 2)
                    {
                        mapFinal[(10 * i) + 4, (10 * j)] = 0;
                        mapFinal[(10 * i) + 5, (10 * j)] = 0;
                        if (j > 0)
                        {
                            mapFinal[(10 * i) + 4, (10 * j) - 1] = 0;
                            mapFinal[(10 * i) + 5, (10 * j) - 1] = 0;
                        }
                    }
                    if (map[i, j].connectDir == 3)
                    {
                        mapFinal[(10 * i), (10 * j) + 4] = 0;
                        mapFinal[(10 * i), (10 * j) + 5] = 0;
                        if (i > 0)
                        {
                            mapFinal[(10 * i) - 1, (10 * j) + 4] = 0;
                            mapFinal[(10 * i) - 1, (10 * j) + 5] = 0;
                        }
                    }
                }
                if (map[i, j].state != 0&&Random.Range(0, 100) <= 45) //creates random doors between rooms
                {
                    if (Random.Range(0, 100) <= 25)
                    {
                        mapFinal[(10 * i) + 4, (10 * j) + 9] = 0;
                        mapFinal[(10 * i) + 5, (10 * j) + 9] = 0;
                        if (j < 9)
                        {
                            mapFinal[(10 * i) + 4, (10 * j) + 10] = 0;
                            mapFinal[(10 * i) + 5, (10 * j) + 10] = 0;
                        }
                    }
                    if (Random.Range(0, 100) <= 25)
                    {
                        mapFinal[(10 * i) + 9, (10 * j) + 4] = 0;
                        mapFinal[(10 * i) + 9, (10 * j) + 5] = 0;
                        if (i < 9)
                        {
                            mapFinal[(10 * i) + 10, (10 * j) + 4] = 0;
                            mapFinal[(10 * i) + 10, (10 * j) + 5] = 0;
                        }
                    }
                    if (Random.Range(0, 100) <= 25)
                    {
                        mapFinal[(10 * i) + 4, (10 * j)] = 0;
                        mapFinal[(10 * i) + 5, (10 * j)] = 0;
                        if (j > 0)
                        {
                            mapFinal[(10 * i) + 4, (10 * j) - 1] = 0;
                            mapFinal[(10 * i) + 5, (10 * j) - 1] = 0;
                        }
                    }
                    if (Random.Range(0, 100) <= 25)
                    {
                        mapFinal[(10 * i), (10 * j) + 4] = 0;
                        mapFinal[(10 * i), (10 * j) + 5] = 0;
                        if (i > 0)
                        {
                            mapFinal[(10 * i) - 1, (10 * j) + 4] = 0;
                            mapFinal[(10 * i) - 1, (10 * j) + 5] = 0;
                        }
                    }
                }

 
            }
        }
 
        }

    int Cycle()
    {
        int seedCount = 0;
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (roomNumber != 0)
                {
                    if (map[i, j].state == 1) //cell is in seed state
                    {
                        seedCount++;
                        int neighbourCount = 0;
                        if (map[i, j + 1].state == 0)
                        {
                            map[i, j].neighbours[0] = 0; neighbourCount++;
                        }
                        else { map[i, j].neighbours[0] = 4; }
                        if (map[i + 1, j].state == 0)
                        {
                            map[i, j].neighbours[1] = 1; neighbourCount++;
                        }
                        else { map[i, j].neighbours[1] = 4; }
                        if (map[i, j - 1].state == 0)
                        {
                            map[i, j].neighbours[2] = 2; neighbourCount++;
                        }
                        else { map[i, j].neighbours[2] = 4; }
                        if (map[i - 1, j].state == 0)
                        {
                            map[i, j].neighbours[3] = 3; neighbourCount++;
                        }
                        else { map[i, j].neighbours[3] = 4; }
                        //check neighbouring cells for expansion candidates
                        bool dirPickBool = false;
                        int dirpick;


                        while (dirPickBool == false)
                        {
                            if (neighbourCount == 0) { dirPickBool = true; }
                            dirpick = Random.Range(0, 4);
                            if (map[i, j].neighbours[dirpick] != 4)
                            {
                                map[i, j].inviteDir = map[i, j].neighbours[dirpick]; //assigns a random neighbour cell to invite into the network
                                dirPickBool = true;



                            }
                        }
                        if (neighbourCount == 0)
                        {
                            map[i, j].state = 3; //if there are no neighbours, mark cell as completed
                        }
                        else
                        {
                            map[i, j].state = 2; //if successfully found target to invite, set cell state to inviting
                        }
                    }
                    if (map[i, j].state == 2)//cell is in invitation state
                    {
                        roomNumber--;
                        if (map[i, j].inviteDir == 0) { map[i, j + 1].state = 1; map[i, j + 1].connectDir = 2; }
                        if (map[i, j].inviteDir == 1) { map[i + 1, j].state = 1; map[i + 1, j].connectDir = 3; }
                        if (map[i, j].inviteDir == 2) { map[i, j - 1].state = 1; map[i, j - 1].connectDir = 0; }
                        if (map[i, j].inviteDir == 3) { map[i - 1, j].state = 1; map[i - 1, j].connectDir = 1; } //connects ivited cell into network in seed state



                        if (Random.Range(0, 100) > 5)
                        {
                            map[i, j].state = 3; //once the target is connected there is a 95% chance the cell is marked as completed
                        }
                        else { map[i, j].state = 1; } //5% chance of returning to seed state, used for branching paths
                    }
                    if (map[i, j].state == 3)//cell is in completion state
                    {

                        bool isValid = false;

                        if (seedCount == 0)//if the desired number of rooms has not yet been reached, set a random valid completed cell to a seed cell
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (map[i, j].neighbours[k] != 4)
                                {
                                    isValid = true;
                                }
                            }
                            if (isValid && (Random.Range(0, 100) <= 5))
                            {
                                map[i, j].state = 1;
                            }
                        }

                    }
                }
            }
        }
        return seedCount;
    }


    void InitInteriors() //initialises pre built room interiors into indvidual arrays
    {
        interior1 = new int[8, 8] 
        {
        {1,1,1,0,0,1,1,1 },
        {1,1,1,0,0,1,1,1 },
        {1,1,1,0,0,1,1,1 },
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },
        {1,1,1,0,0,1,1,1 },
        {1,1,1,0,0,1,1,1 },
        {1,1,1,0,0,1,1,1 },
        };
        
        interior2 = new int[8, 8]
       {
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },
        {0,0,1,0,9,1,0,0 },
        {0,0,1,0,0,1,0,0 },
        {0,0,1,0,0,1,0,0 },
        {0,0,1,9,0,1,0,0 },
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },
       };

        interior3 = new int[8, 8]
        {
        {1,1,0,0,0,0,1,1 },
        {1,1,0,0,0,0,1,1 },
        {0,0,0,0,0,0,0,0 },
        {0,0,0,1,1,0,0,0 },
        {0,0,0,1,1,0,0,0 },
        {0,0,0,0,0,0,0,0 },
        {1,1,0,0,0,0,1,1 },
        {1,1,0,0,0,0,1,1 },
         };
        interior4 = new int[8, 8]
        {
        {0,0,0,0,0,0,0,0 },
        {0,1,1,0,0,1,1,0 },
        {0,1,0,0,0,0,1,0 },
        {0,0,0,1,1,0,0,0 },
        {0,0,0,1,1,0,9,0 },
        {0,1,0,0,0,0,1,0 },
        {0,1,1,0,0,1,1,0 },
        {0,0,0,0,0,0,0,0 },
        };
        interior5 = new int[8, 8]
        {
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },
        {0,0,1,1,1,1,0,0 },
        {0,0,1,1,1,1,0,0 },
        {0,0,1,1,1,1,0,0 },
        {0,0,1,1,1,1,0,0 },
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },
         };

        interior6 = new int[8, 8]
{
        {0,0,0,0,0,0,0,0 },
        {0,0,0,1,0,0,0,0 },
        {0,0,0,0,0,0,1,0 },
        {0,0,1,0,9,0,0,0 },
        {0,0,0,0,0,0,0,0 },
        {0,0,0,0,1,0,0,0 },
        {0,0,1,0,0,0,0,0 },
        {0,0,0,0,0,0,0,0 },
 };


    }

}
