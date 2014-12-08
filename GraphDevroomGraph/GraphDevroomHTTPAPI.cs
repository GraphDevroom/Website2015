/*
 * Copyright (c) 2014 Achim Friedland <achim.friedland@graphdefined.com>
 * This file is part of eMI3 WWCP <http://www.github.com/eMI3/WWCP-Bindings>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Threading;

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Illias.ConsoleLog;
using org.GraphDefined.Vanaheimr.Styx.Arrows;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.Sockets;
using org.GraphDefined.Vanaheimr.Hermod.Sockets.TCP;
using org.GraphDefined.Vanaheimr.Balder;

#endregion

namespace org.GraphDevroom.Graph
{

    /// <summary>
    /// GraphDevroom Graph - HTTP API.
    /// </summary>
    public class GraphDevroomHTTPAPI : HTTPServer
    {

        #region Data

        internal const String __DefaultServerName  = "GraphDevroom HTTP API v0.4";
        internal const String __DefaultHTTPRoot    = "org.GraphDevroom.GraphDevroom2015.HTTPRoot";

        #endregion

        #region Properties

        #region Graph

        private readonly IGenericPropertyGraph<UInt64, Int64, GDVertexLabel,    String, Object,
                                               UInt64, Int64, GDEdgeLabel,      String, Object,
                                               UInt64, Int64, GDMultiEdgeLabel, String, Object,
                                               UInt64, Int64, GDHyperEdgeLabel, String, Object> _Graph;

        public IGenericPropertyGraph<UInt64, Int64, GDVertexLabel,    String, Object,
                                     UInt64, Int64, GDEdgeLabel,      String, Object,
                                     UInt64, Int64, GDMultiEdgeLabel, String, Object,
                                     UInt64, Int64, GDHyperEdgeLabel, String, Object> Graph
        {
            get
            {
                return _Graph;
            }
        }

        #endregion

        #region HTTPRoot

        private readonly String _HTTPRoot;

        public String HTTPRoot
        {
            get
            {
                return _HTTPRoot;
            }
        }

        #endregion

        //#region Logger

        //private readonly ILogger<EMobility> _Logger;

        //public ILogger<EMobility> Logger
        //{
        //    get
        //    {
        //        return _Logger;
        //    }
        //}

        //#endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Initialize the GraphDevroom HTTP API using IPAddress.Any, http port 8080 and start the server.
        /// </summary>
        /// <param name="IPPort">The IP listing port.</param>
        public GraphDevroomHTTPAPI(IPPort                                                                   IPPort                            = null,
                                   String                                                                   DefaultServerName                 = __DefaultServerName,
                                   String                                                                   HTTPRoot                          = __DefaultHTTPRoot,
                                   String                                                                   URIPrefix                         = "",

                                   IGenericPropertyGraph<UInt64, Int64, GDVertexLabel,    String, Object,
                                                         UInt64, Int64, GDEdgeLabel,      String, Object,
                                                         UInt64, Int64, GDMultiEdgeLabel, String, Object,
                                                         UInt64, Int64, GDHyperEdgeLabel, String, Object>   Graph                             = null,

                                   String                                                                   ServerThreadName                  = null,
                                   ThreadPriority                                                           ServerThreadPriority              = ThreadPriority.AboveNormal,
                                   Boolean                                                                  ServerThreadIsBackground          = true,

                                   ConnectionIdBuilder                                                      ConnectionIdBuilder               = null,
                                   ConnectionThreadsNameBuilder                                             ConnectionThreadsNameBuilder      = null,
                                   ConnectionThreadsPriorityBuilder                                         ConnectionThreadsPriorityBuilder  = null,
                                   Boolean                                                                  ConnectionThreadsAreBackground    = true,
                                   TimeSpan?                                                                ConnectionTimeout                 = null,
                                   UInt32                                                                   MaxClientConnections              = TCPServer.__DefaultMaxClientConnections,

                                   IEnumerable<Assembly>                                                    CallingAssemblies                 = null,

                                   Boolean                                                                  Autostart                         = false)

            : base((IPPort != null) ? IPPort : IPPort.Parse(8080),
                   DefaultServerName,
                   CallingAssemblies,

                   ServerThreadName,
                   ServerThreadPriority,
                   ServerThreadIsBackground,

                   ConnectionIdBuilder,
                   ConnectionThreadsNameBuilder,
                   ConnectionThreadsPriorityBuilder,
                   ConnectionThreadsAreBackground,

                   ConnectionTimeout,
                   MaxClientConnections,
                   false)

        {

            this._Graph            = (Graph != null) ? Graph : GraphDevroomGraphFactory.Create(1, "FOSDEM GraphDevroom Graph");
            this._HTTPRoot         = HTTPRoot;
            //this._Logger           = Logger;

            #region / (HTTPRoot)

            this.RegisterResourcesFolder(URIPrefix + "/",
                                         _HTTPRoot,
                                         DefaultFilename: "index.html");

            #endregion

            #region /raw

            this.AddMethodCallback(HTTPMethod.GET,
                                   "/raw",
                                   HTTPContentType.HTML_UTF8,
                                   HTTPRequest => {

                                       return new HTTPResponseBuilder()
                                       {
                                           HTTPStatusCode  = HTTPStatusCode.OK,
                                           ContentType     = HTTPContentType.TEXT_UTF8,
                                           Content         = HTTPRequest.RawHTTPHeader.ToString().ToUTF8Bytes(),
                                           CacheControl    = "private",
                                           //Expires         = "Mon, 25 Jun 2015 21:31:12 GMT",
                                           Connection      = "close"
                                       };

                                   });

            #endregion

            #region ~/{Year}/{Event}/schedule

            #region GET         ~/{Year}/{Event}/schedule

            #region HTML_UTF8

            // -----------------------------------------------------------------------------
            // curl -v -H "Accept: text/html" http://127.0.0.1:8080/{Year}/{Event}/schedule
            // -----------------------------------------------------------------------------
            this.AddMethodCallback(HTTPMethod.GET,
                                   "/{Year}/{Event}/schedule",
                                   HTTPContentType.HTML_UTF8,
                                   HTTPDelegate: HTTPRequest => {

                                       #region Parse Year

                                       UInt16 Year;

                                       if (HTTPRequest.ParsedURIParameters.Length < 1)
                                       {

                                           Log.Timestamp("Bad request: Missing year query parameter!");

                                           return new HTTPResponseBuilder() {
                                               HTTPStatusCode  = HTTPStatusCode.BadRequest,
                                               ContentType     = HTTPContentType.JSON_UTF8,
                                               Content         = new JObject(new JProperty("@context",    "http://emi3group.org/contexts/BadRequest.jsonld"),
                                                                             new JProperty("Description", "Missing year query parameter!")).
                                                                             ToString().ToUTF8Bytes()
                                           };

                                       }

                                       if (!UInt16.TryParse(HTTPRequest.ParsedURIParameters[0], out Year))
                                       {

                                           Log.Timestamp("Bad request: Invalid year query parameter!");

                                           return new HTTPResponseBuilder() {
                                               HTTPStatusCode  = HTTPStatusCode.BadRequest,
                                               ContentType     = HTTPContentType.JSON_UTF8,
                                               Content         = new JObject(new JProperty("@context",    "http://emi3group.org/contexts/BadRequest.jsonld"),
                                                                             new JProperty("Value",       HTTPRequest.ParsedURIParameters[0]),
                                                                             new JProperty("Description", "Invalid year query parameter!")).
                                                                             ToString().ToUTF8Bytes()
                                           };

                                       }

                                       #endregion

                                       var Content = "lala";

                                       return new HTTPResponseBuilder() {
                                           HTTPStatusCode  = HTTPStatusCode.OK,
                                           Server          = this.DefaultServerName,
                                           ETag            = "1",
                                           ContentType     = HTTPContentType.HTML_UTF8,
                                           Content         = Content.ToUTF8Bytes(),
                                           CacheControl    = "no-cache"
                                       };

                                   });

            #endregion

            #endregion

            #endregion


            this.AddEventSource(EventIdentification:     "Semantics.DebugLog",
                                MaxNumberOfCachedEvents: 100,
                                RetryIntervall:          TimeSpan.FromSeconds(5),
                                URITemplate:             URIPrefix + "/DebugLog");


            // HTTP ACCEPT TYPE  !=  HTTP CONTENT TYPE !!!




            if (Autostart)
                this.Start();

        }

        #endregion


    }

}
