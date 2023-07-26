using GameEngine.Rendering;

namespace GameEngine;

public static class MeshLoader
{
    public static Mesh Load(string path)
    {
        string[] lines = ReadFile(path);

        List<float> verticies = new();
        List<uint> indicies = new();

        
        bool HadVerts = false;
        bool HadInds = false;

        foreach(string line in lines)
        {
            string lineString = line.Clean();

            if (!HadVerts)
            {
                if (lineString.StartsWith("#verts"))
                {
                    HadVerts = true;
                }
                continue;
            }
            if (!HadInds)
            {
                if (lineString.StartsWith("#inds"))
                {
                    HadInds = true;
                    continue;
                }
            }
            
            // Skip if the line is a comment or if empty
            if (lineString.StartsWith("//") || lineString.IsEmpty())
            {
                continue;
            }
            if (!HadInds)
            {
                // Vertecies seperated by ","
                var values = lineString.Split(",");
                foreach (var v in values)
                {
                    if (v.IsEmpty())
                    {
                        continue;
                    }

                    string parsed = v.Replace(" ", "");
                    parsed = parsed.Replace("f", "");
                    parsed = parsed.Replace(",", "");
                    
                    float f = float.Parse(parsed);
                    
                    verticies.Add(f);
                }
            }
            else
            {
                // Indicies seperated by ","
                var values = lineString.Split(",");
                foreach (var v in values)
                {
                    if (v.IsEmpty())
                    {
                        continue;
                    }

                    string parsed = v.Replace(" ", "").Replace("(","").Replace(")", "");
                    parsed = parsed.Replace(",", "").Replace("[", "").Replace("]", "");

                    uint i = uint.Parse(parsed);

                    indicies.Add(i);
                }
            }


        }
        return new Mesh(verticies.ToArray(), indicies.ToArray(), path);
    }

    private static string[] ReadFile(string path)
    {
        return File.ReadAllLines(path);
    }
}