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

namespace videoDownload
{
    class videoDownload
    {
        // ////////////////////////////////////////////////////////////////////
#if OBSOLETE
        static void __GetVideosXX( String sDirOut, List<String> listVideoURL, CookieContainer cookieJarCoursera )
        {

            Directory.CreateDirectory( sDirOut );

            foreach(var sVideoURL in listVideoURL)
                {
                try
                    {
                    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create( sVideoURL );

                    webReq.AllowAutoRedirect = true;
                    //webReq.Method = "HEAD";
                    webReq.CookieContainer = cookieJarCoursera;

                    var wResp = webReq.GetResponse();

                    foreach( var sName in wResp.Headers.AllKeys)
                        {
                        Debug.Print( "<hdr name='{0}' val='{1}'/>", sName, wResp.Headers[ sName ] );
                        }

                    var contDisp = wResp.Headers[ "Content-Disposition" ];

                    Debug.Print( "<contDisp>{0}</contDisp>", contDisp );

                    var objCoursera = new CCoursera();
                    String sFileName = objCoursera.getFilenameFromContentDispositionHeader( contDisp );

                    Console.WriteLine( "<video filename='{0}'/>", sFileName );

                    String sPathMP4 = String.Format( "{0}\\{1}", sDirOut, sFileName );
                    Console.WriteLine( "<video sPathMP4='{0}'/>", sPathMP4 );

                    if( File.Exists( sPathMP4 ) )
                        {
                        Console.WriteLine( "<video.skip sPathMP4='{0}'/>", sPathMP4 );

                        wResp.Close();
                        continue;
                        }

                    ////Environment.Exit( 0 );

                    var strmResp = wResp.GetResponseStream();
                    using( FileStream outFile = new FileStream(sPathMP4, FileMode.Create))
                        {
                        byte [] rgbBuf = new byte [ 8192 ];
                        int cByte = 0;
                        while( (cByte = strmResp.Read( rgbBuf, 0, rgbBuf.Length )) > 0 )
                            {
                            outFile.Write(rgbBuf, 0, cByte);
                            }
                        }

                    ////Environment.Exit( 0 );
                    }
                catch( Exception Ex )
                    {
                    Console.WriteLine( "<Ex>{0}</Ex>", Ex );
                    }

                ////break;
                }
        }
#endif
        // ////////////////////////////////////////////////////////////////////
        static void Main(String[] rgsArgs)
        {
            Console.WriteLine( "<videoDownload rev='$Revision: 1.10 $'/>" );

            String sDirOut = "tmp";
            String sCookiesTxt = "coursera.cookies.txt";

            List<String> listVideoURL = new List<string>();

            for(int iA=0; iA<rgsArgs.Length; iA++)
                {
                String sArg = rgsArgs[ iA ];

                if( sArg == "--dir" )
                    {
                    sDirOut = rgsArgs[ ++iA ];
                    }
                else if( sArg == "--load.list" ) // load the list of URLs
                    {
                    String sFileListURL = rgsArgs[ ++iA ];

                    var listOfURLs = File.ReadAllLines( sFileListURL );
                    listVideoURL = new List<string>( listOfURLs );
#if ORIG_CODE_SAVE_FOR_NOW
                    using (StreamReader srFile = new StreamReader( sFileListURL ))
                        {
                        while( ! srFile.EndOfStream )
                            {
                            String sLine = srFile.ReadLine();
                            listVideoURL.Add( sLine );
                            }
                        }
#endif
                    listVideoURL.ForEach( u => Debug.Print( "<video url='{0}'/>", u ) );

                    sDirOut = String.Format( "tmp/{0}", sFileListURL );
                    Debug.Print( "<debug sDirOut='{0}'/>", sDirOut );
                    }
                else if( sArg == "--get.all" )
                    {
                    var objCoursera = new CCoursera();
                    var cookieJarCoursera = objCoursera.getCookieContainer( sCookiesTxt );

                    CVideo objVideo = new CVideo();
                    objVideo.GetVideos( delegate( XElement elemStatus ) { Console.WriteLine( "{0}", elemStatus ); }
                                , sDirOut, listVideoURL, cookieJarCoursera );
                    //__GetVideos( sDirOut, listVideoURL, cookieJarCoursera );
                    }
                }



        }
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
