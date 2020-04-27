using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hangman.ViewModels;

namespace Hangman.Services
{
    class SerializationActions
    {
        public void SerializeSignInVM(string xmlFileName, SignInViewModel entity)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(SignInViewModel));
            FileStream fileStr = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fileStr, entity);
            fileStr.Dispose();
        }

        public SignInViewModel DeserializeSignInVM(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(SignInViewModel));
            FileStream file = new FileStream(xmlFileName, FileMode.Open);
            var entity = xmlser.Deserialize(file);
            file.Dispose();
            return entity as SignInViewModel;
        }
    }
}
