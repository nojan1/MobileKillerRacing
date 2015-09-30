using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KillerMobileRacing.Level
{
    class TrackPieceInfo
    {
        public string Name { get; set; }
        public int WidthX { get; set; }
        public int WidthZ { get; set; }
        public float Raise { get; set; }
        public float RotationOffset { get; set; }
    }

    static class TrackPiecesInfo
    {
        public static List<TrackPieceInfo> Pieces {
            get
            {
                var retVal = new List<TrackPieceInfo>();

                retVal.Add(new TrackPieceInfo
                {
                    Name = "STRAIGHT",
                    WidthX = 10,
                    WidthZ = 10,
                    Raise = 0,
                    RotationOffset = 90
                });

                retVal.Add(new TrackPieceInfo
                {
                    Name = "GOAL",
                    WidthX = 10,
                    WidthZ = 20,
                    Raise = 0,
                    RotationOffset = 0
                });

                retVal.Add(new TrackPieceInfo
                {
                    Name = "BEND_90_LEFT",
                    WidthX = 20,
                    WidthZ = 20,
                    Raise = 0,
                    RotationOffset = 0
                });

                retVal.Add(new TrackPieceInfo
                {
                    Name = "BEND_90_RIGHT",
                    WidthX = 20,
                    WidthZ = 20,
                    Raise = 0,
                    RotationOffset = 0
                });

                return retVal;
            }
        }
    }
}
