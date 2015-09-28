using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KillerMobileRacing.Level
{
    class CourseParser
    {

        public CourseParser()
        {

        }

        public Course Parse(string courseFilePath)
        {
            var file = File.OpenRead(courseFilePath);
            return ParseStream(file);
        }

        public Course ParseContent(string xmlContent)
        {
            var stringBytes = Encoding.Default.GetBytes(xmlContent);
            var ms = new MemoryStream(stringBytes);
            return ParseStream(ms);
        }

        private Course ParseStream(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(Course));
            return serializer.Deserialize(stream) as Course;
        }
    }
}
