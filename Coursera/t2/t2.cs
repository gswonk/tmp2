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


namespace t2
{
    class t2
    {

////83:
////        wget --content-disposition --load-cookies coursera.cookies.txt https://class.coursera.org/images-002/lecture/download.mp4?lecture_id=83


        static void Main(string[] args)
        {
            Console.WriteLine( "<t2 rev='$Revision: 1.4 $'/>" );

            String sCookiesTxt = "coursera.cookies.txt";

            int nLectureID = 145;

            StringBuilder sbURL = new StringBuilder()
                            .AppendFormat( "https://class.coursera.org" )
                            .AppendFormat( "/images-002/lecture" )
                            .AppendFormat( "/download.mp4?lecture_id={0}", nLectureID )
                            ;

            Debug.Print( "<debug url='{0}'/>", sbURL );

            String sContDisp = @"attachment; filename=""8%20-%201%20-%201%20-%20Introduction%20to%20Sparse%20Modeling%20-%20Part%201%20-%20Duration%2010%3A39.mp4""; filename*=UTF-8''8%20-%201%20-%201%20-%20Introduction%20to%20Sparse%20Modeling%20-%20Part%201%20-%20Duration%2010%3A39.mp4";
            Console.WriteLine( "{0}", sContDisp );

            {
            var objCoursera = new CCoursera();
            String sFileMP4 = objCoursera.getFilenameFromContentDispositionHeader( sContDisp );

            Console.WriteLine( "<debug sFileMP4='{0}'/>", sFileMP4 );
            }

            Environment.Exit( 0 );

            foreach(var contDisp in sContDisp.Split( ';' ))
                {
                Console.WriteLine( "--> {0}", contDisp );
                foreach(var nameVal in contDisp.Split( '=' ))
                    {
                    Console.WriteLine( "-----> '{0}'", nameVal );

                    if( nameVal.Trim() == "filename" )  // watch out for leading space!
                        {
                        Console.WriteLine( "=======> {0}", contDisp.Split( '=' )[ 1 ] );
                        String sFilenameEncodedMP4 = contDisp.Split( '=' )[ 1 ];
                        String sFilenameMP4 = WebUtility.HtmlDecode( sFilenameEncodedMP4 );

                        Console.WriteLine( "<mp4 sFilenameEncodedMP4='{0}'/>", sFilenameEncodedMP4 );
                        Console.WriteLine( "<mp4 sFilenameMP4='{0}'/>", sFilenameMP4 );
                        Console.WriteLine( "<mp4 sFilenameMP4='{0}'/>", Uri.UnescapeDataString( sFilenameEncodedMP4 ) );
                        ////Console.WriteLine( "<mp4 sFilenameMP4='{0}'/>", Uri.HexUnescape( sFilenameEncodedMP4, 0 ) );
                        }   
                    }
                
                }
            Environment.Exit ( 0 );

            try
                {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create( sbURL.ToString() );

                CCoursera objCoursera = new CCoursera();
                var cookieJarCoursera = objCoursera.getCookieContainer( sCookiesTxt );
                webReq.CookieContainer = cookieJarCoursera;

                webReq.AllowAutoRedirect = true;
                var wResp = webReq.GetResponse();

                foreach( var sName in wResp.Headers.AllKeys)
                    {
                    Debug.Print( "<hdr name='{0}' val='{1}'/>", sName, wResp.Headers[ sName ] );
                    }

                var contDisp = wResp.Headers[ "Content-Disposition" ];

                Console.WriteLine( "<contDisp>{0}</contDisp>", contDisp );

                Environment.Exit( 0 );



                var strmResp = wResp.GetResponseStream();
                String sFileOut = String.Format( "tmp.{0}.mp4", nLectureID );
                FileStream outFile = new FileStream(sFileOut, FileMode.Create);

                byte [] rgbBuf = new byte [ 8192 ];
                int cByte = 0;
                while( (cByte = strmResp.Read( rgbBuf, 0, rgbBuf.Length )) > 0 )
                    {
                    outFile.Write(rgbBuf, 0, cByte);
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
