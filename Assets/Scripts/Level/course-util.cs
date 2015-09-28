using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KillerMobileRacing.Level
{
    partial class Course
    {
        public CourseTrackpiece SpawnTrackpiece {
            get
            {
                return Track.FirstOrDefault(tp => tp.Id == Spawnpoint.SpawnAt);
            }
        }
    }
}