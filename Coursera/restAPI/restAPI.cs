using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Linq;
using System.Text;

using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mime;
using System.Xml.Linq;

using System.Runtime.Serialization.Formatters;
using System.Runtime;

using System.Web;
using System.Web.Script.Serialization;

using System.Threading;
using System.Threading.Tasks;

using System.Diagnostics;

using Newtonsoft.Json;

////using libCoursera;

namespace restAPI
{
    class restAPI
    {

        // http://tech.coursera.org/app-platform/catalog/index.html

        ////◦Courses: https://api.coursera.org/api/catalog.v1/courses
        ////◦Universities: https://api.coursera.org/api/catalog.v1/universities
        ////◦Categories: https://api.coursera.org/api/catalog.v1/categories
        ////◦Instructors: https://api.coursera.org/api/catalog.v1/instructors
        ////◦Sessions: https://api.coursera.org/api/catalog.v1/sessions

        static String getPrettyPrintedJson(String json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return( JsonConvert.SerializeObject(parsedJson, Formatting.Indented) );
        }
        // ////////////////////////////////////////////////////////////////////
        static void Main(String[] rgsArgs)
        {


            foreach(var sArg in rgsArgs)
                {
                if( sArg == "--db" )
                    {
                    Debug.Listeners.Add( new TextWriterTraceListener( Console.Out ) );
                    }
                else if( sArg == "--cat" )
                    {
                    String sURL = "https://api.coursera.org/api/catalog.v1/categories";

                    var sTextJSON = new WebClient().DownloadString( sURL );
                    Debug.Print( "<sTextJSON>{0}</sTextJSON>", sTextJSON );

                    Console.WriteLine( "{0}", getPrettyPrintedJson( sTextJSON ) );
                    }
                else if( sArg == "--sessions" )
                    {
                    String sURL = "https://api.coursera.org/api/catalog.v1/sessions";

                    var sTextJSON = new WebClient().DownloadString( sURL );
                    Debug.Print( "<sTextJSON>{0}</sTextJSON>", sTextJSON );

                    Console.WriteLine( "{0}", getPrettyPrintedJson( sTextJSON ) );
                    }
                else if( sArg == "--courses" )
                    {
                    String sURL = "https://api.coursera.org/api/catalog.v1/courses";

                    var sTextJSON = new WebClient().DownloadString( sURL );
                    Debug.Print( "<sTextJSON>{0}</sTextJSON>", sTextJSON );

                    Console.WriteLine( "{0}", getPrettyPrintedJson( sTextJSON ) );
                    }
                else
                    {
                    Debug.Assert( false, "Unrecognized arg! " + sArg );
                    }

                }



            Environment.Exit( 0 );


////            JavaScriptSerializer jsSer = new JavaScriptSerializer();
////            var dictResp = (Dictionary<string,object>)jsSer.Deserialize<Dictionary<string,object>>( sTextJSON );

////            Debug.Print( "<debug dictResp.Count='{0}'/>", dictResp.Count );

////            foreach(String sKey in dictResp.Keys)
////                {
////                Debug.Print( "\t<resp sKey='{0}'</resp>", sKey );
///////                    Debug.Print( "\t<resp sKey='{0}'>{1}</resp>", sKey, dictResp[ sKey ] );
////                }

////            var arList = (ArrayList )dictResp[ "elements" ];

////            Debug.Print( "\t<elem elem='{0}'</elem>", arList );
////            Debug.Print( "\t<elem elem='{0}'</elem>", arList.Count );

////            foreach(Dictionary<string,object> dictElem in arList)
////                {
////                Debug.Print( "\t<elem dictElem='{0}'</dictElem>", dictElem );
////                foreach(var key in dictElem.Keys)
////                    {
////                    Debug.Print( "\t<elem key='{0}'>{1}</elem>", key, dictElem[ key ] );
////                    }
////                }


        }
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
