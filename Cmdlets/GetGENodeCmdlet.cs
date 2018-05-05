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
        public string query;

        protected override void BeginProcessing()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_BindingRedirect;
            base.BeginProcessing();
        }

        private Assembly CurrentDomain_BindingRedirect(object sender, ResolveEventArgs args)
        {
            var name = new AssemblyName(args.Name);      
            switch (name.Name)
            {
                case "Newtonsoft.Json":
                    return typeof(Newtonsoft.Json.JsonSerializer).Assembly;
                case "System.Collections.Immutable":
                    return Assembly.LoadFrom("System.Collections.Immutable.dll");
                default:
                    return null;
            }
            //throw new NotImplementedException();
        }

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
            var r = LambdaDSL.Evaluate(query);

            WriteObject(r);
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