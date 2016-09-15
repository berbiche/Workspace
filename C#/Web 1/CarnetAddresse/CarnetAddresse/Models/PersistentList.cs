using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CarnetAddresse.Models
{
    public class KeyAttribute : Attribute
    {
    }

    class Robot
    {
        static int nextId;
        public int RobotId { get; private set; }
        Robot()
        {
            RobotId = Interlocked.Increment(ref nextId);
        }
    }

    public class PersistentList<T> : List<T>
    {
        [NonSerialized]
        private static string fichier = HttpContext.Current.Server.MapPath("~/App_Data/" + typeof(T).Name + ".xml");
        private static string fichier_ids = HttpContext.Current.Server.MapPath("~/App_Data/" + typeof(T).Name + "_id.xml");

        public PersistentList()
        {
            if (File.Exists(fichier))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<T>));
                    using (StreamReader srd = new StreamReader(fichier))
                    {
                        this.AddRange(xs.Deserialize(srd) as List<T>);
                    }
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException("Le fichier XML est probablement corrompu, voir InnerException", e);
                }
            }
        }

        public void SaveChanges()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<T>));
            using (StreamWriter swr = new StreamWriter(fichier))
            {
                xs.Serialize(swr, this.GetRange(0, this.Count));
            }
        }

        private int NextId()
        {
            int nextid = 0;

            if (File.Exists(fichier_ids))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(int));
                    using (StreamReader srd = new StreamReader(fichier_ids))
                    {
                        nextid = (int)xs.Deserialize(srd);
                        nextid++;
                    }
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException("Le fichier XML pour les IDs est probablement corrompu, voir InnerException", e);
                }
            }
            else
            {
                nextid = 1;
            }

            XmlSerializer xs2 = new XmlSerializer(typeof(int));
            using (StreamWriter swr2 = new StreamWriter(fichier_ids))
            {
                xs2.Serialize(swr2, nextid);
            }

            return nextid;

        }

        new public void Add(T item)
        {
            PropertyInfo keyProp = item.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute))).First();
            if (keyProp == null) throw new InvalidOperationException("Une propriété doit posséder l'attribut Key");

            keyProp.SetValue(item, this.NextId());

            base.Add(item);
        }


    }
}