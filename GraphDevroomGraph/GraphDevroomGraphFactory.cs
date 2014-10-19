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

using org.GraphDefined.Vanaheimr.Balder;
using org.GraphDefined.Vanaheimr.Balder.InMemory;

#endregion

namespace org.GraphDevroom.Graph
{

    /// <summary>
    /// Simplified creation of GraphDevroom property graphs.
    /// </summary>
    public static class GraphDevroomGraphFactory
    {

        /// <summary>
        /// Create a new GraphDevroom property graph.
        /// </summary>
        /// <param name="GraphId">The optional graph identification. If no value is given, a unique GraphId will be generated.</param>
        /// <param name="Description">The optional description of the graph.</param>
        /// <param name="GraphInitializer">The optional graph initializer.</param>
        public static IGenericPropertyGraph<UInt64, Int64, GDVertexLabel,    String, Object,
                                            UInt64, Int64, GDEdgeLabel,      String, Object,
                                            UInt64, Int64, GDMultiEdgeLabel, String, Object,
                                            UInt64, Int64, GDHyperEdgeLabel, String, Object>

            Create(UInt64                                                             GraphId,
                   String                                                             Description,
                   GraphInitializer<UInt64, Int64, GDVertexLabel,    String, Object,
                                    UInt64, Int64, GDEdgeLabel,      String, Object,
                                    UInt64, Int64, GDMultiEdgeLabel, String, Object,
                                    UInt64, Int64, GDHyperEdgeLabel, String, Object>  GraphInitializer = null)

        {

            return GraphFactory.CreateLabeledPropertyGraph<GDVertexLabel,
                                                           GDEdgeLabel,
                                                           GDMultiEdgeLabel,
                                                           GDHyperEdgeLabel>(
                                                               GraphId,
                                                               Description,
                                                               GDVertexLabel.   DEFAULT,
                                                               GDEdgeLabel.     DEFAULT,
                                                               GDMultiEdgeLabel.DEFAULT,
                                                               GDHyperEdgeLabel.DEFAULT,
                                                               GraphInitializer);

        }

    }

}
