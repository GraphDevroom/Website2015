/*
 * Copyright (c) 2014-2015 Achim 'ahzf' Friedland <achim@graphdevroom.org>
 * This file is part of GraphDevroom <http://www.github.com/GraphDevroom>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Styx;
using org.GraphDefined.Vanaheimr.Balder;
using org.GraphDefined.Vanaheimr.Duron;
using org.GraphDefined.Vanaheimr.Walkyr.BalderSON;

using org.GraphDevroom.Graph;

#endregion

namespace org.GraphDevroom.GraphDevroom2015
{

    public class Program
    {

        static void Main(String[] Arguments)
        {

            #region Create an empty graph

            var graph = GraphDevroomGraphFactory.Create(1, "FOSDEM GraphDevroom Graph");

            #endregion

            var PopulateGraph = true;

            #region Populate graph

            if (PopulateGraph)
            {

                // Delete old graph snapshots
                new DirectoryInfo(Directory.GetCurrentDirectory()).
                    GetFiles("*.graph").
                    ForEach(FI => FI.Delete());

                #region FOSDEM

                var FOSDEM              = graph.AddVertex(GDVertexLabel.ConfSeries,
                                                          vertex => vertex.SetProperty(Semantics.Name, "FOSDEM"));

                #endregion

                #region Events

                var GraphDevroom2012    = graph.AddVertex(GDVertexLabel.Event,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.ConfSeries,    FOSDEM,  e => e.SetProperty(Semantics.Year, 2012)).
                                                                           SetProperty(Semantics.Year,          2012));

                var GraphDevroom2013    = graph.AddVertex(GDVertexLabel.Event,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.ConfSeries,    FOSDEM,  e => e.SetProperty(Semantics.Year, 2013)).
                                                                           SetProperty(Semantics.Year,          2013));

                var GraphDevroom2014    = graph.AddVertex(GDVertexLabel.Event,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.ConfSeries,    FOSDEM,  e => e.SetProperty(Semantics.Year, 2014)).
                                                                           SetProperty(Semantics.Year,          2014).
                                                                           SetProperty(Semantics.DateKey,       "Sunday, 2. February 2014").
                                                                           SetProperty(Semantics.RoomKey,       "H.1308").
                                                                           SetProperty(Semantics.StartTimeKey,  "09:00").
                                                                           SetProperty(Semantics.EndTimeKey,    "17:00"));

                var GraphDevroom2015    = graph.AddVertex(GDVertexLabel.Event,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.ConfSeries,    FOSDEM,  e => e.SetProperty(Semantics.Year, 2015)).
                                                                           SetProperty(Semantics.Year,          2015).
                                                                           SetProperty(Semantics.DateKey,       "Saturday, 31. January 2015"));
                                                                           //SetProperty(Semantics.RoomKey,       "H.1308").
                                                                           //SetProperty(Semantics.StartTimeKey,  "09:00").
                                                                           //SetProperty(Semantics.EndTimeKey,    "17:00"));

                #endregion


                #region 2014

                #region Welcome to the FOSDEM GraphDevroom 2014

                var WelcomeTalk         = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference, GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "FOSDEM GraphDevroom 2014").
                                                                           SetProperty(Semantics.SubTitleKey,    "Welcome to the FOSDEM GraphDevroom 2014").
                                                                           SetProperty(Semantics.Slug,           "Welcome").
                                                                           SetProperty(Semantics.StartTimeKey,   "09:00").
                                                                           SetProperty(Semantics.EndTimeKey,     "09:05"));

                var AchimFriedland      = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.Speaker, WelcomeTalk).
                                                                           SetProperty(Semantics.Name, "Achim Friedland"));

                #endregion

                #region Intel GraphBuilder

                var IntelGraphBuilderTalk   = graph.AddVertex(GDVertexLabel.Talk,
                                                              vertex => vertex.AddInEdge(GDEdgeLabel.Conference, GraphDevroom2014).
                                                                               SetProperty(Semantics.TitleKey,      "Intel GraphBuilder").
                                                                               SetProperty(Semantics.SubTitleKey,   "...").
                                                                               SetProperty(Semantics.Slug,          "Intel").
                                                                               SetProperty(Semantics.StartTimeKey,  "09:05").
                                                                               SetProperty(Semantics.EndTimeKey,    "09:45"));

                var GraphBuilder     = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.WebLink,        IntelGraphBuilderTalk).
                                                                           SetProperty(Semantics.Url,            "http://www.intel.com/content/www/us/en/software/intel-graph-builder-for-apache-hadoop-software-v2-product-detail.html").
                                                                           SetProperty(Semantics.DescriptionKey, "Intel Graph Builder"));

                var GraphBuilder2    = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.WebLink,        IntelGraphBuilderTalk).
                                                                           SetProperty(Semantics.Url,            "https://01.org/graphbuilder").
                                                                           SetProperty(Semantics.DescriptionKey, "Intel Graph Builder (Download)"));

        

                var Ted              = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.Speaker, IntelGraphBuilderTalk).
                                                                           SetProperty(Semantics.Name, "Ted"));

                #endregion

                #region Structr

                var StructrTalk         = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "From 0 to a complex webapp in 30 minutes").
                                                                           SetProperty(Semantics.SubTitleKey,    "Let's create a complex, graph-based webapp, live, within 30 min, with input from the audience only").
                                                                           SetProperty(Semantics.Slug,           "Structr").
                                                                           SetProperty(Semantics.StartTimeKey,   "09:45").
                                                                           SetProperty(Semantics.EndTimeKey,     "10:15").
                                                                           SetProperty(Semantics.DescriptionKey, "With the help of the audience, I'll try to create a complex webapp within 30 minutes. " +
                                                                                                                 "Complex in the sense of: Custom use case (unprepared, told by audience), custom JSON/REST " +
                                                                                                                 "backend, beautiful HTML5/CSS3 template, dynamic data, user interaction, Twitter/FB connect). " +
                                                                                                                 "Everything we need is the Open Source framework Structr, on top of the graph database Neo4j. " +
                                                                                                                 "This will be very interactive, and even fun if it works. ;-)"));

                var Structr             = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      StructrTalk).
                                                                           SetProperty(Semantics.Url,            "http://structr.org").
                                                                           SetProperty(Semantics.DescriptionKey, "Structr.org"));

                var AxelMorgner         = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      StructrTalk).
                                                                           SetProperty(Semantics.Name,           "Axel Morgner"));

                #endregion

                #region GraphHopper

                var GraphHopperTalk     = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Fast and Memory Efficient Road Routing with GraphHopper").
                                                                           SetProperty(Semantics.SubTitleKey,    "Solving spatial problems with Graphs and OpenStreetMap").
                                                                           SetProperty(Semantics.Slug,           "GraphHopper").
                                                                           SetProperty(Semantics.StartTimeKey,   "10:15").
                                                                           SetProperty(Semantics.EndTimeKey,     "10:45").
                                                                           SetProperty(Semantics.DescriptionKey, "GraphHopper is a fast Open Source road routing engine written in Java running on the server " +
                                                                                                                 "as well as on Android. It uses OpenStreetMap as data source and implements road routing via " +
                                                                                                                 "Dijkstra algorithm and variations. In this talk I'll describe the challenges faced while " +
                                                                                                                 "implementing fast and memory efficient graph algorithms and storage solutions.<br />" +
                                                                                                                 "GraphHopper is a fast Open Source road routing engine written in Java. It uses OpenStreetMap as " +
                                                                                                                 "data source and implements road routing via Dijkstra algorithm and variations. To make shortest-" +
                                                                                                                 "path calculations in under 50ms possible - even for paths of continental length - GraphHopper uses " +
                                                                                                                 "a special \"shortcut-technique\".<br />" +
                                                                                                                 "In this talk I'll give you a brief summary of the internal graph representation and how to scale this " +
                                                                                                                 "to gigabytes of memory and still make it running on Android. Including a short overview of Dijkstra, " +
                                                                                                                 "the memory efficient graph traversal API and how to map the real, geographical world with GPS " +
                                                                                                                 "coordinates and roads into a mathematical graph representation with edges and nodes."));

                var GraphHopper         = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      GraphHopperTalk).
                                                                           SetProperty(Semantics.Url,            "http://graphhopper.com").
                                                                           SetProperty(Semantics.DescriptionKey, "GraphHopper.com"));

                var GraphHopperBlog     = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      GraphHopperTalk).
                                                                           SetProperty(Semantics.Url,            "http://karussell.wordpress.com").
                                                                           SetProperty(Semantics.DescriptionKey, "Blog"));

                var PeterKarich         = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      GraphHopperTalk).
                                                                           SetProperty(Semantics.Name,           "Peter Karich"));

                #endregion

                #region LDBC

                var LDBCTalk            = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "The LDBC Social Graph Data Generator").
                                                                           SetProperty(Semantics.SubTitleKey,    "Graph Query Benchmarking to the next level").
                                                                           SetProperty(Semantics.Slug,           "LDBCSGGenerator").
                                                                           SetProperty(Semantics.StartTimeKey,   "10:45").
                                                                           SetProperty(Semantics.EndTimeKey,     "11:15").
                                                                           SetProperty(Semantics.DescriptionKey, "The Linked Data Benchmark Council (LDBC) is an initiative to develop industry-grade database " +
                                                                                                                 "benchmarks. The talk focuses on the activities of its Social Network Benchmark (SNB) task force " +
                                                                                                                 "of LDBC, which during past year developed an advanced graph generator that creates a huge social " +
                                                                                                                 "graph with realistic correlations between structure and data. The datasets it generates will be " +
                                                                                                                 "tested by three different workloads (interactive, BI, graph anatytics), that I will shortly " +
                                                                                                                 "outline.<br />" +
                                                                                                                 "The talk will cover:" +
                                                                                                                 "<ul>" +
                                                                                                                 "<li> mission of LDBC in general and the social network benchmark task force in specific</li>" +
                                                                                                                 "<li> the LDBC social network graph generator:<br>" +
                                                                                                                   "<ul>" +
                                                                                                                   "<li> schema, data examples</li>" +
                                                                                                                   "<li> how it generates correlated data</li>" +
                                                                                                                   "<li> technical information</li>" +
                                                                                                                   "</ul></li>" +
                                                                                                                 "<li>benchmarks that use it:<br>" +
                                                                                                                   "<ul>" +
                                                                                                                   "<li> interactive workload: many small graph queries and updates</li>" +
                                                                                                                   "<li> business intelligence workloads: grouping, counting, subqueries and graph navigation</li>" +
                                                                                                                   "<li> graph analytics: algorithms (not queries) that analyze graphs (e.g. clustering, pagerank, etc)</li>" +
                                                                                                                 "</ul></li>" +
                                                                                                                 "</ul>"));

                var LDBCWebsite         = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      LDBCTalk).
                                                                           SetProperty(Semantics.Url,            "http://www.ldbc.eu").
                                                                           SetProperty(Semantics.DescriptionKey, "General LDBC project website"));

                var PeterBoncz          = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      LDBCTalk).
                                                                           SetProperty(Semantics.Name,           "Peter Boncz"));

                #endregion

                #region Giraph

                var GiraphTalk          = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Giraph: two years later").
                                                                           SetProperty(Semantics.SubTitleKey,    "The new Giraph APIs for Python, Rexster and Gora").
                                                                           SetProperty(Semantics.Slug,           "Giraph").
                                                                           SetProperty(Semantics.StartTimeKey,   "11:15").
                                                                           SetProperty(Semantics.EndTimeKey,     "11:45").
                                                                           SetProperty(Semantics.DescriptionKey, "Since its initial incubation, Giraph has turned into a different beast. it is now a solid, " +
                                                                                                                 "full-featured tool used in production at many companies that need to analyse massive graphs. " +
                                                                                                                 "The success of a data analytics tool relays on the usability if its programming API and its " +
                                                                                                                 "ability to play well with the ecosystem of data stores.<br />" +
                                                                                                                 "In this last year, much has been done in this aspects, with a new programming API that allows " +
                                                                                                                 "composable jobs, and scripting support. You can now write Giraph applications that will run over " +
                                                                                                                 "billion of vertices with 20 lines of python code.<br />" +
                                                                                                                 "Moreover, Giraph is now able to read and write graphs from any Blueprints-compatible graph " +
                                                                                                                 "database. Furthermore, thanks to Gora, it can read and write data modelled as graphs from a " +
                                                                                                                 "large set of NoSQL (even SQL!) stores.<br />" +
                                                                                                                 "In this presentation, I will focus on these new contributions to Giraph, namely its python API " +
                                                                                                                 "and the rexster and gora input formats.<br />" +
                                                                                                                 "I will show samples or working code, and run a demo where I compute a graph computation expressed " +
                                                                                                                 "in python over a graph stored in a graph database."));

                var Rexster             = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      GiraphTalk).
                                                                           SetProperty(Semantics.Url,            "http://giraph.apache.org/rexster.html").
                                                                           SetProperty(Semantics.DescriptionKey, "Rexster I/O"));

                var ArmandoMiraglia     = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      GiraphTalk).
                                                                           SetProperty(Semantics.Name,           "Armando Miraglia").
                                                                           SetProperty(Semantics.DescriptionKey, "I am a student at the Vrije Universiteit Amsterdam, currently following the Parallel and Distributed " +
                                                                                                                 "Computing Systems master course. I have contributed to the opensource in several ways: I am a " +
                                                                                                                 "commiter for the SSHGuard project, I have developed the regression tests for the simclist and " +
                                                                                                                 "lately I have been working on the core of Giraph. During the 2013, I have participated to the " +
                                                                                                                 "Google Summer of Code coordinated by Claudio Martella aiming to develop the Rexster I/O API."));

                #endregion

                #region Analyze Biologica lData

                var BiologicalDataTalk  = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "The Power of Graphs to Analyze Biological Data").
                                                                           SetProperty(Semantics.Slug,           "AnalyzeBiologicalData").
                                                                           SetProperty(Semantics.StartTimeKey,   "11:45").
                                                                           SetProperty(Semantics.EndTimeKey,     "12:15").
                                                                           SetProperty(Semantics.DescriptionKey, "This talk will illustrate the power and flexibility of Graph Databases to help in the overall " +
                                                                                                                 "analysis of biological data sets. Davy will show how to build a visual exploration environment " +
                                                                                                                 "that helps researchers at identifying clusters within various biological data sets, including " +
                                                                                                                 "gene expression and mutation prevalence data. Additionally, he will demo BRAIN (Bio Relations " +
                                                                                                                 "and Intelligence Network), a powerful data exploration platform that combines various scientific " +
                                                                                                                 "data sources (including Pubmed, Swissprot and Drugbank). It uses a graph database under the cover " +
                                                                                                                 "to both store and enable powerful querying capabilities that provide key insights and deductions."));

                var DavySuvee           = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge(GDEdgeLabel.Speaker, BiologicalDataTalk).
                                                                           SetProperty(Semantics.Name,           "Davy Suvee"));

                #endregion

                #region Bio4jStatika

                var Bio4jStatikaTalk    = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Bio4j + Statika").
                                                                           SetProperty(Semantics.SubTitleKey,    "Managing module dependencies on the type level").
                                                                           SetProperty(Semantics.Slug,           "Bio4jStatika").
                                                                           SetProperty(Semantics.StartTimeKey,   "12:15").
                                                                           SetProperty(Semantics.EndTimeKey,     "12:30").
                                                                           SetProperty(Semantics.DescriptionKey, "Bio4j bioinformatics graph database is modular and customizable, allowing you to import just the " +
                                                                                                                 "data you are interested in. There exist, though, dependencies among these resources that must be " +
                                                                                                                 "taken into account and that's where Statika enters the picture; a set of Scala libraries which " +
                                                                                                                 "allows you to declare dependencies between components of any modular system and track their " +
                                                                                                                 "correctness using Scala type system. Thanks to this, it's possible now to deploy only selected " +
                                                                                                                 "components of the integrated data sets, with Amazon Web Services deployments on hardware specifically " +
                                                                                                                 "configured for them."));

                var Statika             = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      Bio4jStatikaTalk).
                                                                           SetProperty(Semantics.Url,            "http://ohnosequences.com/statika/").
                                                                           SetProperty(Semantics.DescriptionKey, "Statika"));

                var Bio4jWebLink        = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      Bio4jStatikaTalk).
                                                                           SetProperty(Semantics.Url,            "http://www.bio4j.com").
                                                                           SetProperty(Semantics.DescriptionKey, "Bio4j.com"));

                var PabloPareja         = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      Bio4jStatikaTalk).
                                                                           SetProperty(Semantics.Name,           "Pablo Pareja").
                                                                           SetProperty(Semantics.DescriptionKey, "Currently working as Bioinformatics consultant/developer/researcher at http://www.ohnosequences.com " +
                                                                                                                 "Amateur musician and composer, traveller, painter, sporadic writer and always eager to learn " +
                                                                                                                 "more about languages, plants... Too many things to do and too little time for it!"));

                #endregion

                #region Bio4j

                var Bio4jTalk           = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Bio4j: bigger, faster, leaner").
                                                                           SetProperty(Semantics.Slug,           "Bio4j").
                                                                           SetProperty(Semantics.StartTimeKey,   "12:30").
                                                                           SetProperty(Semantics.EndTimeKey,     "13:00").
                                                                           SetProperty(Semantics.DescriptionKey, "Bio4j is a high-performance cloud-enabled graph-based bioinformatics data platform. It " +
                                                                                                                 "integrates most data available in UniProt KB (SwissProt + Trembl), Gene Ontology (GO), UniRef " +
                                                                                                                 "(50, 90, 100), RefSeq, NCBI taxonomy, and Expasy Enzyme DBs. Data is organized in a way " +
                                                                                                                 "semantically equivalent to what it represents in the graph structure, and thanks to this, " +
                                                                                                                 "queries which would even be impossible to perform with a standard Relational DB can just take " +
                                                                                                                 "a couple of seconds with Bio4j.<br>" +
                                                                                                                 "This year has seen important updates and new developments on Bio4j. It now includes 1.216.993.547 " +
                                                                                                                 "relationships and 190.625.351 nodes, almost triple the figures from one year ago. We have " +
                                                                                                                 "introduced a new level of abstraction for the domain model, by decoupling the inner database " +
                                                                                                                 "implementation from the relationships among entities themselves. Interfaces has been developed " +
                                                                                                                 "for each node and relationship present in the database, including methods to access both the " +
                                                                                                                 "properties of the entity it represents and utility methods that allow to easily navigate to the " +
                                                                                                                 "entities that will be linked to it.<br>" +
                                                                                                                 "Implementing that set of interfaces we have developed another layer for the domain model using " +
                                                                                                                 "Blueprints, the de-facto graph data model standard, making the domain model independent from the " +
                                                                                                                 "choice of database technology. Building on that, we now offer specifically tuned data binary " +
                                                                                                                 "distributions for TitanDB, yielding a dramatic increase in performance due to vertex-local edge-" +
                                                                                                                 "typed indexes.<br>" +
                                                                                                                 "Bio4j is open source, available under the AGPLv3 license.").
                                                                           AddOutEdge (GDEdgeLabel.WebLink,      Bio4jWebLink).
                                                                           AddOutEdge (GDEdgeLabel.Speaker,      PabloPareja));

                #endregion

                #region Stardog

                var StardogTalk         = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Semantic Graphs Are For Everyone").
                                                                           SetProperty(Semantics.SubTitleKey,    "Stardog RDF Database").
                                                                           SetProperty(Semantics.Slug,           "Stardog").
                                                                           SetProperty(Semantics.StartTimeKey,   "13:00").
                                                                           SetProperty(Semantics.EndTimeKey,     "14:00").
                                                                           SetProperty(Semantics.DescriptionKey, "Stardog is an RDF database for querying, searching, and reasoning about semantic graphs.<br>" +
                                                                                                                 "Semantic Graphs Are For Everybody explains the approach to query answering, reasoning, " +
                                                                                                                 "integrity constraint validation, and graph analysis of RDF encoded semantic graphs. The talk " +
                                                                                                                 "will explain how we provide these services to users by focusing relentlessly on usability and " +
                                                                                                                 "UX out of the box. The goal of the system and of this talk is to show how semantic graphs are " +
                                                                                                                 "useful for information integration and analysis problems."));

                var StardogLink         = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      StardogTalk).
                                                                           SetProperty(Semantics.Url,            "http://slid.es/kendallclark/stardog-20/fullscreen").
                                                                           SetProperty(Semantics.DescriptionKey, "Some background info"));

                var HectorPerezUrbina   = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      StardogTalk).
                                                                           SetProperty(Semantics.Name,           "Hector Perez-Urbina"));

                #endregion

                #region LevelGraph

                var LevelGraphTalk      = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "LevelGraph - a graph store for node.js and the browser!").
                                                                           SetProperty(Semantics.SubTitleKey,    "#JIFSNIF - JavaScript is fun so node is funnier...").
                                                                           SetProperty(Semantics.Slug,           "LevelGraph").
                                                                           SetProperty(Semantics.StartTimeKey,   "14:00").
                                                                           SetProperty(Semantics.EndTimeKey,     "14:30").
                                                                           SetProperty(Semantics.DescriptionKey, " <br>" +
                                                                                                                 "I would like to publish similar interactive walk through for LevelGraph ASAP:<br>" +
                                                                                                                 "http://nodeschool.io/#levelmeup and we could use it during hands on workshop!" +
                                                                                                                 "Currently it supports RDF through two extensions LevelGraph-N3 and LevelGraph-JSON-LD. " +
                                                                                                                 "We also plan work on LevelGraph-SPARQL"));

                var LevelGraphGitHub    = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      LevelGraphTalk).
                                                                           SetProperty(Semantics.Url,            "https://github.com/mcollina/levelgraph").
                                                                           SetProperty(Semantics.DescriptionKey, "LevelGraph on GitHub"));

                var LevelGraph2         = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      LevelGraphTalk).
                                                                           SetProperty(Semantics.Url,            "http://http//nodeschool.io/#levelmeup").
                                                                           SetProperty(Semantics.DescriptionKey, "LevelDB interactive walkthrough"));

                var LevelGraph3         = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      LevelGraphTalk).
                                                                           SetProperty(Semantics.Url,            "http://www.w3.org/community/rdfjs/").
                                                                           SetProperty(Semantics.DescriptionKey, "W3C: RDF JavaScript Libraries Community Group"));

                var ElfPavlik           = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      LevelGraphTalk).
                                                                           SetProperty(Semantics.Name,           "elf-pavlik"));

                #endregion

                #region NLP with Neo4j

                var NLPWithNeo4jTalk    = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Natural Language Processing with Neo4j").
                                                                           SetProperty(Semantics.Slug,           "NLPWithNeo4j").
                                                                           SetProperty(Semantics.StartTimeKey,   "14:30").
                                                                           SetProperty(Semantics.EndTimeKey,     "15:15").
                                                                           SetProperty(Semantics.DescriptionKey, "Recent natural language processing advancements have propelled search engine " +
                                                                                                                 "and information retrieval innovations into the public spotlight. People want to " +
                                                                                                                 "be able to interact with their devices in a natural way. In this talk I will be " +
                                                                                                                 "introducing you to natural language search using a Neo4j graph database. I will " +
                                                                                                                 "show you how to interact with an abstract graph data structure using natural " +
                                                                                                                 "language and how this approach is key to future innovations in the way we interact " +
                                                                                                                 "with our devices."));

                var BlogKennyBastani    = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      NLPWithNeo4jTalk).
                                                                           SetProperty(Semantics.Url,            "http://www.kennybastani.com/").
                                                                           SetProperty(Semantics.DescriptionKey, "Kenny Bastani's Technology Blog"));

                var KennyBastani        = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      NLPWithNeo4jTalk).
                                                                           SetProperty(Semantics.Name,           "Kenny Bastani").
                                                                           SetProperty(Semantics.DescriptionKey, "Kenny Bastani is a software development consultant and entrepreneur with 10+ years " +
                                                                                                                 "of industry experience as a front-end and back-end engineer. As both an entrepreneur " +
                                                                                                                 "and software designer based in the SF Bay Area, Kenny has gained valuable experience " +
                                                                                                                 "leading teams in both product design and software architecture. Kenny believes graph " +
                                                                                                                 "databases are the future to unlocking the power of big data and is a passionate " +
                                                                                                                 "developer evangelist for Neo4j graph database."));

                #endregion

                #region Graphgists

                var GraphGistTalk       = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Graphgists - live graph documentation on steroids").
                                                                           SetProperty(Semantics.Slug,           "Graphgists").
                                                                           SetProperty(Semantics.StartTimeKey,   "15:15").
                                                                           SetProperty(Semantics.EndTimeKey,     "16:00").
                                                                           SetProperty(Semantics.DescriptionKey, "In this talk, Peter will describe the implementation and working of http://gist.neo4j.org. " +
                                                                                                                 "It is based on ASCIIDOC, Opal.js, Heroku and Neo4j and rendered all client side. Also, " +
                                                                                                                 "Peter will show some of the examples community members have been contributing - " +
                                                                                                                 "everything from Chess play graphs to product configurations."));

                var GraphGist           = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      GraphGistTalk).
                                                                           SetProperty(Semantics.Url,            "http://gist.neo4j.org").
                                                                           SetProperty(Semantics.DescriptionKey, "GraphGist"));

                var PeterNeubauer       = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      GraphGistTalk).
                                                                           SetProperty(Semantics.Name,           "Peter Neubauer").
                                                                           SetProperty(Semantics.DescriptionKey, "Peter is co-founder of a number of popular Open Source projects such as Neo4j, OPS4J " +
                                                                                                                 "and Qi4j. Peter loves connecting things, writing novel prototypes and throwing together " +
                                                                                                                 "new ideas and projects around graphs and society-scale innovation. Right now, Peter is " +
                                                                                                                 "concentrating on bringing Open Source projects into the enterprise at Neo Technology, the " +
                                                                                                                 "company behind Neo4j, the world's leading Graph Database. Also, Peter is a Mentor helping " +
                                                                                                                 "startups at Startupbootcamp Copenhagen, Berlin and at Mozilla WebFD. He likes teaching " +
                                                                                                                 "programming to kids at CoderDojo Malmö and organizing events like " +
                                                                                                                 "http://www.thoughtmade.com and TEDx Öresund.<br>" +
                                                                                                                 "If you want brainstorming - feed him a latte and you are in business.<br>" +
                                                                                                                 "@peterneubauer github.com/peterneubauer"));

                #endregion

                #region GraphSearch

                var GraphSearchTalk     = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Graph Search").
                                                                           SetProperty(Semantics.Slug,           "GraphSearch").
                                                                           SetProperty(Semantics.StartTimeKey,   "16:00").
                                                                           SetProperty(Semantics.EndTimeKey,     "16:30").
                                                                           SetProperty(Semantics.DescriptionKey, "Facebook Graph Search has given the Graph Database community a simpler way to explain " +
                                                                                                                 "what it is we do and why it matters. Max will show you how easy is to build your own " +
                                                                                                                 "Graph Search... and for the truly lazy, a second way to perform graph search with just " +
                                                                                                                 "mouse clicks using the connectedness of the data and a little metadata magic to build a " +
                                                                                                                 "multi term search bar.<br>" +
                                                                                                                 "Facebook Graph Search has given the Graph Database community a simpler way to explain what " +
                                                                                                                 "it is we do and why it matters. Max will show you how easy is to build your own Graph " +
                                                                                                                 "Search... and for the truly lazy, a second way to perform graph search with just mouse " +
                                                                                                                 "clicks using the connectedness of the data and a little metadata magic to build a multi " +
                                                                                                                 "term search bar. I'll also touch on the dangers and privacy issues of this data. Some folks " +
                                                                                                                 "gave me their 4000 facebook likes, I know everything about them and so does the rest of the " +
                                                                                                                 "world. You may keep your information quiet, but your friends curiosity may just betray you... " +
                                                                                                                 "and it gets scarier.... while I can only retrieve data from your 300 or so facebook friends, " +
                                                                                                                 "I can get the data of the first 500 people you or any of your friends are in. That's thousands " +
                                                                                                                 "of profile data from a single facebook token. \"The only winning move is not to play\" with " +
                                                                                                                 "your privacy"));

                var FacebookGraphSearch = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      GraphSearchTalk).
                                                                           SetProperty(Semantics.Url,            "http://maxdemarzi.com/2013/01/28/facebook-graph-search-with-cypher-and-neo4j").
                                                                           SetProperty(Semantics.DescriptionKey, "Facebook Graph Search"));

                var VisualGraphSearch   = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      GraphSearchTalk).
                                                                           SetProperty(Semantics.Url,            "http://maxdemarzi.com/2013/07/03/the-last-mile").
                                                                           SetProperty(Semantics.DescriptionKey, "Visual Graph Search"));

                var MaxDeMarzi          = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      GraphSearchTalk).
                                                                           SetProperty(Semantics.Name,           "Max De Marzi").
                                                                           SetProperty(Semantics.DescriptionKey, "Max De Marzi, founder of getvouched.com, is a seasoned web developer. He started building " +
                                                                                                                 "websites in 1996 and has worked with Ruby on Rails since 2006. The web forced Max to wear many " +
                                                                                                                 "hats and master a wide range of technologies. He can be a system admin, database developer, " +
                                                                                                                 "graphic designer, back-end engineer and data scientist in the course of one afternoon. Max is " +
                                                                                                                 "a graph database enthusiast. He built the Neography Ruby Gem, a simple rest api wrapper to the " +
                                                                                                                 "Neo4j Graph Database. He is addicted to learning new things, loves a challenge and finding " +
                                                                                                                 "pragmatic solutions. Max is very easy to work with, focuses under pressure and has the patience " +
                                                                                                                 "of a rock."));

                #endregion

                #region ArangoDB - Visualize your Graph Database

                var ArandoDBTalk        = graph.AddVertex(GDVertexLabel.Talk,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Conference,   GraphDevroom2014).
                                                                           SetProperty(Semantics.TitleKey,       "Visualize your Graph Database").
                                                                           SetProperty(Semantics.SubTitleKey,    "Techniques to view, explore and modify your graph data with ArangoDB").
                                                                           SetProperty(Semantics.Slug,           "ArangoDB").
                                                                           SetProperty(Semantics.StartTimeKey,   "16:30").
                                                                           SetProperty(Semantics.EndTimeKey,     "17:00").
                                                                           SetProperty(Semantics.DescriptionKey, "If you are using a graph database you might want to get a visual representation of your data. " +
                                                                                                                 "In this talk I will present a visualization tool build on top of the Open Source Database " +
                                                                                                                 "ArangoDB. This tool allows a user to explore the graph by visually traversing through it. I " +
                                                                                                                 "will also present some challenges of graph visualization and my solutions for them.<br>" +
                                                                                                                 "In the NoSQL world a type of databases has emerged which is called graph database. These " +
                                                                                                                 "databases allow you to store data in a graph format (like social networks) natively and query " +
                                                                                                                 "it efficiently. But how can we visualize the data? Typically the resulting graph is too large " +
                                                                                                                 "to be displayed as a whole, but a local view on specific vertex is often useful. The user " +
                                                                                                                 "should be able to continue exploring the graph from this starting point. As we are talking " +
                                                                                                                 "about a graph database the user should be able to modify the graph during this process.<br>" +
                                                                                                                 "In this talk we will see strategies to layout the graph in the process above. Further more we " +
                                                                                                                 "will see limitations of such visualisation and I will present my solutions to these limitations:<br>" +
                                                                                                                 "<ul>" +
                                                                                                                 "<li>grouping of vertices</li>" +
                                                                                                                 "<li>zooming strategies</li>" +
                                                                                                                 "<li>layout optimization Technologies presented in this talk:</li>" +
                                                                                                                 "<li>d3.js: Library to render the graph and for the layout algorithm</li>" +
                                                                                                                 "<li>ArangoDB: The underlying graph database</li>" +
                                                                                                                 "</ul>"));

                var VisualizeGraphs     = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      ArandoDBTalk).
                                                                           SetProperty(Semantics.Url,            "https://www.arangodb.org/2013/11/29/visualize-graphs-screencast").
                                                                           SetProperty(Semantics.DescriptionKey, "Visualize Graphs Screencast"));

                var d3js                = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      ArandoDBTalk).
                                                                           SetProperty(Semantics.Url,            "http://d3js.org").
                                                                           SetProperty(Semantics.DescriptionKey, "D3.js A javascript graph visualization library"));

                var ArangoDB            = graph.AddVertex(GDVertexLabel.WebLink,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.WebLink,      ArandoDBTalk).
                                                                           SetProperty(Semantics.Url,            "http://www.arangodb.org").
                                                                           SetProperty(Semantics.DescriptionKey, "ArangoDB Homepage"));

                var MichaelHackstein    = graph.AddVertex(GDVertexLabel.Speaker,
                                                          vertex => vertex.AddInEdge  (GDEdgeLabel.Speaker,      ArandoDBTalk).
                                                                           SetProperty(Semantics.Name,           "Michael Hackstein").
                                                                           SetProperty(Semantics.DescriptionKey, "Michael is a javascript and NoSQL enthusiast. In his spare time he is organising colognejs, " +
                                                                                                                 "the javascript user group in Cologne Germany scheduled every two month.<br>" +
                                                                                                                 "In his professional life Michael is a Computer Science Student at RWTH Aachen University, in " +
                                                                                                                 "the last weeks of his Master’s Degree studies. Besides his studies he is member of the " +
                                                                                                                 "ArangoDB core team and developing the web frontend and graph visualisation for this project."));

                #endregion


                #region Add 'next'-edges based on timestamps!

                var OrderedTalkVertices = graph.VerticesByLabel(GDVertexLabel.Talk).
                                                OrderBy(vertex => vertex[Semantics.StartTimeKey]).
                                                ToArray();

                for (var i=0; i<OrderedTalkVertices.Length-1; i++)
                {
                    OrderedTalkVertices[i].AsMutable().AddOutEdge(GDEdgeLabel.Next, OrderedTalkVertices[i + 1].AsMutable());
             //       var c = OrderedTalkVertices[i].Out(GDEdgeLabel.Next).First()[Semantics.TitleKey];
                }

                #endregion


                #region Add tags...

                var Neo4jTag            = graph.AddVertex(GDVertexLabel.Tag,
                                                          vertex => vertex.SetProperty(Semantics.TagName, "Neo4j").
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, StructrTalk,          e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, BiologicalDataTalk,   e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, NLPWithNeo4jTalk,     e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, GraphGistTalk,        e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, GraphSearchTalk,      e => e.SetProperty(Semantics.Year, 2014)));

                var RDFTag              = graph.AddVertex(GDVertexLabel.Tag,
                                                          vertex => vertex.SetProperty(Semantics.TagName, "RDF").
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, LevelGraphTalk,       e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, StardogTalk,          e => e.SetProperty(Semantics.Year, 2014)));

                var Bio4jTag            = graph.AddVertex(GDVertexLabel.Tag,
                                                          vertex => vertex.SetProperty(Semantics.TagName, "Bio4j").
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, BiologicalDataTalk,   e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, Bio4jStatikaTalk,     e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, Bio4jTalk,            e => e.SetProperty(Semantics.Year, 2014)));

                var VisualizationTag    = graph.AddVertex(GDVertexLabel.Tag,
                                                          vertex => vertex.SetProperty(Semantics.TagName, "Visualization").
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, BiologicalDataTalk,   e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, ArandoDBTalk,         e => e.SetProperty(Semantics.Year, 2014)));

                var SocialNetworksTag   = graph.AddVertex(GDVertexLabel.Tag,
                                                          vertex => vertex.SetProperty(Semantics.TagName, "SocialNetworks").
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, GraphSearchTalk,      e => e.SetProperty(Semantics.Year, 2014)).
                                                                           AddOutEdge(GDEdgeLabel.TaggedWith, LDBCTalk,             e => e.SetProperty(Semantics.Year, 2014)));

                #endregion

                #endregion


            }

            #endregion

            #region Attach a GraphSnapshooter for graph persistency

            var SnapShot = graph.AttachSnapshooter(GraphSerializer:       graph.NewBalderSONSerializer(),
                                                   Prefix:                "GraphDevroom",
                                                   Suffix:                "graph",

                                                   VertexLabelParser:     GDVertexLabel.   TryParseString,
                                                   EdgeLabelParser:       GDEdgeLabel.     TryParseString,
                                                   MultiEdgeLabelParser:  GDMultiEdgeLabel.TryParseString,
                                                   HyperEdgeLabelParser:  GDHyperEdgeLabel.TryParseString,

                                                   NumberOfBackupFiles:   10U,
                                                   SaveEveryMSec:         10000);

            SnapShot.OnSavePointLoading += (FileName)                  => Console.WriteLine("Loading last save point from '" + FileName + "'...");
            SnapShot.OnSavePointLoaded  += (FileName, V, E, ME, HE, T) => Console.WriteLine(V + " vertices and " + E + " edges loaded from file '" + FileName + " in " + T + "ms...");
            SnapShot.OnSavePointStored  += (FileName, V, E, ME, HE, T) => Console.WriteLine(V + " vertices and " + E + " edges stored into file '" + FileName + " in " + T + "ms!");

            SnapShot.TryLoad();

            #endregion

            #region Init and start GraphDevroom HTTP server

            var _GraphDevroomHTTPServer = new GraphDevroomHTTPAPI(Graph: graph);

            #region Setup HTTP server service/access/error log

            //_GraphDevroomHTTPServer.ServiceLog += (time, message) => {

            //    Console.WriteLine(message);

            //};

            //_GraphDevroomHTTPServer.AccessLog += (time, request, response) => {

            //    Console.WriteLine(request.RemoteHost + ":" +
            //                      request.RemotePort + " - " +
            //                      request.HTTPMethod + " " +
            //                      request.UrlPath    + " (" +
            //                      ((request.ContentType == null) ? "" : request.ContentType.MediaType) + " > " +
            //                      request.BestMatchingAcceptType.MediaType + ") => " +
            //                      response.HTTPStatusCode + " " +
            //                      response.ContentLength + " bytes");

            //};

            //_GraphDevroomHTTPServer.ErrorLog += (time, request, response, error, exception) => {

            //    var _error            = (error     == null) ? "" : error;
            //    var _exceptionMessage = (exception == null) ? "" : Environment.NewLine + exception.Message;

            //    Console.WriteLine(request.RemoteHost + ":" +
            //                      request.RemotePort + " - " +
            //                      request.HTTPMethod + " " +
            //                      request.UrlPath    + " => " + _error + _exceptionMessage);

            //};

            #endregion

            #region Attach HTTP basic authentication

            //_GraphDevroomHTTPServer.UserAuthentication.OnUsernamePasswordCheck += (username, password) => {

            //    if (username != "test")
            //        return false;

            //    if (password != "1234")
            //        return false;

            //    return true;

            //};

            #endregion

            _GraphDevroomHTTPServer.Start();

            #endregion

            while (true)
                Thread.Sleep(100);

        }

    }

}
