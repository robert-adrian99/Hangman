using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hangman.ViewModels;
using Hangman.Models;

namespace Hangman.Helps
{
    class SerializationActions
    {
        public void SerializeUsers(string xmlFileName, UsersList entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(UsersList));
            FileStream fileStr = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }
        public UsersList DeserializeUsers(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(UsersList));
            FileStream file = new FileStream(xmlFileName, FileMode.Open);
            var entity = xmlser.Deserialize(file);
            file.Dispose();
            return entity as UsersList;
        }


        public void SerializeWords(string xmlFileName, Words entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Words));
            FileStream fileStr = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }
        public Words DeserializeWords(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Words));
            FileStream file = new FileStream(xmlFileName, FileMode.Open);
            var entity = xmlser.Deserialize(file);
            file.Dispose();
            return entity as Words;
        }

        public void SerializeImages(string xmlFileName, Images entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Images));
            FileStream fileStr = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }
        public Images DeserializeImages(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(Images));
            FileStream file = new FileStream(xmlFileName, FileMode.Open);
            var entity = xmlser.Deserialize(file);
            file.Dispose();
            return entity as Images;
        }
    }
}
