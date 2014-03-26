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

using HtmlAgilityPack;

using libCoursera;

namespace extractURLs
{
    class extractURLs
    {

        // ////////////////////////////////////////////////////////////////////
        static void ExtractLink( Action<String> actionProc, String sPathHTML, Regex reMatch )
        {
            //
            // http://htmlagilitypack.codeplex.com/wikipage?title=Examples
            //
            HtmlDocument docIndex = new HtmlDocument();
            docIndex.Load( sPathHTML );
            foreach(HtmlNode link in docIndex.DocumentNode.SelectNodes( "//a[@href]" ))
                {
                HtmlAttribute attHref = link.Attributes["href"];

                if( reMatch.IsMatch( attHref.Value ) )
                    {
                    Debug.Print( "<link att='{0}'/>", attHref.Value );
                    //Console.WriteLine( "{0}", attHref.Value );
                    actionProc( attHref.Value );
                    }
                }

        }
        // ////////////////////////////////////////////////////////////////////
        static void Main(String[] rgsArgs)
        {
            Debug.Print( "<extractURLs rev='$Revision: 1.5 $'/>" );

            for(int iA=0; iA<rgsArgs.Length; iA++)
                {
                String sArg = rgsArgs[ iA ];

                if( sArg == "--db" )
                    {
                    Debug.Listeners.Add( new TextWriterTraceListener( Console.Out ) );
                    }
                else if( sArg == "--mp4" )
                    {
                    String sFileHTML = rgsArgs[ ++iA ];
                    Debug.Assert( sFileHTML.Contains( ".html" ) );

                    Action<String> procLink = delegate( String sLink )
                                                {
                                                Console.WriteLine( "{0}", sLink );
                                                };
                    ExtractLink( procLink, sFileHTML, new Regex( ".mp4" ) );
                    }
                else if( sArg == "--all" )
                    {
                    String sFileHTML = rgsArgs[ ++iA ];
                    Debug.Assert( sFileHTML.Contains( ".html" ) );

                    Action<String> procLink = delegate( String sLink )
                                                {
                                                Console.WriteLine( "{0}", sLink );
                                                };
                    ExtractLink( procLink, sFileHTML, new Regex( "https://.*" ) );
                    }
                else if( sArg == "--xml.strip" )
                    {
                    Debug.Assert( false, "Obsolete, do not use!!!" );

                    String sFileHTML = rgsArgs[ ++iA ];
                    Debug.Assert( sFileHTML.Contains( ".html" ) );

                    //
                    // http://htmlagilitypack.codeplex.com/wikipage?title=Examples
                    //
                    HtmlDocument docIndex = new HtmlDocument();
                    docIndex.Load( sFileHTML );
                    foreach(HtmlNode link in docIndex.DocumentNode.SelectNodes( "//a[@href]" ))
                        {
                        HtmlAttribute att = link.Attributes["href"];
                        Console.WriteLine( "<link att='{0}'/>", att.Value );
                        ////att.Value = FixLink(att);
                        }

                    }
                else if( sArg == "--strip" )    // strip the valid URLs from the HTML
                    {
                    Debug.Assert( false, "Obsolete, do not use!!!" );
                    String sFileHTML = rgsArgs[ ++iA ];
                    Debug.Assert( sFileHTML.Contains( ".html" ) );

                    StreamReader srFiles = new StreamReader( sFileHTML );
                    while( ! srFiles.EndOfStream )
                        {
                        String sLine = srFiles.ReadLine();

                        if( sLine.Contains( "download.mp4" ) )
                            {
                            sLine = Regex.Replace( sLine, "^.*href=", "" );
                            sLine = sLine.Replace( "\"", "" );  // remove quotes
                            Console.WriteLine( "{0}", sLine );
                            }
                        }
                    }
                else
                    {
                    Debug.Assert( false, "Unsupported arg!" );
                    }
                }
        }
    }
}
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
// ////////////////////////////////////////////////////////////////////////////
