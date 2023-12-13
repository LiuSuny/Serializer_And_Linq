//#define DOUBLE_FOMART_SERIALIZATION_DESERIALIZATION 
//#define SOAP_FOMART_SERIALIZATION_DESERIALIZATION 
//#define XML_FOMART_SERIALIZATION_DESERIALIZATION 
#define JSON_FOMART_SERIALIZATION_DESERIALIZATION 
//#define LINQ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
//using System.Runtime.Serialization.Formatters.Soap;
//using System.Xml.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Xml.Linq;


namespace Serializer_And_Linq
{
    //1 type serializer format:Двойчный формат -Double fomart
    // 2 SOAP
    // 3 JSON
    // 4 XML
    [Serializable]
    public class Human /*: ISerialization*/
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [NonSerialized]
        const string Group = "Bv_321";

        public Human(int id)
        {
            ID = id;

        }

        public Human(int id, string name, int age)
        {
            ID = id;
            Name = name;
            Age = age;

        }
        public Human() //special for xml file
        {
            
        }
        public void Insert()
        {
            ID = Convert.ToInt32(Console.ReadLine());
            Name = Console.ReadLine();
            Age = Convert.ToInt32(Console.ReadLine());
        }
        public override string ToString()
        {
            return $"{ID} {Name} {Age} {Group}";
        }

    }
    public class Program
    {
        static void Main(string[] args)
        {
#if DOUBLE_FOMART_SERIALIZATION_DESERIALIZATION
            //Serialize is the best way to save our document to file
            //serialization has different type

            // 1 to use Двойчный формат-Double fomart  we need binary formart

            //Human human = new Human(1) { Name = "John", Age = 18 };
            ////Human test = new Human(1);
            ////test.insert();

            //BinaryFormatter bf = new BinaryFormatter();

            //try
            //{
            //    //  //Writing to file serialize
            //    //using (Stream fstr = File.Create("test.bin"))
            //    //{
            //    //    bf.Serialize(fstr, human); //if it array we us item 
            //    //}
            //    //Console.WriteLine("Ok");

            //    //Reading deserialize

            //    Human h_new = null;
            //    using (Stream fstr = File.OpenRead("test.bin"))
            //    {
            //        h_new = (Human)bf.Deserialize(fstr);
            //        Console.WriteLine(h_new);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}


            //Using list collection
            Human human1 = new Human(1) { Name = "John", Age = 18 };
            Human human2 = new Human(2) { Name = "John", Age = 18 };
            Human human3 = new Human(3) { Name = "John", Age = 18 };
            Human[] humans = new Human[] { human1, human2, human3 };
            //Human test = new Human(1);
            //test.insert();

            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                //Writing to file serialize
                //using (Stream fstr = File.Create("testcollection.bin"))
                //{
                //    bf.Serialize(fstr, humans); //if it array we us item 
                //}
                //Console.WriteLine("Ok");

                //Reading deserialize

                // Human h_new = null;
                using (Stream fstr = File.OpenRead("testcollection.bin"))
                {
                    Human h_new = (Human)bf.Deserialize(fstr);
                    foreach (Human item in humans)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
#endif



#if SOAP_FOMART_SERIALIZATION_DESERIALIZATION

            // 2  Soap - simple object access protocol
            //Note: the soap is almost the same thing with binaryformat just that it look like XML
            // and can read or write our file wether our property is private or public 

            //Human human = new Human(1) { Name = "John", Age = 18 };
            //Human test = new Human(1);
            // test.insert();

            SoapFormatter sf = new SoapFormatter();

            try
            {
                //Writing soap to file serialize
                //using (Stream fstr = File.Create("soaptest.bin"))
                //{
                //    sf.Serialize(fstr, human); //if it array we us item 
                //}
                //Console.WriteLine("Ok");

                //Reading soap deserialize

                Human h_new = null;
                using (Stream fstr = File.OpenRead("soaptest.bin"))
                {
                    h_new = (Human)sf.Deserialize(fstr);
                    Console.WriteLine(h_new);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

#endif


#if XML_FOMART_SERIALIZATION_DESERIALIZATION
            //  3. XML 

            Human human = new Human(1) { Name = "John", Age = 18 };
            //Human test = new Human(1);
            //test.insert();

            XmlSerializer bf = new XmlSerializer(typeof(Human));

            try
            {
                //  //Writing to file serialize
                using (Stream fstr = File.Create("Xmltest.xml"))
                {
                    bf.Serialize(fstr, human); //if it array we us item 
                }
                Console.WriteLine("Ok");

                //Reading deserialize

                //Human h_new = null;
                //using (Stream fstr = File.OpenRead("Xmltest.xml"))
                //{
                //    h_new = (Human)bf.Deserialize(fstr);
                //    Console.WriteLine(h_new);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        

#endif
#if JSON_FOMART_SERIALIZATION_DESERIALIZATION

            //4 JSON ---did not work

            Human human = new Human(2, "John", 18);
            //Human test = new Human(1);
            //test.insert();

            //string json_hum = new JsonSerializer.Serialize(human);
            // Console.WriteLine(json_hum);

            //string json_human = new JsonSerializer.Deserialize<Human>(json_hum);
            //Console.WriteLine(json_hum);


            using (FileStream fs = new FileStream("human.json", FileMode.OpenOrCreate))
            {
                Human h = new Human { ID = 1, Name = "Kate", Age = 70 };
                JsonSerializer.Serialize<Human>(fs, h);
                Console.WriteLine("Ok");
            }

            using (FileStream fs = new FileStream("human.json", FileMode.OpenOrCreate))
            {
                Human hum_new = JsonSerializer.Deserialize<Human>(fs);               
                Console.WriteLine($"{hum_new}");
            }

#endif


#if LINQ
            //LINQ-
            Human human1 = new Human(1, "John", 19);
            Human human2 = new Human(2, "John", 20);
            Human human3 = new Human(3, "John", 23);
            Human[] h_2 = new Human[] { human1, human2, human3 };
            //double[] money = { 23.6, 48.2, 50.7, 8.0, 30.1, 76.7 };
            //SELECT - this operator that we want to see
            //FROM - from where to see in these case from money
            //Where --
            //orderby - is for sorting
            IEnumerable<Human> query1 = from item in h_2
                                        where item.Age >= 20
                                        orderby item.Name descending
                                        select item;
            foreach (var item in query1)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine(); 
#endif
        }
                    
    }
}
