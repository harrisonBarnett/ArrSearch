using System;

namespace ArrSearch
{
    public static class ArrSearch
    {
        // image dimensions
        static int dimensionX = 480;
        static int dimensionY = 640;
        // x,y iteration lengths for search functions
        static int searchX = 3;
        static int searchY = 2;
        // reset to 0 after iterations to stay within bounds of searchbox
        static int searchStartX = 0;
        static int searchStartY = 0;
        // image bitmap
        static Byte[,] arrayZ = new Byte[dimensionX, dimensionY];
        // search arr represents a letter to match in the image bitmap
        static Byte[,] search = new byte[searchY, searchX];

        static void Main(string[] args)
        {
            initialize(0xCC);
            scan(0xCC);
        }

        static public void initialize(Byte hex)
        {
            Byte[,] arr = new Byte[dimensionY, dimensionX];
            for(var i = 0; i < dimensionY; i++)
            {
                for(var j = 0; j < dimensionX; j++)
                {
                    arr[i, j] = 0x00;
                }
            }
            arr[200, 201] = hex;
            arr[200, 202] = hex;
            arr[200, 203] = hex;
            arr[201, 201] = hex;
            arr[201, 202] = hex;
            arr[201, 203] = hex;

            arrayZ = arr;

            search[0, 0] = 0xCC;
            search[0, 1] = 0xCC;
            search[0, 2] = 0xCC;
            search[1, 0] = 0xCC;
            search[1, 1] = 0xCC;
            search[1, 2] = 0xCC;

            Console.WriteLine($"Your array of dimensions {dimensionX}x{dimensionY} is ready...");
        }

        static public void scan(Byte hex)
        {
            Console.WriteLine("Scanning image...");

            List<string> matches = new List<string>();

            // iterate "down"
            for(int i = 0; i < dimensionY; i++)
            {
                // iterate "over"
                for(int j = 0; j < dimensionX; j++)
                {
                    if(arrayZ[i,j] == hex)
                    {
                        // we have a match at this index

                        // iterate over offset "down"
                        for(int k = i; k < i + searchY; k++)
                        {
                            // iterate over offset "over"
                            for(int z = j; z < j + searchX; z++)
                            {
                                if(arrayZ[k,z] == search[searchStartX, searchStartY] && !matches.Contains($"[{k}][{z}]"))
                                {
                                    // we have matched indices within the search "window"
                                    matches.Add($"[{k}][{z}]");
                                }
                            }
                        }
                    }
                }
            }
            matches.ForEach(match =>
            {
                Console.WriteLine(match);
            });
        }
    }
}