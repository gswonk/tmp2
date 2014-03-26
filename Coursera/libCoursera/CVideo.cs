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

namespace libCoursera
{
    public class CVideo
    {

        // ////////////////////////////////////////////////////////////////////
        public void GetVideos( Action<XElement> callbackNotify
                                    , String sDirOut
                                    , List<String> listVideoURL
                                    , CookieContainer cookieJarCoursera )
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

                    ////Console.WriteLine( "<video filename='{0}'/>", sFileName );
                    callbackNotify( new XElement( "video", new XAttribute( "filename", sFileName ) ) );

                    String sPathMP4 = String.Format( "{0}\\{1}", sDirOut, sFileName );
                    ////Console.WriteLine( "<video sPathMP4='{0}'/>", sPathMP4 );
                    callbackNotify( new XElement( "video", new XAttribute( "sPathMP4", sPathMP4 ) ) );

                    if( File.Exists( sPathMP4 ) )
                        {
                        ////Console.WriteLine( "<video.skip sPathMP4='{0}'/>", sPathMP4 );
                        callbackNotify( new XElement( "video.skip", new XAttribute( "sPathMP4", sPathMP4 ) ) );

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
                    ////Console.WriteLine( "<Ex>{0}</Ex>", Ex );
                    callbackNotify( new XElement( "Ex", Ex.ToString() ) );
                    }

                ////break;
                }
        }

        // ////////////////////////////////////////////////////////////////////
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
