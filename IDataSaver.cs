using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Bridge between CircleData and Circle (can be generalised if more objects need to be saved and loaded)
 * 
 * LoadData() updates behaviour in game using CircleData passed in
 * SaveData() updates CircleData passed in using new data from current progression of game
 */

public interface IDataSaver
{
    void LoadData(CircleData data);

    //passing by reference allow this method to modify data, instead of just reading it
    void SaveData(ref CircleData data);
}
