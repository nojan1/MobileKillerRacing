using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KillerMobileRacing.Level
{
    public class LevelAutoGenerator
    {
        public Course Generate(float areaRadius)
        {
            var course = new Course();
            var trackPieces = new List<CourseTrackpiece>();

            float maxRadius = areaRadius;
            float minRadius = areaRadius * 0.8f;
            int numAnchors = Random.Range(4, 6);         
            
            PlaceGoal(course, trackPieces);
            var anchorList = PlaceAnchors(numAnchors, minRadius, maxRadius);
            ConnectAnchors(trackPieces, anchorList);
            
            course.Track = trackPieces.ToArray();
            return course;
        }

        private void ConnectAnchors(List<CourseTrackpiece> trackPieces, List<Vector2> anchorList)
        {
            var currentPoint = new Vector2(0, 0);
            float currentDirection = 0f;
            bool firstPiece = true;

            while (anchorList.Count > 0)
            {
                var currentAnchor = anchorList.First(a => a.magnitude == anchorList.Min(a2 => a2.magnitude));

                if (firstPiece)
                {
                    firstPiece = false;
                    currentPoint.x += TrackPiecesInfo.Pieces.Single(p => p.Name == "GOAL").WidthX / 2;

                    if (currentAnchor.y < 0)
                        currentDirection = 180f;
                }

                
       
                //Needs to build a road between currentPoint and currentAnchor without hitting or crossing anything

                //TODO: Factor in roadsize
                if(((currentDirection == 0 || currentDirection == 180) && currentPoint.y == currentAnchor.y) ||
                    ((currentDirection == 90 || currentDirection == 270) && currentPoint.x == currentAnchor.x))
                {
                    //Straight line
                    BuildLine(currentPoint, currentAnchor, trackPieces, ref currentDirection);
                }
                else
                {
                    //Needs 1 (or more) bends
                    BuildBendLine(currentPoint, currentAnchor, trackPieces, ref currentDirection);
                }

                anchorList.Remove(currentAnchor);
            }
        }

        private Vector2 BuildBendLine(Vector2 currentPoint, Vector2 currentAnchor, List<CourseTrackpiece> trackPieces, ref float currentDirection)
        {
            //TODO: Will one bend be enough? How do we now?
            return currentPoint;
        }

        private Vector2 BuildLine(Vector2 currentPoint, Vector2 currentAnchor, List<CourseTrackpiece> trackPieces, ref float currentDirection)
        {
            var straightPiece = TrackPiecesInfo.Pieces.First(p => p.Name == "STRAIGHT");
            currentDirection = Vector2.Angle(currentPoint, currentAnchor);

            float initialDistance = Vector2.Distance(currentPoint, currentAnchor);

            while (Vector2.Distance(currentPoint, currentAnchor) >= straightPiece.WidthZ && Vector2.Distance(currentPoint, currentAnchor) <= initialDistance) 
            {
                trackPieces.Add(new CourseTrackpiece
                {
                    Id = trackPieces.Count,
                    PosX = (int)currentPoint.x,
                    PosY = 0,
                    PosZ = (int)currentPoint.y,
                    Rotation = (int)currentDirection,
                    PieceName = "STRAIGHT"
                });

                currentPoint = new Vector2(
                        Mathf.Round(Mathf.Cos(currentDirection) * straightPiece.WidthX) + currentPoint.x,
                        Mathf.Round(Mathf.Sin(currentDirection) * straightPiece.WidthZ) + currentPoint.y
                    );
            }

            return currentPoint;
        }

        private void PlaceGoal(Course course, List<CourseTrackpiece> trackPieces)
        {
            trackPieces.Add(new CourseTrackpiece
            {
                Id = 0,
                PosX = 0,
                PosY = 0,
                PosZ = 0,
                Rotation = 0,
                PieceName = "GOAL"
            });

            course.Spawnpoint = new CourseSpawnpoint();
            course.Spawnpoint.Direction = 0;
            course.Spawnpoint.SpawnAt = 0;
        }

        private List<Vector2> PlaceAnchors(int numAnchors, float minRadius, float maxRadius)
        {
            var anchorList = new List<Vector2>();

            for (int i = 0; i < numAnchors; i++)
            {
                float currentRadius = Random.Range(minRadius, maxRadius);
                float currentAngle = Mathf.Deg2Rad * (360f / numAnchors) * i;

                var pos = new Vector2(
                        Mathf.Cos(currentAngle) * currentRadius,
                        Mathf.Sin(currentAngle) * currentRadius
                    );

                pos.x = pos.x - (pos.x % 10);
                pos.y = pos.y - (pos.y % 10);

                anchorList.Add(pos);
            }

            return anchorList;
        }
    }
}
