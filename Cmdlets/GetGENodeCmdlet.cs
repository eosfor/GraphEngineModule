using System.Collections.Generic;
using System.Management.Automation;
using Trinity;
using Trinity.Storage;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using FanoutSearch.LIKQ;
using System.Linq;
using System;
using System.Reflection;
using Trinity.Network;
using FanoutSearch;
using Trinity.Storage.Composite;

//using System.Linq;
//using System.ComponentModel.DataAnnotations.Schema;


namespace GraphEngineModule
{
    [Cmdlet("Get", "GEVertex")]
    [Alias("Get-Vertex")]
    public class GetGEVertexCmdlet : TrinityBaseCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "ByID")]
        public long NodeID;

        [Parameter(Mandatory = true, ParameterSetName = "ByTypeName")]
        public string TypeName;

        [Parameter(Mandatory = true, ParameterSetName = "ByQuery")]
        public string ParameterName;

        [Parameter(Mandatory = true, ParameterSetName = "ByQuery")]
        public string ParameterValue;

        [Parameter(Mandatory = true, ParameterSetName = "ByLIKQQuery")]
        public long StartFromID;

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case "ByID":
                    ByID();
                    break;
                case "ByTypeName" :
                    ByTypeName();
                    break;
                case "ByQuery" :
                    ByQuery();
                    break;
                case "ByLIKQQuery":
                    ByLIKQQuery();
                    break;
                default:
                    break;
            }
        }

        private void ByLIKQQuery()
        {
            var t = KnowledgeGraph.StartFrom(StartFromID)
                .FollowEdge("OutEdge")
                .VisitNode(_ => FanoutSearch.Action.Return);
            var r = t.ToList();

            WriteObject(r);
            //throw new NotImplementedException();
        }

        private void ByID()
        {

            IEnumerable<ICell> result = from node in Global.LocalStorage.GenericCell_Selector()
                                        where node.CellId == NodeID
                                        select node;
            WriteObject(result, true);     
        }

        private void ByTypeName ()
        {
            IEnumerable<ICell> result = from node in Global.LocalStorage.GenericCell_Selector()
                                        where node.TypeName == TypeName
                                        select node;


            WriteObject(result,true);
        }

        private void ByQuery()
        {

            // get all available types
            var types = Global.StorageSchema.CellDescriptors.ToArray();

            // filter only types containing field name we need
            var typesFiltered = types
                .Where(x => x.GetFieldNames().Contains(ParameterName))
                .Select(x => x.Type)
                .ToArray();

            // filter only cells of the specific types having specific value of the specified field
            var list = Global.LocalStorage.GenericCell_Selector().AsQueryable();
            var result = list
                .Where(x => typesFiltered.Contains(x.Type))
                .Where(x => x.GetField<string>(ParameterName) == ParameterValue)
                .ToArray();

            WriteObject(result, true);
        }
    }
}
