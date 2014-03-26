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

namespace libCoursera
{
    public class CCoursera
    {

        public String getClassIndexURL( String sNameClass )
        {
            StringBuilder sbURL = new StringBuilder()
                            .AppendFormat( "https://class.coursera.org" )
                            .AppendFormat( "/{0}/lecture", sNameClass )
////                            .AppendFormat( "/images-002/lecture" )
                            .AppendFormat( "/index" )
                            ;

            return( sbURL.ToString() );
        }
        // ////////////////////////////////////////////////////////////////////
        public String getFilenameFromContentDispositionHeader( String sContDisp )
        {

            String sFilename = null;

            foreach(var contDisp in sContDisp.Split( ';' ))
                {
                Debug.Print( "--> {0}", contDisp );

                foreach(var nameVal in contDisp.Split( '=' ))
                    {
                    Debug.Print( "-----> '{0}'", nameVal );

                    if( nameVal.Trim() == "filename" )  // watch out for leading space!
                        {
                        Debug.Print( "=======> {0}", contDisp.Split( '=' )[ 1 ] );
                        String sFilenameEncodedMP4 = contDisp.Split( '=' )[ 1 ];
                        String sFilenameMP4 = WebUtility.HtmlDecode( sFilenameEncodedMP4 );

                        Debug.Print( "<mp4 sFilenameEncodedMP4='{0}'/>", sFilenameEncodedMP4 );
                        Debug.Print( "<mp4 sFilenameMP4='{0}'/>", sFilenameMP4 );
                        Debug.Print( "<mp4 sFilenameMP4='{0}'/>", Uri.UnescapeDataString( sFilenameEncodedMP4 ) );

                        sFilename = Uri.UnescapeDataString( sFilenameEncodedMP4 );

                        sFilename = sFilename.Replace( "\"", "" );  // do not include quotes!

                        sFilename = sFilename.Replace( ":", "." );  // do not include colon!

                        sFilename = sFilename.Replace( "?", "." );  // do not include question mark!

                        sFilename = sFilename.Replace( "'", "" );  // do not include quotes!

                        sFilename = sFilename.Replace( "\\", "" );  // do not include backslash!
                        sFilename = sFilename.Replace( "/", "" );  // do not include backslash!

                        ////Console.WriteLine( "<mp4 sFilenameMP4='{0}'/>", Uri.HexUnescape( sFilenameEncodedMP4, 0 ) );
                        }   
                    }
                
                }

            return( sFilename );
        }
        // ////////////////////////////////////////////////////////////////////
        public CookieContainer getCookieContainer( String sPathCookiesTXT )
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

    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
