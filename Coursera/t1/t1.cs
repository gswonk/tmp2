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

using System.Diagnostics;


using libCoursera;

namespace t1
{
////145:
////        wget --content-disposition --load-cookies coursera.cookies.txt https://class.coursera.org/images-002/lecture/download.mp4?lecture_id=145

////83:
////        wget --content-disposition --load-cookies coursera.cookies.txt https://class.coursera.org/images-002/lecture/download.mp4?lecture_id=83


    class t1
    {
        // ////////////////////////////////////////////////////////////////////
        static CookieContainer getCookieContainer( String sPathCookiesTXT )
        {
            var cookieJarCoursera = new CookieContainer();
            using (StreamReader srFile = new StreamReader( sPathCookiesTXT ))
                {
                ////var sCookies = srFile.ReadToEnd();
                ////Debug.Print( "{0}", sCookies );

                while( ! srFile.EndOfStream )
                    {
                    String sLine = srFile.ReadLine();

                    var rgsLine = sLine.Split( '\t' );

                    Debug.Print( "<line len='{0}'>{1}</line>", rgsLine.Length, sLine );
                    if( rgsLine.Length == 7 )
                        {
                        var sDomain = rgsLine[ 0 ];
                        var sName = rgsLine[ 5 ];
                        var sCookie = rgsLine[ 6 ];

                        cookieJarCoursera.Add( new Cookie( sName, sCookie, "/", sDomain ) );
                        }
                    }

                //cookieContainer.

                ////var cc = new CookieCollection();

                ////cookieJarCoursera.GetCookies( new Uri( "file://coursera.cookies.txt" ) );
                ////var cc = cookieJarCoursera.GetCookies( new Uri( "file:///tmp/coursera.cookies.txt" ) );
                //var cc = cookieJarCoursera.GetCookies( new Uri( "file://cookies.txt" ) );

                //BinaryFormatter formatter = new BinaryFormatter();
                //Console.Out.WriteLine("Done.");
                //return (CookieContainer)formatter.Deserialize(stream);

                Debug.Print( "<cookieJarCoursera>{0}</cookieJarCoursera>", cookieJarCoursera );
                Debug.Print( "<debug cookieJarCoursera.Count='{0}'/>", cookieJarCoursera.Count );

                //Environment.Exit( 0 );
                }

            return( cookieJarCoursera );
        }

        // ////////////////////////////////////////////////////////////////////
        static void Main(string[] args)
        {
            Console.WriteLine( "<t1 rev='$Revision: 1.6 $'/>" );

            String sCookiesTxt = "coursera.cookies.txt";

            // http://stackoverflow.com/questions/1777203/c-writing-a-cookiecontainer-to-disk-and-loading-back-in-for-use
            var cookieJarCoursera = getCookieContainer( sCookiesTxt );
#if NODEF
            using (StreamReader srFile = new StreamReader(sCookiesTxt))
                {
                ////var sCookies = srFile.ReadToEnd();
                ////Debug.Print( "{0}", sCookies );

                while( ! srFile.EndOfStream )
                    {
                    String sLine = srFile.ReadLine();

                    var rgsLine = sLine.Split( '\t' );

                    Debug.Print( "<line len='{0}'>{1}</line>", rgsLine.Length, sLine );
                    if( rgsLine.Length == 7 )
                        {
                        var sDomain = rgsLine[ 0 ];
                        var sName = rgsLine[ 5 ];
                        var sCookie = rgsLine[ 6 ];

                        cookieJarCoursera.Add( new Cookie( sName, sCookie, "/", sDomain ) );
                        }
                    }

                //cookieContainer.

                ////var cc = new CookieCollection();

                ////cookieJarCoursera.GetCookies( new Uri( "file://coursera.cookies.txt" ) );
                ////var cc = cookieJarCoursera.GetCookies( new Uri( "file:///tmp/coursera.cookies.txt" ) );
                //var cc = cookieJarCoursera.GetCookies( new Uri( "file://cookies.txt" ) );

                //BinaryFormatter formatter = new BinaryFormatter();
                //Console.Out.WriteLine("Done.");
                //return (CookieContainer)formatter.Deserialize(stream);

                Debug.Print( "<cookieJarCoursera>{0}</cookieJarCoursera>", cookieJarCoursera );
                Debug.Print( "<debug cookieJarCoursera.Count='{0}'/>", cookieJarCoursera.Count );

                //Environment.Exit( 0 );
                }

            //Environment.Exit( 0 );
#endif

            ////var contents = new WebClient().DownloadString( "http://www.yahoo.com" );
            ////Debug.Print( "<contents>{0}</contents>", contents );


            StringBuilder sbURL = new StringBuilder()
                            .AppendFormat( "https://class.coursera.org" )
                            .AppendFormat( "/images-002/lecture" )
                            .AppendFormat( "/index" )
//                                .AppendFormat( "/download.mp4?lecture_id={0}", 145 )
                            ;

            Debug.Print( "<debug url='{0}'/>", sbURL );

            try
                {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create( sbURL.ToString() );
                webReq.CookieContainer = cookieJarCoursera;

                webReq.AllowAutoRedirect = true;
                var wResp = webReq.GetResponse();
                var srResp = new StreamReader( wResp.GetResponseStream() );
                while( ! srResp.EndOfStream )
                    {
                    String sLine = srResp.ReadLine();
                    Console.WriteLine( "{0}", sLine );
                    }
                }
            catch( Exception Ex )
                {
                Console.WriteLine( "<Ex>{0}</Ex>", Ex );
                }

        }
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
