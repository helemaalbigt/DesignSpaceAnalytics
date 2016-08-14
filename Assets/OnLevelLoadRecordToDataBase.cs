using UnityEngine;
using System.Collections;
using System;

public class OnLevelLoadRecordToDataBase : MonoBehaviour {

    public LookPathToMySQLAsJson _dbAccess;
    public LookAtPointRecorder _pointRecorded;

        void Start () {

        LoadScene.OnStartLoadWithDelay += RecordPath;
	
	    }

    public void RecordPath(string currentScene, string nextScene, float delay          )
        {
            if(_dbAccess && _pointRecorded)
            _dbAccess.PostLookPathData(_pointRecorded._lookPathAffected);
         }
}
