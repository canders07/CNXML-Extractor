using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookCNXMLAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(@"F:\AthenaTextbooks\Elementary Algebra\collection.xml");
            string fileLocation = @"F:\AthenaTextbooks\Elementary Algebra\";
            XNamespace xmlns = "http://cnx.rice.edu/collxml";
            XNamespace cnx = "http://cnx.rice.edu/cnxml";
            XNamespace cnxorg = "http://cnx.rice.edu/system-info";
            XNamespace md = "http://cnx.rice.edu/mdml";
            XNamespace col = "http://cnx.rice.edu/collxml";
            XNamespace cnxml = "http://cnx.rice.edu/cnxml";
            XNamespace m = "http://www.w3.org/1998/Math/MathML"; //Math XML Stuff
            XNamespace q = "http://cnx.rice.edu/qml/1.0";
            XNamespace xhtml = "http://www.w3.org/1999/xhtml";
            XNamespace bib = "http://bibtexml.sf.net/";
            XNamespace cc = "http://bibtexml.sf.net/";
            XNamespace rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";


            //var titles = from t in doc.Descendants()
            //             where t.Name.LocalName == "fullname"
            //             select t;

            //foreach (var t in titles)
            //{
            //    Console.WriteLine(t);
            //}
            

            var fileName = doc.Descendants(col + "module");
           
            foreach (var file in fileName)
            {

                var title = file.Element(md + "title").Value;
                var docName = file.Attribute("document").Value;
                
                LoadCNXML(docName);
                
            }


            Console.ReadKey();
        }

            public static void LoadCNXML(string docName)
        {
            XDocument indexLocation = XDocument.Load(@"F:\AthenaTextbooks\Elementary Algebra\"+docName+@"\index.cnxml");
            XNamespace xmlns = "http://cnx.rice.edu/cnxml";
            XNamespace md = "http://cnx.rice.edu/mdml/0.4";
            XNamespace bib = "http://bibtexml.sf.net/";
            XNamespace m = "http://www.w3.org/1998/Math/MathML"; //Math XML Stuff
            XNamespace q = "http://cnx.rice.edu/qml/1.0";

            var doc = indexLocation.Element(xmlns + "document");
            var content = doc.Element(xmlns + "content");


            var list = content.Element(xmlns + "list");
            var title = doc.Element(xmlns + "title").Value;
            Console.WriteLine(docName);
            Console.WriteLine(title);
            Console.WriteLine();
            try //pull data from non learning chapters
            {
                var items = list.Elements(xmlns + "item");

                Console.WriteLine("Chapter Content:");

                foreach (var item in items)
                {

                    Console.WriteLine(item.Value);
                }
            }
            catch (NullReferenceException e)
            {

            }
            try // pull section data
            {
                var sections = content.Elements(xmlns + "section");
                foreach (var section in sections)
                {
                    try
                    {
                        var sectTitle = section.Element(xmlns + "title").Value;

                        Console.WriteLine("Section Title: " + sectTitle);

                    }
                    catch (NullReferenceException e)
                    {

                    }

                    try // overview title
                    {
                        var sectionItems = section.Element(xmlns + "list").Elements(xmlns + "item");
                        foreach(var item in sectionItems)
                        {
                            Console.WriteLine();
                            Console.WriteLine(item.Value);
                            Console.WriteLine();
                        }
                    }
                        
                    catch (NullReferenceException e)
                    {

                    }

                    try
                    {
                        var para = section.Elements(xmlns + "para");
                    foreach (var paragraph in para) //find actual chapter data
                    {
                       
                            var sectLists = paragraph.Elements(xmlns + "list");
                            Console.WriteLine();
                            Console.WriteLine(paragraph.Value);
                            Console.WriteLine();
                            try
                            {
                                foreach (var sectList in sectLists)
                                {
                                    var item = sectList.Element(xmlns + "item");
                                    Console.WriteLine();
                                    Console.WriteLine(item.Value);
                                    Console.WriteLine();
                                }
                            }
                            catch (NullReferenceException e)
                            { 
                            }
                            try
                            {
                                var examples = section.Elements(xmlns + "example");
                                foreach (var example in examples)
                                {

                                    Console.WriteLine();
                                    Console.WriteLine(example.Value);
                                    Console.WriteLine();
                                }
                            }

                            catch (NullReferenceException e)
                            {

                            }
                            try
                            {

                                var problems = paragraph.Elements(xmlns + "problem");
                                int pcount = 0;
                                foreach (var problem in problems)
                                {
                                    pcount++;
                                    Console.WriteLine();
                                    Console.WriteLine("Problem " + pcount + ": ");
                                    Console.WriteLine();
                                    Console.WriteLine(problem.Value);
                                    Console.WriteLine();
                                }
                            }
                            catch (NullReferenceException e)
                            {

                            }

                        }
                    }
                    catch (NullReferenceException e)
                    {

                    }
                    





                }

            }
            catch (NullReferenceException e)
            {

            }

            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");

        }


    }
}
