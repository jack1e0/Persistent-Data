using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/*
 * Bridge between json file that is saved in default Unity dir, and CircleData
 * 
 * Load() returns latest saved version of CircleData
 * Save() updates json file with new CircleData
 */

public class FileDataHandler
{
    private string dirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dirPath, string dataFileName)
    {
        this.dirPath = dirPath;
        this.dataFileName = dataFileName;
    }

    public CircleData Load()
    {
        //use Path.Combine to account for dif separators for dif OS
        string fullPath = Path.Combine(dirPath, dataFileName);

        CircleData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize into CircleData
                loadedData = JsonUtility.FromJson<CircleData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        return loadedData;
    }

    public void Save(CircleData data)
    {
        string fullPath = Path.Combine(dirPath, dataFileName);

        try
        {
            //create directory, if it doesnt already exist
            Directory.CreateDirectory(dirPath);

            //serialize circle data into json text
            string dataToStore = JsonUtility.ToJson(data, true);
          
            //update file with new serialized data
            //when reading/ writing, use USING blocks, as it ensures streams are closed after use
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            

        } 
        catch (Exception e) 
        {       
            Debug.LogException(e);
        }
    }
}
