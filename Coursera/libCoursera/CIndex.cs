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
    public class CIndex
    {

            public String getLectureIndex( String sNameClass, CookieContainer cookieJarCoursera )
            {
                StringBuilder sbURL = new StringBuilder()
                                .AppendFormat( "https://class.coursera.org" )
                                .AppendFormat( "/{0}/lecture", sNameClass )
                                .AppendFormat( "/index" )
                                ;

                String sLectureURL = sbURL.ToString();
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create( sLectureURL );

                ////CCoursera objCoursera = new CCoursera();
                ////var cookieJarCoursera = objCoursera.getCookieContainer( sCookiesTxt );
                webReq.CookieContainer = cookieJarCoursera;

                webReq.AllowAutoRedirect = true;
                var wResp = webReq.GetResponse();
                var srResp = new StreamReader( wResp.GetResponseStream() );

                //String sFileHTML = String.Format( "{0}.html", sNameClass );
                //StreamWriter swOutHTML = new StreamWriter( sFileHTML );

                StringBuilder sbDoc = new StringBuilder();
                while( ! srResp.EndOfStream )
                    {
                    String sLine = srResp.ReadLine();
                    //swOutHTML.WriteLine( "{0}", sLine );
                    Debug.Print( "{0}", sLine );
                    sbDoc.AppendFormat( "{0}\n", sLine );
                    }

                return( sbDoc.ToString() );
            }
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
