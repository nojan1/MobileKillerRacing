using UnityEngine;
using System.Collections;
using KillerMobileRacing.Level;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class UITrackPiece
{
    public string Name;
    public GameObject Prefab;
}

public class LevelCreator : MonoBehaviour {

    public UITrackPiece[] TrackPieces;
    public TextAsset CourseFile;

	// Use this for initialization
	void Start () {
        // var parser = new CourseParser();
        //var course = parser.ParseContent(CourseFile.text);

        var course = new LevelAutoGenerator().Generate(50f);

        foreach(var piece in course.Track)
        {
            var piecePrefab = TrackPieces.FirstOrDefault(p => p.Name == piece.PieceName);
            if(piecePrefab != null)
            {
                var position = new Vector3(piece.PosX, piece.PosY, piece.PosZ);
                var rotation = Quaternion.Euler(new Vector3(0,
                                                            piece.Rotation,
                                                            0));

                Instantiate(piecePrefab.Prefab, position, rotation);
            }
            else
            {
                Debug.Log("Track piece " + piece.PieceName + " was not found. Ignoring....");
            }
        }

        //Place player
        var player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            player.transform.position = new Vector3(course.SpawnTrackpiece.PosX,
                                                    course.SpawnTrackpiece.PosY,
                                                    course.SpawnTrackpiece.PosZ);
            
            player.transform.rotation = Quaternion.Euler(new Vector3(0, (float)course.Spawnpoint.Direction, 0));
        }

	}

}
