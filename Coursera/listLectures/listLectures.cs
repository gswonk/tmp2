using System;
using System.Collections.Generic;
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

using System.Diagnostics;

using libCoursera;


namespace listLectures
{
    class listLectures
    {

        // http://tech.coursera.org/app-platform/catalog/index.html

        ////◦Courses: https://api.coursera.org/api/catalog.v1/courses
        ////◦Universities: https://api.coursera.org/api/catalog.v1/universities
        ////◦Categories: https://api.coursera.org/api/catalog.v1/categories
        ////◦Instructors: https://api.coursera.org/api/catalog.v1/instructors
        ////◦Sessions: https://api.coursera.org/api/catalog.v1/sessions


        // ////////////////////////////////////////////////////////////////////////////////
        static void __GetLectureIndexHTML( String sDirOut, String sNameClass, String sPathCookiesTxt )
        {
            try
                {
                CCoursera objCoursera = new CCoursera();
                var cookieJarCoursera = objCoursera.getCookieContainer( sPathCookiesTxt );

                CIndex objIndex = new CIndex();
                String sDocHTML = objIndex.getLectureIndex( sNameClass, cookieJarCoursera );

                Directory.CreateDirectory( sDirOut );
                String sFileHTML = String.Format( "{0}/{1}.html", sDirOut, sNameClass );
                StreamWriter swOutHTML = new StreamWriter( sFileHTML );

                swOutHTML.Write( sDocHTML );
                }
            catch( Exception Ex )
                {
                Console.WriteLine( "<Ex>{0}</Ex>", Ex );
                }
        }
        // ////////////////////////////////////////////////////////////////////////////////
        static void Main(String[] rgsArgs)
        {
            Debug.Print( "<listLectures rev='$Revision: 1.25 $'/>" );


            //Debug.Print( "<debug url='{0}'/>", sbURL );

            Dictionary<String,String> dictClasses = new Dictionary<string,string>()
                            {
                                { "--alg", "algs4partI-004" }
                                , { "--calculus", "calcsing-002" }
                                , { "--circuits", "circuits-001" }
                                , { "--crypto", "crypto" }
                                , { "--compphoto", "compphoto-001" }
                                , { "--dynamics", "dynamics1-001" }
                                , { "--gamify", "gamification-003" }
                                , { "--gameprog", "gameprogramming-001" }
                                , { "--guitar", "guitar-001" }
                                , { "--images", "images-002" }
                                , { "--infosec", "infosec-003" }
                                , { "--math", "mathematicalmethods-001" }
                                , { "--matrix", "matrix-001" }
                                , { "--think", "thinkagain-003" }
                            };

            dictClasses.Keys.ToList().ForEach( k => Debug.Print( "<dict k='{0}' v='{1}'/>", k, dictClasses[ k ] ) );
            ////Environment.Exit( 0 );

            String sDirHTML = "html";
            String sCookiesTxt = "coursera.cookies.txt";

            String sNameClass = "images-002";

            for(int iA=0; iA<rgsArgs.Length; iA++)
                {
                String sArg = rgsArgs[ iA ];

                if( sArg == "--db" )
                    {
                    Debug.Listeners.Add( new TextWriterTraceListener( Console.Out ) );
                    }
                else if( sArg == "--dir" )
                    {
                    sDirHTML = rgsArgs[ ++iA ];
                    }
                else if( sArg == "--class" )
                    {
                    sNameClass = rgsArgs[ ++iA ];
                    }
                else if( sArg == "--list" )
                    {
                    dictClasses.Keys.ToList().ForEach( k => Console.WriteLine( "<opt k='{0}' v='{1}'/>", k, dictClasses[ k ] ) );
                    }
                else if( dictClasses.Keys.Contains( sArg ) )
                    {
                    sNameClass = dictClasses[ sArg ];
                    }
                else if( sArg == "--get.all" )
                    {
                    dictClasses.Keys.ToList()
                            .ForEach( opt => __GetLectureIndexHTML( sDirHTML, dictClasses[ opt ], sCookiesTxt ) );
                    }
                else if( sArg == "--get" )  // the index of the HTML file
                    {
                    __GetLectureIndexHTML( sDirHTML, sNameClass, sCookiesTxt );
                    }
                else
                    {
                    Debug.Assert( false, "Invalid arg!" );
                    }
                }
            Environment.Exit( 0 );

        }
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
